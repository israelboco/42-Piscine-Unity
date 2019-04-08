using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerHumans : MonoBehaviour {

	public static ManagerHumans instance { get; private set; }

	[SerializeField]
	private List<HumanController> human_all;

	[SerializeField]
	private List<HumanController> human_selected;

	private Vector3 mousePos;
	private Camera cam;
	private bool click_humain;
	void Awake () {
		instance = this;
		human_all = new List<HumanController> ();
		human_selected = new List<HumanController> ();
	}

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
	}

	// Update is called once per frame
	void Update () {
		mousePos = this.cam.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) {
			SelectedHumain ();

		} else if (Input.GetMouseButtonDown (1)) {
			human_selected.Clear ();
		}
	}

	void SelectedHumain () {
		click_humain = false;
		human_all.RemoveAll (isDead);
		foreach (HumanController human in human_all) {
			if (human.GetComponent<Collider2D> () == Physics2D.OverlapPoint (mousePos)) {
				if (!Input.GetKey (KeyCode.LeftControl) && !Input.GetKey (KeyCode.RightControl))
					human_selected.Clear ();
				human_selected.Add (human);
				human.asset.soundsAction ("Selected");
				click_humain = true;
				break;
			}
		}
		if (!click_humain) {
			human_selected.RemoveAll (isDead);
			foreach (HumanController human in human_selected) {
				human.newTarget (mousePos);
			}
		}
	}

	public void Add (HumanController human) {
		human_all.Add (human);
	}

	public bool isDead (HumanController human) {
		return (human == null);
	}
}