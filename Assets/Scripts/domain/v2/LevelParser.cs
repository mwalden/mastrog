using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class LevelParser
{
	public NewLevels levels;
	public LevelParser(){
		TextAsset targetFile = Resources.Load<TextAsset>("Level2_backup");
		levels = JsonUtility.FromJson<NewLevels> (targetFile.text);
	}

	public NewLevels getLevels(){
		return levels;
	}

	public LevelDetails getNewLevels(){
		TextAsset targetFile = Resources.Load<TextAsset>("Level2");
		return JsonConvert.DeserializeObject<LevelDetails> (targetFile.text);
	}

}
