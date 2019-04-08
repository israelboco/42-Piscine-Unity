using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class dragTowersScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[SerializeField]
	private GameObject dragged;
	public towerScript tower;
	public GameObject objmanager;
	public bool isDraggable = true;

	private gameManager manager;

	// Use this for initialization
	void Start () {
		manager = objmanager.GetComponent<commandButtonsScript> ().manager;
	}

	// Update is called once per frame
	void Update () {
		if (manager.playerEnergy - tower.energy < 0) {
			isDraggable = false;
			GetComponent<Image> ().color = Color.gray;
		} else {
			isDraggable = true;
			GetComponent<Image> ().color = Color.white;
		}
	}

	public void OnBeginDrag (PointerEventData eventData) {
		if (isDraggable) dragged = Instantiate (gameObject, transform);
	}

	public void OnDrag (PointerEventData eventData) {
		if (isDraggable)
			dragged.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10f));
			// dragged.layer = 0;
	}

	public void OnEndDrag (PointerEventData eventData) {
		if (isDraggable) {
			if (dragged != null) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit && hit.collider.transform.tag == "empty") {
					manager.playerEnergy -= tower.energy;
					Instantiate (tower, hit.collider.gameObject.transform.position, Quaternion.identity);
				}
				Destroy (dragged);
				dragged = null;
			}
		}
	}
}