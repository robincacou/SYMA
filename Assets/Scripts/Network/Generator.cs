using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Node NodePrefab;
	public GameObject NodesContainer;
	public Transition TransitionPrefab;
	public GameObject TransitionContainer;

	public uint unitsBetweenNodes;
	public uint randomUnitsBetweenNodes;

	private ArrayList Nodes;

	void Awake ()
	{
		GenerateGraph(36);
	}

	void Update () {
	
	}

	public void GenerateGraph(uint numberOfNodes)
	{
		Nodes = new ArrayList();
		int size = (int)Mathf.Ceil(Mathf.Sqrt(numberOfNodes));
		for (uint i = 0; i < numberOfNodes; i++)
		{
			Node node = (Node)Instantiate(NodePrefab);
			node.transform.parent = NodesContainer.transform;
			node.transform.position = new Vector3(unitsBetweenNodes * (i % size) + Random.Range(-randomUnitsBetweenNodes, randomUnitsBetweenNodes), 0,
			                                      unitsBetweenNodes * (i / size) + Random.Range(-randomUnitsBetweenNodes, randomUnitsBetweenNodes));
			Nodes.Add(node);
		}

		for (int i = 0; i < Nodes.Count; i++)
		{
			Node node = (Node)Nodes[i];
			ArrayList possibleNeighbors = new ArrayList();

			// TODO ADD DIAGONALS
			// TODO DO NOT TAKE ALREADY CONNECTED NODES

			// Top
			if (i - size >= 0)
				possibleNeighbors.Add(Nodes[i - size]);
			// Right
			if ((i % size) != (size - 1))
				possibleNeighbors.Add(Nodes[i + 1]);
			// Bottom
			if (i + size < Nodes.Count)
				possibleNeighbors.Add(Nodes[i + size]);
			// Left
			if ((i % size) != 0)
				possibleNeighbors.Add(Nodes[i - 1]);

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
				trans.initialWeight = (uint)Random.Range(10, 1000);
				trans.transform.parent = TransitionContainer.transform;
			}
		}
	}
}
