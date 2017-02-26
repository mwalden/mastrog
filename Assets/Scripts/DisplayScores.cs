using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayScores : MonoBehaviour {

	public Text score;
	public Text platformsPassed;
	public Text lanesLockedDown;
	public Text errorCount;
	public Text title;

	public Text[] levels;
	private int line;
	public float lineTime;
	public bool showLines;


	private float timeToZero;


	public TimerController timerController;

	void Start(){
		timeToZero = lineTime;
	}

	public DisplayScores(){
		timerController = new TimerController (() => showLine ());
	}

	void Update(){
		if (showLines) {
			timeToZero -= Time.deltaTime;
			if (timeToZero <= 0) {
				Text title = levels [line];
				line++;
				Text score = levels [line];
				title.gameObject.SetActive (true);
				score.gameObject.SetActive (true);
				line++;
				timeToZero = lineTime;
				if (line >= levels.Length) {
					showLines = false;
				}

			}
		}
	}


	public void setInfo(Scores scores){
		score.text = scores.getScores ().ToString();
		platformsPassed.text = scores.getPlatformsPasssed ().ToString();
		errorCount.text = scores.getErrorCount ().ToString();
		lanesLockedDown.text = scores.getLanesLockedDown ().ToString();
		title.text = scores.getLevel ().title;
		
	}

	public void showLine(){
		showLines = true;
	}
}
