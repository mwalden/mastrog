﻿using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {
	private GameScript gameScript;
	public ParticleSystem starEffect;

	void Start(){
		gameScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameScript> ();
	}

	void OnCollisionEnter2D(Collision2D coll){
		Messenger.Broadcast ("landed");
		gameScript.setCurrentPlatform (gameObject);
	}
}
