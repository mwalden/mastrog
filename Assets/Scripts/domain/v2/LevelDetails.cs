using System;
using System.Collections.Generic;


public class Powerbox
{
	public string name { get; set; }
	public bool goodBox { get; set; }
	public bool waitForTitle { get; set; }
}

public class MarcelsLevel
{
	public string folderName { get; set; }
	public double powerBoxChance { get; set; }
	public string preview { get; set; }
	public double changeOfGoodPowerBox { get; set; }
	public int lengthInSeconds { get; set; }
	public string backgroundImage { get; set; }
	public string title { get; set; }
	public int numberOfLanes { get; set; }
	public int startingLane { get; set; }
	public int numberOfLevels { get; set; }
	public List<string> obstacleNames { get; set; }
	public List<List<Obstacle>> rows { get; set; }
}

public class LevelDetails
{
	public List<Powerbox> powerboxes { get; set; }
	public List<MarcelsLevel> levels { get; set; }
}


