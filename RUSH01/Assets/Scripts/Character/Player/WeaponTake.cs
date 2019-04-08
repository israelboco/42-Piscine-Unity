using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTake : MonoBehaviour {

	[SerializeField] private GameObject handPickup;
	// [SerializeField] private List<GameObject> weaponToTake;
	[SerializeField] private Inventory inventoryPlayer;

	[SerializeField] private GameObject equipedWeapon;
	// Use this for initialization
	void Start () {
		inventoryPlayer = GetComponent<Inventory> ();
	}

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerEnter (Collider other) {
		if (other.transform.tag == "Weapon") {
			Debug.Log (other.transform.tag);
			inventoryPlayer.inventorySlot.Add (other.transform.gameObject);
			other.transform.gameObject.GetComponent<WeaponsItems> ().itemPosition = WeaponsItems.itemPositionEnum.INVENTORY;
			// weaponToTake.Add(other.transform.gameObject);
			// weaponToTake.transform.parent = handPickup.transform;
			// weaponToTake.transform.localPosition = handPickup.transform.localPosition;
			// weaponToTake.transform.localEulerAngles = handPickup.transform.localEulerAngles;

		}
	}

	public void AttachWeaponOnPlayer (string name) {
		// Debug.Log(name + "Attach OnWeapon");
		// if (int.Parse (name) > 0) {
			for (int i = 0; i < inventoryPlayer.inventorySlot.Count; i++) {
				if (inventoryPlayer.inventorySlot[i].GetInstanceID ().ToString () == name) {
					GameObject tmp = inventoryPlayer.inventorySlot[i];
					inventoryPlayer.inventorySlot[i] = equipedWeapon;
					equipedWeapon = tmp;
					inventoryPlayer.inventorySlot[i].GetComponent<WeaponsItems> ().itemPosition = WeaponsItems.itemPositionEnum.INVENTORY;
					equipedWeapon.GetComponent<WeaponsItems> ().itemPosition = WeaponsItems.itemPositionEnum.HAND;
					equipedWeapon.transform.parent = handPickup.transform;
					equipedWeapon.transform.localPosition = equipedWeapon.GetComponent<WeaponsItems> ().pickupPosition;
					equipedWeapon.transform.localEulerAngles = equipedWeapon.GetComponent<WeaponsItems> ().pickupRotation;
					// handPickup.GetChi
					// equipedWeapon.
					// weaponToTake.transform.parent = handPickup.transform;
				}
			}
		// }
	}
}