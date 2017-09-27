using UnityEngine;
using System.Collections;

public class EntranceExitScript : MonoBehaviour {

	public string broadcastMessage;

	void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.gameObject.tag == "Player") {
			print ("Entering");
			Messenger.Broadcast<GameObject> (broadcastMessage,gameObject);
			if (broadcastMessage.Equals ("exitObstacle")) {
				Messenger.Broadcast ("disappear",gameObject.transform.parent.gameObject);
			}
		}
	}

}
