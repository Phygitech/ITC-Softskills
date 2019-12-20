using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4_GameManager : MonoBehaviour {

    public static L4_GameManager instance;
    public GameObject tips_btn,tips_1,tips_2,tips_inst,tips_3,tips_4;
    public GameObject anim_parent;
    public List<GameObject>anim_characters;
    public List<GameObject> mausiTips;
    public GameObject L4_inst_ModuleComplete;
    string name;
    public GameObject[] objectList;
    public GameObject almirahDoor1;
    public GameObject almirahDoor2;
	//Object Present on table............
	public GameObject BreakFast,Charger_Set,Cup,Hand_Bag,Tiffin,Towel,Water_Bottel,Shirt;

	public Animation [] UserActivities;
	public GameObject VrPlayer;
	// Use this for initialization
	void Start ()
    {
        //PlayerPrefs.DeleteAll();
        instance = this;
        name="Dhuandhar";
        PlayAnimation();
	}

	
    void PlayAnimation()
    {
        switch (name)
        {
            case "Agnikanth":
                
                anim_characters.Find(x => x.tag == "Agnikanth").transform.SetParent(anim_parent.transform);
                anim_parent.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                LanguageHandler.instance.PlayVoiceOver("Vo_Agnikanth");
                break;

            case "MashaalSingh":
                anim_characters.Find(x => x.tag == "MashaalSingh").transform.SetParent(anim_parent.transform);
                anim_parent.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                LanguageHandler.instance.PlayVoiceOver("Vo_MashaalSingh");
                break;
            case "Dhuandhar":
                anim_characters.Find(x => x.tag == "Dhuandhar").transform.SetParent(anim_parent.transform);
                anim_parent.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
			LanguageHandler.instance.PlayVoiceOver("VO_Dhuadhar_tea");
                break;
        }

		StartCoroutine(DhuadharActivities(UserActivities[0].clip.length));
        
    }



    IEnumerator DhuadharActivities(float t)
    {
        yield return new WaitForSeconds(t+2.5f);


	    anim_parent.transform.GetChild(0).GetChild(0).transform.localScale=new Vector3(0,0,0);
	    anim_parent.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);				
		LanguageHandler.instance.PlayVoiceOver("VO_Dhuadhar_Shirt"); 
		yield return new WaitForSeconds(UserActivities[1].clip.length +2f);
		Shirt.SetActive (true);
		anim_parent.transform.GetChild(0).GetChild(1).transform.localScale=new Vector3(0,0,0);
		anim_parent.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);				
		LanguageHandler.instance.PlayVoiceOver("VO_Dhuadhar_ShoePolish"); 
		Charger_Set.SetActive (true);
		Hand_Bag.SetActive (true);
		yield return new WaitForSeconds(UserActivities[2].clip.length+2f);
		anim_parent.transform.GetChild(0).GetChild(2).transform.localScale=new Vector3(0,0,0);
		anim_parent.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);				
		LanguageHandler.instance.PlayVoiceOver("Vo_Dhuandhar_Sleep"); 
		yield return new WaitForSeconds(UserActivities[3].clip.length+2f);
		anim_parent.transform.GetChild(0).GetChild(3).transform.localScale=new Vector3(0,0,0);
		anim_parent.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);				
		LanguageHandler.instance.PlayVoiceOver("Vo_Dhuandhar_WakeUp"); 
		yield return new WaitForSeconds(UserActivities[4].clip.length+2f);
		Shirt.SetActive (false);
		anim_parent.transform.GetChild(0).GetChild(4).transform.localScale=new Vector3(0,0,0);
		anim_parent.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);				
		LanguageHandler.instance.PlayVoiceOver("Vo_Dhuandhar_Eating");
		BreakFast.SetActive (true);
		Cup.SetActive (true);
		Tiffin.SetActive (true);
		Water_Bottel.SetActive (true);
		Towel.SetActive (true);
		yield return new WaitForSeconds(UserActivities[5].clip.length+2f);
		anim_parent.transform.GetChild(0).GetChild(5).transform.localScale=new Vector3(0,0,0);
		anim_parent.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);	
		almirahDoor1.SetActive(false);
		almirahDoor2.SetActive(false);
		LanguageHandler.instance.PlayVoiceOver("Vo_Dhuandhar_dressed"); 
		BreakFast.SetActive (false);
		Cup.SetActive (false);
		yield return new WaitForSeconds(UserActivities[6].clip.length+2f);
		VrPlayer.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 0));
		Camera.main.transform.rotation = Quaternion.identity;
		anim_parent.transform.GetChild(0).GetChild(6).transform.localScale=new Vector3(0,0,0);
		anim_parent.transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
		almirahDoor1.SetActive(true);
		almirahDoor2.SetActive(true);
		LanguageHandler.instance.PlayVoiceOver("Vo_Dhuandhar_LeavesForWork"); 
		Charger_Set.SetActive (false);
		Hand_Bag.SetActive (false);
		Tiffin.SetActive (false);
		Water_Bottel.SetActive (false);

		StartCoroutine(AfterAnimOver(GlobalAudioSrc.Instance.audioSrc.clip.length + 3f));
    }

    IEnumerator AfterAnimOver(float t)
    {
        yield return new WaitForSeconds(t);
        foreach (GameObject anim in anim_characters)
            anim.SetActive(false);
        
        mausiTips[0].SetActive(true);
        LanguageHandler.instance.PlayVoiceOver("Vo_mausi_DekhBeta");

        yield return new WaitForSeconds(GlobalAudioSrc.Instance.audioSrc.clip.length+2);
		tips_btn.SetActive(true);
		tips_inst.SetActive (true);
  
    }

	public void ClickOnTipBtn()
	{
		tips_btn.SetActive (false);
		tips_inst.SetActive (false);
		tips_1.SetActive (true);
		mausiTips[0].SetActive(false);
		StartCoroutine (Tips());
	}

	IEnumerator Tips()
	{
		        mausiTips[1].SetActive(true);
		        LanguageHandler.instance.PlayVoiceOver("Vo_mausi_nails");
			yield return new WaitForSeconds(GlobalAudioSrc.Instance.audioSrc.clip.length+1f);
				tips_1.SetActive (false);
				tips_2.SetActive (true);
		        mausiTips[1].SetActive(false);
		        mausiTips[2].SetActive(true);
		        LanguageHandler.instance.PlayVoiceOver("Vo_mausi_MouthFreshner");
		        yield return new WaitForSeconds(GlobalAudioSrc.Instance.audioSrc.clip.length+1f);
		  		tips_2.SetActive (false);
				tips_3.SetActive (true);
				mausiTips [2].SetActive (false);
				mausiTips [3].SetActive (true);
				LanguageHandler.instance.PlayVoiceOver ("VO_ChewingGum_Mausi");
				yield return new WaitForSeconds (GlobalAudioSrc.Instance.audioSrc.clip.length + 1f);
				tips_3.SetActive (false);
				tips_4.SetActive (true);
				mausiTips [3].SetActive (false);
				mausiTips [4].SetActive (true);
				LanguageHandler.instance.PlayVoiceOver ("VO_Gentle_gutkha");
				yield return new WaitForSeconds (GlobalAudioSrc.Instance.audioSrc.clip.length + 1f);
				tips_4.SetActive (false);

		        LastInst();
	}



    void LastInst()
    {
        foreach (GameObject mausi in mausiTips)
            mausi.SetActive(false);
       
        L4_inst_ModuleComplete.SetActive(true);
    }
	
    public void ClickOnContinue()
    {
       // MapManager.instance_cps = MapManager.checkPts.cp5;
     //   PlayerPrefs.SetInt("checkPts", 0);
		PlayerPrefs.SetInt ("score", 0);
        SceneLoader.instance.LoadScene(1);
    }




}
