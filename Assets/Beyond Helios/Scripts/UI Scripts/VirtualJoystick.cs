using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour , IDragHandler , IPointerUpHandler , IPointerDownHandler {

	private Image bgImage;
	private Image joystickImage;
	private Vector3 inputVector;

	public bool joystickInUse;

	private void Start(){
		bgImage = GetComponent<Image> ();
		joystickImage = transform.GetChild (0).GetComponent<Image> ();

	}
		
	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos;
		int addedValue = -1;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImage.rectTransform, ped.position, ped.pressEventCamera , out pos)) {

			if (PlayerPrefs.GetInt ("ControlPosition") == 1) {
				addedValue = 1;
			} else {
				addedValue = -1;
			}
			pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x * 2 + addedValue , pos.y * 2 -1, 0); //took out the -1 on each
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			//Move Joystick Image
			joystickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x/2) , inputVector.y * (bgImage.rectTransform.sizeDelta.y/2));
		}
	}

	public virtual void OnPointerDown(PointerEventData ped){
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped){
		inputVector = Vector3.zero;
		joystickImage.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float Horizontal(){
		if (inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis ("Horizontal");
	}

	public float Vertical(){
		if (inputVector.y != 0)
			return inputVector.y;
		else
			return Input.GetAxis ("Vertical");
	}

	void Update(){
		if (inputVector.y != 0 || inputVector.x != 0) {
			joystickInUse = true;
		} else {
			joystickInUse = false;
		}
	}


}
