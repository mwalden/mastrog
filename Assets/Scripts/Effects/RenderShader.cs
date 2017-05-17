using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class RenderShader : MonoBehaviour {

	public Material mat;
	public bool enableSinWaves;
	void Start(){
		Messenger.AddListener ("turnOnWaves", enableWave);
		Messenger.AddListener ("turnOffWaves", disableWave);
	}
	void enableWave(){
		enableSinWaves = true;
	}

	void disableWave(){
		enableSinWaves = false;
	}
	void OnRenderImage(RenderTexture src, RenderTexture destination ){		
		if (enableSinWaves)
			Graphics.Blit (src, destination,mat);
		else
			Graphics.Blit (src, destination);
	}
}
