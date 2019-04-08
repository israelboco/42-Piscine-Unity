using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public int score;
	private Vector3 initPosition;

	public bool stop;
	void Start () {
		initPosition = transform.position;
		stop = false;
	}
	

	void RestartPipes(){
		if (transform.position.x < -8f){
			transform.position = initPosition;
			score += 5;
			if(speed < 20)
				speed += 1;
		}
	}
	// Update is called once per frame
	void Update () {
		if(!stop) {
			transform.Translate(Vector3.left * speed * Time.deltaTime);
			RestartPipes();
		} else {
			
		}
	}
}
