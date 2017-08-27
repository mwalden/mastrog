using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	private GameScript gameScript;
	private Vector3 positionMovingTo;
	private bool moving;
	private Rigidbody2D rigidbody;
	public float speedToSwitchLanes;
	private float distanceTraveled;
	private float baseYValue;

	public bool isMoving(){
		return moving;
	}

	private float getYValue(float x){
		return (-((Mathf.Pow(.333f*x -1,2))) + 1f)/3f;
	}

	void Start(){
		gameScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameScript> ();
		rigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update(){
		
		if (moving) {			
			float step = speedToSwitchLanes * Time.deltaTime;
			distanceTraveled += step;
			Vector3 newPosition = Vector3.MoveTowards (transform.position, positionMovingTo, step);
			Vector3 newerPosition = new Vector3 (newPosition.x, getYValue(Mathf.Abs (distanceTraveled))+ baseYValue, newPosition.z);
			transform.position = newerPosition;
			moving = transform.position.x != positionMovingTo.x;
			if (!moving) {
				rigidbody.isKinematic = false;
			}
		}
	}


	public void resetPlayerPosition(){
		gameScript.resetPlayer ();
	}

	public void moveToPosition(Vector3 position){
		distanceTraveled = 0;
		baseYValue = transform.position.y;
		positionMovingTo = position;
		moving = true;
		rigidbody.isKinematic = true;

	}
}
