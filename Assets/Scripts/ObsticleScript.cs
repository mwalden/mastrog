using UnityEngine;
using System.Collections;

public class ObsticleScript : MonoBehaviour {

	Vector3 startingScale = new Vector3(1,1,1);
	Vector3 expandedScale = new Vector3(1.2f,1.2f,1.2f);
	Vector3 endingScale = new Vector3(0,0,0);
	float timeToMove = .35f;
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag != "Player")
			return;
		GameObject go = coll.gameObject;
		PlayerScript playerScript = go.GetComponent<PlayerScript> ();
		adjustColor ();
		playerScript.resetPlayerPosition ();
	}


	private void adjustColor(){
		StartCoroutine (adjustColorCoroutine());

	}

	IEnumerator adjustColorCoroutine(){
		float elapsedTime = 0;
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		Color oldColor = renderer.color;

		while (elapsedTime < .1f) {
			renderer.color = Color.Lerp(oldColor,Color.red,(elapsedTime / .1f));
			print (renderer.color);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		Color colorToReturnFrom = renderer.color;
		elapsedTime = 0;
		while (elapsedTime <= .25f) {
			renderer.color = Color.Lerp(colorToReturnFrom,oldColor,(elapsedTime / .25f));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		renderer.color = oldColor;
	}


	public void Disappear(){
		StartCoroutine (disappearCoroutine());
	}

	IEnumerator disappearCoroutine(){
		Transform parentTransform = transform.parent.transform;
		float elapsedTime = 0;

		while (parentTransform.localScale.x < 1.2f) {
			parentTransform.localScale = Vector3.Lerp (startingScale, expandedScale, (elapsedTime / .25f));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		elapsedTime = 0;

		while (elapsedTime < .1f) {
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		elapsedTime = 0;
		while (parentTransform.localScale.x > 0) {
			// Don't change position if start and end are the same
			parentTransform.localScale = Vector3.Lerp (expandedScale, endingScale, (elapsedTime / timeToMove));
			elapsedTime += Time.deltaTime;
			yield return null;
		}		
	}
}
