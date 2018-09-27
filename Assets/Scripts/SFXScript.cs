using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXScript : MonoBehaviour {
	//Untuk button sfx

	public Sprite sfxOnSprite;
	public Sprite sfxOffSprite;
	public GameObject SFXToggleButton;


	private SFXPlayer m_SFXPlayer;

	void Awake () {
		m_SFXPlayer = GameObject.FindObjectOfType<SFXPlayer> ();
		UpdateIcon ();
	}


	public void ToggleButton(){			//Ketika tombol di klik
		m_SFXPlayer.ToggleSound ();
		UpdateIcon ();
	}

	public void UpdateIcon(){		//icon button diubah
		if (PlayerPrefs.GetInt ("MutedSFX") == 0) {		//On
			m_SFXPlayer.m_audioSource.volume = 1;
			SFXToggleButton.GetComponent<Image> ().sprite = sfxOnSprite;
		} else {		//Off
			m_SFXPlayer.m_audioSource.volume = 0;
			SFXToggleButton.GetComponent<Image> ().sprite = sfxOffSprite;
		}
	}
}
