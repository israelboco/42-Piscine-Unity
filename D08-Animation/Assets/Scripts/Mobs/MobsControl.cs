using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobsControl : MonoBehaviour {

	[SerializeField] private Transform player;
	[SerializeField] private float speed, range, rangeAttack, life, damage;

	[SerializeField] private NavMeshAgent agent;

	[SerializeField] private Animator animator;
	[SerializeField] private bool isAttack, isRun;
	[SerializeField] private float cooldown = 2f;
	[SerializeField] private float cooldownGlobal;

	private bool Inrange { get { return Vector3.Distance (transform.position, player.position) <= range; } }
	private bool InrangeAttack { get { return Vector3.Distance (transform.position, player.position) <= rangeAttack; } }

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		Chase ();
		Attack ();
	}

	private void Chase () {

		if (Inrange && !isAttack) {
			// Debug.Log ("À porte");
			agent.SetDestination (player.position);
			isRun = true;
		} else {
			agent.SetDestination (transform.position);
			isRun = false;
		}
		animator.SetBool ("Run", isRun);
	}



	private void Attack () {
		if (InrangeAttack) {
			if (cooldownGlobal <= Time.time){
				cooldownGlobal = Time.time + cooldown;
				player.GetComponent<FightPlayer>().GetHit(damage);
			}
			transform.LookAt (player);
			isAttack = true;
		} else {
			isAttack = false;
		}
		animator.SetBool ("Attack", isAttack);

		if (!Inrange && !isAttack) {
			animator.SetBool ("Attack", false);
		}
	}

	private void OnDrawGizmos () {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, range);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, rangeAttack);
	}

	public void GetHit (float damage) {
		life -= damage;
		if(life <= 0)
		{ 
			life = 0;
		}
		// Debug.Log(life);
	}

	private void OnMouseOver () {
		// Debug.Log (gameObject);
		if(Input.GetMouseButton(1)){
			player.GetComponent<FightPlayer>().Target = gameObject;
		}
	}

	// private void OnTriggerEnter (Collider other) {
	// 	if (other.tag == "Player") {
	// 		agent.SetDestination (player.position);
	// 	}
	// }

	// private void OnTriggerExit(Collider other) {
	// 	if (other.tag == "Player") {
	// 		agent.isStopped = true;//  (player.position);
	// 	}
	// }
}