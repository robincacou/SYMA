using UnityEngine;
using System.Collections;

public class SlowTransition : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnMouseDown()
	{
		GetComponentInParent<Transition>().alteredWeight = GetComponentInParent<Transition>().initialWeight * 3;
	}
}
