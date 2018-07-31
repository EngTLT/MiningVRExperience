using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleControl : MonoBehaviour {
	Rigidbody rb;
	SteamVR_TrackedController controller;

	int count = 0;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	
	void OnTriggerEnter(Collider collider) {
		controller = collider.GetComponent<SteamVR_TrackedController>();
	}

	void OnTriggerStay(Collider collider) {
		if (controller.triggerPressed) {
			transform.parent = controller.transform;
			rb.isKinematic = true;
		}else if(count > 10) {
			transform.parent = null;
			rb.isKinematic = false;
		}
		else {
			count++;
		}
	}


}
