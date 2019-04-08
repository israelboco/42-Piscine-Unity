using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

	public float precision;

	public float spawnTime = 5;
	private float time;

	public GameObject cubeA;
	public GameObject cubeS;
	public GameObject cubeD;

	// Use this for initialization

	public static int cubeCnt;

	void selectedCubeSpawn (int ranged) {
		switch (ranged) {
			case 0:
				GameObject.Instantiate (cubeA);
				break;
			case 1:
				GameObject.Instantiate (cubeS);
				break;
			case 2:
				GameObject.Instantiate (cubeD);
				break;
			default:
				break;
		}
	}

	void Start () {
		cubeCnt = 0;
	}


	// Update is called once per frame
	void Update () {

		if (time >= spawnTime && cubeCnt < 3) {
			time = 0;
			cubeCnt += 1;
			this.selectedCubeSpawn (Random.Range (0, 3));
		}
		this.time += Time.deltaTime;
	}
}