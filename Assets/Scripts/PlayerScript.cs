using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	private bool onGround;
	private Quaternion standRotation;
	private Rigidbody2D rb;
	private bool isFirstLand;

	// Use this for initialization
	void Start () {
		isFirstLand = true;
		onGround = true;
		standRotation = Quaternion.Euler (0f,0f,0f);
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(onGround)
			rb.freezeRotation = false;
		else
			rb.freezeRotation = true;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Ground")) {
			onGround = true;
			this.transform.rotation = standRotation;

			PijakanManagerScript.Instance.SpawnPijakan ();
			if (isFirstLand) {
				GameControlScript.Instance.ResetTimer ();
				isFirstLand = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("DeathZone")) {
			GameControlScript.Instance.GameOver();
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
