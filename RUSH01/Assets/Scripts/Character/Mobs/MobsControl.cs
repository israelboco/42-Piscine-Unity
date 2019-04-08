using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobsControl : MonoBehaviour {

	[SerializeField] private Transform player;
	[SerializeField] private Stat player_stat;

	[SerializeField] private Stat mob_stat;

	//[SerializeField] private float speed, range, rangeAttack, life, damage;
	[SerializeField] private float range, rangeAttack;
	[SerializeField] private NavMeshAgent agent;

	[SerializeField] private Animator animator;
	[SerializeField] private bool isAttack = false;
	[SerializeField] private bool isRun = false;
	[SerializeField] private bool isDead = false;

	public List<GameObject> weaponsList;

	public GameObject PotionLife;

	public bool IsDead { get { return isDead; } }

	[SerializeField] private float cooldown = 2f;
	[SerializeField] private float cooldownGlobal;

	private bool Inrange { get { return Vector3.Distance (transform.position, player.position) <= range; } }
	private bool InrangeAttack { get { return Vector3.Distance (transform.position, player.position) <= rangeAttack; } }

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		mob_stat = GetComponent<Stat> ();
		player = GameObject.FindWithTag ("Player").transform;
		player_stat = player.GetComponent<Stat> ();
	}

	// Update is called once per frame
	void Update () {
		if (!isDead) {
			Chase ();
			Attack ();
		}
	}

	private void Chase () {

		if (Inrange && !isAttack) {
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
			if (cooldownGlobal <= Time.time) {
				cooldownGlobal = Time.time + cooldown;
				if (Random.Range (0, 100) <= 75 + mob_stat.AGI - player_stat.AGI) {
					float baseDamage = Random.Range (mob_stat.minDamage, mob_stat.maxDamage);
					player.GetComponent<FightPlayer> ().GetHit (baseDamage * (1 - player_stat.GetComponent<Stat> ().ARMOR / 200));
				}
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
		// Gizmos.color = Color.blue;
		// Gizmos.DrawWireSphere (transform.position, range);
		// Gizmos.color = Color.red;
		// Gizmos.DrawWireSphere (transform.position, rangeAttack);
	}

	public void GetHit (float damage) {
		mob_stat.HP -= damage;
		if (mob_stat.HP <= 0 && !isDead) {
			agent.isStopped = true;
			isDead = true;
			// animator.SetBool ("Attack", false);
			// animator.SetBool ("Run", false);
			// animator.SetBool ("Idle", false);
			AudioManager.instance.Play ("deadzombie");

			player.GetComponent<FightPlayer> ().addXp (mob_stat.XP);

			animator.SetBool ("Dead", true);
			// Debug.Log (mob_stat.HP);
		}
	}

	private void OnMouseOver () {
		// Debug.Log (gameObject);
		if (Input.GetMouseButton (1)) {
			player.GetComponent<FightPlayer> ().Target = gameObject;
		}
	}

	public IEnumerator DeadAnimation () {
		// Vector3 destination = new Vector3(trans)
		if (Random.Range (0, 10) > 5)
			GameObject.Instantiate (PotionLife, new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
		if (Random.Range (0, 10) > 3) {

			GameObject weapon = weaponsList[(Random.Range (0, weaponsList.Count))];
			GameObject dropWap = GameObject.Instantiate (weapon, new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z), weapon.transform.rotation);
			dropWap.GetComponent<RectTransform> ().transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
			dropWap.GetComponent<WeaponsItems> ().DMG = Random.Range (3 * player.GetComponent<Stat> ().LV - 2, 3 * player.GetComponent<Stat> ().LV + 2);
			dropWap.name = dropWap.GetInstanceID ().ToString ();
		}
		agent.enabled = false;
		while (transform.position.y > -10) {
			transform.Translate (Vector3.down * Time.deltaTime * 1.1f);
			yield return null;
		}
		Destroy (gameObject, 5f);
	}

	private void OnTriggerEnter (Collider other) {
		if (other.tag == "Spells") {
			GetHit (other.gameObject.GetComponent<SpellsDommage> ().damage);
		}
	}

	// private void OnTriggerExit(Collider other) {
	// 	if (other.tag == "Player") {
	// 		agent.isStopped = true;//  (player.position);
	// 	}
	// }
}