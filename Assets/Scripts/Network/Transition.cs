using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour
{
	public TextMesh text;
	public Node first;
	public Node second;

	public uint initialWeight;
	public uint alteredWeight = 0;

	private LineRenderer line;
	private BoxCollider box;

	void Start()
	{
		line = GetComponent<LineRenderer>();
		box = GetComponentInChildren<BoxCollider>();

		line.SetPosition(0, first.transform.position);
		line.SetPosition(1, second.transform.position);
		transform.position = Vector3.Lerp(first.transform.position, second.transform.position, 0.5f);
		text.text = "" + initialWeight;
		box.size = new Vector3(1.5f, 1, Vector3.Distance(first.transform.position, second.transform.position));
		box.transform.LookAt(first.transform);
	}

	// TODO When weight changes, update the text

	void Update()
	{

	}
}
