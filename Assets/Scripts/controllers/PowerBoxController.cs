using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;	


public class PowerBoxController : MonoBehaviour {


	public GameObject powerBox;
	public float boxSpeed;

	private bool objectAlreadyOut;
	private LevelDetail level;
	private GameObject box;
	private Bounds bounds;
	private GameObject player;
	private int currentRow;
	//powerBoxChance[0] = probability of no box
	//powerBoxChance[1] = probability of a box being deployed.
	private Dictionary<int,float> powerBoxChance;
	private bool deployBox;
	private bool clearedObstacle;
	private bool landed;
	public GameObject burstPrefab;
	private ParticleSystem burst;
	Dictionary<string, Action> powerboxFunctions;
	public delegate void BoxOpenedEffect();

	private Animator powerBoxTitleAnimator;
	public Text powerBoxTitle;
	Dictionary<int,Powerbox> goodEffects;
	Dictionary<int,Powerbox> badEffects;
	TimerController timerController = new TimerController ();

	BoxOpenedEffect currentEffect;

	void clearLane(){
		powerBoxTitle.text = "Lane Cleared!";
		Messenger.Broadcast ("clearOutLane");
	}
	//wavey script is on RenderShader on the camera
	void wavey(){
		powerBoxTitle.text = "Cant see!";
		timerController.setAction (() => {
			Messenger.Broadcast ("turnOffWaves");	
		});
		timerController.beginTimer (5000);
		Messenger.Broadcast ("turnOnWaves");
	}

	void refillHealth(){
		Messenger.Broadcast<float,bool>("addHealth",1000f,true);
		powerBoxTitle.text = "Added Health";
	}

	void fasterBleeding(){
		powerBoxTitle.text = "Lose Health";
		timerController.setAction (() => {
			Messenger.Broadcast<float> ("adjustBleedFactor", .004f);
		});
		Messenger.Broadcast<float>("adjustBleedFactor",.008f);
		timerController.beginTimer (5000);
	}

	void speedUpRotations(){
		powerBoxTitle.text = "Speed up!";
		RotateScript []rotateScripts = GameObject.FindObjectsOfType<RotateScript> ();
		foreach (RotateScript rotateScript in rotateScripts){
			rotateScript.speedUpPowerup ();
		}
	}

	public void PlayEffect(){
		
		currentEffect ();
	}

	void Start(){
		powerBoxTitleAnimator = GetComponent<Animator> ();
		goodEffects = new Dictionary<int,Powerbox> ();
		badEffects = new Dictionary<int,Powerbox> ();
		powerboxFunctions = new Dictionary<string, Action>()
		{
			{ "refillHealth", () => refillHealth() },
			{ "clearLane", () => clearLane() },
			{ "wavey", () => wavey() },
			{ "drainHealth", () => fasterBleeding() },
			{"speedUpRotations",() => speedUpRotations()}
		};
		List<Powerbox> powerboxes = LevelManager.Instance.getPowerBoxInfo ();
		int goodCount = 0;
		int badCount = 0;
		foreach (Powerbox box in powerboxes){
			if (box.goodBox) {
				goodEffects.Add (goodCount, box);
				goodCount++;
			} else {
				badEffects.Add (badCount, box);
				badCount++;
			}
		}

		bounds = CameraExtensions.OrthographicBounds (Camera.main);
		level = LevelManager.Instance.getCurrentLevelDetail();
		powerBoxChance = new Dictionary<int,float>();
		resetProbabilities (level.powerBoxChance);

		Messenger.AddListener<int> ("setRow", setClearedObstacle);
		Messenger.AddListener ("landed", landedOnPlatform);
		Messenger.AddListener <Vector3>("boxOpened", boxOpened);
		Messenger.AddListener<Scores,GameObject> ("gameOver", gameOver);

	}

	//something else is listening for game over and it has these objects
	void gameOver(Scores scores, GameObject player){
		Messenger.Broadcast ("turnOffWaves");	
	}

	void onDestory(){
		Messenger.RemoveListener<int> ("setRow", setClearedObstacle);
		Messenger.RemoveListener ("landed", landedOnPlatform);
		Messenger.RemoveListener <Vector3>("boxOpened", boxOpened);
		Messenger.RemoveListener<Scores,GameObject> ("gameOver", gameOver);
	}



	public void playBurst(){		
		this.burst.Play ();
	}

	void playBoxOpen(Dictionary<int,Powerbox> powerbox){
		int effect = UnityEngine.Random.Range (0, powerbox.Count);
		print (effect);
		Powerbox box = powerbox [effect];

		powerboxFunctions [box.name] ();
	}

	void boxOpened(Vector3 position){
		Destroy (burst);
		burst = (Instantiate (burstPrefab, position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>();		
		float goodOrBad = UnityEngine.Random.Range (0,1f );
		if (goodOrBad < level.changeOfGoodPowerBox) {
			playBoxOpen (goodEffects);
		} else {
			playBoxOpen (badEffects);
		}
		powerBoxTitleAnimator.SetTrigger ("ShowTitle");
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

		float randomNumber = UnityEngine.Random.Range (0,1f );
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

	void createBox(){
		if (box != null)
			Destroy (box);
		box = Instantiate (powerBox, new Vector3(-100f,-100f,0f), Quaternion.identity) as GameObject;
		box.transform.position = new Vector3 ((level.numberOfLanes -1 ) * bounds.size.x + (bounds.size.x / 2) + box.GetComponent<BoxCollider2D>().size.x,
			player.transform.position.y+1, 
			0);
	}



}
