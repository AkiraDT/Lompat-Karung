using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PijakanBehaviour : MonoBehaviour {

	private bool firstLand;

	void Start(){
		firstLand = true;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Player")) {
			if (firstLand) {
				firstLand = false;
				GameControlScript.Instance.Score++;
				GameControlScript.Instance.ResetTimer ();
			}
		}
	}

}
