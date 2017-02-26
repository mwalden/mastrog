﻿using System;

public class ScoreController{

	private float timeOnPlatform;
	private float maxTimeOnPlatform;
	private float quickestTimeOnPlatform;
	private int score = 0;
	private int lockDownLaneCount;
	private int errorCount;
	private int platformCount;
	private Level level;

	public ScoreController (){}
	public void setCurrentLevel(Level level){
		this.level = level;
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
	public void addScore(int score){
		this.score += score;
	}
	public void removeScore(int score){
		this.score -= score;
	}


	public Scores getScores(){
		return new Scores (score: this.score,
			platformsPassed: platformCount,
			lanesLockedDown: lockDownLaneCount,
			errorCount: errorCount,
			level:this.level);
	}
}
