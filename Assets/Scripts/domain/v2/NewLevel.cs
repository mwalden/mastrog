using System;
using System.Collections.Generic;


[Serializable]
public class NewLevel
{
	public string folderName;
	public int lengthInSeconds;
	public string title;
	public int numberOfLanes;
	public int startingLane;
	public Obstacle[] rows;
	public string [] obstacleNames;
	public int numberOfLevels;	
	public string preview;
	public string backgroundImage;
	public float powerBoxChance;
	public float goodOrBadBoxChance;
}

