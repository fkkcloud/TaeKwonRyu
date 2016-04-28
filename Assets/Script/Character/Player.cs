using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Publics
	public MOVE side = MOVE.RIGHT;

	[Tooltip ("How fast the player will move")]
	[Range(0.1f, 1.0f)] public float speed = 0.35f;

	// Privates
	//private float _moveEndTime;
	private Vector3 _playerMoveDir = new Vector3(1.0f, 0.0f, 0.0f);
	private SpriteRenderer _spriteRenderer;

	private Animator _anim;

	public float GetMovingDirection(){
		return _playerMoveDir.x;
	}

	// Use this for initialization
	void Start () {
	
		_spriteRenderer = GetComponentInChildren<SpriteRenderer> ();

		_anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void TriggerMove() {
		
		float duration = 0.125f;
		Vector3 targetPos = transform.position + _playerMoveDir * speed;
		LeanTween.move(gameObject, targetPos, duration).setEase(LeanTweenType.easeInOutCubic);

		//_anim.SetTrigger("TriggerMove");

	}

	public void SetDirection(MOVE inSide) {
		if (inSide == MOVE.RIGHT) { // when user input is RIGHT
			_playerMoveDir = Vector3.right;
			_spriteRenderer.flipX = true;
			side = MOVE.RIGHT;
		} else { // when user input is LEFT
			_playerMoveDir = Vector3.left;
			_spriteRenderer.flipX = false;
			side = MOVE.LEFT;
		}
	}

	void OnTriggerEnter2D(Collider2D col){

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Attacker> ()) 
		{
			// set direction
			Attacker attacker = obj.GetComponent<Attacker> ();
			if (side == attacker.side) {
				_spriteRenderer.flipX = !_spriteRenderer.flipX;
				_playerMoveDir.x = _playerMoveDir.x * -1; // update the actual moving dir too
			}

			// trigger attack animation
			_anim.SetTrigger("TriggerAttack");
		}
	}
		
	/*
	void OnTriggerStay2D(Collider2D col){

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Attacker> ()) 
		{
			// set direction
			Attacker attacker = obj.GetComponent<Attacker> ();
			if (side == attacker.side) {
				_spriteRenderer.flipX = !_spriteRenderer.flipX;
				_playerMoveDir.x = _playerMoveDir.x * -1; // update the actual moving dir too
			}

			// trigger attack animation
			//_anim.SetTrigger("TriggerAttack");
		}

	}*/
}
