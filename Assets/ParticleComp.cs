using UnityEngine;
using System.Collections;

public class ParticleComp : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// Set the sorting layer of the particle system.
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "FX";
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;

	}

}
