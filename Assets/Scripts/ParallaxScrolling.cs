using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour {

	public GameObject toFollow;
	private Vector3 oldFollowingPosition;
	public float speed = .25f;

	void Start(){
		oldFollowingPosition = toFollow.transform.position;
	}
	void Update(){
		float newX = transform.position.x;
		float newY = transform.position.y;

		if (oldFollowingPosition.x != toFollow.transform.position.x) {
			if (transform.position.x < toFollow.transform.position.x) {
				float diff = oldFollowingPosition.x - toFollow.transform.position.x;	
				newX = diff * speed + transform.position.x;
			} else {
				float diff = toFollow.transform.position.x - oldFollowingPosition.x;
				newX = transform.position.x - diff * speed;
			}
		}

		if (oldFollowingPosition.y != toFollow.transform.position.y) {
			
			if (transform.position.y < toFollow.transform.position.y) {
				float diff = oldFollowingPosition.y - toFollow.transform.position.y;	
				newY = diff * .01f + transform.position.y;
			} else {
				float diff = toFollow.transform.position.y -oldFollowingPosition.y;
				newY = transform.position.y - diff * .01f;
			}
		}

		transform.position = new Vector3 (newX, newY, transform.position.z);
		oldFollowingPosition = toFollow.transform.position;
		
	}


	public void reset(){
		transform.position = new Vector3 (0, 0, 0);
		toFollow.transform.position = new Vector3 (0, 0, 0);
	}
}
