using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnswerLevel2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckAnsWer(GameObject option)
    {

//		if(option.tag == "ten")
//		{
//			GameManagerLevel2.instance.IsTriggered = true;
//		}
//        SoundManager.instance.PlayClickSound();
        if (option.tag == "ten")
        {
			SoundManager.instance.SoundForSroring10 ();
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 10);
			GameManagerLevel2.instance.IsTriggered = true;
        }
        else if(option.tag == "five")
        {
			SoundManager.instance.SoundForSroring5 ();
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 5);
        }
        else if(option.tag == "zero")
        {
			SoundManager.instance.SoundForSroring0 ();
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 0);
        }

      //  GameManagerLevel2.instance.Activity[GameManagerLevel2.Activity_Counter].transform.GetChild(0).GetComponentInChildren<Collider>().enabled = false;

    }
}
