using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
  bool isPressed;
	// Use this for initialization
	void Start ()
  {
    isPressed = false;
	}
	void OnGUI()
	{
		if (!isPressed && GUI.Button(new Rect(10, 10, 50, 50), "Speed"))
    {
      GameObject.Find("DayNightController").GetComponent<DayNightController>().timeMultiplier *= 5;
			isPressed = true;
		}
    if (isPressed && GUI.Button(new Rect(10, 10, 50, 50), "Slow"))
    {
      GameObject.Find("DayNightController").GetComponent<DayNightController>().timeMultiplier /= 5;
      isPressed = false;
    }
	}
	// Update is called once per frame
	void Update ()
  {
    OnGUI();
	}
}
