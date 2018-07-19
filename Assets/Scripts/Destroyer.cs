using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("Ground")) {
			Destroy (col.transform.parent.gameObject);
		}
	}
}
