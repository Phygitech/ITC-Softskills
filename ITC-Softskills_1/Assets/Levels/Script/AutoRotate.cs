using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

    bool Istrigger;
	// Use this for initialization
    public bool x,y,z;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Istrigger)
        {
            if (x)
            {
                transform.Rotate(new Vector3 (Time.timeScale*.5f,0,0));
            }
            else if (y)
            {
                transform.Rotate(new Vector3 (0,Time.timeScale*.5f,0));
            }
            else if (z)
            {
                transform.Rotate(new Vector3 (0,0,Time.timeScale*.5f));
            }
        }


	}

    void OnEnable()
    {
        Istrigger = true;
    }
}
