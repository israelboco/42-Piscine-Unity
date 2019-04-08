using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float speed = 8f;
	public LayerMask ObstacleMask;
	// [HideInInspector]
	public bool weaponEquip = false;
	[HideInInspector]
	public weaponScript weapon;
    public Transform weaponLocation;
	[SerializeField] private Rigidbody2D rb;
	// public GameObject BloodEffect;
	public GameObject legs;
	// public List<AudioClip> DeathSound;
	// public AudioClip takeweaponSound;
	[HideInInspector] public bool Alive = true;


	private void Awake() {
		rb = GetComponent<Rigidbody2D> ();
	}
	private void Start () {
	}
	// Update is called once per frame
	void Update () {
		if (!Alive) {
			return;
		}
		GetRotationFromCamera ();
		Checkweapon ();
		GetKeyMovement ();
        if (weaponEquip)
        {
            weapon.transform.position = weaponLocation.position;
            weapon.transform.rotation = transform.rotation;
        }
	}

	private void FixedUpdate()
	{
        if (Input.GetMouseButtonDown(1)) {
            if (weaponEquip) {
                DropWeapon();
            }
        }
	}

	void GetRotationFromCamera () {
		Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		diff.Normalize ();
		float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rot_z + 90);
	}

	void GetKeyMovement () {
		Vector2 move = Vector2.zero;
		if (Input.GetKey (KeyCode.W)) move += Vector2.up;
		if (Input.GetKey (KeyCode.S)) move += Vector2.down;
		if (Input.GetKey (KeyCode.A)) move += Vector2.left;
		if (Input.GetKey (KeyCode.D)) move += Vector2.right;
		if (move == Vector2.zero)
			legs.GetComponent<Animator> ().SetBool ("isMoving", false);
		else 
			legs.GetComponent<Animator> ().SetBool ("isMoving", true);
		ChangePositionDirection (move);
	}
	void ChangePositionDirection (Vector2 direction) {
		// float step = speed * Time.deltaTime;
		// rb.velocity = new Vector3 (direction * speed, rb.velocity.y);
		rb.velocity = direction * speed;
	}

	void Checkweapon () {
		if (Input.GetMouseButton (0)) {
			if (weaponEquip) {
                weapon.Fire (true);
			}
		}
	}


	// Colision Enter
	void OnCollisionEnter2D (Collision2D Collision) {
        if (Collision.gameObject.tag == "Bullet" && Collision.gameObject.GetComponent<bulletScript>().owner != tag) {
			Debug.Log ("Player Hit");
			Death ();
        }
	}

	void OnTriggerStay2D (Collider2D Collision) {
        if (Collision.gameObject.tag == "Exit")
        {
            GameManager.gm.ReachTheCar();
        }
        if (Collision.gameObject.tag == "Door")
            speed = 2;
        else if (Collision.gameObject.tag == "Weapon")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!weaponEquip && Collision.gameObject.GetComponent<weaponScript>().owner == null)
                {
                    Takeweapon(Collision.GetComponent<weaponScript>());
                }
            }
        }

	}

	void OnTriggerExit2D (Collider2D Collision) {
		if (Collision.gameObject.tag == "Door")
			speed = 7;
	}

    void Takeweapon (weaponScript current) {
        weapon = current;
		weaponEquip = true;
        weapon.Take(tag);
	}

	void DropWeapon () {
		if (weaponEquip) {
			Debug.Log ("Drop weapon");
			weapon.Drop ();
			weaponEquip = false;
		}
	}

	void Death () {
		Alive = false;
        ManagerSounds.instance.Play("dead");
        if (weaponEquip)
        {
            Debug.Log("Drop weapon");
            weapon.DropDead();
            weaponEquip = false;
        }
		Destroy (gameObject, 1.5f);
		GameManager.gm.PlayerKilled();
	}
}