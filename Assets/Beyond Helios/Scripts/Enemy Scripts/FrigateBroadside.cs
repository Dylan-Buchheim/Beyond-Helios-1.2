using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigateBroadside : MonoBehaviour {

	bool destroyed = false;
	public GameObject explosionFX;
	public GameObject destroyedFrigate;

	public float range = 4;
	public float cooldown;
	public LayerMask shootableLayer;
	public GameObject laser;

	private Transform broadside_1;
	private Transform broadside_2;
	IEnumerator topFireCycle;
	IEnumerator bottomFireCycle;

	private AudioManager am;

	void Start(){
		broadside_1 = transform.Find ("Broadside_1");
		broadside_2 = transform.Find ("Broadside_2");
		am = FindObjectOfType<AudioManager> ();
	}

	void FixedUpdate(){
		if (Physics2D.Raycast (broadside_1.transform.position , broadside_1.transform.up, range , shootableLayer)) {
			if(topFireCycle == null){
				topFireCycle = FireTopWeapons();
				StartCoroutine (topFireCycle);
			}
		}
		if (Physics2D.Raycast (broadside_2.transform.position , broadside_2.transform.up, range ,shootableLayer)) {
			if(bottomFireCycle == null){
				bottomFireCycle = FireBottomWeapons();
				StartCoroutine (bottomFireCycle);
			}
		}
		Debug.DrawRay (broadside_1.transform.position , broadside_1.transform.up * range , Color.green);
		Debug.DrawRay (broadside_2.transform.position , broadside_2.transform.up * range , Color.green);
	}

	IEnumerator FireTopWeapons(){
		am.Play ("LaserSmall");
		Instantiate (laser , broadside_1.transform.position , broadside_1.transform.rotation);
		yield return new WaitForSeconds (cooldown);
		topFireCycle = null;
		yield break;
	}
	IEnumerator FireBottomWeapons(){
		am.Play ("LaserSmall");
		Instantiate (laser , broadside_2.transform.position , broadside_2.transform.rotation);
		yield return new WaitForSeconds (cooldown);
		bottomFireCycle = null;
		yield break;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Equals("MothershipLaser") && !destroyed){
			MothershipLaser laserController = other.gameObject.GetComponent<MothershipLaser> ();
			if(laserController.laserOn){
				StartCoroutine (beginDestroying());
			}
		}
	}

	//DestroyShip Method, handles the proccesses of death

	IEnumerator beginDestroying(){
		destroyed = true;
		Instantiate (explosionFX , transform.position , transform.rotation);
		yield return new WaitForSeconds (.6f);
		Instantiate (destroyedFrigate , transform.position , transform.rotation);
		Destroy (this.gameObject);
		yield break;
	}
}
