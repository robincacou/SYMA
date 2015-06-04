using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Node NodePrefab;
	public GameObject NodesContainer;
	public Transition TransitionPrefab;
	public GameObject TransitionContainer;
	public Transport TransportPrefab;
	public GameObject TransportsContainer;

	public uint unitsBetweenNodes;
	public uint randomUnitsBetweenNodes;

	private ArrayList Nodes;
	private ArrayList Transitions;

	void Awake ()
	{
	}

	void Update () {
	
	}

	public void GenerateGraph(int numberOfNodes)
	{
		// NODES
		Nodes = new ArrayList();
		int size = (int)Mathf.Ceil(Mathf.Sqrt(numberOfNodes));
		for (uint i = 0; i < numberOfNodes; i++)
		{
			Node node = (Node)Instantiate(NodePrefab);
			node.transform.parent = NodesContainer.transform;
			node.transform.position = new Vector3(unitsBetweenNodes * (i % size) + Random.Range(-randomUnitsBetweenNodes, randomUnitsBetweenNodes), 0,
			                                      unitsBetweenNodes * (i / size) + Random.Range(-randomUnitsBetweenNodes, randomUnitsBetweenNodes));
			node.name = NameGenerator.GenerateStationName();
			Nodes.Add(node);
		}

		// TRANSITIONS
		Transitions = new ArrayList();
		for (int i = 0; i < Nodes.Count; i++)
		{
			Node node = (Node)Nodes[i];
			ArrayList possibleNeighbors = new ArrayList();

			// Top
			if (i - size >= 0)
			{
				AddNodeIfNotConnected(possibleNeighbors, node, (Node)Nodes[i - size]);

				// Top Right
				if (((i % size) != (size - 1)) && !((Node)Nodes[i - size]).HasTransitionTo((Node)Nodes[i + 1]))
					AddNodeIfNotConnected(possibleNeighbors, node, (Node)Nodes[i - size + 1]);
				// Top Left
				if ((i % size) != 0 && !((Node)Nodes[i - size]).HasTransitionTo((Node)Nodes[i - 1]))
					AddNodeIfNotConnected(possibleNeighbors, node, (Node)Nodes[i - size - 1]);
			}
			// Bottom
			if (i + size < Nodes.Count)
			{
				AddNodeIfNotConnected(possibleNeighbors, node, (Node)Nodes[i + size]);

				// Bottom Right
				if ((i % size) != (size - 1) && !((Node)Nodes[i + size]).HasTransitionTo((Node)Nodes[i + 1]))
					AddNodeIfNotConnected(possibleNeighbors, node, (Node)Nodes[i + size + 1]);
				// Bottom Left
				if ((i % size) != 0 && !((Node)Nodes[i + size]).HasTransitionTo((Node)Nodes[i - 1]))
					AddNodeIfNotConnected(possibleNeighbors, node, (Node)Nodes[i + size - 1]);
			}
			// Right
			if ((i % size) != (size - 1))
				AddNodeIfNotConnected(possibleNeighbors, node, (Node)Nodes[i + 1]);
			// Left
			if ((i % size) != 0)
				AddNodeIfNotConnected(possibleNeighbors, node, (Node)Nodes[i - 1]);

			if (possibleNeighbors.Count == 0)
				continue;

			// Multiple random call and removing items are needed in order to pick random neighbors
			int neighborNumber = Random.Range(1, possibleNeighbors.Count);
			for (int j = 0; j < neighborNumber; j++)
			{
				int indexToConnect = Random.Range(0, possibleNeighbors.Count);
				Node toConnect = (Node)possibleNeighbors[indexToConnect];
				possibleNeighbors.RemoveAt(indexToConnect);

				Transition trans = (Transition)Instantiate(TransitionPrefab);
				trans.first = node;
				trans.second = toConnect;
				trans.initialWeight = (uint)Random.Range(10, 400);
				trans.transform.parent = TransitionContainer.transform;

				node.AddTransition(trans);
				toConnect.AddTransition(trans);

				Transitions.Add(trans);
			}
		}

		// TRANSPORTS
		int transIndex = 0;
		while (transIndex < Transitions.Count)
		{
			Transport newTransport = (Transport)Instantiate(TransportPrefab);
			newTransport.transform.parent = TransportsContainer.transform;

			int journeySize = Random.Range(2, 9);
			newTransport.Journey = new Node[journeySize];

			Transition currentTrans = (Transition)Transitions[transIndex];
			newTransport.Journey[0] = currentTrans.first;
			newTransport.Journey[1] = currentTrans.second;

			// Random part
			Node toConnect = currentTrans.second;
			for (int transNumber = 2; transNumber < journeySize; transNumber++)
			{
				ArrayList nodeTransitions = toConnect.GetTransitions();
				int secondIndex = Random.Range(0, nodeTransitions.Count);
				currentTrans = (Transition)nodeTransitions[secondIndex];
				toConnect = currentTrans.GetOther(newTransport.Journey[transNumber - 1]);
				newTransport.Journey[transNumber] = toConnect;
			}

			transIndex++;
		}
	}

	private void AddNodeIfNotConnected(ArrayList possibleNeighbors, Node current, Node toAdd)
	{
		foreach (Transition trans in current.GetTransitions())
		{
			if (trans.first == toAdd || trans.second == toAdd)
				return;
		}
		possibleNeighbors.Add(toAdd);
	}
}
