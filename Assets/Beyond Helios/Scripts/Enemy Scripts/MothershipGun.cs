using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipGun : MonoBehaviour {

//Variables ---------------------------------

	public Sprite normalSprite;
	public Sprite chargedSprite;
	public Animator supportAnim;
	public GameObject firingPoint1;
	public GameObject firingPoint2;
	public GameObject mothershipLaser;
	public GameObject explosionFX;
	public Sprite destroyedSprite;
	public bool inLineOfSight = false;
	public bool destroyed = false;
	public float cooldown = 1;
	public float chargeTime = 1;

	private float distanceToPlayer;
	private bool readyToFire = true;
	private SpriteRenderer sr;
	private GameObject Player;
	private ScoreKeeper scoreKeep;
	private AudioManager am;

//Start Method ------------------------------

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		Player = GameObject.FindGameObjectWithTag ("Player");
		scoreKeep = GameObject.Find ("_ScoreKeeper").GetComponent<ScoreKeeper> ();
		am = FindObjectOfType<AudioManager> ();
		supportAnim = transform.Find ("Mothership_Support").GetComponent<Animator>();
	}
	
//Update Method -----------------------------

	void Update () {
		//Calculations for distance to player 
		float diffX = Player.transform.position.x - transform.position.x;
		float diffY = Player.transform.position.y - transform.position.y;
		distanceToPlayer = Mathf.Sqrt (Mathf.Pow(diffX , 2) + Mathf.Pow(diffY , 2));

		//Check to see if the gun is ready to fire
		if (distanceToPlayer < 3.5f && inLineOfSight && readyToFire && !destroyed) {
			StartCoroutine ("Fire");
		}
	}

//Fire Method -------------------------------

	IEnumerator Fire (){
		readyToFire = false;
		sr.sprite = chargedSprite;
		supportAnim.SetBool ("Charged" , true);

		yield return new WaitForSeconds (chargeTime);

		if (destroyed) {
			sr.sprite = destroyedSprite;
			supportAnim.SetBool ("Charged" , false);
		} else {
			createBullet ();
			sr.sprite = normalSprite;
			supportAnim.SetBool ("Charged" , false);
		}

		yield return new WaitForSeconds (cooldown);

		readyToFire = true;
	}

	void createBullet(){
		am.Play ("LaserBig");
		Instantiate (mothershipLaser, firingPoint1.transform.position, transform.rotation);
		Instantiate (mothershipLaser, firingPoint2.transform.position, transform.rotation);
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
