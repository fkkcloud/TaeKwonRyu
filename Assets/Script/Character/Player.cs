using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Publics
	public GameObject FX_Attack;

	public MOVE side = MOVE.RIGHT;

	[Tooltip ("How fast the player will move")]
	[Range(0.1f, 1.0f)] 
	public float speed = 0.35f;

	[Tooltip ("Bound amount that player can not go through")]
	[Range(0.5f, 5.0f)]
	public float moveOffset = 1.0f;

	public bool IsLimitBreakReady = false;

	// Privates
	private GameObject _BG_Object;

	private SpriteRenderer _spriteRenderer;
	private Animator _anim;
	private LimitBreak _limitBreak;
	private Sprite _BG_Sprite;
	private MainGame _mainGame;

	private Vector3 _playerMoveDir = new Vector3(1.0f, 0.0f, 0.0f);
	private float _minXMove;
	private float _maxXMove;

	// Use this for initialization
	void Start () {
	
		_spriteRenderer = GetComponentInChildren<SpriteRenderer> ();

		_anim = GetComponent<Animator> ();

		_limitBreak = GameObject.FindGameObjectWithTag ("LimitBreak").GetComponent<LimitBreak>();

		_mainGame = GameObject.Find ("MainGame").GetComponent<MainGame> ();

		_BG_Object = GameObject.FindGameObjectWithTag ("GameMapController");
		_BG_Sprite = _BG_Object.GetComponent<SpriteRenderer> ().sprite;

		_minXMove = _BG_Sprite.bounds.min.x * _BG_Object.transform.localScale.x + _BG_Object.transform.position.x + moveOffset;
		_maxXMove = _BG_Sprite.bounds.max.x * _BG_Object.transform.localScale.x + _BG_Object.transform.position.x - moveOffset;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TriggerMove() {
		if (_mainGame.IsGameEnd == true)
			return;

		float duration = 0.125f;
		Vector3 targetPos = transform.position + _playerMoveDir * speed;
		if (CanPlayerMove(ref targetPos))
			LeanTween.move(gameObject, targetPos, duration).setEase(LeanTweenType.easeInOutCubic);

		//_anim.SetTrigger("TriggerMove");
	}

	private bool CanPlayerMove(ref Vector3 targetPos){
		return targetPos.x > _minXMove && targetPos.x < _maxXMove;
	}

	public void SetDirection(MOVE inSide) {
		if (_mainGame.IsGameEnd == true)
			return;
		
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
		if (_mainGame.IsGameEnd == true)
			return;

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

			// occur hit for limit break component
			_limitBreak.AddHit();

			// allocate FX
			AllocateFXAttack(attacker.gameObject);
		}
	}

	void AllocateFXAttack(GameObject Attackee){
		GameObject fxAttack = Instantiate (FX_Attack) as GameObject;
		Vector3 FXPos = new Vector3(Attackee.transform.position.x + _playerMoveDir.x * 0.25f, Attackee.transform.position.y, Attackee.transform.position.z);
		fxAttack.transform.position = FXPos;
		Destroy (fxAttack, 0.3f);
	}
}
