using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line {

//Variables for the Line Struct

	const float verticalLineGradient = 1e5f;

	float gradient;
	//float y_intercept;
	Vector2 pointOnLine_1;
	Vector2 pointOnLine_2;

	bool approachSide;

//Constructor for the Line Object

	public Line(Vector2 pointOnLine , Vector2 pointPerpendicularToLine){
		float dx = pointOnLine.x - pointPerpendicularToLine.x;
		float dy = pointOnLine.y - pointPerpendicularToLine.y;

		if (dy == 0) {
			gradient = verticalLineGradient;
		} else {
			gradient = -dx / dy;
		}

		//y_intercept = pointOnLine.y - gradient * pointOnLine.x;
		pointOnLine_1 = pointOnLine;
		pointOnLine_2 = pointOnLine + new Vector2 (1 , gradient);

		approachSide = false;
		approachSide = GetSide (pointPerpendicularToLine);
				
	}

//GetSide Method, returns a bool depending upon what side of the line the passed point is

	bool GetSide(Vector2 p) {
		return (p.x - pointOnLine_1.x) * (pointOnLine_2.y - pointOnLine_1.y) > (p.y - pointOnLine_1.y) * (pointOnLine_2.x - pointOnLine_1.x);
	}

//HasCrossedLine Method, returns a bool once the point has passed the line

	public bool HasCrossedLine(Vector2 p){
		return GetSide (p) != approachSide;
	}

//DrawWithGizmos Method, the method which the unit will call to draw its path with gizmos

	public void DrawWithGizmos(float length){
		Vector3 lineDir = new Vector3 (1 , gradient , 0).normalized;
		Vector3 lineCenter = new Vector3 (pointOnLine_1.x , pointOnLine_1.y , 0) + Vector3.forward;
		Gizmos.DrawLine (lineCenter - lineDir * length / 2f , lineCenter + lineDir * length / 2f);
	}
}
