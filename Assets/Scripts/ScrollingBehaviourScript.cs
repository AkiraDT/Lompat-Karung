using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBehaviourScript : MonoBehaviour {

	private PlayerScript PS;
	private Rigidbody2D rb;
	private float scrollSpeed;

	// Use this for initialization
	void Start () {
		/*
		if (this.CompareTag ("BackGround")) {
			scrollSpeed = GameControlScript.Instance.BGScrollSpeed;
		} else {
			scrollSpeed = GameControlScript.Instance.scrollSpeed;
		}
		*/

		PS = GameObject.FindObjectOfType<PlayerScript>();
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		if (this.CompareTag ("BackGround")) {
			scrollSpeed = GameControlScript.Instance.BGScrollSpeed;
		} else {
			scrollSpeed = GameControlScript.Instance.scrollSpeed;
		}

	}

	// Update is called once per frame
	void Update () {
		if (!PS.OnGround) {
			rb.velocity = new Vector2 (scrollSpeed, 0f);
		} else {
			rb.velocity = Vector2.zero;
		}
	}
}
