﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelSelectScript : MonoBehaviour {
	public GameObject levelSelectPrefab;
	public Canvas selectionCanvas;
	public AudioSource audioSource;

	private AudioClip song;
	private int selectedLevel;
	private Level[] levels;
	private List<GameObject> panels;
	private CurrentLevelScript currentLevelScript;
	public TouchGesture.GestureSettings GestureSetting;
	private TouchGesture touch;

	// Use this for initialization
	void Start () {
		panels = new List<GameObject> ();
		currentLevelScript = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<CurrentLevelScript> ();
//		string path = Application.dataPath + "/Resources/levels.json";
//		Levels gameLevels = JsonUtility.FromJson<Levels> (File.ReadAllText (path));
		ParseJsonLevels parser = new ParseJsonLevels();
		Levels gameLevels = parser.getLevels ();
		foreach (Level level in gameLevels.levels){
			GameObject panel = Instantiate(levelSelectPrefab,new Vector3(0,0,10),Quaternion.identity) as GameObject;
			panel.GetComponentInChildren<Text> ().text = level.title;
			panel.transform.SetParent (selectionCanvas.transform,false);
			panels.Add (panel);
		}
		levels = gameLevels.levels;
		panels [selectedLevel].GetComponent<Image> ().color = Color.red;
		PlayMusic ();

		#if UNITY_ANDROID
		touch = new TouchGesture(this.GestureSetting);
		StartCoroutine(touch.CheckVerticleSwipes(
			onSwipeUp: () => { onSwipeUp(); },
			onSwipeDown: () => { onSwipeDown(); }
		));
		#endif
	}

	void onSwipeUp(){
		if (selectedLevel > 0)
			UpdateSelection (selectedLevel - 1);
	}
	void onSwipeDown(){
		if (selectedLevel + 1 < panels.Count) {
			UpdateSelection (selectedLevel + 1);
		}
	}

	void UpdateSelection(int newLevel){
		panels [selectedLevel].GetComponent<Image> ().color = Color.white;
		panels [newLevel].GetComponent<Image> ().color = Color.red;
		selectedLevel = newLevel;
		PlayMusic ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyUp (KeyCode.DownArrow) && selectedLevel + 1 < panels.Count) {
			UpdateSelection (selectedLevel + 1);
		}
		if (Input.GetKeyUp (KeyCode.UpArrow) && selectedLevel > 0) {
			UpdateSelection (selectedLevel - 1);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
 			currentLevelScript.level = levels [selectedLevel];
			SceneManager.LoadScene (1);
		}
	}

	public void onClick(){
		currentLevelScript.level = levels [selectedLevel];
		SceneManager.LoadScene (1);
	}

	void PlayMusic(){
		Level level = levels [selectedLevel];
		string previewName = level.preview;
		song = Resources.Load<AudioClip> ("Audio/Previews/"+previewName);
		audioSource.clip = song;
		audioSource.Play ();
	}
}
