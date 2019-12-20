using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MaterialUI;

//this script add on gameobject which have text component;
public class InputText : MonoBehaviour
{
	public static InputText instance;
	// if u want add number automatic than press in true in start otherwise its false
	public bool ispress;
	private int maxLength = 15;
	int i = 0;
	public GameObject placeHolderText;
	bool animateText = false;
	public MaterialInputField materialInputField;

	void OnEnable()
	{
		if (Cancel.instance != null) {
			Cancel.instance.ClearText ();
		}
	}

	void Start ()
	{
		instance = this;
	}

	public void SelectInputtext ()
	{
        if (i == 0) {
        	i = 1;
         
			if (Cancel.instance != null && !ispress) {
				Cancel.instance.ClearText ();
			}
			ispress = true;
			placeHolderText.SetActive (false);
		//	if (!animateText)
		//		animateText = true;
		}
	
	}

	public void HideKeyPad ()
	{
		ispress = false;
		i = 0;
	}

	void Update ()
	{
	/*	if (animateText)
			materialInputField.AnimateHintTextSelect ();*/

		if (ispress && gameObject.GetComponent<Text> ()) 
		{
			Numpad_Manager.instance.gameObject.SetActive (true);
			if (Numpad_Manager.instance._value.Length < maxLength) {
				Numpad_Manager.instance.isOverlimit = false;
			//	Debug.Log ("_value " + Numpad_Manager.instance._value);
			//	Debug.Log ("Text " + gameObject.GetComponent<Text> ().text);
				gameObject.GetComponent<Text> ().text = Numpad_Manager.instance._value;
			} else {
				Numpad_Manager.instance.isOverlimit = true;
				Numpad_Manager.instance._value = Numpad_Manager.instance._value.Substring (0, maxLength);
				gameObject.GetComponent<Text> ().text = Numpad_Manager.instance._value;
			}
		} 
		else 
		{
			if (Numpad_Manager.instance!= null)
				Numpad_Manager.instance.gameObject.SetActive (false);
		}
	}
}
