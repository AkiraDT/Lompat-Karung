using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {
	GameObject[] CanvasObject = new GameObject[2];

	void Start () {
		CanvasObject[0] = GameObject.Find ("Canvas");
		CanvasObject[1] = GameObject.Find ("Option");

		if (CanvasObject [1] != null) {
			CanvasObject [1].SetActive (false);
		}
		Screen.orientation = ScreenOrientation.Landscape;
		if (SceneManager.GetActiveScene ().name == "HowToPlayScene" && PlayerPrefs.GetInt("IsFirstPlayed") != 1) {
			PlayerPrefs.SetInt ("IsFirstPlayed",1);
		}
	}

	public void LoadLevel(string sceneName){
		if(sceneName.Contains("MainGameScene")){
			if(PlayerPrefs.GetInt("IsFirstPlayed") != 1){
				sceneName = "HowToPlayScene";
			}
		}
		SceneManager.LoadScene (sceneName);
	}

	public void QuitRequest(){
		Application.Quit ();
	}
		
	public void OptionON(){
		CanvasObject[0].SetActive (false);
		CanvasObject[1].SetActive (true);
	}

	public void OptionOFF(){
		CanvasObject[0].SetActive (true);
		CanvasObject[1].SetActive (false);
	}
}
