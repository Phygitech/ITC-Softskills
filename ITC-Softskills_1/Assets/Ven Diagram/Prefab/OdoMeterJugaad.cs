using UnityEngine;
using System.Collections;

public class OdoMeterJugaad : MonoBehaviour {

	public GameObject firstWheel;
	public GameObject secondWheel;
	public bool isFirst=true;
	public bool isSecond=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (isFirst) {
			if (Input.GetAxis ("Vertical") > 0) {
				firstWheel.transform.Rotate (new Vector3 (36,0,0));
			}
			if (Input.GetAxis ("Vertical") < 0) {
				firstWheel.transform.Rotate (new Vector3 (-36,0,0));
			}

		}
	}
}
