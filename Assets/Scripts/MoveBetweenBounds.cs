using UnityEngine;
using System.Collections;

public class MoveBetweenBounds : MonoBehaviour {


	public float speed;
	private int direction = 1;
	private Bounds cameraBounds;
	private Camera gameCamera;
	private Vector3 cameraPosition;
	// Use this for initialization
	void Start () {
		gameCamera = Camera.main;
		cameraBounds = CameraExtensions.OrthographicBounds(gameCamera);
		cameraPosition = new Vector3 (gameCamera.transform.position.x, gameCamera.transform.position.y+ cameraBounds.max.y - 1, 0);
	}
	
	void Update () {
		print ("local . " + transform.localPosition.x);
		print ("position . " + cameraBounds.max.x);
//
		if (transform.localPosition.x < cameraBounds.max.x) {
			transform.localPosition = new Vector3 (transform.localPosition.x + speed * Time.deltaTime,
				transform.localPosition.y, 
				transform.localPosition.z);
		}
	}
}
