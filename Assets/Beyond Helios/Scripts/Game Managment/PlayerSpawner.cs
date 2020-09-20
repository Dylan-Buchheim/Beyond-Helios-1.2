using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	public GameObject[] playerShips;
	public GameObject playerSpawnPoint;

	void Awake(){
		playerSpawnPoint = GameObject.FindGameObjectWithTag ("PlayerSpawnPoint");
		Instantiate (playerShips[PlayerPrefs.GetInt("SelectedShip")] , playerSpawnPoint.transform.position , playerSpawnPoint.transform.rotation);
	}
}
