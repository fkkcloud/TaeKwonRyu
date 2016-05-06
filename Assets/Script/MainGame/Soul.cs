using UnityEngine;
using System.Collections;

public class Soul : MonoBehaviour {

	public GameObject FX_Soul;

	private int _soulPoint;

	private SoulPointDisplay _soulPointDisplay;
	private Attacker _attacker;

	private bool _IsConsumed = false;

	// Use this for initialization
	void Start () {
		Vector3 targetPos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
		LeanTween.move (gameObject, targetPos, 0.5f).setLoopPingPong ().setEase(LeanTweenType.easeInCubic);

		_soulPointDisplay = GameObject.Find ("TextSoulPoint").GetComponent<SoulPointDisplay> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSoulPoint(int SoulPoint){
		_soulPoint = SoulPoint;
	}

	void OnTriggerEnter2D(Collider2D col){

		GameObject obj = col.gameObject;

		if (obj.GetComponent<Player> ()) 
		{
			if (_IsConsumed)
				return;

			// fx
			AllocateSoulFX ();

			// cancel the existing leantween
			LeanTween.cancel (gameObject);

			// move to the Left Top most UI for collection of soul
			float distance = transform.position.z - Camera.main.transform.position.z;
			Vector3 leftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
			leftCorner = new Vector3 (leftCorner.x + 0.5f, (leftCorner.y * -1) - 0.25f, -4);

			_IsConsumed = true;
			LeanTween.move (gameObject, leftCorner, 0.4f);

			Invoke ("AddSoulPoint", 0.3f);

			Destroy (gameObject, 0.4f);
		}
	}

	private void AllocateSoulFX(){
		GameObject fxDie = Instantiate (FX_Soul) as GameObject;
		fxDie.transform.position = gameObject.transform.position;
		Destroy (fxDie, 0.7f);
	}

	void AddSoulPoint(){
		_soulPointDisplay.AddSouls (_soulPoint);
	}
}
