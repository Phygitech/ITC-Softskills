using SmartLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager1 : MonoBehaviour {

    public static ScoreManager1 instance;
    public Text ScoreText;

    public  int score=0;

    void Start()
    {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
       
            ScoreText.text = LanguageManager.Instance.GetTextValue("Score") + score;
       


	}


   
}
