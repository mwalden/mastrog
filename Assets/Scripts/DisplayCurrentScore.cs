using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayCurrentScore : MonoBehaviour {

	private Text currentScoreText;

	private int score;
	// Use this for initialization
	void Start () {
		currentScoreText = GetComponent<Text> ();
		Messenger.AddListener<int> ("addScore", addScore);
	}

	void addScore(int score){
		this.score += score;
		currentScoreText.text = string.Concat("Score:",this.score);
	}
}
