using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioScript : MonoBehaviour {

	Dictionary<int,bool> lockedAudioTracks = new Dictionary<int,bool> ();
	Dictionary<int,float> lockedAudioTracksDuration = new Dictionary<int,float> ();
	List<AudioSource> audioSources = new List<AudioSource>();
	List<AudioSource> successAudio = new List<AudioSource>();
	AudioSource error;
	AudioSource woosh;
	public int currentLane;
	public float lockDownDuration;
	public int level;
	private Level gameLevel;

	private int successAudioLevel;

	public void setGameLevel(Level level){
		gameLevel = level;

		for (int x = 0; x < level.numberOfLanes; x++) {
			AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
			audioSources.Add (audioSource);
			string path = "Audio" + "/"+level.folderName+"/" + (x + 1);
			AudioClip clip = Resources.Load<AudioClip> (path);
			audioSource.clip = clip;
			audioSource.volume = 0;
			audioSource.Play ();
			lockedAudioTracks [x] = false;
			lockedAudioTracksDuration [x] = 0.0f;
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
