using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

	public float velocity = 20f;
	// public string setDirection;
	// private GameObject player;
	// Use this for initialization
	// private Vector3 direction;
	private void Start () {

		// switch (setDirection) {
		// 	case "left":
		// 		direction = Vector3.left;
		// 		break;
		// 	default:
		// 		direction = Vector3.right;
		// 		break;
		// }
	}
	private void OnCollisionStay2D (Collision2D other) {
		// other.transform.Translate(velocity ,other.rigidbody.velocity.y );
		other.transform.Translate(Vector3.right * 10f * Time.deltaTime);
	}

	private void OnCollisionEnter2D (Collision2D other) {
		other.transform.parent.transform.Translate(Vector3.right * 100f * Time.deltaTime);
	}
	// private void OnCollisionExit(Collision other) {
	// velocity 
	// }
}