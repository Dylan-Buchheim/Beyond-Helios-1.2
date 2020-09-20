using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComShipSpawner : MonoBehaviour {

	public ZoneCompleteController zcc;
	public GameObject[] ships;
	GameObject currentShip;


	void Start(){
		Vector3 spawnPoint = new Vector3 (transform.position.x , transform.position.y - 10 , transform.position.z);
		currentShip = Instantiate (ships[PlayerPrefs.GetInt("SelectedShip")] , spawnPoint , transform.rotation);
		currentShip.transform.parent = gameObject.transform;
		zcc.UpdateShip (currentShip);
	
	}
}
