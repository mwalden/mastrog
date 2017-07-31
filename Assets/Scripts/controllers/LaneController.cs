using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaneController : MonoBehaviour {
	public Dictionary<int,bool> enabledLanes;
	public int currentLaneId;
	// Use this for initialization
	void Start () {
		enabledLanes = new Dictionary<int, bool> ();
		Messenger.AddListener<int> ("disableLane", laneDisabled);
		Messenger.AddListener<int> ("enableLane", laneEnabled);
		Messenger.AddListener<int> ("changedLanes", changedLanes);
		Messenger.AddListener<int> ("initializeLanes", initializeLanes);
	}

	private void initializeLanes(int laneCount){
		for (int x = 0; x  < laneCount;x++){
			enabledLanes.Add (x, true);
		}
	}

	public void laneDisabled(int laneId){
		enabledLanes [laneId] = false;
		Messenger.Broadcast<bool> ("isLaneEnabled", enabledLanes[laneId]);
	}

	public void laneEnabled(int laneId){
		enabledLanes [laneId] = true;
		Messenger.Broadcast<bool> ("isLaneEnabled", enabledLanes[laneId]);
	}

	public void changedLanes(int laneId){
		if (!enabledLanes.ContainsKey(laneId))
			return;
		currentLaneId = laneId;
		Messenger.Broadcast<bool> ("isLaneEnabled", enabledLanes[laneId]);
	}
}
