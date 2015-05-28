using UnityEngine;
using System.Collections;

public class NoClipFirstPersonController : MonoBehaviour {

  	public float movementForwardMultiplier = 4f;
  	public float movementSideMultiplier = 4f;
	public float runningMultiplier = 2f;

 	private string forwardAxis = "Vertical";
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

		float forwardMovement = 0;
		float horizontalMovement = 0;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			// Running
    		forwardMovement = Input.GetAxis(forwardAxis) * movementForwardMultiplier * runningMultiplier * Time.deltaTime;
			horizontalMovement = Input.GetAxis(horizontalAxis) * movementSideMultiplier * runningMultiplier * Time.deltaTime;
		}
		else
		{
			forwardMovement = Input.GetAxis(forwardAxis) * movementForwardMultiplier * Time.deltaTime;
			horizontalMovement = Input.GetAxis(horizontalAxis) * movementSideMultiplier * Time.deltaTime;
		}
    	
    	Vector3 movementDelta = new Vector3(horizontalMovement, 0, forwardMovement);
    	transform.position += transform.TransformDirection(movementDelta);
  	}
}
