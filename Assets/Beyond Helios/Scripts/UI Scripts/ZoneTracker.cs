using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using Prime31;

public class ZoneTracker : MonoBehaviour {

	private Text text;


	void Start(){
		text = GetComponent<Text> ();
		text.text = ("Zone " + P31Prefs.getInt("CurrentZone"));
	}



}
