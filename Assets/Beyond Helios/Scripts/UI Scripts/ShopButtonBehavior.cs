using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Prime31;

public class ShopButtonBehavior : MonoBehaviour {

	public int number;
	public int cost;
	public bool ship = true;

	Button button;

	void Start(){
		button = GetComponent<Button> ();
	}

	void Update(){
		if(ship){
			if (P31Prefs.getInt ("ShipUnlock" + number) == 1 || cost > P31Prefs.getInt ("GemCount")) {
				button.interactable = false;
			} else {
				button.interactable = true;
			}
		}

		if(!ship ){
			if(P31Prefs.getInt("ExplosionUnlock" + number) == 1 || cost > P31Prefs.getInt("GemCount")){
				button.interactable = false;
			} else {
				button.interactable = true;
			}
		}
	}
}
