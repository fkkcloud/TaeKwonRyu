using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Publics
	[Tooltip ("How fast the player will move")]
	[Range(0.1f, 20.0f)] public float speed = 4.5f;

	// Privates
	private float _moveEndTime;
	private Vector3 _playerMoveDir = new Vector3(1.0f, 0.0f, 0.0f);
	private SpriteRenderer _spriteRenderer;

	public float GetMovingDirection(){
		return _playerMoveDir.x;
	}

	// Use this for initialization
	void Start () {
	
		_spriteRenderer = GetComponentInChildren<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.timeSinceLevelLoad <= _moveEndTime) {
			Vector3 pushedDist = _playerMoveDir * speed;
			Vector3 targetPos = transform.position + pushedDist;
			float step = Time.deltaTime * 20.0f;
			transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
		}

	}

	public void TriggerMove() {
		float duration = 0.1f;
		_moveEndTime = Time.timeSinceLevelLoad + duration;
	}

	public void SetDirection(float dir) {
		if (dir > 0) {
			_playerMoveDir = Vector3.right;
			_spriteRenderer.flipX = true;
		} else {
			_playerMoveDir = Vector3.left;
			_spriteRenderer.flipX = false;
		}
	}

	void OnTriggerStay2D(Collider2D col){

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Attacker> ()) 
		{
			// set direction
			Attacker attacker = obj.GetComponent<Attacker> ();
			if (_playerMoveDir.x == attacker.GetMovingDirection ()) {
				_spriteRenderer.flipX = !_spriteRenderer.flipX;
				_playerMoveDir.x = _playerMoveDir.x * -1; // update the actual moving dir too
			}
			// trigger attack animation
		}

	}
}
