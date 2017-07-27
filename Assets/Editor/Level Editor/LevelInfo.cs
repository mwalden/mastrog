using System;
using System.Collections.Generic;

namespace LevelEditor
{
	public class LevelInfo
	{
		public string name;
		public string sampleTrack;
		public string trackName;
		public int lengthInSeconds;
		public int numberOfLanes; 
		public List<string> obstacles;
		public float chanceOfPowerBox;
		public string backgroundImage;
		public int startingLane;
		public int numberOfLevels;

		public LevelInfo (string name, 
			string sampleTrack, 
			string trackName, 
			int lengthInSeconds,
			int numberOfLanes, 
			List<string> obstacles, 
			float chanceOfPowerBox,
			string backgroundImage,
			int startingLane,
			int numberOfLevels
		)
		{
			this.name = name;
			this.sampleTrack = sampleTrack;
			this.trackName = trackName;
			this.lengthInSeconds = lengthInSeconds;
			this.numberOfLanes = numberOfLanes;
			this.obstacles = obstacles;
			this.chanceOfPowerBox = chanceOfPowerBox;
			this.backgroundImage = backgroundImage;
			this.startingLane = startingLane;
			this.numberOfLevels = numberOfLevels;
		}
	}
}

