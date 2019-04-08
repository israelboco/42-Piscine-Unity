using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	// Use this for initialization
	private float speed;
	public float precision;
	// private GameObject current;
	void Start () {
		speed = Random.Range (2f, 3f);
	}

	// Destroy Object
	void DestroyCubeRef () {

		Debug.Log ("Precision: " + this.precision);
		CubeSpawner.cubeCnt -= 1;
		GameObject.Destroy (this.gameObject);

	}

	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Translate (Vector3.down * speed * Time.deltaTime);
		precision = transform.position.y + 2.5f;
		if (this.gameObject.transform.position.y < -7) {
			this.DestroyCubeRef();
		} else if ((this.gameObject.transform.position.x == -2.35f) && (Input.GetKeyDown ("a"))
			&& (this.gameObject.transform.position.y <= 0.0f)) {
			this.DestroyCubeRef();
		} else if ((this.gameObject.transform.position.x == 0) && (Input.GetKeyDown ("s"))
			&&	(this.gameObject.transform.position.y <= 0.0f)) {
			this.DestroyCubeRef();
		} else if ((this.gameObject.transform.position.x == 2.35f) && (Input.GetKeyDown ("d"))
			&& (this.gameObject.transform.position.y <= 0.0f)) {
			this.DestroyCubeRef();
		}
	}
}
