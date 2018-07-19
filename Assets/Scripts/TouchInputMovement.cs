using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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


	// Use this for initialization
	void Start () {
		hold = false;
		Player = GameObject.Find ("Player");
		PM = GameObject.FindObjectOfType<PijakanManagerScript> ();
		rb = Player.GetComponent<Rigidbody2D> ();
		minJump = 2f;
		jumpPressure = 0f;
		maxJumpPressure = 10f;

		downRotation = Quaternion.Euler (0f,0f,-90f);
		forwardRotation = Quaternion.Euler (0f,0f,35f);
	}

	void Update(){
		if (hold) {
			if (Player.GetComponent<PlayerScript> ().OnGround) {
				//if (jumpPressure < maxJumpPressure) {
				jumpPressure += Time.deltaTime * 10f;
				//} else {
				//jumpPressure = maxJumpPressure;
				//}
			}
		}

		if (!Player.GetComponent<PlayerScript> ().OnGround) {
			Player.transform.rotation = Quaternion.Lerp (Player.transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
		}
	}

	//ketika menyentuh layar
	public virtual void OnPointerDown(PointerEventData ped){
		hold = true;
	}

	//ketika sentuhan dilepaskan
	public virtual void OnPointerUp(PointerEventData ped){
		//PM.SpawnPijakan ();
		hold = false;
		Player.transform.rotation = forwardRotation;
		if (jumpPressure > 0) {
			jumpPressure = jumpPressure + minJump;
//			rb.velocity = new Vector2 (jumpPressure/2f, jumpPressure);
			rb.velocity = new Vector2 (0f, jumpPressure);
			jumpPressure = 0;
			Player.GetComponent<PlayerScript> ().OnGround = false;
		}
	}
}
