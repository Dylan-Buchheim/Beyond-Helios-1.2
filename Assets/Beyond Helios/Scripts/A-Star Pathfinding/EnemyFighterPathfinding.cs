using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterPathfinding : MonoBehaviour {

//Variables-------------------------------------------

	public bool hacked = false;

	private Transform target;
	public float speed = 5;
	public float turnDst = 2;
	public float turnSpeed = 2;

	Path path;
	EnemyFighterBehavior EFB;
	HackedFighterBehavior HFB;

	private Rigidbody2D rb2d;

//Start Method----------------------------------------

	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		EFB = GetComponent<EnemyFighterBehavior> ();
		HFB = GetComponent<HackedFighterBehavior> ();
		if (!hacked) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		} 

		StartCoroutine ("UpdatePath");
	}

//Update Method---------------------------------------

	void Update(){
		if(target == null && hacked){
			target = HFB.FindNewTarget ().transform;
		}
		rb2d.velocity = (transform.up * speed);
	}

//UpdatePath Method, makes the unit periodically update its path

	IEnumerator UpdatePath(){
		yield return 0;
		while (this.gameObject != null && !EFB.destroyed) {
			PathRequestManager.RequestPath (transform.position,target.position, OnPathFound);
			yield return new WaitForSeconds (0.2f);
		}
	}

//OnPathFound Method, once the enemy has been given a path it will call this method to begin following the path

	public void OnPathFound(Vector3[] waypoints , bool pathSuccessful){
		if (pathSuccessful) {
			path = new Path (waypoints , transform.position , turnDst);

			StopCoroutine ("FollowPath");
			StartCoroutine ("FollowPath");
		}
	}

//FollowPath Coroutine, the coroutine which loops through the enemies given path 

	IEnumerator FollowPath(){

		bool followingPath = true;
		int pathIndex = 0;
		//transform.LookAt (path.lookPoints[0]);

		while (pathIndex <= path.lookPoints.Length - 1) { // This used to be a while true loop
			Vector2 pos2D = new Vector2 (transform.position.x , transform.position.y);

			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
				if (pathIndex == path.finshLineIndex) {
					followingPath = false;
					break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath) { 																// Pathing Error happens here????
				Vector3 diff = path.lookPoints [pathIndex] - transform.position;
				diff.Normalize ();
				float targetAngle = (Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg) - 90;
				Quaternion targetRotation = Quaternion.Euler (0f , 0f , targetAngle);
				transform.rotation = Quaternion.Slerp (transform.rotation , targetRotation , Time.deltaTime * turnSpeed);
			}
			yield return null;
		}
	}

//OnDrawGizmos Method, draws the path that the ship is taking

	public void OnDrawGizmos(){
		if (path != null) {
			path.DrawWithGizmos ();
		}
	}
}
