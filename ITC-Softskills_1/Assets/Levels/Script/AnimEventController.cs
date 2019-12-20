using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartLocalization;

public class AnimEventController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AfterAxnAnim()
    {
        GameManagerLevel3.instance.SelectContinueIns.SetActive(true);
        GameManagerLevel3.instance.Btn_Continue.SetActive(true);
    }

	void _delayTIP(){
		GameManagerLevel3.instance.TipsBtn.SetActive(true);
		GameManagerLevel3.instance.TipsIns.SetActive (true);
	}
    public void PlayLoaferAnim()
    {
        
        GameManagerLevel3.instance.Score.SetActive(false);
		GameManagerLevel3.instance.Timer.SetActive (false);
//        for(int i=0;i<3;i++)
//        {
//            GameManagerLevel3.instance.MausiRxnAnim[i].gameObject.SetActive(false); 
//        }
//       
//        GameManagerLevel3.instance.LoaferAnim.SetActive(true);
//        LanguageHandler.instance.PlayVoiceOver("LoaferVO");
		Invoke("_delayTIP",2f);

        Debug.Log("loaferAnim");

    }

    public void PlayWatchAnim()
    {
		Invoke ("_PlayWatchAnim",1f);
    }

	void _PlayWatchAnim()
	{
		GameManagerLevel3.instance.Tip1.SetActive (false);
		GameManagerLevel3.instance.Tip2.SetActive (true);
		GameManagerLevel3.instance.LoaferAnim.SetActive(false);
		GameManagerLevel3.instance.WatchAnim.SetActive(true);
		LanguageHandler.instance.PlayVoiceOver("WatchVO");
		Debug.Log("WatchAnim");
	}

	public void PlayWellFittedWatchAnim(){
		Invoke ("PWFWAnim", 1f);
	}
	void PWFWAnim(){
		GameManagerLevel3.instance.Tip2.SetActive (false);
		GameManagerLevel3.instance.Tip3.SetActive (true);
		GameManagerLevel3.instance.WatchAnim.SetActive (false);
		GameManagerLevel3.instance.WellFittedWatchAnim.SetActive (true);
		LanguageHandler.instance.PlayVoiceOver ("mausi_well_fitted_watch");
	}

	public void PlayTuckShirtAnim(){
		Invoke ("_TuckShirt", 1f);
	}
	void _TuckShirt(){
		GameManagerLevel3.instance.Tip3.SetActive (false);
		GameManagerLevel3.instance.Tip4.SetActive (true);
		GameManagerLevel3.instance.WellFittedWatchAnim.SetActive (false);
		GameManagerLevel3.instance.TuckShirtAnim.SetActive (true);
		LanguageHandler.instance.PlayVoiceOver ("@_mausi_Shirt_hanging_out");
	}

    public void AfterWatchAnim()
    {
		Invoke ("_SelectModel", 1f);
    }

	void _SelectModel(){
		GameManagerLevel3.instance.Tip4.SetActive(false);
		GameManagerLevel3.instance.TuckShirtAnim.SetActive(false);
		GameManagerLevel3.instance.SelectModel (2);
	}

    public void AfterIntroAnim()
    {

        GameManagerLevel3.instance.Continue1.SetActive(true);
        GameManagerLevel3.instance.SelectConProceed.SetActive(true);
		
    }
}

