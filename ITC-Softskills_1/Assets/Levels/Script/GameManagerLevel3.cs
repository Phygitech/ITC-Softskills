using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartLocalization;
using DG.Tweening;
using UnityEngine.UI;

public class GameManagerLevel3 : MonoBehaviour {
    public static GameManagerLevel3 instance;

    public GameObject Vr_player;
    public GameObject Score,Timer;
    public Animation Dhuadhar_idle, Dhuadhar_Axn;
    public GameObject SelectContinueIns,Btn_Continue,SelectOptIns,SelectConProceed;
    [Header("HairstyleAct")]
    public GameObject[] Options;
    public GameObject[] Lables;
    bool HeadRotate;
    static int ActCounter;
    public GameObject[] Heads;
    public GameObject[] HeadHighLight;
    [Header("FacialHair")]
    public GameObject[] FacialHair;
    bool FaceRotate;
    [Header("Nails")]
    public GameObject[] Nails;

    [Header("Body Jewell")]
    public GameObject[] BodyJewel;
    bool JewelRotate;

    public Animation[] MausiRxnAnim;
    public GameObject LoaferAnim, WatchAnim, WellFittedWatchAnim, TuckShirtAnim;
    [Header("Models")]
    public GameObject[] Names;
    public GameObject[] Models;
    public GameObject[] ModelsIntro;

    public GameObject TipsBtn,Tip1,Tip2,TipsIns,Tip3,Tip4;
    public GameObject Continue1,CharIns,ChooseChar;
	public bool IsMashal,IsAgni,IsDhur;
	public GameObject[] DupIdle;

	//18 Decemeber 2018.............
	public GameObject GoodAveragePoorAppriciation;
	public Text YourScore, Appriciation;
	public GameObject L1_Inst_DhuadharIntro;
	//Used for auto skipped the option in level 3
	public int OptionCounter;
	// Use this for initialization
	void Start () 
    {
		OptionCounter = 0;
        instance = this;
        ActCounter = -1;
        Invoke("Play_Dhuadhar_Axn_Anim",.001f);
       // Play_Dhuadhar_Axn_Anim();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (HeadRotate)
        {
            for (int i = 0; i < Heads.Length; i++)
            {
//                Heads[i].transform.Rotate(0, Time.timeScale * .5f, 0);
//				Heads[i].transform.DORotate(new Vector3(0,60,0),1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
            }
        }

        if(FaceRotate)
        {
            for (int i = 0; i < FacialHair.Length; i++)
            {
//                FacialHair[i].transform.Rotate(0, Time.timeScale * .5f, 0);
            }
        }
        if(JewelRotate)
        {
            for (int i = 2 ; i < BodyJewel.Length; i++)
            {
				//if(i!= 0 || i!=1)
                BodyJewel[i].transform.Rotate(0, Time.timeScale * .5f, 0);
            }
        }
	}

   void Play_Dhuadhar_Axn_Anim()
    {
        Dhuadhar_Axn.Play();
        LanguageHandler.instance.PlayVoiceOver("LookingIntoMirrorVO");
    }

    public void ClickContinue()
    {
        Vr_player.transform.DORotate(new Vector3(0,130,0),2f).OnComplete(AfterContinuelick);
    }

    void AfterContinuelick()
    {
        Dhuadhar_Axn.gameObject.SetActive(false);
        Dhuadhar_idle.gameObject.SetActive(true);
        Dhuadhar_idle.Play();
        ActCounter++;
        Options[ActCounter].SetActive(true);
        Lables[ActCounter].SetActive(true);
        HeadRotate = true;
        SelectOptIns.SetActive(true);
    }

    public void OnHeadClick(int n)
    {
		L3_Timer.ins.StopAllCoroutines ();
//        SoundManager.instance.PlayClickSound();
        HeadHighLight[n].SetActive(true);
        for(int i=0;i<Heads.Length;i++)
        {
            Heads[i].GetComponentInChildren<Collider>().enabled = false;
        }

        Invoke("DisAppearHeads",2f);
    }

    public void DisAppearHeads()
    {
		OptionCounter++;
        HeadRotate = false;
        FaceRotate = true;

        Lables[ActCounter].SetActive(false);
        ActCounter++;
        Options[ActCounter].SetActive(true);
        Lables[ActCounter].SetActive(true);

        for(int i=0;i<Heads.Length;i++)
        {
            Heads[i].SetActive(false);
            HeadHighLight[i].SetActive(false);
        }
    }

    public void OnFacial(int n)
    {
		L3_Timer.ins.StopAllCoroutines ();
//        SoundManager.instance.PlayClickSound();
        HeadHighLight[n].SetActive(true);
        for(int i=0;i<FacialHair.Length;i++)
        {
            FacialHair[i].GetComponentInChildren<Collider>().enabled = false;

        }

        Invoke("DisAppearFace",2f);
    }

   	public void DisAppearFace()
    {
		OptionCounter++;
        FaceRotate = false;
        JewelRotate = true;
        Lables[ActCounter].SetActive(false);
        ActCounter++;
        Options[ActCounter].SetActive(true);
        Lables[ActCounter].SetActive(true);
        for(int i=0;i<FacialHair.Length;i++)
        {
            FacialHair[i].SetActive(false);
            HeadHighLight[i].SetActive(false);
        }
    }

    public void SelectBodyJewel(int n)
    {
		L3_Timer.ins.StopAllCoroutines ();
//        SoundManager.instance.PlayClickSound();
        HeadHighLight[n].SetActive(true);
        for(int i=0;i<BodyJewel.Length;i++)
        {
            BodyJewel[i].GetComponentInChildren<Collider>().enabled = false;   
        }

        Invoke("DisAppearBodyJewel",2f);

    }

    public void DisAppearBodyJewel()
    {
		OptionCounter++;
        JewelRotate = false;
        Lables[ActCounter].SetActive(false);
        ActCounter++;
        Options[ActCounter].SetActive(true);
        Lables[ActCounter].SetActive(true);
        for(int i=0;i<BodyJewel.Length;i++)
        {
            BodyJewel[i].SetActive(false);
            HeadHighLight[i].SetActive(false);
        }

    }



    public void SelectNails(int n)
    {
		L3_Timer.ins.StopAllCoroutines ();
//        SoundManager.instance.PlayClickSound();
        Nails[n].transform.GetChild(0).gameObject.SetActive(true);
        for(int i=0;i<Nails.Length;i++)
        {
            Nails[i].GetComponent<Collider>().enabled=false;
        }
       
        Invoke("FinalScoreDisplay",2f);
    }


    public void FinalScoreDisplay()
    {
		OptionCounter++;
        Lables[ActCounter].SetActive(false);
        SelectOptIns.SetActive(false);
        for(int i=0;i<Nails.Length;i++)
        {
            Nails[i].gameObject.SetActive(false);
        }

        int _score = PlayerPrefs.GetInt("Score");
		GoodAveragePoorAppriciation.SetActive (true);
		YourScore.text = ""+PlayerPrefs.GetInt("Score");
        if (_score >= 30 && _score <= 40)
        {
			Appriciation.text = "Good";
            MausiRxnAnim[0].gameObject.SetActive(true);
			LanguageHandler.instance.PlayVoiceOver ("Clapping_mausi");
        }
        else if(_score >= 25 && _score < 30)
        {
            LanguageHandler.instance.PlayVoiceOver("AverageVO");
            MausiRxnAnim[1].gameObject.SetActive(true);
			Appriciation.text = "Average";
        }
        else if(_score < 25)
        {
            MausiRxnAnim[2].gameObject.SetActive(true);
			LanguageHandler.instance.PlayVoiceOver ("@_Chi Chi_Mausi");
			Appriciation.text = "Poor";
        }

		ContentProvider.instance.UpdateScore (_score);
        Debug.Log("Testing " + _score);
        ContentProvider.instance.score3 = _score;
    }

//	int Char_counter=0;

    public void SelectModel(int n)
    {
//			SoundManager.instance.PlayClickSound();
			for (int i = 0; i < 3; i++)
			{
				Models[i].SetActive(false);
				Names[i].SetActive(false);
				if (i == n)
				{
					ModelsIntro[i].SetActive(true);
					if (i == 0)
					{
						LanguageHandler.instance.PlayVoiceOver("MashalIntroVo");
					}
					else if(i==1)
					{
						LanguageHandler.instance.PlayVoiceOver("AgnintroVO");
					}
					else if(i==2)
					{
						LanguageHandler.instance.PlayVoiceOver("DhundaIntroVO");
//					    L1_Inst_DhuadharIntro.SetActive (true);
					}
				}
			}
			
    }


    public void ClickOnContinue()
    {
        //MapManager.instance_cps = MapManager.checkPts.cp4;
        PlayerPrefs.SetInt("checkPts", 4);
		PlayerPrefs.SetInt ("score", ContentProvider.instance.totalGainedScore);
        SceneLoader.instance.LoadScene(1);
    }

	public void ClickOnTip()
	{
		GoodAveragePoorAppriciation.SetActive (false);
		SoundManager.instance.PlayClickSound ();
		for(int i=0;i<3;i++)
		{
			MausiRxnAnim[i].gameObject.SetActive(false); 
		}
		TipsBtn.SetActive (false);
		TipsIns.SetActive (false);
		Tip1.SetActive (true);
		LoaferAnim.SetActive(true);
		LanguageHandler.instance.PlayVoiceOver("LoaferVO");
	}
}
