using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour {

	public GameObject Object;	
	// Use this for initialization
	void Start () {
		Object.SetActive(true);
	}
	

}
