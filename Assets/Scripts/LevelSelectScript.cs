using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI.Extensions;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelSelectScript : MonoBehaviour {
	public GameObject levelSelectPrefab;
	public HorizontalScrollSnap scroll;
	public AudioSource audioSource;

	private AudioClip song;
	private int selectedLevel;
	private NewLevel[] levels;
	private List<GameObject> panels;
	private CurrentLevelScript currentLevelScript;
	public TouchGesture.GestureSettings gestureSetting;
	private TouchGesture touch;

	void Start () {
		
		panels = new List<GameObject> ();
		currentLevelScript = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<CurrentLevelScript> ();
		ParseJsonLevels parser = new ParseJsonLevels();
		LevelParser levelParser = new LevelParser ();
		NewLevels newLevels = levelParser.getLevels ();
//		Levels gameLevels = parser.getLevels ();
		foreach (NewLevel level in newLevels.levels){
			GameObject panel = Instantiate(levelSelectPrefab,new Vector3(0,0,10),Quaternion.identity) as GameObject;
			panel.GetComponentInChildren<Text> ().text = level.title;
			Sprite sprite = Resources.Load<Sprite> ("images/" + level.backgroundImage);
			panel.GetComponent<Image> ().sprite = sprite;
			scroll.AddChild (panel);
		}
		levels = newLevels.levels;
		PlayMusic (0);
	}
	void Update () {
	}

	public void onChange(){
		if (selectedLevel == scroll.CurrentPage)
			return;

		currentLevelScript.level = levels [scroll.CurrentPage];
		selectedLevel = scroll.CurrentPage;
		PlayMusic (scroll.CurrentPage);

	}

	public void onClick(){
		currentLevelScript.level = levels [selectedLevel];
		SceneManager.LoadScene (1);
	}


	void PlayMusic(int selection){
		NewLevel level = levels [selection];
		string previewName = level.preview;
		song = Resources.Load<AudioClip> ("Audio/Previews/"+previewName);
		audioSource.clip = song;
		audioSource.Play ();
	}
}
