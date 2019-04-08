using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	public GameObject playerLeft;
	public GameObject playerRight;

	private float speed;
	private int score;

	void Start () {
		speed = 10;
		score = 0;
	}

	public int GetScore() {
		return this.score;
	}

	public void SetScore(int i) {
		this.score += i;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("w") && playerLeft.transform.position.y < 4f)
			playerLeft.transform.Translate (Vector3.up * speed * Time.deltaTime);
		if (Input.GetKey ("s") && playerLeft.transform.position.y > -4f)
			playerLeft.transform.Translate (Vector3.down * speed * Time.deltaTime);
		if (Input.GetKey ("up") && playerRight.transform.position.y < 4f)
			playerRight.transform.Translate (Vector3.up * speed * Time.deltaTime);
		if (Input.GetKey ("down") && playerRight.transform.position.y > -4f)
			playerRight.transform.Translate (Vector3.down * speed * Time.deltaTime);
	}
}