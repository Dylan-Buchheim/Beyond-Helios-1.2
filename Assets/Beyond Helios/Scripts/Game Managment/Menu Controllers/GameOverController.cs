using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Prime31;

public class GameOverController : MonoBehaviour {

	Animator transitionAnim;

	//int gemCount;

	void Start(){
		transitionAnim = GameObject.Find ("Transition").GetComponent<Animator> ();
		P31Prefs.setInt ("CurrentZone" , 1);     //Reset the players statistics

		if (P31Prefs.getInt ("DifficultyNumber") == 0) {
			P31Prefs.setInt ("PreviousScore" , PlayerPrefs.GetInt("CurrentEasyScore"));
		} else {
			P31Prefs.setInt ("PreviousScore" , PlayerPrefs.GetInt("CurrentScore"));
		}

		P31Prefs.setInt ("CurrentEasyScore" , 0);
		P31Prefs.setInt ("CurrentScore" , 0);
	}

	public void ReturnToMain(){
		StartCoroutine (ReturnProcess());
		FindObjectOfType<AudioManager> ().Play ("Select1");
	}

	IEnumerator ReturnProcess(){
		transitionAnim.SetBool ("Leaving" , true);
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene ("MainMenu");
	}
}
