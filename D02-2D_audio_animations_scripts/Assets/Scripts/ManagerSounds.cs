using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSounds : MonoBehaviour {

	public AudioClip[] AcknowledgeSounds;
	public AudioClip[] SelectedSounds;
	public AudioClip[] DeadSounds;
	public AudioClip[] AttackSounds;
	public AudioClip[] AnnoyedSounds;
	public float attack_interval = 2.0f;

	private Dictionary<string, AudioSource[]> sounds;
	private float timerAttack;

	public AudioSource AddAudio (AudioClip clip, bool loop, bool playAwake, float vol) {
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;
		return newAudio;
	}

	public void AddClips (string name, ref AudioClip[] clips) {
		AudioSource[] sources = new AudioSource[clips.Length];
		for (int i = 0; i < clips.Length; ++i) {
			sources[i] = AddAudio (clips[i], false, false, 0.5f);
		}
		try {
			sounds[name] = sources;
		} catch (UnityException error) {
			Debug.Log (error);
		}
	}

	// Use this for initialization
	void Start () {
		sounds = new Dictionary<string, AudioSource[]> ();
		timerAttack = 0.0f;
		AddClips ("Acknowledge", ref AcknowledgeSounds);
		AddClips ("Annoyed", ref AnnoyedSounds);
		AddClips ("Selected", ref SelectedSounds);
		AddClips ("Dead", ref DeadSounds);
		AddClips ("Attack", ref AttackSounds);
	}

	public int Play (string name) {
		int length = 0;
		try {
			AudioSource[] array = sounds[name];
			if (array != null && array.Length > 0) {
				int i = Random.Range (0, array.Length);
				array[i].Play ();
				length = (int) array[i].clip.length;
			}
		} catch (UnityException error) {
			Debug.Log (error);
		}
		return length;
	}

	public int Attack () {
		if (timerAttack >= attack_interval) {
			timerAttack = 0.0f;
			return Play ("Attack");
		}
		return 0;
	}

	// Update is called once per frame
	void Update () {
		timerAttack += Time.deltaTime;
	}
}