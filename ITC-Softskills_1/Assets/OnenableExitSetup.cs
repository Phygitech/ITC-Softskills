using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnenableExitSetup : MonoBehaviour {

	public GameObject Ccontroller;
	public GameObject ContinueBtn;

	void OnEnable(){
		ContinueBtn.SetActive (true);
		Ccontroller.SetActive (false);
	}

}
