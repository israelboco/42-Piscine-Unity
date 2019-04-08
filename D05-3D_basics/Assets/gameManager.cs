using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// SceneManager.am
		//  RenderSettings.ambientLight = Color.white;
		//  LightmapSettings.ambientLight;
		// Setting //. = Color.white;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.R)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		};
	}
}
