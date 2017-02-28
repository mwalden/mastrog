﻿using UnityEngine;
using System.Collections;

public class JumpScript : MonoBehaviour {

	public float force;
//	public GameController controller;
//	private PlayerScript ps;
	private GameObject currentRestBar;
	private Rigidbody2D rb;
	public LayerMask layerMask;
	void Start(){
		rb = GetComponent<Rigidbody2D> ();
//		ps = GetComponent<PlayerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			//prevent double jump
			if (rb.velocity.y <= 0.1) {
				rb.velocity =  (new Vector2 (0, force));

			}
		}
		#if UNITY_ANDROID
		if (Input.touches.Length > 0) {
			Touch touch = Input.touches [0];

			if (touch.phase==TouchPhase.Ended && rb.velocity.y <= 0.1){
				rb.velocity = (new Vector2 (0, force));
			}
			return;
		}
		#endif
	}


	void FixedUpdate() {
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		Vector3 position = go.transform.position;

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up,100f,layerMask);
		if (hit.collider != null) {
			
			float distance = Mathf.Abs(hit.transform.position.y-position.y);
			float g = Physics.gravity.magnitude; // get the gravity value
//			Debug.Log(distance);
			force = Mathf.Sqrt(2 * g * distance) + hit.collider.bounds.size.y+.25f;
//			Debug.Log ("Force : " + force);

		}
	}
}
