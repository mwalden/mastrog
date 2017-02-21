using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
	GameObject currentPlatform;
	GameObject player;
	Rigidbody2D playerRigidbody;
	PlayerScript playerScript;
	AudioScript audioScript;
	SoundEffectsScript soundEffectScript;
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


	Bounds bounds;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		cameraScript = cam.GetComponent<CameraScript> ();
		bounds = CameraExtensions.OrthographicBounds (cam);
		audioScript = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioScript> ();
		soundEffectScript = GameObject.FindGameObjectWithTag ("SoundEffectsController").GetComponent<SoundEffectsScript> ();
	}

	void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerRigidbody = player.GetComponent<Rigidbody2D> ();
			playerScript = player.GetComponent<PlayerScript> ();
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
		if (platformProgression % 3 == 0) {
			audioScript.lockDownLane (currentLaneId);
			platformProgression = 0;
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

	public void resetPlayer(){
		soundEffectScript.playError ();
		playerRigidbody.velocity = new Vector2(0.0f,0.0f);
		player.transform.position = currentPlatform.transform.position;
	}
}
