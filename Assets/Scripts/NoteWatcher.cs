using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class NoteWatcher : MonoBehaviour {

	//public int noteNumber;

	public static NoteWatcher instance = null;
	public Transform noteSpawnHeader;
	public GameObject noteCheckSpawnPrefab;
	public RunningCharacter rc;
	public List<int> keysCurrentlyPressed;

	Dictionary<string, int> noteToStartKey;
	Dictionary<string, List<string>> chordToKeys;

	void Awake() {

		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);

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
		newStrings.Add("C#");
		newStrings.Add("E");
		newStrings.Add("B");
		newStrings.Add("D#");
		chordToKeys.Add ("CSmin9", newStrings);

		newStrings = new List<string> ();
		newStrings.Add("C");
		newStrings.Add("E");
		newStrings.Add("B");
		newStrings.Add("G");
		chordToKeys.Add ("Cmaj7", newStrings);

		newStrings = new List<string> ();
		newStrings.Add("A");
		newStrings.Add("C#");
		newStrings.Add("G#");
		newStrings.Add("E");
		chordToKeys.Add ("Amaj7", newStrings);

		newStrings = new List<string> ();
		newStrings.Add("G#");
		newStrings.Add("B");
		newStrings.Add("F#");
		newStrings.Add("D#");
		chordToKeys.Add ("GSm7", newStrings);

		newStrings = new List<string> ();
		newStrings.Add("D");
		newStrings.Add("F#");
		newStrings.Add("C#");
		newStrings.Add("E");
		chordToKeys.Add ("Dmaj9", newStrings);

		newStrings = new List<string> ();
		newStrings.Add("E");
		newStrings.Add("G#");
		newStrings.Add("D#");
		newStrings.Add("F#");
		chordToKeys.Add ("Emaj9", newStrings);

		newStrings = new List<string> ();
		newStrings.Add("D#");
		newStrings.Add("A");
		newStrings.Add("C#");
		newStrings.Add("F#");
		chordToKeys.Add ("DSmin7b5", newStrings);

		newStrings = new List<string> ();
		newStrings.Add("F#");
		newStrings.Add("C#");
		newStrings.Add("E");
		newStrings.Add("A");
		chordToKeys.Add ("FSmin7", newStrings);

		newStrings = new List<string> ();
		newStrings.Add("G#");
		newStrings.Add("C");
		newStrings.Add("D#");
		chordToKeys.Add ("GS", newStrings);

		newStrings = new List<string> ();
		newStrings.Add("E");
		newStrings.Add("C");
		newStrings.Add("D#");
		newStrings.Add("G#");
		newStrings.Add("B");
		chordToKeys.Add ("Emaj7S5", newStrings);

		keysCurrentlyPressed = new List<int> ();


		//Spawn 128 note checkers.

		for (int i = 0; i < 128; i++) {
			GameObject go = Instantiate (noteCheckSpawnPrefab, noteSpawnHeader) as GameObject;
			NoteCheck nc = go.GetComponent<NoteCheck> ();
			nc.nw = this;
			nc.noteToCheck = i;
		}

		if (rc != null) {
			rc.allMidiKeysReleased ();
		}
	}

	void Update() {
		//bool topcheck = false;
		for (int i = 0; i < 128; i++) {
			if (MidiDriver.Instance.GetKeyDown(MidiChannel.All, i)) {
				Debug.Log ("KEYPRESSED");
				//topcheck = true;
			}
		}
	}

	public bool checkOneKey(int key) {
		return MidiMaster.GetKeyDown (key);
	}

	//receives the key down messages from the note checkers
	public void anyKeyDown(int keyDown) {
		Debug.Log ("Key Pressed!:" + keyDown.ToString ());
		keysCurrentlyPressed.Add (keyDown);
		if (rc != null) {
			rc.midiKeyPressed (keyDown);
		}
		if (TitleScreenManager.instance != null) {
			TitleScreenManager.instance.goWithMIDI ();
		}
	}

	public void anyKeyUp(int keyUp) {
		
		keysCurrentlyPressed.Remove (keyUp);

		if (rc != null) {
			rc.allMidiKeysReleased ();
		}
		/*
		if (keysCurrentlyPressed.Count == 0) {
			if (rc != null) {
				rc.allMidiKeysReleased ();
			}
		}
		*/


	}

	public bool checkAnyKeyInChord(string chord) {
		Debug.Log ("Checking Chord:" + chord);
		return checkAnyNotesInList (chordToKeys [chord]);
	}

	public bool checkAnyNotesInList(List<string> notes) {
		foreach (string s in notes) {
			if (checkAllKeysOfNote (s)) {
				return true;
			}
		}
		return false;
	}

	public bool checkAllKeysInChord(string chord) {
		//Debug.Log ("Chord Checking:" + chord);
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
			rotatingCheck += 12;
		}
		return false;
	}
}
