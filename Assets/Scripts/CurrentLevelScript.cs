using UnityEngine;
using System.Collections;

public class CurrentLevelScript : MonoBehaviour {
	public NewLevel level;
	bool created;

	void Awake() {
		if (!created) {
			// this is the first instance - make it persist
			DontDestroyOnLoad(this.gameObject);
			created = true;
		} else {
			// this must be a duplicate from a scene reload - DESTROY!
			Destroy(this.gameObject);
		} 
	}

}
