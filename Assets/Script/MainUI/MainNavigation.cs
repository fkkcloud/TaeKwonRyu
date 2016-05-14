using UnityEngine;
using System.Collections;

public class MainNavigation : MonoBehaviour {

	public GameObject[] lists;

	public int scrollUICount = 3;

	public int initialNavigationID;

	// Use this for initialization
	void Start () {
		lists = new GameObject[scrollUICount];

		lists [0] = GameObject.FindGameObjectWithTag ("Scroll01_Belt");
		lists [1] = GameObject.FindGameObjectWithTag ("Scroll02_Shirt");
		lists [2] = GameObject.FindGameObjectWithTag ("Scroll03_Pant");

		if (lists.Length != scrollUICount)
			Debug.LogError ("scroll UI has not been initialized successfully");

		InitNavigation ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitNavigation(){
		for (int i = 0; i < scrollUICount; i++) {
			lists [i].SetActive (false);
		}

		lists [initialNavigationID].SetActive (true);
	}
}
