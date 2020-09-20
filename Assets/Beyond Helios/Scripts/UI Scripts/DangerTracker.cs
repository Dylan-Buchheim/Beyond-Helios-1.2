using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DangerTracker : MonoBehaviour {

	private GameManager gameManager;

	private Text text;
	public Color lowDanger;
	public Color mediumDanger;
	public Color highDanger;

	void Start(){
		gameManager = GameObject.Find ("_GameManager").GetComponent<GameManager> ();
		text = GetComponent<Text> ();
		lowDanger = text.color;
	}

	void Update(){
		if(gameManager.dangerLevel == 1){
			text.text = ("Danger: Low");
			text.color = lowDanger;
		}
		if(gameManager.dangerLevel == 2){
			text.text = ("Danger: Medium");
			text.color = mediumDanger;
		}
		if(gameManager.dangerLevel == 3){
			text.text = ("Danger: High");
			text.color = highDanger;
		}
	}
}
