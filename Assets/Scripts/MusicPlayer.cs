using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	public AudioClip menuAudio;
	public AudioClip gameAudio;

	private AudioSource audioSource;
	public string[] sceneName;							//Nama Scene untuk dijadikan referensi music mana yang akan diputar
	private bool[] changeMusicHelper;		//agar music hanya ada 1 yang aktif (tidak overlap)

	
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		changeMusicHelper = new bool[sceneName.Length];
		for(int i=0; i<sceneName.Length; i++){
			changeMusicHelper [i] = true;
		}
			

		if (instance != null && instance != this) {		//jika ada lebih dari satu objek Music
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	void Update(){

		//Jika dalam scene Menu utama, cerita dan Score
		if (SceneManager.GetActiveScene ().name == sceneName [0] && changeMusicHelper[0]) {
			audioSource.clip = menuAudio;
			audioSource.Play ();
			changeMusicHelper [0] = false;
			changeMusicHelper [1] = true;
		} 
		//Jika dalam scene Game
		else if (SceneManager.GetActiveScene ().name == sceneName [1] && changeMusicHelper[1]) {
			audioSource.clip = gameAudio;
			audioSource.Play ();
			changeMusicHelper [0] = true;
			changeMusicHelper [1] = false;
		}

	}

	//Untuk menghidupkan dan mematikan music
	public void ToggleSound(){
		if (PlayerPrefs.GetInt ("Muted") == 0) {
			PlayerPrefs.SetInt ("Muted", 1);
			//dijadiin mute
		}else{
			PlayerPrefs.SetInt ("Muted", 0);
			//unmute
		}
	}
}
