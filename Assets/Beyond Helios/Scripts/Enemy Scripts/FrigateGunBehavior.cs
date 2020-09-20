using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigateGunBehavior : MonoBehaviour {

//Variables ------------------------------------
	GameObject firingPoint1;
 	GameObject firingPoint2;
	public GameObject laser;

	public float cooldown = 3;
	public float chargeTime = 1;

	GameObject target;
	GameObject parent;
	float distanceToPlayer;
	bool inLineOfSight = false;
	bool readyToFire = true;

	private AudioManager am;

//Start Method ----------------------------------

	void Start(){
		target = GameObject.FindGameObjectWithTag ("Player");
		parent = transform.parent.gameObject;
		firingPoint1 = transform.Find ("FiringPoint_1").gameObject;
		firingPoint2 = transform.Find ("FiringPoint_2").gameObject;
		am = FindObjectOfType<AudioManager> ();
	}

//Update Method, calculates the angle between the gun and player and whether or not it can fire

	void Update(){
		Vector3 diff = target.transform.position - transform.position;
		diff.Normalize ();
		distanceToPlayer = Vector3.Distance (transform.position , target.transform.position);

		//Calculates the angle between the player and gun and sets its boundaries
		float gunAngle = (Mathf.Atan2 (diff.y , diff.x) * Mathf.Rad2Deg) -90;
		gunAngle = (gunAngle < 0) ? gunAngle + 360 : gunAngle;
		float parentAngle = parent.transform.eulerAngles.z;
		parentAngle = (parentAngle < 0) ? parentAngle + 360 : parentAngle;
		float differenceInAngle = Mathf.Abs (parentAngle - gunAngle);


		//Calculates if the player is in the bounds of the shooting area
		if (differenceInAngle > 90) {
			gunAngle = transform.rotation.z;
			inLineOfSight = false;
		} else {
			transform.rotation = Quaternion.Euler (0f , 0f , gunAngle);
			inLineOfSight = true;
		}

		//Checks the conditions for shooting
		if(distanceToPlayer < 3.5f && inLineOfSight && readyToFire){
			StartCoroutine ("Fire");
		}
	}

//Fire Coroutine, handles the processes of firing the gun

	IEnumerator Fire(){
		readyToFire = false;

		yield return new WaitForSeconds (chargeTime);

		CreateBullet ();

		yield return new WaitForSeconds (cooldown);

		readyToFire = true;
	}

	void CreateBullet(){
		am.Play ("LaserBig");
		Instantiate (laser , firingPoint1.transform.position , transform.rotation);
		Instantiate (laser , firingPoint2.transform.position , transform.rotation);
	}
}
