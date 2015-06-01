using UnityEngine;
using System.Collections;

public class Transport : MonoBehaviour
{
	public Node[] Journey;

	private int currentId;
	private bool forward;
	private Node current;
	private Node destination;
	private Transition currentTrans;
	private float timeToDestination;
	private int capacity = 10;
	private ArrayList travellers;

	private bool initialized;
	private float timeMultiplier;

	void Start()
	{
		currentId = 0;
		forward = true;
		if (Journey.Length < 2)
			Debug.LogError("Empty journey (or only one node) for " + name);
		travellers = new ArrayList ();
		initialized = false;
	}

	void Update ()
	{
		if (! initialized)
		{
			current = Journey[0];
			destination = Journey[1];
			transform.position = current.transform.position;
			
			UpdateTransAndSpeed();

			initialized = true;
		}

		// float percentage = Vector3.Distance(current.transform.position, transform.position) / Vector3.Distance(current.transform.position, destination.transform.position);

		float speed = Vector3.Distance(current.transform.position, destination.transform.position) / timeToDestination;
	
		transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed * timeMultiplier * Time.deltaTime);
		if (transform.position == destination.transform.position)
		{
			current = destination;

			if (forward)
				currentId++;
			else
				currentId--;

			if (currentId == 0 || currentId == Journey.Length - 1) // On the first or last node
				forward = !forward;

			if (forward)
				destination = Journey[currentId + 1];
			else
				destination = Journey[currentId - 1];

			//DISEMBARK
			ArrayList travellersToDisembark = new ArrayList();

			foreach(Traveller t in travellers)
			{
				if (!t.StayInTransport(current, destination))
					travellersToDisembark.Add(t);
			}

			foreach(Traveller t in travellersToDisembark)
			{
				travellers.Remove(t);
				t.OnTransportArrived();
			}
			travellersToDisembark.Clear();


			//EMBARK
			ArrayList travellersToEmbark = new ArrayList();
			if (travellers.Count < capacity)
			{
				foreach(Traveller t in current.GetTravellers())
				{
					if(t.ShouldIGoInThisTransport(destination))
						travellersToEmbark.Add(t);
					if (travellers.Count + travellersToEmbark.Count >= capacity)
					{
						//print("MAX CAPACITY ENCOUNTERED");
						break;
					}
				}

				foreach(Traveller t in travellersToEmbark)
				{
					t.OnEmbark();
					travellers.Add(t);
				}

			}
			else
			{
				// print("MAX CAPACITY ENCOUNTERED");
			}

			travellersToEmbark.Clear();

			UpdateTransAndSpeed();
		}
	}

	private void UpdateTransAndSpeed()
	{
		currentTrans = null;
		foreach(Transition trans in current.GetTransitions())
		{
			if((trans.first == destination) || (trans.second == destination))
			{
				currentTrans = trans;
				break;
			}
		}
		if (currentTrans == null)
			Debug.LogError("NO VALID TRANSITION for " + name);
		
		timeToDestination = currentTrans.initialWeight + currentTrans.alteredWeight;
	}

	public void setTimeMultiplier(float mult)
	{
		timeMultiplier = 12 * mult;
	}
}
