﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeatManager : MonoBehaviour {

	public Text beatText;
	public Text pointText;
	public float beatTimerStartQuarterNote;
	float beatTimerQN;
	float beatTimerHN;
	float beatTimerDHPDQ;
	float beatTimerEPW;
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
	public Queue<float> chordChangeTimings;
	//chord change timings will be
	// 1 - Quarter Note
	// 2 - Half Note
	// 4 - Whole Note
	// 0.5 - Eighth note
	// (measures amount of time to the enxt note)
	public float realTimeScale;
	public RunningCharacter rc;
	public string currentChord;
	public Transform chordPanel;
	public Text chordTextPrefab;
	public Image FadeImg;
	public float fadeSpeed = 1.5f;


	void FadeToBlack()
	{
		FadeImg.color = Color.Lerp (FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	public IEnumerator EndSceneRoutine(string sceneName) {
		FadeImg.enabled = true;
	
		do {
			FadeToBlack ();
			if (FadeImg.color.a >= 0.95f) {
				SceneManager.LoadScene (sceneName);
				yield break;
			} else {
				yield return null;
			}
		} while (true);
	}

	// Use this for initialization
	void Start () {
		beatTimerQN = beatTimerStartQuarterNote;
		beatTimerEN = beatTimerQN / 2;
		beatTimerDHPDQ = beatTimerQN * 3.5f;
		beatTimerEPW = beatTimerQN * 4.5f;
		beatTimerHN = beatTimerQN * 2;
		beatTimerWN = beatTimerQN * 4;
		pointText.text = points.ToString();
		asource = this.GetComponent<AudioSource> ();
		Time.timeScale = realTimeScale;
		FadeImg.rectTransform.localScale = new Vector2 (Screen.width, Screen.height);


		//setting up chord progression
		chordNoteProgression = new Queue<string>();
		/*
		chordNoteProgression.Enqueue ("C#");
		chordNoteProgression.Enqueue ("C");

		chordNoteProgression.Enqueue ("A");
		chordNoteProgression.Enqueue ("G#");
		chordNoteProgression.Enqueue ("C#");
		chordNoteProgression.Enqueue ("C");

		//chordNoteProgression.Enqueue ("E");
		chordNoteProgression.Enqueue ("C");
		*/

		chordNoteProgression.Enqueue("CSmin9");
		chordNoteProgression.Enqueue("Cmaj7");
		chordNoteProgression.Enqueue("Amaj7");
		chordNoteProgression.Enqueue("GSm7");
		chordNoteProgression.Enqueue("CSmin9");
		chordNoteProgression.Enqueue("Cmaj7");
		chordNoteProgression.Enqueue("Dmaj9");
		chordNoteProgression.Enqueue("Emaj9");
		chordNoteProgression.Enqueue("CSmin9");
		chordNoteProgression.Enqueue("Cmaj7");
		chordNoteProgression.Enqueue("Amaj7");
		chordNoteProgression.Enqueue("GSm7");
		chordNoteProgression.Enqueue("CSmin9");
		chordNoteProgression.Enqueue("Cmaj7");
		chordNoteProgression.Enqueue("Dmaj9");
		chordNoteProgression.Enqueue("Emaj9");
		chordNoteProgression.Enqueue("CSmin9");
		chordNoteProgression.Enqueue("Cmaj7");
		chordNoteProgression.Enqueue("Amaj7");
		chordNoteProgression.Enqueue("GSm7");
		chordNoteProgression.Enqueue("CSmin9");
		chordNoteProgression.Enqueue("Cmaj7");
		chordNoteProgression.Enqueue("DSmin7b5");
		chordNoteProgression.Enqueue("FSmin7");
		chordNoteProgression.Enqueue("GS");
		chordNoteProgression.Enqueue("Emaj7S5");
		chordNoteProgression.Enqueue("CSmin9");
		chordNoteProgression.Enqueue ("FINAL");



		chordChangeTimings = new Queue<float> ();
		/*
		chordChangeTimings.Enqueue (beatTimerWN);
		chordChangeTimings.Enqueue (beatTimerWN);
		chordChangeTimings.Enqueue (beatTimerWN);
		chordChangeTimings.Enqueue (beatTimerWN);
		chordChangeTimings.Enqueue (beatTimerWN);
		chordChangeTimings.Enqueue (beatTimerWN);
		chordChangeTimings.Enqueue (beatTimerWN);
		*/

		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerHN);
		chordChangeTimings.Enqueue(beatTimerHN);
		chordChangeTimings.Enqueue(beatTimerHN);
		chordChangeTimings.Enqueue(beatTimerHN);
		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerDHPDQ);
		chordChangeTimings.Enqueue(beatTimerEPW);
		chordChangeTimings.Enqueue(beatTimerWN);
		chordChangeTimings.Enqueue(beatTimerWN);
		chordChangeTimings.Enqueue(beatTimerHN);
		initializeChordQueueList ();

		rc.nextNoteText.text = this.chordNoteProgression.Peek ();


	}

	void initializeChordQueueList() {
		string chordPull = "start";
		string dequeuedNote;
		float timechange;
		GameObject go;
		Text txt;
		while (!chordPull.Equals ("FINAL")) {
			timechange = chordChangeTimings.Dequeue ();
			dequeuedNote = chordNoteProgression.Dequeue ();
			txt = Instantiate (chordTextPrefab, chordPanel);
			txt.text = dequeuedNote;
			txt.fontSize = Mathf.RoundToInt(timechange * 40f);
			chordPull = dequeuedNote;
			chordNoteProgression.Enqueue (dequeuedNote);
			chordChangeTimings.Enqueue (timechange);
		}

		txt = Instantiate (chordTextPrefab, chordPanel);
		txt.text = "FINAL";
		chordNoteProgression.Enqueue ("FINAL");




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
			float nextChange = chordChangeTimings.Dequeue ();

			chordChangeTimings.Enqueue (nextChange);
			Destroy(chordPanel.GetChild (0).gameObject);
			StartCoroutine (PeriodicUpdater(nextChange));
			string cnp = chordNoteProgression.Dequeue ();
			currentChord = cnp;
			rc.nextNoteText.text = cnp;
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
			//beatText.text = timeText.ToString ();
			activeTime = timeText;
			if (Time.time >= curTime + beatTimerToTrack) {
				//Debug.Log ("Do something based on timer");
				float nextChange = chordChangeTimings.Dequeue ();
				curTime += nextChange;
				chordChangeTimings.Enqueue (nextChange);
				string nextNote = chordNoteProgression.Dequeue ();
				if (nextNote.Equals("FINAL")) {
					InputChecker.instance.winHuh = true;
					StartCoroutine ("EndSceneRoutine", "CreditsScene");
				}
				currentChord = nextNote;
				Destroy(chordPanel.GetChild (0).gameObject);
				rc.nextNoteText.text = currentChord;
				//rc.nextNoteText.fontSize = 10 * nextChange;
				ps.spawnPlatforms (nextNote, nextChange);
				chordNoteProgression.Enqueue (nextNote);


				pointGainedThisBeat = false;
			}
			yield return new WaitForFixedUpdate ();
		}

	}

	IEnumerator swapScene() {


		yield return null;
	}


}
