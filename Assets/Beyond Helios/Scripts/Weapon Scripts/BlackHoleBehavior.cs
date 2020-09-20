using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBehavior : MonoBehaviour {

	private GameObject[] enemyFighters;
	bool sucking;
	bool dying = false;
	bool creating = true;
	List<GameObject> suckedFighters = new List<GameObject>();

	public int upTime = 2;
	public int power = 3;
	public float range;

	// Use this for initialization
	void Start () {
		BlackHole ();
	}

	void FixedUpdate(){
		if(creating){
			CreateBlackHole ();
		}
		if(sucking){
			Suck ();
		}
		if(dying){
			DestroyBlackHole ();
		}

		transform.Rotate (Vector3.forward * Time.deltaTime * 250);
	}
	
	void BlackHole(){
		StartCoroutine (SuckAndExplode ());
	}



	void Suck(){
		enemyFighters = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject n in enemyFighters){
			if(n != null){
				float distance = Vector3.Distance (transform.position , n.transform.position);
				if(distance <= range){
					suckedFighters.Add (n);
				}
			}
		}
		foreach(GameObject n in suckedFighters){
			if(n != null){
				Vector3 difference = n.transform.position - transform.position;
				Rigidbody2D rb2d = n.GetComponent<Rigidbody2D> ();
				rb2d.AddForce(-difference.normalized * power, ForceMode2D.Force);
			}
		}
	}

	IEnumerator SuckAndExplode(){
		sucking = true;
		yield return new WaitForSeconds (upTime);
		sucking = false;
		dying = true;
	}

	float count = 1;
	void DestroyBlackHole(){
		if (count > 0) {
			count -= 0.05f;
			transform.localScale = new Vector3 (1 * count, 1 * count, 1 * count);
		} else {
			Destroy (this.gameObject);
		}
	}
	float createCount = 0.1f;
	void CreateBlackHole(){
		if (createCount < 1) {
			createCount += 0.05f;
			transform.localScale = new Vector3 (1 * createCount, 1 * createCount, 1 * createCount);
		} else {
			creating = false;
		} 
	}
}
