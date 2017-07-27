using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayCurrentScore : MonoBehaviour {

	private Text currentScoreText;

	// Use this for initialization
	void Start () {
		currentScoreText = GetComponent<Text> ();
		Messenger.AddListener<int> ("displayScore", displayScore);
	}

	void displayScore(int score){
		currentScoreText.text = string.Concat("Score:",score);
	}
}
