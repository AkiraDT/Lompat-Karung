using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBehaviourScript : MonoBehaviour {
	public float localScrollSpeed;

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
		//if (this.CompareTag ("BackGround")) {
		//	scrollSpeed = GameControlScript.Instance.BGScrollSpeed;
		//} else {
			scrollSpeed = GameControlScript.Instance.scrollSpeed;
		//}

	}

	// Update is called once per frame
	void Update () {
		if (GameControlScript.Instance.IsGameOn) {
			//For Parallax
			if (this.CompareTag ("BackGround")) {
				if (GameControlScript.Instance.IsBGMove && GameControlScript.Instance.IsGameOn) {
					rb.velocity = new Vector2 (scrollSpeed - localScrollSpeed, 0f);
				} else if (!PS.OnGround) {
					rb.velocity = new Vector2 (scrollSpeed, 0f);
				} else {
					rb.velocity = Vector2.zero;
				}

			}
			else if(this.CompareTag ("ContinousBG")){
				rb.velocity = Vector2.zero;
				if (!PS.OnGround) {
					rb.velocity = new Vector2 (- Mathf.Abs(scrollSpeed) - localScrollSpeed, 0f);
				} else {
					rb.velocity = new Vector2 (-localScrollSpeed, 0f);
				}
			} 
			else {
				if (!PS.OnGround) {
					rb.velocity = new Vector2 (scrollSpeed, 0f);
				} else {
					rb.velocity = Vector2.zero;
				}
			}
		}else{
			if (!CompareTag ("ContinousBG")) {
				//rb.velocity = Vector2.zero;
				//rb.velocity = new Vector2 (- Mathf.Abs(scrollSpeed) - localScrollSpeed, 0f);
			}
		}
	}
}
