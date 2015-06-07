using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel : MonoBehaviour {

	public Text selectText;
	public GameObject innerPanelContainer;
	public Toggle toggle;
	public GameObject parent;
	public Text travellerButtonPrefab;

	private Node currentNode;

	public void ActivateInnerPanel(Node current)
	{
		currentNode = current;

		selectText.gameObject.SetActive(currentNode == null);
		innerPanelContainer.SetActive(currentNode != null);

		if (currentNode != null)
		{
			toggle.isOn = currentNode.informationOn;

			// Not enough time to add travelers in interface
			/*
			foreach (Traveller trav in currentNode.travellers)
			{
				Text newButton = (Text)Instantiate(travellerButtonPrefab);
				newButton.text = trav.name;
				newButton.GetComponentInChildren<Text>().text = trav.name;
				newButton.transform.SetParent(parent.transform);
			}
			*/
		}
	}

	public void OnToggleCHange(bool value)
	{
		currentNode.SetInformation(value);
	}
}
