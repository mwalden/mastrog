using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayCurrentScore : MonoBehaviour {

	public Text currentScoreText;
	public Text currentMultiplier;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<int> ("displayScore", displayScore);
		Messenger.AddListener<int> ("setMultiplierText", setMultiplier);
	}

	void displayScore(int score){
		currentScoreText.text = string.Concat("Score:",score);
	}

	void setMultiplier(int multiplier){
		currentMultiplier.text = string.Concat ("Multiplier:", multiplier) + "x";
	}

}
