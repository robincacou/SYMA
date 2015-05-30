using UnityEngine;
using System.Collections;

public class WorldHandler : MonoBehaviour {

	public GameObject NodesGO;
	public GameObject TransitionsGO;

	private Node[] nodes;
	private Transition[] transitions;

	void Start()
	{
		nodes = NodesGO.GetComponentsInChildren<Node>();
		transitions = TransitionsGO.GetComponentsInChildren<Transition>();

		AssignTransitionsToNodes();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.P))
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
}
