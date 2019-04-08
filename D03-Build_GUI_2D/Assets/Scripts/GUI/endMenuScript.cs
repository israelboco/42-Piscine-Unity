using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endMenuScript : MonoBehaviour {

	public Text title, score, grade, buttonText;
	public GameObject objmanager;
	private gameManager manager;
	private enum gameState { WIN, LOOSE, INGAME }

	private gameState state = gameState.INGAME;

	private int energieScore, hpScore, playerScore;

	// Use this for initialization
	void Start () {
		SetComponent (GetComponent<CanvasGroup> (), 0, false, false);
		manager = objmanager.GetComponent<commandButtonsScript> ().manager;
	}

	// Update is called once per frame
	void Update () {
		if (manager.playerHp <= 0) state = gameState.LOOSE;
		else if (manager.lastWave) state = gameState.WIN;
		if (state == gameState.LOOSE) LooseCondition ();
		else if (state == gameState.WIN) WinCondition ();
	}

	void LooseCondition () {
		manager.pause (true);
		title.text = "You loose :(";
		score.text = manager.score.ToString ();
		grade.text = "YOU SUXX";
		buttonText.text = "Retry";
		DisplayMenu ();
	}

	void WinCondition () {
		GetScore ();
		manager.pause (true);
		title.text = "You win !";
		switch (playerScore) {
			case 10:
				grade.text = "SSS+";
				break;
			case 9:
				grade.text = "SSS";
				break;
			case 8:
				grade.text = "SS";
				break;
			case 7:
				grade.text = "S";
				break;
			case 6:
				grade.text = "A";
				break;
			case 5:
				grade.text = "B";
				break;
			case 4:
				grade.text = "C";
				break;
			case 3:
				grade.text = "D";
				break;
			case 2:
				grade.text = "E";
				break;
			case 1:
				grade.text = "F";
				break;
			default:
				grade.text = "-";
				break;
		}
		score.text = manager.score.ToString ();
		buttonText.text = "Next level";
		DisplayMenu ();
	}

	void DisplayMenu () {
		SetComponent (GetComponent<CanvasGroup> (), 1, true, true);
	}

	void HideMenu () {
		SetComponent (GetComponent<CanvasGroup> (), 0, false, false);
	}

	void GetScore () {
		if (manager.playerHp == manager.playerMaxHp) hpScore = 5;
		else if (manager.playerHp > 17) hpScore = 4;
		else if (manager.playerHp > 13) hpScore = 3;
		else if (manager.playerHp > 7) hpScore = 2;
		else hpScore = 1;
		if (manager.playerEnergy > 500) energieScore = 5;
		else if (manager.playerEnergy > 300) energieScore = 4;
		else if (manager.playerEnergy > 250) energieScore = 3;
		else if (manager.playerEnergy > 100) energieScore = 2;
		else energieScore = 1;
		playerScore = energieScore + hpScore;
	}

	void SetComponent (CanvasGroup canava, float alpha, bool blocksRaycasts, bool interactable) {
		canava.alpha = alpha;
		canava.blocksRaycasts = blocksRaycasts;
		canava.interactable = interactable;
	}

	public void selectButtonEndRestartOrNext () {
		if (state == gameState.LOOSE) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		} else if (state == gameState.WIN)
			if (SceneManager.GetActiveScene ().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
			else
				SceneManager.LoadScene ("ex00");
	}
}