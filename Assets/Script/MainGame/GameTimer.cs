using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour {

	private AudioSource _audioSource;
	private LevelManager _levelManager;
	private GameObject _labelWin;
	private Text _text;

	public float levelSeconds;
	private bool _isEndOfLevel = false;

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();
		_levelManager = GameObject.FindObjectOfType<LevelManager> ();
		_text = GetComponent<Text> ();

		// will have to show lose screen 
		/*
		_labelWin = GameObject.Find ("Win");
		if (!_labelWin) {
			Debug.LogWarning ("Please create Win obejct");
		}
		_labelWin.SetActive (false);
		*/
	}
	
	// Update is called once per frame
	void Update () {

		int secondLeft = Mathf.RoundToInt (levelSeconds - Time.timeSinceLevelLoad);

		_text.text = secondLeft.ToString();

		bool timeIsUp = (0 >= secondLeft);

		if (timeIsUp && !_isEndOfLevel) {
			//_audioSource.Play ();
			//Invoke ("LoadNextLevel", _audioSource.clip.length);
			//_isEndOfLevel = true;
			//_labelWin.SetActive(true);
		}

	}

	void LoadNextLevel(){
		_levelManager.LoadNextLevel ();
	}
}
