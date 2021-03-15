using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PijakanBehaviour : MonoBehaviour {

	private bool firstLand;
	private float jumpPressure;
	void Start(){
		firstLand = true;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Player")) {
			if (firstLand) {
				jumpPressure = FindObjectOfType<TouchInputMovement> ().jumpPressureScore;
				firstLand = false;
				GameControlScript.Instance.Score++;
				if (jumpPressure >= 10f) {
					GameControlScript.Instance.Score++;
				}
				GameControlScript.Instance.ResetTimer ();
			}
		}
	}
}
