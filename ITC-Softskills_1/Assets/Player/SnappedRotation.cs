using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class SnappedRotation : MonoBehaviour
{
    public static SnappedRotation instance;
    public float rotateAngle = 20;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (VrSelector.instance.IsDaydream)
        {
            #if UNITY_EDITOR
            NormalController();
            #endif

            Vector2 TouchAxis = GvrControllerInput.TouchPosCentered;

            if (!GvrController.IsTouching)
            {
                return;
            }

            if (TouchAxis.magnitude > 0.3f)
            {
                if (Mathf.Abs(TouchAxis.x) > Mathf.Abs(TouchAxis.y))
                {
                    if (GvrControllerInput.TouchDown)
                    {
                        if (TouchAxis.x > 0)
                            transform.Rotate(0, rotateAngle, 0);

                        if (TouchAxis.x < 0)
                            transform.Rotate(0, -rotateAngle, 0);
                    }
                }
            }
        }
        else if (VrSelector.instance.IsOculus)
        {
            #if UNITY_EDITOR
            NormalController();
            #endif

            Vector2 TouchAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

            if (!OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
            {
                return;
            }

            if (TouchAxis.magnitude > 0.3f)
            {
                if (Mathf.Abs(TouchAxis.x) > Mathf.Abs(TouchAxis.y))
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
                    {
                        if (TouchAxis.x > 0)
                            transform.Rotate(0, rotateAngle, 0);

                        if (TouchAxis.x < 0)
                            transform.Rotate(0, -rotateAngle, 0);
                    }
                }
            }
        }
        else if (VrSelector.instance.IsCardboard)
        {
            NormalController();
        }
    }

    void NormalController()
    {
        if (Input.GetButtonDown("Horizontal"))
            transform.Rotate(0, rotateAngle * Input.GetAxisRaw("Horizontal"), 0);
    }
}