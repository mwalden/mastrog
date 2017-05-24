using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerBoxController : MonoBehaviour {


	public GameObject powerBox;
	public float boxSpeed;

	private bool objectAlreadyOut;
	private NewLevel level;
	private GameObject box;
	private Bounds bounds;
	private GameObject player;
	private CurrentLevelScript currentLevelScript;
	private int currentRow;
	//powerBoxChance[0] = probability of no box
	//powerBoxChance[1] = probability of a box being deployed.
	private Dictionary<int,float> powerBoxChance;
	private bool deployBox;
	private bool clearedObstacle;
	private bool landed;


	public delegate void BoxOpenedEffect();
	//wavey script is on RenderShader on the camera

	Dictionary<int,BoxOpenedEffect> goodEffects;
	Dictionary<int,BoxOpenedEffect> badEffects;

	public void clearLane(){
		Messenger.Broadcast ("clearOutLane");
	}
	public void wavey(){
		Messenger.Broadcast ("turnOnWaves");
	}

	void Start(){
		goodEffects = new Dictionary<int,BoxOpenedEffect> ();
		badEffects = new Dictionary<int,BoxOpenedEffect> ();
		goodEffects.Add (0, clearLane);
		badEffects.Add (0, wavey);
		bounds = CameraExtensions.OrthographicBounds (Camera.main);
		Messenger.AddListener<NewLevel> ("setLevel", setLevel);
		Messenger.AddListener<int> ("setRow", setClearedObstacle);
		Messenger.AddListener ("landed", landedOnPlatform);
		Messenger.AddListener ("boxOpened", boxOpened);
		powerBoxChance = new Dictionary<int,float>();
	}

	void boxOpened(){
		float goodOrBad = Random.Range (0,1f );
		if (goodOrBad < level.changeOfGoodPowerBox) {
			int effect = Random.Range (0, goodEffects.Count - 1);
			goodEffects [effect] ();
		} else {
			int effect = Random.Range (0, badEffects.Count - 1);
			badEffects [effect] ();
		}
	}

	void landedOnPlatform(){
		if (clearedObstacle) {
			setRow ();
			clearedObstacle = false;
		}
	}
	void setClearedObstacle(int row){
		clearedObstacle = true;
		currentRow = row;
	}


	void setRow(){
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		float randomNumber = Random.Range (0,1f );
		deployBox = (randomNumber <= powerBoxChance [1]);
		if (deployBox && !objectAlreadyOut){
			resetProbabilities (level.powerBoxChance);
			createBox ();
		} else {
			float newProbability = Mathf.Log(currentRow / level.numberOfLevels) + 1;
			objectAlreadyOut = true;
			resetProbabilities (newProbability);
		}
	 }

	void resetProbabilities(float probability){
		powerBoxChance [0] = 1 - probability;
		powerBoxChance [1] = probability;
	}

	void setLevel(NewLevel level){
		this.level = level;
		resetProbabilities (level.powerBoxChance);
	}

	void createBox(){
		if (box != null)
			Destroy (box);
		box = Instantiate (powerBox, new Vector3(-100f,-100f,0f), Quaternion.identity) as GameObject;
		box.transform.position = new Vector3 ((level.numberOfLanes -1 ) * bounds.size.x + (bounds.size.x / 2) + box.GetComponent<BoxCollider2D>().size.x,
			player.transform.position.y+1, 
			0);
	}

	

	

}
