﻿using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	public TextMesh text;

	private ArrayList transitions;

	void Awake()
	{
		transitions = new ArrayList();
		text.text = "Node: " + name;
	}

	void Update()
	{
	
	}

	public void AddTransition(Transition trans)
	{
		if (!transitions.Contains(trans))
			transitions.Add(trans);
	}

	public void DebugTrans()
	{
		print(text.text + ", trans : ");
		foreach(Transition trans in transitions)
			print(trans.name);
	}
}
