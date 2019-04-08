using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetProfile : MonoBehaviour {

	public void deletePlayerPrefs() {
		Debug.Log ("delete player prefs");
		PlayerPrefs.DeleteAll ();
	}
}
