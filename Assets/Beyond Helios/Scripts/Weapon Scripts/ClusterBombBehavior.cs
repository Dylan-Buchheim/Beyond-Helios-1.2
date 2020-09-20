using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBombBehavior : MonoBehaviour {

	//Variables-------------------------------

	public GameObject bomb;

	void Start(){
		StartCoroutine (SpawnCluster());
	}

	IEnumerator SpawnCluster(){
		yield return new WaitForSeconds (0.38f);
		Vector3 bombPosition = new Vector3 (transform.position.x,transform.position.y + 0.5f,transform.position.z);
		Instantiate (bomb , bombPosition, transform.rotation);
		bombPosition = new Vector3 (transform.position.x,transform.position.y - 0.5f,transform.position.z);
		Instantiate (bomb , bombPosition, transform.rotation);
		bombPosition = new Vector3 (transform.position.x + 0.5f,transform.position.y,transform.position.z);
		Instantiate (bomb , bombPosition, Quaternion.Euler(0,0,90));
		bombPosition = new Vector3 (transform.position.x - 0.5f,transform.position.y,transform.position.z);
		Instantiate (bomb , bombPosition, Quaternion.Euler(0,0,90));

		yield return new WaitForSeconds (3f);
		Destroy (this.gameObject);
	}


}
