using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLevel2 : MonoBehaviour {

    public static GameManagerLevel2 instance;

    public GameObject VrPlayer;
    public GameObject BathRoom;
    public GameObject[] WardRobeDoor;
    public GameObject BedroomOst,MoveToHighlightArea,HighLightVfx;
    public GameObject Continue_Btn, Continue_Ins;
    [Header("Mausi Responce")]
    public GameObject[] Mausi_Responce_Anim;
    [Header("Tips")]
    public GameObject[] Tips;

    [Header("ActivityOption")]
    public GameObject[] Activity;
    public GameObject[] Labels;
    public static int Activity_Counter;
    public static int Label_Counter;
    public GameObject Score,Timer;
    public GameObject TipsBtn,TipsIns,Tip1,Tip2,Tip3,Tip4;
	//18-December '18
	public GameObject GoodAveragePoorAppriciation;
	public Text YourScore,Appriciation;
	// Use this for initialization

	public bool IsTriggered;

    public int _score;
    void Awake()
    {
        instance = this;
    }
	void Start () 
    {
        Activity_Counter = -1;
        Label_Counter = -1;
        Invoke("PlayBathRoomAnim",.02f);
	}
	
    void PlayBathRoomAnim()
    {
        Debug.Log("BathroomAnim");
        BathRoom.SetActive(true);

        LanguageHandler.instance.PlayVoiceOver("BathroomVO");
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void ObjectSelection()
    {
        Activity[Activity_Counter].transform.GetChild(0).gameObject.SetActive(false);
       

    }

	//Ye Click ho rha hai... 
    public void DisableAllColliderSet1(GameObject obj)
    {
		L2_Timer.ins.StopAllCoroutines ();
        Collider[] col=obj.GetComponentsInChildren<Collider>();
        foreach(Collider c in col)
        {
            c.enabled = false;
        }
         
		if (IsTriggered)
			Invoke("EnableSet1Act", 2f);
		else
		{
			Invoke("WrongOnFirstAttempt",2f);
		}


        //Invoke("EnableSet1Act",2f);
    }

	public void WrongOnFirstAttempt()
	{
		IsTriggered = false;
		print("Activity_Counter " + Activity_Counter);
		IsTriggered = false;
		Activity[Activity_Counter].transform.gameObject.SetActive(false);
		Labels[Label_Counter].transform.gameObject.SetActive(false);
		Activity_Counter+=1;
		//If Else used to solve null of Label_Counter...........
		if (Label_Counter % 2 != 0) {
			Label_Counter += 1;		
		} else {
			Label_Counter += 2;
		}
		//......................................................
		if(Activity_Counter <= 7)
		{
			Activity[Activity_Counter].transform.gameObject.SetActive(true);
			Labels[Label_Counter].transform.gameObject.SetActive(true);
		}
		else 
		{
			MausiResponse();
		}

	}

    void EnableSet1Act()
    {
		IsTriggered = false;
//        LanguageHandler.instance.PlayVoiceOver("AppVo");
        Activity[Activity_Counter].transform.GetChild(0).gameObject.SetActive(false);
        Activity[Activity_Counter].transform.GetChild(1).gameObject.SetActive(true);
        Labels[Label_Counter].transform.gameObject.SetActive(false);
        Labels[++Label_Counter].transform.gameObject.SetActive(true);

    }

    public void DisableAllColliderSet2(GameObject obj)
    {
		L2_Timer.ins.StopAllCoroutines ();
        Collider[] col=obj.GetComponentsInChildren<Collider>();
        foreach(Collider c in col)
        {
            c.enabled = false;
        }

        Invoke("EnableSet2Act",2f);
    }

    void EnableSet2Act()
    {
        
		IsTriggered = false;
            if (Activity_Counter < 7)
            {
            Debug.Log("IF " +Activity_Counter);
//            LanguageHandler.instance.PlayVoiceOver("ChoseVO");
            Labels[Label_Counter].transform.gameObject.SetActive(false);
            Activity[Activity_Counter].transform.gameObject.SetActive(false);

                Activity[++Activity_Counter].transform.gameObject.SetActive(true);
                Labels[++Label_Counter].transform.gameObject.SetActive(true);
           
            Debug.Log("If2 " + Activity_Counter);
            }
        else 
        {
            Labels[Label_Counter].transform.gameObject.SetActive(false);
            Activity[Activity_Counter].transform.gameObject.SetActive(false);
            MausiResponse();
        }
      
    }


    public void MausiResponse()
    {
        Debug.Log("Mausi");
		Timer.SetActive (false);
        Score.SetActive(false);
		GoodAveragePoorAppriciation.SetActive(true);
		YourScore.text = ""+PlayerPrefs.GetInt("Score");
        _score = PlayerPrefs.GetInt("Score");
        if(_score >= 130 && _score<=160)
        {
            Mausi_Responce_Anim[0].SetActive(true);
			LanguageHandler.instance.PlayVoiceOver ("Clapping_mausi");
			Appriciation.text = "Good";
        }
        else if(_score < 130 && _score >= 100)
        {
            Mausi_Responce_Anim[1].SetActive(true);
			LanguageHandler.instance.PlayVoiceOver ("mausi_PULL_YOU_SOCKS");
			Appriciation.text = "Average";
        }
        else if(_score < 100)
        {
			Appriciation.text = "Poor";
            Mausi_Responce_Anim[2].SetActive(true);
			LanguageHandler.instance.PlayVoiceOver ("@_Chi Chi_Mausi");
        }

		ContentProvider.instance.UpdateScore (_score);
        ContentProvider.instance.score2 = _score;
    }

    public void ClickOnContinue()
    {
       // MapManager.instance_cps = MapManager.checkPts.cp3;
        PlayerPrefs.SetInt("checkPts", 3);
		PlayerPrefs.SetInt ("score", ContentProvider.instance.totalGainedScore);
        SceneLoader.instance.LoadScene(1);
    }

	public void ClickTip1()
	{
		//18 Decemnber........................................
		GoodAveragePoorAppriciation.SetActive (false);
//		Timer.SetActive (false);
//		Score.SetActive (false);
		//.....................................................
		SoundManager.instance.PlayClickSound ();
		TipsBtn.SetActive (false);
		Tip1.SetActive (true);
		Tips[0].SetActive(true);
		LanguageHandler.instance.PlayVoiceOver("UniformVo");
		TipsIns.SetActive (false);
		for(int i=0;i<3;i++)
		{
			GameManagerLevel2.instance.Mausi_Responce_Anim[i].SetActive(false);
		}
	}

}
