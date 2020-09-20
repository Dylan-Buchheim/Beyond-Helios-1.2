using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingComponent : MonoBehaviour {

	EnemyFighterBehavior EFB;
	public float fuseTime = 3;

	void Start(){
		EFB = GetComponentInParent<EnemyFighterBehavior> ();
	}

	void Update(){
		fuseTime -= Time.deltaTime;

		if(fuseTime <0){
			EFB.DestroyShip ();
		}
	}
}
