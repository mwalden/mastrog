using UnityEngine;
using System.Collections;

public class ScalingScript : MonoBehaviour {
	public float scalingSpeed;
	bool shrinking;
	private Transform _transform;

	void Start(){
		_transform = transform;
	}

	void Update(){
		getShrinkingStatus ();
		
		if (shrinking) {
			setVector (_transform.localScale.x-(scalingSpeed * Time.deltaTime));
		} else {
			setVector(_transform.localScale.x + (scalingSpeed * Time.deltaTime));
		}
	}

	bool getShrinkingStatus(){
		if (_transform.localScale.x >= 1 && !shrinking)
			shrinking = true;
		if (_transform.localScale.x <= .5 && shrinking)
			shrinking = false;
		return false;
	}

	void setVector(float v){
		Vector3 vector = new Vector3 (v, v, _transform.localScale.z);
		_transform.localScale = vector;
	}
}
