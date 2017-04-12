using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadDataController : MonoBehaviour {

	bool loaded;
	public int partLoaded;
	public Slider slider; 
	void Start () {
		StartCoroutine (loadData());
	}
	public int numberToLoad;
	
	// Update is called once per frame
	IEnumerator loadData () {
		if (!loaded) {
			for (int x = 0; x < numberToLoad; x++) {
				string path = "Audio" + "/First/" + (x + 1);
				Debug.Log (path);
				ResourceRequest resourceRequest = Resources.LoadAsync<AudioClip> (path);
				while (!resourceRequest.isDone) {
					Debug.Log ("still loading");
					yield return 0;
				}
				partLoaded++;
				slider.value = partLoaded / numberToLoad;
				Debug.Log ("part got loaded");
			}
			Debug.Log ("LOADED!");
			loaded = true;
		} else {
			Debug.Log ("already loaded");
			yield return null;
		}
	}
}
