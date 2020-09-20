using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerator : MonoBehaviour {

//Variables --------------------------------

	GenerationGrid grid;
	GameWipe wipe;
	Node currentNode;
	List<Node> currentNeighbors;

	public GameObject[] motherships;
	public GameObject frigate;
	public GameObject[] pickups;
	public GameObject[] asteroids;
	public int numberOfMotherships;
	public int numberOfPickups;
	public int numberOfAsteroids;
	public int maxNumberOfFrigates;
	public int maxNumberOfAsteroids = 3;
	int numberOfFrigates = 0;
	int side;

	private Grid fighterPathfindingGrid;
	private GameManager gameManager;

//Awake and Start Methods ------------------

	void Awake(){
		gameManager = GameObject.Find ("_GameManager").GetComponent<GameManager> ();
		grid = GetComponent<GenerationGrid> ();
		fighterPathfindingGrid = GameObject.Find ("A*_Fighters").GetComponent<Grid> ();
		wipe = GetComponent<GameWipe> ();
	}

	void Start(){
		maxNumberOfFrigates = Random.Range (0,3);
		side = Random.Range (0,2);
		GenerateMap ();
	}

	IEnumerator currentGeneration;
	void Update(){
		
		if(currentGeneration == null){
			currentGeneration = UpdateGrid ();
			StartCoroutine (currentGeneration);
		}
	}

	IEnumerator UpdateGrid(){
		yield return new WaitForSeconds (0.25f);
		fighterPathfindingGrid.nodeRadius = 0.25f;
		fighterPathfindingGrid.CreateGrid ();
		currentGeneration = null;
	}

//GenerateMap Method, method which generates a game board for the player

	public void GenerateMap(){
		numberOfMotherships = 0; // move this to the clear game board method
		grid.CreateGrid ();
		while(numberOfMotherships < gameManager.maxAmountOfMotherships){ 
			SpawnMothership ();
		}
		int maxNumberOfPickups = (int)Mathf.Floor(gameManager.maxAmountOfMotherships / 2);
		while(numberOfPickups < maxNumberOfPickups){
			SpawnPickup ();
		}
		while(numberOfAsteroids < maxNumberOfAsteroids){
			SpawnAsteroid ();
		}
		while(numberOfFrigates < maxNumberOfFrigates && spawnAttempts < 15){
			SpawnFrigate ();
		}

		fighterPathfindingGrid.CreateGrid ();// Generates the pathfinding Grid once all the motherships are in place
	}

	public void ClearThreats(){
		wipe.ClearMap ();
	}

//SpawnMothership Method, method which finds a random node to spawn a mothership in provided the area is clear

	void SpawnMothership(){
		currentNode = grid.RandomNode ();
		currentNeighbors = grid.GetNeighbors (currentNode);
		bool areaClear = true;

		if (currentNode.walkable) {
			foreach (Node n in currentNeighbors) {
				if(!n.walkable){
					areaClear = false;
					break;
				}
			}
		} else {
			areaClear = false;
		}

		if(areaClear){
			int random = Random.Range (0, 2);
			Instantiate (motherships[random] , currentNode.worldPosition , Quaternion.Euler(0 , 0 , 0));
			numberOfMotherships ++;
			currentNode.walkable = false;
			foreach (Node n in currentNeighbors) {
				n.walkable = false;
			}
		}
	}

	void SpawnPickup(){
		currentNode = grid.RandomNode ();
		currentNeighbors = grid.GetNeighbors (currentNode);
		bool areaClear = false;

		int random = Random.Range (0, 4);

		if (currentNode.walkable) {
			foreach (Node n in currentNeighbors) {
				if (!n.walkable) {
					areaClear = true;
					break;
				}
			}
		} else {
			areaClear = false;
		}

		if(areaClear){
			Instantiate (pickups[random] , currentNode.worldPosition , Quaternion.Euler(0 , 0 , 0));
			numberOfPickups ++;
			currentNode.walkable = false;
		}
	}

	void SpawnAsteroid(){
		currentNode = grid.RandomNode ();
		bool areaClear = false;

		if (currentNode.walkable) {
			areaClear = true;
		} else {
			areaClear = false;
		}

		if(areaClear){
			Instantiate (asteroids[numberOfAsteroids] , currentNode.worldPosition , Quaternion.Euler(0,0,0));
			numberOfAsteroids++;
			currentNode.walkable = false;
		}
	}

	int spawnAttempts = 0;
	void SpawnFrigate(){
		currentNode = grid.RandomSideNode (side);
		currentNeighbors = grid.GetNeighbors (currentNode);
		bool areaClear = true;

		if (currentNode.walkable) {
			foreach (Node n in currentNeighbors) {
				if(!n.walkable){
					areaClear = false;
					break;
				}
			}
		} else {
			areaClear = false;
		}

		if(areaClear){
			if(side == 0)
				Instantiate (frigate , currentNode.worldPosition , Quaternion.Euler(0 , 0 , -90));//Change the angle based on the side chosen
			else
				Instantiate (frigate , currentNode.worldPosition , Quaternion.Euler(0 , 0 , 90));//Change the angle based on the side chosen
			numberOfFrigates ++;
			currentNode.walkable = false;
			foreach (Node n in currentNeighbors) {
				n.walkable = false;
			}
		}
		spawnAttempts++;
	}
}
