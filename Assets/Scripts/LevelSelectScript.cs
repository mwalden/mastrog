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
	private List<LevelDetail> levels;
	private List<GameObject> panels;
	public TouchGesture.GestureSettings gestureSetting;
	private TouchGesture touch;

	void Start () {
		panels = new List<GameObject> ();

		levels = LevelManager.Instance.getLevels();

		foreach (LevelDetail level in levels){
			GameObject panel = Instantiate(levelSelectPrefab,new Vector3(0,0,10),Quaternion.identity) as GameObject;
			panel.GetComponentInChildren<Text> ().text = level.title;
			Sprite sprite = Resources.Load<Sprite> ("images/" + level.backgroundImage);
			panel.GetComponent<Image> ().sprite = sprite;
			scroll.AddChild (panel);
		}
		PlayMusic (0);
	}

	public void onChange(){
		if (selectedLevel == scroll.CurrentPage)
			return;
		LevelManager.Instance.currentLevel = levels[scroll.CurrentPage].id;
		selectedLevel = scroll.CurrentPage;
		PlayMusic (scroll.CurrentPage);

	}

	public void onClick(){
		LevelManager.Instance.currentLevel = levels [selectedLevel].id;
		SceneManager.LoadScene (1);
	}

	void PlayMusic(int selection){
		LevelDetail level = levels [selection];
		string previewName = level.preview;
		song = Resources.Load<AudioClip> ("Audio/Previews/"+previewName);
		audioSource.clip = song;
		audioSource.Play ();
	}
}
