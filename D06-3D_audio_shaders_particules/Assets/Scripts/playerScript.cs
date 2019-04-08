using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour {

	public detectionBarScript detection;
	private bool runPlayer = false;

	public float speed;

	public AudioClip getKeySound;
	public AudioClip musicNormal;
	public AudioClip musicPanic;
	public GameObject foot;

	private enum audioEnum { normal, panic }
	private audioEnum currentAudio = audioEnum.normal;
	private enum walkSpeed { stay, normal, run }
	private walkSpeed currentWalkSpeed = walkSpeed.normal;

	public GameObject openDoor;
	public GameObject exitDoor;
	[SerializeField]
	private bool takeKey = false;
	[SerializeField]

	private bool takePaper = false;

	// Use this for initialization
	void Start () {
		if (detection == null) {
			detection = GetComponent<detectionBarScript> ();
		}
		StartCoroutine ("WalkSound");
	}

	public bool playertakeKey () {
		return takeKey;
	}

	public void removeKey () {
		takeKey = false;
	}

	void RunningDetection () {
		detection.ScrollInvisibleBar ();
		if (runPlayer) detection.AlarmDetection (1f);
		else if (!runPlayer) detection.DisableAlarmDetection (0.5f);
	}

	void RunningKey () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 10;
			runPlayer = true;
			currentWalkSpeed = walkSpeed.run;
		} else {
			speed = 5;
			runPlayer = false;
			currentWalkSpeed = walkSpeed.normal;
		}
	}

	void MusicDetection () {
		if (detection.invisible >= 75 && currentAudio != audioEnum.panic) {
			AudioSource audio = GetComponent<AudioSource> ();
			audio.clip = musicPanic;
			audio.Play ();
			currentAudio = audioEnum.panic;
		} else if (detection.invisible < 75 && currentAudio != audioEnum.normal) {
			AudioSource audio = GetComponent<AudioSource> ();
			audio.clip = musicNormal;
			audio.Play ();
			currentAudio = audioEnum.normal;
		}
		if (detection.invisible < 10) {
			gameManager.gm.ActiveAlarm (false);
		}
	}

	void OutOfGame () {
		if (detection.invisible >= 100) {
			gameManager.gm.SetMsg ("");
			StartCoroutine ("RestartGame");
		}
	}

	/* OnTrigger */

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Exit") {
			if (takePaper) {
				exitDoor.GetComponent<Animator> ().SetTrigger ("isEnter");
				gameManager.gm.SetMsg ("Thanks to play");
				StartCoroutine ("RestartGame");
			} else { }

		}

	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.tag == "Key") {
			if (!takeKey) {
				gameManager.gm.SetMsg ("Press E to take KeyCard !");
				if (Input.GetKey (KeyCode.E)) {
					AudioSource audio = other.GetComponent<AudioSource> ();
					audio.Play ();
					StartCoroutine (DestroyKeyTaken (other.gameObject));
					takeKey = true;
					gameManager.gm.SetMsg ("You Can Open The Door ! Good Luck Student !");
				}
			}
		} else if (other.gameObject.tag == "Papers" && !takePaper) {
			gameManager.gm.SetMsg ("Press E to take paper !");
			if (Input.GetKey (KeyCode.E)) {
				takePaper = true;
				gameManager.gm.SetMsg ("Well DONE Agent, Go out KNOW");
			}
		} else if (other.gameObject.tag == "Terminal") {
			if (takeKey) {
				gameManager.gm.SetMsg ("Press E to open Door !");
				if (Input.GetKey (KeyCode.E)) {
					openDoor.GetComponent<Animator> ().SetTrigger ("openDoor");
					openDoor.GetComponent<AudioSource> ().Play ();
				}

			} else {
				gameManager.gm.SetMsg ("Need to Badge to open door");
			}
		} else if (other.gameObject.tag == "light") {
			detection.AlarmDetection (.5f);
		}

	}

	void OnTriggerExit (Collider other) {
		detection.catchPlayer = false;
	}

	/* Update */
	void Update () {

		RunningDetection ();
		RunningKey ();
		MusicDetection ();
		OutOfGame ();
		if (Input.GetKey (KeyCode.W)) transform.Translate (Vector3.forward * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.S)) transform.Translate (Vector3.back * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.A)) transform.Translate (Vector3.left * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.D)) transform.Translate (Vector3.right * speed * Time.deltaTime);
		if (!Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.W)) currentWalkSpeed = walkSpeed.stay;

	}

	/* Coroutine */

	IEnumerator WalkSound () {
		while (true) {
			float wait = 0;
			switch (currentWalkSpeed) {
				case walkSpeed.normal:
					foot.GetComponent<AudioSource> ().Play ();
					wait = 0.5f;
					break;
				case walkSpeed.run:
					foot.GetComponent<AudioSource> ().Play ();
					wait = 0.2f;
					break;
				default:
					foot.GetComponent<AudioSource> ().Stop ();
					break;
			}
			yield return new WaitForSeconds (wait);
		}
	}

	IEnumerator RestartGame () {
		yield return new WaitForSeconds (4);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	IEnumerator DestroyKeyTaken (GameObject key) {
		yield return new WaitForSeconds (.5f);
		Destroy (key);
	}
}