using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualTrialManager : MonoBehaviour {

	public static VirtualTrialManager instance;
	public GameObject ClickMeCanvas;
	// Use this for initialization
	void Start () {
		instance = this;	
	}

}
