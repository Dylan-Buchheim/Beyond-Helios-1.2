using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

public class ShipSelector : MonoBehaviour {

	public MainMenuController mmc;
	public GameObject[] allShips;
	public GameObject[] ships;
	public int[] shipNumbers;

	public int currentShipNumber = 0;
	public GameObject currentShip;
	bool changing = false;

	Vector3 spawnPoint;

	void Start(){
		P31Prefs.setInt ("ShipUnlock0" , 1);
		CheckShipList ();

		spawnPoint = new Vector3 (transform.position.x , transform.position.y - 10 , transform.position.z);
		for(int x =0; x < shipNumbers.Length; x++){
			if(shipNumbers[x] == P31Prefs.getInt ("SelectedShip")){
				currentShipNumber = x;
			}
		}
		currentShip = Instantiate (ships[currentShipNumber] , spawnPoint , transform.rotation);
		currentShip.transform.parent = gameObject.transform;
		mmc.UpdateShip (currentShip);
	}

	void CheckShipList(){
		int numberOfUnlockedShips = 0;
		for(int x = 0; x < allShips.Length; x++){
			if(P31Prefs.getInt("ShipUnlock" + x) == 1){
				numberOfUnlockedShips++;
			}
		}

		ships = new GameObject[numberOfUnlockedShips];
		shipNumbers = new int[numberOfUnlockedShips];

		int index = 0;
		for(int x = 0; x < allShips.Length; x++){
			if(P31Prefs.getInt("ShipUnlock" + x) == 1){
				ships [index] = allShips [x];
				shipNumbers [index] = x;
				index++;
			}
		}

	}


//Logic for changing your selected ship --------------------------
	public void ForwardShip(){
		if(!changing && ships.Length > 1){
			changing = true;
			StartCoroutine (ForwardMove());
		}
	}

	IEnumerator ForwardMove(){
		if (currentShipNumber < ships.Length - 1) {
			currentShipNumber++;
			P31Prefs.setInt ("SelectedShip" , shipNumbers[currentShipNumber]);
		} else {
			currentShipNumber = 0;
			P31Prefs.setInt ("SelectedShip" , shipNumbers[currentShipNumber]);
		}
		currentShip.SendMessage ("StartMoving");

		currentShip = Instantiate (ships[currentShipNumber] , spawnPoint , transform.rotation);
		currentShip.transform.parent = gameObject.transform;
		mmc.UpdateShip (currentShip);

		yield return new WaitForSeconds (1);
		changing = false;
	}

	public void BackShip(){
		if(!changing && ships.Length > 1){
			changing = true;
			StartCoroutine (BackMove());
		}
	}

	IEnumerator BackMove(){
		if (currentShipNumber > 0) {
			currentShipNumber--;
			P31Prefs.setInt ("SelectedShip" , shipNumbers[currentShipNumber]);
		} else {
			currentShipNumber = ships.Length - 1;
			P31Prefs.setInt ("SelectedShip" , shipNumbers[currentShipNumber]);
		}
		currentShip.SendMessage ("StartMoving");

		currentShip = Instantiate (ships[currentShipNumber] , spawnPoint , transform.rotation);
		currentShip.transform.parent = gameObject.transform;
		mmc.UpdateShip (currentShip);

		yield return new WaitForSeconds (1);
		changing = false;
	}
}
