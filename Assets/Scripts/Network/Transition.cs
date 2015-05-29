﻿using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

	public TextMesh text;
	public Node first;
	public Node second;

	public uint weight;


	private LineRenderer line;

	void Start ()
	{
		line = GetComponent<LineRenderer>();

		line.SetPosition(0, first.transform.position);
		line.SetPosition(1, second.transform.position);

		text.text = "" + weight;
		text.transform.position = Vector3.Lerp(first.transform.position, second.transform.position, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
