using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Numpad_Manager : MonoBehaviour
{
	public static Numpad_Manager instance;

	[HideInInspector] public List< char> inputChar = new List<char> ();
	[HideInInspector] public bool toggle = false;
	 public string _value = "";
	public bool isOverlimit = false;

	void Start ()
	{
		instance = this;
		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
		_value = new string (inputChar.ToArray ());
	}

	public void Reset()
	{
		inputChar = new List<char> ();
		_value = "";
	}

	public void One ()
	{
		Debug.Log ("1");
	}

	void OnDisable ()
	{
		if (Cancel.instance != null)
			Cancel.instance.ClearText ();
	}
}
