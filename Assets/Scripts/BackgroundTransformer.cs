using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundTransformer : MonoBehaviour {


	public GameObject[] backgrounds;
	private GameObject currentBackground;

	void Start(){
		Messenger.AddListener< string >( "enableBackground", EnableBackground );
	}

	void EnableBackground(string background){
		print ("Got " + background);
		if (currentBackground != null)
			currentBackground.SetActive (false);
		foreach(GameObject g in backgrounds){
			if (g.name.Equals (background)) {
				print("Found " + background);
				currentBackground = g;
				currentBackground.SetActive (true);
			}
		}

	}

}
