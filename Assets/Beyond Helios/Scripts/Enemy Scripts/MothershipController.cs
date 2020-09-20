using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipController : MonoBehaviour {

//Public Variables --------------------------------

	public GameObject destroyedMothership;
	public GameObject explosionFX;

//Private Variables -------------------------------

	private bool destroyedMS = false;

	private ScoreKeeper scoreKeep;

	private GameObject gunTR;
	MothershipGun controllerTR;
	private GameObject gunTL;
	MothershipGun controllerTL;
	private GameObject gunBL;
	MothershipGun controllerBL;
	private GameObject gunBR;
	MothershipGun controllerBR;
	private GameObject gunTarget;

//Start Method ------------------------------------

	void Start () {
		gunTarget = GameObject.FindGameObjectWithTag ("Player");
		gunTR = this.gameObject.transform.Find ("Mothership_Gun_1").gameObject;
		gunTL = this.gameObject.transform.Find ("Mothership_Gun_2").gameObject;
		gunBL = this.gameObject.transform.Find ("Mothership_Gun_3").gameObject;
		gunBR = this.gameObject.transform.Find ("Mothership_Gun_4").gameObject;
		controllerTR = gunTR.GetComponent<MothershipGun> ();
		controllerTL = gunTL.GetComponent<MothershipGun> ();
		controllerBL = gunBL.GetComponent<MothershipGun> ();
		controllerBR = gunBR.GetComponent<MothershipGun> ();

		scoreKeep = GameObject.Find ("_ScoreKeeper").GetComponent<ScoreKeeper> ();
	}
	
//Update Method -----------------------------------

	void Update () {
		SetupTurrets ();
		OnMothershipDestroy ();
	}
//Turret Setup Method---------------------------------------------------------------

	void SetupTurrets(){

		//Vector Bewtween Turrets and Player

		Vector3 diffTR = gunTarget.transform.position - gunTR.transform.position;
		diffTR.Normalize ();
		Vector3 diffTL = gunTarget.transform.position - gunTL.transform.position;
		diffTL.Normalize ();
		Vector3 diffBL = gunTarget.transform.position - gunBL.transform.position;
		diffBL.Normalize ();
		Vector3 diffBR = gunTarget.transform.position - gunBR.transform.position;
		diffBR.Normalize ();

		//Floats that store the angle between gun and target 

		float gunTRAngle = (Mathf.Atan2 (diffTR.y, diffTR.x) * Mathf.Rad2Deg) - 90;
		gunTRAngle = (gunTRAngle < 0) ? gunTRAngle + 360 : gunTRAngle;
		float gunTLAngle = (Mathf.Atan2 (diffTL.y, diffTL.x) * Mathf.Rad2Deg) - 90;
		gunTLAngle = (gunTLAngle < 0) ? gunTLAngle + 360 : gunTLAngle;
		float gunBLAngle = (Mathf.Atan2 (diffBL.y, diffBL.x) * Mathf.Rad2Deg) - 90;
		gunBLAngle = (gunBLAngle < 0) ? gunBLAngle + 360 : gunBLAngle;
		float gunBRAngle = (Mathf.Atan2 (diffBR.y, diffBR.x) * Mathf.Rad2Deg) - 90;
		gunBRAngle = (gunBRAngle < 0) ? gunBRAngle + 360 : gunBRAngle;

		//Turret Limits and turning

		//TR Limits
		if (controllerTR.destroyed == false) {
			if (gunTRAngle > 25 && gunTRAngle < 245) {
				gunTRAngle = transform.rotation.z;
				controllerTR.inLineOfSight = false;
			} else {
				gunTR.transform.rotation = Quaternion.Euler(0f , 0f , gunTRAngle);
				controllerTR.inLineOfSight = true;
			}
		} else {
			gunTR.transform.rotation = Quaternion.Euler (0 , 0 , 0);
		}

		//TL Limits
		if (controllerTL.destroyed == false) {
			if (gunTLAngle > 115 && gunTLAngle < 335) {
				gunTLAngle = 45;
				controllerTL.inLineOfSight = false;
			} else {
				gunTL.transform.rotation = Quaternion.Euler(0f , 0f , gunTLAngle);
				controllerTL.inLineOfSight = true;
			}
		} else {
			gunTL.transform.rotation = Quaternion.Euler (0 , 0 , 0);
		}

		//BL Limits
		if (controllerBL.destroyed == false) {
			if (gunBLAngle > 205 || gunBLAngle < 65) {
				gunBLAngle = 135;
				controllerBL.inLineOfSight = false;
			} else {
				gunBL.transform.rotation = Quaternion.Euler(0f , 0f , gunBLAngle);
				controllerBL.inLineOfSight = true;
			}
		} else {
			gunBL.transform.rotation = Quaternion.Euler (0 , 0 , 0);
		}

		//BR Limits
		if (controllerBR.destroyed == false) {
			if (gunBRAngle > 295 || gunBRAngle < 155) {
				gunBRAngle = 225;
				controllerBR.inLineOfSight = false;
			} else {
				gunBR.transform.rotation = Quaternion.Euler(0f , 0f , gunBRAngle);
				controllerBR.inLineOfSight = true;
			}
		} else {
			gunBR.transform.rotation = Quaternion.Euler (0 , 0 , 0);
		}

	}

//Method that checks for the destruction of the Mothership---------------------------

	void OnMothershipDestroy(){
		if(controllerTR.destroyed && controllerTL.destroyed && controllerBL.destroyed && controllerBR.destroyed && !destroyedMS){
			destroyedMS = true;
			scoreKeep.AddScore (200);
			Instantiate (explosionFX , transform.position , transform.rotation);
			StartCoroutine (DestroyProccess());
		}
	}

	IEnumerator DestroyProccess(){
		yield return new WaitForSeconds (0.75f);

		Instantiate (destroyedMothership , transform.position , transform.rotation);
		Destroy (this.gameObject);
	}
}
