﻿using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public float speed;
	
	void Update () {
		transform.Rotate (0, 0, speed * Time.deltaTime);
	}
}
