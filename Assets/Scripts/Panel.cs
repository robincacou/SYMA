using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel : MonoBehaviour {

	public Text selectText;
	public ScrollRect innerPanel;
	public Scrollbar scrollbar;

	public void ActivateInnerPanel(bool activated)
	{
		selectText.gameObject.SetActive(!activated);
		innerPanel.gameObject.SetActive(activated);
		scrollbar.gameObject.SetActive(activated);
	}
}
