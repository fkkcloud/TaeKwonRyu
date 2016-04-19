using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	// const is also static in class in c#!!
	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFF_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

	/// <summary>
	/// Master Volume
	/// </summary>
	public static void SetMasterVolume(float volume){
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Volume invalid input!");
		}
	}

	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

	/// <summary>
	/// Unlock Level
	/// </summary>
	/// <param name="level">Level.</param>
	public static void UnlockLevel(int level){
		if (level <= Application.loadedLevel - 1) {
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString(), 1);
		} else {
			Debug.LogError ("Trying to unlock that is not in build order");
		}
	}

	public static bool IsLevelUnlocked(int level){
		if (level <= Application.loadedLevel - 1) {
			int level_value = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString());
			return (level_value == 1);
		} else {
			Debug.LogError ("Trying to ask for unloaded level");
			return false;
		}
	}

	/// <summary>
	/// Difficulty - 3 levels of difficulty
	/// </summary>
	public static void SetDiff(float val){
		if (val >= 1 && val <= 3) {
			PlayerPrefs.SetFloat (DIFF_KEY, val);
		} else {
			Debug.LogError ("Error : Trying to set invalid diff level");
		}
	}

	public static float GetDiff(){
		return PlayerPrefs.GetFloat (DIFF_KEY);
	}
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
