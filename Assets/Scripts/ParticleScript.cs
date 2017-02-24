using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {
	bool particleMoving;
	Bounds bounds;
	Camera cam;

	public ParticleSystem particleSystem;
	public float particleSpeed;


	void Start () {
		cam = Camera.main;
		particleSystem.Stop();
	}
	
	void Update () {
		if (particleMoving) {
			bounds = CameraExtensions.OrthographicBounds (cam);
			if (particleSystem.transform.position.y < bounds.center.y + bounds.extents.y){
				particleSystem.transform.position = new Vector3 (particleSystem.transform.position.x, particleSystem.transform.position.y + particleSpeed, particleSystem.transform.position.z);
			}else{
				particleMoving = false;
				particleSystem.Stop();
			}
		}
	}

	public void playParticleSystem(){
		particleMoving = true;
		particleSystem.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y-2.5f, cam.transform.position.z);
		particleSystem.Play ();

	}
}
