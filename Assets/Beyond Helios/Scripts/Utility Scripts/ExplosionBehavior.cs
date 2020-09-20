using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour {

	public float shakeDelay = 0;

	public Properties screenShakeProperties;

	private CameraShake cs;

	void Start(){
		cs = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();
		StartCoroutine (screenShake());
	}

	IEnumerator screenShake(){
		if(shakeDelay > 0){
			FindObjectOfType<AudioManager> ().Play ("ExplosionBig");
		}
		yield return new WaitForSeconds (shakeDelay);

		cs.StartShake (screenShakeProperties);
	}
}
