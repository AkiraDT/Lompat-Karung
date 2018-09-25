using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class PlayerScript : MonoBehaviour {
	private bool onGround;
	private Quaternion standRotation;
	private Rigidbody2D rb;


	private UnityArmatureComponent armature;
	private string idleAnimation = "idle";
	private string landingAnimation = "landing";

	private float timerAnimationToIdle = 0.75f;
	private bool isLanding = false;
	private TouchInputMovement TIM;	//buat supaya pas udah neken, ga kepanggil idlenya
	// Use this for initialization
	void Start () {
		onGround = true;
		standRotation = Quaternion.Euler (0f,0f,0f);
		rb = GetComponent<Rigidbody2D> ();
		armature = GetComponentInChildren<UnityArmatureComponent> ();
		TIM = FindObjectOfType<TouchInputMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (onGround) {
			rb.freezeRotation = false;
		} else {
			rb.freezeRotation = true;
			isLanding = false;
		}
		if (isLanding) {
			timerAnimationToIdle -= Time.deltaTime;
			if (timerAnimationToIdle <= 0 && !TIM.Hold) {
				armature.animation.FadeIn (idleAnimation,-0.25f,-1);
				timerAnimationToIdle = 0.75f;
				isLanding = false;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Ground")) {
			onGround = true;
			this.transform.rotation = standRotation;

			PijakanManagerScript.Instance.SpawnPijakan ();
			armature.animation.Play (landingAnimation,1);
			isLanding = true;
			timerAnimationToIdle = 0.75f;
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
