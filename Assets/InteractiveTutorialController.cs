using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveTutorialController : MonoBehaviour {

	public GameObject tutorialPanel;
	public Text textWindow;
	public TutorialPrompt[] prompts;
	public int promptNumber = 0;

	private GameObject currentBorder;
	private GameObject currentWindow;
	private bool tutorialStarted = false;
	
	//Start Method
	void Start(){
		PlayerPrefs.SetInt ("PlayedTutorial", 1);
	}

	// Update is called once per frame
	float elapsedTime;
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime > 0.5f && !tutorialStarted){
			tutorialStarted = true;
			tutorialPanel.SetActive (!tutorialPanel.activeSelf);
			DisplayUIPrompt (prompts[promptNumber].border,prompts[promptNumber].tutorialText);
			Time.timeScale = 0;
		}

		if((Input.GetTouch(0).phase == TouchPhase.Began || Input.GetKeyDown(KeyCode.Return)) && promptNumber < prompts.Length && tutorialStarted){
			promptNumber++;
			if (promptNumber > prompts.Length - 1) {
				tutorialPanel.SetActive (!tutorialPanel.activeSelf);
				Time.timeScale = 1;
			} else {
				Time.timeScale = 1;
				DisplayUIPrompt (prompts[promptNumber].border,prompts[promptNumber].tutorialText);
				Time.timeScale = 0;
			}
		}
	}

	public void DisplayUIPrompt(GameObject border, string prompt){
		if(currentBorder != null){
			currentBorder.SetActive (false);
		}
		currentBorder = border;
		currentBorder.SetActive (true);

		textWindow.text = prompt;
	}

	public void DisplayTextPrompt(GameObject promptWindow){
		currentWindow = promptWindow;
		currentWindow.SetActive (true);
		Time.timeScale = 0;

	}

	public void HideTextPrompt(){
		if(currentWindow != null){
			currentWindow.SetActive (false);
			Time.timeScale = 1;
		}
	}
}

[System.Serializable]
public class TutorialPrompt{
	public GameObject border;
	[TextArea]
	public string tutorialText;
}
