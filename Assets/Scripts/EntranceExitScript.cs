using UnityEngine;
using System.Collections;

public class EntranceExitScript : MonoBehaviour {

	public string broadcastMessage;

	void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.gameObject.tag == "Player")
			Messenger.Broadcast (broadcastMessage);
	}
}
