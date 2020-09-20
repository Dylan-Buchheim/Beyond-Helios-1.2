using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class PlayerController2D : MonoBehaviour {

// Public Variables---------------------------------------------------------------

	public bool shotgunFire = false;
	public bool laserFire = false;
	public int lives = 3;
	public int missileCount = 0;
	public int playerSpeed = 20;
	public int turnSpeed = 1;
	public float bombCooldown = 1;
	public bool destroyed = false;
	public bool leaving = false;
	public GameObject defaultLaser;
	public GameObject defaultBomb;
	public GameObject defaultMissile;
	public GameObject explosionFX;
	public ParticleSystem[] trails;
	public ParticleSystem.EmissionModule[] playerTrails;

	public VirtualJoystick joystick;


// Private Variables--------------------------------------------------------------

	private ContinuousLaserBehavior laser;
	private bool bombReady = true;
	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	private int targetAngle = 0;
	private Transform firingPoint1;
	private Transform firingPoint2;
	private GameObject spawnPoint;

	private GameManager gameManager;
	private PickupManager pickupManager;
	private AudioManager audioManager;

	IEnumerator slowCycle;


// Start Method-------------------------------------------------------------------

	void Start () {
		//Instantiation-------------
		if (laserFire) {
			laser = GetComponent<ContinuousLaserBehavior> ();
		} else {
			laser = null;
		}

		rb2d = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		gameManager = GameObject.Find ("_GameManager").GetComponent<GameManager> ();
		pickupManager = GetComponent<PickupManager> ();
		audioManager = FindObjectOfType<AudioManager> ();
		joystick = GameObject.FindGameObjectWithTag ("Joystick").GetComponent<VirtualJoystick> ();
		firingPoint1 = transform.Find ("FiringPoint_1");
		firingPoint2 = transform.Find ("FiringPoint_2");
		spawnPoint = GameObject.FindGameObjectWithTag ("PlayerSpawnPoint");
		//playerTrail = trail.emission;
		playerTrails = new ParticleSystem.EmissionModule[trails.Length];
		for(int x = 0; x < trails.Length; x++){
			playerTrails [x] = trails [x].emission;
		}

	}
	
// Update Method------------------------------------------------------------------

	void Update () {
		if((Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown("Fire1")) && !destroyed){
			defaultFire ();
		}
		if((Input.GetKeyDown (KeyCode.LeftShift) || Input.GetButtonDown("Fire2")) && bombReady && !destroyed){
			createBomb ();
		}
	}

// Fixed Update Method------------------------------------------------------------

	void FixedUpdate(){
		
		//Forward Movement----------
		if (!destroyed && !leaving) {
			rb2d.velocity = (transform.up * playerSpeed);
		}
		if(destroyed){
			rb2d.velocity = (transform.up * 0);
		}
		if(leaving){
			rb2d.velocity = (transform.up * playerSpeed * 2);
		}
			
		//Player Turning------------

		if (joystick.joystickInUse) {
			targetAngle = -(int)(Mathf.Atan2 (joystick.Horizontal (), joystick.Vertical ()) * Mathf.Rad2Deg);
		} else {
			targetAngle = -(int)(Mathf.Atan2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg);
		}
			
		if(!leaving){
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);
		}

	}

//Fire Laserbeam Method, fires the default double laser cannon--------------------

	public void defaultFire(){
		if(!destroyed && missileCount > 0){
			Instantiate (defaultMissile , transform.position , transform.rotation);
			missileCount--;
		}else if(!destroyed){
			if(!shotgunFire && !laserFire){
				audioManager.Play ("LaserPlayer");
				Instantiate (defaultLaser, firingPoint1.position,firingPoint1.transform.rotation);
				Instantiate (defaultLaser, firingPoint2.position,firingPoint2.transform.rotation);
			}
			if(shotgunFire){
				audioManager.Play ("LaserPlayer");
				Instantiate (defaultLaser, firingPoint1.position,firingPoint1.transform.rotation);
				Instantiate (defaultLaser, firingPoint2.position,firingPoint2.transform.rotation);
				Instantiate (defaultLaser, firingPoint1.position,firingPoint1.transform.rotation * Quaternion.Euler(0,0,2.5f));
				Instantiate (defaultLaser, firingPoint2.position,firingPoint2.transform.rotation * Quaternion.Euler(0,0,-2.5f));
			}
			if(laserFire){
				laser.StartLaserCycle ();
			}
		}
	}

//Deploy Singularity Bomb, drops a singularity bomb behind the player-------------

	IEnumerator bombDrop(){
		Instantiate (defaultBomb, transform.position - (transform.up * 0.1f), Quaternion.Euler(0,0,0));
		yield return new WaitForSeconds (bombCooldown);
		bombReady = true;
	}

	public void createBomb(){
		if(bombReady && !destroyed){
			bombReady = false;	
			StartCoroutine (bombDrop());
		}
	}

//Collision Detection ------------------------------------------------------------

	void OnCollisionEnter2D(Collision2D other){
		if(!destroyed && pickupManager.shielded){
			pickupManager.ToggleShield ();
			DestroyPlayer ();
		}
		else if(!destroyed){
			DestroyPlayer ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Equals("enemyBullet") || other.tag.Equals("mothershipBullet") || other.tag.Equals("humanBullet") && !destroyed){

			if (pickupManager.shielded) {
				pickupManager.ToggleShield ();
			} else {
				DestroyPlayer ();
			}

		}
		if(other.tag.Equals("enemySlow") && slowCycle == null){
			slowCycle = Slow ();
			StartCoroutine (slowCycle);
		}

		if(other.tag.Equals("MothershipLaser") && !destroyed){
			MothershipLaser laserController = other.gameObject.GetComponent<MothershipLaser> ();
			if(laserController.laserOn){
				if (pickupManager.shielded) {
					pickupManager.ToggleShield ();
					DestroyPlayer ();
				} else {
					DestroyPlayer ();
				}
			}
		}
	}


	IEnumerator Slow(){
		playerSpeed -= 2;
		yield return new WaitForSeconds (1);
		playerSpeed += 2;
		slowCycle = null;
	}

//Destroy Method, handles the processes of destroying the player------------------

	public void DestroyPlayer(){
		destroyed = true;
		for(int x = 0; x < playerTrails.Length; x++){
			playerTrails [x].enabled = false;
		}
		sr.enabled = false;
		Instantiate (explosionFX , transform.position , transform.rotation);
		transform.position = spawnPoint.transform.position;
		rb2d.angularVelocity = 0;
		transform.rotation = Quaternion.Euler (0 , 0 , 0);
		if(respawnCycle == null){
			respawnCycle = Respawn ();
			StartCoroutine (respawnCycle);
		}
	}

	IEnumerator respawnCycle;

	IEnumerator Respawn(){
		yield return new WaitForSeconds (1.6f);
		lives--;
		if (lives >= 0) {
			targetAngle = 0;
			destroyed = false;
			sr.enabled = true;
			for(int x = 0; x < playerTrails.Length; x++){
				playerTrails [x].enabled = true;
			}
			if(P31Prefs.getInt("DifficultyNumber") == 0){
				pickupManager.ToggleShield ();
			}
		} else {
			gameManager.GameOver ();
		}
		respawnCycle = null;
	}
}
