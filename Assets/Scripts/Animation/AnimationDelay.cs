using UnityEngine;
using System.Collections;

public class AnimationDelay : MonoBehaviour {

	public string animationName;
	public float delay;
	private Animation animator;
	void Start () {
		animator = GetComponent<Animation>();


	}

}
