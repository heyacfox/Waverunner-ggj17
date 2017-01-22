using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class NoteCheck : MonoBehaviour {

	public NoteWatcher nw;
	public int noteToCheck;
	public bool verifyCheck = false;

	void Awake() {
		Debug.Log ("I'M WIDE AWAKE");
	}

	void Update() {
		verifyCheck = true;
		if (noteToCheck == 0) {
			Debug.Log (MidiMaster.GetKeyDown (MidiChannel.All, noteToCheck).ToString());
		}
		if (MidiMaster.GetKeyDown (noteToCheck)) {
			Debug.Log ("KeyDownHappened with" + noteToCheck.ToString());
			nw.anyKeyDown (noteToCheck);
		}
		if (MidiMaster.GetKeyUp (noteToCheck)) {
			nw.anyKeyUp (noteToCheck);
		}
	}

}
