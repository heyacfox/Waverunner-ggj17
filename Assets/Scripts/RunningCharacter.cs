using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningCharacter : MonoBehaviour {

	Rigidbody2D rb2d;
	public float jumpHeight;
	public BeatManager bm;

	void Awake() {
		rb2d = this.GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (0, 0);

	}

	void Update() {
		if (Input.GetKeyDown ("space")) {
			bm.checkIfPoint ();
			rb2d.velocity = new Vector2 (0, jumpHeight);
		}
	}
	

}
