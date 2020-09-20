using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Prime31;

public class GemTracker : MonoBehaviour {

	Text text;

	void Start(){
		text = GetComponent<Text> ();
	}

	void Update(){
		text.text = P31Prefs.getInt ("GemCount").ToString();
	}
}
