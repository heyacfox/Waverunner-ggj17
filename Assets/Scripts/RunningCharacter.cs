using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningCharacter : MonoBehaviour {

	Rigidbody2D rb2d;
	public float jumpStrength;
	public BeatManager bm;
	bool jumping = true;

	void Awake() {
		rb2d = this.GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (0, 0);

	}

	void Update() {
		if (Input.GetKeyDown ("space") && !jumping) {
			//bm.checkIfPoint ();
			jumping = true;
			rb2d.velocity = new Vector2 (0, jumpStrength);
			rb2d.gravityScale = 0;
		}
		if (Input.GetKeyUp ("space")) {
			rb2d.gravityScale = 1;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "audioblock") {
			PlatformMovement pm = col.gameObject.GetComponent<PlatformMovement> ();
			bm.checkToPlayBackingThenPlay ();
			if (bm.checkIfPoint ()) {
				pm.playNoteWin ();
			} else {
				pm.playNoteFail ();
			}
			jumping = false;
		}
	}
	

}
