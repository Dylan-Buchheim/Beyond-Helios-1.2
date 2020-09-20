using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;
	private AudioSource[] sources;
	public float soundVolume;

	public static AudioManager instance;

	void Awake(){

		soundVolume = PlayerPrefs.GetFloat ("SoundLevel");

		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		foreach(Sound s in sounds){
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.volume = soundVolume;
			s.source.volume = s.volume;

			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

		sources = GetComponents<AudioSource> ();
	}

	void Update(){
		if (soundVolume != PlayerPrefs.GetFloat ("SoundLevel")) {
			soundVolume = PlayerPrefs.GetFloat ("SoundLevel");
			foreach(AudioSource s in sources){
				s.volume = soundVolume;
			}
		}

	}

	public void Play(string name){
		Sound s = Array.Find (sounds , sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("Sound: " + name + " was not found!");
			return;
		}
			
		s.source.Play ();
	}
}
