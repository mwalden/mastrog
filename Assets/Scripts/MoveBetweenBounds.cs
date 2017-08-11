using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveBetweenBounds : MonoBehaviour {


	enum DIRECTION{
		TOP_LEFT,TOP_RIGHT,BOTTOM_LEFT,BOTTOM_RIGHT,CENTER
	}

	public float speed;
	private int direction = 1;
	public Bounds cameraBounds;
	private Camera gameCamera;
	private bool moving;
	public Vector3 movingTowards;
	private List<Vector3> stoppingVectors;

	private List<DIRECTION> directions = new List<DIRECTION> () {
		DIRECTION.TOP_LEFT,
		DIRECTION.TOP_RIGHT,
		DIRECTION.BOTTOM_LEFT,
		DIRECTION.BOTTOM_RIGHT,
		DIRECTION.CENTER
	};
	// Use this for initialization
	void Start () {
		gameCamera = Camera.main;
		cameraBounds = CameraExtensions.OrthographicBounds(gameCamera);
		Vector3 topLeft = new Vector3 (cameraBounds.min.x, cameraBounds.max.y-1, 1);
		Vector3 topRight = new Vector3 (cameraBounds.max.x, cameraBounds.max.y-1, 1);
		Vector3 bottomLeft = new Vector3 (cameraBounds.min.x, cameraBounds.min.y, 1);
		Vector3 bottomRight = new Vector3 (cameraBounds.max.x, cameraBounds.min.y, 1);
		Vector3 center = new Vector3 (3.5f,0, 1);
		movingTowards = topLeft;
		transform.localPosition = center;
		movingTowards = center;
		Debug.Log (cameraBounds.max);
		Debug.Log (cameraBounds.min);
		Debug.Log (center);
		stoppingVectors = new List<Vector3> (){ topLeft, topRight, bottomLeft, bottomRight, center };
	}
	
	void Update () {
//		print (transform.localPosition);
//		float step = speed * Time.deltaTime;
//		transform.localPosition = Vector3.MoveTowards(transform.localPosition, movingTowards, step);
//		if (!moving) {
//			StartCoroutine (pickDirection ());
//			moving = true;
//		}

//		if (!moving) {
//			moving = true;
//			moveLeft = UnityEngine.Random.Range (0, 2)==0;
//			moveUp = UnityEngine.Random.Range (0, 2)==0;
//			print ("Move Left : " + moveLeft);
//			print ("Move Up : " + moveUp);
//		}
//		float newX = transform.localPosition.x;
//		float newY = transform.localPosition.y;
//
//		if (transform.localPosition.x < cameraBounds.max.x && moveLeft) {
//			newX = transform.localPosition.x + speed * Time.deltaTime;
//		} else if (transform.localPosition.x > cameraBounds.min.x && !moveLeft){
//			newX = transform.localPosition.x - speed * Time.deltaTime;
//		}
//
//		if (transform.localPosition.y < cameraBounds.max.y && moveUp) {
//			newY = transform.localPosition.y + speed * Time.deltaTime;
//		} else if (transform.localPosition.y > cameraBounds.min.y && !moveUp){
//			newY = transform.localPosition.y - speed * Time.deltaTime;
//		}
//
//		transform.localPosition = new Vector3 (newX, newY, transform.localPosition.z);

//		if (transform.localPosition.x > cameraBounds.max.x)
//			direction = 0;
//		if (transform.localPosition.x < cameraBounds.min.x)
//			direction = 1;

	}
	IEnumerator pickDirection(){
		print ("START : " +Time.time);
		yield return new WaitForSeconds (10f);
		int direction = UnityEngine.Random.Range (0, 4);
		movingTowards = stoppingVectors [direction];
		moving = false;

	}

}
