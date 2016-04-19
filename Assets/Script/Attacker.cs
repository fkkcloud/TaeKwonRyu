using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class Attacker : MonoBehaviour {

	//[Range(-1f, 1.5f)] public float walkSpeed;
	private float walkSpeed;
	[Tooltip ("Average Seconds between Appearances")]
	public float seenEverySeconds;
	private GameObject currentTarget;
	private Animator anim;

	// Use this for initialization
	void Start () {
		Rigidbody2D myRigidBody2D = gameObject.AddComponent<Rigidbody2D> ();
		myRigidBody2D.isKinematic = true;
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.left * walkSpeed * Time.deltaTime);
		if (!currentTarget && anim) {
			anim.SetBool ("IsAttacking", false);
		}
	}

	void OnTriggerEnter2D(){
		//Debug.Log ("Collistion with Attacker " + name);
	}

	public void SetSpeed(float speed){
		walkSpeed = speed;
	}

	// Called at the time when actual attack happens in Animator (animation state!)
	public void StrikeCurrentTarget(float damage){
		Debug.Log (name + " damaged for " + damage);
		if (currentTarget) {
			Health health = currentTarget.GetComponent<Health> ();
			if (health) {
				health.DealDamage (damage);
			}
		}
	}

	public void Attack(GameObject obj){
		currentTarget = obj;
	}
}
