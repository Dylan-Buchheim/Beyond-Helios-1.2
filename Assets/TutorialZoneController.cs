using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZoneController : MonoBehaviour {

	public InteractiveTutorialController tutorialController;
	public GameObject promptWindow;
	public bool active = false;
	public bool activated = false;

	void Start(){
		tutorialController = FindObjectOfType<InteractiveTutorialController> ();
	}

	void Update(){
		if(active){
			if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetKeyDown (KeyCode.Return)) {
				tutorialController.HideTextPrompt ();
				active = false;
				activated = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Equals("Player") && !activated){
			tutorialController.DisplayTextPrompt (promptWindow);
			active = true;
		}
	}
}
