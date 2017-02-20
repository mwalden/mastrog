using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ParseJsonLevels {
	public Levels levels;
	public ParseJsonLevels(){
		string path = Application.dataPath + "/Resources/levels.json";
		levels = JsonUtility.FromJson<Levels> (File.ReadAllText (path));
	}

	public Levels getLevels(){
		return levels;
	}

}
