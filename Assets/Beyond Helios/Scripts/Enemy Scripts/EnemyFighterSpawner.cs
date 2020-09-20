using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterSpawner : MonoBehaviour {

	public float maxDistance = 9f;
	private GameObject player;
	public bool inRange = false;

	public GameObject[] fighters;
	public GameObject gleam;
	public float radiusForSpawning;
	public int random;
	private GameManager gameManager;
	IEnumerator currentRandomCycle;

	void Start(){
		gameManager = GameObject.Find ("_GameManager").GetComponent<GameManager> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}


	void Update(){
		float distance = Vector3.Distance (player.transform.position , transform.position);
		if (distance < maxDistance) {
			inRange = true;
		} else {
			inRange = false;
		}
		if(gameManager.shipsInPlay.Length < gameManager.maxAmountOfShips && currentRandomCycle == null && inRange){
			currentRandomCycle = RandomSpawn ();
			StartCoroutine (currentRandomCycle);
		}
	}

	IEnumerator RandomSpawn(){
		random = Random.Range (0, 8);
		if(random == 5){
			random = 0;
			Vector3 pos = RandomCircle (transform.position , radiusForSpawning);
			Quaternion rot = Quaternion.FromToRotation (Vector3.up , pos-transform.position);
			Instantiate (gleam , pos , rot);
			yield return new WaitForSeconds (0.417f);
			Instantiate (fighters[Random.Range(0,fighters.Length)] , pos , rot);
		}
		yield return new WaitForSeconds (gameManager.spawnRate);
		currentRandomCycle = null;
	}

	Vector3 RandomCircle(Vector3 center , float radius){
		float angle = Random.value * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin (angle * Mathf.Deg2Rad);
		pos.y = center.y + radius * Mathf.Cos (angle * Mathf.Deg2Rad);
		pos.z = center.z;
		return pos;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position,maxDistance);
	}
}
