using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	private GameScript gameScript;
	private Vector3 positionMovingTo;
	private bool moving;
	private Rigidbody2D rigidbody;
	public float speedToSwitchLanes;
	public bool isMoving(){
		return moving;
	}

	void Start(){
		gameScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameScript> ();
		rigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update(){
		if (moving) {
			float step = speedToSwitchLanes * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, positionMovingTo, step);
			moving = transform.position != positionMovingTo;
			if (!moving) {
				rigidbody.isKinematic = false;
			}
		}
	}


	public void resetPlayerPosition(){
		gameScript.resetPlayer ();
	}

	public void moveToPosition(Vector3 position){
		
		positionMovingTo = position;
		moving = true;
		rigidbody.isKinematic = true;

	}
}
