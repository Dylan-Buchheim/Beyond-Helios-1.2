using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Prime31;

public class ContinueButton : MonoBehaviour {

	Button button;

	void Start(){
		button = GetComponent<Button> ();
	}

	void Update(){
		if (P31Prefs.getInt ("CurrentZone") > 1) {
			button.interactable = true;
		} else {
			button.interactable = false;
		}
	}
}
