using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour {

	/* [SERIALIZED]*/
	[SerializeField] private float speed;
	[SerializeField] private Animator animator;

	[SerializeField] private NavMeshAgent agent;

	[SerializeField] private bool isRunning;
	[SerializeField] private LayerMask layer;

	// Use this for initialization
	private void Awake () {
		
	}
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			GetPosition ();
		}
		MoveToPosition ();
	}

	private void GetPosition () {

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Mettre des layerMask pour raycast Out les Montages
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, layer)) {
			agent.destination = hit.point;
		}
	}

	private void MoveToPosition () {

		if(agent.remainingDistance <= agent.stoppingDistance){
			isRunning = false;
		}
		else {
			isRunning = true;
		}
		animator.SetBool("Run", isRunning);

	}

	private void OnDrawGizmos() {
		// Gizmos.color = Color.red;
		// Gizmos.DrawWireSphere(transform.position, rangeAttack);
	}
}