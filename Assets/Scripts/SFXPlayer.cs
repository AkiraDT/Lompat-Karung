using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXPlayer : MonoBehaviour {

	static SFXPlayer instance = null;
	public AudioClip[] sfxAudio;							//0 = charge, 1 = jump, 2 = landing

	private AudioSource audioSource;
	private TouchInputMovement TIM;
	private PlayerScript player;

	void Awake(){
		audioSource = GetComponent<AudioSource> ();
	}

	void Start () {
		if (instance != null && instance != this) {		//jika ada lebih dari satu objek SFX
			Destroy (gameObject);
			print ("Duplicate SFX player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	//Untuk menghidupkan dan mematikan sfx
	public void ToggleSound(){
		if (PlayerPrefs.GetInt ("MutedSFX") == 0) {
			PlayerPrefs.SetInt ("MutedSFX", 1);
			//dijadiin mute
		}else{
			PlayerPrefs.SetInt ("MutedSFX", 0);
			//unmute
		}
	}

	public AudioSource m_audioSource{
		get{
			return audioSource;
		}
	}
}
