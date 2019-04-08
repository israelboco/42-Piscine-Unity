using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour {

	/* Mouvement And Speed */
	public bool isMvt;
	public float speed;

	/* Jumg Varialbe */
	public float jumpForce = 300.0f;
	public bool doubleJump = false;
	public Transform groundCheck;
	public float groundRadius = 0.5f;
	public bool grounded = false;
	public LayerMask whatIsGround;
	private Rigidbody2D rb;
	/* Use this for initialization */
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		isMvt = false;
	}

	/* Update is called once per frame */
	void Update () {
		if (!isMvt) {
			DisableRagdoll();
		} else {
			EnableRagdoll();
			CheckKeyPlayerMoove ();
			CheckKeyPlayerJump ();
		}
	}

	void EnableRagdoll () {
		// rb.isKinematic = false;
		rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
		// rb.detectCollisions = true;
	}

	// Let animation control the rigidbody and ignore collisions.
	void DisableRagdoll () {
		rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		// rb.detectCollisions = false;
	}
	public void setRigiBodyTypeAndMove (bool mvt) {
		isMvt = mvt;

		// rb = GetComponent<Rigidbody2D>();
		// rb.isKinematic = kine;
	}

	/* Method Inside */
	void CheckKeyPlayerMoove () {
		float move = Input.GetAxis ("Horizontal");
		if (move < 0) {
			rb.velocity = new Vector3 (move * speed, rb.velocity.y);
		}
		if (move > 0) {
			rb.velocity = new Vector3 (move * speed, rb.velocity.y);
		}
		if (move == 0) {
			rb.velocity = new Vector3 (move * speed, rb.velocity.y);
		}
	}

	void CheckKeyPlayerJump () {
		bool jump = Input.GetKeyDown ("space");

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		if (grounded) doubleJump = false;
		if (jump && (grounded || !doubleJump) && rb.velocity.y == 0 ) {
			rb.AddForce (Vector3.up * jumpForce);
			if (!doubleJump && !grounded) doubleJump = true;
		}
	}

}