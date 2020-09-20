using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizePlanet : MonoBehaviour {

	public Sprite[] planets;
	private SpriteRenderer sr;

	void Start(){
		sr = GetComponent<SpriteRenderer> ();
		sr.sprite = planets[Random.Range(0,planets.Length)];
	}
}
