using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayer : MonoBehaviour {

	private GameObject target;
	public GameObject Target { get { return target; } set { target = value; } }

	[SerializeField] private float life = 100f;
	[SerializeField] private float damage = 10f;
	[SerializeField] private float cooldown = 2f;
	[SerializeField] private float cooldownGlobal;
	[SerializeField] private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (target);
		Attack ();
	}

	private void Attack () {
		if (Input.GetKey (KeyCode.Space) && target != null && cooldownGlobal <= Time.time) {
			cooldownGlobal = Time.time + cooldown;
			animator.SetBool("Attack", true);
			animator.SetBool("Idle", false);
			animator.SetBool("Run", false);
			Debug.Log("ATTACK");
			// target.GetComponent<MobsControl> ().GetHit (damage);
		}
	}


	public void EndAnimation(){
		animator.SetBool("Attack", false);
	}
	public void GetHit (float damage) {
		life -= damage;
		if (life <= 0) {
			life = 0;
		}
		// Debug.Log (life);
	}
}