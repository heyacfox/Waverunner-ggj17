using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class InputChecker : MonoBehaviour {
	public static InputChecker instance = null;
	public bool inputTypeSpaceHuh;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this);
		}
		DontDestroyOnLoad (this);
	}

}
