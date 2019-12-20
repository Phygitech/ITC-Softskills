using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Backspace : MonoBehaviour//, IGvrGazeResponder
{

	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter ()
	{
		Debug.Log ("OnGazeEnter");

	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit ()
	{
		Debug.Log ("OnGazeExit");

	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeDown ()
	{
		//SoundManager.sm.PlayNumClickSound ();

		backspace ();
	}

	void backspace ()
	{
		if (Numpad_Manager.instance.inputChar.Count > 0) {
			Numpad_Manager.instance.inputChar.RemoveAt (Numpad_Manager.instance.inputChar.Count - 1);

//			for (int i = 0; i < numPad_colliders.instance.Num_colliders.Length; i++) 
//			{
//				numPad_colliders.instance.Num_colliders [i].GetComponent<BoxCollider> ().enabled = true;
//			}
//			for (int i = 0; i < numPad_colliders.instance.backSpace_Clear.Length; i++) {
//				numPad_colliders.instance.backSpace_Clear[i].GetComponent<BoxCollider> ().enabled = false;
//			}
		}
	//	else
			//InputText.instance.placeHolderText.SetActive (true);
	}

	public void OnGazeDrag ()
	{
	}

	public void OnGazeUp ()
	{		
		//CanMove = true;
		//		if (ParentObject != null) {
		//			transform.parent = ParentObject.transform;
		//		} else {
		//			transform.parent = null;
		//
		//		}
		//if (IsInsideCollider) {
		//	DragCompletion (gameObject);
		//}

	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger ()
	{

	}

	#endregion

	void OnMouseDown ()
	{

		//backspace ();

	}
}
