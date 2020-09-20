using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigatePathfinding : MonoBehaviour {

	//Variables-------------------------------------------

	public Vector2 boundaries;

	private Vector3 target;
	public float speed = 5;
	public float turnDst = 2;
	public float turnSpeed = 2;

	Path path;

	private Rigidbody2D rb2d;

	//Start Method----------------------------------------

	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		target = transform.position + (transform.up * 40);
		Debug.DrawRay (transform.position + (transform.up * 2) , transform.up * 2 , Color.green);
		StartCoroutine (WaitToRequest());
	}

	IEnumerator WaitToRequest(){
		yield return new WaitForSeconds (0.1f);
		PathRequestManager.RequestPath (transform.position + (transform.up * 2), target , OnPathFound);
	}

	//Update Method---------------------------------------

	void Update(){
		rb2d.velocity = (transform.up * speed);

		if(Mathf.Abs(transform.position.x) > boundaries.x || Mathf.Abs(transform.position.y) > boundaries.y){
			Destroy (this.gameObject);
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

		while (true) {
			Vector2 pos2D = new Vector2 (transform.position.x , transform.position.y);

			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
				if (pathIndex == path.finshLineIndex) {
					followingPath = false;
					break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath) {
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