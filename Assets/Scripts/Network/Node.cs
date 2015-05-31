using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	public TextMesh text;

	private ArrayList transitions;
	public ArrayList travellers;
	public uint capacity = 5;
	private Vector2 posOfNextTraveller;

	void Awake()
	{
		transitions = new ArrayList();
		travellers = new ArrayList ();
		posOfNextTraveller = new Vector2 (0, 0);
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

	public Vector2 GetPosOfNextTraveller()
	{
		return posOfNextTraveller;
	}

	public void SetPosOfNextTraveller(float x, float y)
	{
		posOfNextTraveller.x = x;
		posOfNextTraveller.y = y;
	}
}
