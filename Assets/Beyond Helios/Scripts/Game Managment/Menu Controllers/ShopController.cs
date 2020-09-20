using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Prime31;

public class ShopController : MonoBehaviour {

	Animator transition;
	AudioManager am;

	public GameObject ui;
	public ShopItemSpawner itemSpawner;
	public int gemCount;

	bool shipSelected;
	GameObject ship;
	int shipNumber;
	int cost;

	void Start(){
		transition = GameObject.Find ("Transition").GetComponent<Animator> ();
		gemCount = P31Prefs.getInt ("GemCount");
		am = FindObjectOfType<AudioManager> ();
	}

	public void PlaySelectSound(){
		am.Play ("Select1");
	}

	public void SetItem(GameObject _ship){
		ship = _ship;
	}
	public void SetShipNumber(int _shipNumber){
		shipNumber = _shipNumber;
	}
	public void SetCost(int _cost){
		cost = _cost;
	}

	public void SelectShip(){
		if(P31Prefs.getInt("ShipUnlock" + shipNumber) == 0 && gemCount >= cost ){
			am.Play ("Select1");
			shipSelected = true;
			ToggleConfirmScreen ();
			itemSpawner.SpawnItem (ship);
		}
	}

	public void SelectExplosion(){
		if(P31Prefs.getInt("ExplosionUnlock" + shipNumber) == 0 && gemCount >= cost){
			am.Play ("Select1");
			shipSelected = false;
			ToggleConfirmScreen ();
			itemSpawner.SpawnItem (ship);
		}
	}

	public void UnlockShip(){
		if (shipSelected) {

			if (P31Prefs.getInt ("ShipUnlock" + shipNumber) == 0 && gemCount >= cost) {
				P31Prefs.setInt ("ShipUnlock" + shipNumber, 1);
				gemCount -= cost;
				P31Prefs.setInt ("GemCount", gemCount);
				am.Play ("Select1");
				ToggleConfirmScreen ();
			}
		
		} else {
		
			if(P31Prefs.getInt("ExplosionUnlock" + shipNumber) == 0 && gemCount >= cost ){
				P31Prefs.setInt ("ExplosionUnlock" + shipNumber , 1);
				gemCount -= cost;
				P31Prefs.setInt ("GemCount" , gemCount);
				am.Play ("Select1");
				ToggleConfirmScreen();
			}
		
		}
			
	}
		
	public void CancelPurchase(){
		am.Play ("Select2");
		ToggleConfirmScreen ();
	}


	void ToggleConfirmScreen(){
		ui.SetActive (!ui.activeSelf);
	}





	//Return To Main Menu Funtion -------------

	public void ReturnToMain(){
		StartCoroutine (ReturnProcess());
	}

	IEnumerator ReturnProcess(){
		transition.SetBool ("Leaving" , true);
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene ("MainMenu");
	}
}
