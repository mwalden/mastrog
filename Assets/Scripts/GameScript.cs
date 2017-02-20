using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
	GameObject currentPlatform;
	GameObject player;
	Rigidbody2D playerRigidbody;
	PlayerScript playerScript;
	Camera cam;
	CameraScript cameraScript;
	public float distanceToMove;
	private bool justMoved;
	private int currentPlatformLevel;

	Bounds bounds;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		cameraScript = cam.GetComponent<CameraScript> ();
		bounds = CameraExtensions.OrthographicBounds (cam);
	}
	
	void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerRigidbody = player.GetComponent<Rigidbody2D> ();
			playerScript = player.GetComponent<PlayerScript> ();
		}

		if (Input.GetKeyUp(KeyCode.LeftArrow)){
			cameraAndPlayer (true);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)){
			cameraAndPlayer (false);
		}
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
	}

	public void setCurrentPlatform(GameObject platform){
		if (currentPlatform != platform) {
			if (justMoved) {
				currentPlatform = platform;	
				justMoved = false;
				return;
			}
			if (currentPlatform != null) {
				if (playerScript.isMoving ()) {
					movedOneLevelUp ();
				}
			}

			currentPlatform = platform;
			Vector3 cameraDestination = new Vector3 (cam.transform.position.x, platform.transform.position.y + 2, cam.transform.position.z);
			cameraScript.moveCameraToPosition (cameraDestination);
		}
	}

	public void resetPlayer(){
		playerRigidbody.velocity = new Vector2(0.0f,0.0f);
		player.transform.position = currentPlatform.transform.position;
	}
}
