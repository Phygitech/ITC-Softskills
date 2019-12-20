using UnityEngine;
using System.Collections;

public class SwapUI : MonoBehaviour
{
	public  static SwapUI instance;

	public Transform close, sync, sync2;

	Vector3 intial1, intial2, intial3;

	void Awake ()
	{
		instance = this;
	}
	// Use this for initialization
	void Start ()
	{
		intial1 = close.transform.position;
		intial2 = sync.transform.position;
		intial3 = sync2.transform.position;

		Swap ();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}


	public void Swap ()
	{
		if (LanguageHandler.instance.IsLeftToRight) {
			close.transform.position = intial1;
			sync.transform.position = intial2;
			sync2.transform.position = intial3;
		} else {
			close.transform.position = new Vector3 (-intial1.x, close.transform.position.y, close.transform.position.z);
			sync.transform.position = new Vector3 (-intial2.x, sync.transform.position.y, sync.transform.position.z);
			sync2.transform.position = new Vector3 (-intial3.x, sync2.transform.position.y, sync2.transform.position.z);
		}
	}
}
