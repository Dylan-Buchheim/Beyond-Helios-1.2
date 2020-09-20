using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

public class BlackHoleBombBehavior : MonoBehaviour {

	//Variables-------------------------------
	public GameObject blackHoleObject;

	public float fuseTime = 3;
	public GameObject defaultExplosion;
	public GameObject[] customExplosions;
	public List<GameObject> explosions = new List<GameObject>();

	//Start Method----------------------------

	void Start(){

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
			BlackHole ();
		}

		transform.Rotate (Vector3.forward * Time.deltaTime * 350);
	}



	//Explode Method, method that handles the processes of exploding the bomb

	void BlackHole(){
		Instantiate (blackHoleObject, transform.position, transform.rotation);
		Explode ();
	}

	void Explode(){
		int random = Random.Range (0 , explosions.Count);
		Instantiate (explosions[random], transform.position, Quaternion.Euler(0,0,0));
		Destroy (this.gameObject);
	}

	//OnTriggerEnter Method, handles what happens when something collides with the bomb

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.layer == 9 || other.gameObject.layer == 14){
			BlackHole ();
		}
	}
}
