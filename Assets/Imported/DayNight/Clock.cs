using UnityEngine;

public class Clock : MonoBehaviour
{
  // A reference to the DayNightController script.
  public DayNightController controller;

  void Awake()
  {
  }

  void Update()
  {
		float currentHour = controller.GetCurrentHour();
		float currentMinute = controller.GetCurrentMinute();
    gameObject.GetComponent<GUIText>().text = Mathf.Floor(currentHour).ToString() + "h" + Mathf.Floor(currentMinute).ToString();
  }
}