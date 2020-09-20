using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ControlFlipping : MonoBehaviour {

	RectTransform rect;

	public float posX , posY;
	public float anchorPoint;

	void Start(){
		rect = GetComponent<RectTransform> ();
		posX = rect.anchoredPosition.x;
		posY = rect.anchoredPosition.y;
		anchorPoint = rect.anchorMin.x;
		if (PlayerPrefs.GetInt ("ControlPosition") == 1) {
			SwapControl ();
		}
		if(PlayerPrefs.GetFloat("GUIScale") != 0){
			SetScale (PlayerPrefs.GetFloat("GUIScale"));
		}

	}

	public void SwapControl(){
		if (anchorPoint == 0) {
			anchorPoint = 1;
		} else {
			anchorPoint = 0;
		}
		posX = -posX;
		rect.anchorMin = new Vector2 (anchorPoint,0);
		rect.anchorMax = new Vector2 (anchorPoint,0);
		rect.pivot = new Vector2 (anchorPoint,0);
		rect.anchoredPosition = new Vector2 (posX , posY);

	}

	public void SetScale(float scaleNumber){
		rect.localScale = new Vector3 (scaleNumber , scaleNumber , scaleNumber);
	}
}
