using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour {

	private AudioSource _audioSource;
	private LevelManager _levelManager;
	private GameObject _labelWin;
	private Text _text;
	private MainGame _mainGame;
	private WarningMsg _warningMsg;

	public float levelSeconds;
	private bool _isEndOfLevel = false;

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();
		_levelManager = GameObject.FindObjectOfType<LevelManager> ();
		_text = GetComponent<Text> ();
		_mainGame = GameObject.FindGameObjectWithTag ("MainGame").GetComponent<MainGame> ();
		_warningMsg = GameObject.Find ("WarningMSG").GetComponent<WarningMsg> ();

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

		if (_mainGame.IsGameEnd)
			return;

		int secondLeft = Mathf.RoundToInt (levelSeconds - Time.timeSinceLevelLoad);
		_text.text = secondLeft.ToString();

		bool timeIsUp = (0 >= secondLeft);
		if (timeIsUp && !_isEndOfLevel) {
			_isEndOfLevel = true;
			_mainGame.IsGameEnd = true;
			_mainGame.TriggerLoseCondition ();
			_warningMsg.ActivateMsgWithText ("Time is up!");

			//_audioSource.Play ();
			Invoke ("ReloadLevel", 3.0f/*_audioSource.clip.length*/);
			//_labelWin.SetActive(true);
		}

	}

	void ReloadLevel(){
		_levelManager.LoadLevel("01c_Room");
	}
}
