using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {

	public GameObject FXHerDisappear;

	public bool IsGameEnd = false;

	private GameObject _her;

	// Use this for initialization
	void Start () {
		_her = GameObject.FindGameObjectWithTag ("Her");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	public void TriggerLoseCondition(){
		AllocateFXHerDisappear ();
		Destroy (_her, 0.2f);
	}

	public void TriggerWinCondition(){

	}

	void AllocateFXHerDisappear(){
		GameObject fx = Instantiate (FXHerDisappear) as GameObject;
		Vector3 FXPos = new Vector3(_her.transform.position.x, _her.transform.position.y, _her.transform.position.z);
		fx.transform.position = FXPos;
		Destroy (fx, 0.5f);
	}
}
