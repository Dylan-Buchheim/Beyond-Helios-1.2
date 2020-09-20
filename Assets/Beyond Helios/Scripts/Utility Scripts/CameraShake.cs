using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public Properties testProperties;

	IEnumerator currentShakeCoroutine;
	Camera cam;

	void Awake(){
		cam = GetComponent<Camera> ();
		if (PlayerPrefs.GetFloat ("ZoomLevel") != 0) {
			cam.orthographicSize = PlayerPrefs.GetFloat ("ZoomLevel");
		} else {
			cam.orthographicSize = 3.5f;
		}
	}

	public void StartShake(Properties properties){	
		if(currentShakeCoroutine != null){
			StopCoroutine (currentShakeCoroutine);
		}
		currentShakeCoroutine = Shake (properties);
		StartCoroutine (currentShakeCoroutine);
	}

	IEnumerator Shake(Properties properties){
		float completionPercent = 0;
		float movePercent = 0;

		float angle_radians = properties.angle * Mathf.Deg2Rad - Mathf.PI;
		Vector3 previousWaypoint = Vector3.zero;
		Vector3 currentWaypoint = Vector3.zero;
		float moveDistance = 0;

		while (true) {

			if(movePercent >= 1 || completionPercent == 0){
				float dampingFactor = DampingCurve (completionPercent, properties.dampingPercent);
				float noiseAngle = (Random.value - .5f) * Mathf.PI;
				angle_radians += Mathf.PI + noiseAngle * properties.noisePercent;
				currentWaypoint = new Vector3 (Mathf.Cos(angle_radians) , Mathf.Sin(angle_radians)) * properties.strength * dampingFactor;
				previousWaypoint = new Vector3(transform.localPosition.x + 0.001f , transform.localPosition.y + 0.001f , transform.localPosition.z + 0.001f) ;

				moveDistance = Vector3.Distance (currentWaypoint , previousWaypoint);
				movePercent = 0;
			}

			completionPercent += Time.deltaTime / properties.duration;
			movePercent += Time.deltaTime / moveDistance * properties.speed;
			transform.localPosition = Vector3.Lerp (previousWaypoint , currentWaypoint , movePercent + 0.001f);

			yield return null;
		}
	}

	float DampingCurve(float x , float dampingPercent){
		x = Mathf.Clamp01 (x);
		float a = Mathf.Lerp (2 , .25f , dampingPercent);
		float b = 1 - Mathf.Pow (x, a);
		return b * b * b;
	}


}

[System.Serializable]
public class Properties{
	public float angle;
	public float strength;
	public float speed;
	public float duration;
	[Range(0,1)]
	public float noisePercent;
	[Range(0,1)]
	public float dampingPercent;
}