using UnityEngine;
using System.Collections;

public class CurrentLevelScript : MonoBehaviour {
	public Level level;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}

}
