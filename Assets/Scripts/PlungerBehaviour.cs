using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerBehaviour : MonoBehaviour {
	public GameObject lcontroller, rcontroller, rDoor, lDoor;
	Rigidbody rb;
	SteamVR_TrackedController controller;
	bool handled = false, controllersTrigger = true;
	ParticleSystem explo;
	

	public GameObject explosion, box, video;

	void Update() {
		if (transform.localPosition.y < 11.495) {
			transform.localPosition = new Vector3(transform.localPosition.x, 11.495f, transform.localPosition.z);
			rb.isKinematic = true; //player can't keep pressing it down
			if (explosion != null) 
				explo.Play();


		}
		if (transform.localPosition.y < 11.624) {
			float dist = 11.624f - transform.localPosition.y;
			transform.Translate(0, 0, dist / 30); //translate in z direction because object is rotated and this will move it up
		}else transform.localPosition = new Vector3(transform.localPosition.x, 11.624f, transform.localPosition.z);
		if (transform.localPosition.y > 11.56)
			rb.isKinematic = false;

		if(controllersTrigger && lcontroller.transform.position.x > 207) { //when the player moves into the bunker, this will close the doors and make the mesh collider
																		   //on the controllers not trigger so they can press down the plunger
			lcontroller.GetComponent<SphereCollider>().isTrigger = false;
			rcontroller.GetComponent<SphereCollider>().isTrigger = false;
			controllersTrigger = false;

			rDoor.transform.Rotate(Vector3.up, -90);
			lDoor.transform.Rotate(Vector3.up, 90);

		}
		
	}

	int count = 0;

	void Start() {
		rb = GetComponent<Rigidbody>();
		explo = explosion.GetComponent<ParticleSystem>();
	}

	void OnCollisionrEnter(Collision collision) {
		controller = collision.collider.GetComponent<SteamVR_TrackedController>();
	}

	void OnCollisionStay(Collision collision) {
		if (controller.triggerPressed) {
			handled = true;
		}
		else if (count > 10) {
			transform.parent = box.transform.parent;
			handled = false;
		}
		else {
			count++;
		}

		if (handled) {
			Debug.Log(transform.position.y);
			if (controller.transform.position.y > 11.482 && controller.transform.position.y <= 11.624) {
				Debug.Log("moving");
				transform.position = new Vector3(transform.position.x, controller.transform.position.y, transform.position.z);
			}
		}
	}
}
