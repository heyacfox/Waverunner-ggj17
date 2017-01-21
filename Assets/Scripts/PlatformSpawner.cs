using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

	public float spawnTimerStart;
	float spawnTimer;
	public GameObject largePlatform;
	public BeatManager bm;

	void Awake() {
		
		spawnTimer = spawnTimerStart;
	}

	// Update is called once per frame
	void Update () {
		/*
		spawnTimerStart = bm.beatTimerWN * 2;

		spawnTimer -= Time.deltaTime;
		if (spawnTimer < 0) {
			
			spawnTimer = spawnTimerStart;
			GameObject go = Instantiate (largePlatform, 
				new Vector3 (14, -4 + (Random.value*2)), 
				Quaternion.identity) as GameObject;
			PlatformMovement pm = go.GetComponent<PlatformMovement> ();
			pm.platformSpeedAsNegative = bm.beatTimerWN * -5f;


		}
		*/
	}

	public void spawnPlatform(string noteToSpawn) {
		GameObject go = Instantiate (largePlatform, 
			new Vector3 (14, -4 + (Random.value*2)), 
			Quaternion.identity) as GameObject;
		PlatformMovement pm = go.GetComponent<PlatformMovement> ();
		pm.GetComponent<Rigidbody2D>().velocity = new Vector2(bm.beatTimerWN * -10f, 0);
		pm.selfNote = noteToSpawn;

	}
}
