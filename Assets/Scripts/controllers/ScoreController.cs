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

	private bool isLaneEnabled = true;

	public ScoreController (){
		completedLevel = true;
		level = LevelManager.Instance.getCurrentLevelDetail ();
		Messenger.AddListener<bool> ("isLaneEnabled", laneEnabled);
		Messenger.AddListener<int,int>("addScore", addScore);
		Messenger.AddListener<int>("addScoreIgnoreLaneEnabled", addScoreIgnoreLaneEnabled);
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
		Messenger.Broadcast ("displayScore", this.score);
	}

	public void addScore(int laneId, int score){
		if (!isLaneEnabled)
			return;
		this.score += score;
		Messenger.Broadcast ("displayScore", this.score);
	}
	public void removeScore(int score){
		this.score -= score;
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

