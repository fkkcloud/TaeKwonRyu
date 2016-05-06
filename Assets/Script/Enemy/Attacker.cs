using UnityEngine;
using System.Collections;

public enum MOVE
{
	LEFT,
	RIGHT,
}

[RequireComponent (typeof(Rigidbody2D))]
public class Attacker : MonoBehaviour {

	// Publics
	public MOVE side;

	[Tooltip ("How much the attacker will be pushed")]
	[Range(0.1f, 20.0f)] 
	public float pushMult = 1.0f;

	[Tooltip ("How fast the attacker will move")]
	[Range(0.1f, 5.0f)] 
	public float _walkSpeed = 1.2f;

	[Tooltip ("Point of Soul")]
	public int SoulPoint = 1;

	[Tooltip ("Average Seconds between Appearances")]
	public float seenEverySeconds;

	// Privates
	private GameObject _currentTarget;
	private GameObject _her;

	private MainGame _mainGame;
	private Animator _anim;
	private SpriteRenderer _spriteRenderer;
	private SpriteRenderer _healthBar;
	private Health _health;

	private Vector3 _moveDir = new Vector3(1.0f, 0.0f, 0.0f);
	private float _moveEndTime;
	private float _maxHitPoint;
	private bool _IsMoving = true;
	private bool _IsCapturing = false;

	// Use this for initialization
	void Start () {
		Rigidbody2D myRigidBody2D = gameObject.AddComponent<Rigidbody2D> ();
		myRigidBody2D.isKinematic = true;

		_anim = GetComponent<Animator> ();

		_her = GameObject.Find ("Her");

		foreach (Transform child in transform) 
		{
			if (child.gameObject.CompareTag("Body")){
				_spriteRenderer = child.gameObject.GetComponent<SpriteRenderer> ();
			}
			else if (child.gameObject.CompareTag("HealthBar")){
				_healthBar = child.gameObject.GetComponent<SpriteRenderer> ();
			}
		}

		_health = GetComponent<Health> ();
		_maxHitPoint = _health.HitPoint;

		_mainGame = GameObject.FindGameObjectWithTag ("MainGame").GetComponent<MainGame> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_mainGame.IsGameEnd == true)
			return;

		// PUSHED
		if (!_IsMoving && Time.timeSinceLevelLoad <= _moveEndTime) 
		{
			// Attacker gets pushed
			Vector3 pushedDist = _moveDir * pushMult;
			Vector3 targetPos  = transform.position + pushedDist;
			float step         = Time.deltaTime * 7.0f;
			transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
			return;
		} 
		else 
		{
			// Attacker finished pushed away
			_IsMoving = true;
		}

		// MOVING
		if (_IsMoving && !_IsCapturing) {
			
			// Move towards her
			bool IsOnRightSide = (_her.transform.position.x <= transform.position.x);

			if (IsOnRightSide) {
				side = MOVE.LEFT;
				_moveDir = Vector3.left;
				_spriteRenderer.flipX = true;
			} else {
				side = MOVE.RIGHT;
				_moveDir = Vector3.right;
				_spriteRenderer.flipX = false;
			}
			
			transform.Translate (_moveDir * _walkSpeed * Time.deltaTime);
		}
	}


	void OnTriggerExit2D(Collider2D col){

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Her> ()) 
		{
			_IsCapturing = false;
		}
	}


	void OnTriggerStay2D(Collider2D col){
		if (_mainGame.IsGameEnd == true)
			return;

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Player> ()) 
		{
			TriggerPush (obj);
		}
		else if (obj.GetComponent<Her> ()) 
		{
			_IsCapturing = true;
		}
	}

	void TriggerPush(GameObject pusher) {
		bool IsOnRightSide = (pusher.transform.position.x <= transform.position.x);

		if (IsOnRightSide) {
			_moveDir = Vector3.right;
		} else {
			_moveDir = Vector3.left;
		}
		
		_moveEndTime = Time.timeSinceLevelLoad + 0.1f;

		_IsMoving = false;

		_health.DealDamage (20.0f);

		Vector3 healthBarScale = new Vector3 (_health.HitPoint / _maxHitPoint, _healthBar.gameObject.transform.localScale.y, _healthBar.gameObject.transform.localScale.z);
		_healthBar.gameObject.transform.localScale = healthBarScale;

		TintAttacked ();

		_anim.SetTrigger ("TriggerHit");
	}

	private void TintAttacked(){
		Color c = Color.red;
		c.a = 0.9f;
		LeanTween.color(_spriteRenderer.gameObject, c, 0.25f).setEase(LeanTweenType.easeInOutCubic).setLoopPingPong(1);
	}
}
