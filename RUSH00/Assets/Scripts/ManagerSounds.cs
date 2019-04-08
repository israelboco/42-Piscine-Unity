using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSounds : MonoBehaviour {

	public static ManagerSounds instance { get; private set; }
	public AudioClip dropWeapon;
	public AudioClip Magnum;
	public AudioClip Punch;
	public AudioClip RocketLauncher;
	public AudioClip Saber;
	public AudioClip Shotgun;
	public AudioClip Sniper;
	public AudioClip Uzi;
	public AudioClip Reload;
	public AudioClip currentLvl;
	public AudioClip dead;
	public AudioClip winLevel;
	public AudioClip looseLevel;
	public AudioClip mainMenu;
	public AudioClip pauseGame;

	public float soundsTimer;

	private Dictionary<string, AudioSource> audioByName;

	public AudioSource AddAudio (AudioClip clip, bool loop, bool playAwake, float vol) {
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;
		return newAudio;
	}

	public void AddClips (string name, ref AudioClip clips) {
		new AudioSource ();
		AudioSource sources = new AudioSource ();
		// for (int i = 0; i < clips.Length; ++i) {
		sources = AddAudio (clips, false, false, 0.5f);
		// }
		try {
			audioByName[name] = sources;
		} catch (UnityException error) {
			Debug.Log (error);
		}
	}

	private void Awake () {
		instance = this;
	}
	// Use this for initialization
	void Start () {
		soundsTimer = Time.time;
		audioByName = new Dictionary<string, AudioSource> ();
        AddClips("currentLvl", ref currentLvl);
		AddClips ("dropWeapon", ref dropWeapon);

		AddClips ("Magnum", ref Magnum);
		AddClips ("Punch", ref Punch);
		AddClips ("RocketLauncher", ref RocketLauncher);
		AddClips ("Saber", ref Saber);
		AddClips ("Shotgun", ref Shotgun);
		AddClips ("Sniper", ref Sniper);
		AddClips ("Uzi", ref Uzi);

		AddClips ("Reload", ref Reload);

		AddClips ("dead", ref dead);
		AddClips ("winLevel", ref winLevel);
		AddClips ("looseLevel", ref looseLevel);
		AddClips ("mainMenu", ref mainMenu);
		AddClips ("pauseGame", ref pauseGame);

	}

	public int Play (string name) {
		int length = 0;
		try {
			Debug.Log(name);
			AudioSource source = audioByName[name];
			if (source != null) {
				source.Play ();
				// int i = Random.Range (0, array.Length);
				// array[i].Play ();
				// length = (int) array[i].clip.length;
			}
		} catch (KeyNotFoundException) {
			Debug.Log ("TEST");
		}
		return length;
	}

	// Update is called once per frame
	void Update () { }
}