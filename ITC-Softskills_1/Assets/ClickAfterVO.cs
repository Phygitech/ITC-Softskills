using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClickAfterVO : MonoBehaviour {

	public Button[] buttons;

	void OnEnable(){
		StartCoroutine (_enableClick ());
	}

	IEnumerator _enableClick(){
		yield return new WaitForSeconds (.1f);
		for (int i = 0; i < buttons.Length; i++)
			buttons [i].interactable = false;
		print (GlobalAudioSrc.Instance.audioSrc.isPlaying+"Audio is playing");
		if (GlobalAudioSrc.Instance.audioSrc.isPlaying) {
			float f = GlobalAudioSrc.Instance.audioSrc.clip.length;
			print (f+"length of audio");
			yield return new WaitForSeconds (f);
			for (int i = 0; i < buttons.Length; i++)
				buttons [i].interactable = true;
		} else {
			for (int i = 0; i < buttons.Length; i++)
				buttons [i].interactable = true;
		}
	}

	public void ActiveButtonAfterVO(){
		StartCoroutine (_enableClick ());
	}
}
