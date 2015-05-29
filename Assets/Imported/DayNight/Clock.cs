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
    // Calculate the current hour and minute according to the currentTimeOfDay
    // variable in the DayNightController.
    // The extra calculation for the current minute is to make sure it stays
    // between 0 and 60 and not keeps increasing as the hours increase.
    float currentHour = 24 * controller.currentTimeOfDay;
    float currentMinute = 60 * (currentHour - Mathf.Floor(currentHour));
    gameObject.GetComponent<GUIText>().text = Mathf.Floor(currentHour).ToString() + "h" + Mathf.Floor(currentMinute).ToString();
  }
}