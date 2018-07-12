using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	private bool onGround;
	private Quaternion standRotation;

	// Use this for initialization
	void Start () {
		onGround = true;
		standRotation = Quaternion.Euler (0f,0f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Ground")) {
			onGround = true;
			this.transform.rotation = standRotation;
		}
	}


	public bool OnGround{
		get{
			return onGround;
		}
		set{ 
			onGround = value;
		}
	}
}
