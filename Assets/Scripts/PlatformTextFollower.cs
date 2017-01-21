using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTextFollower : MonoBehaviour {

	public Transform platformToFollow;
	public bool clampToScreen = false;
	public float clampBorderSize = 0.05f;
	public bool useMainCamera = true;

	Camera cam;
	Transform thisTransform;
	Transform camTransform;

	void Start () {
		thisTransform = transform;
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		thisTransform.position = cam.WorldToViewportPoint (platformToFollow.position);
		if (this.transform.position.x <= -40) {
			Destroy (this);
		}

	}
}
