using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {

//Variables-------------------------------

	PathRequestManager requestManager;
	Grid grid;

//Awake Method----------------------------

	void Awake(){
		requestManager = GetComponent<PathRequestManager> ();
		grid = GetComponent<Grid> ();
	}

//StartFindPath Method, initiates the FindPath Coroutine 

	public void StartFindPath(Vector3 startPos , Vector3 targetPos){
		StartCoroutine (FindPath (startPos, targetPos));
	}

//FindPath Method, method that finds the shortest path between two points

	IEnumerator FindPath(Vector3 startPos , Vector3 targetPos){

		Stopwatch sw = new Stopwatch ();
		sw.Start ();

		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);

		if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node> ();
			openSet.Add (startNode);

			while (openSet.Count > 0){
				Node currentNode = openSet.RemoveFirst ();
				closedSet.Add (currentNode);

				if (currentNode == targetNode) {
					sw.Stop ();
					// print ("Path found: " + sw.ElapsedMilliseconds + " ms");
					pathSuccess = true;

					break;
				}

				foreach (Node neighbor in grid.GetNeighbors(currentNode)) {
					if (!neighbor.walkable || closedSet.Contains (neighbor)) {
						continue;
					}

					int newMovementCostToNeighbor = currentNode.gCost + GetDistance (currentNode, neighbor);

					if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains (neighbor)) {
						neighbor.gCost = newMovementCostToNeighbor;
						neighbor.hCost = GetDistance (neighbor, targetNode);
						neighbor.parent = currentNode;

						if (!openSet.Contains (neighbor)) {
							openSet.Add (neighbor);
						} else {
							openSet.UpdateItem (neighbor);
						}
					}
				}
			}
		}

		yield return null;
		if (pathSuccess) {
			waypoints = RetracePath (startNode, targetNode);
		}
		requestManager.FinishedProcessingPath (waypoints, pathSuccess);
	}

//Retrace Path Method, method that computes all nodes used in the path created from the find path method

	Vector3[] RetracePath(Node startNode , Node endNode){
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath (path);
		Array.Reverse (waypoints);
		return waypoints;
	}

//SimplifyPath Method, goes through the found path and only adds nodes where the path changes direction

	Vector3[] SimplifyPath(List<Node> path){
		List<Vector3> waypoints = new List<Vector3> ();
		Vector2 directionOld = Vector2.zero;

		for(int i = 1; i < path.Count; i ++){
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX , path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray ();
	}

//Get Distance Method, returns the int for the minimum number of jumps between two nodes in the grid

	int GetDistance(Node nodeA , Node nodeB){
		int dstX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

		if (dstX > dstY) {
			return 14 * dstY + 10 * (dstX - dstY);
		} else {
			return 14 * dstX + 10 * (dstY - dstX);
		}
	}
		
}
