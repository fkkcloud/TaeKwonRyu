using UnityEngine;
using System.Collections;

public class Her : MonoBehaviour {

	[Tooltip ("How fast she will move")]
	[Range(0.1f, 2.0f)] public float walkSpeed = 1.3f;

	private GameObject _playerObject;

	private Animator _anim;
	private WarningHUD _warningObject;
	private MainGame _mainGame;

	private float _originalWalkSpeed;

	// Use this for initialization
	void Start () {
		_playerObject = GameObject.Find ("Player");
		_anim = GetComponent<Animator> ();
		_warningObject = GameObject.Find ("WarningHUD").GetComponent<WarningHUD>();
		_mainGame = GameObject.FindGameObjectWithTag ("MainGame").GetComponent<MainGame> ();

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
	}
		
	void OnTriggerEnter2D(Collider2D col){
		if (_mainGame.IsGameEnd == true)
			return;

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Attacker> ()) 
		{
			_warningObject.StartWarning ();
			walkSpeed = 0f;
		}
	}

	void OnTriggerExit2D(Collider2D col){

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Attacker> ()) 
		{
			_warningObject.StopWarning ();

			// reset the walk speed
			walkSpeed = _originalWalkSpeed;
		}
	}
}
