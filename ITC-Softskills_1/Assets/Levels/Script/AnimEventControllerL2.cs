using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventControllerL2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisableWardrobeDoor()
    {
        for (int i = 0; i < 2; i++)
        {
            GameManagerLevel2.instance.WardRobeDoor[i].SetActive(false);
        }
    }

    public void AfterReachingwardrobe()
    {
       // GameManagerLevel2.Counter++;
        GameManagerLevel2.instance.Activity[++GameManagerLevel2.Activity_Counter].SetActive(true);
        GameManagerLevel2.instance.Labels[++GameManagerLevel2.Label_Counter].SetActive(true);
        LanguageHandler.instance.PlayVoiceOver("ChoseVO");

    }

    public void MausiAnimTip1()
    {
		
		Invoke ("_DelayInTip", 2f);
//        GameManagerLevel2.instance.Tips[0].SetActive(true);
//        LanguageHandler.instance.PlayVoiceOver("UniformVo");
//        for(int i=0;i<3;i++)
//        {
//            GameManagerLevel2.instance.Mausi_Responce_Anim[i].SetActive(false);
//        }

    }
	void _DelayInTip(){
		GameManagerLevel2.instance.TipsBtn.SetActive(true);
		GameManagerLevel2.instance.TipsIns.SetActive (true);
	}

    public void MausiAnimTip2()
    {
		Invoke ("_MausiAnimTip2",1f);
    }

	void _MausiAnimTip2()
	{
		GameManagerLevel2.instance.Tips[0].SetActive(false);
		GameManagerLevel2.instance.Tip1.SetActive (false);
		GameManagerLevel2.instance.Tip2.SetActive (true);
		GameManagerLevel2.instance.Tips[1].SetActive(true);
		LanguageHandler.instance.PlayVoiceOver("SocksVO");
	}
		
	public void MausiAnimTip3(){
		Invoke ("_mausiAnimtip3",1f);
	}
	void _mausiAnimtip3(){
		GameManagerLevel2.instance.Tips[1].SetActive(false);
		GameManagerLevel2.instance.Tip2.SetActive (false);
		GameManagerLevel2.instance.Tip3.SetActive (true);
		GameManagerLevel2.instance.Tips[2].SetActive(true);
		LanguageHandler.instance.PlayVoiceOver("@_mausi_shoes_without_socks");
	}	

	public void MausiAnimTip4(){
		Invoke ("_mausiAnimtip4",1f);
	}
	void _mausiAnimtip4(){
		GameManagerLevel2.instance.Tips[2].SetActive(false);
		GameManagerLevel2.instance.Tip3.SetActive (false);
		GameManagerLevel2.instance.Tip4.SetActive (true);
		GameManagerLevel2.instance.Tips[3].SetActive(true);
		LanguageHandler.instance.PlayVoiceOver("@_mausi_ganjis");
	}


    public void EnableContinue()
    {
		Invoke ("DelayContinue", 2f);
    }
	void DelayContinue(){
		GameManagerLevel2.instance.Tip4.SetActive(false);
		GameManagerLevel2.instance.Tips[3].SetActive(false);
		GameManagerLevel2.instance.Continue_Btn.SetActive(true);
		GameManagerLevel2.instance.Continue_Ins.SetActive(true);
	}
}
