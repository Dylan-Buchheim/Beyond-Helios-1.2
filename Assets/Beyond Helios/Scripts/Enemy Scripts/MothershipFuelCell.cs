using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipFuelCell : MonoBehaviour {

	//Variables ---------------------------------

	public Sprite normalSprite;
	public Sprite destroyedSprite;

	public GameObject explosionFX;

	public bool destroyed = false;

	private SpriteRenderer sr;
	private ScoreKeeper scoreKeep;


	//Start Method ------------------------------

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		scoreKeep = GameObject.Find ("_ScoreKeeper").GetComponent<ScoreKeeper> ();
	}

	//Destroy Method ----------------------------

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "playerBullet" && !destroyed){
			DestroyGun ();
		}
		if(other.gameObject.tag == "humanBullet" && !destroyed){
			DestroyGun ();
		}
	}

	public void DestroyGun(){
		destroyed = true;
		gameObject.tag = "Untagged";
		scoreKeep.AddScore (50);
		Instantiate (explosionFX , transform.position , transform.rotation);
		sr.sprite = destroyedSprite;
	}

}
