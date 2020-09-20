using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPBlast : MonoBehaviour {

	public bool playSound = true;
	public bool shakeScreen = true;
	public float range = 5;

	private CameraShake cs;

	GameObject[] enemyFighters;
	public GameObject explosionFX;

	Properties screenShakeProperties;


	void Start(){
		cs = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();

		screenShakeProperties = new Properties ();
		screenShakeProperties.angle = 0;
		screenShakeProperties.strength = .5f;
		screenShakeProperties.speed = 10;
		screenShakeProperties.duration = .5f;
		screenShakeProperties.noisePercent = .35f;
		screenShakeProperties.dampingPercent = .15f;

		EMP ();
	}

	//EMP Processes -----------------------

	void EMP(){
		StartCoroutine (EmpProccess());
	}

	IEnumerator EmpProccess(){
		enemyFighters = GameObject.FindGameObjectsWithTag ("Enemy");
		if(playSound){
			FindObjectOfType<AudioManager> ().Play ("ExplosionBig");
		}

		yield return new WaitForSeconds (.75f);

		if(shakeScreen){
			cs.StartShake (screenShakeProperties);
		}

		foreach(GameObject n in enemyFighters){
			if(n != null){
				float distance = Vector3.Distance (transform.position , n.transform.position);
				if(distance <= range){
					Instantiate (explosionFX , n.transform.position , Quaternion.Euler(0,0,0));
					Destroy (n);
				}
			}
		}
	}
}
