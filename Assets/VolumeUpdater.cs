using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeUpdater : MonoBehaviour {

	public float scaleFactor = 0.5f;

	private float soundVolume;

	AudioSource source;

	void Start(){
		source = GetComponent<AudioSource> ();
		soundVolume = PlayerPrefs.GetFloat ("SoundLevel");
		source.volume = soundVolume * scaleFactor;
	}

	void Update(){
		if (soundVolume != PlayerPrefs.GetFloat ("SoundLevel")) {
			soundVolume = PlayerPrefs.GetFloat ("SoundLevel");
			source.volume = soundVolume * scaleFactor;
		}
	}
}
