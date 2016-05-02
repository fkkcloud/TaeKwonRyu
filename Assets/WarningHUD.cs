using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningHUD : MonoBehaviour {

	[Tooltip ("How fast the game will end as 'her' gets collided with attackers")]
	public float FadeInDuration;

	private bool _IsOnWarning = false;
	private float _fadeInDueTime;
	private Color _current_color;
	private Image _warningHUD;
	private MainGame _mainGame;

	// Use this for initialization
	void Start () {
		_warningHUD = GetComponent<Image> ();
		_current_color = _warningHUD.color;
		_mainGame = GameObject.Find ("MainGame").GetComponent<MainGame> ();
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad < _fadeInDueTime) {
			// fade in
			float currentAlpha = Time.deltaTime / FadeInDuration;
			_current_color.a += currentAlpha;
			_warningHUD.color = _current_color;
		} else if (_fadeInDueTime > 0 && Time.timeSinceLevelLoad >= _fadeInDueTime){
			EndGameLose ();
		}
	}
		
	public void StartWarning (){
		if (!_IsOnWarning) {
			
			_fadeInDueTime = Time.timeSinceLevelLoad + FadeInDuration;
			_IsOnWarning = true;
		}
	}

	public void StopWarning(){
		_IsOnWarning = false;
		_fadeInDueTime = 0;
		_current_color.a = 0f;
		_warningHUD.color = _current_color;
	}

	void EndGameLose(){
		if (!_mainGame.IsGameEnd) {
			_mainGame.IsGameEnd = true;

			_current_color.a = 0.4f;
			_warningHUD.color = _current_color;

			_mainGame.TriggerLoseCondition ();
		}
	}
}
