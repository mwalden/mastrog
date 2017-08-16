using UnityEngine;
using System.Collections;

public class BounceLeftRightScript : MonoBehaviour {
	private Bounds bounds;
	private Transform left;
	private Transform right;
	// Use this for initialization
	private float width;
	private Vector3 leftBound;
	private Vector3 rightBound;

	public float speed;

	void Start () {
		bounds = CameraExtensions.OrthographicBounds (Camera.main);
		Transform[] gos = gameObject.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < gos.Length; i++) {
			Transform go = gos [i];
			if (go.name == "left")
				left = go;
			else if (go.name=="right")
				right = go;
			
		}
		width = left.gameObject.GetComponent<SpriteRenderer> ().bounds.size.x;
		leftBound = new Vector3(bounds.min.x + width/2, left.transform.position.y,left.transform.position.z);
		rightBound = new Vector3 (bounds.max.x - width/2, left.transform.position.y, left.transform.position.z);
	}
	

	void Update () {
		float step = speed * Time.deltaTime;
//		left.transform.position = Vector3.MoveTowards(left.transform.position, leftBound, step);
//		right.transform.position = Vector3.MoveTowards(right.transform.position, rightBound, step);


		left.transform.position = Vector3.Lerp(left.transform.position, leftBound, step);
		right.transform.position = Vector3.Lerp(right.transform.position, rightBound, step);
	}
}
