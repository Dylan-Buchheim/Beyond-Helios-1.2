using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarArrow : MonoBehaviour {

//Private Variables --------------------

	private GameObject[] motherships;
	private GameObject player;
	private PlayerController2D playerController;
	private SpriteRenderer sr;
	private int numberOfMotherships;
	private int indexOfClosestShip;
	private float closestDistance;

//Public Variables ----------------------

	public float distanceFromPlayer = 2;
	public float distanceThreshold = 10;
	public float maximumDistance = 2;

//Start Method for initialization

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

//Update Method -------------------------

	void Update(){
		UpdateList();
		if (numberOfMotherships > 0) {
			FindLeastDistance ();
			MoveArrow ();
		} else {
			Destroy (this.gameObject);
		}
		if (closestDistance < distanceThreshold || playerController.destroyed) {
			sr.enabled = false;
		} else {
			sr.enabled = true;
		}

	}

//Update List Method, updates the list of motherships in case one is destroyed

	void UpdateList(){
		motherships = GameObject.FindGameObjectsWithTag ("Mothership");
		numberOfMotherships = motherships.Length;
	}

//FindLeastDistance Method, calculates the closest mothership to the player

	void FindLeastDistance(){
		float currentBestDistance = Vector3.Distance(player.transform.position , motherships[0].transform.position);
		int index = 0;
		for(int i = 0; i <motherships.Length; i++){
			float currentDistance = Vector3.Distance (player.transform.position , motherships[i].transform.position);
			if (currentDistance < currentBestDistance) {
				currentBestDistance = currentDistance;
				index = i;
			}
		}
		indexOfClosestShip = index;
		closestDistance = currentBestDistance;
	}

//MoveArrow, moves the arrow to the proper position based on the angle between the closest mothership

	void MoveArrow(){
		Vector2 diff = player.transform.position - motherships [indexOfClosestShip].transform.position;
		diff.Normalize ();
		float angle = (Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg) - 90;
		Vector3 newPos = new Vector3 (0 , 0 , 0);
		newPos.x = player.transform.position.x + Mathf.Clamp(distanceFromPlayer * (closestDistance / 4) , distanceFromPlayer , maximumDistance) * Mathf.Sin(angle * Mathf.Deg2Rad);
		newPos.y = player.transform.position.y - Mathf.Clamp(distanceFromPlayer * (closestDistance / 4) , distanceFromPlayer , maximumDistance) * Mathf.Cos(angle * Mathf.Deg2Rad);
		transform.position = newPos;
		transform.rotation = Quaternion.Euler (0 , 0 , angle);
	}

}
