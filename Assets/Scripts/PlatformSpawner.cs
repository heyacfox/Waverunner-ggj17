using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

	public float spawnTimerStart;
	float spawnTimer;
	public GameObject largePlatform;

	void Awake() {
		spawnTimer = spawnTimerStart;
	}

	// Update is called once per frame
	void Update () {

		spawnTimer -= Time.deltaTime;
		if (spawnTimer < 0) {
			
			spawnTimer = spawnTimerStart;
			Instantiate (largePlatform, new Vector3 (5, -2 + Random.value), Quaternion.identity);
		}
	}
}
