using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentProvider : MonoBehaviour {

	AndroidJavaObject jo;
	public Text _userName;
	public Text _currentTime;

	public Text getText;
	public Text saveText;

	public static ContentProvider instance;
	public static bool isLoggedIn = false;

	public int tempValue;

	public int totalScore;
	public int totalGainedScore;

	[HideInInspector]
	public string currentTime;
	[HideInInspector]
	public string mobileNo;
	[HideInInspector]
	public string userId;
	[HideInInspector]
	public string user_Name;
    public int score1;
    public int score2;
    public int score3;

	[HideInInspector]
	public bool isFirstMonth;

	void Awake()
	{
		DontDestroyOnLoad (this);

		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (gameObject);
		} else
			instance = this;

		totalScore = 350;

		if (PlayerPrefs.HasKey ("score")) 
		{
			totalGainedScore = PlayerPrefs.GetInt ("score");
		} else
			totalGainedScore = 0;

#if UNITY_ANDROID && !UNITY_EDITOR
        Init();
#endif
    }

    public void UpdateScore (int _totalGainedScore)
	{
		totalGainedScore += _totalGainedScore;
		Debug.Log ("total score " + totalScore + ", total gained score " + totalGainedScore);
	}

	public float CalculatePercentage ()
	{
		float percentage = (float)(totalGainedScore * 100) / totalScore;

		Debug.Log ("score in percentage " + percentage);
		return percentage;
	}

    public float Calculate_Percentage(int totalScore, int score)
    {
        float percentage = (float)(score * 100) / totalScore;

        Debug.Log("score in percentage " + percentage);
        return percentage;
    }

    void Init()
	{
		jo = new AndroidJavaObject("com.example.dbprovider.KVDBHandler");
		jo.Call("Init");
	}


	public void SetKey(string Key,string Value)
	{
		jo.Call("SetKey", Key, Value);
	}


	public bool HasKey(string Key)
	{
		return jo.Call<bool>("HasKey", Key);
	}


	public string GetValue(string Key)
	{
		return jo.Call<string>("GetValue", Key);
	}


	public void DeleteKey(string Key)
	{
		jo.Call("DeleteKey", Key);
	}


	public void DeleteAllKey()
	{
		jo.Call("DeleteAllKey");
	}

	public void GetSavedData ()
	{
		_userName.text = GetValue ("user_id");
		_currentTime.text = GetValue ("time");
	}

	private void Start()
	{
       // PlayerPrefs.SetInt("checkPts", 1);

      //  LoginManager.instance.gamePlayMainMenuPane.SetActive (false);

		if (Numpad_Manager.instance != null)
		Numpad_Manager.instance.gameObject.SetActive (false);

		Debug.Log ("before start");



#if UNITY_ANDROID && !UNITY_EDITOR
        if (HasKey("current_module"))
        {
            if (GetValue("current_module") == "")
            {
                SetKey("current_module", "com.test.bbg");
            }
        }
        else
            SetKey("current_module", "com.test.bbg");


        if (HasKey ("user_id")) 
        {
            if (!string.IsNullOrEmpty (GetValue ("user_id"))) 
            {
                // user id exist in database, check for login validation time
                LoginManager.instance.ValidateMobNumber(false);
             //   LoginManager.instance.gamePlayMainMenuPane.SetActive (true);
                if (Numpad_Manager.instance != null)
                    Numpad_Manager.instance.gameObject.SetActive(false);
            }
            else
            {
                // user id doesnt exist in database, show login panel
                LoginManager.instance.loginViaNoPanel.SetActive(true);
            }
        }
        else
        {
            // there is not key named user_id, show login panel
            LoginManager.instance.loginViaNoPanel.SetActive(true);
		Debug.Log ("C01- Panel Enabled");
        }
		Debug.Log ("C02- ");
#elif UNITY_EDITOR
        //if (!HasKey ("current_module")) 
        if (PlayerPrefs.HasKey ("current_module")) {
			if (PlayerPrefs.GetString ("current_module") == "") {
				//SetKey("current_module","com.test.cd.itc_1");
				PlayerPrefs.SetString ("current_module", "com.test.bbg");
			}
		}
		else
			PlayerPrefs.SetString ("current_module", "com.test.bbg");

        Debug.Log("player prefab module value " + PlayerPrefs.GetString("current_module"));

        //  if (HasKey ("user_id")) 
        if (PlayerPrefs.HasKey("user_id"))
        {
            //if (!string.IsNullOrEmpty (GetValue ("user_id"))) 
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString("user_id")))
            {
                // user id exist in database, check for login validation time
             //   LoginManager.instance.ValidateMobNumber(false);
             //   LoginManager.instance.gamePlayMainMenuPane.SetActive (true);
                Debug.Log("user id " + PlayerPrefs.GetString("user_id"));
                if (Numpad_Manager.instance != null)
                    Numpad_Manager.instance.gameObject.SetActive(false);
            }
            else
            {
                // user id doesnt exist in database, show login panel
             //   LoginManager.instance.loginViaNoPanel.SetActive(true);
                Debug.Log("user id " + PlayerPrefs.GetString("user_id"));
            }
        }
        else
        {
            // there is not key named user_id, show login panel
          //  LoginManager.instance.loginViaNoPanel.SetActive(true);
            Debug.Log("user id " + PlayerPrefs.GetString("user_id"));
        }
        //PlayerPrefs.DeleteAll ();

#endif


    }

    public void OnGetData ()
	{
	//	text.text = GetValue ("test") + "";
		Debug.Log ("Get value " + GetValue ("test"));
		getText.text = GetValue ("test") + "";
	}
	/*
	public void OnSaveData ()
	{
		string data = inputField.text.ToString ();
		SetKey ("test", data);
		Debug.Log ("Set value " + data);
		saveText.text = data;
	}*/
}


//GetValue ("user_id")
//GetValue ("time")
//GetValue ("mobile_no")
//GetValue ("current_module")
//("firstLoginTime")
//("language") = "hindi" "english"

//total earned score (int)-  "score"