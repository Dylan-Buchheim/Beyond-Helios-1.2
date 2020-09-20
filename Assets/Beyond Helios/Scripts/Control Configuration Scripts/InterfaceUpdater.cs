using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceUpdater : MonoBehaviour {

	public Slider guiScale;
	public Slider zoomLevel;
	public Slider musicLevel;
	public Slider soundLevel;

	void Start(){
		if(PlayerPrefs.GetFloat ("GUIScale") != 0){
			guiScale.value = PlayerPrefs.GetFloat ("GUIScale");
		}
		if(PlayerPrefs.GetFloat ("ZoomLevel") != 0){
			zoomLevel.value = PlayerPrefs.GetFloat ("ZoomLevel");
		}

		musicLevel.value = PlayerPrefs.GetFloat ("MusicLevel");

		soundLevel.value = PlayerPrefs.GetFloat ("SoundLevel");

	}
}
