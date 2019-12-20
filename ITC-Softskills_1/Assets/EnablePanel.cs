using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LevelSelection
{
	L2,L3,
};
public class EnablePanel : MonoBehaviour {

	public LevelSelection Level;
    public GameObject ContinuePanel;

	
	void Start () {
		if (Level == LevelSelection.L2) {
			GameManagerLevel2.instance.Timer.SetActive (false);
			ContinuePanel.SetActive(true);
		} else if (Level == LevelSelection.L3) {
			GameManagerLevel3.instance.Timer.SetActive (false);
			LanguageHandler.instance.PlayVoiceOver ("L3_SelectOpt");
		}
		Camera.main.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = false;
		float f = GlobalAudioSrc.Instance.audioSrc.clip.length + 1f;
		Invoke ("_EnableRaycastter",f);
    }

	void _EnableRaycastter(){
		if (Level == LevelSelection.L2) {
			GameManagerLevel2.instance.Timer.SetActive (true);
			GetComponent<L2_Timer> ().enabled = true;
		} else if (Level == LevelSelection.L3) {
			GameManagerLevel3.instance.Timer.SetActive (true);
			GetComponent<L3_Timer> ().enabled = true;
		}
		Camera.main.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = true;

	}
   

}
