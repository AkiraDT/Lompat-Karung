using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour {

	private float count;
	private TextMeshProUGUI displayText;

	void Start () {
		displayText = GetComponent<TextMeshProUGUI> ();
	}

	void Update () {
		if (!GameControlScript.Instance.IsGameOver) {
			count = GameControlScript.Instance.countDownTimer;
			displayText.text = Mathf.Round (count).ToString ();
		}

		if (count <= 0.5f && !GameControlScript.Instance.IsGameOver) {
			GameControlScript.Instance.GameOver ();
		}
	}
}
