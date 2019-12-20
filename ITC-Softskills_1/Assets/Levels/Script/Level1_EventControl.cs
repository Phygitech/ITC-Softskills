using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_EventControl : MonoBehaviour {

	public GameObject ContinueBtn;

	public void After_WakeUP(){
		Debug.Log ("1");
		DragDropProbes.ins.Probs_Parent.SetActive (true);
		Camera.main.transform.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = false;
//		LanguageHandler.instance.PlayVoiceOver ("Probs");

		DragDropProbes.ins.L1_Inst_probs.SetActive (true);
		float f = GlobalAudioSrc.Instance.audioSrc.clip.length + 1f;
		Invoke ("_StartDragDrop", f);
	}

	void _StartDragDrop(){
		Debug.Log ("start drag drop");
		Camera.main.transform.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = true;
		DragDropProbes.ins.Score.SetActive (true);
		DragDropProbes.ins.Timer.SetActive (true);
		DragDropProbes.ins.Help_Btn.SetActive (true);
//		DragDropProbes.ins.L1_Inst_probs.SetActive (false);
//		DragDropProbes.ins.L1_Inst_dragWithinTwoMin.SetActive (true);
		for (int i = 0; i < DragDropProbes.ins.BinsEffect.Length; i++) {
			DragDropProbes.ins.BinsEffect [i].SetActive (true);
		}

	}

	public void AnimComplete ()
	{
		if (PlayerPrefs.GetString ("currentLanguage") == "hi-IN") 
		{
			StartCoroutine (DelayForHindi ());
		}
		else
			ContinueBtn.SetActive (true);
	}

	IEnumerator DelayForHindi ()
	{
		yield return new WaitForSeconds (4f);
		ContinueBtn.SetActive (true);
	}

	public void L1_IntroMausiVO(){
		LanguageHandler.instance.PlayVoiceOver ("@_NinjaMausi_intro");
	}
}
