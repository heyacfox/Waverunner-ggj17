using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class InputChecker : MonoBehaviour {
	public static InputChecker instance = null;
	public bool inputTypeSpaceHuh;
	public bool winHuh;
	public float platformSizeMultiplier;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}

}
