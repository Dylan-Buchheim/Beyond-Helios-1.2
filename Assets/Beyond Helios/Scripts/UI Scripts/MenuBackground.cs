using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour {

	private MeshRenderer mr;
	private Material mat;
	private Vector2 offset;

	void Start () {
		mr = GetComponent<MeshRenderer> ();
		mat = mr.material;
	}

	void Update () {
		offset = mat.mainTextureOffset;

		offset.x = 0;
		offset.y += Time.deltaTime * 0.3f;

		mat.mainTextureOffset = offset;
	}
}
