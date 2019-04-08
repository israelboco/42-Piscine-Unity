using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Camera_Follow01 : MonoBehaviour {
	// Use this for initialization

	public playerScript_ex01[] players;

	// [SerializeField]
	private int current = 0;
	// private bool endGame = false;
	private bool changeLevel = false;
	public string nextScene;

	void Start () {
		SwitchPlayer (current);
		changeLevel = false;
	}

	void RestartScene () {
		if (Input.GetKey ("r")) {
			Scene scene = SceneManager.GetActiveScene ();
			SceneManager.LoadScene (scene.name);
		}
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

	bool EndGame () {
		foreach (playerScript_ex01 player in players) {
			if (!player.checkedBoxAligned) {
				return false;
			}
		}
		return true;
	}

	// Update is called once per frame
	void Update () {
		if (!changeLevel) {
			if (!EndGame ()) {
				CheckcurrentPlayer ();
				RestartScene ();
				FowardPlayer ();
			} else {
				Debug.Log ("GG gonext lvl");
				if(nextScene != "null")
					SceneManager.LoadScene (nextScene);
				else
					Debug.Log ("Game FINISH GROS BRO!");
				changeLevel = true;
			}
		}
	}
}