﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthController : MonoBehaviour {
	//this was for that scene where we werent using the scale. 
	//not in the game
	public Image health;

	public float totalHealth = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		health.fillAmount = totalHealth;
	}
}
