using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
	public Slider diffSlider;
	public LevelManager levelManager;
	private MusicManager _musicManager;

	// Use this for initialization
	void Start () {
		_musicManager = GameObject.FindObjectOfType<MusicManager> ();
		if (_musicManager) {
			
		}

		volumeSlider.value = PlayerPrefsManager.GetMasterVolume ();
		diffSlider.value = PlayerPrefsManager.GetDiff ();
		_musicManager.SetVolume (volumeSlider.value);
	}
	
	// Update is called once per frame
	void Update () {
		_musicManager.SetVolume (volumeSlider.value);
	}

	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
		PlayerPrefsManager.SetDiff (diffSlider.value);

		Debug.Log ("Saved Diff :" + PlayerPrefsManager.GetDiff ());
		Debug.Log ("Saved Volume :" + PlayerPrefsManager.GetMasterVolume ());

		levelManager.LoadLevel ("01a_MainMenu");
	}

	public void SetDefault(){
		volumeSlider.value = 0.25f;
		diffSlider.value = 2f;
	}
}
