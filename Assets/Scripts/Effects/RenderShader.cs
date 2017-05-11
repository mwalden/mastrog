using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class RenderShader : MonoBehaviour {

	public Material mat;
	public bool enableSinWaves;
	void Start(){
		Messenger.AddListener ("turnOnWaves", enableWave);
	}
	void enableWave(){
		enableSinWaves = true;
	}
	void OnRenderImage(RenderTexture src, RenderTexture destination ){		
		if (enableSinWaves)
			Graphics.Blit (src, destination,mat);
		else
			Graphics.Blit (src, destination);
	}
}
