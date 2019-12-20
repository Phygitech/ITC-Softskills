using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class Setup : Editor
{
	
	static Canvas[] _canvas;
	static object[] allobjects;
	static Camera[] allCamera;
	static Canvas _camcanvas;

//	[MenuItem("Setup/Oculus")]

	public static void Enable_Oculus()
	{
		

		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) 
		{
			EditorSceneManager.OpenScene (EditorBuildSettings.scenes [i].path, OpenSceneMode.Additive);

				allobjects = Resources.FindObjectsOfTypeAll (typeof(GameObject));
				allCamera = Editor.FindObjectsOfType <Camera> ();
		
				foreach (GameObject obj in allobjects) {
					GvrPointerGraphicRaycaster gvr = obj.GetComponent (typeof(GvrPointerGraphicRaycaster)) as GvrPointerGraphicRaycaster;
					OVRRaycaster ovr = obj.GetComponent (typeof(OVRRaycaster)) as OVRRaycaster;
					if (obj.GetComponent<Canvas> ()) {

						if (obj.activeSelf || !obj.activeSelf) 
					    {

						if (gvr)
						   obj.GetComponent<GvrPointerGraphicRaycaster> ().enabled = false;

							if (!ovr) 
						    {
							    if (obj.name != "MainMenuCanvas") 
								    obj.AddComponent (typeof(OVRRaycaster));
							} 
						     else 
						    {
							
								obj.GetComponent<OVRRaycaster> ().enabled = true;
							}
						}
					}
				}

				for (int k = 0; k < allCamera.Length; k++)
					for (int canvasnum = 0; canvasnum < allCamera [k].transform.childCount; canvasnum++)
						DestroyImmediate (allCamera [k].transform.GetChild (canvasnum).GetComponent<OVRRaycaster> ());

				foreach (Camera obj in allCamera)
				{
					if (obj.tag == "MainCamera") 
					{
						if (obj.transform.GetComponent<GvrPointerPhysicsRaycaster> () != null) 
						{
						obj.transform.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = false;
						}
					DestroyImmediate (obj.GetComponent<GraphicRaycaster> ());
					}
				DestroyImmediate (obj.GetComponent<OVRRaycaster> ());
			}	
			EditorSceneManager.SaveScene (SceneManager.GetSceneByBuildIndex(i));
			EditorSceneManager.CloseScene (SceneManager.GetSceneByBuildIndex(i),true);
			}

		}

//	[MenuItem("Setup/DayDream-Cardboard")]

	public static void Enable_DayDream_Cardboard()
	{
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) 
		{
			EditorSceneManager.OpenScene (EditorBuildSettings.scenes [i].path, OpenSceneMode.Additive);

			allobjects =Resources.FindObjectsOfTypeAll(typeof(GameObject));
			allCamera = Editor.FindObjectsOfType <Camera>();
		
			foreach (GameObject obj in allobjects)
			{
				GvrPointerGraphicRaycaster gvr = obj.GetComponent (typeof(GvrPointerGraphicRaycaster)) as GvrPointerGraphicRaycaster;
				OVRRaycaster ovr = obj.GetComponent (typeof(OVRRaycaster)) as OVRRaycaster;
				if (obj.GetComponent<Canvas> ())
				{
					if (obj.activeSelf || !obj.activeSelf) 
					{
						if (ovr)
							obj.GetComponent<OVRRaycaster> ().enabled = false;

						if (!gvr) 
						{
							if (obj.name != "MainMenuCanvas") 
								obj.AddComponent (typeof(GvrPointerGraphicRaycaster));
						} 
						else 
						{
							obj.GetComponent<GvrPointerGraphicRaycaster> ().enabled = true;
						}
					}
				}

			}

			for (int k = 0; k < allCamera.Length; k++) 
			{
				for (int canvasnum = 0; canvasnum < allCamera [k].transform.childCount; canvasnum++)
					DestroyImmediate (allCamera [k].transform.GetChild (canvasnum).GetComponent<GvrPointerGraphicRaycaster> ());

				DestroyImmediate (allCamera [k].GetComponent<GvrPointerGraphicRaycaster> ());
			}

			foreach (Camera obj in allCamera) 
			{
				if (obj.tag == "MainCamera") 
				{
					if (obj.transform.GetComponent<GvrPointerPhysicsRaycaster> () != null) {
						obj.transform.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = true;
					}
					if (obj.transform.GetComponent<GvrPointerPhysicsRaycaster> () == null) {
						obj.gameObject.AddComponent (typeof(GvrPointerPhysicsRaycaster));
						obj.GetComponent<GvrPointerPhysicsRaycaster> ().raycasterEventMask.value = -5;
					}
					DestroyImmediate (obj.GetComponent<GraphicRaycaster> ());
				}
			}
				
			if(Camera.main.transform.GetComponent<GvrPointerGraphicRaycaster> () != null)
				DestroyImmediate (Camera.main.transform.GetComponent<GvrPointerGraphicRaycaster> ());

			EditorSceneManager.SaveScene (SceneManager.GetSceneByBuildIndex(i));
			EditorSceneManager.CloseScene (SceneManager.GetSceneByBuildIndex(i),true);
		}
	}
}
