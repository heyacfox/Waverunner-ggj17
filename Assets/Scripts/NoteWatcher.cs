using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class NoteWatcher : MonoBehaviour {

	//public int noteNumber;
	public Transform noteSpawnHeader;
	public GameObject noteCheckSpawnPrefab;
	public RunningCharacter rc;
	public List<int> keysCurrentlyPressed;

	Dictionary<string, int> noteToStartKey;
	Dictionary<string, List<string>> chordToKeys;

	void Awake() {
		noteToStartKey = new Dictionary<string, int> ();
		noteToStartKey.Add ("C", 0);
		noteToStartKey.Add ("C#", 1);
		noteToStartKey.Add ("D", 2);
		noteToStartKey.Add ("D#", 3);
		noteToStartKey.Add ("E", 4);
		noteToStartKey.Add ("F", 5);
		noteToStartKey.Add ("F#", 6);
		noteToStartKey.Add ("G", 7);
		noteToStartKey.Add ("G#", 8);
		noteToStartKey.Add ("A", 9);
		noteToStartKey.Add ("A#", 10);
		noteToStartKey.Add ("B", 11);

		chordToKeys = new Dictionary<string, List<string>>();



		List<string> newStrings = new List<string> ();
		newStrings.Add ("C#");

		keysCurrentlyPressed = new List<int> ();


		//Spawn 128 note checkers.

		for (int i = 0; i < 128; i++) {
			GameObject go = Instantiate (noteCheckSpawnPrefab, noteSpawnHeader) as GameObject;
			NoteCheck nc = go.GetComponent<NoteCheck> ();
			nc.nw = this;
			nc.noteToCheck = i;
		}

	}

	public bool checkOneKey(int key) {
		return MidiMaster.GetKeyDown (key);
	}

	//receives the key down messages from the note checkers
	public void anyKeyDown(int keyDown) {
		Debug.Log ("Key Pressed!:" + keyDown.ToString ());
		keysCurrentlyPressed.Add (keyDown);
		rc.midiKeyPressed (keyDown);
	}

	public void anyKeyUp(int keyUp) {
		
		keysCurrentlyPressed.Remove (keyUp);

		if (keysCurrentlyPressed.Count == 0) {
			rc.allMidiKeysReleased ();
		}


	}

	public bool checkAnyKeyInChord(string chord) {
		//whatever
		return false;
	}

	public bool checkAllKeysInChord(string chord) {
		List<string> keysToCheck = chordToKeys [chord];
		//whatever
		return false;
	}

	//C



	//returns true if any of the keys of the note are playing
	public bool checkAllNotesInList(List<string> notes) {
		foreach (string s in notes) {
			if (!checkAllKeysOfNote (s)) {
				return false;
			}
		}
		return true;
	}

	public bool checkInChord(string chord) {
		return false;
	}

	//returns true if one of the keys of a specific note are playing
	public bool checkAllKeysOfNote(string note) {
		int rotatingCheck = noteToStartKey [note];
		while (rotatingCheck < 128) {
			if (checkOneKey (rotatingCheck)) {
				return true;
			}
			rotatingCheck++;
		}
		return false;
	}
}
