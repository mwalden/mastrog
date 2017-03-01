using UnityEngine;
using System.Collections;

public class ScalingScript : MonoBehaviour {
	public float scalingSpeed;
	bool shrinking;
	void Update(){
		getShrinkingStatus ();
		
		if (shrinking) {
			setVector (transform.localScale.x-(scalingSpeed * Time.deltaTime));
		} else {
			setVector(transform.localScale.x + (scalingSpeed * Time.deltaTime));
		}


		
	}

	bool getShrinkingStatus(){
		if (transform.localScale.x >= 1 && !shrinking)
			shrinking = true;
		if (transform.localScale.x <= .5 && shrinking)
			shrinking = false;
		return false;
	}

	void setVector(float v){
		Vector3 vector = new Vector3 (v, v, transform.localScale.z);
		transform.localScale = vector;
	}
}
