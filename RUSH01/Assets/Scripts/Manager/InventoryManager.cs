using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public Stat playerStat;
	public Inventory inventoryPlayer;
	private GameObject[] slots;
	private GameObject[] current_slots;
	private int current_index = 0;
	private Image[] uiSprites;
	public Sprite test;
	public GameObject prefab_sprite;
	private List<GameObject> stuff;

	private bool stop = false;
	private int slot_busy = 0;
	private int equiped_slot = 0;
	private float cad = 0.0f;

	public Image image_weapon;
	public Text nameWeapon;

	public GameObject Tooltip;
	public GameObject main_Weapon_Equiped;
	public Text stat_1;
	public Text stat_2;
	public Text rare;

	public GameObject[] weapons;
	private bool open_inventory = false;
	public RectTransform inventory;
	public float transition;

	// Use this for initialization
	void Start () {
		slots = GameObject.FindGameObjectsWithTag ("Slot");
		// playerStat = GetComponent<Stat>();
	}

	// Update is called once per frame
	void Update () {

		// check_open_inventory();
		check_mouse_tooltip ();
		MainWeapon ();

		CheckDeleteWithDrag ();
		if (slot_busy < inventoryPlayer.inventorySlot.Count) {
			AddInventory ();
		}
	}

	void AddInventory () {
		int i = 0;
		foreach (GameObject weapon in inventoryPlayer.inventorySlot) {
			if (i == slot_busy) {
				if (!LookSlotIsempty (weapon))
					Debug.Log ("inventory is full");
			}
			i++;
		}
	}

	bool LookSlotIsempty (GameObject weapon) {
		for (int y = 0; y < slots.Length; y++) {
			if (slots[y].transform.childCount == 0) {
				prefab_sprite.GetComponent<Image> ().sprite = weapon.GetComponent<Image> ().sprite;
				GameObject ret = Instantiate (prefab_sprite, slots[y].transform, false);
				ret.name = weapon.GetInstanceID ().ToString ();
				// Debug.Log (ret.name);
				// current_slots[current_index] = ret;
				// current_index
				slot_busy += 1;
				return true;
			}
		}
		return false;
	}

	void CheckDeleteWithDrag () {
		for (int i = 0; i < slots.Length; i++) {
			if (slots[i].GetComponent<DragAndDropCell> ().delete_one == true) {
				if (delete_this (slots[i].GetComponent<DragAndDropCell> ().deleted_object.name) == false) {
					slots[i].GetComponent<DragAndDropCell> ().delete_one = false;
				}
				slot_busy -= 1;
			}
		}
	}

	bool delete_this (string nametodelete) {

		foreach (GameObject weapon in inventoryPlayer.inventorySlot) {
			if (weapon.GetInstanceID ().ToString () == nametodelete) {
				Debug.Log ("remove ok");
				inventoryPlayer.inventorySlot.Remove (weapon);
				return true;
			}
		}
		return false;
	}

	void MainWeapon () {
		if (main_Weapon_Equiped.transform.childCount > 0) {
			// Debug.Log(main_Weapon_Equiped.transform.GetChild(0).name);
			foreach (GameObject weapon in inventoryPlayer.inventorySlot) {
				if (weapon.name == main_Weapon_Equiped.transform.GetChild (0).name) {
					// print(playerStat);
					print(weapon);
					playerStat.minDamage = playerStat.minDamage_base + weapon.GetComponent<WeaponsItems>().DMG;
					playerStat.maxDamage = playerStat.maxDamage_base + weapon.GetComponent<WeaponsItems>().DMG;
				}
			}
		} else {
			playerStat.minDamage = playerStat.minDamage_base;
			playerStat.maxDamage = playerStat.maxDamage_base;
		}
	}

	// void check_open_inventory () {
	// 	if (Input.GetKeyDown (KeyCode.B)) {
	// 		transition = 1;
	// 		if (open_inventory == false) {
	// 			open_inventory = true;
	// 		}
	// 		else {
	// 			open_inventory = false;
	// 		}
	// 	}
	// 	if (open_inventory == true)
	// 		inventory.gameObject.SetActive (true);
	// 	else
	// 		inventory.gameObject.SetActive (false);
	// }

	void check_mouse_tooltip () {
		// bool no_std_ray = false;

		// if (open_inventory) {
			PointerEventData pointer = new PointerEventData (EventSystem.current);
			pointer.position = Input.mousePosition;
			List<RaycastResult> rayresult = new List<RaycastResult> ();
			EventSystem.current.RaycastAll (pointer, rayresult);
			if (rayresult.Count > 0) {

				// no_std_ray = true;
				// Debug.Log(rayresult[0].gameObject.tag);
				if (rayresult[0].gameObject.tag == "WeaponImage" || rayresult[0].gameObject.tag == "weapon") {
					match_weapon (rayresult[0].gameObject.name);
					// Debug.Log("IMage Weapon");
					Tooltip.SetActive (true);
					// no_std_ray = true;
				} else {

					Tooltip.SetActive (false);
				}
				// if () {
				// 	match_weapon (rayresult[0].gameObject.name);
				// 	// for (int i = 0; i < slots.Length; i++) {
				// 	// 	if (slots[i].GetComponent<DragAndDropCell> ().deleted_object.name == ) {
				// 	// 		// if (delete_this (slots[i].GetComponent<DragAndDropCell> ().deleted_object.name) == false) {
				// 	// 		// 	slots[i].GetComponent<DragAndDropCell> ().delete_one = false;
				// 	// 		// }
				// 	// 		slot_busy -= 1;
				// 	// 	}
				// 	// }

				// }

			}
		// }
		// }
		// if (no_std_ray == false) {
		// 	RaycastHit hit;
		// 	Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		// 	if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
		// 			Debug.Log("Case Weapon");
		// 		if (hit.collider.gameObject.tag == "weapon") {
		// 			Debug.Log("Case Weapon");
		// 			// nameWeapon.text = hit.collider.gameObject.name;
		// 			// stat_1.text = hit.collider.gameObject.GetComponent<WeaponsItems>().DMG.ToString();
		// 			// hat_is_color(hit.collider.gameObject.GetComponent<WeaponsItems>().quality);
		// 			// stat_2.text = hit.collider.gameObject.GetComponent<WeaponsItems>().ATTACKSPEED.ToString();
		// 			// image_weapon.sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
		// 			// Tooltip.SetActive(true);
		// 		} else {
		// 			// Tooltip.SetActive(false);
		// 		}
		// 	}
		// }
		// }
	}
	void match_weapon (string nameID) {

		foreach (GameObject weapon in inventoryPlayer.inventorySlot) {
			if (weapon.GetInstanceID ().ToString () == nameID) {
				// Debug.Log(nameID);
				nameWeapon.text = weapon.name;
				stat_1.text = weapon.GetComponent<WeaponsItems> ().DMG.ToString ();
				WhatRareety (weapon.GetComponent<WeaponsItems> ().quality);
				stat_2.text = weapon.GetComponent<WeaponsItems> ().ATTACKSPEED.ToString ();
				image_weapon.sprite = weapon.GetComponent<Image> ().sprite;
				// Debug.Log(weapon);
				// weapon.sprite = weapon.GetComponent<Image>().sprite;
			}
		}

	}

	void WhatRareety (int quality) {
		if (quality == 1) {
			nameWeapon.color = Color.red;
			rare.text = "Legendary";
			rare.color = Color.red;
		} else if (quality == 2) {
			nameWeapon.color = Color.magenta;
			rare.text = "Epic";
			rare.color = Color.magenta;
		} else if (quality == 3) {
			nameWeapon.color = Color.blue;
			rare.text = "Rare";
			rare.color = Color.blue;
		} else if (quality == 4) {
			nameWeapon.color = Color.green;
			rare.text = "Superior";
			rare.color = Color.green;
		} else {
			nameWeapon.color = Color.white;
			rare.text = "Commun";
			rare.color = Color.white;
		}
	}
}