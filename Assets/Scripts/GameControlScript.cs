﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControlScript : MonoBehaviour {

	public static GameControlScript Instance;
	public float scrollSpeed;
	public GameObject ScoreHolder1;
	public GameObject ScoreHolder2;
	public GameObject HighScoreHolder;
	public GameObject[] GameState;
	public float countDownTimer = 10.5f;

	private float countDownTimerT;
	private PlayerScript PS;
	private TextMeshProUGUI ScoreText1;
	private TextMeshProUGUI ScoreText2;
	private TextMeshProUGUI HighScoreText;
	private int score;
	private bool isGameOn;
	private bool isBGMove;
	private bool isGameOver;
	private MusicPlayer m_MusicPlayer;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
		isGameOn = true;
		Time.timeScale = 1;
		isGameOver = false;
	}

	void Start(){
		countDownTimerT = countDownTimer;
		PS = GameObject.FindObjectOfType<PlayerScript>();
		IsBGMove = false;
		if(ScoreHolder1 != null)
			ScoreText1 = ScoreHolder1.GetComponent<TextMeshProUGUI> ();

		if(ScoreHolder2 != null)
			ScoreText2 = ScoreHolder2.GetComponent<TextMeshProUGUI> ();

		if(HighScoreHolder != null)
			HighScoreText = HighScoreHolder.GetComponent<TextMeshProUGUI> ();
		score = -1;

		GameState [1].SetActive (false);
		GameState [2].SetActive (false);

		m_MusicPlayer = FindObjectOfType<MusicPlayer> ();
	}
	
	void FixedUpdate () {
		if (ScoreHolder1 != null) {
			if (score < 0)
				ScoreText1.text = "0";
			else
				ScoreText1.text = score.ToString ();
		}

		if (PS != null) {
			if (PS.OnGround && isGameOn && !isBGMove) {
				countDownTimer -= Time.deltaTime;
			}
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
		isGameOver = true;
		m_MusicPlayer.m_audioSource.Stop ();
		m_MusicPlayer.m_audioSource.PlayOneShot (m_MusicPlayer.gameOverAudio);
		PS.rbPlayer.velocity = Vector2.zero;
		PS.rbPlayer.gravityScale = 0;
		PS.OnGround = true;
		PS.GetComponentInChildren<LaunchArcRenderer> ().velocity = 0;
		//Invoke ("GameOverPopUp", 3f);		//if we wanted to have delay before the popup shown
		GameOverPopUp();
	}

	public void GameOverPopUp(){
		GameState [0].SetActive (false);
		GameState [1].SetActive (true);
		GameState [2].SetActive (false);
		SaveHighScore ();
	}

	public void Pause(){
		GameState [0].SetActive (false);
		GameState [2].SetActive (true);
		FindObjectOfType<SFXPlayer> ().m_audioSource.Stop ();
		Time.timeScale = 0;
	}

	public void Resume(){
		GameState [0].SetActive (true);
		GameState [2].SetActive (false);
		Time.timeScale = 1;
		FindObjectOfType<TouchInputMovement> ().CancelJump ();
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
		m_MusicPlayer.m_audioSource.clip = m_MusicPlayer.gameAudio;
		m_MusicPlayer.m_audioSource.Play ();
	}

	public bool IsGameOn{
		get{
			return isGameOn;
		}
		set{
			isGameOn = value;
		}
	}

	public bool IsBGMove{
		get{
			return isBGMove;
		}
		set{
			isBGMove = value;
		}
	}

	public bool IsGameOver{
		get{
			return isGameOver;
		}
		set{
			isGameOver = value;
		}
	}

	public void ResetTimer(){
		countDownTimer = countDownTimerT;
	}
}
