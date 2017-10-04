using System;
using System.Collections.Generic;

public class ScoreController{

	private float timeOnPlatform;
	private float maxTimeOnPlatform;
	private float quickestTimeOnPlatform;
	private int score = 0;
	private int lockDownLaneCount;
	private int errorCount;
	private int platformCount;
	public bool completedLevel { get; set;}
	private LevelDetail level;
	private int multiplier = 1;
	private bool isLaneEnabled = true;
	private int MAX_MULTIPLIER_VALUE = 4;

	public ScoreController (){
		completedLevel = true;
		level = LevelManager.Instance.getCurrentLevelDetail ();
		Messenger.AddListener ("defferedIncreaseMultiplier",defferedIncreaseMultiplier);
		Messenger.AddListener<bool> ("isLaneEnabled", laneEnabled);
//		Messenger.AddListener<int> ("increaseMultiplier", increaseMultiplier);
		Messenger.AddListener<int> ("setMultiplier", setMultiplier);
		Messenger.AddListener<int,int>("addScore", addScore);
		Messenger.AddListener<int>("addScoreIgnoreLaneEnabled", addScoreIgnoreLaneEnabled);
	}
	public void defferedIncreaseMultiplier(){
		increaseMultiplier (1);
	}

	public void setMultiplier(int value){
		multiplier = value;
		Messenger.Broadcast<int> ("setMultiplierText", multiplier);
	}

	public void increaseMultiplier(int value){
		if (value + multiplier >= MAX_MULTIPLIER_VALUE) {
			setMultiplier (MAX_MULTIPLIER_VALUE);
			return;
		}

		multiplier += value;
		Messenger.Broadcast<int> ("setMultiplierText", multiplier);
	}
	public void setTimeOnPlatform(float time){
		timeOnPlatform += time;
	}
	public void addPlatform(){
		platformCount++;
	}
	public void addError(){
		errorCount++;
	}
	public void addLockDownLane(){
		lockDownLaneCount++;
	}

	public void addScoreIgnoreLaneEnabled(int score){
		this.score += score;
		Messenger.Broadcast ("displayScore", this.score * multiplier);
	}

	public void addScore(int laneId, int score){		
		if (!isLaneEnabled)
			return;
		this.score += score;
		Messenger.Broadcast ("displayScore", this.score * multiplier);
	}
	public void removeScore(int score){
		this.score -= score;
		Messenger.Broadcast ("displayScore", this.score);
	}

	private void laneEnabled(bool isLaneEnabled){
		this.isLaneEnabled = isLaneEnabled;
	}

	public Scores getScores(){
		return new Scores (score: this.score,
			platformsPassed: platformCount,
			lanesLockedDown: lockDownLaneCount,
			errorCount: errorCount,
			level:this.level,
			completedLevel:this.completedLevel);
	}
}

