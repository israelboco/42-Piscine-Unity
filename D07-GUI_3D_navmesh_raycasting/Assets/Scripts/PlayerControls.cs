using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerControls : MonoBehaviour {

	// Use this for initialization
	// [Range (0, 20)]
	public float speed = 0.6f;

	public GameObject canon;
	public GameObject gunShoot;
	public GameObject missilShoot;
	private float rotY;
	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	public bool smooth;
	public float smoothTime = 5f;

	public int ammoMissile = 10;
	public bool lockCursor = true;
	[Range (10, 100)]
	public float range = 50f;

	private float boost = 100;

	[SerializeField]

	private bool fireGun = false;
	private Quaternion m_CharacterTargetRot;
	private Quaternion m_CameraTargetRot;
	private bool m_cursorIsLocked = true;
	public Rigidbody rigid;
	private Vector3 mouspos;

	// Use this for initialization
	void Start () {
		Init (canon.transform, Camera.main.transform);
		if (rigid == null) {
			rigid = this.GetComponentInChildren<Rigidbody> ();
		}
	}

	void GunFight () {

		RaycastHit hit;
		mouspos = Input.mousePosition;
		mouspos.z = 0;
		// Ray ray = Camera.main.ScreenPointToRay(mouspos);
		Vector3 fwd = canon.transform.TransformDirection (Vector3.forward);
		if (Physics.Raycast (canon.transform.position, fwd, out hit, range)) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				// Debug.DrawLine (canon.transform.position, hit.point, Color.red, 1f);
				fireGun = true;
				// canon.GetComponent<AudioSource> ().clip = gunSound;
				AudioManager.instance.Play("gun");
				Destroy (Instantiate (gunShoot, hit.point, Quaternion.identity), 0.5f);
			} else if (Input.GetKeyDown (KeyCode.Mouse1) && ammoMissile > 0) {
				// if (hit.transform.tag == "tank")
					AudioManager.instance.Play("explose");
					Destroy (Instantiate (missilShoot, hit.point, Quaternion.identity), 0.5f);
				ammoMissile -= 1;
			} else {
				fireGun = false;
			}
			// if (hit.transform != null)
		}
	}
	// Update is called once per frame
	void Update () {

		LookRotation (canon.transform, Camera.main.transform);
		GunFight ();
		InputKeyFunction ();
		BoostOption ();
	}

	private void BoostOption () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			if (boost > 0) {
				boost -= 10;
				speed = 0.8f;
			}
		} else speed = 0.4f;
		if (boost < 100) boost += 5;
	}
	private void InputKeyFunction () {
		// if (Input.GetKey (KeyCode.W))
			// transform.Translate (Vector3.forward * speed * Time.deltaTime);
		// if (Input.GetKey (KeyCode.S))
		rigid.MovePosition(rigid.transform.position + (rigid.transform.forward * speed * Input.GetAxis("Vertical")));
			// transform.Translate (Vector3.back * speed * Time.deltaTime);
		// if (Input.GetKey (KeyCode.D))
			// rigid.MoveRotation(Quaternion.Euler(rigid.transform.position.x , 10, rigid.transform.position.z));

			// transform.Translate (Vector3.right * speed * Time.deltaTime);

			// transform.Rotate (0, 10, 0);
		// if (Input.GetKey (KeyCode.A))
		rigid.MoveRotation(rigid.rotation * Quaternion.Euler(rigid.transform.up * 2 * Input.GetAxis("Horizontal")));// rigid.transform.position.z));
			// transform.Translate (Vector3.left * speed * Time.deltaTime);
			// transform.Rotate (0, -10, 0);
	}
	public void Init (Transform character, Transform camera) {
		m_CharacterTargetRot = character.localRotation;
		// m_CameraTargetRot = camera.localRotation;
	}

	public void LookRotation (Transform character, Transform camera) {

		float yRot = Input.GetAxis ("Mouse X");// * XSensitivity;

		m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);

		if (smooth) {
			character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
				smoothTime * Time.deltaTime);
		} else {
			character.localRotation = m_CharacterTargetRot;
		}

		UpdateCursorLock ();
	}

	public void SetCursorLock (bool value) {
		lockCursor = value;
		if (!lockCursor) { //we force unlock the cursor if the user disable the cursor locking helper
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	public void UpdateCursorLock () {
		// if the user set "lockCursor" we check & properly lock the cursos
		if (lockCursor)
			InternalLockUpdate ();
	}

	private void InternalLockUpdate () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			m_cursorIsLocked = false;
		} else if (Input.GetMouseButtonUp (0)) {
			m_cursorIsLocked = true;
		}

		if (m_cursorIsLocked) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else if (!m_cursorIsLocked) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	Quaternion ClampRotationAroundXAxis (Quaternion q) {

		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

		angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}