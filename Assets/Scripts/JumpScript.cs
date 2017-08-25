using UnityEngine;
using System.Collections;

public class JumpScript : MonoBehaviour {

	public float force;
//	public GameController controller;
//	private PlayerScript ps;
	private GameObject currentRestBar;
	private Rigidbody2D rb;
	public LayerMask layerMask;
	private bool jumpingDisabled;
	public TouchGesture.GestureSettings gestureSetting;
	private TouchGesture touch;
	private float MAX_VELOCITY_FOR_STRETCH = 5;
	void Start(){
		rb = GetComponent<Rigidbody2D> ();
		Messenger.AddListener ("disableJumping",disableJumping);
		#if UNITY_ANDROID
//			touch = new TouchGesture(this.gestureSetting);
//			StartCoroutine(touch.CheckVerticleSwipes(
//				onSwipeUp: () => { jump();},
//				onSwipeDown: () => { ignore();}
//			));
		#endif
	}
	void ignore(){
	}

	void jump(){
		if (rb.velocity.y <= 0.1) {
			rb.velocity =  (new Vector2 (0, force));

		}
	}

	void Update () {
		if (rb.velocity.y > .1) {
			float newVel = rb.velocity.y < 0.1 ? 0 : rb.velocity.y;
			float y =  Mathf.Min(2,newVel/ (MAX_VELOCITY_FOR_STRETCH - 1) + 1);
			transform.localScale = new Vector3 (transform.localScale.x, y, transform.localScale.z);
		} else if (transform.localScale.y > 1) {
			transform.localScale = new Vector3 (transform.localScale.x, 1, transform.localScale.z);
		}
			
		if (Input.GetKeyDown (KeyCode.Space) && !jumpingDisabled) {
			//prevent double jump
			if (rb.velocity.y <= 0.1) {
				Messenger.Broadcast ("jumped");
				rb.velocity =  (new Vector2 (0, force));

			}
		}
		#if UNITY_ANDROID
		if (Input.touches.Length > 0 && !jumpingDisabled) {
			Touch touch = Input.touches [0];
			Messenger.Broadcast ("jumped");
			if (touch.phase==TouchPhase.Ended && rb.velocity.y <= 0.1){
				rb.velocity = (new Vector2 (0, force));
			}
			return;
		}
		#endif
	}


	void FixedUpdate() {
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		Vector3 position = go.transform.position;

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up,100f,layerMask);
		if (hit.collider != null) {
			
			float distance = Mathf.Abs(hit.transform.position.y-position.y);
			float g = Physics.gravity.magnitude; // get the gravity value
			force = Mathf.Sqrt(2 * g * distance) + hit.collider.bounds.size.y+.25f;
		}
	}

	void disableJumping(){
		jumpingDisabled = true;
	}
}
