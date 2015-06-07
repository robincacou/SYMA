using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Plane : MonoBehaviour {

	void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		WorldHandler world = FindObjectOfType<WorldHandler>();
		if (world != null)
			world.SetSelectedNode(null);
	}
}
