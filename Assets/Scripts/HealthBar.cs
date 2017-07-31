using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private Image healthBar;
	private Transform _transform;
	public bool bleeding;
	[Range(0f,.02f)]
	public float bleedFactor;
	private bool gameOver;
	private bool isLaneEnabled = true;


	// Use this for initialization
	void Start () {
		healthBar = GetComponent<Image> ();
		_transform = healthBar.transform;
		Messenger.AddListener<float,bool> ("addHealth", addHealth);
		Messenger.AddListener<float> ("removeHealth", removeHealth);
		Messenger.AddListener<float> ("adjustBleedFactor", adjustBleedFactor);
		Messenger.AddListener<bool> ("isLaneEnabled", laneEnabled);
		Messenger.AddListener("landed", bleedHealthEnabled);
		Messenger.AddListener("jumped", bleedHealthdisabled);
	}

	void Update(){
		if (_transform.localScale.y > 0 && bleeding)
			setScale (_transform.localScale.y - bleedFactor * Time.deltaTime);
		if (_transform.localScale.y <= 0 && !gameOver) {
			gameOver = true;
			Messenger.Broadcast ("ranOutOfHealth");
		}
	}

	void bleedHealthEnabled(){
		bleeding = true;
	}
	void bleedHealthdisabled(){
		bleeding = false;
	}

	private void laneEnabled(bool isLaneEnabled){
		this.isLaneEnabled = isLaneEnabled;
	}
	void addHealth(float amount,bool overrideEnabled){
		if (!overrideEnabled && !isLaneEnabled)
			return;
		float total = Mathf.Min (_transform.localScale.y + amount, 1f);
		setScale (total);
	}
	void removeHealth(float amount){
		float total = Mathf.Max (_transform.localScale.y - amount, 0);
		setScale (total);
	}
	void adjustBleedFactor(float amount){
		bleedFactor = amount;
	}

	void setScale(float v){
		Vector3 vector = new Vector3 (_transform.localScale.x,v , _transform.localScale.z);
		_transform.localScale = vector;
	}


}
