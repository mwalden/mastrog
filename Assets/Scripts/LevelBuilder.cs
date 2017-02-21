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

		for (int i = 0; i < numberOfLanes ; i++) {
//			float y = bounds.center.y + (bounds.size.y * i);
			float y = 8 * i;
			for (int j=0;j < obstacles.Length;j++){
				Obstacle obstacle = obstacles [j];
				GameObject go =  Instantiate(Resources.Load("Obstacles/"+obstacle.name, typeof(GameObject))) as GameObject;
				go.tag = obstacle.name;
				setObstacleInDictionary (go.tag, go);
				go.transform.position = new Vector3 (bounds.center.x + (bounds.size.x * j), y, 10);

				Instantiate(platform,new Vector3(bounds.center.x + (bounds.size.x * j),bounds.min.y+1+y,10),Quaternion.identity);
			}

		}
		Instantiate (playerPrefab, new Vector3 (bounds.center.x + (bounds.size.x * startingLane), bounds.min.y + 1.7f, 10f), Quaternion.identity);
		gameScript.setCurrentGameLevel (level);
		gameScript.setStartingLane (startingLane);
	}

	public void cleanUpObstacles(){
//		foreach (string key in obstacles.Keys) {
//			List<GameObject> list = obstacles [key];
//			foreach (GameObject obstacle in list) {
////				if (obstacle.transform.position.y < mainCamera.transform.position.y + bounds.min.y) {
//					obstacle.SetActive (false);
////				}
//			}
//		}
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
