using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Landscape;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel(string sceneName){
		if(name.Contains("MainMenuScene")){
			if(PlayerPrefs.GetInt("IsFirstPlayed") != 1){
				sceneName = "HowToPlayeScene";
			}
		}

		SceneManager.LoadScene (sceneName);
	}

	public void QuitRequest(){
		Application.Quit ();
	}
}
