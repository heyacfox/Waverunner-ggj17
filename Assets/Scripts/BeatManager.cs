using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatManager : MonoBehaviour {

	public Text beatText;
	public float beatTimerStartQuarterNote;
	float beatTimerQN;
	float beatTimerHN;
	float beatTimerWN;
	float beatTimerEN;

	// Use this for initialization
	void Start () {
		beatTimerQN = beatTimerStartQuarterNote;
		beatTimerEN = beatTimerQN / 2;
		beatTimerHN = beatTimerQN * 2;
		beatTimerWN = beatTimerQN * 4;
		StartCoroutine (PeriodicUpdater(beatTimerHN));
	}


	
	// Update is called once per frame

	IEnumerator PeriodicUpdater(float beatTimerToTrack)
	{
		//Debug.Log ("StartedCoroutine");

		float curTime = Time.time;
		while (true) {
			Debug.Log (Time.time.ToString ());
			float timeText = curTime + beatTimerToTrack - Time.time;
			beatText.text = timeText.ToString ();
			if (Time.time >= curTime + beatTimerToTrack) {
				//Debug.Log ("Do something based on timer");
				curTime += beatTimerToTrack;
			}
			yield return new WaitForFixedUpdate ();
		}

	}
}
