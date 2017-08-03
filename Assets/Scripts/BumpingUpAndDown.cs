using UnityEngine;
using System.Collections;

public class BumpingUpAndDown : MonoBehaviour {
	//this was used on that floating square. probably wont use it anymore.
	public float diff = 1f;
	public float distance;
	public float switchTime;

	private float timeDiff;
	// Update is called once per frame
	void Start(){
		timeDiff = switchTime;
	}

	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y + (diff * distance * Time.deltaTime), transform.position.y);
		timeDiff -= Time.deltaTime;
		if (timeDiff < 0) {
			diff *= -1;
			timeDiff = switchTime;
		}
	}
}
