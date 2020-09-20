using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDetection : MonoBehaviour {

	private bool connected = false;
	private bool previousStatus = false;
	public GameObject joystick;
	public GameObject button1;
	public GameObject button2;

	void Awake() {
		StartCoroutine(CheckForControllers());
	}

	void Update(){
		if (connected != previousStatus) {
			ToggleUI ();
			previousStatus = connected;
		}
	}

	IEnumerator CheckForControllers() {
		while (true) {
			var controllers = Input.GetJoystickNames();
			if (!connected && controllers.Length > 0) {
				connected = true;
				Debug.Log("Connected");
			} else if (connected && controllers.Length == 0) {
				connected = false;
				Debug.Log("Disconnected");
			}
			yield return new WaitForSeconds(1f);
		}
	}

	void ToggleUI(){
		joystick.SetActive (!joystick.activeSelf);
		button1.SetActive (!button1.activeSelf);
		button2.SetActive (!button2.activeSelf);
	}


}
