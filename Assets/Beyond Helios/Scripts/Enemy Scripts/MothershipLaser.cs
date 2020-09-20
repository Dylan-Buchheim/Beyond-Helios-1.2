using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipLaser : MonoBehaviour {

	MothershipLaserController mothershipController;
	Animator anim;

	public bool laserOn = false;

	void Start(){
		mothershipController = GetComponentInParent<MothershipLaserController> ();
		anim = GetComponent<Animator> ();
	}

	void Update(){
		//Checks to see is the mothership is spinning 
		if (mothershipController.spinning && !laserOn) {
			laserOn = true;
			anim.SetBool ("Spinning", true);
		}else if(!mothershipController.spinning && laserOn){
			laserOn = false;
			anim.SetBool ("Spinning", false);
		}
	}
}
