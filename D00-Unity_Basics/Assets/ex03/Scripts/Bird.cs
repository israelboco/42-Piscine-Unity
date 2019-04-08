using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	// Use this for initialization

	public Pipe pipe;
	private Vector3 direction;

	// public 
	public float gravity;
	public float jump;

	private bool playerDead = false;
	private bool animatedDead = false;
	void Start () {
		direction = Vector3.down;
		jump = 50f;
		gravity = 0f;
	}

	bool CheckColidedGround () {
		return (transform.position.y <= -2.8);
	}

	bool CheckColidedPipes() {
		return (pipe.transform.position.x <= 0.8 && pipe.transform.position.x >= -0.5f && (transform.position.y >= 2.8f || transform.position.y <= -1f));
	}

	void CheckMaxGravity () {
		if (gravity < 11.8)
			gravity += 0.2f;
	}

	void AnimatedDead() {
		if (transform.position.y > -2.8) {
			transform.Translate(Vector3.up * (5 * Time.deltaTime));
		}
		if (transform.position.y < -2.8) {
			transform.position = new Vector3(0,-2.8f,-2);
			animatedDead = true;	
		}
	}

	bool CheckPlayerDead () {
		if (CheckColidedGround () || CheckColidedPipes()) {
			if (!playerDead) {
				transform.Rotate(0f,0f, -180f);
				Debug.Log("Score: " + pipe.score);
				Debug.Log("Time: " + Mathf.RoundToInt(Time.time) + "s");
			}
			pipe.stop = true;
			playerDead = true;
		}
		return playerDead;
	}
	// Authorize function  : Debug.Log, Input.GetKeyDown, Transform.Rotate, Transform.Translate, Mathf.RoundToInt

	// Update is called once per frame
	void Update () {
		if (!CheckPlayerDead ()) {
			CheckMaxGravity ();
			if (Input.GetKeyDown ("space")) {
				transform.Translate (-direction * (jump * Time.deltaTime));
				gravity = 0;
			} else {
				transform.Translate (direction * (gravity * Time.deltaTime));
			}
		} else {
			if(!animatedDead)
				AnimatedDead();
		}
	}
}