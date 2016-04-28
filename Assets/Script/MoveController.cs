﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveController : MonoBehaviour {


	public MOVE side;

	private float _fadeInDuration;
	private float _fadeInDelta = 0.25f;

	private GameObject _player;

	private Image _panel;
	private Color _current_color;

	// Use this for initialization
	void Start () {

		_player = GameObject.Find ("Player");

		_panel = GetComponent<Image> ();

		_current_color = _panel.color;
		_current_color.a = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad < _fadeInDuration) {
			// fade in
			float currentAlpha = Time.deltaTime / _fadeInDelta;
			_current_color.a -= currentAlpha;
			_panel.color = _current_color;
		}
	}

	void OnMouseDown(){

		Player playerComp = _player.GetComponent<Player> ();
		if (playerComp) {
			playerComp.SetDirection(side);
			playerComp.TriggerMove ();

			_current_color.a = 1f;
			_panel.color = _current_color;
			_fadeInDuration = Time.timeSinceLevelLoad + _fadeInDelta;
		}
		
	}
		
}
