using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] attackerPrefabArray;

	private GameObject _coreWorld;

	private MainGame _mainGame;

	// Use this for initialization
	void Start () {
		_coreWorld = GameObject.Find ("Attackers");

		_mainGame = GameObject.FindGameObjectWithTag ("MainGame").GetComponent<MainGame> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_mainGame.IsGameEnd) {
			return;
		}

		foreach (GameObject attacker in attackerPrefabArray) {
			if (IsTimeToSpawn (attacker)) {
				Spawn (attacker);
			}
		}
	}

	void Spawn(GameObject attackerGameObj){
		GameObject myAttacker = Instantiate (attackerGameObj) as GameObject;
		myAttacker.transform.parent = _coreWorld.transform;
		myAttacker.transform.position = transform.position;
	}

	bool IsTimeToSpawn(GameObject attackerGameObj){
		Attacker attacker = attackerGameObj.GetComponent<Attacker> ();

		float meanSpawnDelay = attacker.seenEverySeconds;
		float spawnsPerSecond = 1 / meanSpawnDelay;

		if (Time.deltaTime > meanSpawnDelay) {
			Debug.LogError ("Spawn rate is capped by framerate");
		}

		float threshold = spawnsPerSecond * Time.deltaTime;

		return (Random.value < threshold);
	}
}
