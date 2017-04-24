using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour {

	public float speed;
	private Camera camera;
	public GameObject prefab;
	public float diff;
	private Bounds bounds;
	public float spacer;

	private List<GameObject> prefabs;

	void Start () {
		prefabs = new List<GameObject> ();
		camera  = Camera.main;
		bounds = CameraExtensions.OrthographicBounds (Camera.main);
		float max = bounds.max.y;
		float min = bounds.min.y;
		float current = max;
		while (current > min) {
			GameObject go = Instantiate (prefab, new Vector3 (0f, current, 10f), Quaternion.identity) as GameObject;
			go.transform.SetParent (this.transform);
			current -= spacer;
			prefabs.Add (go);
		}
	}
	
	void Update () {		
		diff += speed;
		transform.position -= new Vector3(0f,speed,0f);
		if (diff >= spacer) {
			diff = 0;
			GameObject old = prefabs [prefabs.Count - 1];
			old.transform.position = new Vector3 (this.transform.position.x, bounds.max.y + camera.transform.position.y, 10f);
			prefabs.Remove (old);
			prefabs.Insert (0, old);

		}
		
	}
}
