using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public LevelManager levelManager;

	private MusicManager _musicManager;

	// Use this for initialization
	void Start () {
		_musicManager = GameObject.FindObjectOfType<MusicManager> ();
		SetupSound ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SaveAndExit(){
		levelManager.LoadLevel ("01a_MainMenu");
	}

	private void SetupSound(){
		if (_musicManager) {

		}
		int isMasterVolumeOn = PlayerPrefsManager.GetMasterVolume ();
		if (isMasterVolumeOn == 1) {
			// show on button
			_musicManager.SetVolume(0.5f);
		} else {
			// show off button
			_musicManager.SetVolume(0.0f);
		}
		int isSFXVolumeOn = PlayerPrefsManager.GetSFXVolume ();
		if (isSFXVolumeOn == 1) {
			// show on button
			_musicManager.isSFXON = true;
		} else {
			// show off button
			_musicManager.isSFXON = false;
		}
	}

	public void TogglePowerSave(){
		int value;

		if (PlayerPrefsManager.GetFPS () == 30) {
			value = 60;
			Application.targetFrameRate = 60;
		} else {
			value = 30;
			Application.targetFrameRate = 30;
		}

		PlayerPrefsManager.SetFPS (value);
	}

	// callback for master volume btn
	public void ToggleMasterVolume(){
		int value;

		// reverse the result
		if (PlayerPrefsManager.GetMasterVolume () == 1) {
			value = 0;
			_musicManager.SetVolume(0);
		} else {
			value = 1;
			_musicManager.SetVolume(1);
		}

		PlayerPrefsManager.SetMasterVolume (value);
	}

	// callback for sfx volume btn
	public void ToggleSFXVolume(){
		int value;

		// reverse the result
		if (PlayerPrefsManager.GetSFXVolume () == 1) {
			value = 0;
			_musicManager.isSFXON = false;
		} else {
			value = 1;
			_musicManager.isSFXON = true;
		}

		PlayerPrefsManager.SetSFXVolume (value);
	}
}
