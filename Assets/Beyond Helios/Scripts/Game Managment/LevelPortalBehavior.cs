using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortalBehavior : MonoBehaviour {

	FollowCamera followCamera;
	PlayerController2D playerController;
	Animator transitionAnim;

	public bool levelComplete = false;

	void Start(){
		followCamera = GameObject.Find ("CameraContainer").GetComponent<FollowCamera> ();
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController2D> ();
		transitionAnim = GameObject.Find ("Transition").GetComponent<Animator> ();
	}

	void OnTriggerStay2D (Collider2D other){
		if(levelComplete && other.tag == "Player"){
			StartCoroutine (LeaveLevel());
		}
	}

	IEnumerator LeaveLevel(){
		followCamera.active = false;  			// Stops the camera from following the player
		playerController.leaving = true;	// Speeds the player up and stops them from controlling the player
		yield return new WaitForSeconds (0.5f);
		transitionAnim.SetBool ("Leaving" , true);
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene ("LevelCompleteMenu");
		FindObjectOfType<AudioManager>().Play ("PlayerWin");
	}
}
