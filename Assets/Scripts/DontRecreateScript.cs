using UnityEngine;
using System.Collections;

public class DontRecreateScript : MonoBehaviour {
	public GameObject prefab;

	private static GameObject instance;

	void Awake(){
		
		if (instance == null) {
			instance = Instantiate (prefab);

		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (this);
	}
}
