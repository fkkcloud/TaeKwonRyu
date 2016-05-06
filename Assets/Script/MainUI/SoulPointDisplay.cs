using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class SoulPointDisplay : MonoBehaviour {

	private Text _soulText;
	private int _souls = 0;

	public enum Status {SUCCESS, FAILURE};

	// Use this for initialization
	void Start () {
		_soulText = GetComponent<Text> ();
		UpdateDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddSouls(int amount) {
		_souls += amount;
		PlayerPrefsManager.SetSouls (_souls);
		UpdateDisplay ();
	}

	private void UpdateDisplay(){
		int soulCount = PlayerPrefsManager.GetSouls ();
		_soulText.text = soulCount.ToString ();
	}
}
