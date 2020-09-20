using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

public class TurretBombBehavior : MonoBehaviour {

	//Variables-------------------------------

	private AudioManager am;

	private GameObject target;
	private GameObject[] enemyFighters;
	private IEnumerator fireCycle = null;

	public float range = 2;
	public float fuseTime = 3;
	public GameObject bullet;
	public GameObject defaultExplosion;
	public GameObject[] customExplosions;
	public List<GameObject> explosions = new List<GameObject>();

	//Start Method----------------------------

	void Start(){
		am = FindObjectOfType<AudioManager> ();


		for(int x = 0; x < customExplosions.Length; x++){
			if(P31Prefs.getInt("ExplosionUnlock" + (x+1)) == 1){
				explosions.Add (customExplosions[x]);
			}
		}

		explosions.Add (defaultExplosion);
	}

	//Update Method----------------------------

	void Update(){
		fuseTime -= Time.deltaTime;

		if(fuseTime <0){
			Explode ();
		}

		transform.Rotate (Vector3.forward * Time.deltaTime * 250);

		if(target == null){
			CheckArea ();
		}else{
			AimTurret ();
			if(fireCycle == null){
				fireCycle = FiringCycle ();
				StartCoroutine (fireCycle);
			}
		}
	}

	void AimTurret(){
		Vector3 diff = (target.transform.position + (target.transform.up * 0.5f)) - transform.position;
		diff.Normalize ();
		float gunTRAngle = (Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg) - 90;
		transform.rotation = Quaternion.Euler(0f , 0f , gunTRAngle);
	}

	void FireGun(){
		Instantiate (bullet , transform.position + transform.up * 0.25f , transform.rotation);
		am.Play ("LaserSmall");
	}

	IEnumerator FiringCycle(){
		FireGun ();
		yield return new WaitForSeconds (0.35f);
		fireCycle = null;
	}

	//Check Area Method
	void CheckArea(){
		enemyFighters = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach(GameObject n in enemyFighters){
			if(n != null){
				float distance = Vector3.Distance (transform.position , n.transform.position);
				if(distance <= range){
					target = n;
					break;
				}
			}
		}
	}

	//Explode Method, method that handles the processes of exploding the bomb

	void Explode(){
		int random = Random.Range (0 , explosions.Count);
		Instantiate (explosions[random], transform.position, Quaternion.Euler(0,0,0));
		Destroy (this.gameObject);
	}

	//OnTriggerEnter Method, handles what happens when something collides with the bomb

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.layer == 9 || other.gameObject.layer == 14){
			Explode ();
		}
	}
}
