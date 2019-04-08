using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	// Personnal Information
	public float viewRadius;
	public float viewAngle;
	public float speed = 8f;

	// Layers 
	public LayerMask targetMask;
	public LayerMask obstacleMask;

	// Compostant
	public GameObject alertItem;
	public GameObject stun;
	public GameObject Head;
	public GameObject Body;
	public GameObject Legs;

	public List<Sprite> HeadSprite;
	public List<Sprite> BodySprite;
	public List<weaponScript> weapons;

	// Move Option WayPoint
	private int PaternStep = 0;
	public List<GameObject> Patern;

	// Player
	[SerializeField] private GameObject targetPlayer;

	public List<Transform> visibleTargets = new List<Transform> ();
	public Transform visibleTarget;

	public GameObject[] Doors;
	public GameObject targetDoor;

	public bool isAlive = true;

	private Rigidbody2D rb;

	public weaponScript currentWeapon;
	public Transform weaponLocation;
	public bool weaponEquip = false;

	/* 0     ME CASSE LES .....   0 */

	void Start () {

        //alertItem = transform.Find("alert").GetComponent<GameObject>();
        //stun = transform.Find("stun").GetComponent<GameObject>();
        //Head = transform.Find("head").GetComponent<GameObject>();
        //Body = transform.Find("body").GetComponent<GameObject>();
        //Legs = transform.Find("legs").GetComponent<GameObject>();
		// Coroutine
		StartCoroutine ("FindTarget", .2f);
		//Ribybody
		rb = GetComponent<Rigidbody2D> ();
		// instance weapon
		GameObject.Instantiate (weapons[Random.Range (0, weapons.Count)], transform.position, Quaternion.Euler (Vector3.zero));
		// change component random sprite ennemi
		Head.GetComponent<SpriteRenderer> ().sprite = HeadSprite[Random.Range (0, HeadSprite.Count)];
		Body.GetComponent<SpriteRenderer> ().sprite = BodySprite[Random.Range (0, BodySprite.Count)];
	}

	void Update () {
		if (weaponEquip) {
			currentWeapon.transform.position = weaponLocation.position;
			currentWeapon.transform.rotation = transform.rotation;
		}

		if (visibleTarget != null && isAlive) {
			RefreshTarget (visibleTarget.GetComponent<GameObject> ());
			if (visibleTarget.tag == "Player") {
				Fire ();
			}
		} else {
			Vector3 move = PathFinding ();
			if (move == Vector3.zero) {
				if (targetPlayer != null) LookOnPlayer (targetPlayer.transform.position);
			} else {
				LookOnPlayer (move);
				ChangePosition (move);
			}
		}

	}

	IEnumerator FindTarget (float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindPlayerOnList ();
			FindTargetCollder ();
		}
	}

	// Couroutine

	Transform FindInFront () {
		Collider2D targetInViewRadius = Physics2D.OverlapCircle (transform.position, viewRadius, targetMask);
		if (targetInViewRadius != null) {
			Transform target = targetInViewRadius.transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (-transform.up, dirToTarget) < viewAngle / 2) {
				float distanceToPlayer = Vector3.Distance (transform.position, target.position);
				if (!Physics2D.Raycast (transform.position, dirToTarget, distanceToPlayer, obstacleMask)) {
					Debug.Log ("With ColliderFind");
					return target;
				}
			}
		}
		return null;
	}

	Transform FindInBack () {
		Collider2D targetInViewRadius = Physics2D.OverlapCircle (transform.position, viewRadius / 10, targetMask);
		if (targetInViewRadius != null) {
			Transform target = targetInViewRadius.transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			float distanceToPlayer = Vector3.Distance (transform.position, target.position);
			Debug.Log ("With ColliderFindReduce");
			if (!Physics2D.Raycast (transform.position, dirToTarget, distanceToPlayer, obstacleMask)) {
				return target;
			} else {
				return CrossTheDoor (target);
			}
		}
		return null;
	}

	Transform CrossTheDoor (Transform target) {
		Transform currentDoor = null;
		float enemyDoor = 0;
		float playerDoor = 0;
		float check = 100;
		foreach (GameObject door in Doors) {
			enemyDoor = Vector3.Distance (transform.position, door.transform.position);
			playerDoor = Vector3.Distance (target.position, door.transform.position);
			if (check > ((enemyDoor * enemyDoor + playerDoor * playerDoor) / 2)) {
				check = (enemyDoor * enemyDoor + playerDoor * playerDoor) / 2;
				currentDoor = door.transform;
			}
		}
		if (currentDoor != null) {
			RefreshTargetDoor (currentDoor.GetComponent<GameObject> ());
		}
		return target;
	}
	void FindTargetCollder () {
		visibleTarget = null;
		visibleTarget = FindInFront ();
		if (visibleTarget == null) {
			visibleTarget = FindInBack ();
		}
		if (visibleTarget == null) {
			visibleTarget = FindBySounds ();
		}
	}
	void FindPlayerOnList () {
		visibleTargets.Clear ();
		Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll (transform.position, viewRadius, targetMask);
		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.TransformDirection (new Vector3 (0, -1, 0)), dirToTarget) < viewAngle / 2) {
				float distanceToPlayer = Vector3.Distance (transform.position, target.position);
				if (!Physics2D.Raycast (transform.position, dirToTarget, distanceToPlayer, obstacleMask)) {
					visibleTargets.Add (target);
					Vector3 diff = visibleTargets[0].position - transform.position;
					float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Euler (0.5f, 0f, rot_z + 90);
				}
			}
		}
	}

	void RefreshTarget (GameObject target) {
		alertItem.SetActive (true);
		targetPlayer = target;
		StopCoroutine ("StopFollow");
		StartCoroutine ("StopFollow");
	}

	void RefreshTargetDoor (GameObject target) {
		targetDoor = target;
	}

	Vector3 PathFinding () {
		if (targetPlayer != null && isAlive) {
			bool canSeeTarget = CanSeeTarget ();
			Debug.Log ("See Target");
			if (canSeeTarget) {
				Debug.Log ("See Target");
			} else if (targetDoor != null) {
				Debug.Log ("See Door");
				if (Vector3.Distance (transform.position, targetDoor.transform.position) < 1 && CanSeePositionTarget (targetDoor.transform.position)) {
					Debug.Log ("I search the door");
					return targetPlayer.transform.position;
				} else {
					Debug.Log ("I see the door");
					return targetDoor.transform.position;
				}
			}
		}
		return MovePath ();
	}

	Vector3 MovePath () {
		if (Patern.Count > 0) {
			if (Vector3.Distance (transform.position, Patern[PaternStep].transform.position) < 0.3) {
				PaternStep++;
			}
			if (PaternStep == Patern.Count) {
				PaternStep = 0;
			}
			return Patern[PaternStep].transform.position;
		}
		return Vector3.zero;
	}
	Transform FindBySounds () {

		return null;
	}

	void LookOnPlayer (Vector3 destination) {
		Vector3 diff = destination - transform.position;
		float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rot_z + 90);
	}

	bool CanSeeTarget () {
		Vector3 dirToTarget = (targetPlayer.transform.position - transform.position).normalized;
		float distanceToPlayer = Vector3.Distance (transform.position, targetPlayer.transform.position);
		return !Physics2D.Raycast (transform.position, dirToTarget, distanceToPlayer, obstacleMask);
	}

	bool CanSeePositionTarget (Vector3 position) {
		Vector3 dirToTarget = (position - transform.position).normalized;
		float distanceToPlayer = Vector3.Distance (transform.position, position);
		return Physics2D.Raycast (transform.position, dirToTarget, distanceToPlayer, obstacleMask);
	}

	void ChangePosition (Vector3 direction) {
		Debug.Log ("Move On Player");
		float step = speed * Time.deltaTime;
		rb.velocity = direction * speed;
	}

	/* ENter Colission */
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Bullet" && col.gameObject.GetComponent<bulletScript> ().owner != tag) {
			Debug.Log ("Player Hit");
			Death ();
		}
		if (col.gameObject.tag == "Weapon" && col.gameObject.GetComponent<weaponScript> ().owner != tag) {
			weaponScript takeWeaponHead = col.gameObject.GetComponent<weaponScript> ();
			if (!takeWeaponHead.melee) {
				StartCoroutine ("Stun");
			} else {
				Death ();
			}
		}
	}

	IEnumerator StopFollow () {
		yield return new WaitForSeconds (3f);
		alertItem.SetActive (false);
		targetPlayer = null;
		targetDoor = null;
	}

	IEnumerator StunAnimation () {
		if (isAlive) {
			isAlive = false;
			stun.SetActive (true);
			yield return new WaitForSeconds (1f);
			isAlive = true;
		}
		stun.SetActive (false);
	}

	void OnTriggerStay2D (Collider2D Collision) {
		if (Collision.gameObject.tag == "Weapon") {
			if (!weaponEquip) {
				Takeweapon (Collision.GetComponent<weaponScript> ());
			}
		}
	}

	void Takeweapon (weaponScript current) {
		currentWeapon = current;
		weaponEquip = true;
		currentWeapon.Take (tag);
        currentWeapon.totalBullets = 9999999;
	}

	/* CHECK */
	void Fire () {
		Debug.Log ("Fire On Player");
		if (isAlive) {
			currentWeapon.Fire (false);
		}
	}

	void Death () {
        ManagerSounds.instance.Play("dead");
        currentWeapon.DropDead();
		isAlive = false;
		alertItem.SetActive (false);
		Destroy (this);
	}

	void OnDestroy () {
		Destroy (this);
	}
}