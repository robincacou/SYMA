using UnityEngine;
using System.Collections;

public class NoClipFirstPersonController : MonoBehaviour {

  	public float movementForwardMultiplier = 4f;
  	public float movementSideMultiplier = 4f;
	public float runningMultiplier = 2f;
	public float zoomMultiplier = 200f;

 	private string verticalAxis = "Vertical";
 	private string horizontalAxis = "Horizontal";

	private NoClipMouseLook mouseLook = null;

	void Start()
	{
		mouseLook = GetComponent<NoClipMouseLook>();
	}

 	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) // Pressing space locks the camera
			mouseLook.enabled = !mouseLook.enabled;

		float zoom = 0;
		float sideMovement = 0;
		float horizontalMovement = 0;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			// Running
			sideMovement = Input.GetAxis(verticalAxis) * movementSideMultiplier * runningMultiplier * Time.deltaTime;
			horizontalMovement = Input.GetAxis(horizontalAxis) * movementForwardMultiplier * runningMultiplier * Time.deltaTime;
		}
		else
		{
			sideMovement = Input.GetAxis(verticalAxis) * movementSideMultiplier * Time.deltaTime;
			horizontalMovement = Input.GetAxis(horizontalAxis) * movementSideMultiplier * Time.deltaTime;
		}
    	
		
		zoom = Input.GetAxis("Mouse ScrollWheel") * zoomMultiplier * Time.deltaTime;

		Vector3 movementDelta = new Vector3(horizontalMovement, sideMovement, zoom);
    	transform.position += transform.TransformDirection(movementDelta);
  	}
}
