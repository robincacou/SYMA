using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	public TextMesh text;

	void Start()
	{
		text.text = "Node: " + name;
	}

	void Update()
	{
	
	}
}
