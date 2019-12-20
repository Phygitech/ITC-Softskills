using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtCam : MonoBehaviour {


    public Transform Camera;

    void Update()
    {

        transform.LookAt(Camera);

    }
	
}
