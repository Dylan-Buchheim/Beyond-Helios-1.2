using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationGrid : MonoBehaviour {

//Variables ---------------------------------

	public bool drawGizmos = true;
	public Vector2 worldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	public int gridSizeX , gridSizeY;

	public int MaxSize{
		get{ 
			return gridSizeX * gridSizeY;
		}
	}

//Awake Method ------------------------------

	void Awake(){
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (worldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (worldSize.y / nodeDiameter);
		CreateGrid ();
	}

//CreateGrid Method, calculates grid size and populates the array with nodes

	public void CreateGrid(){

		grid = new Node[gridSizeX , gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.up * worldSize.y / 2;

		for(int x = 0; x < gridSizeX; x ++){
			for(int y = 0; y < gridSizeY; y ++){
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
				bool walkable = true;
				grid [x, y] = new Node (walkable , worldPoint , x , y);
			}
		}
	}

//GetNeighbors Method, returns all neighboring nodes in a list

	public List<Node> GetNeighbors(Node node){
		List<Node> neighbors = new List<Node> ();

		for (int x = -1; x <= 1; x++) {

			for (int y = -1; y <= 1; y++) {

				if(x == 0 && y == 0){
					continue;
				}

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbors.Add (grid[checkX , checkY]);
				}

			}
		}

		return neighbors;
	}

//RandomNode Method, returns a random node from the grid

	public Node RandomNode(){
		int x = Random.Range (0, gridSizeX);
		int y = Random.Range (0 , gridSizeY);

		return grid[x , y];
	}

	public Node RandomSideNode(int side){
		int x = 0;
		if(side == 0){
			x = 0;
		}
		if(side == 1){
			x = gridSizeX-1;
		}
		int y = Random.Range (0 , gridSizeY);

		return grid[x , y];
	}

//OnDrawGizmos method draws gizmos that represent nodes and their status

	void OnDrawGizmos(){

		Gizmos.DrawWireCube (transform.position , new Vector3(worldSize.x , worldSize.y , 1));

		if (grid != null && drawGizmos) {
			foreach(Node n in grid){
				Gizmos.color = (n.walkable) ? Color.black : Color.red;
				Gizmos.DrawWireCube (n.worldPosition , Vector3.one * (nodeDiameter - .4f));
			}
		}
	}
}
