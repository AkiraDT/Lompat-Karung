using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PijakanManagerScript : MonoBehaviour {
	public static PijakanManagerScript Instance;

	public GameObject[] PijakanPrefab;
	public float spawnRate = 4f;

	private float pijakanXMin = 5f;
	private float pijakanXMax = 7.5f;
	private float pijakanYMin = -5f;
	private float pijakanYMax = -3f;
	private GameObject LastPijakan;
	private GameObject[] Pijakans;
	private float timeIntervalSpawn;
	//private float spawnXpos = 5f;
	private int currentPijakan = 0;

	// Use this for initialization
	void Start () {
		if (Instance == null) {
			Instance = this;
		}else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	public void SpawnPijakan(){
		//timeIntervalSpawn = 0f;
		float spawnYpos = Random.Range (pijakanYMin, pijakanYMax);
		float spawnXpos = Random.Range (pijakanXMin, pijakanXMax);
		Vector2 pijakanPosition;

		if (LastPijakan != null) {
			pijakanPosition = new Vector2 (LastPijakan.transform.position.x + spawnXpos, spawnYpos);
		} else {
			pijakanPosition = new Vector2 (5f, spawnYpos);
		}

		LastPijakan = (GameObject) Instantiate (PijakanPrefab[currentPijakan], pijakanPosition, Quaternion.identity);

	}
}
