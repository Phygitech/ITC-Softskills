using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelection : MonoBehaviour {

	public Toggle hindiToggle;
	public Toggle englishToggle;

    public GameObject mainMenu;

    void Awake ()
    {
        ContentProvider.instance.SetKey("language", "english");
    }

	public void SelectLanguage ()
	{
		if (hindiToggle.isOn) 
		{
			// hindi language selected
			#if UNITY_ANDROID && !UNITY_EDITOR
				ContentProvider.instance.SetKey("language", "hindi");
			#endif

		}
		else 
		{
			// english language selected
			#if UNITY_ANDROID && !UNITY_EDITOR
				ContentProvider.instance.SetKey("language", "english");
			#endif
		}

        mainMenu.SetActive(true);
		gameObject.SetActive (false);
	}
}
