using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class my_hack_slash_cam : MonoBehaviour {

	// Use this for initialization

	public GameObject player;

	public Vector3 offcet;
	void Start () {
		transform.position = player.transform.position + offcet;
		transform.LookAt(player.transform);
	}
	
	// Update is called once per frame
	void Update ()
	{

		transform.position = player.transform.position + offcet;
	}
}
