using UnityEngine;
using System.Collections;

public class PowerBoxScript : MonoBehaviour {
	public float boxSpeed = 2f;
	public bool keepMoving = true;
	private Transform _transform;


	void Start(){
		_transform = transform;

	}
	void Update () {
		if (!keepMoving)
			return;
		transform.position = new Vector3 (_transform.position.x - boxSpeed * Time.deltaTime,
			_transform.position.y,
			0);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag != "Player")
			return;
		Messenger.Broadcast<Vector3> ("boxOpened",_transform.position);
		_transform.position = new Vector3 (100, 100, 100);



	}

	
}
