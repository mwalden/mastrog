using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameScript : MonoBehaviour {
	private GameObject currentPlatform;
	private GameObject player;
	private Rigidbody2D playerRigidbody;
	private PlayerScript playerScript;
	//plays the song
	private AudioScript audioScript;
	//plays the jump success sound and error sound
	private SoundEffectsScript soundEffectScript;
	private Camera cam;
	private CameraScript cameraScript;
	private bool justMoved;
	private float distanceToMoveX;

	//going left/right on the platforms
	private int currentLaneId;
	//level the player has selected
	private LevelDetail currentGameLevel;
	//when the timer goes off, the game ends
	private TimerController timerController;
	//bounds of the camera. used to calculate distanceToMoveX 
	private Bounds bounds;
	private ScoreController scoreController;
	//game has ended. call the end game script to move camera up
	private bool gameOver;

	//going up the game. counts how many platforms youve gone up.
	public int currentPlatformLevel;
	//keeps track of what progression sound to make
	public int platformProgression;
	public int score;
	public EndGameScript endGameScript;
	public TouchGesture.GestureSettings gestureSetting;
	private TouchGesture touch;
	//handles the red swiping particle system when locking down a level
	public ParticleScript particleSystemScript;
	public LevelBuilder levelBuilder;

	//top right counter how far you have moved
	public BarCounterController barCounterController;

	//stops someone from moving within an obsticle
	private bool disableMovement;

	//count of how many obstacles you have traversed
	private int obstaclesPassed;



	// Use this for initialization
	void Start () {
		Messenger.AddListener ("exitObstacle", exitedCollider);
		Messenger.AddListener ("enteredObstacle", enteredCollider);
		Messenger.AddListener ("clearOutLane", clearOutLane);
		Messenger.AddListener ("ranOutOfHealth", ranOutOfHealth);
		cam = Camera.main;
		scoreController = new ScoreController ();
		timerController = new TimerController (() => TimesUp ());
		cameraScript = cam.GetComponent<CameraScript> ();
		bounds = CameraExtensions.OrthographicBounds (cam);
		audioScript = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioScript> ();
		soundEffectScript = GameObject.FindGameObjectWithTag ("SoundEffectsController").GetComponent<SoundEffectsScript> ();
		currentGameLevel = LevelManager.Instance.getCurrentLevelDetail ();
		timerController.beginTimer(currentGameLevel.lengthInSeconds * 1000);
		#if UNITY_ANDROID
			touch = new TouchGesture(this.gestureSetting);
			StartCoroutine(touch.CheckHorizontalSwipes(
			onLeftSwipe: () => { moveRight();},
			onRightSwipe: () => {moveLeft(); }
			));
		#endif

	}

	private void TimesUp (){
		gameOver = true;
	}

	void moveLeft(){
		if (disableMovement)
			return;
		if (currentLaneId > 0 && !playerScript.isMoving ()) {
			setCurrentLaneId (currentLaneId - 1);
			cameraAndPlayer (isLeft:true);
			platformProgression = 0;
			barCounterController.emptyBars ();
		}
	}
	void moveRight(){

		if (disableMovement)
			return;
		if (currentLaneId + 1 < currentGameLevel.numberOfLanes && !playerScript.isMoving ()) {
			setCurrentLaneId (currentLaneId + 1);
			cameraAndPlayer (isLeft:false);
			platformProgression = 0;
			barCounterController.emptyBars ();
		}
	}
	void Update () {
		if (disableMovement)
			return;
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerRigidbody = player.GetComponent<Rigidbody2D> ();
			playerScript = player.GetComponent<PlayerScript> ();
		}
		if (gameOver) {
			if (playerScript.isMoving () || justMoved) {
				return;
			}
			Messenger.Broadcast ("disableJumping");
			Messenger.Broadcast<Scores,GameObject> ("gameOver",scoreController.getScores(),player);

			disableMovement = true;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			SceneManager.LoadScene ("Level Selection");
		}
		#if UNITY_5
		if (Input.GetKeyUp (KeyCode.C)) {
			levelBuilder.cleanUpObstacles (platformProgression);
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow) && currentLaneId > 0 && !playerScript.isMoving()){
			setCurrentLaneId (currentLaneId - 1);
			cameraAndPlayer (true);
			platformProgression = 0;
			barCounterController.emptyBars ();
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)&& currentLaneId + 1 < currentGameLevel.numberOfLanes && !playerScript.isMoving()){
			setCurrentLaneId (currentLaneId + 1);
			cameraAndPlayer (false);
			platformProgression = 0;
			barCounterController.emptyBars ();
		}
		#endif
	}

	private void setCurrentLaneId(int id){
		currentLaneId = id;
		Messenger.Broadcast ("jumped");
		Messenger.Broadcast ("changedLanes", id);
		audioScript.setCurrentLane (currentLaneId);
	}

	public void setStartingLane(int startingLane){
		setCurrentLaneId (startingLane);
		//player is being set inside of the level builder. consider moving this there. makes more sense but you wont get the moving camera feel.
		distanceToMoveX = (bounds.max.x * 2f);
		float dist = distanceToMoveX * startingLane;
		Vector3 cameraDestination = new Vector3 (cam.transform.position.x + dist, cam.transform.position.y, cam.transform.position.z);
		cameraScript.moveCameraToPosition (cameraDestination);
		justMoved = true;
	}

	void cameraAndPlayer(bool isLeft){
		distanceToMoveX = (bounds.max.x * 2f);
		float dist = isLeft ? -distanceToMoveX : distanceToMoveX;
		Vector3 cameraDestination = new Vector3 (cam.transform.position.x + dist, cam.transform.position.y, cam.transform.position.z);
		Vector3 playerDestination = new Vector3 (player.transform.position.x + dist, player.transform.position.y, player.transform.position.z);
		playerScript.moveToPosition (playerDestination);
		cameraScript.moveCameraToPosition (cameraDestination);
		justMoved = true;
	}

	private void movedOneLevelUp(){		
		currentPlatformLevel++;
		platformProgression++;
		soundEffectScript.playLevelProgression (platformProgression);
		barCounterController.addBar ();
		scoreController.addPlatform ();
		
		if (platformProgression % 4 == 0) {
			clearOutLane();
		}
		
	}

	public void clearOutLane(){
		scoreController.addLockDownLane ();
		audioScript.lockDownLane (currentLaneId);
		barCounterController.emptyBars ();
		platformProgression = 0;
		particleSystemScript.playParticleSystem ();
		Messenger.Broadcast<int> ("disableLane", currentLaneId);
		soundEffectScript.playWoosh ();
	}

	public void setCurrentPlatform(GameObject platform){
		if (currentPlatform != platform) {
			if (justMoved) {
				currentPlatform = platform;	
				justMoved = false;
				return;
			}
			if (currentPlatform != null) {
				if (!playerScript.isMoving ()) {
					Messenger.Broadcast<GameObject> ("showStars", platform);
					movedOneLevelUp ();
				}
			}

			currentPlatform = platform;
			Vector3 cameraDestination = new Vector3 (cam.transform.position.x, platform.transform.position.y + 2, cam.transform.position.z);
			cameraScript.moveCameraToPosition (cameraDestination);
		}
	}

	public void cleanUpLevel(){
		levelBuilder.cleanUpObstacles (currentPlatformLevel/2);
	}

	public void resetPlayer(){
		scoreController.removeScore (score);
		scoreController.addError ();
		soundEffectScript.playError ();
		playerRigidbody.velocity = new Vector2(0.0f,0.0f);
		Messenger.Broadcast<float> ("removeHealth", .3f);
		player.transform.position = currentPlatform.transform.position;
	}

	private void exitedCollider(){
		disableMovement = false;
		obstaclesPassed++;
		Messenger.Broadcast<int,int> ("addScore", currentLaneId,score);
		Messenger.Broadcast<float,bool> ("addHealth", .05f,false);
		Messenger.Broadcast<int> ("setRow", obstaclesPassed);
	}
	private void enteredCollider(){
		disableMovement = true;
	}

	private void ranOutOfHealth(){
		disableMovement = false;
		scoreController.completedLevel = false;
		timerController.endTimer ();
		gameOver = true;
	}
}
