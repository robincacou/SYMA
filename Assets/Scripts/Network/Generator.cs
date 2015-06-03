using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Node NodePrefab;
	public GameObject NodesContainer;

	public uint unitsBetweenNodes;

	void Start ()
	{
		GenerateGraph(36);
	}

	void Update () {
	
	}

	public void GenerateGraph(uint numberOfNodes)
	{
		uint size = (uint)Mathf.Ceil(Mathf.Sqrt(numberOfNodes));
		for (uint i = 0; i < numberOfNodes; i++)
		{
			Node node = (Node)Instantiate(NodePrefab);
			node.transform.parent = NodesContainer.transform;
			node.transform.position = new Vector3(unitsBetweenNodes * (i % size), 0, unitsBetweenNodes * (i / size));
		}
	}
}
