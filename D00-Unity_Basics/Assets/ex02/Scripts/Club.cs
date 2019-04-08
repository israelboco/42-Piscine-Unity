using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {

	public Ball ball;
	public float strenght = 0;

	private bool stopClubLaunch = true;
	private bool positionClub = true;
	private Vector3 initPos;
	private Vector3 prevPos;

	private bool endGame = false;

	// Use this for initialization
	void Start () {
		initPos = this.gameObject.transform.position;
	}

	void ChangeInitPositionClub (Vector3 ballpos) {
		this.gameObject.transform.position = new Vector3 (ballpos.x - 0.2f, ballpos.y, ballpos.z);
		if (ballpos.y > 4f) {
			//need rotation here
		}
		initPos = this.gameObject.transform.position;

	}
	void InitClubPosition (Vector3 pos) {
		this.gameObject.transform.position = pos;
	}
	// Update is called once per frame
	void Update () {
		if (!endGame) {
			if (ball.CheckBallEnterHole()) {
				ball.DisplayScore(true);
				endGame = true;
			} else {
				if (ball.ballStop && ball.velocity == 0 && !positionClub) {
					ChangeInitPositionClub (ball.transform.position);
					ball.DisplayScore (false);
					positionClub = true;
				}
				if (Input.GetKey ("space") && ball.velocity == 0) {
					if (this.stopClubLaunch) {
						InitClubPosition (this.initPos);
					}
					if (this.strenght < 20f) {
						this.strenght += 1.1f;
						this.gameObject.transform.Translate (Vector3.down * this.strenght * Time.deltaTime);
					}
					this.stopClubLaunch = false;
				} else if (this.strenght != 0) {
					if (!stopClubLaunch) {
						InitClubPosition (this.initPos);
						stopClubLaunch = true;
						ball.CheckDirection ();
						ball.velocity = strenght;
						positionClub = false;
						strenght = 0;
					}
				}
			}
		}
	}
}