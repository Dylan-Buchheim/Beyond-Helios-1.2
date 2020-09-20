using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {

	Animator transitionAnim;
	AudioManager am;

	void Start(){
		transitionAnim = GameObject.Find ("Transition").GetComponent<Animator> ();
		am = FindObjectOfType<AudioManager> ();
	}

	public void LoadScene(string sceneToLoad){
		StartCoroutine (LoadProccess(sceneToLoad));
	}

	IEnumerator LoadProccess (string sceneToLoad){
		transitionAnim.SetBool ("Leaving", true);
		yield return new WaitForSeconds (.25f);
		SceneManager.LoadScene (sceneToLoad);
	}

	public void PlaySelectSound(){
		am.Play ("Select1");
	}


}
