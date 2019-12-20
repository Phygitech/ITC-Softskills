using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonsAction : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Menu)) {
//			SceneManager.LoadScene ("MainMenu");
			LoadingScene.LoadingSceneIndex = 0;
		}

		if (Input.GetKeyDown (KeyCode.JoystickButton1) || Input.GetButtonDown("Cancel")) {
//			SceneManager.LoadScene ("MainMenu");
			LoadingScene.LoadingSceneIndex = 0;
		}
	}
}
