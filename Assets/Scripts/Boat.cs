using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

	void Start () {
		StartCoroutine(MoveToLakeCenter());
	}

	IEnumerator MoveToLakeCenter() {
		float speed = 0.2f;
		transform.Rotate(0, 0, 5);
		while (transform.position.x < 200f) {
			transform.Translate(Vector3.right * speed, Space.World);
			yield return new WaitForEndOfFrame();
		}
		while(speed > 0.01) {
			transform.Translate(Vector3.right * speed, Space.World);
			speed -= 0.001f;
			transform.Rotate(0, 0, -0.0275f);
			yield return new WaitForEndOfFrame();
		}
	}
}
