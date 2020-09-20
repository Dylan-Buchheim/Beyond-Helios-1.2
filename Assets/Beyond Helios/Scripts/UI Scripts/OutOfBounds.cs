using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

//Public Variables ----------------------- 

	public Vector2 worldBoundaries;
	private Transform player;
	private PlayerController2D playerController;
	LevelPortalBehavior portal;
	private Animator anim;
	private bool outOfBounds = false;
	private Text text;

//Start Method ---------------------------

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerController = player.GetComponent<PlayerController2D> ();
		portal = GameObject.Find ("_LevelPortal").GetComponent<LevelPortalBehavior> ();
		anim = GetComponent<Animator> ();
		text = GetComponent<Text> ();
	}

//Update Method --------------------------

	float elapsedTime = 10;

	void Update(){
		CheckBoundaries ();
		if (outOfBounds && !playerController.destroyed && !portal.levelComplete) {
			anim.SetBool ("FadeInOut" , true);
			elapsedTime -= Time.deltaTime;
			text.text = ("Return to the Battlefield!\n" + Mathf.RoundToInt(elapsedTime));
			if(elapsedTime < 0){
				playerController.DestroyPlayer ();
				elapsedTime = 10;
			}
		} else{
			anim.SetBool ("FadeInOut" , false);
			elapsedTime = 10;
		}
	}

//CheckBoundaries Method, checks to see if the player has left the specified world boundaries

	void CheckBoundaries(){
		if (player.position.x < -worldBoundaries.x || player.position.x > worldBoundaries.x ||
		   player.position.y < -worldBoundaries.y || player.position.y > worldBoundaries.y) {
			outOfBounds = true;
		} else{
			outOfBounds = false;
		}
	}

}
