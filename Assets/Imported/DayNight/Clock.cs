using UnityEngine;

public class Clock : MonoBehaviour
{
  // A reference to the DayNightController script.
  DayNightController controller;

  void Awake()
  {
    // Find the DayNightController game object by its name and get the DayNightController script on it.
    controller = GameObject.Find("DayNightController").GetComponent<DayNightController>();
  }

  void Update()
  {
		float currentHour = controller.GetCurrentHour();
		float currentMinute = controller.GetCurrentMinute();
    gameObject.GetComponent<GUIText>().text = Mathf.Floor(currentHour).ToString() + "h" + Mathf.Floor(currentMinute).ToString();
  }
}