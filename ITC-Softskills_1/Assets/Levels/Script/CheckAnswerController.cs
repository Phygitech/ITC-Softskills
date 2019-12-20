using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnswerController  : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckAnsWer(GameObject option)
    {

        if (option.tag == "ten")
        {
			SoundManager.instance.SoundForSroring10 ();
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 10);
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

    }
}
