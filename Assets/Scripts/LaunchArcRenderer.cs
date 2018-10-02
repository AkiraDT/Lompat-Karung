using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour {

	LineRenderer lr;
	public float velocity;
	public float angle;
	public int resolution;

	float g;	//gravity on y axis
	float radianAngle;

	void Awake(){
		lr = GetComponent<LineRenderer> ();	
		g = Mathf.Abs (Physics2D.gravity.y);
	}

	void OnValidate(){
		if (lr != null && Application.isPlaying) {
			RenderArc ();
		}
	}

	void FixedUpdate () {
		RenderArc ();
	}

	//Populating the arc renderer with the appropriate setting
	void RenderArc (){
		lr.positionCount = resolution + 1;
		lr.SetPositions (CalculateArcArray ());
	}

	//create an array of vector3 position for arc 
	Vector3[] CalculateArcArray(){
		Vector3[] arcArray = new Vector3[resolution + 1];

		radianAngle = Mathf.Deg2Rad * angle;
		float maxDistance = (velocity * velocity * Mathf.Sin (2 * radianAngle)) / g;

		for (int i = 0; i <= resolution; i++) {
			float t = (float)i / (float)resolution;
			arcArray [i] = CalculateArcPoint (t, maxDistance);
		}

		return arcArray;
	}

	//calculate height and distance of each vertex
	Vector3 CalculateArcPoint (float t, float maxDistance){
		float x = t * maxDistance;
		float y = x * Mathf.Tan (radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos (radianAngle) * Mathf.Cos (radianAngle)));
		//-10 and -1 are the initial position of the character and -1 are for the z axis
		if (velocity != 0) {
			return new Vector3 (transform.parent.transform.position.x + x, transform.parent.transform.position.y -1.5f + y, -1);
		} else {
			return new Vector3 (transform.parent.transform.position.x, transform.parent.transform.position.y -1.5f, -1);
		}
	}
}
