using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RotateScript : MonoBehaviour {

	public float speed = 100.0f;
	private float maxSpeed = 300.0f;
	public bool turnLeft = true;
	public bool xAxis = false;
	public bool yAxis = false;
	public bool zAxis = true;

	private int x;
	private int y;
	private int z;

	private int direction;
	private Transform _transform;
	private float speedUpTime = 10f;
	void Start(){
		_transform = transform;
		direction = turnLeft ? 1 : -1;
	}
	void Update () {
		x = xAxis ? 1 : 0;
		y = yAxis ? 1 : 0;
		z = zAxis ? 1 : 0;

		_transform.Rotate (x * direction * speed * Time.deltaTime, 
			y * direction * speed * Time.deltaTime, 
			z * direction * speed * Time.deltaTime);
	}


	public void speedUpPowerup(){
		StartCoroutine (speedUpRotation(5f));
	}

	IEnumerator speedUpRotation(float seconds){

		float initialSpeed = speed;
		print("BEFORE " + speed);
		while (speed < maxSpeed - 1) {
			speed = Mathf.Lerp (speed, maxSpeed, .1f);
			yield return new WaitForSeconds (.02f);
		}

		yield return new WaitForSeconds (seconds);

		while (initialSpeed < speed -1 ){
			print("after " + (initialSpeed < speed -1 ));
			speed = Mathf.Lerp (speed, initialSpeed, .1f);
			yield return new WaitForSeconds (.02f);
		}
		//make sure it goes back to the exact value
		speed = initialSpeed;
	}

}
