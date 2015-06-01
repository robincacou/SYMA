using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	public TextMesh text;

	private ArrayList transitions;
	public ArrayList travellers;
	private Vector2 posOfNextTraveller;
	private uint capacity;

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

	public void AddTraveller(Traveller t)
	{
		/*Vector2 p = posOfNextTraveller;

		if (travellers.Count != 0)
			t.transform.localScale = ((Traveller)travellers [0]).transform.localScale;
		else
			t.transform.localScale = new Vector3 (1, 1, 1);
		t.transform.position = new Vector3 (transform.position.x + 2 +  p.x * transform.localScale.x * 2, 5,
		                                   transform.position.z + p.y * transform.localScale.y * 2);

		if (p.x < Mathf.Sqrt(capacity) - 1)
			SetPosOfNextTraveller (p.x + 1, p.y);
		else
			SetPosOfNextTraveller (0, p.y + 1);*/

		travellers.Add (t);
		SetPosOfNextTraveller(0, 0);
		RePaint ();
		//if (travellers.Count > capacity / t.transform.localScale.x)
			//Paint();
	}

	public void RemoveTraveller(Traveller t)
	{
		travellers.Remove(t);
		SetPosOfNextTraveller(0, 0);
		if (travellers.Count != 0)
			RePaint();
	}

	public void Paint()
	{
		SetPosOfNextTraveller(0, 0);
		foreach(Traveller t in travellers)
		{
			Vector2 p = GetPosOfNextTraveller ();
			//t.transform.localScale = new Vector3(t.transform.localScale.x / 2, t.transform.localScale.y / 2, t.transform.localScale.z / 2);
			t.transform.position = new Vector3 (transform.position.x + 2 +  p.x * t.transform.localScale.x * 2, 5,
			                                    transform.position.z + p.y * t.transform.localScale.y * 2);
			if (p.x < Mathf.Sqrt(capacity) - 1)
				SetPosOfNextTraveller (p.x + 1, p.y);
			else
				SetPosOfNextTraveller (0, p.y + 1);
		}
	}

	public void RePaint()
	{
		Traveller trav = (Traveller)travellers [0];
		//while (travellers.Count > capacity / trav.transform.localScale.x)
			//trav.transform.localScale = new Vector3(trav.transform.localScale.x / 2, trav.transform.localScale.y / 2, trav.transform.localScale.z / 2);

		foreach(Traveller t in travellers)
		{
			Vector2 p = GetPosOfNextTraveller ();
			t.transform.localScale = trav.transform.localScale;
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
