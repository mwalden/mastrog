using UnityEngine;
using System.Collections;

public class BackgroundTransformer : MonoBehaviour {

	public bool rotateX;
	public bool rotateY;
	public bool rotateZ;

	public float speed;

	// Update is called once per frame
	void Update () {
		float newX = (rotateX)?speed*Time.deltaTime:0;
		float newY = (rotateY)?speed*Time.deltaTime:0;
		float newZ = (rotateZ)?speed*Time.deltaTime:0;
		transform.Rotate(new Vector3(newX,newY,newZ));
	}
}
