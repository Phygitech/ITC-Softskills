using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using SmartLocalization;
public class TimerScript : MonoBehaviour 
{
	public Text ActivityTimerTxt;
	public GameObject TimeUP;

	public int minutes,localTimer;

	public static bool TimeEnd = true;

	public static TimerScript ins;


	void Start ()
	{
		ins = this;
		TimeEnd = true;
		StartCoroutine (StartTimer());
	}

	internal void SetActivityTimerText(int Value)
	{
        string niceTime;// = string.Format("{0:00}:{1:00}", minutes, Value);

        if (PlayerPrefs.GetString("currentLanguage") == "hi-IN")
        {
            niceTime = string.Format("{0:00}%{1:00}", minutes, Value);
            string time =
                ActivityTimerTxt.text = LanguageManager.Instance.GetTextValue("C_Timer") + " % " + niceTime;//+" Sec";
        }
        else
        {
            niceTime = string.Format("{0:00}:{1:00}", minutes, Value);
            string time =
                ActivityTimerTxt.text = LanguageManager.Instance.GetTextValue("C_Timer") + " : " + niceTime;//+" Sec";
        }
		
	}



	public IEnumerator StartTimer() 
	{
		while (localTimer >= 0)
		{
//			SoundManager.instance.PlayTikTikSound ();
			SetActivityTimerText (localTimer);
			yield return new WaitForSeconds (1);
			localTimer--;
			if(localTimer==-1)
			{
				if (minutes > 0)
				{
					localTimer = 59;
					minutes--;
				}
				else
				{
					TimeEnd = false;
					StopCoroutine ("StartTimer");
					DragDropProbes.ins.TimeUP ();
					GetComponent<AudioSource> ().enabled = false;
				}
			}
		}
	}

//	void PlayTikSound(){
//		
//	}

}