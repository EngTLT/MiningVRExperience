using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

	bool inVehicle;

	float speed;

	enum Direction { forward, backward };

	Direction LastDirection;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_TrackedController trackedController;

	public GameObject rightHand, teleportPoint, teleportation, miningVehicle;

	//Audio Stuff
	public AudioSource idlingSound, startSound, drivingSound, stoppingSound;
	bool driving; //is player driving forward?

    public GameObject[] path;

	void Start() {
		inVehicle = false;
		driving = false;
		speed = 0;
		LastDirection = Direction.forward;

		trackedObj = rightHand.GetComponent<SteamVR_TrackedObject>();
		trackedController = rightHand.GetComponent<SteamVR_TrackedController>();

		//call Coroutine that only adds teleport point after intro is done
		StartCoroutine(PlaceTeleportPointinVehicle());
	}

	// Update is called once per frame
	void Update () {

		if (transform.position.x < 375)
			SceneManager.LoadScene("Hub");

		//Check until player is in vehicle before allowing vehicle movement
		if (!inVehicle) {
			if (Vector3.Distance(teleportPoint.transform.position, transform.position) < 2) {
				inVehicle = true; //once teleported into cab of vehicle, update boolean
				transform.rotation = Quaternion.Euler(0, -90f-18.371f, 0); //make camera rig face down the tunnel (so driving is oriented properly)
				miningVehicle.transform.parent = transform; //vehicle transform is now child of camRig, so when camRig moves so does the vehicle
				Destroy(teleportation); //get rid of teleportation (teleportation should be the prefab that allows teleporting in SteamVR)
				Destroy(teleportPoint); //so point doesn't show up when pressing track pad
				GetComponent<Rigidbody>().isKinematic = false; //forces can now affect movement (i.e. gravity)

				StartCoroutine(Audio());
			}
			return; //prevent the rest of codes execution until in vehicle
		}

		float zAngle = transform.rotation.eulerAngles.z;

		if (zAngle > 180)
			zAngle -= 360;

		bool flipped = Mathf.Abs(zAngle) > 90;

		bool forward = controller.GetPress(SteamVR_Controller.ButtonMask.Trigger); //trigger for moving forward
		bool backward = controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad); //touchpad for backward?


		if ((forward || backward) && !flipped) { //___________1
			driving = true; //player is moving

			Vector3 direction = trackedObj.transform.forward;

			if (forward)
				LastDirection = Direction.forward;
			else if (backward) {
				LastDirection = Direction.backward;
			}

			if (speed < 0.1f) //this is the max speed for the vehicle
				speed += 0.001f; //acceleration


			Vector3 newOrientation = new Vector3(0,trackedObj.transform.localRotation.eulerAngles.y,0);

			if (newOrientation.y > 180)
				newOrientation.y -= 360;

			transform.rotation = Quaternion.Euler(newOrientation*0.03f + transform.rotation.eulerAngles);

		}
		else {
			driving = false;
		} //END _____________________1

		if (LastDirection == Direction.forward) {
			transform.position = transform.position + transform.forward.normalized * speed * Time.deltaTime * 40;
		}
		else if (LastDirection == Direction.backward) {
			transform.position = transform.position + transform.forward.normalized * -speed/3 * Time.deltaTime * 40; //reverse at fraction of forward speed
		}

		if (!(forward || backward) && speed > 0) {
			speed -= 0.001f; //DECELERATION	
		}
		else if (speed < 0 ) { //speed will not go below zero
			speed = 0;
		}
	}

	IEnumerator Audio() {
		startSound.Play();
		yield return new WaitForSeconds(2.7f);

		idlingSound.Play();
		
		while (true) {
			yield return new WaitForFixedUpdate();
			if (driving && !drivingSound.isPlaying) {
				drivingSound.Play();
				if(stoppingSound.isPlaying)
					stoppingSound.Stop();
			}
			else if (!driving && drivingSound.isPlaying) {
				drivingSound.Stop();
				stoppingSound.Play();
			}
		}
	}

	IEnumerator PlaceTeleportPointinVehicle() {
		yield return new WaitForSeconds(3); //wait until intro is finished
		teleportPoint.SetActive(true);
	}

}
