using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour {

	private Slider slider;
	private AudioSource audioSource;
	private LevelManager levelManager;
	private GameObject labelWin;

	public float levelSeconds;
	private float secondsLeft;
	private bool isEndOfLevel = false;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		audioSource = GetComponent<AudioSource> ();
		secondsLeft = levelSeconds;
		levelManager = GameObject.FindObjectOfType<LevelManager> ();

		// Find Win
		labelWin = GameObject.Find ("Win");
		if (!labelWin) {
			Debug.LogWarning ("Please create Win obejct");
		}
		labelWin.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
		slider.value = Time.timeSinceLevelLoad / levelSeconds;

		bool timeIsUp = (Time.timeSinceLevelLoad >= levelSeconds);
		if (timeIsUp && !isEndOfLevel) {
			audioSource.Play ();
			Invoke ("LoadNextLevel", audioSource.clip.length);
			isEndOfLevel = true;
			labelWin.SetActive(true);
		}

	}

	void LoadNextLevel(){
		levelManager.LoadNextLevel ();
	}
}
