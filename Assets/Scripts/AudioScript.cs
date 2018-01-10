using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioScript : MonoBehaviour {

	Dictionary<int,bool> lockedAudioTracks = new Dictionary<int,bool> ();
	Dictionary<int,float> lockedAudioTracksDuration = new Dictionary<int,float> ();
	List<AudioSource> audioSources = new List<AudioSource>();

	AudioSource error;
	AudioSource woosh;
	public int currentLane;
	public float lockDownDuration;
	public int level;
	private LevelDetail gameLevel;

	void Start(){
		gameLevel = LevelManager.Instance.getCurrentLevelDetail();

		for (int x = 0; x < gameLevel.numberOfLanes; x++) {
			AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
			audioSources.Add (audioSource);
			string path = "Audio" + "/"+gameLevel.folderName+"/" + (x + 1);
			AudioClip clip = Resources.Load<AudioClip> (path);
			audioSource.clip = clip;
			audioSource.volume = 0;
			audioSource.Play ();
			lockedAudioTracks [x] = false;
			lockedAudioTracksDuration [x] = 0.0f;
		}
	}

	void Update(){
		float deltaTime = Time.deltaTime;
		for (int x = 0; x < gameLevel.numberOfLanes; x++) {
			if (lockedAudioTracks [x]) {
				lockedAudioTracksDuration [x] -= deltaTime;
				if (lockedAudioTracksDuration [x] <= 0) {
					lockedAudioTracksDuration [x] = 0;
					lockedAudioTracks [x] = false;
					Messenger.Broadcast<int> ("enableLane", x);
					if (currentLane != x) 
						audioSources [x].volume = 0;
				}
			}
		}
	}

	public void setCurrentLane(int lane){
		if (!lockedAudioTracks [currentLane]) {
			audioSources [currentLane].volume = 0;
		}
		currentLane = lane;
		audioSources [lane].volume = 100;
	}

	public void lockDownLane(int lane){
		lockedAudioTracks[lane] = true;
		lockedAudioTracksDuration [lane] = lockDownDuration;

	}
}
