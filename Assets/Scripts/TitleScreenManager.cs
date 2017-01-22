using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MidiJack;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour {

	public string sceneToLoad;
	public static TitleScreenManager instance = null;
	Text winLoseText;

	void Awake() {
		if (instance == null)
			instance = this;
		//DontDestroyOnLoad(this);
		Profiler.BeginSample("SceneSwap");

		GameObject go = GameObject.Find ("WinLoseText");
		if (go != null) {
			if (InputChecker.instance.winHuh) {
				go.GetComponent<Text> ().text = "You Win!";
			} else {
				go.GetComponent<Text> ().text = "Try Again!";
			}
		}

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			InputChecker.instance.inputTypeSpaceHuh = true;
			SceneManager.LoadScene (sceneToLoad);
		}
		/*
		for (int i = 0; i < 128; i++) {
			if (MidiMaster.GetKeyDown (i)) {
				InputChecker.instance.inputTypeSpaceHuh = false;
				SceneManager.LoadScene (sceneToLoad);
			}
		}
		*/

	}

	public void goWithMIDI() {
		InputChecker.instance.inputTypeSpaceHuh = false;
		SceneManager.LoadScene (sceneToLoad);
	}
}
