using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StopWatch : MonoBehaviour {

    private const float
   
    secondsToDegrees = 360f / 60f;

    public Transform Needle;


    void Update () {
        
            TimeSpan timespan = DateTime.Now.TimeOfDay;
            
        Needle.localRotation =Quaternion.Euler(0f,0f,-(float)timespan.TotalSeconds * -secondsToDegrees);


        
       
    }
}

