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
	private TouchInputMovement TIM;
	private SFXPlayer m_SFXPlayer;
	private MusicPlayer m_MusicPlayer;

	void Start () {
		onGround = false;
		standRotation = Quaternion.Euler (0f,0f,0f);
		rb = GetComponent<Rigidbody2D> ();
		armature = GetComponentInChildren<UnityArmatureComponent> ();
		TIM = FindObjectOfType<TouchInputMovement> ();
		m_SFXPlayer = FindObjectOfType<SFXPlayer> ();
		m_MusicPlayer = FindObjectOfType<MusicPlayer> ();
	}

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

		//stop sfx when gameover
		if (GameControlScript.Instance.IsGameOver) {
			m_SFXPlayer.m_audioSource.Stop ();
		}
	}

	//if player land on the top of the platform
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Ground")) {
			onGround = true;
			this.transform.rotation = standRotation;

			PijakanManagerScript.Instance.SpawnPijakan ();		//spawn platform
			armature.animation.Play (landingAnimation,1);
			isLanding = true;
			timerAnimationToIdle = 0.75f;						//play idle animation
			m_SFXPlayer.m_audioSource.PlayOneShot (m_SFXPlayer.sfxAudio [2]);		//landing sfx
		}			
	}

	//If player touch deathzone (for game over)
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("DeathZone")) {
			GameControlScript.Instance.GameOver ();
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

	public Rigidbody2D rbPlayer{
		get{
			return this.rb;
		}
		set{
			this.rb = value;
		}
	}
}
