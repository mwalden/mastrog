using UnityEngine;
using System.Collections;

public class ObsticleScript : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag != "Player")
			return;
		GameObject go = coll.gameObject;
		PlayerScript playerScript = go.GetComponent<PlayerScript> ();
		playerScript.resetPlayerPosition ();
	}
}
