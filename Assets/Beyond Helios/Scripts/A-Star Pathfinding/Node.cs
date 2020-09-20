using System.Collections;
using UnityEngine;

public class Node : IHeapItem<Node>{

//Local variables that store the walkability and worldposition of each node in the grid array

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;
	int heapIndex;



//Constructor for a Node Object

	public Node(bool _walkable , Vector3 _worldPosition , int _gridX , int _gridY){
		walkable = _walkable;
		worldPosition = _worldPosition;
		gridX = _gridX;
		gridY = _gridY;
	}

//Integer Methods

	public int fCost{
		get{ 
			return gCost + hCost;
		}
	}

	public int HeapIndex{
		get{ 
			return heapIndex;
		}
		set{ 
			heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare){
		int compare = fCost.CompareTo (nodeToCompare.fCost);

		if (compare == 0) {
			compare = hCost.CompareTo (nodeToCompare.hCost);
		}
		return -compare;
	}


}
