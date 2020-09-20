using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LifeTracker : MonoBehaviour {

	private GameObject player;
	private PlayerController2D playerController;
	private Text text;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController2D> ();
		text = GetComponent<Text> ();
	}

	void Update(){
		if (playerController.lives >= 0) {
			text.text = ("Extra Ships x " + playerController.lives);
		} else {
			text.text = "Out of Ships";
		}

	}

}
