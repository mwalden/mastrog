using System;

public class Scores
{
	private int scores;
	private int platformsPassed;
	private int lanesLockedDown;
	private int errorCount;
	public bool completedLevel { get; set;}
	private LevelDetail level;

	public Scores (int score, int platformsPassed, int lanesLockedDown, int errorCount, LevelDetail level, bool completedLevel)
	{
		this.scores = score;
		this.platformsPassed = platformsPassed;
		this.lanesLockedDown = lanesLockedDown;
		this.errorCount = errorCount;
		this.level = level;
		this.completedLevel = completedLevel;
	}

	public LevelDetail getLevel(){
		return level;
	}
		
	public int getScores(){
		return scores;
	}

	public int getPlatformsPasssed(){
		return platformsPassed;
	}

	public int getLanesLockedDown(){
		return lanesLockedDown;
	}

	public int getErrorCount(){
		return errorCount;
	}


}

