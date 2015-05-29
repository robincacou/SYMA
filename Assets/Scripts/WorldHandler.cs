using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldHandler : MonoBehaviour {

	public GameObject NodesGO;
	public GameObject TransitionsGO;

	private Node[] nodes;
	private Transition[] transitions;

	void Start ()
	{
		nodes = NodesGO.GetComponentsInChildren<Node>();
		transitions = TransitionsGO.GetComponentsInChildren<Transition>();

		AssignTransitionsToNodes();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.A))
			foreach(Node node in nodes)
				node.DebugTrans();
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

			foreach(Transition t in u.getTransitions())
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

	public Stack findSeq(Node start, Node destination, Dictionary<Node, Node> prev)
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
}