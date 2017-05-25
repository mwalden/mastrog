using UnityEngine;
using System.Collections;

public class HudObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Messenger.AddListener ("HideHud", hide);
	}
	
	void OnDestroy () {
		Messenger.RemoveListener ("HideHud",hide);
	}

	void hide(){
		gameObject.SetActive (false);
	}
}
