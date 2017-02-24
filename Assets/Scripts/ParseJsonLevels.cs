using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ParseJsonLevels {
	public Levels levels;
	public ParseJsonLevels(){
		string path = Application.dataPath + "/levels";
		TextAsset targetFile = Resources.Load<TextAsset>("levels");
		levels = JsonUtility.FromJson<Levels> (targetFile.text);
	}

	public Levels getLevels(){
		return levels;
	}

}
