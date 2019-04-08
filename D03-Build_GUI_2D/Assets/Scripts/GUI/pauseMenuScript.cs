using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenuScript : MonoBehaviour {

	public gameManager manager;
	public GameObject exitConfirm;
	public GameObject pauseMenu;
	private bool pause = false;

	// Use this for initialization
	void Start () {
		SetComponent (pauseMenu.GetComponent<CanvasGroup> (), 0.0f, false, false);
		SetComponent (exitConfirm.GetComponent<CanvasGroup> (), 0.0f, false, false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !pause) {
			setPause (true);
			displayMenu ();
		}
	}

	// Pause Menu
	public void PauseContinueButton () {
		hideMenu ();
	}

	public void PauseQuitButton () {
		displayExitMenu ();
	}

	// Confirmed Button
	public void ConfirmedContinueButton () {
		hideExitMenu ();
	}
	public void ConfirmedExitButton () {
		SceneManager.LoadScene ("ex00");
	}

	void displayExitMenu () {
		SetComponent (pauseMenu.GetComponent<CanvasGroup> (), 0.5f, false, false);
		SetComponent (exitConfirm.GetComponent<CanvasGroup> (), 1, true, true);
	}

	void hideExitMenu () {
		Debug.Log ("hide exit menu");
		SetComponent (exitConfirm.GetComponent<CanvasGroup> (), 0, false, false);
		displayMenu ();
	}

	void displayMenu () {

		SetComponent (pauseMenu.GetComponent<CanvasGroup> (), 1, true, true);
	}

	void hideMenu () {
		setPause (false);
		SetComponent (pauseMenu.GetComponent<CanvasGroup> (), 0, false, false);
	}

	void setPause (bool isPos) {
		pause = isPos;
		manager.pause (isPos);
	}

	void SetComponent (CanvasGroup canava, float alpha, bool blocksRaycasts, bool interactable) {
		canava.alpha = alpha;
		canava.blocksRaycasts = blocksRaycasts;
		canava.interactable = interactable;
	}
}