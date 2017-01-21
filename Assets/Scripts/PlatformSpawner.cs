using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

	float timeLeft = 6;
	public GameObject largePlatform;

	// Update is called once per frame
	void Update () {

		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			
			timeLeft = 10;
			Instantiate (largePlatform, new Vector3 (5, -2 + Random.value), Quaternion.identity);
		}
	}
}
