using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] private Transform target;
	private Vector3 _cameraOffset;
	[SerializeField] private float distance, height;
	// Use this for initialization
	void Start () {
		height = 10f;
		distance = 10f;
	}

	// Update is called once per frame
	void Update () {
		TargetFollow();
	}

	private void TargetFollow () {
		transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - distance);
		transform.LookAt(target);
	}
}