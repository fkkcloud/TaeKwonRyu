using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Attacker))]
public class Fox : MonoBehaviour {

	private Animator anim;
	private Attacker attacker;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		attacker = GetComponent<Attacker> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col){

		GameObject obj = col.gameObject;

		// if its not defender, abort.
		if (!obj.GetComponent<Defender> ()) {
			return;
		}

		if (obj.GetComponent<Stone> () && anim) {
			anim.SetTrigger ("TriggerJump");
		} else {
			anim.SetBool ("IsAttacking", true);
			attacker.Attack (obj);
		}

		Debug.Log (name + " Collided with " + col);
	}
}
