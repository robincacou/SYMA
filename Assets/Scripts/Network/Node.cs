using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	public TextMesh text;

	private ArrayList transitions;
	public ArrayList travellers;
	public uint capacity = 5;

	void Awake()
	{
		transitions = new ArrayList();
		travellers = new ArrayList ();
		text.text = name;
	}

	void Update()
	{
	
	}

	public void AddTransition(Transition trans)
	{
		if (!transitions.Contains(trans))
			transitions.Add(trans);
	}

	public void DebugTrans()
	{
		print(text.text + ", trans : ");
		foreach(Transition trans in transitions)
			print(trans.name);
	}

	public ArrayList GetTransitions()
	{
		return transitions;
	}
}
