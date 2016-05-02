using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	GameObject _player;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find ("Player");
	}
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;

	// Update is called once per frame
	void Update () 
	{
		if (_player)
		{
			Vector3 delta = new Vector3(_player.transform.position.x - transform.position.x, 0f, 0f); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
}
