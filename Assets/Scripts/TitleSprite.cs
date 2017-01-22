using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSprite : MonoBehaviour {

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
		this.setCharacterAnimatorController ();
		Debug.Log ("TSAWAKE");
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
}
