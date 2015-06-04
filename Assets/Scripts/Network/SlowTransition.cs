﻿using UnityEngine;
using System.Collections;

public class SlowTransition : MonoBehaviour
{
	private Transition trans;
	public GameObject explosion;
	public GameObject heal;

	// Use this for initialization
	void Start()
	{
		trans = GetComponentInParent<Transition>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnMouseDown()
	{
		if (trans.alteredWeight == 0)
		{
			print("Slowing" + transform.parent.name);
			Instantiate(explosion, transform.position, transform.rotation);
			trans.SlowDown();
		}
		else
		{
			print("Healing" + transform.parent.name);
			Instantiate(heal, transform.position, transform.rotation);
			trans.Heal();
		}
		FindObjectOfType<WorldHandler> ().UpdateWeights ();
	}
}
