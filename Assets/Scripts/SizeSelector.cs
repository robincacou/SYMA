using UnityEngine;
using System.Collections;

public class SizeSelector : MonoBehaviour {

	public GameObject canvas;
	public Generator generator;
	public WorldHandler world;
	public DayNightController controller;

	public void StartGeneration(int nodesNumber)
	{
		generator.GenerateGraph(nodesNumber);
		canvas.gameObject.SetActive(true);
		world.gameObject.SetActive(true);
		controller.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}
}
