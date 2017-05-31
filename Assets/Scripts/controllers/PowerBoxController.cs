﻿using UnityEngine;
using UnityEngine.UI;
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
	public GameObject burstPrefab;
	private ParticleSystem burst;
	public delegate void BoxOpenedEffect();

	private Animator powerBoxTitleAnimator;
	public Text powerBoxTitle;
	Dictionary<int,BoxOpenedEffect> goodEffects;
	Dictionary<int,BoxOpenedEffect> badEffects;
	TimerController timerController = new TimerController ();
	ParticleSystem.EmissionModule psemit;
//	BoxOpenedEffect currentEffect;

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

	void fullHealth(){
		Messenger.Broadcast<float>("addHealth",1000f);
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

//	public void PlayEffect(){
//		currentEffect ();
//	}

	void Start(){
		powerBoxTitleAnimator = GetComponent<Animator> ();
		goodEffects = new Dictionary<int,BoxOpenedEffect> ();
		badEffects = new Dictionary<int,BoxOpenedEffect> ();
		goodEffects.Add (0, clearLane);
		goodEffects.Add (1, fullHealth);
		badEffects.Add (0, wavey);
		badEffects.Add (1, fasterBleeding);
		bounds = CameraExtensions.OrthographicBounds (Camera.main);
		Messenger.AddListener<NewLevel> ("setLevel", setLevel);
		Messenger.AddListener<int> ("setRow", setClearedObstacle);
		Messenger.AddListener ("landed", landedOnPlatform);
		Messenger.AddListener <Vector3>("boxOpened", boxOpened);
		Messenger.AddListener<Scores,GameObject> ("gameOver", gameOver);
		powerBoxChance = new Dictionary<int,float>();
	}

	//something else is listening for game over and it has these objects
	void gameOver(Scores scores, GameObject player){
		Messenger.Broadcast ("turnOffWaves");	
	}

	void onDestory(){
		Messenger.RemoveListener<NewLevel> ("setLevel", setLevel);
		Messenger.RemoveListener<int> ("setRow", setClearedObstacle);
		Messenger.RemoveListener ("landed", landedOnPlatform);
		Messenger.RemoveListener <Vector3>("boxOpened", boxOpened);
		Messenger.RemoveListener<Scores,GameObject> ("gameOver", gameOver);
	}



	public void playBurst(){
		Debug.Log ("Areasdfadsfadf you playing :" +burst.isPlaying);
		Debug.Log ("Have you stopped? :" +burst.isStopped);

		this.burst.Play ();
	}
	void boxOpened(Vector3 position){
		Destroy (burst);
		burst = (Instantiate (burstPrefab, position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>();		
		float goodOrBad = Random.Range (0,1f );
		if (goodOrBad < level.changeOfGoodPowerBox) {
			int effect = Random.Range (0, goodEffects.Count);
			goodEffects [effect]();
		} else {
			int effect = Random.Range (0, badEffects.Count);
			badEffects[effect]();
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
