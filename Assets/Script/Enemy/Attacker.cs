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

	[Tooltip ("How fast the attacker will move")]
	[Range(0.1f, 20.0f)] public float pushMult = 1.0f;

	[Tooltip ("Average Seconds between Appearances")]
	public float seenEverySeconds;

	// Privates
	private bool _IsMoving = true;
	private bool _IsCapturing = false;

	private float _walkSpeed;
	private float _moveEndTime;

	private Vector3 _moveDir = new Vector3(1.0f, 0.0f, 0.0f);

	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private Health _health;

	private GameObject _currentTarget;
	private GameObject _her;

	// Use this for initialization
	void Start () {
		Rigidbody2D myRigidBody2D = gameObject.AddComponent<Rigidbody2D> ();
		myRigidBody2D.isKinematic = true;

		_animator = GetComponent<Animator> ();

		_her = GameObject.Find ("Her");

		_spriteRenderer = GetComponentInChildren<SpriteRenderer> ();

		_health = GetComponent<Health> ();
	}
	
	// Update is called once per frame
	void Update () {

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
				_spriteRenderer.flipX = false;
			} else {
				side = MOVE.RIGHT;
				_moveDir = Vector3.right;
				_spriteRenderer.flipX = true;
			}
			
			transform.Translate (_moveDir * _walkSpeed * Time.deltaTime);
		}
			
		/*
		if (!_currentTarget && _animator) {
			_animator.SetBool ("IsAttacking", false);
		}*/
	}


	void OnTriggerExit2D(Collider2D col){

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Her> ()) 
		{
			_IsCapturing = false;
		}
	}


	void OnTriggerStay2D(Collider2D col){

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

	void OnTriggerEnter2D(Collider2D col){
		
		//GameObject obj = col.gameObject;

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

		TintAttacked ();
	}

	private void TintAttacked(){
		Color c = Color.red;
		c.a = 0.9f;
		LeanTween.color(_spriteRenderer.gameObject, c, 0.1f).setEase(LeanTweenType.easeInOutCubic).setLoopPingPong(1);
	}


	public void SetSpeed(float speed){
		_walkSpeed = speed;
	}

	// Called at the time when actual attack happens in Animator (animation state!)
	public void StrikeCurrentTarget(float damage){
		Debug.Log (name + " damaged for " + damage);
		if (_currentTarget) {
			Health health = _currentTarget.GetComponent<Health> ();
			if (health) {
				health.DealDamage (damage);
			}
		}
	}

	public void Attack(GameObject obj){
		_currentTarget = obj;
	}

}
