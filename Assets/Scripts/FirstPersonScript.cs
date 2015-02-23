// * @author Craig Docherty
// * @inspiration Quill18 FPS Tutorial.
// * @date 16/7/14

using UnityEngine;
using System.Collections;
[RequireComponent (typeof(CharacterController))]
public class FirstPersonScript : MonoBehaviour {
	public float movementSpeed = 10.0f;
	public float mouseSensitivity = 5.0f;
	float verticalRotation = 0;
	public float upDownRange = 60.0f;
	public float jumpSpeed = 5.0f;
	
	float verticalVelocity = 0;
	CharacterController characterController;
	
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController> ();
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//rotation
		float rotLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0, rotLeftRight, 0);
		
		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, (-upDownRange), upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
		
		
		//Movement
		
		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		
		//GetButtonDown is: has the button went down since the last frame?
		//GetButton returns true or false every frame that it is pressed or not.
		if (characterController.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpSpeed;
		}
		
		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);
		
		
		speed = transform.rotation * speed;
		
		
		
		//time.deltaTime - the time since the last frame was done. Consistency over all frame rates for movement.
		
		characterController.Move (speed * Time.deltaTime);
	}
}
