﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunningCharacter : MonoBehaviour {

	Rigidbody2D rb2d;
	public float jumpStrength;
	public BeatManager bm;
	bool jumping = true;
	public float gravityActual;
	PlatformMovement linkedPM;
	public string nextNoteToHit;
	public bool inputTypeSpace;
	public NoteWatcher nw;
	public GUIText nextNoteText;

	void Awake() {
		rb2d = this.GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (0, 0);
		rb2d.gravityScale = gravityActual;

	}

	void Update() {
		if (inputTypeSpace) {
			if (validateInput () && !jumping) {
				//bm.checkIfPoint ();
				if (bm.checkIfPoint ()) {
					jumping = true;
					linkedPM.playNoteWin ();
					this.GetComponent<BoxCollider2D> ().enabled = false;
					rb2d.velocity = new Vector2 (0, jumpStrength);
					rb2d.gravityScale = 0;
				} else {
					linkedPM.playNoteFail ();
				}

			}
			if (Input.GetKeyUp ("space")) {
				rb2d.gravityScale = gravityActual;
				this.GetComponent<BoxCollider2D> ().enabled = true;
			}
		}
		if (this.transform.position.y <= -15) {
			SceneManager.LoadScene ("CreditsScene");
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "audioblock") {
			linkedPM = col.gameObject.GetComponent<PlatformMovement> ();
			nextNoteToHit = bm.chordNoteProgression.Peek();
			nextNoteText.text = nextNoteToHit;
			bm.checkToPlayBackingThenPlay ();
			/*
			if (bm.checkIfPoint ()) {
				pm.playNoteWin ();
			} else {
				pm.playNoteFail ();
			}
			*/
			jumping = false;
		}
	}

	bool validateInput() {
		if (inputTypeSpace) {
			return Input.GetKeyDown ("space");
		} else {
			return false;
		}
	}

	public void midiKeyPressed(int keyDown) {
		if (!inputTypeSpace) {
			if (!jumping) {
				if (nw.checkAnyKeyInChord (linkedPM.selfNote)) {
					jumping = true;
					linkedPM.playNoteWin ();
					this.GetComponent<BoxCollider2D> ().enabled = false;
					rb2d.velocity = new Vector2 (0, jumpStrength);
					rb2d.gravityScale = 0;
				} else {
					linkedPM.playNoteFail ();
				}
			}
		}
	}

	public void allMidiKeysReleased() {
		if (!inputTypeSpace) {
			rb2d.gravityScale = gravityActual;
			this.GetComponent<BoxCollider2D> ().enabled = true;
		}
	}
	

}
