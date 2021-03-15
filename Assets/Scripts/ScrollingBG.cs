using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour {

	public float bgSize;

	private Transform cameraTransform;
	private Transform[] layers;
	private float viewZone = 10;
	private int leftIndex;
	private int rightIndex;


	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;
		layers = new Transform[transform.childCount];
		for(int i = 0; i < transform.childCount; i++){
			layers[i] = transform.GetChild(i);
		}

		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}

	void Update(){
		if (cameraTransform.position.x > (layers [rightIndex].transform.position.x - viewZone)) {
			ScrollRight ();
		}

		if (cameraTransform.position.x < (layers [leftIndex].transform.position.x + viewZone)) {
			ScrollLeft ();
		}
	}
	
	private void ScrollRight(){
		layers [leftIndex].position = Vector3.right * (layers [rightIndex].position.x + bgSize);
		rightIndex = leftIndex;
		leftIndex++;
		if(leftIndex == layers.Length){
			leftIndex = 0;
		}
	}

	private void ScrollLeft(){
		layers [rightIndex].position = Vector3.right * (layers [leftIndex].position.x - bgSize);
		leftIndex = rightIndex;
		rightIndex--;
		if(rightIndex < 0){
			rightIndex = layers.Length-1;
		}
	}
}
