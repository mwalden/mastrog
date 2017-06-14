using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LevelManager
{
	private static LevelManager _instance;
	private LevelDetails levelDetails;
	public int currentLevel{ get; set; }
	private const int DEFAULT_LEVEL = 2;
	private LevelManager ()
	{
		currentLevel = -1;
		TextAsset targetFile = Resources.Load<TextAsset>("Level2");
		levelDetails = JsonConvert.DeserializeObject<LevelDetails> (targetFile.text);
	}

	public static LevelManager Instance{
		get{
			if (_instance == null)
				_instance = new LevelManager ();
			return _instance;
		}
	}

	public LevelDetail levelByPosition(int position){		
		return levelDetails.levels [position];
	}

	public LevelDetail levelById(int id){
		foreach (LevelDetail levelDetail in levelDetails.levels) {
			if (levelDetail.id == id)
				return levelDetail;
		}
		return null;
	}

	public List<LevelDetail> getLevels(){
		return levelDetails.levels;
	}

	public List<Powerbox> getPowerBoxInfo(){
		return levelDetails.powerboxes;
	}
	public LevelDetail getCurrentLevelDetail(){
		foreach (LevelDetail levelDetail in levelDetails.levels) {
			if (levelDetail.id == currentLevel)
				return levelDetail;
		}
		currentLevel = DEFAULT_LEVEL;
		return levelDetails.levels[DEFAULT_LEVEL];
	}


}

