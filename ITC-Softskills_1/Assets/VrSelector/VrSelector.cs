using UnityEngine;
using UnityEngine.VR;
using UnityEngine.EventSystems;

public class VrSelector : MonoBehaviour
{
	public static VrSelector instance;
	public GameObject _EditorEmulator, _CardboardPointer, _DaydreamController, _DaydreamReticle, _OvrController;
	[HideInInspector]
	public Transform _MainCamera;
	internal bool IsDaydream, IsOculus , IsCardboard;

	Canvas[] _canvas;


	void Awake()
	{
		instance = this;
		_EditorEmulator.SetActive(false);
		_CardboardPointer.SetActive(false);
		_DaydreamController.SetActive(false);
		_OvrController.SetActive(false);
		_canvas = FindObjectsOfType<Canvas>();
		_MainCamera = Camera.main.transform;
	}

	void Start()
	{
		#if UNITY_EDITOR
		_EditorEmulator.SetActive(true);
		#endif

		switch (CurrentPLatform())
		{
		case "daydream":
			IsCardboard = true;
			_CardboardPointer.transform.SetParent (_MainCamera);
			_CardboardPointer.transform.localPosition = Vector3.zero;
			_CardboardPointer.transform.localRotation = Quaternion.identity;
			_CardboardPointer.SetActive (true);
		
			break;

			case "daydream1":
				IsDaydream = true;
				_DaydreamController.transform.SetParent(_MainCamera.parent);
				_DaydreamController.transform.localPosition = Vector3.zero;
				_DaydreamController.transform.localRotation = Quaternion.identity;
				_DaydreamController.SetActive(true);
	
			break;

			case "Oculus":
				IsOculus = true;
				_OvrController.transform.SetParent(_MainCamera.parent);
				_OvrController.transform.localPosition = Vector3.zero;
				_OvrController.transform.localRotation = Quaternion.identity;
				_OvrController.SetActive(true);
				break;
		}

		#if UNITY_EDITOR
		_EditorEmulator.GetComponent<GvrEditorEmulator>().Recenter();
		#else
		UnityEngine.XR.InputTracking.Recenter();
		#endif
	}

	string CurrentPLatform() {
		if (!Debug.isDebugBuild) {
			if (PlayerPrefs.HasKey ("platform"))
				return PlayerPrefs.GetString ("platform");
		} 
		return UnityEngine.XR.XRSettings.supportedDevices [0];
	}

	public void EnablePhysicsRayCasters()
	{
		
		if (UnityEngine.XR.XRSettings.supportedDevices[0] == "daydream" || UnityEngine.XR.XRSettings.supportedDevices[0] == "cardboard")
		{
			
			if(_MainCamera.GetComponent<GvrPointerPhysicsRaycaster>()!=null)
				_MainCamera.GetComponent<GvrPointerPhysicsRaycaster>().enabled = true;
		}
		else if (UnityEngine.XR.XRSettings.supportedDevices[0] == "Oculus")
		{
			if(_OvrController.GetComponent<OVRPhysicsRaycaster>()!=null)
				_OvrController.GetComponent<OVRPhysicsRaycaster>().enabled = true;
		}
	}

	public void DisablePhysicsRayCasters()
	{

		if (UnityEngine.XR.XRSettings.supportedDevices[0] == "daydream" || UnityEngine.XR.XRSettings.supportedDevices[0] == "cardboard")
		{

			if(_MainCamera.GetComponent<GvrPointerPhysicsRaycaster>()!=null)
				_MainCamera.GetComponent<GvrPointerPhysicsRaycaster>().enabled = false;
		}
		else if (UnityEngine.XR.XRSettings.supportedDevices[0] == "Oculus")
		{
			if(_OvrController.GetComponent<OVRPhysicsRaycaster>()!=null)
				_OvrController.GetComponent<OVRPhysicsRaycaster>().enabled = false;
		}
	}

	public void EnableUIRayCasters()
	{
		
		if (UnityEngine.XR.XRSettings.supportedDevices[0] == "daydream" || UnityEngine.XR.XRSettings.supportedDevices[0] == "cardboard")
		{

			for (int i = 0; i < _canvas.Length; i++)
			{
				if (_canvas[i].GetComponent<GvrPointerGraphicRaycaster>() != null)
					_canvas[i].GetComponent<GvrPointerGraphicRaycaster>().enabled = true;

				if (_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>() != null)
					_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>().enabled = true;
			}
		}
		else if (UnityEngine.XR.XRSettings.supportedDevices[0] == "Oculus")
		{
			for (int i = 0; i < _canvas.Length; i++)
			{
				if (_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>() != null)
					_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>().enabled = true;

				if (_canvas[i].GetComponent<OVRRaycaster>() != null)
					_canvas[i].GetComponent<OVRRaycaster>().enabled = true;
			}
		}
	}

	public void DisableUIRayCasters()
	{
		
		if (UnityEngine.XR.XRSettings.supportedDevices[0] == "daydream" || UnityEngine.XR.XRSettings.supportedDevices[0] == "cardboard")
		{
	
			for (int i = 0; i < _canvas.Length; i++)
			{
				if (_canvas[i].GetComponent<GvrPointerGraphicRaycaster>() != null)
					_canvas[i].GetComponent<GvrPointerGraphicRaycaster>().enabled = false;

				if (_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>() != null)
					_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>().enabled = false;
			}
		}
		else if (UnityEngine.XR.XRSettings.supportedDevices[0] == "Oculus")
		{
			for (int i = 0; i < _canvas.Length; i++)
			{
				if (_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>() != null)
					_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>().enabled = false;

				if (_canvas[i].GetComponent<OVRRaycaster>() != null)
					_canvas[i].GetComponent<OVRRaycaster>().enabled = false;
			}
		}
	}
}
