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
			GameObject instance = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
			Destroy(instance, 5f);
			trans.SlowDown();
		}
		else
		{
			GameObject instance = (GameObject)Instantiate(heal, transform.position, transform.rotation);
			Destroy(instance, 5f);
			trans.Heal();
		}
		w.UpdateWeights();
	}
}
