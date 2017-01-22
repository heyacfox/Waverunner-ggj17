using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformSpawner : MonoBehaviour {

	public float spawnTimerStart;
	float spawnTimer;
	public GameObject largePlatform;
	public BeatManager bm;
	public GameObject platformTextFollower;

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

	public void spawnPlatform(string noteToSpawn, float heightBegin, float speedWidthOfSpawn) {
		GameObject go = Instantiate (largePlatform, 
			new Vector3 (14, heightBegin + (Random.value*2)), 
			Quaternion.identity) as GameObject;
		PlatformMovement pm = go.GetComponent<PlatformMovement> ();
		pm.GetComponent<Rigidbody2D>().velocity = new Vector2(bm.beatTimerWN * -10f, 0);
		pm.transform.localScale = new Vector3 ((Random.value * speedWidthOfSpawn) + .3f, 1, 1);
		pm.selfNote = noteToSpawn;
		//GameObject ptfo = Instantiate (platformTextFollower, Vector2.zero, Quaternion.identity) as GameObject;
		//ptfo.GetComponent<PlatformTextFollower> ().platformToFollow = go.transform;
		//tfo.GetComponent<GUIText> ().text = bm.chordNoteProgression.Peek();

	}

	public void spawnPlatforms(string noteToSpawn, float speedWidthOfSpawn) {
		spawnPlatform (noteToSpawn, -4, speedWidthOfSpawn);
		//spawnPlatform (noteToSpawn, -1);
		//spawnPlatform (noteToSpawn, 2);
	}
}
