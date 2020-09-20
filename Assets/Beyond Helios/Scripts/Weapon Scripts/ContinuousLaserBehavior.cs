using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousLaserBehavior : MonoBehaviour {

	public float upTime = 3;
	public float cooldown = 2;
	public float range = 10;
	public LayerMask collidableLayer;
	public LayerMask shootableLayer;

	public GameObject laserObject;
	LineRenderer lineRender;
	float count;
	bool extended;

	bool firing = false;

	AudioSource audioS;
	PlayerController2D playerController;

	void Start(){
		lineRender = laserObject.GetComponent<LineRenderer> ();
		audioS = GetComponent<AudioSource> ();
		playerController = GetComponent<PlayerController2D> ();
	}

	IEnumerator currentLaserCycle;

	public void StartLaserCycle(){
		if(currentLaserCycle == null){
			currentLaserCycle = LaserCycle ();
			StartCoroutine (currentLaserCycle);
		}
	}

	IEnumerator LaserCycle(){
		firing = true;
		audioS.Play ();
		count = 0;
		extended = false;
		yield return new WaitForSeconds (upTime);
		firing = false;
		audioS.Stop ();
		yield return new WaitForSeconds (cooldown);
		currentLaserCycle = null;
	}

	void FixedUpdate(){
		if (firing && !playerController.destroyed) {
			CheckCollision ();
		} else if (count > 0 && !playerController.destroyed) {
			lineRender.SetPosition (0, transform.position);
			lineRender.SetPosition (1, transform.position + (transform.up * count));
			count -= (count / 10);
		} else {
			lineRender.SetPosition (0, transform.position);
			lineRender.SetPosition (1, transform.position);
		}

		if(playerController.destroyed && currentLaserCycle != null){
			StopCoroutine (currentLaserCycle);
			currentLaserCycle = null;
			firing = false;
			audioS.Stop ();
		}
	}

	void CheckCollision(){
		float distance;
		RaycastHit2D hitDestroy;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, range, collidableLayer); 
		if (hit.collider != null) {

			distance = Vector2.Distance (transform.position, hit.point);
			if (!extended) {
				hitDestroy = Physics2D.Raycast (transform.position, transform.up, count, shootableLayer);
			} else {
				hitDestroy = Physics2D.Raycast (transform.position, transform.up, distance, shootableLayer);
			}

			Debug.DrawRay (transform.position , transform.up * distance , Color.red);

			if(hitDestroy.collider != null){
				if (hitDestroy.collider.gameObject.GetComponent<MothershipGun> () != null) {
					MothershipGun mg = hitDestroy.collider.gameObject.GetComponent<MothershipGun> ();
					if(!mg.destroyed){
						mg.DestroyGun ();
					}
				}
				if (hitDestroy.collider.gameObject.GetComponent<MothershipFuelCell> () != null) {
					MothershipFuelCell mf = hitDestroy.collider.gameObject.GetComponent<MothershipFuelCell> ();
					if(!mf.destroyed){
						mf.DestroyGun ();
					}
				}
				if (hitDestroy.collider.gameObject.GetComponent<EnemyFighterBehavior> () != null) {
					EnemyFighterBehavior efb = hitDestroy.collider.gameObject.GetComponent<EnemyFighterBehavior> ();
					efb.DestroyShip ();
				}
			}

		} else {
			distance = range;
		}

		lineRender.SetPosition (0, transform.position);
		if(count < distance && !extended){
			count += (distance / 10);
			lineRender.SetPosition (1, transform.position + (transform.up * count));
		}else{
			extended = true;
			count = distance;
			lineRender.SetPosition (1, transform.position + (transform.up * distance));
		}

		Debug.DrawRay (transform.position , transform.up * range , Color.green);
	}


}
