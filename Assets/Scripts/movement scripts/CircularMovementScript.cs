using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircularMovementScript : MonoBehaviour {
	public GameObject prefab;
	public int total;
	private List<GameObject> obstacles;
	public float speed;
	public Vector3 leftBound;
	private Vector3 originalLeft;
	private List<Vector3> obstacleBounds;
	private List<Vector3> obstacleOrigins;
	public GameObject children;
	//move this to start 
	GameObject go;
	Vector3 newPosition = new Vector3();
	Vector3 destination;
	Vector3 origin;
	public bool movingOut = true;
	float startingZ = 0;
	void Start () {
		obstacles = new List<GameObject> ();
		float angleDifference = 360 / total;
		float width;
		Bounds bounds = CameraExtensions.OrthographicBounds (Camera.main);
		obstacleBounds = new List<Vector3> ();
		obstacleOrigins = new List<Vector3> ();

		float radians = angleDifference * Mathf.Deg2Rad;
		for (int i = 0; i < total; i++) {
			float x = Mathf.Cos(radians + (radians * i));
			float y = Mathf.Sin(radians + (radians * i));
			Vector3 position = new Vector3 (transform.position.x + x, transform.position.y + y, 1f);
//			Vector3 position = new Vector3 (x, y, 1f);
			GameObject go = Instantiate (prefab, position, Quaternion.identity) as GameObject;
//			go.transform.localPosition = position;
			go.transform.SetParent (children.transform);
			width = go.gameObject.GetComponent<SpriteRenderer> ().bounds.size.x;
			obstacleOrigins.Add (go.transform.localPosition);
			obstacleBounds.Add(new Vector3(go.transform.localPosition.x*3,go.transform.localPosition.y * 3,1));
			obstacles.Add (go);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		float step = speed * Time.deltaTime;
//
		for (int i = 0; i < total; i++) {
			GameObject go = obstacles [i];
			destination = obstacleBounds [i];
			origin = obstacleOrigins [i];
			if (movingOut)
				newPosition = Vector3.Lerp (go.transform.localPosition, destination, step);
			else
				newPosition = Vector3.Lerp (go.transform.localPosition, origin, step);
			go.transform.localPosition = new Vector3 (newPosition.x, newPosition.y, startingZ);
		}
		GameObject lastObject = obstacles [total - 1]; 

		if (movingOut) {
			destination = obstacleBounds [total - 1];
			if (Mathf.Abs (destination.x) - Mathf.Abs (lastObject.transform.localPosition.x) < .3f) {
//				print ("turning off moving out");
				movingOut = false;
			}
		}
		else {
			origin = obstacleOrigins [total - 1];
			if (Mathf.Abs (lastObject.transform.localPosition.x) - Mathf.Abs (origin.x) < .3f) {
//				print ("turning on moving out");
				movingOut = true;
			}
		}
	}
}
