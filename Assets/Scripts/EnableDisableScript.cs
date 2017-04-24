using UnityEngine;
using System.Collections;

public class EnableDisableScript : MonoBehaviour {

	public Sprite disabledSprite;

	private Sprite currentSprite;

	public void disableObstacle(){		
		changeState (false);
	}
	public void enableObstacle(){
		changeState (true);
	}

	public void changeState(bool enabled){
		SpriteRenderer []renderers = GetComponentsInChildren<SpriteRenderer> ();
		Collider2D []colliders = GetComponentsInChildren<Collider2D> ();
		foreach (SpriteRenderer render in renderers) {
			if (render.tag == "obsticle") {
				if (currentSprite == null)
					currentSprite = render.sprite;
				render.sprite = enabled?currentSprite:disabledSprite;
			}
		}

		foreach (Collider2D collider in colliders) {
			if (collider.tag == "obsticle")
				collider.enabled = enabled;
		}
	}
}
