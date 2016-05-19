using UnityEngine;
using System.Collections;

public class RoomNavigator : MonoBehaviour {

	private LevelManager _levelManager;

	// Use this for initialization
	void Start () {
		_levelManager = GameObject.FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		_levelManager.LoadLevel ("02_Level_01");
	}
}
