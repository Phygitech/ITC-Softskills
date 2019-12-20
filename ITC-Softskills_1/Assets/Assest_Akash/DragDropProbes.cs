using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

//using testDatabase;

public class DragDropProbes : MonoBehaviour
{
	public static DragDropProbes ins;
	public GameObject Probs_Parent;
	public GameObject L1_Inst_probs;
	public GameObject[] BinsEffect;
	public GameObject[] Probes;
	public GameObject[] Positions;
	//public GameObject[] ProbesOnBody;
	public GameObject[] Colliders;

	public GameObject OriginalParent;

	public GameObject WellDone, TryAgain;

	public GameObject player;
	public GameObject Wall,Bin,StopWatch;

    public AudioSource As;

    public GameObject[] MausiAnim;
    Vector3 initialRot;
   
    static int countDrag=0;
	public GameObject tips_btn,tips_1,tips_2,tips_inst,tips_3,tips_4;
	public GameObject[] tips_anim;

	public GameObject Select_Inst_Continue,Continue_btn;
	public GameObject Score,Timer,L1_Inst_dragWithinTwoMin,Help_Btn;
	public GameObject GoodAverageBadMeter;
	public Text YouScored, ApriciationTxt;
 	void Start ()
	{
		ins=this;
		player.GetComponent<MovementController> ().enabled = false;
		//player.GetComponent<FirstPersonController>().enabled = false;

	}


	public void OnGazeDown (int n)
	{
		SoundManager.instance.PlayMessageSound ();
		Probes [n].transform.GetChild (0).gameObject.SetActive (false);
		Probes [n].transform.SetParent (Camera.main.transform);
        initialRot = Probes[n].transform.rotation.eulerAngles;
		Probes [n].GetComponent<Collider> ().enabled = false;

        for (int i = 0; i < Probes.Length; i++)
        {
            Probes [i].GetComponent<Collider> ().enabled = false;
        }

        Probes [n].gameObject.layer = 11;

//        if (Probes[n].tag == "HasChild")
//        {
//            for(int i=0;i<Probes[n].transform.childCount;i++)
//            {
//                Probes[n].transform.GetChild(i).gameObject.layer = 11;
//            }
//        }
       
		





	}

	public void OnGazeUp (int n)
	{
		Probes [n].transform.GetChild (0).gameObject.SetActive (true);
		RaycastHit hit;
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);

		if (Physics.Raycast (ray, out hit)) 
        {
			Transform objectHit = hit.transform;
//            Probes[n].SetActive(false);

            if (objectHit.tag == "Bin1" ) 
                {
				Probes[n].SetActive(false);
                    countDrag += 1;

				if (n == 14 || n == 2 || n == 9 || n == 4 || n == 5)
				{
					SoundManager.instance.SoundForSroring10 ();
					ScoreManager1.instance.score += 10;
				}

				if ((n == 8 || n == 1 || n == 13 || n == 12 || n == 6))
				{
					SoundManager.instance.SoundForSroring5 ();
					ScoreManager1.instance.score += 5;
				}



				if (n == 7 || n == 3 || n == 0 || n == 10 || n == 11)
				{
					SoundManager.instance.SoundForSroring0 ();
					ScoreManager1.instance.score += 0;
				}
                if (countDrag == 15)
                {
					Score.SetActive (false);
					GoodAverageBadMeter.SetActive(true);
					YouScored.text = ""+ScoreManager1.instance.score;
					DragDropProbes.ins.L1_Inst_probs.SetActive (false);
//					L1_Inst_dragWithinTwoMin.SetActive(false);
					Timer.SetActive (false);
                    OriginalParent.SetActive(false);
                    StopWatch.SetActive(false);
                    Bin.SetActive(false);

                    if (ScoreManager1.instance.score <= 150 && ScoreManager1.instance.score >= 120)
                    {
						ApriciationTxt.text = "Good";
                        MausiAnim[0].SetActive(true);

                    }
                    else if (ScoreManager1.instance.score <= 119 && ScoreManager1.instance.score >= 90)
                    {
						ApriciationTxt.text = "Average";
                        MausiAnim[1].SetActive(true);

                    }
                    else if (ScoreManager1.instance.score <= 90)
                    {
                        MausiAnim[2].SetActive(true);
						ApriciationTxt.text = "Poor";

                    }

					ContentProvider.instance.UpdateScore (ScoreManager1.instance.score);
                    ContentProvider.instance.score1 = ScoreManager1.instance.score;
                }

                

                    
                }

            else if (objectHit.tag == "Bin2")
                {
				Probes[n].SetActive(false);
                countDrag += 1;
				if (n == 14 || n == 2 || n == 9 || n == 4 || n == 5)
				{
					SoundManager.instance.SoundForSroring5 ();
					ScoreManager1.instance.score += 5;

				}

				if ((n == 8 || n == 1 || n == 13 || n == 12 || n == 6))
				{
					ScoreManager1.instance.score += 10;
					SoundManager.instance.SoundForSroring10 ();
				}



				if (n == 7 || n == 3 || n == 0 || n == 10 || n == 11)
				{
					ScoreManager1.instance.score += 5;

					SoundManager.instance.SoundForSroring5 ();


				}
                if (countDrag == 15)
                {
					GoodAverageBadMeter.SetActive(true);
					YouScored.text = "" + ScoreManager1.instance.score;
					Score.SetActive (false);
//					L1_Inst_dragWithinTwoMin.SetActive(false);
					DragDropProbes.ins.L1_Inst_probs.SetActive (false);
					Timer.SetActive (false);
                    OriginalParent.SetActive(false);
                    StopWatch.SetActive(false);
                    Bin.SetActive(false);

                    if (ScoreManager1.instance.score <= 150 && ScoreManager1.instance.score >= 120)
                    {
                        MausiAnim[0].SetActive(true);
						ApriciationTxt.text = "Good";
                    }
                    else if (ScoreManager1.instance.score <= 119 && ScoreManager1.instance.score >= 90)
                    {
						ApriciationTxt.text = "Average";
                        MausiAnim[1].SetActive(true);

                    }
                    else if (ScoreManager1.instance.score <= 90)
                    {
                        MausiAnim[2].SetActive(true);
						ApriciationTxt.text = "Poor";

                    }
					ContentProvider.instance.UpdateScore (ScoreManager1.instance.score);
                    ContentProvider.instance.score1 = ScoreManager1.instance.score;
                }

                

                    
                } 
            else if (objectHit.tag == "Bin3")
                {
				Probes[n].SetActive(false);
                countDrag += 1;
				if (n == 14 || n == 2 || n == 9 || n == 4 || n == 5)
				{
					ScoreManager1.instance.score += 0;
					SoundManager.instance.SoundForSroring0 ();

				}

				if ((n == 8 || n == 1 || n == 13 || n == 12 || n == 6))
				{
					ScoreManager1.instance.score += 0;
					SoundManager.instance.SoundForSroring0 ();
				}



				if (n == 7 || n == 3 || n == 0 || n == 10 || n == 11)
				{
					ScoreManager1.instance.score += 10;
					SoundManager.instance.SoundForSroring10 ();
				}
                if (countDrag == 15)
                {
					Score.SetActive (false);
					GoodAverageBadMeter.SetActive(true);
					YouScored.text = ""+ScoreManager1.instance.score;
					DragDropProbes.ins.L1_Inst_probs.SetActive (false);
//					L1_Inst_dragWithinTwoMin.SetActive(false);
					Timer.SetActive (false);
                    OriginalParent.SetActive(false);
                    StopWatch.SetActive(false);
                    Bin.SetActive(false);

                    if (ScoreManager1.instance.score <= 150 && ScoreManager1.instance.score >= 120)
                    {
                        MausiAnim[0].SetActive(true);
						ApriciationTxt.text = "Good";
                    }
                    else if (ScoreManager1.instance.score <= 119 && ScoreManager1.instance.score >= 90)
                    {

                        MausiAnim[1].SetActive(true);
						ApriciationTxt.text = "Average";
                    }
                    else if (ScoreManager1.instance.score <= 90)
                    {
                        MausiAnim[2].SetActive(true);
						ApriciationTxt.text = "Poor";

                    }

					ContentProvider.instance.UpdateScore (ScoreManager1.instance.score);
                    ContentProvider.instance.score1 = ScoreManager1.instance.score;
                }
                        

                

                    
                }
               
			
			 else {
				/////////////////////if ray hits on wrong collider
				//TryAgain.SetActive (true);
				Wall.SetActive (true);
				StartCoroutine (WaitToFinish_WD_TA (n));
				Probes [n].transform.parent = OriginalParent.transform;
				Probes [n].transform.DOLocalMove (Positions [n].transform.localPosition, 1);
                print(initialRot);
                Probes [n].transform.rotation = Quaternion.Euler (initialRot);
                //Probes [n].transform.localRotation = Quaternion.identity;
			}

	

            Debug.Log(countDrag);


		}

        else {
			
			//////////////////////if ray doesn't hit any collider///////////////////////
			//TryAgain.SetActive (true);
           // Debug.Log();
			Wall.SetActive (true);
			StartCoroutine (WaitToFinish_WD_TA (n));
			Probes [n].transform.parent = OriginalParent.transform;
			Probes [n].transform.DOLocalMove (Positions [n].transform.localPosition, .1f);
            //Probes[n].transform.localEulerAngles = initialRot;
            print(initialRot);
            Probes [n].transform.rotation = Quaternion.Euler (initialRot);
            //Probes [n].transform.localRotation = Quaternion.identity;

		}

        for (int i = 0; i < Probes.Length; i++)
        {
            Probes [i].GetComponent<Collider> ().enabled = true;
        }

	}
		

	IEnumerator WaitToFinish_WD_TA (int n)
	{

		yield return new WaitForSeconds (1f);
		//TryAgain.SetActive (false);
		Wall.SetActive (false);
		Probes [n].GetComponent<Collider> ().enabled = true;
		Probes [n].gameObject.layer = 9;
//        if (Probes[n].tag == "HasChild")
//        {
//            for(int i=0;i<Probes[n].transform.childCount;i++)
//            {
//                Probes[n].transform.GetChild(i).gameObject.layer = 9;
//            }
//        }

		
	}

	IEnumerator Waitfor_WD (int n)
	{

		yield return new WaitForSeconds (2f);
		//WellDone.SetActive (false);
		Wall.SetActive (false);

	}

	


	public float Audio_length (string audioName)
	{
		return  Resources.Load < AudioClip > ("VoiceOvers/" + LanguageHandler.instance.Languages [LanguageHandler.instance.CurrentLanguageIndex].LanguageID + "/" + audioName).length;
	}


	void EnableLastIns ()
	{
		player.GetComponent<MovementController> ().enabled = false;

	}

	IEnumerator WaitforlastWD ()
	{

		yield return new WaitForSeconds (Audio_length (WellDone.name));
		
	}


    public void OnGazeEnter()
    {

        As.Play();

    }

    public void OnGazeExit()
    {

        As.Stop();

    }

    public void ClickOnContinue()
    {
        //MapManager.instance_cps = MapManager.checkPts.cp2;
        PlayerPrefs.SetInt("checkPts", 2);
		PlayerPrefs.SetInt ("score", ContentProvider.instance.totalGainedScore);
        SceneLoader.instance.LoadScene(1);
    }


	public void ClickOnTipBtn()
	{
		foreach (GameObject mausi in MausiAnim)
			mausi.SetActive (false);
		tips_btn.SetActive (false);
		tips_inst.SetActive (false);
		tips_1.SetActive (true);
		tips_anim [0].SetActive (true);
		StartCoroutine (Tips());
	}


	IEnumerator Tips()
	{
		yield return new WaitForSeconds (.15f);
		yield return new WaitForSeconds (GlobalAudioSrc.Instance.audioSrc.clip.length+1f);
		tips_1.SetActive (false);
		tips_anim [0].SetActive (false);
		tips_2.SetActive (true);
		tips_anim [1].SetActive (true);
		yield return new WaitForSeconds (.15f);
		yield return new WaitForSeconds (GlobalAudioSrc.Instance.audioSrc.clip.length+1f);
		tips_2.SetActive (false);
		//New Addintion - 21-12-2018.......................................................
		tips_anim[1].SetActive(false);
		tips_3.SetActive (true);
		tips_anim [2].SetActive (true);
		yield return new WaitForSeconds (.15f);
		yield return new WaitForSeconds (GlobalAudioSrc.Instance.audioSrc.clip.length+1f);
		tips_3.SetActive (false);
		tips_anim[2].SetActive(false);
		tips_4.SetActive (true);
		tips_anim [3].SetActive (true);
		yield return new WaitForSeconds (.15f);
		yield return new WaitForSeconds (GlobalAudioSrc.Instance.audioSrc.clip.length+1f);
		tips_4.SetActive (false);
		//.................................................................................
		Select_Inst_Continue.SetActive (true);
		Continue_btn.SetActive (true);

	}

	public void TimeUP()
	{
//		L1_Inst_dragWithinTwoMin.SetActive (false);
		DragDropProbes.ins.L1_Inst_probs.SetActive (false);
		GoodAverageBadMeter.SetActive(true);
		Score.SetActive (false);
		Timer.SetActive (false);
		OriginalParent.SetActive(false);
		StopWatch.SetActive(false);
		Bin.SetActive(false);
		YouScored.text = "" + ScoreManager1.instance.score;
		if (ScoreManager1.instance.score <= 150 && ScoreManager1.instance.score >= 120)
		{
			ApriciationTxt.text = "Good";
			MausiAnim[0].SetActive(true);
		}
		else if (ScoreManager1.instance.score <= 119 && ScoreManager1.instance.score >= 90)
		{
			ApriciationTxt.text = "Average";
			MausiAnim[1].SetActive(true);
		}
		else if (ScoreManager1.instance.score <= 90)
		{
			ApriciationTxt.text = "Poor";
			MausiAnim[2].SetActive(true);
		}
	}

	public void ClickOnHelp_Btn(){
		LanguageHandler.instance.StopVoiceOver ();
		LanguageHandler.instance.PlayVoiceOver ("L1_Inst_probs");
	}
}