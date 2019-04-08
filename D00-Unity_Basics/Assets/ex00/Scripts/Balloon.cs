using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

	public float airBalloon;
	private float breath;
	private float maxCapacity;

	// Use this for initialization
	void Start () {
		this.airBalloon = 75;
		this.maxCapacity = 100;
		this.breath = 35;
	}

	void EndGame () {
		Debug.Log ("Balloon life time: " + Mathf.RoundToInt (Time.realtimeSinceStartup) + 's');
		Destroy (this.gameObject);
	}

	void SendBreath () {
		this.airBalloon += 1.5f;
		this.breath -= 25;
	}

	Vector3 BallonScaleUp (float a) {
		return new Vector3 (this.gameObject.transform.localScale.x + a, this.gameObject.transform.localScale.y + a, this.gameObject.transform.localScale.z);
	}

	Vector3 BallonScaleDown (float a) {
		return new Vector3 (this.gameObject.transform.localScale.x - a, this.gameObject.transform.localScale.y - a, this.gameObject.transform.localScale.z);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") && this.breath > 0) {
			this.SendBreath ();
			this.gameObject.transform.localScale = this.BallonScaleUp (1.5f);
		} else if (this.airBalloon < this.maxCapacity && this.gameObject.transform.localScale.y > 0) {
			this.gameObject.transform.localScale = this.BallonScaleDown (0.05f);
			this.breath += 1.5f;
			this.airBalloon -= 0.05f;
		}
		if (this.airBalloon >= this.maxCapacity || this.airBalloon <= 0 || this.gameObject.transform.localScale.y <= 0) {
			this.EndGame ();
		}
	}
}