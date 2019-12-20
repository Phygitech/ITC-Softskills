using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using socket.io;
using System.Linq;
using testDatabase;
using LitJson;
using System;


public class ChatData
{
    public string teacherId;
    public string contId;
    public string action;
    public string androidPkg;
};

public class UserData
{
    public string userId;
    public string message;
};

public class SocketIOScript : MonoBehaviour
{
    public static SocketIOScript instance;

//    [HideInInspector]
    public string serverURL = "";
    protected Socket socket = null;
    Camera[] CaptureCameras;
    public RenderTexture screenCaptureTex;
    byte[] imageBytes;
    Texture2D tex;
    String imgToSendInBase64;
    Rect texRect;
    float frameTime;
    UserData userData;
    bool closeImageCapture;
    GameObject captureCam;

    void Awake()
    {
		#if UNITY_EDITOR
        PlayerPrefs.SetString("teacherId", "1095");
		PlayerPrefs.SetString("socketURL", "http://52.5.117.32:3000/");
		#endif
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        CaptureCameras = FindObjectsOfType<Camera>();
        for (int i = 0; i < CaptureCameras.Length; i++)
        {
            captureCam = new GameObject("Capture Camera");

            captureCam.transform.SetParent(CaptureCameras[i].gameObject.transform);
            captureCam.transform.localPosition = Vector3.zero;
            captureCam.transform.localRotation = Quaternion.identity;
	
            captureCam.AddComponent<Camera>();
            captureCam.GetComponent<Camera>().targetTexture = screenCaptureTex;
	
            captureCam.GetComponent<Camera>().clearFlags = CaptureCameras[i].clearFlags;
            if (captureCam.GetComponent<Camera>().clearFlags == CameraClearFlags.Color)
            {
                captureCam.GetComponent<Camera>().backgroundColor = Color.black;
            }
            captureCam.GetComponent<Camera>().cullingMask = CaptureCameras[i].cullingMask;
            captureCam.GetComponent<Camera>().depth = CaptureCameras[i].depth;
            captureCam.GetComponent<Camera>().nearClipPlane = CaptureCameras[i].nearClipPlane;
            captureCam.GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        userData = new UserData();
        tex = new Texture2D(screenCaptureTex.width, screenCaptureTex.height, TextureFormat.RGB24, false);
        texRect = new Rect(0, 0, screenCaptureTex.width, screenCaptureTex.height);
        serverURL=PlayerPrefs.GetString("socketURL");
        DoOpen();

    }

    void SendImage()
    {
        RenderTexture.active = screenCaptureTex;
        tex.ReadPixels(texRect, 0, 0);
        tex.Apply();
        imageBytes = tex.EncodeToJPG();
        imgToSendInBase64 = Convert.ToBase64String(imageBytes);
        Debug.Log("Capturing");
    }

    public void DoOpen()
    {
        if (socket == null)
        {
            socket = Socket.Connect(serverURL);
            socket.On(SystemEvents.connect, () =>
                {
				
                    Debug.Log("Socket.IO connected.");

                });
            socket.On("refresh", (string data) =>
                {

                    Debug.Log("Refresh");
                    StartCaptureImage(data);

                });

            socket.On("image_response", (string data) =>
                {

                    Debug.Log("image_response");
                    CaptureImage(data);

                });

            socket.On("close_content_monitor", (string data) =>
                {

                    Debug.Log("close");
                    StopCaptureImage(data);

                });

            socket.On("class room", (string data) =>
                {

                    Debug.Log("class room");
                    ParseData(data);

                });
        }
			
    }



    void SendChat(string str)
    {
        if (socket != null)
        {
            socket.Emit("class room", str);
        }
    }

    public void ParseData(string data)
    {
		
        JsonData itemData = JsonMapper.ToObject(data);
        ChatData chat = new ChatData();
        chat.action = itemData["action"].ToString();
        chat.androidPkg = itemData["androidPkg"].ToString();
        chat.teacherId = itemData["teacherId"].ToString();
        chat.contId = itemData["contId"].ToString();

        if (chat.teacherId == PlayerPrefs.GetString("teacherId"))
        {
            if (chat.action == "play")
            {
                Time.timeScale = 1;
                if (BackMenu.instance != null && BackMenu.instance.IsBackMenuEnabled)
                {
                    PlayPauseSimulation.instance.OnPlayWeb(0);
                }
                else if (BackMenu.instance != null && !BackMenu.instance.IsBackMenuEnabled)
                {
                    PlayPauseSimulation.instance.OnPlayWeb(1);
                }
                else
                {
                    PlayPauseSimulation.instance.OnPlayWeb(1);
                }

            }
            else if (chat.action == "pause")
            {
                PlayPauseSimulation.instance.OnPauseWeb();
           

            }
            else if (chat.action == "stop")
            {
                Debug.Log("stop");
                Application.Quit();

            }
        }


    }

    public void StartCaptureImage(string data)
    {
        Debug.Log("strat ajgfsagfhvgdkjah captur img");
        JsonData itemData = JsonMapper.ToObject(data);
        UserData userData = new UserData();
        userData.userId = itemData["userIds"].ToString();
        userData.message = itemData["status"].ToString();
        if (userData.userId.Split(",".ToCharArray()).ToList().Contains(DatabaseManager.dbm.userID.ToString()) && userData.message == "True")
        {

            closeImageCapture = false;
            captureCam.SetActive(true);
            SendImage();
            userData.userId = DatabaseManager.dbm.userID.ToString();
            userData.message = imgToSendInBase64;

            string dataTosend = JsonUtility.ToJson(userData);
            socket.EmitJson("content_monitor", dataTosend);

        }
    }

    public void StopCaptureImage(string data)
    {
        JsonData itemData = JsonMapper.ToObject(data);
        UserData userData = new UserData();
        userData.userId = itemData["userIds"].ToString();
        userData.message = itemData["status"].ToString();
        if (userData.userId.Split(",".ToCharArray()).ToList().Contains(DatabaseManager.dbm.userID.ToString()) && userData.message == "True")
        {

            closeImageCapture = true;
            captureCam.SetActive(false);

        }
    }

    public void CaptureImage(string data)
    {

        Debug.Log("Unity Data "+data);
        JsonData itemData = JsonMapper.ToObject(data);
        UserData userData = new UserData();
        userData.userId = itemData["userId"].ToString(); ;
        userData.message = itemData["status"].ToString();
        if (DatabaseManager.dbm.userID.ToString() == userData.userId && userData.message == "True" && !closeImageCapture)
        {
            SendImage();
            userData.userId = DatabaseManager.dbm.userID.ToString();
            userData.message = imgToSendInBase64;

            string dataTosend = JsonUtility.ToJson(userData);
            socket.EmitJson("content_monitor", dataTosend);

        }
    }

}


