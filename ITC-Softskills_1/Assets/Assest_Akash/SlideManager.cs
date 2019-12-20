using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlideManager : MonoBehaviour {

	
    public GameObject[] imgs,Img_text;

    public GameObject BackBtn, NextBtn,OkBtn;

    public static int n = 0;

    void Start()
    {
        n = 0;
        imgs[n].SetActive(true);
		Img_text [n].SetActive (true);
        BackBtn.SetActive(false);
        NextBtn.SetActive(true);
        OkBtn.SetActive(false);

    }


    public void SlideFwd()
    {
        if (n <= 7)
        {
            imgs[n].SetActive(false);
			Img_text [n].SetActive (false);
            imgs[n+=1].SetActive(true);
			Img_text [n].SetActive (true);

        }

        if (n == 8)
        {

            NextBtn.SetActive(false);
            BackBtn.SetActive(true);
            OkBtn.SetActive(true);

        }
        
    }

    public void SlideBckwd()
    {

        if (n >= 1)
        {
            imgs[n].SetActive(false);
			Img_text [n].SetActive (false);
            imgs[n-=1].SetActive(true);
			Img_text [n].SetActive (true);
        }
        if (n == 0)
        {

            BackBtn.SetActive(false);
            NextBtn.SetActive(true);

        }
        OkBtn.SetActive(false);

    }

    public void OnOKClick()
    {
       // MapManager.instance_cps = MapManager.checkPts.cp1;
		if (!ContentProvider.instance.isFirstMonth) 
		{
			/*if (PlayerPrefs.GetInt ("checkPts") == 0 || PlayerPrefs.GetInt ("checkPts") == 1 || PlayerPrefs.GetInt ("checkPts") == 2) 
			{
				if (PlayerPrefs.GetInt ("checkPts") != 3 && PlayerPrefs.GetInt ("checkPts") != 4)
				{
					PlayerPrefs.SetInt ("checkPts", 4);
					PlayerPrefs.SetInt ("score", 0);
				}

			}*/
		} 
		else 
		{
			if (PlayerPrefs.GetInt ("checkPts") == 0) 
			{
				PlayerPrefs.SetInt ("checkPts", 1);
				PlayerPrefs.SetInt ("score", 0);
			}
		}
    }


}
