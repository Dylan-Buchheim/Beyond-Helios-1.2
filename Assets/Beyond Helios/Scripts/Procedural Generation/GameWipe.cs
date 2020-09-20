using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWipe : MonoBehaviour {

	private GameObject[] enemies;
	private GameObject[] mothershipBullets;
	private GameObject[] enemyBullets;
	private GameObject[] frigates;

	public GameObject explosionFX;


	public void ClearMap(){
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		mothershipBullets = GameObject.FindGameObjectsWithTag ("mothershipBullet");
		enemyBullets = GameObject.FindGameObjectsWithTag ("enemyBullet");
		frigates = GameObject.FindGameObjectsWithTag ("Frigate");
	
		//Add list for collectables

		foreach (GameObject n in enemies){
			Instantiate (explosionFX , n.transform.position , n.transform.rotation );
			Destroy (n);
		}
		foreach (GameObject n in frigates){
			Instantiate (explosionFX , n.transform.position , n.transform.rotation );
			Destroy (n);
		}
		foreach (GameObject n in mothershipBullets){
			Destroy (n);
		}
		foreach (GameObject n in enemyBullets){
			Destroy (n);
		}

	}
}
