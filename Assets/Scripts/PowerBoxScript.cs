using UnityEngine;
using System.Collections;

public class PowerBoxScript : MonoBehaviour {

	public GameObject burstPrefab;
	public float boxSpeed = 2f;
	public bool keepMoving = true;
	private ParticleSystem burst;

	void Start(){
		burst = (Instantiate (burstPrefab, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject).GetComponent<ParticleSystem>();		
	}

	void Update () {
		if (!keepMoving)
			return;
		transform.position = new Vector3 (transform.position.x - boxSpeed * Time.deltaTime,
			transform.position.y,
			0);
	}

	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log ("entered");
		burst.transform.position = transform.position;
		transform.position = new Vector3 (100, 100, 100);
		this.burst.Play ();
		Messenger.Broadcast ("clearOutLane");
	}

	
}
