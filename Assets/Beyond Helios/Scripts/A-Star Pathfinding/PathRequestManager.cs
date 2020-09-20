using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestManager : MonoBehaviour {

//Variables -------------------------------------

	Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
	PathRequest currentPathRequest;

	static PathRequestManager instance;
	Pathfinding pathfinding;

	bool isProcessingPath;

//Awake Method ----------------------------------

	void Awake(){
		instance = this;
		pathfinding = GetComponent<Pathfinding> ();
	}

//Request Path Method, method used to request a new path

	public static void RequestPath(Vector3 pathStart , Vector3 pathEnd , Action<Vector3[] , bool> callback){
		PathRequest newRequest = new PathRequest (pathStart , pathEnd , callback);
		instance.pathRequestQueue.Enqueue (newRequest);
		instance.TryProcessNext ();
	}

//RemoveRequest, method which removes the top request from the queue

	public void RemoveRequest(){
		instance.pathRequestQueue.Dequeue ();
	}

//TryProcessNext Method, tries to process the next path in the queue once finished with its current

	void TryProcessNext(){
		if (!isProcessingPath && pathRequestQueue.Count > 0) {
			currentPathRequest = pathRequestQueue.Dequeue ();
			isProcessingPath = true;
			pathfinding.StartFindPath (currentPathRequest.pathStart , currentPathRequest.pathEnd);
		}
	}

//FinishedProcessingPath Method tells the manager that is has finshed processing its current path

	public void FinishedProcessingPath(Vector3[] path, bool success){
		object obj = currentPathRequest.callback.Target;
		if (obj == null) {
			Debug.Log ("behavior was destroyed, no callback");
		} else if ((obj is UnityEngine.Object) && (obj.Equals (null))) {
			Debug.Log ("behavior was destroyed, no callback");
		} else {
			currentPathRequest.callback (path,success);
		}
		isProcessingPath = false;
		TryProcessNext ();
	}

//Struct that contains all the data of a PathRequest

	struct PathRequest{
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public Action<Vector3[] , bool> callback;

		public PathRequest(Vector3 _start , Vector3 _end , Action<Vector3[] , bool> _callback){
			pathStart = _start;
			pathEnd = _end;
			callback = _callback;
		}
	}

}
