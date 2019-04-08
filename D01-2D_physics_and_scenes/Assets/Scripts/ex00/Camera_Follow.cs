using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_Follow : MonoBehaviour {

	// Use this for initialization

	public playerScript_ex01[] players;

	// [SerializeField]
	private int current = 0;

	void Start () {
		SwitchPlayer(current);
	}

	void RestartScene () {
		if (Input.GetKey ("r"))
			SceneManager.LoadScene ("ex00");
	}

	void CheckcurrentPlayer () {
		if (Input.GetKey ("1"))
			SwitchPlayer (0);
		if (Input.GetKey ("2"))
			SwitchPlayer (1);
		if (Input.GetKey ("3"))
			SwitchPlayer (2);
	}

	void FowardPlayer () {
		transform.position = new Vector3 (players[current].transform.position.x, players[current].transform.position.y, transform.position.z);
	}

	void SwitchPlayer (int index) {
		for (int i = 0; i < players.Length; i++) {
			if (index == i) {
				players[i].setRigiBodyTypeAndMove (true);
			} else
				players[i].setRigiBodyTypeAndMove (false);
		}
		current = index;
	}

	// Update is called once per frame
	void Update () {
		CheckcurrentPlayer ();
		RestartScene ();
		FowardPlayer ();
	}
}