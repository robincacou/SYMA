using UnityEngine;
using System.Collections;

public class SlowTransition : MonoBehaviour
{
	private Transition trans;
	public GameObject explosion;
	public GameObject heal;
	private WorldHandler w;

	// Use this for initialization
	void Start()
	{
		trans = GetComponentInParent<Transition>();
		w = FindObjectOfType<WorldHandler> ();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnMouseDown()
	{
		if (trans.alteredWeight == 0)
		{
			Instantiate(explosion, transform.position, transform.rotation);
			trans.SlowDown();
		}
		else
		{
			Instantiate(heal, transform.position, transform.rotation);
			trans.Heal();
		}
		w.UpdateWeights();
	}
}
