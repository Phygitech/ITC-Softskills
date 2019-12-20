using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TurnON_OffPhysicsRaycaster : StateMachineBehaviour {


	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
//		base.OnStateEnter (animator, stateInfo, layerIndex);
		Camera.main.transform.GetComponent<GvrPointerPhysicsRaycaster>().enabled = false;
		VirtualTrialManager.instance.ClickMeCanvas.GetComponent<GvrPointerGraphicRaycaster> ().enabled = false;
	}

	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller)
	{
//		base.OnStateExit (animator, stateInfo, layerIndex, controller);
		Camera.main.transform.GetComponent<GvrPointerPhysicsRaycaster>().enabled = true;
		VirtualTrialManager.instance.ClickMeCanvas.GetComponent<GvrPointerGraphicRaycaster> ().enabled = true;
	}
}
