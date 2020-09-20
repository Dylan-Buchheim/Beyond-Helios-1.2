using UnityEngine.Audio;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour {

	public Sound[] sounds;
	private AudioSource[] sources;
	public float musicVolume;

	public static MusicManager instance;

	void Awake(){

		musicVolume = PlayerPrefs.GetFloat ("MusicLevel");

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

			s.volume = musicVolume;
			s.source.volume = s.volume;

			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

		sources = GetComponents<AudioSource> ();
	}

	AudioSource currentSource;
	int index;
	void Start(){
		index = UnityEngine.Random.Range (0, sounds.Length);
		Sound s = sounds[index];
		currentSource = s.source;
		s.source.Play ();
	}

	void Update(){
		if (musicVolume != PlayerPrefs.GetFloat ("MusicLevel")) {
			musicVolume = PlayerPrefs.GetFloat ("MusicLevel");
			foreach(AudioSource s in sources){
				s.volume = musicVolume;
			}
		}
		if (currentSource.isPlaying == false ) {
			SwitchSong ();
		}
	}

	void SwitchSong(){
		Sound s = sounds[index];
		if (index == sounds.Length - 1) {
			index = 0;
		} else {
			index++;
		}
		currentSource = s.source;
		s.source.Play ();
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
