using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButtonBehavior : MonoBehaviour {

	public bool laserButton = true;

	Button button;
	public PlayerController2D playerController;

	void Start(){
		button = GetComponent<Button> ();
		button.onClick.AddListener (TaskOnClick);


	}

	void TaskOnClick(){
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController2D> ();
		if (laserButton) {
			playerController.defaultFire ();
		} else {
			playerController.createBomb ();
		}
	}
}
