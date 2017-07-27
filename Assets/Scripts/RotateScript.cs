using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RotateScript : MonoBehaviour {

	public float speed = 100.0f;
	public bool turnLeft = true;
	public bool xAxis = false;
	public bool yAxis = false;
	public bool zAxis = true;

	private int x;
	private int y;
	private int z;

	private int direction;
	private Transform _transform;
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

}
