using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {

	private Animator _animator;


	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D collider){
		Attacker attacker = collider.GetComponent<Attacker> ();

		if (attacker) {
			_animator.SetTrigger ("TriggerUnderAttack");
		}
	}
}
