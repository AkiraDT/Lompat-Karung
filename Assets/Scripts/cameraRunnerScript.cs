using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRunnerScript : MonoBehaviour {

	public Transform player;
	private PlayerScript PS;
	private Rigidbody2D rb;

	void Start(){
		PS = GameObject.FindObjectOfType<PlayerScript>();
		rb = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		if (!PS.OnGround) {
			rb.velocity = new Vector2 (GameControlScript.Instance.scrollSpeed, 0f);
		} else {
			rb.velocity = Vector2.zero;
			if (transform.position.x <= player.position.x + 6) {
				transform.Translate (Vector3.right * 0.1f);
			}
		}
	}
}
