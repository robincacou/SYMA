﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldHandler : MonoBehaviour {

	public GameObject NodesGO;
	public GameObject TransitionsGO;

	public Traveller TravellerPrefab;
	public GameObject TravellersContainer;

	private Node[] nodes;
	private Transition[] transitions;
	private ArrayList travellers;

	void Start()
	{
		travellers = new ArrayList();
		nodes = NodesGO.GetComponentsInChildren<Node>();
		transitions = TransitionsGO.GetComponentsInChildren<Transition>();

		AssignTransitionsToNodes();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.T))
			SpawnTraveller();

		// DEBUG
		if (Input.GetKeyDown(KeyCode.P))
			foreach(Node node in nodes)
				node.DebugTrans();
		if (Input.GetKeyDown (KeyCode.B)) 
		{
			print("START: " + nodes[3].name);
			print("END: " + nodes[4].name);
			Stack S = findSeq(nodes[4], Dijkstra(nodes[3], false));

			foreach(Node n in S)
				print(n.name);
		}
	}

	private void AssignTransitionsToNodes()
	{
		foreach(Transition trans in transitions)
		{
			trans.first.AddTransition(trans);
			trans.second.AddTransition(trans);
		}
	}

	public Dictionary<Node, Node> Dijkstra(Node start, bool updateOn)
	{
		Dictionary<Node, uint> dist = new Dictionary<Node, uint>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
		ArrayList Q = new ArrayList();

		dist[start] = 0;                       // Distance from source to source
		prev[start] = null;               // Previous node in optimal path initialization


		foreach(Node v in nodes)  // Initialization
		{
			if (v != start)            // Where v has not yet been removed from Q (unvisited nodes)
			{
				dist[v] = 600000;             // Unknown distance function from source to v
				prev[v] = null;            	// Previous node in optimal path from source
			}
			Q.Add(v); // All nodes initially in Q (unvisited nodes)
		}
					
		while(Q.Count != 0)
		{
			Node u = (Node) Q[0];
			foreach(Node vertex in Q)
			{
				if (dist[vertex] < dist[u])
					u = vertex;
			}
			// Source node in first case
			Q.Remove(u);

			foreach(Transition t in u.GetTransitions())
			{
				Node v;
				if (u == t.first)
					v = t.second; // where v is still in Q.
				else
					v = t.first;

				uint alt = dist[u] + t.initialWeight;
				if(updateOn)
					alt += t.alteredWeight;
				if(alt < dist[v])               // A shorter path to v has been found
				{
					dist[v] = alt;
					prev[v] = u;
				}
			}
		}
		return prev;
	}

	public Stack findSeq(Node destination, Dictionary<Node, Node> prev)
	{
		Stack S = new Stack();
		Node u = destination;
		while(prev[u] != null)                 // Construct the shortest path with a stack S
		{
			S.Push(u);  						// Push the vertex onto the stack
			u = prev[u];                         // Traverse from target to source
		}
		return S;
	}

	private void SpawnTraveller()
	{
		int index = Random.Range(0, nodes.Length);
		Traveller trav = (Traveller)Instantiate(TravellerPrefab, nodes[index].transform.position, Quaternion.identity);
		trav.gameObject.transform.parent = TravellersContainer.transform;
		Node curr = nodes[index];
		trav.SetCurrent(curr);
		int destination = Random.Range(0, nodes.Length - 1);
		if (destination >= index)
			destination++;
		trav.SetDestination(nodes[destination]);

		trav.Print(); // TODO REMOVE
		
		Vector2 p = curr.GetPosOfNextTraveller ();

		if (curr.travellers.Count != 0)
			trav.transform.localScale = ((Traveller) curr.travellers[0]).transform.localScale;

		trav.transform.position = new Vector3 (curr.transform.position.x + 5 +  p.x * trav.transform.localScale.x * 2, 5,
		                                       curr.transform.position.z + p.y * trav.transform.localScale.y * 2);

		if (p.x < Mathf.Sqrt(curr.capacity) - 1)
			curr.SetPosOfNextTraveller (p.x + 1, p.y);
		else
			curr.SetPosOfNextTraveller (0, p.y + 1);

		//travellers.Add(trav);
		curr.travellers.Add (trav);
		if (curr.travellers.Count > curr.capacity / trav.transform.localScale.x)
		{
			curr.SetPosOfNextTraveller(0, 0);
			foreach(Traveller t in curr.travellers)
			{
				p = curr.GetPosOfNextTraveller ();
				t.transform.localScale = new Vector3(t.transform.localScale.x / 2, t.transform.localScale.y / 2, t.transform.localScale.z / 2);
				t.transform.position = new Vector3 (curr.transform.position.x + 5 +  p.x * t.transform.localScale.x * 2, 5,
				                                       curr.transform.position.z + p.y * t.transform.localScale.y * 2);
				if (p.x < Mathf.Sqrt(curr.capacity) - 1)
					curr.SetPosOfNextTraveller (p.x + 1, p.y);
				else
					curr.SetPosOfNextTraveller (0, p.y + 1);
			}
		}
	}
}
