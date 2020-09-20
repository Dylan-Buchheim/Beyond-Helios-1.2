using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Prime31;

public class MainMenuController : MonoBehaviour {

	Animator transitionAnim;
	MenuShip menuShip;
	AudioManager am;

	void Start(){
		transitionAnim = GameObject.Find ("Transition").GetComponent<Animator> ();
		am = FindObjectOfType<AudioManager> ();

		if(P31Prefs.getInt("CurrentZone") == 0){
			P31Prefs.setInt ("CurrentZone", 1);
		}
	}

	public void UpdateShip(GameObject newShip){
		menuShip = newShip.GetComponent<MenuShip> ();
	}

	public void StartNewGame(){
		StartCoroutine (NewGame());

	}

	public void StartCurrentGame(){
		StartCoroutine (ContinueGame());

	}

	public void GoToMenu(string levelName){
		StartCoroutine (ControlProcess (levelName));

	}

	public void PlaySelectSound(){
		am.Play ("Select1");
	}

	IEnumerator NewGame(){
		P31Prefs.setInt ("CurrentZone" , 1);
		P31Prefs.setInt ("CurrentScore" , 0);
		P31Prefs.setInt ("CurrentEasyScore" , 0);

		menuShip.StartMoving ();
		yield return new WaitForSeconds (0.5f);
		transitionAnim.SetBool ("Leaving" ,true);
		yield return new WaitForSeconds (0.25f);
		if (PlayerPrefs.GetInt ("PlayedTutorial") == 0) {
			SceneManager.LoadScene ("InteractiveTutorial");
		} else {
			SceneManager.LoadScene ("BuildScene");
		}
	}

	IEnumerator ContinueGame(){

		menuShip.StartMoving ();
		yield return new WaitForSeconds (0.5f);
		transitionAnim.SetBool ("Leaving" ,true);
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene ("BuildScene");

	}

	IEnumerator ControlProcess(string levelName){
		transitionAnim.SetBool ("Leaving" , true);
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene (levelName);
	}
}
