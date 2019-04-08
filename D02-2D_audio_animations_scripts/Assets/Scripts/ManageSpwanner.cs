using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSpwanner : MonoBehaviour {

	public GameObject type_of_unit;
	public AttackEntity castle;
	public float spawn_time = 10.0f;

	private float timer;
	private float spawn_interval;
	
	// Use this for initialization
	void Start () {
		spawn_interval = spawn_time;
		timer = spawn_time;
	}
	
	// Update is called once per frame
	void Update () {
		if (castle != null) {
			timer += Time.deltaTime;
			if (timer >= spawn_interval) {
				timer = 0.0f;
				GameObject.Instantiate (type_of_unit, transform.position, transform.rotation);
			}
		}
	}

	public void buildingDestroyed() {
		spawn_interval += 2.5f;
	}
}
