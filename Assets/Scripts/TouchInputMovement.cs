using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DragonBones;

public class TouchInputMovement : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
	//Untuk mengontrol player
	public float tiltSmooth;

	GameObject Player;
	PijakanManagerScript PM;
	private float jumpPressure;
	private float minJump;
	private float maxJumpPressure;
	private Rigidbody2D rb;
	private bool hold;

	private UnityArmatureComponent armature;		
	private string idleAnimation = "idle";
	private string prepareChargeAnimation = "prepare_charge";
	private string chargeAnimation = "charge";
	private string jumpAnimation = "jump";
	private string onAirAnimation = "on_air";
	private string prepareLandingAnimation = "prepare_landing";
	private string landingAnimation = "landing";

	private SFXPlayer m_SFXPlayer;
	private float minJumpDur;			//minimal time to jump
	private LineRenderer LR;

	void Start () {
		hold = false;
		Player = GameObject.Find ("Player");
		PM = GameObject.FindObjectOfType<PijakanManagerScript> ();
		rb = Player.GetComponent<Rigidbody2D> ();
		minJump = 0f;
		jumpPressure = 0f;
		maxJumpPressure = 10f;
		armature = Player.GetComponentInChildren<UnityArmatureComponent> ();
		m_SFXPlayer = FindObjectOfType<SFXPlayer> ();
		LR = Player.GetComponentInChildren<LineRenderer> ();
		LR.enabled = false;
	}

	void Update(){
		//when player hold press	/ charging jump
		if (hold) {
			if (Player.GetComponent<PlayerScript> ().OnGround && !GameControlScript.Instance.IsBGMove &&
			    !GameControlScript.Instance.IsGameOver) {
				//if (jumpPressure < maxJumpPressure) {
				jumpPressure += Time.deltaTime * 9f;
				//} else {
				//jumpPressure = maxJumpPressure;
				//}

				Player.GetComponentInChildren<LaunchArcRenderer> ().velocity = jumpPressure;		//draw arc line
				minJumpDur -= Time.deltaTime;
			}
		}
	}

	//when press
	public virtual void OnPointerDown(PointerEventData ped){
		if (GameControlScript.Instance.IsGameOn && Player.GetComponent<PlayerScript> ().OnGround &&
			!GameControlScript.Instance.IsBGMove && !GameControlScript.Instance.IsGameOver) {
			hold = true;
			armature.animation.Play(prepareChargeAnimation,1);
			armature.animation.FadeIn (chargeAnimation,0.2f,-1);
			if (GameControlScript.Instance.Score < 20) {
				LR.enabled = true;
			}
			m_SFXPlayer.m_audioSource.PlayOneShot( m_SFXPlayer.sfxAudio [0]);
			minJumpDur = 0.25f;
		}
	}

	//when let go
	public virtual void OnPointerUp(PointerEventData ped){
		//PM.SpawnPijakan ();
		if (GameControlScript.Instance.IsGameOn && Player.GetComponent<PlayerScript> ().OnGround &&
		    !GameControlScript.Instance.IsBGMove && !GameControlScript.Instance.IsGameOver && minJumpDur <= 0f) {
			hold = false;
			jumpPressure /= 1.3f;
			GameControlScript.Instance.scrollSpeed = -jumpPressure;
			if (jumpPressure > 0) {
				jumpPressure = jumpPressure + minJump;
				rb.velocity = new Vector2 (0f, jumpPressure);
				jumpPressure = 0;
				Player.GetComponent<PlayerScript> ().OnGround = false;
				armature.animation.FadeIn (jumpAnimation, -1, 1);
				armature.animation.FadeIn (onAirAnimation, 0.25f, 1);
				armature.animation.FadeIn (prepareLandingAnimation, 0.5f, -1);
			}

			Player.GetComponentInChildren<LaunchArcRenderer> ().velocity = 0;		//reset arc line
			m_SFXPlayer.m_audioSource.Stop ();
			m_SFXPlayer.m_audioSource.PlayOneShot (m_SFXPlayer.sfxAudio [1]);

		} else if(Player.GetComponent<PlayerScript> ().OnGround){		//player can't jump if the press duration is <= 0.5s
			hold = false;
			jumpPressure = 0f;
			m_SFXPlayer.m_audioSource.Stop ();
			Player.GetComponentInChildren<LaunchArcRenderer> ().velocity = 0;
			armature.animation.FadeIn (idleAnimation, -1, 1);
		}
		LR.enabled = false;
	}

	public bool Hold{
		get{
			return hold;
		}
	}
		
}
