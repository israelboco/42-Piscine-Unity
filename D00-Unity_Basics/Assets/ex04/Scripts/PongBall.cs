using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour {

	// Debug.Log, Random.Range, Input.GetKey, Transform.Translate

	// Use this for initialization
	public Player playerLeft, playerRight;
	private float speed;

	private Vector3 direction;

	void ResetBallDirection () {
		speed = 3;
		transform.position = new Vector3 (0f, 0f, 0f);
		int x = Random.Range (-2, 2);
		if (x == 0)
			x = 1;
		int y = Random.Range (-2, 2);
		if (y == 0)
			y = 1;
		direction = new Vector3 (x, y, 0f);
	}
	void Start () {
		ResetBallDirection ();
	}

	void CheckColidedWall () {
		if (transform.position.y >= 4.9 || transform.position.y <= -4.9) {
			direction = new Vector3 (direction.x, -direction.y, 0f);
		}
	}

	void CheckOutWall () {
		if (transform.position.x >= 9) {
			playerLeft.SetScore (1);
			Debug.Log ("Player 1 : " + playerLeft.GetScore () + " | Player 2 : " + playerRight.GetScore ());
			ResetBallDirection ();
		} else if (transform.position.x <= -9) {
			playerRight.SetScore (1);
			Debug.Log ("Player 1 : " + playerLeft.GetScore () + " | Player 2 : " + playerRight.GetScore ());
			ResetBallDirection ();
		}
	}

	bool CheckRightPlayerCol () {
		return (transform.position.x >= 7.6 && transform.position.x <= 8.4 &&
			(playerRight.transform.position.y + 1f >= transform.position.y &&
				playerRight.transform.position.y - 1f <= transform.position.y) &&
			direction.x > 0);
	}

	bool CheckLeftPlayerCol () {
		return (transform.position.x <= -7.6 && transform.position.x >= -8.4 &&
			(playerLeft.transform.position.y + 1f >= transform.position.y &&
				playerLeft.transform.position.y - 1f <= transform.position.y) &&
			direction.x < 0);
	}
	void CheckColidedPlayer () {
		if (CheckRightPlayerCol () || CheckLeftPlayerCol ()) {
			speed += 0.5f;
			direction = new Vector3 (-direction.x, direction.y, 0f);
		}
	}
	// Update is called once per frame
	void Update () {
		transform.Translate (direction * speed * Time.deltaTime);
		CheckColidedPlayer ();
		CheckColidedWall ();
		CheckOutWall ();
	}
}