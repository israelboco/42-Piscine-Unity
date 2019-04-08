using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

	public GameObject platform;
	public float moveSpeed;

	public Transform currentPoint;
	public Transform[] points;

	public int pointSelection;


	// private Vector3 direction;
	public float speed = 10f;
	private Vector3 initPos;

	// Use this for initialization
	void Start () {
		// initPos = transform.position;
		currentPoint = points[pointSelection];
	}

	// Update is called once per frame
	void Update () {

		platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);
		if(platform.transform.position == currentPoint.position) {
			pointSelection++;
			if(pointSelection == points.Length)
			{
				pointSelection = 0;
			}
			currentPoint = points[pointSelection];
		}
		// if (transform.position.y >= initPos.y + 5f) {
		// 	moveRight = false;
		// 	direction = Vector3.up;
		// }
		// if (transform.position.y <= initPos.y - 5f) {
		// 	moveRight = true;
		// 	direction = Vector3.down;
		// }
		// if (moveRight) {
		// 	transform.Translate(direction * speed * Time.deltaTime);
		// } else {
		// 	transform.Translate(direction * speed * Time.deltaTime);
			
			// transform.position = new Vector2 (transform.position.x, transform.position.y - speed * Time.deltaTime);
		// }
	}

	private void OnCollisionEnter2D (Collision2D other) {
		other.transform.parent = this.transform;
	}
	private void OnCollisionStay2D (Collision2D other) { }
	private void OnCollisionExit2D (Collision2D other) {
		other.transform.parent = null;
	}
}