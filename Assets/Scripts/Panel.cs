﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel : MonoBehaviour {

	public Text selectText;
	public GameObject innerPanelContainer;
	public Toggle toggle;
	public GameObject parent;

	private Node currentNode;

	public void ActivateInnerPanel(Node current)
	{
		currentNode = current;

		selectText.gameObject.SetActive(currentNode == null);
		innerPanelContainer.SetActive(currentNode != null);

		if (currentNode != null)
			toggle.isOn = currentNode.informationOn;
	}

	public void OnToggleCHange(bool value)
	{
		currentNode.SetInformation(value);
	}
}
