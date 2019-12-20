using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Character : MonoBehaviour//, IGvrGazeResponder
{
	public static Character instance;
	public char _char;
	char specialcase = '0';

	void Start ()
	{
		instance = this;
	
	}

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
	//	SoundManager.sm.PlayNumClickSound ();
		inputCharacter ();
	}


	void inputCharacter ()
	{

	//	if (!Numpad_Manager.instance.isOverlimit)// && Numpad_Manager.instance.inputChar.Count < 2) 
		{
			if (!Numpad_Manager.instance.inputChar.Contains ('.'))
			{
				
				if (Numpad_Manager.instance.inputChar.Count == 0 && _char == '.') 
				{	
					Numpad_Manager.instance.inputChar.Add (specialcase);
					Numpad_Manager.instance.inputChar.Add (_char);
				}
				else
				{
					Numpad_Manager.instance.inputChar.Add (_char);
//					if (Numpad_Manager.instance.inputChar.Count == 2)
//					{
//						for (int i = 0; i < numPad_colliders.instance.Num_colliders.Length; i++) 
//						{
//							numPad_colliders.instance.Num_colliders [i].GetComponent<BoxCollider> ().enabled = false;
//						}
//
//					}
				}

			} else {
				if (_char != '.')
					Numpad_Manager.instance.inputChar.Add (_char);
			}
		} 
//		else 
//		{
//			for (int i = 0; i < numPad_colliders.instance.Num_colliders.Length; i++) 
//			{
//				numPad_colliders.instance.Num_colliders [i].GetComponent<BoxCollider> ().enabled = false;
//			}
//		}
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

		//inputCharacter ();

	}
}
