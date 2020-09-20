using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;


public class DifficultyPopup : MonoBehaviour {

	public GameObject ui;

	//Start Method used for initialization

	public void StartEasyGame(){
		P31Prefs.setInt ("DifficultyNumber", 0);
	}

	public void StartHardGame(){
		P31Prefs.setInt ("DifficultyNumber", 1);
	}

	//Toggle Method, toggles the pause menu

	public void Toggle(){

		ui.SetActive (!ui.activeSelf);

	}
}
