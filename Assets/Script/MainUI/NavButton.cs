using UnityEngine;
using System.Collections;

public class NavButton : MonoBehaviour {

	private MainNavigation _mainNavigation;

	// Use this for initialization
	void Start () {
		_mainNavigation = GameObject.Find ("MainNavigation").GetComponent<MainNavigation>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActiveScrollUIWithID(int id){
		for (int i = 0; i < _mainNavigation.scrollUICount; i++) {
			_mainNavigation.lists [i].SetActive (false);
		}

		_mainNavigation.lists [id].SetActive (true);
	}
}
