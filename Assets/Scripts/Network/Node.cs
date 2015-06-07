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

	public GameObject meshContainer;
	public GameObject smallMesh;
	public GameObject bigMesh;
	private WorldHandler w;

	public GameObject hilight;
	public TextMesh nameText;

	private bool isSelected = false;

	void Awake()
	{
		transitions = new ArrayList();
		travellers = new ArrayList ();
		posOfNextTraveller = new Vector2 (0, 0);

		hilight.SetActive(false);

		UpdateMesh();

	}

	void Start()
	{
		w = FindObjectOfType<WorldHandler> ();
		nameText.text = name;
	}

	void Update()
	{
		if (isSelected)
			hilight.SetActive(true);
		text.text = travellers.Count.ToString();	
	}

	void OnMouseEnter()
	{
		hilight.SetActive(true);
	}

	void OnMouseExit()
	{
		hilight.SetActive(false);
	}

	void OnMouseDown()
	{
		w.SetSelectedNode(this);
		isSelected = true;
	}

	public void Deselect()
	{
		isSelected = false;
		hilight.SetActive(false);
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
			if (!t.GetSmartPhone())
			{
				t.SetWaitingTime(0);
				t.SetStack(w.AssignNewPath(this, t.GetDestination()));
			}
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
			t.transform.position = new Vector3 (transform.position.x + 2 +  p.x * t.transform.localScale.x * 2 + 3, 0,
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

	public bool HasTransitionTo(Node other)
	{
		foreach(Transition trans in transitions)
			if (trans.first == other || trans.second == other)
				return true;
		return false;
	}

	public void UpdateMesh()
	{
		smallMesh.SetActive(!informationOn);
		bigMesh.SetActive(informationOn);
	}

	public void SetInformation(bool info)
	{
		informationOn = info;
		UpdateMesh();
	}
}
