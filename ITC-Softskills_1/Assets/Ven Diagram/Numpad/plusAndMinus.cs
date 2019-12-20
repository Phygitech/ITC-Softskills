using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class plusAndMinus : MonoBehaviour//, IGvrGazeResponder
{
	public char _char;

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
		Togglepositive ();
	}

	void Togglepositive ()
	{
		Numpad_Manager.instance.toggle = !Numpad_Manager.instance.toggle;
		if (!Numpad_Manager.instance.toggle) {
			if (Numpad_Manager.instance.inputChar.Count > 0 && Numpad_Manager.instance.inputChar [0] == _char)
				Numpad_Manager.instance.inputChar.RemoveAt (0);
		} else {
			if (!Numpad_Manager.instance.isOverlimit)
				Numpad_Manager.instance.inputChar.Insert (0, _char);
		}
			
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

		//Togglepositive ();

	}
}
