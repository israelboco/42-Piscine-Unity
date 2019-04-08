using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour {

	/* [SERIALIZED]*/
	[SerializeField] private float speed;
	[SerializeField] private Animator animator;

	[SerializeField] private NavMeshAgent agent;

	[SerializeField] private bool isRunning;
	[SerializeField] private bool isStay = true;
	[SerializeField] private LayerMask layer;
	[SerializeField] private FightPlayer fightPlayer;

	// Use this for initialization
	private void Awake () {

	}
	void Start () {
		fightPlayer = GetComponent<FightPlayer> ();
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {
			if(EventSystem.current.IsPointerOverGameObject())
				return;
			GetPosition ();
		}
		MoveToPosition ();
	}

	private void GetPosition () {

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, layer)) {
			fightPlayer.IsAttack = false;
			agent.destination = hit.point;
		}
	}

	private void MoveToPosition () {

		if (agent.remainingDistance >= agent.stoppingDistance) {
			if (!fightPlayer.IsAttack) {
				isRunning = true;
				isStay = false;
			} else {
				agent.destination = transform.position;
			}
		} else {
			isRunning = false;
			isStay = true;
		}
		animator.SetBool ("Run", isRunning);
		animator.SetBool ("Idle", isStay);
	}
}