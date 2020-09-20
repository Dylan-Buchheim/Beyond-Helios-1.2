using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

public class AttractiveBombBehavior : MonoBehaviour {

	//Variables-------------------------------

	private AudioSource am;
	private GameObject target;
	private GameObject[] enemyFighters;
	private LineRenderer lineRender;

	public float range = 2;
	public float fuseTime = 3;
	public float movementSmoothing = 1;
	public GameObject defaultExplosion;
	public GameObject[] customExplosions;
	public List<GameObject> explosions = new List<GameObject>();


	//Start Method----------------------------

	void Start(){
		am = GetComponent<AudioSource> ();
		lineRender = GetComponentInChildren<LineRenderer> ();
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
			lineRender.SetPosition (0,transform.position);
			lineRender.SetPosition (1,transform.position);
		}else{
			transform.position = Vector3.Lerp (transform.position , target.transform.position , movementSmoothing * Time.deltaTime);
			lineRender.SetPosition (0,transform.position);
			lineRender.SetPosition (1,target.transform.position);
			if(!am.isPlaying){
				am.Play ();
			}
		}
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
