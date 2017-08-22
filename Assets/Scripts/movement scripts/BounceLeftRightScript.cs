using UnityEngine;
using System.Collections;

public class BounceLeftRightScript : MonoBehaviour {
	private Bounds bounds;
	private Transform left;
	private Transform right;
	// Use this for initialization
	private float width;
	public Vector3 leftBound;
	public Vector3 rightBound;
	private Vector3 originalLeft;
	private Vector3 originalRight;

	public float leftYBound = 2f;
	public float rightYBound =1f;
	public bool movingOut = true;
	public float speed;
	private float z = -5f;

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
		originalLeft = left.transform.localPosition;
		originalRight = right.transform.localPosition;
		leftBound = new Vector3(bounds.min.x + width/2, left.transform.localPosition.y + leftYBound,left.transform.position.z);
		rightBound = new Vector3 (bounds.max.x - width/2, left.transform.localPosition.y + rightYBound, left.transform.position.z);
	}
	

	void Update () {
		float step = speed * Time.deltaTime;
		if (Mathf.Abs(leftBound.x)- Mathf.Abs(left.transform.localPosition.x)  < .3f && movingOut) {
			movingOut = false;
		}
		if (Mathf.Abs(left.transform.localPosition.x) - Mathf.Abs(originalLeft.x) < .02f && !movingOut) {
			movingOut = true;
		}
		if (movingOut) {
			Vector3 newLeft = Vector3.Lerp (left.transform.localPosition, leftBound, step);
			Vector3 newRight = Vector3.Lerp (right.transform.localPosition, rightBound, step);
			left.transform.localPosition = new Vector3 (newLeft.x, newLeft.y, z);
			right.transform.localPosition =new Vector3 (newRight.x, newRight.y, z); 
		}
		if (!movingOut) {
			Vector3 newLeft = Vector3.Lerp (left.transform.localPosition, originalLeft, step);
			Vector3 newRight = Vector3.Lerp (right.transform.localPosition, originalRight, step);
			left.transform.localPosition = new Vector3 (newLeft.x, newLeft.y, z);
			right.transform.localPosition =new Vector3 (newRight.x, newRight.y, z); 
		} 
	}
}
