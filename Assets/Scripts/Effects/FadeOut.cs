using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {
	public GameObject obj;
	private bool canFade;
	private float timeToFade = .2f;

	public void Start()
	{
		canFade = false;
	}

	public Color color;
	public void Update()
	{
		var material = GetComponent<Renderer>().material;
		 color = material.color;

		material.color = new Color(color.r, color.g, color.b, color.a - (timeToFade * Time.deltaTime));
	}
}
