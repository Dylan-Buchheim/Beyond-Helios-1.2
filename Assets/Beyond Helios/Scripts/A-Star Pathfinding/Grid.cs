using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

//Variables ------------------------------

	public bool drawGizmos = true;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	public int MaxSize{
		get{ 
			return gridSizeX * gridSizeY;
		}
	}

//Awake Method ---------------------------

	void Awake(){
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y/nodeDiameter);
		CreateGrid (); //May want to remove for redundancy
	}

//CreateGrid Method, calculates grid size and populates the grid array with Nodes

	public void CreateGrid(){
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y/nodeDiameter);
		grid = new Node[gridSizeX , gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

		for(int x = 0; x < gridSizeX; x ++){
			for(int y = 0; y < gridSizeY; y ++){
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics2D.OverlapCircle(new Vector2(worldPoint.x , worldPoint.y) , nodeRadius , unwalkableMask));
				grid [x, y] = new Node (walkable , worldPoint , x , y);
			}
		}

	}

//Node from world point method, method that returns the node in the grid that contains the worldpostion passed to it

	public Node NodeFromWorldPoint(Vector3 worldPosition){

		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];

	}

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
		
//OnDrawGizmos method draws gizmos that represent nodes and their status


	void OnDrawGizmos(){
		
		Gizmos.DrawWireCube (transform.position , new Vector3(gridWorldSize.x , gridWorldSize.y , 1));

		if (grid != null && drawGizmos) {
			foreach(Node n in grid){
				Gizmos.color = (n.walkable) ? Color.black : Color.red;
				Gizmos.DrawWireCube (n.worldPosition , Vector3.one * (nodeDiameter - .4f));
			}
		}
	}

}
