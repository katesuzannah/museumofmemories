using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	CharacterController playerCharCon;

	Vector3 moveVector;
	public float moveSpeed = 5f;
	public float crouchY;
	float standY;
	float distCovered;
	float fracJourney;
	float journeyLength;
	float startTime;
	public float crouchLerpSpeed;
	bool uncrouching;
	public bool frozen;

	void Start() {
		playerCharCon = GetComponent<CharacterController>();
		standY = Camera.main.transform.localPosition.y;
		uncrouching = false;
	}


	void Update() {
		if (!frozen) {
			float frontBack = Input.GetAxis("Vertical") * moveSpeed;
			float leftRight = Input.GetAxis("Horizontal") * moveSpeed;
			playerCharCon.Move(transform.forward * frontBack * Time.deltaTime);
			playerCharCon.Move(transform.right * leftRight * Time.deltaTime);
		}
		// Add gravity
		playerCharCon.Move(Physics.gravity * .008f); //move the controller down

		//Crouch
		if (Input.GetKeyDown(KeyCode.C)) {
			uncrouching = false;
			startTime = Time.time;
			journeyLength = Vector3.Distance(Camera.main.transform.localPosition, new Vector3(Camera.main.transform.localPosition.x, crouchY, Camera.main.transform.localPosition.z));
		}
		if (Input.GetKey(KeyCode.C)) {
			distCovered = (Time.time - startTime) * crouchLerpSpeed;
			if (journeyLength != 0f) {
				fracJourney = distCovered / journeyLength;
				Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(Camera.main.transform.localPosition.x, crouchY, Camera.main.transform.localPosition.z), fracJourney);
			}
		}
		if (Input.GetKeyUp(KeyCode.C)) {
			startTime = Time.time;
			journeyLength = Vector3.Distance(Camera.main.transform.localPosition, new Vector3(Camera.main.transform.localPosition.x, standY, Camera.main.transform.localPosition.z));
			uncrouching = true;
		}
		if (uncrouching) {
			distCovered = (Time.time - startTime) * crouchLerpSpeed;
			if (journeyLength != 0f) {
				fracJourney = distCovered / journeyLength;
				Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(Camera.main.transform.localPosition.x, standY, Camera.main.transform.localPosition.z), fracJourney);
			}
		}
	}
}