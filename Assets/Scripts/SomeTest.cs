using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class SomeTest : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (MidiMaster.GetKeyDown (35)) {
			Debug.Log ("Bro Key");
		}
	}
}
