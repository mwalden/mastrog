using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Vector3 positionMovingTo;
	public bool moving;
	public float speed;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (moving) {

			float step = speed * Time.deltaTime;
			transform.position =  Vector3.MoveTowards(transform.position, positionMovingTo,step);
			moving = transform.position != positionMovingTo;
		}
	}

	public void moveCameraToPosition(Vector3 position){
//		Debug.Log ("Setting position");
//
//		Debug.Log ("from : " + transform.position);
//		Debug.Log ("to : " + positionMovingTo);
		positionMovingTo = position;
		moving = true;
	}

}
