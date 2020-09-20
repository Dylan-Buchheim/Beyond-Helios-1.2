using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParentRotation : MonoBehaviour {

	Quaternion rotation;
	Vector3 position;

	void Start(){
		rotation = transform.rotation;
		position = transform.position;
	}

	void LateUpdate(){
		transform.rotation = rotation;
		transform.position = position;
	}
}
