using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GazeSourceDetector : MonoBehaviour
{
	public static GazeSourceDetector instance;
	internal GameObject LeftGearHandModel, RightGearHandModel, LeftGOHandModel, RightGOHandModel;
    OVRGazePointer _GazePointerRing;
    OVRInputModule _EventSystem;

    Transform LeftHand, RightHand;

    // Use this for initialization
    void Start()
    {
		instance = this;
        transform.position = Camera.main.transform.position;
        _GazePointerRing = transform.Find("OvrGazePointer").GetComponent<OVRGazePointer>();
        _EventSystem = transform.Find("OvrEventSystem").GetComponent<OVRInputModule>();
        Transform _TrackingSpace = transform.Find("TrackingSpace");

        LeftHand = _TrackingSpace.Find("LeftHandAnchor");
        RightHand = _TrackingSpace.Find("RightHandAnchor");

        LeftGearHandModel = LeftHand.GetChild(0).GetChild(0).gameObject;
        RightGearHandModel = RightHand.GetChild(0).GetChild(0).gameObject;

        LeftGOHandModel = LeftHand.GetChild(0).GetChild(1).gameObject;
        RightGOHandModel = RightHand.GetChild(0).GetChild(1).gameObject;

    }
	
    // Update is called once per frame
    void Update()
    {
        if (LeftGearHandModel.activeSelf || LeftGOHandModel.activeSelf)
        {
            _GazePointerRing.rayTransform = LeftHand;
           _EventSystem.rayTransform = LeftHand;
        }
        else if (RightGearHandModel.activeSelf || RightGOHandModel.activeSelf)
        {
            _GazePointerRing.rayTransform = RightHand;
            _EventSystem.rayTransform = RightHand;
        }
        else
        {
            _GazePointerRing.rayTransform = Camera.main.transform;
            _EventSystem.rayTransform = Camera.main.transform;
        }
    }
}
