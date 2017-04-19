using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class LevelParser
{
	public NewLevels levels;
	public LevelParser(){
		TextAsset targetFile = Resources.Load<TextAsset>("Level2");
		levels = JsonUtility.FromJson<NewLevels> (targetFile.text);
	}

	public NewLevels getLevels(){
		return levels;
	}
}
