using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterBehavior : MonoBehaviour {

//Variables ---------------------------------------

	public bool hacked = false;

	public Sprite normalSprite;
	public Sprite chargedSprite;

	private SpriteRenderer sr;
	private AudioManager am;

	public bool human = false;
	public bool doubleShot;
	public float range;
	public float cooldown;
	public float chargeTime = 1;
	public LayerMask shootableLayer;
	public LayerMask hackedLayer;

	public GameObject laser;
	public GameObject explosionFX;
	private Transform firingPoint_1;
	private Transform firingPoint_2;
	private ScoreKeeper scoreKeep;

	public bool destroyed = false;
	IEnumerator currentFireCycle;


//Start Method, used for initiallization

	void Start(){
		sr = GetComponent<SpriteRenderer> ();
		am = FindObjectOfType<AudioManager> ();
		scoreKeep = GameObject.Find ("_ScoreKeeper").GetComponent<ScoreKeeper> ();
		firingPoint_1 = transform.Find ("FiringPoint_1");
		if (doubleShot) {
			firingPoint_2 = transform.Find ("FiringPoint_2");
		} else {
			firingPoint_2 = transform.Find ("FiringPoint_1");
		}
	}

//FixedUpdate Method for physics calculations

	void FixedUpdate(){
		if (hacked) {
			if (Physics2D.Raycast (transform.position + transform.up * .3f, transform.up, range , hackedLayer)) {
				if(currentFireCycle == null){
					currentFireCycle = FireWeapons();
					StartCoroutine (currentFireCycle);
				}
			}
		} else {
			if (Physics2D.Raycast (transform.position + transform.up * .3f, transform.up, range , shootableLayer)) {
				if(currentFireCycle == null){
					currentFireCycle = FireWeapons();
					StartCoroutine (currentFireCycle);
				}
			}
		}

		Debug.DrawRay (transform.position + transform.up * .3f, transform.up * range , Color.green);
	}

//FireWeapons Coroutine, handles firing the fighters weapons once all the conditions are met

	IEnumerator FireWeapons(){
		sr.sprite = chargedSprite;
		yield return new WaitForSeconds (chargeTime);
		sr.sprite = normalSprite;
		am.Play ("LaserSmall");
		Instantiate (laser, firingPoint_1.position, transform.rotation);
		if(doubleShot){
			Instantiate (laser, firingPoint_2.transform.position, transform.rotation);
		}

		yield return new WaitForSeconds (.1f);
		am.Play ("LaserSmall");
		Instantiate (laser, firingPoint_1.position, transform.rotation);
		if(doubleShot){
			Instantiate (laser, firingPoint_2.transform.position, transform.rotation);
		}

		yield return new WaitForSeconds (.1f);
		am.Play ("LaserSmall");
		Instantiate (laser, firingPoint_1.position, transform.rotation);
		if(doubleShot){
			Instantiate (laser, firingPoint_2.transform.position, transform.rotation);
		}

		yield return new WaitForSeconds (cooldown);
		currentFireCycle = null;
		yield break;
	}

//Collision Detection, all methods that handle collisions for the ship

	void OnCollisionEnter2D(Collision2D other){
		if (!destroyed && !other.gameObject.CompareTag("Enemy") && !hacked) {
			StartCoroutine (beginDestroying());
		}else if(!destroyed && hacked){
			StartCoroutine (beginDestroying());
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if(human){
			if (hacked) {
				if(other.tag.Equals("mothershipBullet") || other.tag.Equals("enemyBullet")  && !destroyed){
					StartCoroutine (beginDestroying());
				}
			} else {
				if(other.tag.Equals("playerBullet") || other.tag.Equals("mothershipBullet") || other.tag.Equals("enemyBullet")  && !destroyed){
					StartCoroutine (beginDestroying());
				}
			}

		}
		if(!human){
			if (hacked) {
				if(other.tag.Equals("mothershipBullet") || other.tag.Equals("humanBullet")  && !destroyed){
					StartCoroutine (beginDestroying());
				}
			} else {
				if(other.tag.Equals("playerBullet") || other.tag.Equals("mothershipBullet") || other.tag.Equals("humanBullet")  && !destroyed){
					StartCoroutine (beginDestroying());
				}
			}

		}

		if(other.tag.Equals("MothershipLaser") && !destroyed){
			MothershipLaser laserController = other.gameObject.GetComponent<MothershipLaser> ();
			if(laserController.laserOn){
				StartCoroutine (beginDestroying());
			}
		}

	}

//DestroyShip Method, handles the proccesses of death

	IEnumerator beginDestroying(){
		destroyed = true;
		//yield return new WaitForSeconds (.1f);
		DestroyShip ();
		yield break;
	}

	public void DestroyShip(){
		scoreKeep.AddScore (25);
		Instantiate (explosionFX , transform.position , transform.rotation);
		Destroy (this.gameObject);
	}
}
