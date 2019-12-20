using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapManager : MonoBehaviour {

	public static MapManager instance;

    public enum checkPts{cp0,cp1,cp2,cp3,cp4,cp5}


    public static checkPts instance_cps ;

    public GameObject[] RedBtn;
    public GameObject[] GreenBtn;


	void Awake ()
	{
		instance = this;
	}

	void Start ()
	{
	//	PlayerPrefs.SetInt("checkPts", 5);
	//	PlayerPrefs.DeleteAll ();
		ResetMap ();
	}

	// Use this for initialization
	public void ResetMap () 
    {
      //  PlayerPrefs.DeleteAll();
        if(PlayerPrefs.GetInt("checkPts")==0)
        {

            foreach (GameObject red in RedBtn)
                red.SetActive(false);

            foreach (GameObject green in GreenBtn)
                green.SetActive(false);
        }


       

        if(!PlayerPrefs.HasKey("checkPts"))
        {
            PlayerPrefs.SetInt("checkPts",0);
			PlayerPrefs.SetInt ("score", 0);
        }
        Debug.Log("Test " + PlayerPrefs.GetInt("checkPts"));
       
        if(PlayerPrefs.GetInt("checkPts")==1)
        {
            
            RedBtn[0].SetActive(true);
        }
        else if(PlayerPrefs.GetInt("checkPts")==2)
        {
            
            GreenBtn[0].SetActive(true);
            RedBtn[0].SetActive(false);
            RedBtn[1].SetActive(true);
        }
        else if(PlayerPrefs.GetInt("checkPts")==3)
        {
            GreenBtn[0].SetActive(true);
            GreenBtn[1].SetActive(true);
            RedBtn[1].SetActive(false);

			if (!ContentProvider.instance.isFirstMonth) 
				RedBtn[2].SetActive(true);
        }
        else if(PlayerPrefs.GetInt("checkPts")==4)
        {

            GreenBtn[0].SetActive(true);
            GreenBtn[1].SetActive(true);
            GreenBtn[2].SetActive(true);
            RedBtn[2].SetActive(false);
            RedBtn[3].SetActive(true);
        }
		else if(PlayerPrefs.GetInt("checkPts")==5)
		{

			GreenBtn[0].SetActive(true);
			GreenBtn[1].SetActive(true);
			GreenBtn[2].SetActive(true);
			GreenBtn[3].SetActive(true);
			RedBtn[2].SetActive(false);
			RedBtn[3].SetActive(false);
		}


//        if (instance_cps == checkPts.cp1)
//        {
//            RedBtn[0].SetActive(true);
//
//            
//        }
//
//        if (instance_cps == checkPts.cp2)
//        {
//            GreenBtn[0].SetActive(true);
//            RedBtn[0].SetActive(false);
//            RedBtn[1].SetActive(true);
//
//        }
//        if (instance_cps == checkPts.cp3)
//        {
//            GreenBtn[0].SetActive(true);
//            GreenBtn[1].SetActive(true);
//            RedBtn[1].SetActive(false);
//            RedBtn[2].SetActive(true);
//
//        }
//
//        if (instance_cps == checkPts.cp4)
//        {
//            GreenBtn[0].SetActive(true);
//            GreenBtn[1].SetActive(true);
//            GreenBtn[2].SetActive(true);
//            RedBtn[2].SetActive(false);
//            RedBtn[3].SetActive(true);
//
//        }
//        else if(PlayerPrefs.GetInt("checkPts")==0)
//        {
//
//            foreach (GameObject red in RedBtn)
//                red.SetActive(false);
//
//            foreach (GameObject green in GreenBtn)
//                green.SetActive(false);
//        }
//        if (instance_cps == checkPts.cp5)
//        {
//            
//
//            foreach (GameObject red in RedBtn)
//                red.SetActive(false);
//
//            foreach (GameObject green in GreenBtn)
//                green.SetActive(false);
//            
//        }

	}
	


    public void OnEnter(GameObject Img)
    {

		Img.transform.DOScale(new Vector3(10.5f, 10.5f, 10.5f),0.5f);

    }

    public void OnExit(GameObject Img)
    {

        Img.transform.DOScale(new Vector3(10f, 10f, 10f),0.5f);

    }

}
