using System;
using System.Collections.Generic;


public class Powerbox
{
	public string name { get; set; }
	public bool goodBox { get; set; }
	public bool waitForTitle { get; set; }
}

public class LevelDetail
{
	public int id{ get; set;}
	public string folderName { get; set; }
	public float powerBoxChance { get; set; }
	public string preview { get; set; }
	public float changeOfGoodPowerBox { get; set; }
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
	public List<LevelDetail> levels { get; set; }
}


