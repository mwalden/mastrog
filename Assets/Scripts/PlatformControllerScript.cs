using UnityEngine;
using System.Collections;

public class PlatformControllerScript : MonoBehaviour {
	GameObject[] platforms;
	Rigidbody2D playerRigidbody2d;

	void Update () {
		if (platforms == null) {
			platforms = GameObject.FindGameObjectsWithTag ("platform");
		}
		if (playerRigidbody2d == null) {
			playerRigidbody2d = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
		} else {
			
			if (playerRigidbody2d.velocity.y  <.03f)
				
				enablePlatforms ();
			else
				disablePlatforms ();
		}
	}

	public void enablePlatforms(){
		foreach (GameObject go in platforms) {
			go.GetComponent<BoxCollider2D> ().enabled = true;
		}
	}


	public void disablePlatforms(){
		platforms = GameObject.FindGameObjectsWithTag ("platform");
		foreach (GameObject go in platforms) {
			go.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}
}
