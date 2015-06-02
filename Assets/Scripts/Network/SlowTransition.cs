using UnityEngine;
using System.Collections;

public class SlowTransition : MonoBehaviour
{
	private Transition trans;

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
			trans.SlowDown();
		}
		else
		{
			print("Healing" + transform.parent.name);
			trans.Heal();
		}
	}
}
