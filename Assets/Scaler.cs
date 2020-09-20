using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour {

	//float scaleFactor = 3.5f;

	void Start(){
		//if(PlayerPrefs.GetFloat("ZoomLevel") != 0){
			//scaleFactor = PlayerPrefs.GetFloat("ZoomLevel");
		//}
		//transform.localScale = new Vector3(scaleFactor/2 * (Screen.width/Screen.height),scaleFactor/2 * (Screen.width/Screen.height),scaleFactor/2 * (Screen.width/Screen.height)) * 9f;
		transform.localScale = new Vector3(transform.localScale.y * Screen.width / Screen.height,transform.localScale.y * Screen.width / Screen.height,0) * 0.6f;
	}
}
