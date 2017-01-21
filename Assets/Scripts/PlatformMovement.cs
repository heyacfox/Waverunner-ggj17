using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

	Rigidbody2D rb2d;
	public float platformSpeedAsNegative;
	public List<AudioClip> CSmin9;
	public List<AudioClip> Cmaj7;
	public List<AudioClip> Amaj7;
	public List<AudioClip> GSm7;
	public List<AudioClip> Dmaj9;
	public List<AudioClip> Emaj9;
	public List<AudioClip> DSmin7b5;
	public List<AudioClip> FSmin7;
	public List<AudioClip> GS;
	public List<AudioClip> Emaj7S5;


	Dictionary<string, List<AudioClip>> noteToAudioList;
	public string selfNote;
	AudioSource asource;



	void Awake() {
		rb2d = this.GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (platformSpeedAsNegative, 0);
		noteToAudioList = new Dictionary<string, List<AudioClip>> ();
		noteToAudioList.Add ("CSmin9", CSmin9);
		noteToAudioList.Add ("Cmaj7", Cmaj7);
		noteToAudioList.Add ("Amaj7", Amaj7);
		noteToAudioList.Add ("GSm7", GSm7);
		noteToAudioList.Add ("Dmaj9", Dmaj9);
		noteToAudioList.Add ("Emaj9", Emaj9);
		noteToAudioList.Add ("DSmin7b5", DSmin7b5);
		noteToAudioList.Add ("FSmin7", FSmin7);
		noteToAudioList.Add ("GS", GS);
		noteToAudioList.Add ("Emaj7S5", Emaj7S5);
		asource = this.GetComponent<AudioSource> ();

	}

	public void playNoteWin() {
		List<AudioClip> selecterList = noteToAudioList [selfNote];
		asource.clip = selecterList [Random.Range (1, selecterList.Count)];
		//asource.clip = selecterList [0];
		asource.Play ();

	}

	void Update() {
		if (transform.position.x < -50) {
			Destroy (this);
		}
	}

	public void playNoteFail() {
		List<AudioClip> selecterList = noteToAudioList [selfNote];
		asource.clip = selecterList [0];
		asource.Play ();
	}
}
