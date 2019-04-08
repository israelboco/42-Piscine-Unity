using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class textRotation : MonoBehaviour {

	//	private float speed = 20;
	public float initialRotationZ = 0.1f;
	public bool isSelected = false;

	// Use this for initialization


	void Start ()

	{

	}

	void Awake () {
		StartCoroutine (rotate ());
	}

	// Update is called once per frame
	IEnumerator rotate () {
		float initialZ = transform.rotation.eulerAngles.z;
		Vector3 currentRotation = new Vector3 (0, 0, initialRotationZ);
		while (true) {
			if (isSelected) {
				float angle = transform.localEulerAngles.z;
				angle = (angle > 180) ? angle - 360 : angle;
				if (angle > initialZ + 3) currentRotation = new Vector3 (0, 0, -0.2f);
				else if (angle < initialZ - 3) currentRotation = new Vector3 (0, 0, 0.2f);
				transform.Rotate (currentRotation);
				// text.color = Color.white;
				
			}
			yield return new WaitForSeconds (0.1f);
		}
	}
}