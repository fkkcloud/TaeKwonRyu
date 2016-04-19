using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {

	private StarDisplay starDisplay;

	// Use this for initialization
	void Start () {
		starDisplay = GameObject.FindObjectOfType<StarDisplay> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(){
		//Debug.Log ("Collistion with Defender " + name);

	}

	void AddStars(int amount){
		starDisplay.AddStars (amount);
	}
}
