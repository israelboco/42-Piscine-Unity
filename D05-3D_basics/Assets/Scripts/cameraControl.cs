using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class cameraControl : MonoBehaviour {

	// public static instance { get; private set; }

	public golfScript golf;
	public Camera cam;
	public Rigidbody rb;
	public SphereCollider coll;

	public bool startingPosition;
	public float mouseRatio = 0.5f;
	public float speed = 50f;

	private Vector3 lastMousePosition;
	private float cameraX;
	private float cameraY;

	private Quaternion shoot;
	private bool fireDirectionSet = false;

	private float shootPower = 0f;
	public Vector3 powerDirection = new Vector3 (0, 0.5f, 0.5f);

	public RectTransform shootBar;

	// Terrain

	public Terrain myTerrain;

	public float nonPassibleBorderWidth = 0;

	public Vector3 mapMinBounds;
	public Vector3 mapMaxBounds;

	void Awake () {

	}
	void Start () {
		cam = GetComponent<Camera> ();
		rb = GetComponent<Rigidbody> ();
		coll = GetComponent<SphereCollider> ();
		lastMousePosition = Input.mousePosition;
		startingPosition = true;
		CameraResetRotation ();
	}

	void RestartShootingBall () {
		if ((!startingPosition && Input.GetKeyDown (KeyCode.Space)) ||
			Input.GetKeyDown (KeyCode.Escape) ||
			Input.GetKeyDown (KeyCode.F)) {
			startingPosition = true;
			shootPower = 0f;
			CameraResetRotation ();
		} else if (startingPosition) {
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.E) || Input.GetKeyDown (KeyCode.Q))
				startingPosition = false;

		}
	}

	void CameraResetRotation () {
		cameraX = cam.transform.rotation.eulerAngles.x;
		cameraY = cam.transform.rotation.eulerAngles.y;
	}
	void MoveCameraWorld () {
		coll.radius = 5f;
		RemoveArrow (false);
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		Vector3 move = (Input.mousePosition - lastMousePosition) * mouseRatio;

		cameraX -= move.y;
		cameraY += move.x;

		cam.transform.rotation = Quaternion.Euler (new Vector3 (cameraX, cameraY, 0));

		float y = 0f;
		if (Input.GetKey (KeyCode.E)) y += 1;
		if (Input.GetKey (KeyCode.Q)) y -= 1;
		rb.velocity = cam.transform.rotation * new Vector3 (Input.GetAxis ("Horizontal"), y, Input.GetAxis ("Vertical")) * speed;
	}

	void RemoveArrow (bool ty) {
		golf.arrow.SetActive (ty);
	}

	void Update () {
		RestartShootingBall ();
		if (!startingPosition) MoveCameraWorld ();
		else ShootBallGolf ();
		rb.angularVelocity *= 0f;
		lastMousePosition = Input.mousePosition;
	}

	void CheckRotationArrow () {
		transform.RotateAround (transform.position, Vector3.up * Input.GetAxis ("Horizontal") * 3, 1);
		golf.arrow.transform.rotation = transform.rotation;
		cam.transform.position = golf.transform.position + (transform.rotation * new Vector3 (0, 1, -2));
	}
	void ShootBallGolf () {
		coll.radius = 0.06f;
		RemoveArrow (true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		// rb.velocity = Vector3.zero;

		
		// if (!fireDirectionSet) {
			// golf.transform.LookAt (golf.hole.transform.position);
			// shoot = golf.transform.rotation;
			// fireDirectionSet = true;
		// }
		// Vector3 angles = shoot.eulerAngles;
		// transform.rotation = Quaternion.Euler (new Vector3 (angles.x, angles.y + Input.GetAxis ("Horizontal") * 3, angles.z));
		// golf.arrow.transform.rotation = transform.rotation;
		// shoot = transform.rotation;
		// cam.transform.position = golf.transform.position + (shoot * new Vector3 (0, 1, -2));

		CheckRotationArrow();

		if (shootPower != 0) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				startingPosition = false;
				golf.rb.AddForce (cam.transform.rotation * powerDirection * (Mathf.PingPong (shootPower, 1) * 10f), ForceMode.Impulse);
				shootPower = 0f;
				cam.transform.position = golf.transform.position + (transform.rotation * new Vector3 (0, 1, -2));
			}
			shootPower += Time.deltaTime;
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			shootPower += Time.deltaTime;
		}



		shootBar.offsetMax = new Vector2 (-390f + (Mathf.PingPong (shootPower, 1) * 290f), shootBar.offsetMax.y);
	}

}