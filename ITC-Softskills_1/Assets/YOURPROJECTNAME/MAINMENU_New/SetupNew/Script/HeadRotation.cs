using UnityEngine;
using DG.Tweening;
using System.Collections;

public class HeadRotation : MonoBehaviour
{
    public bool RotationAbility = true;
	public static HeadRotation instance;
  //  GvrHead GHead;
  //  public GvrViewer GViewer;
    bool CheckBool;

    void Start()
    {
		instance = this;
        CheckBool = true;
        //GHead = Camera.main.transform.GetComponent<GvrHead>();
        //GHead.trackRotation = true;
    }


    void Update()
    {
        

        if (CheckBool != RotationAbility)
        {
            CheckBool = RotationAbility;
            if (RotationAbility)
            {
                //GViewer.Recenter();
//				GvrEditorEmulator.Instance.Recenter();
//				GvrCardboardHelpers.Recenter();
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
               // GHead.trackRotation = true;

            }
            else
            {
                //GHead.trackRotation = false;
                Camera.main.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);
            }
//			if (RotationAbility) {
//				#if UNITY_EDITOR
//				FindObjectOfType<GvrEditorEmulator>().enabled = true;
//				#endif  // UNITY_EDITOR
//
//				#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
//				VRDevice.DisableAutoVRCameraTracking(Camera.main, false);
//				#endif  // (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
//
//			} else {
//				#if UNITY_EDITOR
//				FindObjectOfType<GvrEditorEmulator>().enabled = false;
//				#endif  // UNITY_EDITOR
//
//				#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
//				VRDevice.DisableAutoVRCameraTracking(Camera.main, true);
//				#endif  // (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
//
//			}
        }


    }
		

}
