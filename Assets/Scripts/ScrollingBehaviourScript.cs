using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBehaviourScript : MonoBehaviour {
	public float localScrollSpeed;

	private PlayerScript PS;
	private Rigidbody2D rb;
	private float scrollSpeed;

	void Start () {
		PS = GameObject.FindObjectOfType<PlayerScript>();
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		scrollSpeed = GameControlScript.Instance.scrollSpeed;
	}

	void Update () {
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
	}
}
