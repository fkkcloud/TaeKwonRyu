using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BeltScrollList : MonoBehaviour {

	public Belt[] items;
	public TextAsset textAsset;

	private Belt[] _balanceDatas;

	// Use this for initialization
	void Start () {

		LoadBalanceData ();

		// loop through each item in the scroll list
		int id = 0;
		foreach (Transform child in transform.GetChild(0)) {

			// get all the text comps in each item in the scroll list
			Text[] textUIs = child.gameObject.GetComponentsInChildren<Text> ();
			for (int i = 0; i < textUIs.Length; i++) {
				
				if (textUIs [i].tag == "UI_Slot_Level") {
					textUIs [i].text = _balanceDatas [i].level.ToString() + "/10"; 
				} else if (textUIs [i].tag == "UI_Slot_ATK") {
					textUIs [i].text = _balanceDatas [i].damages [_balanceDatas [i].level].ToString();
				} else if (textUIs [i].tag == "UI_Slot_Cost") {
					textUIs [i].text = _balanceDatas [i].costs [_balanceDatas [i].level].ToString();
				} else if (textUIs [i].tag == "UI_Slot_ItemName") {
					textUIs [i].text = _balanceDatas [i].itemname;
				}

			}


			Text itemNameUI = child.gameObject.GetComponentInChildren<Text> ();

			if (textAsset && items.Length > id && _balanceDatas.Length > id) {

				items [id].level = _balanceDatas [id].level; // current level of this belt

				items [id].itemname = _balanceDatas [id].itemname;
				items [id].costs = _balanceDatas [id].costs;
				items [id].damages = _balanceDatas [id].damages;

				itemNameUI.text = items [id].itemname;
			}

			id++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadBalanceData(){
		if (textAsset){
			string[] jsons = textAsset.text.Split ('$');

			int beltCount = jsons.Length;
			_balanceDatas = new Belt[beltCount];

			for (int i = 0; i < jsons.Length; i++) {
				string json = jsons [i];
				Belt belt = JsonUtility.FromJson<Belt> (json);	
				_balanceDatas [i] = belt;
				_balanceDatas [i].level = PlayerPrefsManager.GetBeltLevel(i);
			}
		}

	}
}
