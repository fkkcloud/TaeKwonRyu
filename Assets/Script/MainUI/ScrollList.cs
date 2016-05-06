using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollList : MonoBehaviour {

	public string[] items;

	// Use this for initialization
	void Start () {
		int id = 0;
		foreach (Transform child in transform.GetChild(0)) {
			Text itemName = child.gameObject.GetComponentInChildren<Text> ();
			itemName.text = items [id];
			id++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
