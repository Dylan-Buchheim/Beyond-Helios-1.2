using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class PickupManager : MonoBehaviour {

//Shield Variables ----------------

	public bool shielded;

	Animator shieldAnim;
	IEnumerator shieldProccess;
	Properties screenShakeProperties;

	private CameraShake cs;
	private AudioManager audioM;

//Life Pickup Variables -----------

	PlayerController2D playerController;

//EMP Variables -------------------

	public GameObject empFX;

	void Start(){
		shieldAnim = transform.Find ("PlayerShield").GetComponent<Animator>();
		playerController = GetComponent<PlayerController2D> ();

		cs = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();
		audioM = FindObjectOfType<AudioManager> ();

		screenShakeProperties = new Properties ();
		screenShakeProperties.angle = 0;
		screenShakeProperties.strength = .15f;
		screenShakeProperties.speed = 10;
		screenShakeProperties.duration = .15f;
		screenShakeProperties.noisePercent = .35f;
		screenShakeProperties.dampingPercent = .15f;

		if(P31Prefs.getInt("DifficultyNumber") == 0){
			ToggleShield ();
		}

	}

//Shield Processes ------------------

	public void ToggleShield(){
		if (shielded && shieldProccess == null) {
			shieldProccess = DestroyShield ();
			StartCoroutine (shieldProccess);
		} 

		else if(shieldProccess == null){
			shieldAnim.SetBool ("Shielded" , true);
			shielded = true;
		}
	}

	IEnumerator DestroyShield(){
		shieldAnim.SetBool ("Shielded" , false);
		cs.StartShake (screenShakeProperties);
		yield return new WaitForSeconds (0.5f);
		shielded = false;
		shieldProccess = null;
	}

//Collision Detection -----------------

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "PickupShield" && !shielded){
			ToggleShield ();
			audioM.Play ("PickupShield");
			Destroy (other.gameObject);
		}

		if(other.tag == "PickupLife" && playerController.lives < 5){
			playerController.lives++;
			audioM.Play ("PickupLife");
			Destroy (other.gameObject);
		}

		if(other.tag == "PickupEMP"){
			Instantiate (empFX , other.transform.position , Quaternion.Euler(0,0,0));
			Destroy (other.gameObject);
		}

		if(other.tag == "PickupMissile"){
			playerController.missileCount += 3;
			audioM.Play ("PickupMissile");
			Destroy (other.gameObject);
		}
	}
}
