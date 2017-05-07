using UnityEngine;
using System.Collections;

public class EntranceExitScript : MonoBehaviour {

	public string broadcastMessage;

	void OnTriggerEnter2D(Collider2D coll){
		Messenger.Broadcast (broadcastMessage);
	}
}
