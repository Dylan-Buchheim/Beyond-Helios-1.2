using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigurationController : MonoBehaviour {

	public ControlFlipping virtualJoystick , laserBtn , bombBtn;
	public Camera cam;
	public Animator transition;
	AudioManager am;

	void Start(){
		am = FindObjectOfType<AudioManager> ();
	}

	public void PlaySelectSound(){
		am.Play ("Select1");
	}

	public void SwapControls(){
		virtualJoystick.SwapControl ();
		laserBtn.SwapControl ();
		bombBtn.SwapControl ();
		if (PlayerPrefs.GetInt ("ControlPosition") == 0) {
			PlayerPrefs.SetInt ("ControlPosition", 1);
		} else {
			PlayerPrefs.SetInt ("ControlPosition", 0);
		}
	}

	public void SetZoomLevel(float zoomLevel){
		PlayerPrefs.SetFloat ("ZoomLevel" , zoomLevel);
		cam.orthographicSize = zoomLevel;
	}

	public void SetGUIScale(float scaleNumber){
		virtualJoystick.SetScale (scaleNumber);
		laserBtn.SetScale (scaleNumber);
		bombBtn.SetScale (scaleNumber);
		PlayerPrefs.SetFloat ("GUIScale" , scaleNumber);
	}

	public void ReturnToMain(){
		StartCoroutine (ReturnProcess());
	}

	IEnumerator ReturnProcess(){
		transition.SetBool ("Leaving" , true);
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene ("MainMenu");
	}

	public void SetMusicLevel(float musicLevel){
		PlayerPrefs.SetFloat ("MusicLevel" , musicLevel);
	}

	public void SetSoundLevel(float soundLevel){
		PlayerPrefs.SetFloat ("SoundLevel" , soundLevel);
	}
}
