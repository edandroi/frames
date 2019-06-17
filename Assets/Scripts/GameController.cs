using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
		_InitializeServices();
	}
	

	private void _InitializeServices()
	{
		Services.GameController = this;	
		
		// Game Manager
		Services.GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	
		// Audio Manager
		var audioManagerGameObject = new GameObject("AudioManager");
		Services.AudioManager = audioManagerGameObject.AddComponent<AudioManager>();
		/*
		// Event Manager
		var eventManagerObj = new GameObject("Event Manager");
		Services.EventManager = eventManagerObj.AddComponent<EventManager>();
		*/

		Services.PlayerSquare = FindObjectOfType<DrawSquare>();
		
		var goalManagerGameObject = new GameObject("GoalManager");
		Services.GoalManager = goalManagerGameObject.AddComponent<GoalManager>();

	}
}
