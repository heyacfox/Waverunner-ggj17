using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Profiling;

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
	public Text nextNoteText;

	public RuntimeAnimatorController CMO_anim;
	public RuntimeAnimatorController CWY_anim;
	public RuntimeAnimatorController AWO_anim;
	public RuntimeAnimatorController AMO_anim;
	public RuntimeAnimatorController BWO_anim;
	public RuntimeAnimatorController BMO_anim;
	public RuntimeAnimatorController AWY_anim;
	public RuntimeAnimatorController AMY_anim;
	public RuntimeAnimatorController BWY_anim;
	public RuntimeAnimatorController BMY_anim;


	void Awake() {
		rb2d = this.GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (0, 0);
		rb2d.gravityScale = gravityActual;
		if (InputChecker.instance != null) {
			if (InputChecker.instance.inputTypeSpaceHuh) {
				inputTypeSpace = true;
			} else {
				inputTypeSpace = false;
			}
		} else {
			inputTypeSpace = true;
		}
		if (NoteWatcher.instance != null) {
			NoteWatcher.instance.rc = this;
		}
		//this.nextNoteText.text = bm.chordNoteProgression.Peek ();

		this.setCharacterAnimatorController ();





	}

	void setCharacterAnimatorController() {

		float oldPoints = 0.27f;
		float genderPoints = 0.42f;
		Animator anim = this.GetComponent<Animator> ();

		float checkVal = Random.value;
		Debug.Log ("OldPoints:" + checkVal.ToString ());
		if (checkVal < oldPoints) {
			
			checkVal = Random.value;
			Debug.Log ("OldWomanPoints:" + checkVal.ToString ());
			if (checkVal < genderPoints) {
				//add old woman
				checkVal = Random.value;
				if (checkVal < 0.51) {
					anim.runtimeAnimatorController = AWO_anim;
				} else {
					anim.runtimeAnimatorController = BWO_anim;
				}

			} else {
				// add old man
				checkVal = Random.value;
				if (checkVal < 0.34) {
					anim.runtimeAnimatorController = CMO_anim;
				} else if (checkVal < 0.67) {
					anim.runtimeAnimatorController = AMO_anim;
				} else {
					anim.runtimeAnimatorController = BMO_anim;
				}
			}

		} else {
			checkVal = Random.value;
			Debug.Log ("YoungWomanPoints:" + checkVal.ToString ());
			if (checkVal < genderPoints) {
				//add young woman
				checkVal = Random.value;
				if (checkVal < 0.34) {
					anim.runtimeAnimatorController = CWY_anim;
				} else if (checkVal < 0.67) {
					anim.runtimeAnimatorController = AWY_anim;
				} else {
					anim.runtimeAnimatorController = BWY_anim;
				}

			} else {
				checkVal = Random.value;
				if (checkVal < 0.34) {
					anim.runtimeAnimatorController = AMY_anim;
				} else if (checkVal < 0.67) {
					anim.runtimeAnimatorController = BMY_anim;
				} else {
					//do nothing leave default anim
				}
			}
		}



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
					this.GetComponent<Animator> ().SetTrigger ("jump");
					this.GetComponent<Animator> ().ResetTrigger ("land");
					rb2d.gravityScale = 0;
				} else {
					linkedPM.playNoteFail ();
				}

			}
			if (Input.GetKeyUp ("space") || this.transform.position.y > 5) {
				characterFalls ();
			}
		}
		if (this.transform.position.y <= -15) {
			SceneManager.LoadScene ("CreditsScene");
		}
		if (this.transform.position.y > 4) {
			characterFalls ();
		}
	}

	void characterFalls() {
		rb2d.gravityScale = gravityActual;
		this.GetComponent<Animator> ().SetTrigger ("fall");
		this.GetComponent<BoxCollider2D> ().enabled = true;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "audioblock") {
			linkedPM = col.gameObject.GetComponent<PlatformMovement> ();
			//nextNoteText.text = nextNoteToHit;
			this.GetComponent<Animator> ().SetTrigger ("land");
			//Profiler.EndSample ("SceneSwap");	

			bm.checkToPlayBackingThenPlay ();
			//nextNoteToHit = bm.chordNoteProgression.Peek();
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
		Debug.Log ("Input Type Space Value:" + inputTypeSpace.ToString ());
		if (!inputTypeSpace) {
			if (!jumping) {
				if (nw.checkAnyKeyInChord (bm.currentChord)) {
					jumping = true;
					linkedPM.playNoteWin ();
					this.GetComponent<BoxCollider2D> ().enabled = false;
					rb2d.velocity = new Vector2 (0, jumpStrength);
					this.GetComponent<Animator> ().SetTrigger ("jump");
					this.GetComponent<Animator> ().ResetTrigger ("land");
					rb2d.gravityScale = 0;
				} else {
					linkedPM.playNoteFail ();
				}
			}
		}
	}

	public void allMidiKeysReleased() {
		if (!inputTypeSpace) {
			characterFalls ();
		}
	}
	

}
