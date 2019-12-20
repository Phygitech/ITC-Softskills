using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropScriptDemo : MonoBehaviour
{
    Ray ray;
    Vector3 OriginalPosition;
    Quaternion OriginalRotation;
    Vector3 OriginalScale;
    Transform OriginalParent;
    GameObject DragableObj;


    // Use this for initialization
    void Start()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
        OriginalScale = transform.localScale;
        DragableObj = new GameObject("DragDropCanvas", typeof(Canvas));
        OriginalParent = transform.parent;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (VrSelector.instance.IsDaydream)
        {
            DragableObj.transform.position = VrSelector.instance._DaydreamReticle.transform.position;
        }
        else if (VrSelector.instance.IsOculus)
        {
            DragableObj.transform.position = VrSelector.instance._OvrController.transform.GetChild(1).position;
        }
        else if (VrSelector.instance.IsCardboard)
        {
            DragableObj.transform.SetParent(Camera.main.transform);
        }
    }

    public void OnMouseDown()
    {
        transform.SetParent(DragableObj.transform);
    }

    public void OnMouseUp()
    {
        transform.SetParent(OriginalParent);
        transform.position = OriginalPosition;
        transform.rotation = OriginalRotation;
        transform.localScale = OriginalScale;
    }
}
