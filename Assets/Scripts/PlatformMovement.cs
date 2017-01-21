﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

	Rigidbody2D rb2d;
	public float platformSpeedAsNegative;
	public List<AudioClip> cSharpMelodies;
	public List<AudioClip> cMelodies;
	public List<AudioClip> aMelodies;
	public List<AudioClip> GSharpMMelodies;
	Dictionary<string, List<AudioClip>> noteToAudioList;
	public string selfNote;
	AudioSource asource;



	void Awake() {
		rb2d = this.GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (platformSpeedAsNegative, 0);
		noteToAudioList = new Dictionary<string, List<AudioClip>> ();
		noteToAudioList.Add ("C", cMelodies);
		noteToAudioList.Add ("C#", cSharpMelodies);
		noteToAudioList.Add ("A", aMelodies);
		noteToAudioList.Add ("G#", GSharpMMelodies);
		asource = this.GetComponent<AudioSource> ();

	}

	public void playNoteWin() {
		List<AudioClip> selecterList = noteToAudioList [selfNote];
		asource.clip = selecterList [Random.Range (1, selecterList.Count)];
		//asource.clip = selecterList [0];
		asource.Play ();

	}

	void Update() {
		if (transform.position.x < -50) {
			Destroy (this);
		}
	}

	public void playNoteFail() {
		List<AudioClip> selecterList = noteToAudioList [selfNote];
		asource.clip = selecterList [0];
		asource.Play ();
	}
}
