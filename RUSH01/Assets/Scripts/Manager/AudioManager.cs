using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public Sounds[] sounds;
	// Use this for initialization
	public static AudioManager instance ;
	private void Awake () {
		if(instance == null)
			instance = this;
		else {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		foreach (Sounds s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();

			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	
	private void Start() {
		Play("witcher");
	}
	public void Play (string name) {
		if (sounds != null && sounds.Length > 0) {
			for (int i = 0; i < sounds.Length; i++) {
				if (sounds[i].name == name) {
					Sounds s = sounds[i];
					s.source.Play();
					break;
				}
			}
		} else 
			return;
	}
}