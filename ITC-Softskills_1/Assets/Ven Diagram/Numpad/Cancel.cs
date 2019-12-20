using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Cancel : MonoBehaviour//, IGvrGazeResponder
{
	public static Cancel instance;


	void Start(){
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

		ClearText ();
	}

	public void ClearText ()
	{
		Numpad_Manager.instance.inputChar.Clear ();
		Numpad_Manager.instance.toggle = false;
		//InputText.instance.placeHolderText.SetActive (true);

//		for (int i = 0; i < numPad_colliders.instance.Num_colliders.Length; i++) 
//		{
//			numPad_colliders.instance.Num_colliders [i].GetComponent<BoxCollider> ().enabled = true;
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

		//ClearText ();

	}
}
