using UnityEngine;
using System.Collections;

public class StarBurstScript : MonoBehaviour {

	public ParticleSystem starBurst;
	// Use this for initialization
	void Start () {
		starBurst = GetComponent<ParticleSystem> ();
		Messenger.AddListener<GameObject>( "showStars", playParticleSystem );
	}
	

	private void playParticleSystem(GameObject go){
		Vector3 position = go.transform.position;
		starBurst.transform.position = position;
		starBurst.Play ();
	}
}
