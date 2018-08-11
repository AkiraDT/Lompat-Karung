using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour {
	//Untuk button suara/music

	public Sprite musicOnSprite;
	public Sprite musicOffSprite;
	public GameObject MusicToggleButton;


	private MusicPlayer m_MusicPlayer;

	// Use this for initialization
	void Awake () {
		m_MusicPlayer = GameObject.FindObjectOfType<MusicPlayer> ();
		UpdateIcon ();
	}


	public void ToggleButton(){			//Ketika tombol di klik
		m_MusicPlayer.ToggleSound ();
		UpdateIcon ();
	}

	public void UpdateIcon(){		//icon button diubah
		if (PlayerPrefs.GetInt ("Muted") == 0) {		//On
			AudioListener.volume = 1;
			MusicToggleButton.GetComponent<Image> ().sprite = musicOnSprite;
		} else {		//Off
			AudioListener.volume = 0;
			MusicToggleButton.GetComponent<Image> ().sprite = musicOffSprite;
		}
	}
}
