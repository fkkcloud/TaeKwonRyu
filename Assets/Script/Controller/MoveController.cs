using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveController : MonoBehaviour {


	public MOVE side;

	private float _fadeInDuration;
	private float _fadeInDelta = 0.25f;

	private GameObject _player;

	private Image _highlight;
	private Color _current_color;

	// Use this for initialization
	void Start () {

		_player = GameObject.Find ("Player");

		foreach (Transform child in transform) {
			_highlight = child.gameObject.GetComponent<Image> ();
		}

		_current_color = _highlight.color;
		_current_color.a = 0f;
		_highlight.color = _current_color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad < _fadeInDuration) {
			// fade in
			float currentAlpha = Time.deltaTime / _fadeInDelta;
			_current_color.a -= currentAlpha;
			_highlight.color = _current_color;
		}
	}

	void OnMouseDown(){

		Player player = _player.GetComponent<Player> ();
		if (player) {
			player.SetDirection(side);
			player.TriggerMove ();

			_current_color.a = 1f;
			_highlight.color = _current_color;
			_fadeInDuration = Time.timeSinceLevelLoad + _fadeInDelta;
		}
		
	}
		
}
