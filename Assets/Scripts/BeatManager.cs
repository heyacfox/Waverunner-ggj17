using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatManager : MonoBehaviour {

	public Text beatText;
	public Text pointText;
	public float beatTimerStartQuarterNote;
	float beatTimerQN;
	float beatTimerHN;
	public float beatTimerWN;
	float beatTimerEN;
	public float acceptableVariance;
	bool pointGainedThisBeat = false;
	float activeTime;
	float currentBeatTimeTracking;
	int points = 0;
	public AudioClip backingTrack;
	AudioSource asource;
	public PlatformSpawner ps;
	public Queue<string> chordNoteProgression;
	public float realTimeScale;


	// Use this for initialization
	void Start () {
		beatTimerQN = beatTimerStartQuarterNote;
		beatTimerEN = beatTimerQN / 2;
		beatTimerHN = beatTimerQN * 2;
		beatTimerWN = beatTimerQN * 4;
		pointText.text = points.ToString();
		asource = this.GetComponent<AudioSource> ();
		Time.timeScale = realTimeScale;


		//setting up chord progression
		chordNoteProgression = new Queue<string>();
		chordNoteProgression.Enqueue ("C");
		chordNoteProgression.Enqueue ("C#");




	}

	public bool checkIfPoint() {
		if (!pointGainedThisBeat) {
			if (activeTime - acceptableVariance <= 0 ||
			    activeTime + acceptableVariance >= currentBeatTimeTracking) {
				points++;
				pointText.text = points.ToString ();
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	public void checkToPlayBackingThenPlay() {
		if (!asource.isPlaying) {
			StartCoroutine (PeriodicUpdater(beatTimerWN));
			string cnp = chordNoteProgression.Dequeue ();
			chordNoteProgression.Enqueue (cnp);
			asource.clip = backingTrack;
			asource.Play ();

		}
	}

	
	// Update is called once per frame

	IEnumerator PeriodicUpdater(float beatTimerToTrack)
	{
		//Debug.Log ("StartedCoroutine");
		currentBeatTimeTracking = beatTimerToTrack;

		float curTime = Time.time;
		while (true) {
			//Debug.Log (Time.time.ToString ());
			float timeText = curTime + beatTimerToTrack - Time.time;
			beatText.text = timeText.ToString ();
			activeTime = timeText;
			if (Time.time >= curTime + beatTimerToTrack) {
				//Debug.Log ("Do something based on timer");
				curTime += beatTimerToTrack;
				string nextNote = chordNoteProgression.Dequeue ();
				ps.spawnPlatform (nextNote);
				chordNoteProgression.Enqueue (nextNote);
				pointGainedThisBeat = false;
			}
			yield return new WaitForFixedUpdate ();
		}

	}
}
