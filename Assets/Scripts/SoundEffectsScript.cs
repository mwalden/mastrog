using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffectsScript : MonoBehaviour {
	int successAudioLevel;
	List<AudioSource> successAudio = new List<AudioSource>();
	AudioSource error;
	AudioSource woosh;
	bool created;

	void Awake() {
		if (!created) {
			// this is the first instance - make it persist
			DontDestroyOnLoad(this.gameObject);
			created = true;
		} else {
			// this must be a duplicate from a scene reload - DESTROY!
			Destroy(this.gameObject);
		} 
	}


	void Start () {
		woosh = gameObject.AddComponent<AudioSource> ();
		string wooshPath = "Sounds/Woosh";
		AudioClip clip2 = Resources.Load<AudioClip> (wooshPath);
		woosh.clip = clip2;

		error = gameObject.AddComponent<AudioSource> ();
		string errorPath = "Sounds/Error";
		AudioClip clip1 = Resources.Load<AudioClip> (errorPath);
		error.clip = clip1;

		for (int i = 0; i < 4; i++) {
			AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
			successAudio.Add (audioSource);
			string path = "Sounds/Success - " + (i + 1);
			AudioClip clip = Resources.Load<AudioClip> (path);
			audioSource.clip = clip;
		}
	}

	public void playWoosh(){
		woosh.Play ();
	}
	public void playError(){
		error.Play ();
	}

	public void playLevelProgression(int level){		
		if (level != 0) {
			successAudio [level - 1].Play ();
		}
	}

}
