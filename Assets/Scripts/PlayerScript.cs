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

	private SFXPlayer m_SFXPlayer;
	private MusicPlayer m_MusicPlayer;
	// Use this for initialization
	void Start () {
		onGround = true;
		standRotation = Quaternion.Euler (0f,0f,0f);
		rb = GetComponent<Rigidbody2D> ();
		armature = GetComponentInChildren<UnityArmatureComponent> ();
		TIM = FindObjectOfType<TouchInputMovement> ();
		m_SFXPlayer = FindObjectOfType<SFXPlayer> ();
		m_MusicPlayer = FindObjectOfType<MusicPlayer> ();
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
		if (GameControlScript.Instance.IsGameOver) {
			m_SFXPlayer.m_audioSource.Stop ();
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
			m_SFXPlayer.m_audioSource.PlayOneShot (m_SFXPlayer.sfxAudio [2]);
		}
			
	}

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
