using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Landscape;
		if (SceneManager.GetActiveScene ().name == "HowToPlayScene" && PlayerPrefs.GetInt("IsFirstPlayed") != 1) {
			PlayerPrefs.SetInt ("IsFirstPlayed",1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
