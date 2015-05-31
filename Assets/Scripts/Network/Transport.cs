﻿using UnityEngine;
using System.Collections;

public class Transport : MonoBehaviour {

	public Node[] Journey;

	private int currentId;
	private bool forward;
	private Node current;
	private Node destination;
	private Transition currentTrans;
	private float speed;

	private bool initialized;

	void Start()
	{
		currentId = 0;
		forward = true;
		if (Journey.Length < 2)
			Debug.LogError("Empty journey (or only one node) for " + name);

		initialized = false;
	}

	void Update ()
	{
		if (! initialized)
		{
			current = Journey[0];
			destination = Journey[1];
			transform.position = current.transform.position;
			
			UpdateTransAndSpeed();

			initialized = true;
		}

		transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed * Time.deltaTime);
		if (transform.position == destination.transform.position)
		{
			current = destination;

			if (forward)
				currentId++;
			else
				currentId--;

			if (currentId == 0 || currentId == Journey.Length - 1) // On the first or last node
				forward = !forward;

			if (forward)
				destination = Journey[currentId + 1];
			else
				destination = Journey[currentId - 1];

			UpdateTransAndSpeed();
		}
	}

	void UpdateTransAndSpeed()
	{
		currentTrans = null;
		foreach(Transition trans in current.GetTransitions())
		{
			if((trans.first == destination) || (trans.second == destination))
			{
				currentTrans = trans;
				break;
			}
		}
		if (currentTrans == null)
			Debug.LogError("NO VALID TRANSITION for " + name);
		
		speed = currentTrans.initialWeight; // + alteredWeight
	}
}