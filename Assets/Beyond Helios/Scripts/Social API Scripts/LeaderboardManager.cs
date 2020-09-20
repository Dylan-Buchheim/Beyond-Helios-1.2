using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

using UnityEngine.SocialPlatforms;

public class LeaderboardManager : MonoBehaviour {

	#region GAME_CENTER
	long highScore;
	long easyHighScore;

	void Start(){
		#if UNITY_IPHONE
			//Authenticate and register a calllback
			Social.localUser.Authenticate(ProcessAuthentication);
			//Set HighScore = PlayerPrefs HighScore
			highScore = P31Prefs.getInt("HighScore");
			easyHighScore = P31Prefs.getInt("EasyHighScore");
			Social.ReportScore (highScore, "Beyond_Helios_High_Score_Leaderboard", HighScoreCheck);
			Social.ReportScore (easyHighScore, "Beyond_Helios_Easy_High_Score_Leaderboard", HighScoreCheck);
		#endif
	}

	//Callback for Authentication
	void ProcessAuthentication(bool success){
		#if UNITY_IPHONE
		if(success){
			Debug.Log ("Authenticated");
		}else{
			Debug.Log ("Failed to Authenticate");
		}
		#endif
	}

	//Callback for HighScore Reporting
	void HighScoreCheck(bool result){
		#if UNITY_IPHONE
		if(result){
			Debug.Log ("Score submission successful");
		}else{
			Debug.Log ("Score submission failed");
		}
		#endif
	}

	public void LoadLeaderboard(){
		#if UNITY_IPHONE
			Social.ShowLeaderboardUI ();
		#endif
	}
	#endregion
}
