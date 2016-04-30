using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float HitPoint = 100f;

	public GameObject FX_Die;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// return wheather pawn is dead or not
	public bool DealDamage(float damage){
		HitPoint -= damage;
		if (HitPoint <= 0) {
			// Optionally do die animation
			// Important thing to know is that if I have die animation,
			// make sure to call the DestroyObject function in the Animator when the animation end! just like the SetSpeed()!
			AllocateDieFX();

			DestroyObject();
			return true;
		}
		return false;
	}

	private void DestroyObject(){
		Destroy (gameObject);
	}

	private void AllocateDieFX(){
		GameObject fxDie = Instantiate (FX_Die) as GameObject;
		fxDie.transform.position = gameObject.transform.position;
		Destroy (fxDie, 0.7f);
	}
}
