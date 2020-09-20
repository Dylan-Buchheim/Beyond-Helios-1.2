using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

//Private Variables------------------------------------------------------------

	public float parralax = 2f;
	private MeshRenderer mr;
	private Material mat;
	private Vector2 offset;

//Start Method for variable initialization-------------------------------------

	void Start () {
		mr = GetComponent<MeshRenderer> ();
		mat = mr.material;
	}
	
//Update Method for scrolling the background based on the ships position-------

	void Update () {
		offset = mat.mainTextureOffset;

		offset.x = transform.position.x / transform.localScale.x / parralax;
		offset.y = transform.position.y / transform.localScale.y / parralax;

		mat.mainTextureOffset = offset;
	}
}
