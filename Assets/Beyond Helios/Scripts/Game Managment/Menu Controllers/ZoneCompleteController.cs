using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

using Prime31;

public class ZoneCompleteController : MonoBehaviour {

	public MenuShip menuShip;
	private Animator panelAnim;
	private Animator transitionAnim;
	private AudioManager am;



	void Start(){
		panelAnim = GameObject.Find ("ZoneCompletePanel").GetComponent<Animator> ();
		transitionAnim = GameObject.Find ("Transition").GetComponent<Animator> ();

		am = FindObjectOfType<AudioManager> ();
		// Increases the players gem count everytime they beat a level
		if (P31Prefs.getInt ("DifficultyNumber") == 0) {
			P31Prefs.setInt ("GemCount" , P31Prefs.getInt ("GemCount") + (1 + (Mathf.RoundToInt(P31Prefs.getInt("CurrentZone") / 15))));
		} else {
			P31Prefs.setInt ("GemCount" , P31Prefs.getInt ("GemCount") + (1 + (Mathf.RoundToInt(P31Prefs.getInt("CurrentZone") / 10))));
		}
		 
	}

	public void UpdateShip(GameObject ship){
		menuShip = ship.GetComponent<MenuShip> ();
	}

	public void NextZone(){
		StartCoroutine (NextZoneProcess());
	}

	public void SaveAndQuit(){
		StartCoroutine (SaveProcess());
	}

	public void PlaySelectSound(){
		am.Play ("Select1");
	}
		
	IEnumerator NextZoneProcess(){
		panelAnim.SetBool ("Leaving" , true);
		yield return new WaitForSeconds (0.75f);
		menuShip.StartMoving ();
		yield return new WaitForSeconds (0.5f);
		transitionAnim.SetBool ("Leaving" , true);
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene ("BuildScene");

	}

	IEnumerator SaveProcess(){
		transitionAnim.SetBool ("Leaving" , true);
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene ("MainMenu");
	}
}
