using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporte : MonoBehaviour {

	// Use this for initialization
	public Transform destination;

	private void OnTriggerEnter2D(Collider2D other) {
		other.transform.position = destination.position;
	}
}
