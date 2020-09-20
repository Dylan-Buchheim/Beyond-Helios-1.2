using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {

//Read Only Variables for the unit to read its path

	public readonly Vector3[] lookPoints;
	public readonly Line[] turnBoundaries;
	public readonly int finshLineIndex;

//Constructor for the Path Object

	public Path(Vector3[] waypoints , Vector3 startPos , float turnDst){
		lookPoints = waypoints;
		turnBoundaries = new Line[lookPoints.Length];
		finshLineIndex = turnBoundaries.Length - 1;

		Vector2 previousPoint = V3ToV2 (startPos);
		for(int i = 0; i < lookPoints.Length; i ++){
			Vector2 currentPoint = V3ToV2 (lookPoints[i]);
			Vector2 dirToCurrentPoint = (currentPoint - previousPoint).normalized;
			Vector2 turnBoundaryPoint = (i == finshLineIndex)?currentPoint : currentPoint - dirToCurrentPoint * turnDst;
			turnBoundaries [i] = new Line (turnBoundaryPoint , previousPoint - dirToCurrentPoint * turnDst);
			previousPoint = turnBoundaryPoint;
		}
	}

//V3ToV2 Method, method whic converts Vector3 in Vector2

	Vector2 V3ToV2(Vector3 v3){
		return new Vector2 (v3.x , v3.y);
	}

//DrawWithGizmos Method, method which the unit calls to draw its path

	public void DrawWithGizmos(){

		Gizmos.color = Color.red;
		foreach(Vector3 p in lookPoints){
			Gizmos.DrawCube (p + Vector3.forward , new Vector3(.3f,.3f,.3f));
		}

		Gizmos.color = Color.red;
		foreach(Line l in turnBoundaries){
			l.DrawWithGizmos (1);
		}
	}
}
