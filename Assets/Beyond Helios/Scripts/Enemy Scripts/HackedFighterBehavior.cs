using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackedFighterBehavior : MonoBehaviour {

	public GameObject hackedFighter;
	public bool hacked = false;

	GameObject[] fighters;
	List<GameObject> targets = new List<GameObject>();

	public GameObject FindNewTarget(){
		GameObject target = null;
		fighters = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach(GameObject n in fighters){
			targets.Add (n);
		}
			
		if(targets.Count > 0){

			float currentClosest = 0;
			foreach (GameObject n in targets) {
				if(n != null){
					currentClosest = Vector3.Distance(transform.position , n.transform.position);
					target = n;
					break;
				}
			}

			foreach(GameObject n in targets){
				if(n != null){
					float distance = Vector3.Distance(transform.position , n.transform.position);
					if(distance < currentClosest){
						target = n;
						currentClosest = distance;
					}
				}
			}

			if(target != null){
				if(Vector3.Distance(transform.position , target.transform.position) > 15f){
					target = this.gameObject;
				}
			}
		}
		if(target == null){
			target = this.gameObject;
		}
		return target;
	}

	public void Hack(){
		if(!hacked){
			Instantiate (hackedFighter, transform.position, transform.rotation);
			Destroy (this.gameObject);
		}
	}
}
