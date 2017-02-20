using UnityEngine;
using System.Collections;
using System;

public class LevelBuilder : MonoBehaviour {

	public GameObject platform;
	public GameScript gameScript;
	public GameObject playerPrefab;
	private int numberOfLanes;

	void Start() {
		GameObject currentLevelGO = GameObject.FindGameObjectWithTag ("CurrentLevel");
		gameScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameScript> ();
		CurrentLevelScript currentLevelScript = null;
		if (currentLevelGO != null) {
			currentLevelScript = currentLevelGO.GetComponent<CurrentLevelScript> ();
		}
		Level level = null;
		if (currentLevelScript != null) {
			level = currentLevelScript.level;
		} else {
			if (currentLevelScript == null) {
				ParseJsonLevels parser = new ParseJsonLevels ();
				level = parser.getLevels ().levels [0];
			}
		}
		int startingLane = level.startingLane;
		gameScript.setStartingLane (startingLane);

		numberOfLanes = level.numberOfLanes;
		Obstacle[] obstacles = level.obstacles;

		Bounds bounds = CameraExtensions.OrthographicBounds (Camera.main);
		for (int i = 0; i < numberOfLanes ; i++) {
			float y = bounds.center.y + (bounds.size.y * i);
			for (int j=0;j < obstacles.Length;j++){
				Obstacle obstacle = obstacles [j];

				GameObject go =  Instantiate(Resources.Load("Obstacles/"+obstacle.name, typeof(GameObject))) as GameObject;
				go.transform.position = new Vector3 (bounds.center.x + (bounds.size.x * j), y, 10);

				Instantiate(platform,new Vector3(bounds.center.x + (bounds.size.x * j),bounds.min.y+1,10),Quaternion.identity);
				Instantiate(platform,new Vector3(bounds.center.x + (bounds.size.x * j),bounds.max.y-1,10),Quaternion.identity);
			}
		}
		Instantiate (playerPrefab, new Vector3 (bounds.center.x + (bounds.size.x * startingLane), bounds.min.y + 1.7f, 10f), Quaternion.identity);
	}
}
