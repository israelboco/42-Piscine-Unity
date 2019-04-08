using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

	public playerScript player;
	public static gameManager gm { get; private set; }
	public Text message;
	public GameObject megaPhone;

	// Use this for initialization
	void Start () {
		if (gm == null)
			gm = this;
		message.GetComponent<Animator> ().SetTrigger ("showText");

	}

	// Update is called once per frame
	void Update () {

	}

	public void ActiveAlarm (bool isplay) {
		if (isplay) {
			if (!megaPhone.GetComponent<AudioSource> ().isPlaying) {
				megaPhone.GetComponent<AudioSource> ().Play ();
			}
		} else {
				megaPhone.GetComponent<AudioSource> ().Stop ();
		}
	}
	public void cameraSpotPlayer () {
		player.detection.AlarmDetection (20);
	}

	public void SetMsg (string msg) {
		message.text = msg;
		message.GetComponent<Animator> ().SetTrigger ("showText");
	}
}