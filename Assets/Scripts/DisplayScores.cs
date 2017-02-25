using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayScores : MonoBehaviour {

	public Text score;
	public Text platformsPassed;
	public Text lanesLockedDown;
	public Text errorCount;
	public Text title;

	public void setInfo(Scores scores){
		score.text = scores.getScores ().ToString();
		platformsPassed.text = scores.getPlatformsPasssed ().ToString();
		errorCount.text = scores.getErrorCount ().ToString();
		lanesLockedDown.text = scores.getLanesLockedDown ().ToString();
		title.text = scores.getLevel ().title;
	}
}
