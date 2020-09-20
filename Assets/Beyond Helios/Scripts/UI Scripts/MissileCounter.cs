using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileCounter : MonoBehaviour {

	PlayerController2D playerController;
	Text missileCount;
	GameObject image;
	GameObject text;

	void Start(){
		missileCount = transform.Find ("Missile Text").GetComponent<Text> ();
		text = transform.Find ("Missile Text").gameObject;
		image = transform.Find ("Missile Image").gameObject;
	}

	void Update(){
		if(playerController == null){
			playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController2D> ();
		}
		if (playerController.missileCount > 0) {
			image.SetActive (true);
			text.SetActive (true);
		} else {
			image.SetActive (false);
			text.SetActive (false);
		}
		missileCount.text = playerController.missileCount.ToString();
	}


}

