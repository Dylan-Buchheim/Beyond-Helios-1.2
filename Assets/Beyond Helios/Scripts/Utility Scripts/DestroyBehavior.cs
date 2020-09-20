using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBehavior : MonoBehaviour {

	public float upTime;
	
	void Update () {
		upTime -= Time.deltaTime;

		if(upTime < 0){
			Destroy (this.gameObject);
		}
	}
}
