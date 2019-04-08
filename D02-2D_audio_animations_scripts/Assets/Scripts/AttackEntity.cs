using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEntity : MonoBehaviour {

	// Use this for initialization
	public string nameOfUnity = "ChooseYourName";
	public int alliance = 0;
	public int hitPoints = 100;
	public int attack = 0;
	public float atckI = 1.0f;
	public bool Hcastle = false;
	public bool OCastle = false;
	private int hP;
	private bool gameOver;
	private float atckT;
	private bool dead = false;

	[SerializeField]
	private Animator animator;
	public ManagerSounds sounds;

	// Use this for initialization
	void Start () {
		hP = hitPoints;
		gameOver = false;
		atckT = 0.0f;
	}

	void Update () {
		atckT += Time.deltaTime;
		if (Hcastle && !gameOver && hitPoints < 1) OrcWins ();
		if (OCastle && !gameOver && hitPoints < 1) HumanWins ();
		if ((Hcastle || OCastle) && gameOver) return;
		if (this.hitPoints < 1 && this.dead == false) DetectDeadRaison ();
	}

	void OrcWins () {
		gameOver = true;
		Debug.Log ("The Orc Team wins.");
		CircleCollider2D.Destroy (GetComponent<CircleCollider2D> ());
		SpriteRenderer.Destroy (GetComponent<SpriteRenderer> ());
		Invoke ("Restart", 5);
	}

	void HumanWins () {
		gameOver = true;
		Debug.Log ("The Human Team wins.");
		CircleCollider2D.Destroy (GetComponent<CircleCollider2D> ());
		SpriteRenderer.Destroy (GetComponent<SpriteRenderer> ());
		Invoke ("Restart", 5);

	}
	void DetectDeadRaison () {
		dead = true;
		attack = 0;
		HumanController human = GetComponent<HumanController> ();
		if (human) human.dead = true;
		Rigidbody2D body = GetComponent<Rigidbody2D> ();
		if (body) Rigidbody2D.Destroy (body);
		if (sounds) sounds.Play ("Dead");
		if (animator) animator.SetTrigger ("Dead");
		if (sounds || animator) Invoke ("Kill", 2.5f);
		else Kill ();
	}
	void Kill () {
		GameObject.Destroy (this.gameObject);
	}

	void Restart () {
		Debug.Log ("FinDe Partie");
	}
	void OnCollisionEnter2D (Collision2D coll) {
		AttackEntity enemy = coll.gameObject.GetComponent<AttackEntity> ();
		HumanController human = GetComponent<HumanController> ();
		OrcController orc = GetComponent<OrcController> ();
		if (human && enemy && enemy.alliance != alliance) human.entitySelect = enemy;
		if (orc && enemy && enemy.alliance != alliance) orc.entityTarget = enemy;
	}

	void OnCollisionStay2D (Collision2D coll) {
		AttackEntity enemy = coll.gameObject.GetComponent<AttackEntity> ();
		if (enemy && attack > 0 && enemy.alliance != alliance && atckT >= atckI) {
			atckT = 0.0f;
			enemy.hitPoints -= attack;
			Debug.Log (enemy.nameOfUnity + " [" + enemy.hitPoints + "/" + enemy.hP + "] has been attacked.");
		}
	}
}