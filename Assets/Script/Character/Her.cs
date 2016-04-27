using UnityEngine;
using System.Collections;

public class Her : MonoBehaviour {

	[Tooltip ("How fast she will move")]
	[Range(0.1f, 1.0f)] public float walkSpeed = 0.5f;

	private GameObject _playerObject;

	// Use this for initialization
	void Start () {
		_playerObject = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		float deltaBetweenHerAndPlayer = _playerObject.transform.position.x - transform.position.x;
		if (deltaBetweenHerAndPlayer > 0.3f) {
			transform.Translate (Vector3.right * walkSpeed * Time.deltaTime);
		}
	}
}
