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
	private string landingAnimation = "landing";

	private SFXPlayer m_SFXPlayer;
	private float minJumpDur;			//minimal time to jump
	private LineRenderer LR;
	private float jumpPressureX;		//for adjusting scrollSpeed

	private float jumpPressureForScore;

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
				jumpPressureForScore = jumpPressure;
			}
		}
	}

	public void CancelJump(){
		hold = false;
		armature.animation.FadeIn (idleAnimation, 0.25f, 1);
		jumpPressure = 0f;
		Player.GetComponentInChildren<LaunchArcRenderer> ().velocity = 0;		//reset arc line
		LR.enabled = false;
	}

	//when press
	public virtual void OnPointerDown(PointerEventData ped){
		if (GameControlScript.Instance.IsGameOn && Player.GetComponent<PlayerScript> ().OnGround &&
			!GameControlScript.Instance.IsBGMove && !GameControlScript.Instance.IsGameOver && 
			!Player.GetComponent<PlayerScript> ().IsLanding) {
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
		m_SFXPlayer.m_audioSource.Stop ();
		if (GameControlScript.Instance.IsGameOn && Player.GetComponent<PlayerScript> ().OnGround &&
		    !GameControlScript.Instance.IsBGMove && !GameControlScript.Instance.IsGameOver && minJumpDur <= 0f) {
			hold = false;
			jumpPressure /= 1.3f;
			jumpPressureX = jumpPressure - 1f;
				
			GameControlScript.Instance.scrollSpeed = -jumpPressureX;
			if (jumpPressure > 0) {
				jumpPressure = jumpPressure + minJump;
				rb.velocity = new Vector2 (0f, jumpPressure);
				Player.GetComponent<PlayerScript> ().OnGround = false;
				armature.animation.FadeIn (jumpAnimation, 0.1f, 1);
				GameControlScript.Instance.IsGameOn = false;
			}
			m_SFXPlayer.m_audioSource.PlayOneShot (m_SFXPlayer.sfxAudio [1]);
			Player.GetComponent<PlayerScript> ().AirTransitionTime = Player.GetComponentInChildren<LaunchArcRenderer> ().TimeFlight;
		} else if(Player.GetComponent<PlayerScript> ().OnGround && GameControlScript.Instance.IsGameOn){		//player can't jump if the press duration is <= 0.5s
			hold = false;
			armature.animation.FadeIn (idleAnimation, 0.25f, 1);
		}
		jumpPressure = 0f;
		Player.GetComponentInChildren<LaunchArcRenderer> ().velocity = 0;		//reset arc line
		LR.enabled = false;
	}

	public bool Hold{
		get{
			return hold;
		}
	}

	public float jumpPressureScore{
		get{
			return jumpPressureForScore;
		}
	}
		
}
