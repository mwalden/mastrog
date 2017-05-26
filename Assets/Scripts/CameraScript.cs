using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Vector3 positionMovingTo;
	public bool moving;
	public float speed;
	private GameScript gameScript;

	// Use this for initialization
	void Start () {
		gameScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameScript> ();
	}

	// Update is called once per frame
	void Update () {
		if (moving) {
			float step = speed * Time.deltaTime;
			transform.position =  Vector3.MoveTowards(transform.position, positionMovingTo,step);
			if (transform.position == positionMovingTo){
				moving = false;
				gameScript.cleanUpLevel ();
			}
		}
	}

	public void moveCameraToPosition(Vector3 position){
		if (moving)
			return;	
		
		positionMovingTo = position;
		moving = true;
	}

}
