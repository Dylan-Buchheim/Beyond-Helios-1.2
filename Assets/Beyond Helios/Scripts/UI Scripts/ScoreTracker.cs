using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Prime31;

public class ScoreTracker : MonoBehaviour {

	private Text text;
	private ScoreKeeper scoreKeep;

	public bool onMenu = false;
	public bool gameOver = false;

	int gemsAdded;
	string gemText;

	void Start(){
		scoreKeep = GameObject.Find ("_ScoreKeeper").GetComponent<ScoreKeeper> ();
		text = GetComponent<Text> ();

		if (P31Prefs.getInt ("DifficultyNumber") == 0) {
			gemsAdded = (1 + (Mathf.RoundToInt (PlayerPrefs.GetInt ("CurrentZone") / 15)));
		} else {
			gemsAdded = (1 + (Mathf.RoundToInt (PlayerPrefs.GetInt ("CurrentZone") / 10)));
		}

		gemText = " gem";
	}


	void Update(){
		
		if (gemsAdded > 1) {
			gemText = " gems";
		}

		if (gameOver) {
			text.text = ("Score: " + PlayerPrefs.GetInt ("PreviousScore"));
		} else {

			if (!onMenu) {
				text.text = ("Hi-Score: " + scoreKeep.highScore + "\nScore: " + scoreKeep.score);
			} else {
				text.text = ("Score: " + scoreKeep.score + 
							 "\nHi-Score: " + scoreKeep.highScore +
					"\n" + gemsAdded + gemText + " Earned");
			}

		}
	}
}
