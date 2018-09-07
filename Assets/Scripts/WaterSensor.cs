using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterSensor : MonoBehaviour {
	public GameObject skull, boat;
	public GameObject niceTerrain, badTerrain;
	SteamVR_TrackedController controller;

	bool manipulated = false;

	int count = 0;

	void Start () {
		StartCoroutine(ReadWater());
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Controller")
			controller = collider.GetComponent<SteamVR_TrackedController>();
	}

	void OnTriggerExit(Collider collider) {
		controller = null;
	}

	void OnTriggerStay(Collider collider) {
		
		if(controller != null) {
			if (controller.triggerPressed && !manipulated) {
				transform.parent = controller.gameObject.transform; //to prevent warping scale, empty object is parent and needs to be used
				manipulated = true;
			}
			else if(manipulated && !controller.triggerPressed) {
				count++;
				if (count > 10) {
					count = 0;
					manipulated = false;
				}
			}
			else if (controller.triggerPressed) {
				count = 0;
			}
		}
	}

	IEnumerator ReadWater() {
		yield return new WaitForSeconds(10);
		while (true) {
			Vector3 GlobPos = transform.position;
			if (GlobPos.y < 11 && (GlobPos.z < 240.68f || GlobPos.z > 243.8f))
				break;

			yield return new WaitForEndOfFrame();
		}

		for (int i = 0;i < 10; i++){
			skull.SetActive(false);
			yield return new WaitForSeconds(0.3f);
			skull.SetActive(true);
			yield return new WaitForSeconds(0.3f);
		}

		SteamVR_Fade.Start(Color.black, 4, true);
		yield return new WaitForSeconds(4);
		niceTerrain.SetActive(true);
		badTerrain.SetActive(false);
		SteamVR_Fade.Start(Color.clear, 4, true);

		yield return new WaitForSeconds(10);
		SceneManager.LoadScene("Hub");
	}

}
