using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	public TextMesh text;

	private ArrayList transitions;
	public ArrayList travellers;
	private Vector2 posOfNextTraveller;
	private uint capacity;
	public bool informationOn = true;

	void Awake()
	{
		transitions = new ArrayList();
		travellers = new ArrayList ();
		posOfNextTraveller = new Vector2 (0, 0);
	}

	void Update()
	{
		text.text = travellers.Count.ToString();	
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

	public void InformTravellers()
	{
		foreach (Traveller t in travellers) {
			t.SetWaitingTime(0);
			t.SetStack(FindObjectOfType<WorldHandler> ().AssignNewPath(this, t.GetDestination()));
		}
	}

	public void AddTraveller(Traveller t)
	{
		travellers.Add (t);
		SetPosOfNextTraveller(0, 0);
		RePaint ();
	}

	public void RemoveTraveller(Traveller t)
	{
		travellers.Remove(t);
		SetPosOfNextTraveller(0, 0);
		if (travellers.Count != 0)
			RePaint();
	}

	public void RePaint()
	{
		foreach(Traveller t in travellers)
		{
			Vector2 p = GetPosOfNextTraveller ();
			t.transform.position = new Vector3 (transform.position.x + 2 +  p.x * t.transform.localScale.x * 2, 5,
			                                    transform.position.z + p.y * t.transform.localScale.y * 2);
			if (p.x < Mathf.Sqrt(capacity) - 1)
				SetPosOfNextTraveller (p.x + 1, p.y);
			else
				SetPosOfNextTraveller (0, p.y + 1);
		}
	}

	public ArrayList GetTravellers()
	{
		return travellers;
	}

	public void SetCapacity(uint c)
	{
		capacity = c;
	}
}
