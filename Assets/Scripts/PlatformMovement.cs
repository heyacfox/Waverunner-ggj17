﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

	Rigidbody2D rb2d;
	public float platformSpeedAsNegative;

	void Awake() {
		rb2d = this.GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (platformSpeedAsNegative, 0);

	}
}