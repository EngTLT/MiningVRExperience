using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFan : MonoBehaviour {
	bool highlighted = false;
	public Material highlight;
	GameObject player;
	void Start() {
		player = GameObject.Find("[CameraRig]");
	}
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, -1f);
		if(!highlighted && player.transform.position.z < -193) {


			MeshRenderer[] meshrenderer = GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer meshrender in meshrenderer) {
				meshrender.materials = new Material[] { meshrender.material, highlight }; //add a second material to fan
			}

			highlighted = false;
		}
	}
}
