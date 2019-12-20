using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour 
{
    public Text scoreText;

	// Use this for initialization
	void Start () 
	{
	        PlayerPrefs.SetInt("Score", 0);
	}
  
	// Update is called once per frame
	void Update () 
	{
		if ((LanguageHandler.instance.IsRightToLeft)) 
		{
			LanguageHandler.instance.OnLanguageChangeListener (scoreText, "Score");
			scoreText.text = PlayerPrefs.GetInt ("Score") +" : " +scoreText.text ;
		}
		else
		{
            if (PlayerPrefs.GetString("currentLanguage") == "hi-IN")
            {
                LanguageHandler.instance.OnLanguageChangeListener(scoreText, "Score");
                scoreText.text = scoreText.text + " % " + PlayerPrefs.GetInt("Score");
            }
            else
            {
                LanguageHandler.instance.OnLanguageChangeListener(scoreText, "Score");
                scoreText.text = scoreText.text + " : " + PlayerPrefs.GetInt("Score");
            }
		}

   	}
}
