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


	private Quaternion downRotation;
	private Quaternion forwardRotation;


	private UnityArmatureComponent armature;		
	private string idleAnimation = "idle";
	private string prepareChargeAnimation = "prepare_charge";
	private string chargeAnimation = "charge";
	private string jumpAnimation = "jump";
	private string onAirAnimation = "on_air";
	private string prepareLandingAnimation = "prepare_landing";
	private string landingAnimation = "landing";

	// Use this for initialization
	void Start () {
		hold = false;
		Player = GameObject.Find ("Player");
		PM = GameObject.FindObjectOfType<PijakanManagerScript> ();
		rb = Player.GetComponent<Rigidbody2D> ();
		minJump = 0f;
		jumpPressure = 0f;
		maxJumpPressure = 10f;

		downRotation = Quaternion.Euler (0f,0f,-90f);
		forwardRotation = Quaternion.Euler (0f,0f,35f);

		armature = Player.GetComponentInChildren<UnityArmatureComponent> ();
	}

	void Update(){
		if (hold) {
			if (Player.GetComponent<PlayerScript> ().OnGround && !GameControlScript.Instance.IsBGMove) {
				//if (jumpPressure < maxJumpPressure) {
				jumpPressure += Time.deltaTime * 9f;
				//} else {
				//jumpPressure = maxJumpPressure;
				//}

				Player.GetComponentInChildren<LaunchArcRenderer> ().velocity = jumpPressure;
			}
		}

		if (!Player.GetComponent<PlayerScript> ().OnGround) {
			//Player.transform.rotation = Quaternion.Lerp (Player.transform.rotation, downRotation, tiltSmooth * Time.deltaTime);

		}
		
	}

	//ketika menyentuh layar
	public virtual void OnPointerDown(PointerEventData ped){
		if (GameControlScript.Instance.IsGameOn && Player.GetComponent<PlayerScript> ().OnGround && !GameControlScript.Instance.IsBGMove) {
			hold = true;

			armature.animation.Play(prepareChargeAnimation,1);
			armature.animation.FadeIn (chargeAnimation,0.2f,-1);
		}
	}

	//ketika sentuhan dilepaskan
	public virtual void OnPointerUp(PointerEventData ped){
		//PM.SpawnPijakan ();
		if (GameControlScript.Instance.IsGameOn && Player.GetComponent<PlayerScript> ().OnGround && !GameControlScript.Instance.IsBGMove) {
			hold = false;
			//Player.transform.rotation = forwardRotation;
			jumpPressure/=1.3f;

			GameControlScript.Instance.scrollSpeed = -jumpPressure;
			if (jumpPressure > 0) {
				jumpPressure = jumpPressure + minJump;
//			rb.velocity = new Vector2 (jumpPressure/2f, jumpPressure);
				rb.velocity = new Vector2 (0f, jumpPressure);
				jumpPressure = 0;
				Player.GetComponent<PlayerScript> ().OnGround = false;
			}
			armature.animation.FadeIn (jumpAnimation,-1,1);
			armature.animation.FadeIn (onAirAnimation,0.25f,1);
			armature.animation.FadeIn (prepareLandingAnimation, 0.5f, -1);
			Player.GetComponentInChildren<LaunchArcRenderer> ().velocity = 0;
		}
	}

	public bool Hold{
		get{
			return hold;
		}
	}
}
