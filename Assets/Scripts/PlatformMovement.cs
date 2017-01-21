﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

	Rigidbody2D rb2d;
	public float platformSpeedAsNegative;
	public List<AudioClip> cSharpMelodies;
	public List<AudioClip> cMelodies;
	Dictionary<string, List<AudioClip>> noteToAudioList;
	public string selfNote;
	AudioSource asource;



	void Awake() {
		rb2d = this.GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (platformSpeedAsNegative, 0);
		noteToAudioList = new Dictionary<string, List<AudioClip>> ();
		noteToAudioList.Add ("C", cMelodies);
		noteToAudioList.Add ("C#", cSharpMelodies);

	}

	public void playNote() {
		List<AudioClip> selecterList = noteToAudioList [selfNote];
		asource.clip = selecterList [Random.Range (0, selecterList.Count)];
		asource.Play ();

	}
}
