using UnityEngine;
using System.Collections;

public class PowerBoxScript : MonoBehaviour {

	public ParticleSystem particleSystem;
	public float boxSpeed = 2f;

	void Update () {
		transform.position = new Vector3 (transform.position.x - boxSpeed * Time.deltaTime,
			transform.position.y,
			0);
	}

	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log ("entered");
		this.particleSystem.Play ();
	}

	
}
