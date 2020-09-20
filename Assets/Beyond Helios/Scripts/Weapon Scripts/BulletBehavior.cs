using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

//Basic Laserbeam logic for now---------------------------------

//Variables-------------------------

	public int bulletSpeed = 5;
	public int maxDistance = 200;
	public bool collidesWithPlayer = false;
	public bool collidesWithShips = false;
	public bool collidesWithLargeObjects = false;

	private Vector3 lastPosition;
	private float distanceTravelled = 0;
	private Rigidbody2D rb2d;

	float timeElapsed;
	
//Start Method----------------------

	void Start(){
		lastPosition = transform.position;
		rb2d = GetComponent<Rigidbody2D> ();
	}

//Update Method---------------------

	void Update () {

		timeElapsed += Time.deltaTime;

		//Distance Tracking
		distanceTravelled += Vector3.Distance(transform.position , lastPosition);
		lastPosition = transform.position;

		if (distanceTravelled > maxDistance) {
			Destroy (this.gameObject);
		}
	}

	void FixedUpdate(){

		//Forward Movement
		rb2d.velocity = (transform.up * bulletSpeed);
	}

//Detects if the bullet has collided with anything on the BulletCollide Layer

	void OnTriggerStay2D (Collider2D other){
		if ((other.gameObject.layer == 8 || other.gameObject.layer == 12 || other.gameObject.layer == 13) && collidesWithLargeObjects && timeElapsed > .05f && !other.tag.Equals("MothershipLaser")) {
			Destroy (this.gameObject); //Collides with all pathfinding blockers, motherships, and frigates
		}
		if ((other.gameObject.layer == 9 || other.gameObject.layer == 14) && collidesWithShips && timeElapsed > .05f) {
			Destroy (this.gameObject); // Collides with human and alien fighters
		}
		if (other.gameObject.layer == 10 && collidesWithPlayer && timeElapsed > .05f) {
			Destroy (this.gameObject); // Collides with the player
		}

		if(other.tag.Equals("MothershipLaser")){
			MothershipLaser laserController = other.gameObject.GetComponent<MothershipLaser> ();
			if(laserController.laserOn){
				Destroy (this.gameObject);
			}
		}
	}




}
