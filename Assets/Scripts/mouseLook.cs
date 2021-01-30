using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour {

	float upDownLook = 0f;
	float mouseSensitivity = 1000f;
	float rotationX;

	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
		
	void Update () {

		// 0 . Lock mouse
		if (Input.GetMouseButton(0)) {
			Cursor.lockState = CursorLockMode.Locked; //lock cursor in middle of screen
			Cursor.visible = false;
		}

		// 1. Get mouse input data
		float mouseX = Input.GetAxis ("Mouse X") * Time.deltaTime * mouseSensitivity; //horizontal mouseSpeed
		float mouseY = Input.GetAxis ("Mouse Y") * Time.deltaTime * mouseSensitivity; //vertical mouseSpeed

		rotationX += mouseX;

		// 2. Rotate the camera
		transform.parent.localEulerAngles = new Vector3 (transform.parent.localEulerAngles.x, rotationX, 0f);

		// 2b. Clamp/constrain the X angle (pitch) so we can't look too far up or down
		upDownLook -= mouseY;
		upDownLook = Mathf.Clamp (upDownLook, -80f, 80f); //Constrain look 80 degrees up or down

		// 3. Unroll the camera
		transform.localEulerAngles = new Vector3(upDownLook, transform.localEulerAngles.y, 0f);
	}
}
