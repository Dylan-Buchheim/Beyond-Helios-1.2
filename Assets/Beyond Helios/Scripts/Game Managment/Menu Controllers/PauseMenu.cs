using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public GameObject ui;

	public Slider musicLevel;
	public Slider soundLevel;

	void Start(){

		musicLevel.value = PlayerPrefs.GetFloat ("MusicLevel");
				
		soundLevel.value = PlayerPrefs.GetFloat ("SoundLevel");
		
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			Toggle ();
		}
	}

//Toggle Method, toggles the pause menu

	public void Toggle(){

		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}



	public void SetMusicLevel(float musicLevel){
		PlayerPrefs.SetFloat ("MusicLevel" , musicLevel);
	}

	public void SetSoundLevel(float soundLevel){
		PlayerPrefs.SetFloat ("SoundLevel" , soundLevel);
	}
}
