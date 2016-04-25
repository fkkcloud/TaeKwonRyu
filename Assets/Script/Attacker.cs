using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class Attacker : MonoBehaviour {

	// Publics
	[Tooltip ("How fast the attacker will move")]
	[Range(0.1f, 20.0f)] public float speed = 4.5f;

	[Tooltip ("Average Seconds between Appearances")]
	public float seenEverySeconds;

	// Privates
	private bool _IsMoving = true;
	private bool _IsCapturing = false;

	private float _walkSpeed;
	private float _moveEndTime;

	private Vector3 _moveDir;

	private Animator _animator;
	private SpriteRenderer _spriteRenderer;

	private GameObject _currentTarget;
	private GameObject _her;

	// Use this for initialization
	void Start () {
		Rigidbody2D myRigidBody2D = gameObject.AddComponent<Rigidbody2D> ();
		myRigidBody2D.isKinematic = true;

		_animator = gameObject.GetComponent<Animator> ();

		_her = GameObject.Find ("Her");

		_spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		// If Pushed, evaluate pushed amount
		if (Time.timeSinceLevelLoad <= _moveEndTime) {
			
			// Attacker gets pushed
			Vector3 pushedDist = _moveDir * speed;
			Vector3 targetPos  = transform.position + pushedDist;
			float step         = Time.deltaTime * 5.0f;
			transform.position = Vector3.MoveTowards (transform.position, targetPos, step);

			return;
		} else {

			// Attacker finished pushed away
			_IsMoving = true;
		}

		// Continue moving
		if (_IsMoving && !_IsCapturing) {
			
			// Move towards her
			bool IsOnRightSide = (_her.transform.position.x <= transform.position.x);

			if (IsOnRightSide) {
				_moveDir = Vector3.left;
				_spriteRenderer.flipX = false;
			} else {
				_moveDir = Vector3.right;
				_spriteRenderer.flipX = true;
			}
			
			transform.Translate (_moveDir * _walkSpeed * Time.deltaTime);
		}
			
		if (!_currentTarget && _animator) {
			_animator.SetBool ("IsAttacking", false);
		}
	}

	void OnTriggerStay2D(Collider2D col){

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Player> ()) 
		{
			TriggerMove (obj);
		}
		else if (obj.GetComponent<Her> ()) 
		{
			_IsCapturing = true;
		} 

	}

	void OnTriggerEnter2D(Collider2D col){
		
		GameObject obj = col.gameObject;

		// if its not defender, abort.
		if (obj.GetComponent<Player> ()) 
		{
			TriggerMove (obj);
		} 
		else if (obj.GetComponent<Her> ()) 
		{
			_IsCapturing = true;
		} 
		else {
			return;
		}

		Debug.Log (name + " Collided with " + col);
	}

	public void TriggerMove(GameObject pusher) {
		bool IsOnRightSide = (pusher.transform.position.x <= transform.position.x);

		if (IsOnRightSide)
			_moveDir = Vector3.right;
		else
			_moveDir = Vector3.left;
		
		_moveEndTime = Time.timeSinceLevelLoad + 0.1f;

		_IsMoving = false;
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
