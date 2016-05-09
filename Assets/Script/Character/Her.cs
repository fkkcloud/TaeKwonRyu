using UnityEngine;
using System.Collections;

public class Her : MonoBehaviour {

	[Tooltip ("How fast she will move")]
	[Range(0.1f, 2.0f)] public float walkSpeed = 1.3f;

	private GameObject _playerObject;
	private GameObject _attackers;

	private Animator _anim;
	private WarningHUD _warningHUD;
	private MainGame _mainGame;

	private float _originalWalkSpeed;

	// Use this for initialization
	void Start () {
		_playerObject = GameObject.Find ("Player");
		_anim = GetComponent<Animator> ();
		_warningHUD = GameObject.Find ("WarningHUD").GetComponent<WarningHUD>();
		_mainGame = GameObject.FindGameObjectWithTag ("MainGame").GetComponent<MainGame> ();
		_attackers = GameObject.Find ("Attackers");

		_originalWalkSpeed = walkSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (_mainGame.IsGameEnd == true)
			return;
		
		float deltaBetweenHerAndPlayer = _playerObject.transform.position.x - transform.position.x;
		if (deltaBetweenHerAndPlayer > 0.5f) {
			
			if (_anim.GetBool ("BoolHerWalk") == false) {
				_anim.SetBool ("BoolHerWalk", true);			
			}

			transform.Translate (Vector3.right * walkSpeed * Time.deltaTime);

		} else {

			if (_anim.GetBool ("BoolHerWalk") == true) {
				_anim.SetBool ("BoolHerWalk", false);
			}

			float spawnsPerSecond = 0.5f;
			float threshold = spawnsPerSecond * Time.deltaTime;
			if (Random.value < threshold) {
				_anim.SetTrigger ("TriggerHerLook");
			}

		}

		// see if there is any attackers near by and if there is none, call stop warning to be safe.
		bool isAttackerNear = false;
		foreach (Transform child in _attackers.transform) {
			float squaredDist = Mathf.Pow (child.position.x - transform.position.x, 2) + Mathf.Pow (child.position.y - transform.position.y, 2);
			if (squaredDist < 0.1){
				isAttackerNear = true;
				break;
			}
		}
		if (!isAttackerNear) {
			_warningHUD.StopWarning ();
			walkSpeed = _originalWalkSpeed;
		}
	}
		
	/* 
	 * Using OnTriggerStay2D instead of OnTriggerEnter2D since
	 * there could be onScreenWarning already activated and
	 * when it is end we want to activate general attacker engage 
	 * warning right away.
	 * */
	void OnTriggerStay2D(Collider2D col){
		if (_mainGame.IsGameEnd == true)
			return;

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Attacker> ()) 
		{
			_warningHUD.StartWarning ();
			walkSpeed = 0f;
		}
	}

	/*
	 * TriggerExit to disable warning seems to not working very precisly..!
	void OnTriggerExit2D(Collider2D col){

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Attacker> ()) 
		{
			_warningHUD.StopWarning ();

			// reset the walk speed
			walkSpeed = _originalWalkSpeed;
		}
	}
	*/
}
