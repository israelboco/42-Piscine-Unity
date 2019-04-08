using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	public playerScript_ex01 player;
	// Use this for initialization
	public bool checkTrigger = false;

	
	void Start () {
		// player = GetComponent<playerScript_ex01>();
	}

	// Update is called once per frame
	// public Transform2D other;
    public float closeDistance = 5.0f;

    void Update()
    {
        // if (other)
        // {
        //     Vector3 offset = other.position - transform.position;
        //     float sqrLen = offset.sqrMagnitude;

        //     // square the distance we compare with
        //     if (sqrLen < closeDistance * closeDistance)
        //     {
        //         print("The other transform is close to me!");
        //     }
        // }
    }

	private void OnTriggerStay2D(Collider2D other) {
		if (other.tag == player.tag && !checkTrigger) {
			bool compare = other.bounds.Contains(player.transform.position);
			if(compare){
				checkTrigger = true;
				// Debug.Log("Yes");
				player.checkedBoxAligned = true;
			}
        }
	}

	private void OnTriggerExit2D (Collider2D other) {
		if (other.tag == player.tag) {
			// Debug.Log("exit");
			checkTrigger = false;
			player.checkedBoxAligned = false;
		}
	}
}