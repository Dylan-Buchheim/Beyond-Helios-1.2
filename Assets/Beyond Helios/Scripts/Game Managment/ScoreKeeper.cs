using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31; 

public class ScoreKeeper : MonoBehaviour {

//Public Variables

	public bool tutorial = false;
	public int score =0;
	public int highScore =0;

//Start Method initializes the highScore with the one saved in the PlayerPrefsFile 

	void Start(){
		if (P31Prefs.getInt ("DifficultyNumber") == 0) {
			highScore = P31Prefs.getInt ("EasyHighScore");
			score = P31Prefs.getInt ("CurrentEasyScore");
		} else {
			highScore = P31Prefs.getInt ("HighScore");
			score = P31Prefs.getInt ("CurrentScore");
		} 

	}

//Update Method, used to set the high score if it has been beaten

	void Update(){
		if(!tutorial){
			if (P31Prefs.getInt ("DifficultyNumber") == 0) {
				P31Prefs.setInt ("CurrentEasyScore" , score);
				if(score > highScore){
					highScore = score;
					P31Prefs.setInt ("EasyHighScore" , highScore);
				}
			} else {
				P31Prefs.setInt ("CurrentScore" , score);
				if(score > highScore){
					highScore = score;
					P31Prefs.setInt ("HighScore" , highScore);
				}
			}

		}
	}

//AddScore Method, method called by other classes to add to the current score

	public void AddScore(int amount){
		score += amount;
	}
}
