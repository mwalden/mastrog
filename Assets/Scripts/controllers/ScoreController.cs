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
	private Dictionary<int,bool> enabledLanes;

	public ScoreController (){
		completedLevel = true;
		level = LevelManager.Instance.getCurrentLevelDetail ();
		enabledLanes = new Dictionary<int,bool> ();
		Messenger.AddListener<int> ("disableLane", laneDisabled);
		Messenger.AddListener<int> ("enableLane", laneEnabled);
		Messenger.AddListener<int,int>("addScore", addScore);
	}

	public void laneDisabled(int laneId){
		if (enabledLanes.ContainsKey(laneId))
			enabledLanes [laneId] = false;
		else
			enabledLanes.Add (laneId, false);

	}

	public void laneEnabled(int laneId){
		if (enabledLanes.ContainsKey(laneId))
			enabledLanes [laneId] = true;
		else
			enabledLanes.Add (laneId, true);
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
	public void addScore(int laneId, int score){
		if (enabledLanes.ContainsKey(laneId) && !enabledLanes [laneId])
			return;
		this.score += score;
		Messenger.Broadcast ("displayScore", this.score);
	}
	public void removeScore(int score){
		this.score -= score;
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

