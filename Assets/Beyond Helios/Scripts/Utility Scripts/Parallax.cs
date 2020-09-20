using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

//Variables--------------------------------

	public float parallaxAmount = 2f;
	public float smoothing = 1f;
	public Transform cam;

	private Vector3 previousCamPosition;
	private Vector3 startingPosition;

	public bool randomizePosition = false;

//Start Method ----------------------------

	void Awake () {
		cam = Camera.main.transform;
	}

	void Start(){
		if (randomizePosition) {
			startingPosition = new Vector3 (Random.Range(-10 , 10) , Random.Range(-5 , 6) , 0);
			transform.position = startingPosition;
		} else {
			startingPosition = transform.position;
		}

		previousCamPosition = cam.position;
	}
	
//Update Method ---------------------------

	void FixedUpdate () {
		Vector3 offset = (previousCamPosition - cam.position) * parallaxAmount;
		Vector3 targetPosition = transform.position + offset;

		transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);

		previousCamPosition = cam.position;

	}
}
