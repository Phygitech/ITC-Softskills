using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateYOYO : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DORotate(new Vector3(0,60,0),1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
	}
}
