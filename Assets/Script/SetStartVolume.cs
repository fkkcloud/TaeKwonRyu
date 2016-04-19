using UnityEngine;
using System.Collections;

public class SetStartVolume : MonoBehaviour {

	private MusicManager _musicManager;

	// Use this for initialization
	void Start () {
		_musicManager = GameObject.FindObjectOfType<MusicManager> ();
		if (_musicManager) {
			float masterVolume = PlayerPrefsManager.GetMasterVolume ();
			_musicManager.SetVolume (masterVolume);
		} else {
			Debug.LogError ("There is no music manager available at this moment.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
