using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

//Obhect Reference to the desired target object, set in editor

	public bool active =  true;

	private GameObject target;
	private PlayerController2D targetController;
	private bool lerping = false;

//Start Method for initialization

	void Start(){
		target = GameObject.FindGameObjectWithTag ("Player");
		targetController = target.GetComponent<PlayerController2D> ();
	}

// Update Method which sets the position of the camera to the target object position with a z offset of -10	

	void Update () {
		if (!targetController.destroyed && active) {
			transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10);
			lerping = false;
		}
		if(!lerping && targetController.destroyed){
			StartCoroutine ("MoveToSpawn");
		}
		//This still isnt working right its not pausing upon death
		if (lerping) {
			transform.position = Vector3.Slerp (transform.position , new Vector3(target.transform.position.x , target.transform.position.y , -10) , Time.deltaTime * 5);
		}
	}

	IEnumerator MoveToSpawn(){
		yield return new WaitForSeconds (1);
		lerping = true;
	}

}
