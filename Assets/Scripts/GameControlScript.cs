using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour {

	public static GameControlScript Instance;
	public float scrollSpeed;
	public GameObject ScoreHolder;
	public float BGScrollSpeed;

	private Text ScoreText;
	private int score;

	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	void Start(){
		ScoreText = ScoreHolder.GetComponent<Text> ();
		score = -1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(score<0)
			ScoreText.text = "0";
		else
			ScoreText.text = score.ToString ();
	}

	public int Score{
		set{ 
			score = value;
		}
		get{ 
			return score;
		}
	}
}
