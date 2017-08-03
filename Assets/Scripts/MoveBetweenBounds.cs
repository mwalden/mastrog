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
		if (transform.localPosition.x < cameraBounds.max.x && direction == 1) {
			transform.localPosition = new Vector3 (transform.localPosition.x + speed * Time.deltaTime,
				transform.localPosition.y, 
				transform.localPosition.z);
		} else if (transform.localPosition.x > cameraBounds.min.x && direction == 0){
			transform.localPosition = new Vector3 (transform.localPosition.x - speed * Time.deltaTime,
				transform.localPosition.y, 
				transform.localPosition.z);
		}

		if (transform.localPosition.x > cameraBounds.max.x)
			direction = 0;
		if (transform.localPosition.x < cameraBounds.min.x)
			direction = 1;

	}
}
