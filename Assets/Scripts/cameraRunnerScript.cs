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

	void Update () {
		if (!PS.OnGround) {
			rb.velocity = new Vector2 (GameControlScript.Instance.scrollSpeed, 0f);
		} else {		//camera will scroll back to player
			rb.velocity = Vector2.zero;
			if (transform.position.x < player.position.x + 6 && !GameControlScript.Instance.IsGameOver) {
				transform.Translate (Vector3.right * 0.3f);
				GameControlScript.Instance.IsBGMove = true;
			} else {
				GameControlScript.Instance.IsBGMove = false;
			}
		}
	}
}
