using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	float count;
	private PlayerScript PS;
	private Text displayText;

	// Use this for initialization
	void Start () {
		count = GameControlScript.Instance.countDownTimer;
		PS = GameObject.FindObjectOfType<PlayerScript>();
		displayText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(PS.OnGround){
			count -= Time.deltaTime;
		}

		displayText.text = Mathf.Round (count).ToString ();

		if (count <= 0) {
			GameControlScript.Instance.GameOver ();
		}
	}
}
