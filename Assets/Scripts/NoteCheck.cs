using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class NoteCheck : MonoBehaviour {

	public NoteWatcher nw;
	public int noteToCheck;

	void Update() {
		if (MidiMaster.GetKeyDown (noteToCheck)) {
			Debug.Log ("KeyDownHappened with" + noteToCheck.ToString());
			nw.anyKeyDown (noteToCheck);
		}
		if (MidiMaster.GetKeyUp (noteToCheck)) {
			nw.anyKeyUp (noteToCheck);
		}
	}

}
