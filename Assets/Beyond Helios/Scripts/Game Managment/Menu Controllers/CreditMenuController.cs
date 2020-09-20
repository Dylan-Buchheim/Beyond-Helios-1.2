using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMenuController : MonoBehaviour {

	public Animator transitionAnim;
	AudioManager am;

	void Start(){
		am = FindObjectOfType<AudioManager> ();
	}

	public void LoadScene(string _levelName){
		StartCoroutine (LoadSceneProcess(_levelName));
	} 

	IEnumerator LoadSceneProcess (string _levelName){
		transitionAnim.SetBool ("Leaving", true);
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene (_levelName);
	}

	public void LoadURL(string URL){
		Application.OpenURL (URL);
	}

	public void PlaySelectSound(){
		am.Play ("Select1");
	}
}
