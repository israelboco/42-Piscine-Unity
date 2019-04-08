using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float velocity;
	public Vector3 direction;
	private float slowing = 0.1f;
	public bool ballStop = true;
	public bool changePosition = false;
	// Use this for initialization
	public int score;

	void Start () {
		direction = Vector3.up;
		score = -15;
	}

	Vector3 DirectionChange () {
		return new Vector3 (direction.x, -direction.y, direction.z);
	}

	Vector3 getNewPositionBall (Vector3 pos) {

		float yPos = Mathf.Clamp (pos.y, -4.8f, 4.8f);
		float xPos = Mathf.Clamp (pos.x, -2f, 2f);
		// Update the position.
		return new Vector3 (xPos, yPos, pos.z);
	}

	public void DisplayScore (bool win) {
		if (win) {
			transform.position =  new Vector3(transform.position.x, transform.position.y, 5);
			Debug.Log ("Score: " + score);
			return;
		}
		score += 5;
		Debug.Log ("Score: " + score);
		if (score == 5) {
			Debug.Log ("You Lose");
		}
	}
	void ChangeDirection () {
		if (transform.position.y >= 4.8f || transform.position.y <= -4.8f) {
			direction = DirectionChange ();
		}
	}

	public void CheckDirection () {
		if (direction.y <= 3.8f)
			direction = Vector3.up;
	}

	bool CheckColided (Vector3 col) {
		return (col.y <= 4.1f &&
			col.y >= 3.5f &&
			col.x <= 0.5f &&
			col.x >= -0.5f);
	}

	public bool CheckBallEnterHole () {
		return (velocity <= 1.5f && this.CheckColided (transform.position));
	}

	void HitBallMove () {
		ballStop = !ballStop;
		transform.Translate (direction * velocity * Time.deltaTime);
		velocity -= slowing;
	}

	// Update is called once per frame
	void Update () {
		if (velocity > 0)
			HitBallMove ();
		transform.position = getNewPositionBall (transform.position);
		ChangeDirection ();
		if (velocity <= 0) {
			ballStop = !ballStop;
			velocity = 0;
		}

	}
}