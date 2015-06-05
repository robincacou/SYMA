using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
  // A reference to the DayNightController script.
  public DayNightController controller;

	public Text text;

  void Awake()
  {
  }

  void Update()
  {
		float currentHour = controller.GetCurrentHour();
		float currentMinute = controller.GetCurrentMinute();
    	text.text = Mathf.Floor(currentHour).ToString() + ":" + Mathf.Floor(currentMinute).ToString();
  }
}