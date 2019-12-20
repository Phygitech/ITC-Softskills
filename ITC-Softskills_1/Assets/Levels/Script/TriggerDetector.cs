using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "vr_player")
        {
            Debug.Log("Enter");
            GameManagerLevel2.instance.HighLightVfx.SetActive(false);
            GameManagerLevel2.instance.VrPlayer.GetComponent<MovementController>().enabled = false;
            GameManagerLevel2.instance.MoveToHighlightArea.SetActive(false);
        }
    }
}
