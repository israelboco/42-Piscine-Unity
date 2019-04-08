using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelMenu : MonoBehaviour {

	public Text currentLevelName, currentLevelScore, playerDeath, playerRings;
	public int currentLevel = 0;
	public int maxLevels = 4;
	public playerProfile profile;

	public Image[] levels;

	// Use this for initialization
	void Start () {
		playerRings.text = profile.GetPlayerRings ().ToString ();
		playerDeath.text = profile.GetPlayerDeath ().ToString ();
	}

	// Update is called once per frame
	void Update () {
		GetKeyInput ();
		SwitchDisplayFromLevel ();
	}


	void GetKeyInput () {
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			currentLevel++;
			if (currentLevel >= maxLevels) currentLevel = 0;
			ChangeLevelSelection (currentLevel);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			currentLevel--;
			if (currentLevel < 0) currentLevel = levels.Length - 1;
			ChangeLevelSelection (currentLevel);
		}
	}
	void SwitchDisplayFromLevel () {
		switch (currentLevel) {
			case 0:
				currentLevelScore.text = "best score : " + profile.GetAngelScore () + " pts";
				currentLevelName.text = "Angel Island";
				break;
			case 1:
				currentLevelScore.text = "best score : " + profile.GetOilScore () + " pts";
				currentLevelName.text = "Oil Ocean";
				break;
			case 2:
				currentLevelScore.text = "best score : " + profile.GetFlyingScore () + " pts";
				currentLevelName.text = "FlyingBattery";
				break;
			case 3:
				currentLevelScore.text = "best score : " + profile.GetChemicalScore () + " pts";
				currentLevelName.text = "Chemical Plant";
				break;
			default:
				break;
		}
	}
	void ChangeLevelSelection (int level) {
		for (int i = 0; i < levels.Length; i++) {
			Color c = levels[i].GetComponent<Image> ().color;
			if (i != level) c.a = 0;
			else c.a = 1;
			levels[i].color = c;
		}

	}
}