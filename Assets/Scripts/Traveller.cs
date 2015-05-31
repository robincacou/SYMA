using UnityEngine;
using System.Collections;

public class Traveller : MonoBehaviour {

	private Node destination;
	private Node current;
	private bool transit;
	private Stack path;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetDestination(Node dest)
	{
		destination = dest;
	}

	public void SetCurrent(Node cur)
	{
		current = cur;
	}

	public void Print()
	{
		print("Traveller: Current = " + current + ", Destination = " + destination);
	}
}
