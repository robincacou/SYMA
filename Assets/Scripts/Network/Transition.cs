﻿using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour
{
	public TextMesh text;
	public Node first;
	public Node second;

	public uint initialWeight;
	public uint alteredWeight = 0;

	public Material safe;
	public Material destroyed;

	public GameObject spriteContainer;
	public SpriteRenderer spritePrefab;

	private LineRenderer line;
	private BoxCollider box;

	void Start()
	{
		line = GetComponent<LineRenderer>();
		box = GetComponentInChildren<BoxCollider>();

		line.SetPosition(0, first.transform.position);
		line.SetPosition(1, second.transform.position);
		transform.position = Vector3.Lerp(first.transform.position, second.transform.position, 0.5f);

		float distance = Vector3.Distance(first.transform.position, second.transform.position);
		text.text = initialWeight.ToString();
		box.size = new Vector3(1.5f, 1, distance);
		box.transform.LookAt(first.transform);

		GetComponent<LineRenderer>().material = safe;

		spriteContainer.transform.position = first.transform.position;

		float lineLength = 0f;
		while (lineLength < distance)
		{
			SpriteRenderer sprite = (SpriteRenderer)Instantiate(spritePrefab);
			sprite.transform.parent = spriteContainer.transform;
			sprite.transform.localPosition = new Vector3(0f, 0.06f, lineLength);
			lineLength += sprite.sprite.bounds.size.x;
		}

		spriteContainer.transform.LookAt(second.transform);
	}

	void Update()
	{

	}

	public void SlowDown()
	{
		// TODO Timer to heal ?
		alteredWeight = 3 * initialWeight;

		text.color = new Color(0, 0, 0, 255);
		text.text = (initialWeight + alteredWeight).ToString();

		GetComponent<LineRenderer>().material= destroyed;
	}

	public void Heal()
	{
		alteredWeight = 0;
		text.color = new Color(255, 255, 255, 255);
		text.text = initialWeight.ToString();

		GetComponent<LineRenderer>().material = safe;
	}

	public Node GetOther(Node node)
	{
		if (first == node)
			return second;
		if (second == node)
			return first;
		Debug.LogError("GetOther for " + name + " with invalid node (" + node.name + ")");
		return null;
	}
}
