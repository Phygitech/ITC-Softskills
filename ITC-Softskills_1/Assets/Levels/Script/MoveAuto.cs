using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class MoveAuto : MonoBehaviour {

	public GameObject Auto;
	public Vector3 AutoFinalPosition;
	public GameObject Clickable_BTN_AfterAutoMove;

	private IEnumerator _Ie;
	// Use this for initialization
	void Start () {
		_Ie = StartAuto ();
		StartCoroutine (_Ie);
	}

	public IEnumerator StartAuto(){
		Auto.transform.DOLocalMove (AutoFinalPosition,2f);
		yield return new WaitForSeconds (2f);
		Clickable_BTN_AfterAutoMove.GetComponent<Image> ().raycastTarget = true;
	}
}
