using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BarCounterController : MonoBehaviour {
	public Sprite empty;
	public Sprite full;
	public List<GameObject> bars;
	//bar we should update when we get told to.
	private int barNumber;


	public void addBar(){
		GameObject go = bars [barNumber];
		go.GetComponent<Image> ().sprite = full;
		barNumber++;
	}

	public void emptyBars(){
		foreach (GameObject go in bars) {
			go.GetComponent<Image> ().sprite = empty;
		}
		barNumber = 0;
	}
}
