using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class RenderCamera : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {
/// <summary>
	/// This script is used while using the render camera  or for using more than one camera in the scene. 
	/// Or to render the object on the top of other objet so that the object could not intesect each other.
/// </summary>
	[Header("\tDrop entire meshes of the object to put on top while dragging")]
	public Renderer[] Meshes;

	public void OnPointerDown(PointerEventData data)
	{
		for (int i = 0; i < Meshes.Length; i++) 
		{
			Meshes [i].gameObject.layer = 11;
		}
//		GameObjectController.Instance.Reticle.layer = 12;
	}
	public void OnPointerUp(PointerEventData data)
	{
		for (int i = 0; i < Meshes.Length; i++) {
			Meshes [i].gameObject.layer = 9;
		}
//		GameObjectController.Instance.Reticle.layer = 0;
	}
}
