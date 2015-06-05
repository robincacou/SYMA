using UnityEngine;
using System.Collections;

public class Traveller : MonoBehaviour {

	public MeshRenderer mesh;
	public Material normal;
	public Material phone;

	private Node destination;
	private Node current;
	private bool transit;
	private Stack path;
	private uint waitingTime;
	private bool smartPhone;

	void Awake()
	{
		SetSmartPhone(false);
	}

	// Use this for initialization
	void Start ()
	{
		waitingTime = 0;
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
		current = curr;
		path.Pop();

		if (smartPhone && current != destination)
			path = FindObjectOfType<WorldHandler> ().AssignNewPath (current, destination);

		if (path.Count != 0 && (Node)path.Peek() == next)
			return true;
		transit = false;
		mesh.enabled = true;
		return false;
	}

	public void OnTransportArrived()
	{
		if (current == destination || path.Count == 0)
		{
			if ((path.Count != 0 && current == destination) || (path.Count == 0 && current != destination))
			{
				print("ERROR IN TRANSPORTARRIVED");
				print("PATH COUNT: " + path.Count);
				print("SMARTPHONE: " + smartPhone);
				print("CURRENT " + current.name+ " Destination: " + destination.name);
			}
			FindObjectOfType<WorldHandler> ().OnTravellerLeaves ();
			Destroy (this.gameObject);
		} else {
			current.AddTraveller (this);
			if (current.informationOn && !smartPhone)
				path = FindObjectOfType<WorldHandler> ().AssignNewPath(current, destination);
		}
	}

	public void OnEmbark()
	{
		waitingTime = 0;
		current.RemoveTraveller (this);
	}

	public bool ShouldIGoInThisTransport(Node next)
	{
		if (path.Count == 0) {
			current.RemoveTraveller(this);
			FindObjectOfType<WorldHandler> ().OnTravellerLeaves ();
			print("ERROR IN SHOULDIGOINTRANSPORT");
			Destroy (this.gameObject);
			return false;
		}
		if (path.Peek () == next)
		{
			transit = true;
			mesh.enabled = false;
			return true;
		}
		return false;
	}

	public void CheckingWaitingTime(Transition t)
	{
		++waitingTime;
		if (waitingTime % 3 == 0)
		{
			Node next = (Node) path.Peek ();
			if (smartPhone)
				path = FindObjectOfType<WorldHandler> ().AssignNewWaitingPath (current, destination, waitingTime, true, t);
			else
				path = FindObjectOfType<WorldHandler> ().AssignNewWaitingPath (current, destination, waitingTime, current.informationOn, t);

			if (next != path.Peek())
				waitingTime = 0;
		}
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

	public void SetWaitingTime(uint i)
	{
		waitingTime = i;
	}

	public uint GetWaitingTime()
	{
		return waitingTime;
	}

	public bool GetSmartPhone()
	{
		return smartPhone;
	}

	public void SetSmartPhone(bool b)
	{
		smartPhone = b;

		if (smartPhone)
			mesh.material = phone;
		else
			mesh.material = normal;
	}


}
