using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace LevelEditor{
	public class Level : MonoBehaviour {
		[HideInInspector]
		public string levelName = "Enter level name";
		[HideInInspector]
		public Sprite image = null;
		[HideInInspector]
		public string sampleTrack = "Enter sample track file";
		[HideInInspector]
		public int lengthInSeconds = 0;
		[HideInInspector]
		public int numberOfLanes = 0;
		Bounds bounds = CameraExtensions.OrthographicBounds (Camera.main);
		[HideInInspector]
		public int obstaclePosition;
		[HideInInspector]
		public float chanceOfPowerbox;
		[HideInInspector]
		public float chanceOfGoodPowerbox;
		[HideInInspector]
		public AudioClip previewAudio;

		private LevelDetail selectedLevel;

		private Camera gameCamera;
		float y;
		List<GameObject> obstacles = new List<GameObject>();
		public List<string> powerboxes = new List<string>();

		void Start(){
			obstacles = new List<GameObject>();
			powerboxes = new List<string>();
		}
		public void initalize(){
			obstacles = new List<GameObject>();
			powerboxes = new List<string>();
		}

		public void setPreviewClip(AudioClip clip){
			if (!clip) {
				previewAudio = null;
				sampleTrack = "";
				return;
			}
			previewAudio = clip;
			sampleTrack = clip.name;
		}

		public void addObstacleWithType(string obstacleName){
			float spacer = 4f;
			GameObject go =  Instantiate(Resources.Load("Obstacles/"+obstacleName, typeof(GameObject))) as GameObject;
			go.transform.position = new Vector3 (spacer + (spacer * obstaclePosition), y, 10f);
			go.transform.name = obstacleName;
			obstacles.Add (go);
			obstaclePosition++;
			if (obstaclePosition == numberOfLanes) {
				obstaclePosition = 0;
				y += spacer;
				Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x,
					Camera.main.transform.position.y + 3.75f,
					Camera.main.transform.position.z);
			}
		}


		public void clearPowerBoxes(){
			powerboxes = new List<string>();
		}

		public void resetLevel(){
			resetObstacles ();
			selectedLevel = null;
			levelName = "";
			image = null;
			sampleTrack = "";
			lengthInSeconds = 0;
			numberOfLanes = 4;
			chanceOfGoodPowerbox = .5f;
			chanceOfPowerbox = .5f;
		}

		public void addPowerbox(string name){
			if (!powerboxes.Contains (name))
				powerboxes.Add (name);
		}

		public void resetObstacles(){
			obstaclePosition = 0;
			gameCamera = Camera.main;
			y = 0;
			if (obstacles != null)
				foreach (GameObject go in obstacles) {
					DestroyImmediate (go);
				}
			obstacles = new List<GameObject>();
			gameCamera.transform.position = new Vector3 (10f, 0, 0);
		}

		public void serialize(){
			LevelDetail levelDetail = selectedLevel != null?selectedLevel:new LevelDetail();
			levelDetail.backgroundImage = image.name;
			levelDetail.folderName = levelName;
			levelDetail.numberOfLanes = numberOfLanes;
			levelDetail.numberOfLevels = 4;
			levelDetail.lengthInSeconds = lengthInSeconds;
			levelDetail.preview = sampleTrack;
			levelDetail.startingLane = 1;
			levelDetail.title = levelName;
			List<List<Obstacle>> rows = new List<List<Obstacle>> ();
			HashSet<string> obstacleNames = new HashSet<string> ();
			levelDetail.powerBoxChance = chanceOfPowerbox;
			levelDetail.changeOfGoodPowerBox = chanceOfGoodPowerbox;
			int count = 0;
			List<Obstacle> row = new List<Obstacle> ();
			foreach (GameObject go in obstacles) {
				obstacleNames.Add (go.name);
				Obstacle obstacle = new Obstacle ();
				obstacle.name = go.name;
				obstacle.speed = 100;
				count ++;
				row.Add (obstacle);
				if (count == numberOfLanes) {
					rows.Add (row);
					row = new List<Obstacle> ();
					count = 0;
				}
			}


			levelDetail.rows = rows;
			levelDetail.obstacleNames = obstacleNames.ToList();
			List<LevelDetail> levels = LevelManager.Instance.getLevels ();
			int index = -1;
			for (int x = 0; x < levels.Count; x++) {
				LevelDetail level = levels [x];
				if (level.folderName.Equals (levelDetail.folderName)) {
					index = x;
					break;
				}
			}	

			if (index >= 0) {
				levelDetail.id = levels [index].id;
				levels [index] = levelDetail;
			} else {
				levelDetail.id = levels.Count;
				levels.Add (levelDetail);
			}


			LevelDetails levelDetails = LevelManager.Instance.getLevelDeatils();
			levelDetails.levels = levels;
			string json = JsonConvert.SerializeObject(levelDetails);
			System.IO.File.WriteAllText("Assets/Resources/test1.json", json);
			print (json);
		}

		public void loadLevel(int id){
			LevelDetail levelDetail = LevelManager.Instance.getLevels() [id];
			resetObstacles ();
			selectedLevel = levelDetail;
			numberOfLanes = levelDetail.numberOfLanes;
			image = Resources.Load<Sprite> ("images/" + levelDetail.backgroundImage);
			setPreviewClip(Resources.Load<AudioClip> ("Audio/Previews/" + levelDetail.preview));
			levelName = levelDetail.folderName;
			lengthInSeconds = levelDetail.lengthInSeconds;

			chanceOfPowerbox = levelDetail.powerBoxChance;
			chanceOfGoodPowerbox = levelDetail.changeOfGoodPowerBox;
			List<List<Obstacle>> rows = levelDetail.rows;
			foreach (List<Obstacle> row in rows) {
				foreach (Obstacle obstacle in row) {
					addObstacleWithType (obstacle.name);
				}
			}

		}
	}
}
