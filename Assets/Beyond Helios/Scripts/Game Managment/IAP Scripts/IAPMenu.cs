using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPMenu : MonoBehaviour {

	public GameObject ui;

//Start Method used for initialization

	void Start(){
		
	}

//Toggle Method, toggles the pause menu

	public void Toggle(){

		ui.SetActive (!ui.activeSelf);

	}
}
