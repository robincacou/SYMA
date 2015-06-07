using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {

	void OnMouseDown()
	{
		WorldHandler world = FindObjectOfType<WorldHandler>();
		if (world != null)
			world.SetSelectedNode(null);
	}
}
