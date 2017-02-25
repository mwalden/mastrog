using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	private bool playerInfoSet;
	private Vector3 destination;
	private Camera cam;
	private GameObject player;
	public float speed;
	private bool moving;
	private float playerSpeed;

	//the rate the player will move faster than the camera;
	public float playerSpeedIncrement;
	void Update(){
		if (moving) {
			playerSpeed += playerSpeedIncrement;
			player.transform.position = Vector3.MoveTowards(player.transform.position, destination,playerSpeed);
			cam.transform.position = Vector3.MoveTowards(cam.transform.position, destination,speed);
			Bounds bounds = CameraExtensions.OrthographicBounds (cam);
			if (cam.transform.position.y + bounds.size.y + 10f < player.transform.position.y)
				moving = false;
//			if (cam.transform.position.y >= destination.y)
//				moving = false;
		}
	}
	public void PlayEndGame(GameObject player){
		Debug.Log ("END GAME!");
		playerSpeed = speed;
		this.player = player;
		Collider2D[] colliders = GameObject.FindObjectsOfType<Collider2D> ();
		foreach (Collider2D collider2d in colliders) {
			collider2d.enabled = false;
		}
		player.GetComponent<Rigidbody2D> ().isKinematic = true;
		cam = Camera.main;
		destination = new Vector3 (player.transform.position.x,
			player.transform.position.y + 100,
			-10);
		moving = true;
	}
}
