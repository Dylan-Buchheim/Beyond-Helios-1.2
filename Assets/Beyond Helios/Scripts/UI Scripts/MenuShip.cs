using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShip : MonoBehaviour {

	private Rigidbody2D rb2d;
	public Vector3 endPoint;

	bool leaving = false;

	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		endPoint = transform.parent.position;
	}

	void Update(){
		if(!leaving){
			float step = 20 * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position , endPoint , step);
		}
	}
		
	float velMultiplier = 0.125f;
	IEnumerator MovingUp(){
		while(velMultiplier <= 20){
			rb2d.velocity = transform.up * velMultiplier;
			velMultiplier += 0.125f;
		}

		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}


	public void StartMoving(){
		leaving = true;
		StartCoroutine (MovingUp());
	}
}
