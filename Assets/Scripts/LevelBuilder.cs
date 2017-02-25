using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelBuilder : MonoBehaviour {

	public GameObject platform;
	public GameScript gameScript;
	public GameObject playerPrefab;
	private int numberOfLanes;
	public Camera mainCamera;
	public Level level;
	public Bounds bounds;
	public float offset = 2.5f;
	private int maxLevelBuilt;

//	private List<GameObject> obstacles;
	private Dictionary<string,List<GameObject>> obstacles = new Dictionary<string,List<GameObject>>();

	void Start() {
		mainCamera = Camera.main;

		//trying to pull alevel from level selection.
		GameObject currentLevelGO = GameObject.FindGameObjectWithTag ("CurrentLevel");
		gameScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameScript> ();
		CurrentLevelScript currentLevelScript = null;
		if (currentLevelGO != null) {
			currentLevelScript = currentLevelGO.GetComponent<CurrentLevelScript> ();
		}
		if (currentLevelScript != null) {
			level = currentLevelScript.level;
		} else {
			//if its not there (IE: coming from editor), create it and pull song 0.
			if (currentLevelScript == null) {
				ParseJsonLevels parser = new ParseJsonLevels ();
				level = parser.getLevels ().levels [0];
			}
		}
		int startingLane = level.startingLane;

		numberOfLanes = level.numberOfLanes;
		int numberOfLevels = level.numberOfLevels;
		Obstacle[] obstacles = level.obstacles;

		bounds = CameraExtensions.OrthographicBounds (Camera.main);

		for (int i = 0; i < numberOfLevels ; i++) {
			maxLevelBuilt++;
//			float y = bounds.center.y + (bounds.size.y * i);
			float y = 8 * i;
			for (int j=0;j < obstacles.Length;j++){
				Obstacle obstacle = obstacles [j];
				GameObject go =  Instantiate(Resources.Load("Obstacles/"+obstacle.name, typeof(GameObject))) as GameObject;
				go.tag = obstacle.name;
				ObstacleID id = go.GetComponent<ObstacleID> ();
				id.levelId = i;
				setObstacleInDictionary (go.tag, go);
				go.transform.position = new Vector3 (bounds.center.x  + (bounds.size.x * j), y, 10);

				Instantiate(platform,new Vector3(bounds.center.x + (bounds.size.x * j)  ,bounds.min.y+1+y,10),Quaternion.identity);
			}

		}
		GameObject player = Instantiate (playerPrefab, new Vector3 (bounds.center.x + (bounds.size.x * startingLane), bounds.min.y + 1.7f, 10f), Quaternion.identity) as GameObject;
		player.tag = "Player";
		gameScript.setCurrentGameLevel (level);

		gameScript.setStartingLane (startingLane);
	}

	public void cleanUpObstacles(int currentLevel){
		int y = (maxLevelBuilt * 8);
		bool updateMaxLevel = false;
		foreach (string key in obstacles.Keys) {
			List<GameObject> list = obstacles [key];
			foreach (GameObject obstacle in list) {
				ObstacleID id = obstacle.GetComponent<ObstacleID> ();
				if (currentLevel - id.levelId >= 2) {
					obstacle.transform.position = new Vector3 (obstacle.transform.position.x, y, 10);
					updateMaxLevel = true;
					id.levelId = maxLevelBuilt + 1;
					Instantiate(platform,new Vector3(obstacle.transform.position.x,bounds.min.y+1+y,10),Quaternion.identity);
				}
			}
		}
		if (updateMaxLevel)
			maxLevelBuilt++;
	}

	private void setObstacleInDictionary(string name, GameObject obstacle){
		//probably best to create a few of these then disable them 
		//vs creating them as they are read. will work for now.
		if (!obstacles.ContainsKey(name)) {
			obstacles [name] = new List<GameObject> ();
		}
		obstacles [name].Add (obstacle);
	}
}
