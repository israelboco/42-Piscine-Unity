using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	/* Target And Smoot */
	public Transform target;
	[Range (0, 2)]
	public float pitch = 2f;
	public Vector3 offset;
	public bool lookAtPlayer = false;

	/* Zoom */
	public float zoomSpeed = 4f;
	public float minZoom = 1f;
	public float maxZoom = 15f;
	[SerializeField]
	private float currentZoom = 3f;

	/* Rotate Camera */
	[Range (0, 100)]
	public float yawSpeed = 10f;
	private float currentYaw = 0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame

	private void Update () {

		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

		// currentYaw -= Input.GetAxis ("Horizontal") * yawSpeed * Time.deltaTime;
	}

	private void LateUpdate () {

		transform.position = target.position - offset * currentZoom;
		// Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);
		// transform.position = desiredPosition;
		// if (lookAtPlayer)
			transform.LookAt (target.position + Vector3.up * pitch);
		// else {
			// transform.rotation = target.rotation;
		// }
		// transform.RotateAround (target.position, Vector3.up, currentYaw);
		
	}



}