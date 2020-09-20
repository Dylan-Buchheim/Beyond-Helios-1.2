using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipLaserController : MonoBehaviour {

	//Public Variables --------------------------------

	public AudioClip charge;
	public AudioClip beam;
	AudioSource source;

	public float cooldown = 5;
	public float turnSpeed = 2;

	public GameObject destroyedMothership;
	public GameObject explosionFX;

	//Private Variables -------------------------------

	private bool destroyedMS = false;
	public bool spinning = false;
	private float targetAngle;

	private ScoreKeeper scoreKeep;

	private GameObject fuelCellL;
	MothershipFuelCell controllerFCL;
	private GameObject fuelCellR;
	MothershipFuelCell controllerFCR;

	private Animator mothershipAnim;


	IEnumerator spinCycle = null;

	//Start Method ------------------------------------

	void Start () {
		fuelCellL = this.gameObject.transform.Find ("Mothership_Laser_FuelCell (Left)").gameObject;
		fuelCellR = this.gameObject.transform.Find ("Mothership_Laser_FuelCell (Right)").gameObject;
		mothershipAnim = GetComponent<Animator> ();

		controllerFCL = fuelCellL.GetComponent<MothershipFuelCell> ();
		controllerFCR = fuelCellR.GetComponent<MothershipFuelCell> ();

		source = GetComponent<AudioSource> ();

		scoreKeep = GameObject.Find ("_ScoreKeeper").GetComponent<ScoreKeeper> ();
	}

	void Update(){
		OnMothershipDestroy ();

		if(spinCycle == null){
			spinCycle = SpinCycle ();
			StartCoroutine (spinCycle);
		}
	}

	void FixedUpdate(){
		if(spinning){
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);
		}
	}
		
	//Spin Logic Section, holds coroutine and method that control mothership spinning and attacking

	IEnumerator SpinCycle(){
		//Do Mothership Charge Animation
		mothershipAnim.SetBool("Charging" , true);
		source.PlayOneShot (charge);
		yield return new WaitForSeconds (0.5f);
		//Begin Spinning and turn on lasers
		targetAngle = transform.rotation.eulerAngles.z + 180;
		spinning = true;
		source.PlayOneShot (beam);


		yield return new WaitForSeconds (cooldown);
		spinning = false;
		mothershipAnim.SetBool("Charging" , false);
		yield return new WaitForSeconds (2f);
		spinCycle = null;
	}

	//Method that checks for the destruction of the Mothership---------------------------

	void OnMothershipDestroy(){
		if(controllerFCL.destroyed && controllerFCR.destroyed  && !destroyedMS){
			destroyedMS = true;
			scoreKeep.AddScore (200);
			Instantiate (explosionFX , transform.position , transform.rotation);
			StartCoroutine (DestroyProccess());
		}
	}

	IEnumerator DestroyProccess(){
		yield return new WaitForSeconds (0.75f);

		Instantiate (destroyedMothership , transform.position , transform.rotation);
		Destroy (this.gameObject);
	}

}
