using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRunnerScript : MonoBehaviour {

	public Transform player;

	void Start(){
		Screen.orientation = ScreenOrientation.Landscape;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x + 6, 0, -10);
	}
}
