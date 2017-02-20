using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {
	private GameScript gameScript;

	void Start(){
		gameScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameScript> ();
	}
//	void OnCollisionExit2D(Collision2D coll){
//		gameScript.setCurrentPlatform (gameObject);
//	}

	void OnCollisionEnter2D(Collision2D coll){
		gameScript.setCurrentPlatform (gameObject);
	}
}
