using UnityEngine;
using System.Collections;

public class Traveller : MonoBehaviour {

	public MeshRenderer mesh;

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

	public bool StayInTransport(Node curr, Node next)
	{
		path.Pop();
		current = curr;
		if (path.Count != 0 && (Node)path.Peek() == next)
			return true;
		transit = false;
		mesh.enabled = true;
		return false;
	}

	public void OnTransportArrived()
	{
		if (current == destination)
		{
			print("ARRIVED AT DESTINATION: " + destination.text.text);
			Destroy (this.gameObject);
		}
		else
			current.AddTraveller (this);
	}

	public void OnEmbark()
	{
		current.RemoveTraveller (this);
	}

	public bool ShouldIGoInThisTransport(Node next)
	{
		if (path.Peek () == next)
		{
			transit = true;
			mesh.enabled = false;
			return true;
		}
		return false;
	}

	public void SetStack(Stack S)
	{
		path = S;
	}

	public Node GetDestination()
	{
		return destination;
	}

	public Node GetCurrent()
	{
		return current;
	}

	public Stack GetStack()
	{
		return path;
	}
}
