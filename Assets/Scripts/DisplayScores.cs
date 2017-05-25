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
	public float lineTime;
	public bool showLines;
	public Animator burstAnimator;
	private float timeToZero;
	private int line;

	public Sprite successBurst;
	public Sprite failBurst;
	public Text endGameTextMessage;

	public SpriteRenderer burstSpriteRenderer;


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
		if (scores.completedLevel) {
			burstSpriteRenderer.sprite = successBurst;
			endGameTextMessage.text = "WELL DONE!";
		} else {
			burstSpriteRenderer.sprite = failBurst;
			endGameTextMessage.text = "FAILED";
		}
		
	}

	public void showLine(){
		Vector3 cPos = Camera.main.transform.position;
		Vector3 burstPosition = new Vector3 (cPos.x, cPos.y + 1.5f, cPos.z);

		burstAnimator.gameObject.transform.position = burstPosition;
		burstAnimator.gameObject.SetActive (true);

		burstAnimator.Play ("burst");
		showLines = true;
	}
}
