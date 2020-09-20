using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour {

	//Variables-------------------------

	public int bulletSpeed = 5;
	public int maxDistance = 200;
	public bool collidesWithPlayer = false;
	public bool collidesWithShips = false;
	public bool collidesWithLargeObjects = false;


	TrailRenderer trail;
	GameObject[] fighters;
	GameObject[] guns;
	List<GameObject> targets = new List<GameObject>();

	public GameObject target;

	public GameObject explosionFX;

	private Vector3 lastPosition;
	private float distanceTravelled = 0;
	private Rigidbody2D rb2d;

	float timeElapsed;

	//Start Method----------------------

	void Start(){
		trail = GetComponentInChildren<TrailRenderer> ();
		lastPosition = transform.position;
		rb2d = GetComponent<Rigidbody2D> ();

		fighters = GameObject.FindGameObjectsWithTag ("Enemy");
		guns = GameObject.FindGameObjectsWithTag ("MothershipGun");

		foreach(GameObject n in fighters){
			targets.Add (n);
		}
		foreach(GameObject n in guns){
			targets.Add (n);
		}
			
		if(targets.Count > 0){

			float currentClosest = Vector3.Distance(transform.position , targets[0].transform.position);
			target = targets [0];
			foreach(GameObject n in targets){
				float distance = Vector3.Distance(transform.position , n.transform.position);
				if(distance < currentClosest){
					target = n;
					currentClosest = distance;
				}
			}

			if(Vector3.Distance(transform.position , target.transform.position) > 7.5f){
				target = null;
			}

		}
			
	}

	//Update Method---------------------

	void Update () {

		timeElapsed += Time.deltaTime;

		//Distance Tracking
		distanceTravelled += Vector3.Distance(transform.position , lastPosition);
		lastPosition = transform.position;

		if (distanceTravelled > maxDistance) {
			Instantiate (explosionFX , transform.position , transform.rotation);
			Destroy (this.gameObject);
		}
			
	}

	void FixedUpdate(){
		if (target == null) {
			rb2d.velocity = (transform.up * bulletSpeed);
		} else {
		
			//Forward Movement
			rb2d.velocity = (transform.up * bulletSpeed);

			Vector3 diff = target.transform.position - transform.position;
			diff.Normalize ();

			float angle = (Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg) - 90;
			angle = (angle < 0) ? angle + 360 : angle;

			transform.rotation = Quaternion.Lerp(transform.rotation , Quaternion.Euler(0f , 0f , angle) , Time.deltaTime * 10);

		}


	}

	//Detects if the bullet has collided with anything on the BulletCollide Layer

	void OnTriggerEnter2D (Collider2D other){
		if ((other.gameObject.layer == 8 || other.gameObject.layer == 12 || other.gameObject.layer == 13) && collidesWithLargeObjects && timeElapsed > .05f && !other.tag.Equals("MothershipLaser")) {
			Instantiate (explosionFX , transform.position , transform.rotation);
			Destroy (this.gameObject); //Collides with all pathfinding blockers, motherships, and frigates
		}
		if ((other.gameObject.layer == 9 || other.gameObject.layer == 14) && collidesWithShips && timeElapsed > .05f) {
			Instantiate (explosionFX , transform.position , transform.rotation);
			Destroy (this.gameObject); // Collides with human and alien fighters
		}
		if (other.gameObject.layer == 10 && collidesWithPlayer && timeElapsed > .05f) {
			Instantiate (explosionFX , transform.position , transform.rotation);
			Destroy (this.gameObject); // Collides with the player
		}

		if(other.tag.Equals("MothershipLaser")){
			MothershipLaser laserController = other.gameObject.GetComponent<MothershipLaser> ();
			if(laserController.laserOn){
				Destroy (this.gameObject);
			}
		}
	}

	void OnDestroy(){
		trail.transform.parent = null;
		trail.autodestruct = true;
		trail = null;
	}
}
