using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour {

	Vector3 startPos;
	Vector3 centerPos;
	GameObject reticle;
	float rotationX;
	float rotationY;
	public bool lockY;
	public float localZ;

	void Start () {
		centerPos = new Vector3 (0f, 0f, localZ);
//		reticle = GameObject.FindGameObjectWithTag ("Reticle");
		//transform.localPosition = new Vector3 (0f, 0f, localZ);
	}

	void Update () {
		//if (Input.GetMouseButtonDown(1)) {
//			startPos = transform.localPosition;
//			transform.localPosition = centerPos;
//			mouseLook.rotatingObject = true;
//			reticle.SetActive (false);
//		}
		//if (Input.GetMouseButton(1)) {
			//Rotate according to mouse position
			rotationX += mouseLook.mouseX;
			if (!lockY) {
				rotationY += mouseLook.mouseY;
			}
			else {
				rotationY = 270f;
			}
			// 2. Rotate the camera
			transform.localEulerAngles = new Vector3 (rotationY, -rotationX, 0f);
//		}
//		if (Input.GetMouseButtonUp(1)) {
//			transform.localPosition = startPos;
//			mouseLook.rotatingObject = false;
//			reticle.SetActive (true);
//		}
	}
	public void Enter () {
		centerPos = new Vector3 (0f, 0f, localZ);
//		reticle = GameObject.FindGameObjectWithTag ("Reticle");
		startPos = transform.localPosition;
		transform.localPosition = centerPos;
		//mouseLook.rotatingObject = true;
	}
	public void Exit () {
		//centerPos = new Vector3 (0f, 0f, localZ);
//		reticle = GameObject.FindGameObjectWithTag ("Reticle");
		//transform.localPosition = startPos;
		//mouseLook.rotatingObject = false;
	}
}