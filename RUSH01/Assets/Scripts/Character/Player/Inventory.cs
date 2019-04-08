using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	// Use this for initialization
	public List<GameObject> inventorySlot = new List<GameObject> () ;
	public GameObject Equiped;
	public GameObject InventoryManager;

	[SerializeField] private bool isOpen = false;

	private void Awake () { }
	private void Start () {

		if (InventoryManager == null) {
			InventoryManager = GameObject.Find ("InventoryManager");
			inventorySlot.Add(Equiped);
		}
			InventoryManager.SetActive(false);
	}
	private void Update () {
	

		if (Input.GetKeyDown(KeyCode.I) && InventoryManager != null) {
			if (isOpen) {
				InventoryManager.SetActive (false);
				isOpen = false;
			} else {
				InventoryManager.SetActive (true);
				isOpen = true;
			}
		}
	}
}