using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RenderShader : MonoBehaviour {

	public Material mat;

	void OnRenderImage(RenderTexture src, RenderTexture destination ){
		Graphics.Blit (src, destination,mat);
	}
}
