using System;

public class Scores
{
	private int scores;
	private int platformsPassed;
	private int lanesLockedDown;
	private int errorCount;
	private Level level;

	public Scores (int score, int platformsPassed, int lanesLockedDown, int errorCount, Level level)
	{
		this.scores = score;
		this.platformsPassed = platformsPassed;
		this.lanesLockedDown = lanesLockedDown;
		this.errorCount = errorCount;
		this.level = level;
	}

	public Level getLevel(){
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

