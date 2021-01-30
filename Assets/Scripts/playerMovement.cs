using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	CharacterController playerCharCon;

	Vector3 moveVector;
	public float moveSpeed = 5f;


	void Start () {
		playerCharCon = GetComponent<CharacterController>();
	}
	

	void Update () {
		float frontBack = Input.GetAxis("Vertical") * moveSpeed;
		float leftRight = Input.GetAxis("Horizontal") * moveSpeed;
		playerCharCon.Move(transform.forward * frontBack * Time.deltaTime);
		playerCharCon.Move(transform.right * leftRight * Time.deltaTime);
		// Add gravity
		playerCharCon.Move(Physics.gravity*.008f); //move the controller down
	}
}
