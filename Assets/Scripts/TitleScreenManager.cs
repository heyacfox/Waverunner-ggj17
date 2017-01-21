using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MidiJack;

public class TitleScreenManager : MonoBehaviour {

	public string sceneToLoad;
	public static TitleScreenManager instance = null;

	void Awake() {
		if (instance == null)
			instance = this;
		//DontDestroyOnLoad(this);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			InputChecker.instance.inputTypeSpaceHuh = true;
			SceneManager.LoadScene (sceneToLoad);
		}
		for (int i = 0; i < 128; i++) {
			if (MidiMaster.GetKeyDown (i)) {
				InputChecker.instance.inputTypeSpaceHuh = false;
				SceneManager.LoadScene (sceneToLoad);
			}
		}
	}
}
