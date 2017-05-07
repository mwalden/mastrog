using UnityEngine;
using System.Collections;

public class ParticleBackgroundSystem : MonoBehaviour {
	public GameObject prefab;
	private Camera gameCamera;
	private Bounds bounds;
	// Use this for initialization
	void Start () {
		gameCamera = Camera.main;
		bounds = CameraExtensions.OrthographicBounds (gameCamera);
	}
	bool created;
	GameObject go;
	public Vector3 started;
	public Vector3 ended;
	private float journeyLength;
	public float speed = 1.0F;
	private float startTime;

	public float delayTime = 3.0f;
	// Update is called once per frame
	void Update () {
		
		if (delayTime > 0) {
			delayTime -= Time.deltaTime;
			return;
		}
			
		if (!created) {
			created = true;
			startTime = Time.time;
			started = new Vector3 (gameCamera.transform.position.x+ bounds.max.x + 1, gameCamera.transform.position.y+ bounds.max.y - 1, 0);
			ended = new Vector3 (bounds.min.x - 1, bounds.min.y + 1, 0);
			journeyLength = Vector3.Distance(started, ended);
			go = Instantiate (prefab,started , Quaternion.identity) as GameObject;
		} else {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			go.transform.position = Vector3.Lerp(started, ended, fracJourney);
		}
	}
}
