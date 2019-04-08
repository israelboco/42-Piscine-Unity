using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commandButtonsScript : MonoBehaviour {

	public gameManager manager;

	public void play() {
		manager.changeSpeed (1);
	}

	public void speedButton() {
		manager.changeSpeed (2.5f);
	}

	public void speedMoreButton() {
		manager.changeSpeed (5);
	}

	public void pause() {
		manager.changeSpeed (0);
	}
}
