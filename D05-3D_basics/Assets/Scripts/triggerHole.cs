using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerHole : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerStay (Collider other) {
		if (other.tag == "Ball") {
			if (Input.GetKey (KeyCode.Return)) {
				if (SceneManager.GetActiveScene ().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
					SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
				else
					SceneManager.LoadScene ("ex00");
			}
		}
	}
}