using UnityEngine;
using System.Collections;

public class DefenderSpawner : MonoBehaviour {

	public Camera myCamera;

	private GameObject defenderParent;

	private StarDisplay starDisplay;

	// Use this for initialization
	void Start () {
		defenderParent = GameObject.Find ("Defenders");
		starDisplay = GameObject.FindObjectOfType<StarDisplay> ();

		if (!defenderParent) {
			defenderParent = new GameObject();
			defenderParent.name = "Defenders";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Vector2 SnapToGrid(Vector2 WorldPos){
		float newX = Mathf.RoundToInt (WorldPos.x);
		float newY = Mathf.RoundToInt (WorldPos.y);

		return new Vector2 (newX, newY);
	}

	void OnMouseDown() { // Finger Clicked

		GameObject defender = Button.selectedDefender;
		int defenderCost = defender.GetComponent<Defender> ().starCost;

		if (starDisplay.UseStars (defenderCost) == StarDisplay.Status.SUCCESS) {
			SpawnDefender ();
		} else {
			Debug.Log ("Not enough suns");
		}

	}

	void SpawnDefender(){
		Vector2 rawPos = CalculateWorldPointOfMouseClick();
		Vector2 roundedPos = SnapToGrid(rawPos);
		Quaternion zeroRot = Quaternion.identity;
		GameObject defender = Instantiate (Button.selectedDefender, roundedPos, zeroRot) as GameObject;
		defender.transform.parent = defenderParent.transform;
	}

	Vector2 CalculateWorldPointOfMouseClick(){
		float mousePositionX = Input.mousePosition.x;
		float mousePositionY = Input.mousePosition.y;
		float distanceFromCam = 10f;

		Vector3 weirdTriplet = new Vector3 (mousePositionX, mousePositionY, distanceFromCam);
		Vector2 worldPoint = myCamera.ScreenToWorldPoint (weirdTriplet);

		return worldPoint;
	}
}
