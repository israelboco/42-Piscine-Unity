using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	public GameObject zombie_type;

	public GameObject actual_zombie;
	// Use this for initialization
	private bool in_wait = false;

	IEnumerator spanw_in_delay(float t)
	{
		in_wait = true;
		yield return new WaitForSeconds(t);
		actual_zombie = Instantiate(zombie_type, transform.position,Quaternion.identity);
		in_wait = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!actual_zombie && in_wait == false)
			StartCoroutine(spanw_in_delay(20));


	}
}
