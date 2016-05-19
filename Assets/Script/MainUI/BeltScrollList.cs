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

			// get elements to update for this child
			Text levelTextUI = null;
			Text atkTextUI = null;
			Text costTextUI = null;
			Text itemNameTextUI = null;
			Image itemImageUI = null;

			Text[] textUIs = child.gameObject.GetComponentsInChildren<Text> ();
			for (int i = 0; i < textUIs.Length; i++) {
				if (textUIs [i].tag == "UI_Slot_Level") {
					levelTextUI = textUIs [i];
				} else if (textUIs [i].tag == "UI_Slot_ATK") {
					atkTextUI = textUIs [i];
				} else if (textUIs [i].tag == "UI_Slot_Cost") {
					costTextUI = textUIs [i];
				} else if (textUIs [i].tag == "UI_Slot_ItemName") {
					itemNameTextUI = textUIs [i];
				}
			}
			Image[] imageUIs = child.gameObject.GetComponentsInChildren<Image> ();
			for (int i = 0; i < imageUIs.Length; i++) {
				if (imageUIs [i].tag == "UI_Slot_ItemImage") {
					itemImageUI = imageUIs [i];
					break;
				}
			}


			if (textAsset && items.Length > id && _balanceDatas.Length > id) {

				items [id].level = _balanceDatas [id].level; // current level of this belt

				items [id].itemname = _balanceDatas [id].itemname;
				items [id].costs = _balanceDatas [id].costs;
				items [id].damages = _balanceDatas [id].damages;

				// update ui elements
				int currentLevel = items [id].level;

				levelTextUI.text = currentLevel.ToString() + "/10"; 

				atkTextUI.text = items[id].damages[currentLevel].ToString();

				costTextUI.text = items[id].costs[currentLevel].ToString();

				itemImageUI.color = items [id].color;

				itemNameTextUI.text = items [id].itemname;
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
