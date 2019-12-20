using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjWithTime : MonoBehaviour {

	// Use this for initialization

    public int n;

    public GameObject Object,wall,DragInst;

	void Start () {
		
        StartCoroutine(delay());

	}
	
  

    IEnumerator delay()
    {

        yield return new WaitForSeconds(n);

//        Object.SetActive(true);
//		DragInst.SetActive (true);
       wall.SetActive(false);
    }
}
