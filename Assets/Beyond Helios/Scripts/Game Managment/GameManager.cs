using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Prime31;

public class GameManager : MonoBehaviour {

	GameGenerator gameGenerator;
	AudioManager audioManager;
	Animator zoneClearAnim;
	Animator transitionAnim;
	LevelPortalBehavior portal;

	public bool tutorial = false;
	bool levelCompleted = false;

	public int maxAmountOfMotherships;
	public int maxAmountOfShips;
	public int dangerLevel  = 1;
	public float spawnRate = 2;

	public int zoneCounter = 1;
	public int zoneMultiplier;

	float elapsedTime = 0;
	public float timeUntillNextDangerLevel = 0;

	public GameObject[] mothershipsInPlay;
	public GameObject[] shipsInPlay;

//Start Method ---------------------------

	void Start(){
		//Instantiation
		if (tutorial) {
			gameGenerator = null;
		} else {
			gameGenerator = GameObject.Find ("_GameGenerator").GetComponent<GameGenerator> ();
		}
		audioManager = FindObjectOfType<AudioManager> ();
		zoneClearAnim = GameObject.Find ("ZoneClear Label").GetComponent<Animator> ();
		transitionAnim = GameObject.Find ("Transition").GetComponent<Animator> ();
		portal = GameObject.Find ("_LevelPortal").GetComponent<LevelPortalBehavior> ();
		//zoneCounter = PlayerPrefs.GetInt ("CurrentZone");
		zoneCounter = P31Prefs.getInt ("CurrentZone");

		//Difficulty Multipliers 
		zoneMultiplier = Mathf.RoundToInt(zoneCounter / 5);
		maxAmountOfMotherships = Random.Range(1 + zoneMultiplier, 3 + zoneMultiplier);
		if(maxAmountOfMotherships > 10){
			maxAmountOfMotherships = 10;
		}

		for(int x =1; x<= maxAmountOfMotherships; x++){
			timeUntillNextDangerLevel += (15 / (0.75f*x));
		}
	}

//Update Method --------------------------

	void Update(){
		mothershipsInPlay = GameObject.FindGameObjectsWithTag ("Mothership");
		shipsInPlay = GameObject.FindGameObjectsWithTag ("Enemy");

		CheckDangerLevel ();

		if(mothershipsInPlay.Length == 0 && !levelCompleted){
			LevelComplete ();
			levelCompleted = true;
		}
	}

//LevelComplete Method, methods which handles the processes of completing a zone

	void LevelComplete(){
		if(!tutorial){
			gameGenerator.ClearThreats ();						//Destroys all remaining fighters
		}
			
		zoneClearAnim.SetBool ("Leaving" , true);			//Turns on the zone clear prompt

		portal.levelComplete = true;

		zoneCounter++;   									// Increases the zone number
		P31Prefs.setInt("CurrentZone" , zoneCounter);



	}

//GameOver Method, method which handles to processes of losing the game

	public void GameOver(){
		StartCoroutine (GameOverProcess());
	}

	IEnumerator GameOverProcess(){
		yield return new WaitForSeconds (2f);
		transitionAnim.SetBool ("Leaving" , true);
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene ("GameOver");
		audioManager.Play ("PlayerLose");
	}

//CheckDangerLevel Method, checks to see if the alloted amount of time has passed before incrementing the danger level

	void CheckDangerLevel(){
		elapsedTime += Time.deltaTime;

		if(elapsedTime >= timeUntillNextDangerLevel){
			elapsedTime = 0;
			if(dangerLevel < 3){
				dangerLevel++;
			}
		}

		UpdateAmountOfShips ();
	}
		
//UpdateAmountOfShips Method, checks to see if the danger level has changed, then it increases the max amount of ships accordingly

	void UpdateAmountOfShips(){
		if(dangerLevel == 1){
			
			if (P31Prefs.getInt ("DifficultyNumber") == 0) {
				spawnRate = .7f;
				maxAmountOfShips = 2;
			} else {
				spawnRate = .6f;
				maxAmountOfShips = 3;
			}

		}
		if(dangerLevel == 2){

			if (P31Prefs.getInt ("DifficultyNumber") == 0) {
				spawnRate = .5f;
				maxAmountOfShips = 4;
			} else {
				spawnRate = .4f;
				maxAmountOfShips = 6;
			}

		}
		if(dangerLevel == 3){

			if (P31Prefs.getInt ("DifficultyNumber") == 0) {
				spawnRate = .3f;
				maxAmountOfShips = 6;
			} else {
				spawnRate = .2f;
				maxAmountOfShips = 9;
			}

		}
	}
}
