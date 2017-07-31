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
	public LevelDetail level;
	public Bounds bounds;
	public float offset = 2.5f;
	private int maxLevelBuilt;
	private int lockedDownLane = -1;

	public int obstacleArrayMarker = 0;
	public Dictionary<string,List<GameObject>> obstaclePool = new Dictionary<string,List<GameObject>>();
	List<GameObject> obstaclesInUse = new List<GameObject>();

	void Start() {
		Messenger.AddListener< int >( "enableLane", unlockLane );
		Messenger.AddListener< int >( "disableLane", lockDownLane );

		mainCamera = Camera.main;

		//trying to pull alevel from level selection.

		gameScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameScript> ();

		level = LevelManager.Instance.getCurrentLevelDetail();
		int startingLane = level.startingLane;

		numberOfLanes = level.numberOfLanes;
		int numberOfLevels = level.numberOfLevels;
		List<List<Obstacle>> rows = level.rows;
		initilizeObjectPool ();
		bounds = CameraExtensions.OrthographicBounds (Camera.main);
		int obstaclePosition = 0;
		for (int i = 0; i <= numberOfLevels - 1; i++) {
			maxLevelBuilt++;
			float y = 8 * i;
			obstaclePosition = 0;
			List<Obstacle> rowsObstacles = rows [i];
			for (int j = 0; j <= rowsObstacles.Count-1; j++) {
				Obstacle obstacle = rowsObstacles [j];
				GameObject go = getObjectFromPoolByName (obstacle.name, i);
				go.transform.position = new Vector3 (bounds.center.x  + (bounds.size.x * obstaclePosition), y, 10);
				Instantiate(platform,new Vector3(bounds.center.x + (bounds.size.x * obstaclePosition)  ,bounds.min.y+1+y,10),Quaternion.identity);
				obstaclePosition++;
			}
		}

		GameObject player = Instantiate (playerPrefab, new Vector3 (bounds.center.x + (bounds.size.x * startingLane), bounds.min.y + 1.7f, 10f), Quaternion.identity) as GameObject;
		player.tag = "Player";
		gameScript.setStartingLane (startingLane);
		Messenger.Broadcast<int>("initializeLanes",level.numberOfLanes);
	}

	private void initilizeObjectPool(){
		foreach (string obstacleName in level.obstacleNames) {
			for (int x = 0; x < 20; x++) {
				GameObject go =  Instantiate(Resources.Load("Obstacles/"+obstacleName, typeof(GameObject))) as GameObject;
				go.SetActive (false);
				if (!obstaclePool.ContainsKey(obstacleName)) {
					obstaclePool [obstacleName] = new List<GameObject> ();
				}
				go.name = obstacleName;
				obstaclePool [obstacleName].Add (go);
			}
		}	
	}

	private void putItemBackInPool(GameObject go){
		List<GameObject> objects = obstaclePool [go.name];
		EnableDisableScript script = go.GetComponent<EnableDisableScript> ();
		if (script != null)
			script.enableObstacle ();
		go.SetActive(false);
		obstaclesInUse.Remove (go);
		objects.Add (go);
	}

	private GameObject getObjectFromPoolByName(string name, int levelId){
		List<GameObject> objects = obstaclePool [name];
		GameObject go = objects[0];
		ObstacleID id = go.GetComponent<ObstacleID> ();
		id.levelId = levelId;
		objects.Remove (go);
		go.SetActive(true);
		obstaclesInUse.Add (go);

		obstacleArrayMarker++;
		return go;
	}

	private void addRowToScene(int levelId){
		int y = (levelId - 1) * 8;
		if (levelId >= level.numberOfLevels)
			levelId = 0;
		for (int i = 0; i < numberOfLanes; i++) {
			Obstacle obstacle = level.rows[levelId][i];
			GameObject go = getObjectFromPoolByName (obstacle.name, levelId);
			if (i == lockedDownLane)
				go.GetComponent<EnableDisableScript> ().disableObstacle ();
			go.transform.position = new Vector3 (bounds.center.x  + (bounds.size.x * i), y, 10);
			Instantiate(platform,new Vector3(bounds.center.x + (bounds.size.x * i)  ,bounds.min.y+1+y,10),Quaternion.identity);
		}
	}

	public void cleanUpObstacles(int currentLevel){
		bool drawNewRow = false;
		List<GameObject> objectsToPutBack = new List<GameObject> ();
		foreach (GameObject go in obstaclesInUse) {
			if (mainCamera.transform.position.y + bounds.min.y > go.transform.position.y) {
				go.SetActive (false);
				objectsToPutBack.Add(go);
				drawNewRow = true;
			}
		}

		if (drawNewRow) {
			for (int i = 0; i < objectsToPutBack.Count;i++){
				putItemBackInPool (objectsToPutBack [i]);
			}

			maxLevelBuilt++;
			addRowToScene (maxLevelBuilt);
		}
	}
	private void enableLane(int laneLocked){
		for (int x = 0; x < obstaclesInUse.Count; x++) {
			GameObject go = obstaclesInUse [x];
			if (x % level.numberOfLanes == laneLocked) {
				EnableDisableScript script = go.GetComponent<EnableDisableScript> ();
				if (script != null)
					script.enableObstacle ();
			}
		}
	}
	private void disableLane(int laneLocked){
		for (int x = 0; x < obstaclesInUse.Count; x++) {
			GameObject go = obstaclesInUse [x];
			if (x % level.numberOfLanes == laneLocked) {
				EnableDisableScript script = go.GetComponent<EnableDisableScript> ();
				if (script != null)
					script.disableObstacle ();
			}
		}
	}
	public void lockDownLane(int laneLocked){
		lockedDownLane = laneLocked;
		disableLane (laneLocked);
	}
	public void unlockLane(int lane){
		lockedDownLane = -1;
		enableLane (lane);
	}


}
