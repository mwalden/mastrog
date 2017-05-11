using UnityEngine;
using System.Collections;

public class PowerBoxController : MonoBehaviour {


	public GameObject powerBox;
	public float boxSpeed;

	private bool objectAlreadyOut;
	private NewLevel level;
	private GameObject box;
	private Bounds bounds;
	private GameObject player;
	private CurrentLevelScript currentLevelScript;



	void Start(){
		bounds = CameraExtensions.OrthographicBounds (Camera.main);
		Messenger.AddListener<NewLevel> ("setLevel", setLevel);
	}
	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		if (!objectAlreadyOut && level != null) {
			if (box == null) {
				createBox ();
				objectAlreadyOut = true;
			}
		} else {
			
		}
	}

	void setLevel(NewLevel level){
		this.level = level;
	}

	void createBox(){
		box = Instantiate (powerBox, new Vector3(-100f,-100f,0f), Quaternion.identity) as GameObject;
		box.transform.position = new Vector3 ((level.numberOfLanes -1 ) * bounds.size.x + (bounds.size.x / 2) + box.GetComponent<BoxCollider2D>().size.x,
			player.transform.position.y+1, 
			0);
		//finish this later
		setPowerOnBox ();
	}

	void setPowerOnBox (){
	}
}
