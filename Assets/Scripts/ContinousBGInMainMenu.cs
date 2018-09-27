using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousBGInMainMenu : MonoBehaviour {
	public float localScrollSpeed;

	private Rigidbody2D rb;
	private float scrollSpeed;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.velocity = new Vector2 (-localScrollSpeed, 0f);
	}
}
