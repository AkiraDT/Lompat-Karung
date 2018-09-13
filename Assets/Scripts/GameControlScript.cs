using System.Collections;
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
	//public float BGScrollSpeed;
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

	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
		isGameOn = true;
		Time.timeScale = 1;
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
	}
	
	// Update is called once per frame
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
		//BGScrollSpeed = scrollSpeed - 2;

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
		GameState [2].SetActive (false);
		SaveHighScore ();
		isGameOn = false;
		Time.timeScale = 0;
	}

	public void Pause(){
		GameState [0].SetActive (false);
		GameState [2].SetActive (true);
		isGameOn = false;
		Time.timeScale = 0;
	}

	public void Resume(){
		GameState [0].SetActive (true);
		GameState [2].SetActive (false);
		isGameOn = true;
		Time.timeScale = 1;
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

	public void ResetTimer(){
		countDownTimer = countDownTimerT;
	}
}
