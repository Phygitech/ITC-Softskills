using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_animEventController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void AfterMausiReaction()

	{
		Invoke ("_delayInTip", 2f);
	}

	void _delayInTip(){
		DragDropProbes.ins.tips_btn.SetActive (true);
		DragDropProbes.ins.tips_inst.SetActive (true);
	}
}
