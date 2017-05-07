using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public float speed;
	public bool turnLeft = true;

	private int direction;
	void Start(){
		direction = turnLeft ? 1 : -1;
	}
	void Update () {
		transform.Rotate (0, 0, direction * speed * Time.deltaTime);
	}
}
