using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace LevelEditor
{
	[CustomEditor(typeof(Level))]
	[CanEditMultipleObjects]
	public class LevelEditor : Editor
	{
		SerializedProperty name;
		SerializedProperty image;
		SerializedProperty sampleTrack;
		SerializedProperty timeInSeconds;
		SerializedProperty obstacleType;
		SerializedProperty chanceOfPowerbox;


		int selectedObstacleIndex = 0;
		int selectedLevelIndex = 0;
		Level level;
		List<string> levelNames = new List<string>();

		void OnEnable(){
			
			level = (Level)target;
			level.initalize ();
			name = serializedObject.FindProperty ("levelName");
			image = serializedObject.FindProperty ("image");
			sampleTrack = serializedObject.FindProperty ("sampleTrack");
			timeInSeconds = serializedObject.FindProperty ("timeInSeconds");
			obstacleType = serializedObject.FindProperty ("obstacleType");
			chanceOfPowerbox = serializedObject.FindProperty ("chanceOfPowerbox");

			List<LevelDetail> levelDetails = LevelManager.Instance.getLevels ();

			foreach (LevelDetail levelDetail in levelDetails) {
				levelNames.Add (levelDetail.folderName);
			}
		}
		public override void OnInspectorGUI()
		{	
			serializedObject.Update ();
			int newSelectedLevelIndex = EditorGUILayout.Popup ("Select level to edit", selectedLevelIndex, levelNames.ToArray ());
			GUILayoutOption[] opts = new GUILayoutOption[]{ GUILayout.MaxWidth(200)};
			level.image = EditorGUILayout.ObjectField ("Select sprite", level.image, typeof(Sprite), false,opts) as Sprite;

			level.setPreviewClip (EditorGUILayout.ObjectField ("Select Preview Audio", level.previewAudio, typeof(AudioClip), false) as AudioClip);

			EditorGUILayout.LabelField ("Level Information");
			EditorGUILayout.BeginVertical ("Box");
			level.levelName = EditorGUILayout.TextField ("Name", level.levelName);
//			level.image = EditorGUILayout.TextField ("Image file", level.image);
			level.lengthInSeconds = EditorGUILayout.IntField ("Length of track", level.lengthInSeconds);
			level.numberOfLanes = EditorGUILayout.IntField ("Number of lanes", level.numberOfLanes);
			EditorGUILayout.EndVertical();

			EditorGUILayout.LabelField ("Powerbox info");
			EditorGUILayout.BeginVertical ("Box");
				level.chanceOfPowerbox = EditorGUILayout.Slider ("Chance of deploying", level.chanceOfPowerbox, 0.0f, 1.0f);
				level.chanceOfGoodPowerbox = EditorGUILayout.Slider ("Chance of good/bad", level.chanceOfGoodPowerbox, 0.0f, 1.0f);
			EditorGUILayout.EndVertical ();


			EditorGUILayout.LabelField ("Powerboxes to add");
			EditorGUILayout.BeginHorizontal ("Box",opts);
			EditorGUILayout.BeginVertical ("Box",opts);
			if (GUILayout.Button ("Refill Health")) {
				level.addPowerbox ("refillHealth");
			}
			if (GUILayout.Button ("Drain Health")) {
				level.addPowerbox ("drainHealth");
			}
			if (GUILayout.Button ("Clear Lane")) {
				level.addPowerbox ("clearLane");
			}
			if (GUILayout.Button ("Wavey and slow")) {
				level.addPowerbox ("wavey");
			}
			EditorGUILayout.EndVertical ();
			if (GUILayout.Button ("Clear powerboxes")) {
				if (EditorUtility.DisplayDialog ("Clear powerboxes ", "You are sure you want to clear the poweboxes?", "yes", "no")) {
					level.clearPowerBoxes ();
				}
			}
			EditorGUILayout.EndHorizontal ();



			EditorGUILayout.LabelField ("Obstacles to add");
			EditorGUILayout.BeginVertical ("Box");

			EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Circle")) {
					level.addObstacleWithType ("circle");
				}
				if (GUILayout.Button ("Square")) {
					level.addObstacleWithType ("square");
				}
				if (GUILayout.Button ("Pyramid")) {
					level.addObstacleWithType ("pyramid");
				}
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Petal")) {
					level.addObstacleWithType ("petal");
				}
				if (GUILayout.Button ("Scaling Square")) {
					level.addObstacleWithType ("scalingsquare");
				}
				if (GUILayout.Button ("Circle w/no platform")) {
					level.addObstacleWithType ("circleNoPlatform");
				}
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.EndVertical ();
			EditorGUILayout.LabelField ("Actions");
			EditorGUILayout.BeginVertical ("Box");
			if (newSelectedLevelIndex != selectedLevelIndex)
			if (EditorUtility.DisplayDialog ("Load level ","Load level " + levelNames [newSelectedLevelIndex] + "?", "yes", "no")) {
				selectedLevelIndex = newSelectedLevelIndex;
				level.loadLevel (newSelectedLevelIndex);
			}

			if (GUILayout.Button("Reset obstacles")){
				if (EditorUtility.DisplayDialog ("WARNING!!!!", "ARE YOU SURE YOU WANT TO DO THIS!!???!?!", "yes","no"))
					level.resetObstacles();
			}
			if (GUILayout.Button("Reset level")){
				if (EditorUtility.DisplayDialog ("WARNING!!!!", "ARE YOU SURE YOU WANT TO DO THIS!!???!?!", "yes","no"))
					level.resetLevel();
			}
			if (GUILayout.Button ("Save to JSON")) {
				level.serialize ();
			}
			EditorGUILayout.EndVertical ();
			serializedObject.ApplyModifiedProperties ();
			EditorGUILayout.Space ();
			base.OnInspectorGUI ();

//			if (GUILayout.Button ("Add Powerbox")) {
//				powerboxes.Add (new Powerbox ());

//			}
		}
	}
}

