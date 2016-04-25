using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {

	public float FadeInDuration;

	private Image _fade_panel;
	private Color _current_color = Color.white;

	// Use this for initialization
	void Start () {
		_fade_panel = GetComponent<Image> ();

		_current_color.a = 1f;
		_fade_panel.color = _current_color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad < FadeInDuration) {
			// fade in
			float currentAlpha = Time.deltaTime / FadeInDuration;
			_current_color.a -= currentAlpha;
			_fade_panel.color = _current_color;
		} else {
			// finish
			gameObject.SetActive(false);
		}
	}
}
