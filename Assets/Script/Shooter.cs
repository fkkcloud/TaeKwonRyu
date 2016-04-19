using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public GameObject projectile;
	public GameObject gun;

	private GameObject projectileParent;
	private Animator animator;

	private Spawner myLaneSpawner;

	// Use this for initialization
	void Start () {
		// create parent if necessary
		projectileParent = GameObject.Find ("Projectiles");
		if (!projectileParent) {
			projectileParent = new GameObject();
			projectileParent.name = "Projectiles";
		}

		// grab animator
		animator = GameObject.FindObjectOfType<Animator> ();

		// find the lanespawner
		SetMyLaneSpawner ();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsAttackerAhead () && animator) {
			animator.SetBool ("IsAttacking", true);
		} else {
			animator.SetBool ("IsAttacking", false);
		}
	}

	private void Fire(){
		GameObject proj = Instantiate (projectile); // instantiate prefab!
		proj.transform.parent = projectileParent.transform;
		proj.transform.position = gun.transform.position;
	}

	private bool IsAttackerAhead(){

		// if there is not Attacker
		if (myLaneSpawner.transform.childCount == 0)
			return false;

		// there is Attacker ahead!
		foreach (Transform childTransform in myLaneSpawner.transform) {
			if (childTransform.position.x > transform.position.x) {
				return true;
			}
		}

		// there is Attacker but its behind
		return false;
	}

	// Look through all spawner and set spawner
	void SetMyLaneSpawner(){
		Spawner[] spawnerArray = GameObject.FindObjectsOfType<Spawner> ();

		foreach (Spawner spawner in spawnerArray) {
			if (spawner.transform.position.y == transform.position.y) {
				myLaneSpawner = spawner;
				return;
			}
		}

		Debug.LogError (name + " can't spawner available");
	}
}
