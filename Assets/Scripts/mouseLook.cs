using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour {

	float upDownLook = 0f;
	float mouseSensitivity = 1000f;
	float rotationX;
	public static bool rotatingObject;
	public static float mouseX;
	public static float mouseY;
	public LayerMask myRaycastMask;
	public GameObject currentlyHeld;
	Transform[] currentlyHeldChildren;
	Rigidbody currentRB;
	Collider[] currentColliders;
	public GameObject reticle;
	playerMovement player;
	Transform startTransform;
	public float forceAmount;

	void Start() {
		rotatingObject = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
	}

	void Update() {
		// 0 . Lock mouse
		if (Input.GetMouseButton(0)) {
			Cursor.lockState = CursorLockMode.Locked; //lock cursor in middle of screen
			Cursor.visible = false;
		}
		// 1. Get mouse input data
		mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity; //horizontal mouseSpeed
		mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity; //vertical mouseSpeed

		if (!rotatingObject) {
			rotationX += mouseX;
			// 2. Rotate the camera
			transform.parent.localEulerAngles = new Vector3(transform.parent.localEulerAngles.x, rotationX, 0f);
			// 2b. Clamp/constrain the X angle (pitch) so we can't look too far up or down
			upDownLook -= mouseY;
			upDownLook = Mathf.Clamp(upDownLook, -80f, 80f); //Constrain look 80 degrees up or down
															 // 3. Unroll the camera
			transform.localEulerAngles = new Vector3(upDownLook, transform.localEulerAngles.y, 0f);
		}

		//Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.green);
		//Check for interactions
		if (Input.GetMouseButtonDown(0)) {
			if (currentlyHeld != null) {
				Drop();
			}
			else {
				Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
				RaycastHit rayHit = new RaycastHit();
				if (Physics.Raycast(ray, out rayHit, 5f, myRaycastMask) && rayHit.collider.tag != "Untagged") {
					if (rayHit.collider.tag == "Item") {
						currentlyHeld = rayHit.collider.gameObject;
						currentlyHeldChildren = currentlyHeld.GetComponentsInChildren<Transform>();
						foreach (Transform child in currentlyHeldChildren) {
							child.gameObject.layer = LayerMask.NameToLayer("Held");
						}
						currentlyHeld.transform.position = transform.position;
						currentlyHeld.transform.SetParent(Camera.main.transform);
						currentRB = currentlyHeld.GetComponent<Rigidbody>();
						currentRB.isKinematic = true;
						currentlyHeld.GetComponent<rotateObject>().enabled = true;
						reticle.SetActive(false);
						player.frozen = true;
						currentlyHeld.GetComponent<rotateObject>().Enter();
						currentRB.detectCollisions = false;
						currentColliders = currentlyHeld.GetComponentsInChildren<Collider>();
						foreach (Collider col in currentColliders) {
							col.enabled = false;
						}
						//Play audio
						Debug.Log("item");
					}
				}
			}
		}
	}

	void Drop() {
		//Drop it
		if (currentlyHeld != null) {
			foreach (Transform child in currentlyHeldChildren) {
				child.gameObject.layer = LayerMask.NameToLayer("Items");
			}
			currentlyHeld.transform.localPosition = new Vector3(0f, 0f, 1f);
			currentlyHeld.transform.SetParent(null);
			currentRB.isKinematic = false;
			reticle.SetActive(true);
			currentlyHeld.GetComponent<rotateObject>().Exit();
			currentlyHeld.GetComponent<rotateObject>().enabled = false;
			currentRB.detectCollisions = true;
			foreach (Collider col in currentColliders) {
				col.enabled = true;
			}
			currentRB.AddForce(Camera.main.transform.forward * forceAmount);
			currentlyHeld = null;
		}
		else {
			mouseLook.rotatingObject = false;
		}
		player.frozen = false;
	}
}