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
	private string onAirAnimation = "on_air";
	private string prepareLandingAnimation = "prepare_landing";
	private float timerAnimationToIdle = 0.75f;
	private bool isLanding = false;
	private TouchInputMovement TIM;
	private SFXPlayer m_SFXPlayer;
	private MusicPlayer m_MusicPlayer;
	private float airTransitionTime;

	void Start () {
		onGround = true;
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
			if(!GameControlScript.Instance.IsGameOn){
				if (airTransitionTime >= 0.5f) {
					airTransitionTime -= Time.deltaTime;
					if(airTransitionTime <= 0.5f){
						armature.animation.FadeIn (onAirAnimation, 0.3f, 1);
					}
				}else if(airTransitionTime >= 0f) {
					airTransitionTime -= Time.deltaTime;
					if(airTransitionTime <= 0f && airTransitionTime != -1f){
						armature.animation.FadeIn (prepareLandingAnimation, 0.1f, -1);
						airTransitionTime = -1f;
					}
				}
			}
		}

		if (isLanding) {
			timerAnimationToIdle -= Time.deltaTime;
			if (timerAnimationToIdle <= 0 && !TIM.Hold) {
				armature.animation.FadeIn (idleAnimation,0.1f,-1);
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
			Invoke ("TurnGameOn", 1f);

			PijakanManagerScript.Instance.SpawnPijakan ();		//spawn platform
			PijakanManagerScript.Instance.SpawnPijakan ();
			armature.animation.FadeIn (landingAnimation,0.1f,1);
			isLanding = true;
			timerAnimationToIdle = 0.75f;						//play idle animation
			m_SFXPlayer.m_audioSource.PlayOneShot (m_SFXPlayer.sfxAudio [2]);		//landing sfx
		}			
	}

	//If player touch deathzone (for game over)
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("DeathZone") && !GameControlScript.Instance.IsGameOver) {
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

	public float AirTransitionTime{
		set{
			airTransitionTime = value;
		}
	}

	public bool IsLanding{
		get{
			return isLanding;
		}
	}

	private void TurnGameOn(){
		GameControlScript.Instance.IsGameOn = true;
	}
}
