using System;

[Serializable]
public class Level
{
	public string folderName;
	public int lengthInSeconds;
	public string title;
	public int numberOfLanes;
	public int startingLane;
	public Obstacle[] obstacles;
	public int numberOfLevels;	
	public string preview;

}

