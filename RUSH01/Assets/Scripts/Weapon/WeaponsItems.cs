using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsItems : MonoBehaviour {

	// Use this for initialization
	public enum itemPositionEnum { GROUND, INVENTORY, HAND }
	public itemPositionEnum itemPosition = itemPositionEnum.GROUND;
	public float DMG;
	public float ATTACKSPEED;
	public int quality;

	public Vector3 pickupPosition;
	public Vector3 pickupRotation;
	// Use this for initialization
	void Start () {
		ATTACKSPEED = 1;
		DMG = 1;
		quality = Random.Range (0, 100);
		if (quality < 2) quality = 1;
		else if (quality < 7) quality = 2;
		else if (quality < 20) quality = 3;
		else if (quality < 40) quality = 4;
		else quality = 5;
		DMG += (25 - quality * 5);
		ATTACKSPEED += (5 - quality * 1);
	}

	// Update is called once per frame



	void Update () {
		if (itemPosition == itemPositionEnum.INVENTORY) {
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<BoxCollider> ().enabled = false;
		}
		else if (itemPosition == itemPositionEnum.HAND) {
			GetComponent<MeshRenderer> ().enabled = true;
		}
		else if (itemPosition == itemPositionEnum.GROUND) {
			GetComponent<MeshRenderer> ().enabled = true;
			GetComponent<BoxCollider> ().enabled = true;
		}
	}
}
