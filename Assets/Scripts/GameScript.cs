using UnityEngine;
using System.Collections;
using System;

public class GameScript : MonoBehaviour {
	GameObject currentPlatform;
	GameObject player;
	Rigidbody2D playerRigidbody;
	PlayerScript playerScript;
	AudioScript audioScript;
	SoundEffectsScript soundEffectScript;
	public ParticleScript particleSystemScript;
	public LevelBuilder levelBuilder;
	Camera cam;
	CameraScript cameraScript;
	public float distanceToMove;
	private bool justMoved;
	//going up the game. counts how many platforms youve gone up.
	public int currentPlatformLevel;
	//keeps track of what progression sound to make
	public int platformProgression;

	//going left/right on the platforms
	private int currentLaneId;
	private Level currentGameLevel;

	public TouchGesture.GestureSettings GestureSetting;
	private TouchGesture touch;

	Bounds bounds;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		cameraScript = cam.GetComponent<CameraScript> ();
		bounds = CameraExtensions.OrthographicBounds (cam);
		audioScript = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioScript> ();
		soundEffectScript = GameObject.FindGameObjectWithTag ("SoundEffectsController").GetComponent<SoundEffectsScript> ();
		#if UNITY_ANDROID
		touch = new TouchGesture(this.GestureSetting);
		StartCoroutine(touch.CheckHorizontalSwipes(
			onLeftSwipe: () => {  moveRight(); },
			onRightSwipe: () => {moveLeft(); }
		));
		#endif

	}



	void moveLeft(){
		if (currentLaneId > 0 && !playerScript.isMoving ()) {
			setCurrentLaneId (currentLaneId - 1);
			cameraAndPlayer (isLeft:true);
			platformProgression = 0;
		}
	}
	void moveRight(){
		if (currentLaneId + 1 < currentGameLevel.numberOfLanes && !playerScript.isMoving ()) {
			setCurrentLaneId (currentLaneId + 1);
			cameraAndPlayer (isLeft:false);
			platformProgression = 0;
		}
	}
	void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerRigidbody = player.GetComponent<Rigidbody2D> ();
			playerScript = player.GetComponent<PlayerScript> ();
		}
		#if UNITY_5
		if (Input.GetKeyUp (KeyCode.C)) {
			levelBuilder.cleanUpObstacles (platformProgression);
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow) && currentLaneId > 0 && !playerScript.isMoving()){
			setCurrentLaneId (currentLaneId - 1);
			cameraAndPlayer (true);
			platformProgression = 0;
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)&& currentLaneId + 1 < currentGameLevel.numberOfLanes && !playerScript.isMoving()){
			setCurrentLaneId (currentLaneId + 1);
			cameraAndPlayer (false);
			platformProgression = 0;
		}
		#endif

	}

	public void setCurrentGameLevel(Level gameLevel){
		this.currentGameLevel = gameLevel;
		audioScript.setGameLevel (gameLevel);
	}

	private void setCurrentLaneId(int id){
		currentLaneId = id;
		audioScript.setCurrentLane (currentLaneId);
	}

	public void setStartingLane(int startingLane){
		setCurrentLaneId (startingLane);
		//player is being set inside of the level builder. consider moving this there. makes more sense but you wont get the moving camera feel.
		distanceToMove = (bounds.max.x * 2f);
		float dist = distanceToMove * startingLane;
		Vector3 cameraDestination = new Vector3 (cam.transform.position.x + dist, cam.transform.position.y, cam.transform.position.z);
//		Vector3 playerDestination = new Vector3 (player.transform.position.x + dist, player.transform.position.y, player.transform.position.z);
//		playerScript.moveToPosition (playerDestination);
		cameraScript.moveCameraToPosition (cameraDestination);
		justMoved = true;
	}

	void cameraAndPlayer(bool isLeft){
		distanceToMove = (bounds.max.x * 2f);
		float dist = isLeft ? -distanceToMove : distanceToMove;
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
		if (platformProgression % 4 == 0) {
			audioScript.lockDownLane (currentLaneId);
			platformProgression = 0;
			particleSystemScript.playParticleSystem ();
			soundEffectScript.playWoosh ();
		}
		
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
		soundEffectScript.playError ();
		playerRigidbody.velocity = new Vector2(0.0f,0.0f);
		player.transform.position = currentPlatform.transform.position;
	}
}
