using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PijakanManagerScript : MonoBehaviour {
	public GameObject[] PijakanPrefab;
	public int pijakanSize = 3;
	public float spawnRate = 4f;
	public float pijakanXMin = 5f;
	public float pijakanXMax = 7f;
	public float pijakanYMin = -5f;
	public float pijakanYMax = -3f;


	private GameObject[] Pijakans;
	private float timeIntervalSpawn;
	//private float spawnXpos = 5f;
	private int currentPijakan = 0;

	// Use this for initialization
	void Start () {
		Pijakans = new GameObject[pijakanSize];
		float xPos = -15f;
		for(int i = 0; i<pijakanSize; i++){
			Vector2 pijakanPosition = new Vector2 (xPos,25f);
			Pijakans [i] = (GameObject)Instantiate (PijakanPrefab[i], pijakanPosition, Quaternion.identity);
			xPos += 5f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//timeIntervalSpawn += Time.deltaTime;

		//if(timeIntervalSpawn >= spawnRate){
		//SpawnPijakan();
		//}
	}

	public void SpawnPijakan(){
		//timeIntervalSpawn = 0f;
		float spawnYpos = Random.Range (pijakanYMin, pijakanYMax);
		float spawnXpos = Random.Range (pijakanXMin, pijakanXMax);
		Pijakans [currentPijakan].transform.position = new Vector2 (spawnXpos, spawnYpos);
		currentPijakan++;
		if (currentPijakan >= pijakanSize) {
			currentPijakan = 0;
		}
	}
}
