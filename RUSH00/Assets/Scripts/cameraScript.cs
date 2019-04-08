using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	public GameObject player;

	public Color color1 = Color.red;
	public Color color2 = Color.blue;
	public float duration = 3.0F;

	public Camera cam;

	void Start () {
		cam = GetComponent<Camera> ();
		cam.clearFlags = CameraClearFlags.SolidColor;
	}

	void Update () {
		float t = Mathf.PingPong (Time.time, duration) / duration;
		cam.backgroundColor = Color.Lerp (color1, color2, t);
		if (Input.GetKey(KeyCode.LeftShift)){
			// transform.Translate(new Vector3(Camera.s))
		}
	}

	void LateUpdate () {
		if (player != null)
			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
	}
}