﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour {

	public static GameControlScript Instance;
	public float scrollSpeed;
	public GameObject ScoreHolder1;
	public GameObject ScoreHolder2;
	public GameObject HighScoreHolder;

	public float BGScrollSpeed;
	public GameObject[] GameState;


	private Text ScoreText1;
	private Text ScoreText2;
	private Text HighScoreText;
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
		if(ScoreHolder1 != null)
			ScoreText1 = ScoreHolder1.GetComponent<Text> ();

		if(ScoreHolder2 != null)
			ScoreText2 = ScoreHolder2.GetComponent<Text> ();

		if(HighScoreHolder != null)
			HighScoreText = HighScoreHolder.GetComponent<Text> ();
		score = -1;

		GameState [1].SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (ScoreHolder1 != null) {
			if (score < 0)
				ScoreText1.text = "0";
			else
				ScoreText1.text = score.ToString ();
		}
	}

	public int Score{
		set{ 
			score = value;
		}
		get{ 
			return score;
		}
	}

	public void GameOver(){
		GameState [0].SetActive (false);
		GameState [1].SetActive (true);
		SaveHighScore ();
	}

	public void SaveHighScore(){
		if (score > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", score);
		}
		ScoreText2.text = score.ToString ();
		HighScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString ();
	}


	public void TryAgain(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}
