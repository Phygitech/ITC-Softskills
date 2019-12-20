using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTips : MonoBehaviour {

    public GameObject Tip1,Tip2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        Tip1.SetActive(false);
        Tip2.SetActive(false);
    }
}
