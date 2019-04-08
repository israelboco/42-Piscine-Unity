using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProfile : MonoBehaviour {

	public int[] levels_unlock;
	public int nbrDead = 0;
	public int nbrRings = 0;
	public string bestScoreByLevel;

	//
	public int angel_score = 5;
	public int oil_score = 0;
	public int flying_score = 0;
	public int chemical_score = 0;

	// Use this for initialization
	void Start () { 
		// OnlyForDemo
		PlayerPrefs.SetInt("angel_score", angel_score);
	}

	// Update is called once per frame
	void Update () {
		nbrDead = PlayerPrefs.GetInt ("nbrDead");
		nbrRings = PlayerPrefs.GetInt ("nbrRings");
		angel_score = PlayerPrefs.GetInt ("angel_score");
		oil_score = PlayerPrefs.GetInt ("oil_score");
		flying_score = PlayerPrefs.GetInt ("flying_score");
		chemical_score = PlayerPrefs.GetInt ("chemical_score");
	}

	public int GetAngelScore () {
		return angel_score;
	}
	public int SetAngelScore () {
		return angel_score;
	}

	public int GetOilScore () {
		return oil_score;
	}

	public int GetFlyingScore () {
		return flying_score;
	}

	public int GetChemicalScore () {
		return chemical_score;
	}

	public int GetPlayerDeath () {
		return nbrDead;
	}

	public int GetPlayerRings () {
		return nbrRings;
	}
}