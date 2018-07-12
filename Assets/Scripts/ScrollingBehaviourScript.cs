using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBehaviourScript : MonoBehaviour {

	private PlayerScript PS;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		PS = GameObject.FindObjectOfType<PlayerScript>();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!PS.OnGround) {
			rb.velocity = new Vector2 (GameControlScript.Instance.scrollSpeed, 0f);
		} else {
			rb.velocity = Vector2.zero;
		}
	}
}
