using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTPPopup : MonoBehaviour {

	public GameObject ui;

	//Start Method used for initialization

	void Start(){
		if(PlayerPrefs.GetInt("FirstTime") == 0){
			Toggle ();
			PlayerPrefs.SetFloat ("MusicLevel", 0.25f);
			PlayerPrefs.SetFloat ("SoundLevel", 1f);
			PlayerPrefs.SetInt ("FirstTime" , 1);
		}
	}

	//Toggle Method, toggles the pause menu

	public void Toggle(){

		ui.SetActive (!ui.activeSelf);

	}
}
