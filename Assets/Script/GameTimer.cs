using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour {

	private Slider _slider;
	private AudioSource _audioSource;
	private LevelManager _levelManager;
	private GameObject _labelWin;
	private GameObject _progressBarBG;
	private GameObject _progressBarGauge;

	public float levelSeconds;
	private float _secondsLeft;
	private bool _isEndOfLevel = false;

	// Use this for initialization
	void Start () {
		_slider = GetComponent<Slider> ();
		_audioSource = GetComponent<AudioSource> ();
		_secondsLeft = levelSeconds;
		_levelManager = GameObject.FindObjectOfType<LevelManager> ();
		_progressBarBG = GameObject.FindGameObjectWithTag ("GameTimerBarBG");
		_progressBarGauge = GameObject.FindGameObjectWithTag ("GameTimerBarGauge");
		_progressBarGauge.transform.localScale = new Vector3 (0.0f, _progressBarGauge.transform.localScale.y, _progressBarGauge.transform.localScale.z);

		// Find Win
		_labelWin = GameObject.Find ("Win");
		if (!_labelWin) {
			Debug.LogWarning ("Please create Win obejct");
		}
		_labelWin.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
		_slider.value = Time.timeSinceLevelLoad / levelSeconds;

		_progressBarGauge.transform.localScale = new Vector3 (Time.timeSinceLevelLoad / levelSeconds, _progressBarGauge.transform.localScale.y, _progressBarGauge.transform.localScale.z);

		bool timeIsUp = (Time.timeSinceLevelLoad >= levelSeconds);
		if (timeIsUp && !_isEndOfLevel) {
			_audioSource.Play ();
			Invoke ("LoadNextLevel", _audioSource.clip.length);
			_isEndOfLevel = true;
			_labelWin.SetActive(true);
		}

	}

	void LoadNextLevel(){
		_levelManager.LoadNextLevel ();
	}
}
