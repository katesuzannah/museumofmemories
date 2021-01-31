using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class holdSwitch : MonoBehaviour {

	public LayerMask myRaycastMask;
	public LayerMask handReticleMask;
	public GameObject currentlyHeld;
	AudioSource memoryAudio;
	AudioSource thisSource;
	public Image reticleImage;
	public Sprite hand;
	public Sprite circle;
	GameObject currentMemoryObject;
	public GameObject reticle;
	playerMovement player;
	Transform startTransform;

	void Start () {
		thisSource = GetComponent<AudioSource> ();
		reticle = GameObject.FindGameObjectWithTag ("Reticle");
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerMovement> ();
	}

	void Update () {
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit rayHit = new RaycastHit ();

		if (Physics.Raycast (ray, out rayHit, 5f, myRaycastMask)) {
			if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer("Interact")) {
				reticleImage.sprite = hand;
			}
			else {
				reticleImage.sprite = circle;
			}
		}

		//If you click, play audio
		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast(ray, out rayHit, 5f, myRaycastMask) && rayHit.collider.tag != "Untagged") {
				if ((rayHit.collider.tag == "Memory")) {
					if (memoryAudio != null) {
						memoryAudio.Stop();
					}
					if (memoryAudio != rayHit.collider.gameObject.GetComponent<AudioSource>()) {
						memoryAudio = rayHit.collider.gameObject.GetComponent<AudioSource>();
						memoryAudio.Play();
					}
					else {
						memoryAudio = null;
                    }
				}
			}
		}
	}
}