using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackMenu : MonoBehaviour
{
    public static BackMenu instance;

    AudioSource[ ] _AllAudioSrc;
    List <bool> audioPlaying;

    public GameObject _Menu, _QuitMenu, _CameraFade;

    [Tooltip("Set object's scale 0. Put Canvas and Scalable GameObjects here.")]
    public GameObject[] _HideObjects;
    Vector3[] _HideObjectsScale;
    [Tooltip("Disable object in scene. Put Halo, Line Renderer etc here.")]
    public GameObject[] _DisableObjects;
    bool[] _DisableObjectsState;

    public bool IsBackMenuEnabled;
    float _dist = 1f;

    public bool mode;
    GameObject CamY;
    Camera MainCam;

    void Awake()
    {
        instance = this;
        audioPlaying = new List<bool>();
        audioPlaying.Clear();
        _HideObjectsScale = new Vector3[_HideObjects.Length];
        _DisableObjectsState = new bool[_DisableObjects.Length];
        _Menu.SetActive(false);
        _QuitMenu.SetActive(false);
        _CameraFade.SetActive(false);
        CamY = new GameObject("CamY");
        MainCam = Camera.main;
        CamY.transform.SetParent(MainCam.transform.parent);
        UpdateCamY();
    }

    void UpdateCamY()
    {
        CamY.transform.position = MainCam.transform.position;
        CamY.transform.eulerAngles = new Vector3(0, MainCam.transform.eulerAngles.y, 0);
    }

    void Start()
    {
        #if !UNITY_EDITOR
        mode = PlayerPrefs.GetInt ( "mode" ) != 0 ? true : false ;
        #endif

        if (mode)
        {
            _Menu.transform.Find("C_Btn_Quit").gameObject.SetActive(false);
        }
        else
        {
            _Menu.transform.Find("C_Btn_Quit").gameObject.SetActive(true);
        }
    }

    bool OkClicked = false;

    void LateUpdate()
    {
        if (mode)
            return;

        UpdateCamY();
        if (Input.GetButton("Fire1") || GvrControllerInput.ClickButton || GvrControllerInput.ClickButtonDown)
            return;

        if (Input.anyKeyDown && Input.inputString.Equals(System.Environment.NewLine) && !OkClicked)
            OkClicked = true;

        if (!Input.anyKey)
            OkClicked = false;

        if (Input.GetButton("Fire1") || GvrController.ClickButton || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
            return;

        if (!OkClicked)
        {            
            if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.JoystickButton1) || GvrController.AppButtonDown)
            {
                if (PlayPauseSimulation.instance != null && PlayPauseSimulation.instance.isPaused)
                    return;

                if (ChatbotManager.Instance.chatBotState != ChatbotState.Active)
                    ToggleBackMenu();
            }
        }

        _CameraFade.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0);
        _CameraFade.transform.position = Camera.main.transform.position + Camera.main.transform.forward * (_dist + 0.1f);
    }




    public void ToggleBackMenu()
    {
        if (IsBackMenuEnabled)
            ResumeLevel();
        else
            EnableBackMenu();
    }

    void PauseSimulation()
    {
        Time.timeScale = 0;
        _AllAudioSrc = Resources.FindObjectsOfTypeAll < AudioSource >();

        if (_AllAudioSrc.Length == 0)
            return;

        audioPlaying.Clear();

        for (int i = 0; i < _AllAudioSrc.Length; i++)
        {
            if (_AllAudioSrc[i].isPlaying)
            {
                audioPlaying.Add(true);
                _AllAudioSrc[i].Pause();
            }
            else
                audioPlaying.Add(false);
        }
    }

    void PlaySimulation()
    {
        Time.timeScale = 1;
        _AllAudioSrc = Resources.FindObjectsOfTypeAll < AudioSource >();

        if (_AllAudioSrc.Length == 0)
            return;

        if (audioPlaying.Count == 0)
            return;

        for (int i = 0; i < _AllAudioSrc.Length; i++)
        {
            if (audioPlaying[i])
            {
                _AllAudioSrc[i].UnPause();
            }
        }
    }

    public void EnableBackMenu()
    {
        IsBackMenuEnabled = true;
        if (Time.timeScale != 0)
            PauseSimulation();

        //transform.eulerAngles = Camera.main.transform.eulerAngles;
        //transform.position = Camera.main.transform.position + CamY.transform.forward * _dist;

        transform.eulerAngles = CamY.transform.eulerAngles;
        transform.position = CamY.transform.position + CamY.transform.forward * _dist;

        _Menu.SetActive(true);
        _QuitMenu.SetActive(false);

        _CameraFade.SetActive(true);

        for (int i = 0; i < _HideObjects.Length; i++)
        {
            _HideObjectsScale[i] = _HideObjects[i].transform.localScale;
        }

        for (int i = 0; i < _HideObjects.Length; i++)
        {
            _HideObjects[i].transform.localScale = Vector3.zero;
        }


        for (int i = 0; i < _DisableObjects.Length; i++)
        {
            _DisableObjectsState[i] = _DisableObjects[i].activeSelf;
        }

        for (int i = 0; i < _DisableObjects.Length; i++)
        {
            _DisableObjects[i].SetActive(false);
        }

        SoundManager.instance.PlayClickSound();
        VrSelector.instance.DisablePhysicsRayCasters();
        VrSelector.instance.DisableUIRayCasters();
        if (UnityEngine.XR.XRSettings.supportedDevices[0] == "daydream" || UnityEngine.XR.XRSettings.supportedDevices[0] == "cardboard")
        {
            GetComponent<GvrPointerGraphicRaycaster>().enabled = true;
        }
        else if (UnityEngine.XR.XRSettings.supportedDevices[0] == "Oculus")
        {
            GetComponent<OVRRaycaster>().enabled = true;
        }
    }


    public void ResetLevel()
    {
        Time.timeScale = 1;
        SoundManager.instance.PlayClickSound();
        LoadingScene.LoadingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void GOHome()
    {
        SoundManager.instance.PlayClickSound();
        #if !UNITY_EDITOR
        System.Diagnostics.Process.GetCurrentProcess().Kill();
        #endif
        Debug.Log("Quit");
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SoundManager.instance.PlayClickSound();
        LoadingScene.LoadingSceneIndex = 1;
    }

    public void ResumeLevel()
    {
        IsBackMenuEnabled = false;

        Time.timeScale = 1;
        SoundManager.instance.PlayClickSound();
        _Menu.SetActive(false);
        _QuitMenu.SetActive(false);
        _CameraFade.SetActive(false);
        PlaySimulation();

        for (int i = 0; i < _HideObjects.Length; i++)
        {
            _HideObjects[i].transform.localScale = _HideObjectsScale[i];
        }

        for (int i = 0; i < _DisableObjects.Length; i++)
        {
            _DisableObjects[i].SetActive(_DisableObjectsState[i]);
        }

        VrSelector.instance.EnablePhysicsRayCasters();
        VrSelector.instance.EnableUIRayCasters();

        LanguageHandler.instance.StopBackMenuVoiceOver();
    }

    public void OpenQuitPanel()
    {
        SoundManager.instance.PlayClickSound();
        _Menu.SetActive(false);
        _QuitMenu.SetActive(true);
    }

    public void CloseQuitPanel()
    {
        SoundManager.instance.PlayClickSound();
        _QuitMenu.SetActive(false);
        _Menu.SetActive(true);
    }
}