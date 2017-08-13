﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ParticlePathController : MonoBehaviour {
	public ParticleSystem ps;
	public ParticleSystem.Particle[] pArr;
	public float m_Drift = .01f;
	public bool moveParticles;
	public Vector3 destination = new Vector3(-1.5f,5f,0f);
	void Start () {
		Messenger.AddListener<Vector3> ("playScoreBurst", playBurst);
		pArr = new ParticleSystem.Particle[1000];
		ps = GetComponent<ParticleSystem> ();
		print ("Start : " + ps.GetParticles (pArr));
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!moveParticles)
			return;
		int count = ps.GetParticles (pArr);
		for (int i = 0; i < count; i++)
		{
			if (Vector3.Distance(pArr [i].position,destination) < 0.01f)
				pArr [i].lifetime = 0;
			Vector3 newPos = Vector3.MoveTowards (pArr [i].position, destination, .25f);
			pArr [i].position = newPos;

		}
		ps.SetParticles(pArr, count);
	}

	public void playBurst(Vector3 position){
		ps.transform.position = position;
		moveParticles = false;
		StartCoroutine (timer ());
		ps.Play ();

	}

	private IEnumerator timer(){
		print ("in timer");
		yield return new WaitForSeconds (1);
		moveParticles = true;
	}
}
