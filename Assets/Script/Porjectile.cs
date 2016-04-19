using UnityEngine;
using System.Collections;

public class Porjectile : MonoBehaviour {

	public float speed = 1f;
	public float damage = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * speed * Time.deltaTime);
	}

	/*
	void OnBecameInvisible() {
		Destroy (gameObject);
	}
	*/

	void OnTriggerEnter2D(Collider2D col){
		Attacker attacker = col.gameObject.GetComponent<Attacker> ();
		Health health = col.gameObject.GetComponent<Health> ();

		if (attacker && health) {
			health.DealDamage (damage);
			Destroy (gameObject);
		}
	}
}
