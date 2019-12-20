using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesablePanel : MonoBehaviour {

	public GameObject ObjectToDesable;

	void OnDisable(){
		ObjectToDesable.SetActive (false);
	}
}
