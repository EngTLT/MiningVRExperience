using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlungerBehaviour : MonoBehaviour {
	public GameObject lcontroller, rcontroller, rDoor, lDoor;
	Rigidbody rb;
	SteamVR_TrackedController controller;
	bool handled = false, controllersTrigger = true;
	ParticleSystem explo;

	public GameObject explosion, box;
	public VideoPlayer video1;

	void Update() {
		if (transform.localPosition.y < 11.495) {
			transform.localPosition = new Vector3(transform.localPosition.x, 11.495f, transform.localPosition.z);
			rb.isKinematic = true; //player can't keep pressing it down
			if (explosion != null) 
				explo.Play();

			StartCoroutine(Explosion());
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
		video1.Play(); video1.Pause();

		rb = GetComponent<Rigidbody>();
		explo = explosion.GetComponent<ParticleSystem>();
	}

	//THIS IS OBSOLETE, DELETE WHEN SURE YOU DON'T NEED IT
	//void OnCollisionrEnter(Collision collision) {
	//	controller = collision.collider.GetComponent<SteamVR_TrackedController>();
	//}

	//void OnCollisionStay(Collision collision) {
	//	if (controller.triggerPressed) {
	//		handled = true;
	//	}
	//	else if (count > 10) {
	//		transform.parent = box.transform.parent;
	//		handled = false;
	//	}
	//	else {
	//		count++;
	//	}

	//	if (handled) {
	//		Debug.Log(transform.position.y);
	//		if (controller.transform.position.y > 11.482 && controller.transform.position.y <= 11.624) {
	//			Debug.Log("moving");
	//			transform.position = new Vector3(transform.position.x, controller.transform.position.y, transform.position.z);
	//		}
	//	}
	//}

	IEnumerator Explosion() {
		video1.Play();
		yield return new WaitForSeconds(0.5f);
		
		Transform camrig = lcontroller.GetComponentsInParent<Transform>()[1]; //get transform of parent not controller
		for (int i = 0; i < 40; i++) {
			//this will "shake" the player
			camrig.rotation = Quaternion.Euler(new Vector3(Random.Range(-1, 1), -90, Random.Range(-1, 1))); //DO WE WANT TO KEEP THIS


			SteamVR_Controller.Input((int)lcontroller.GetComponent<SteamVR_TrackedController>().controllerIndex).TriggerHapticPulse(500);
			SteamVR_Controller.Input((int)rcontroller.GetComponent<SteamVR_TrackedController>().controllerIndex).TriggerHapticPulse(500);
			yield return new WaitForSeconds(0.01f);
		}
		camrig.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("Hub");
	}
}
