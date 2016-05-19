using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningHUD : MonoBehaviour {

	[Tooltip ("How fast the game will end as 'her' gets collided with attackers")]
	public float FadeInDuration;

	private GameObject _her;

	private Image _warningHUD;
	private MainGame _mainGame;
	private Color _current_color;
	private WarningMsg _warningMsg;

	private float _xMin;
	private float _xMax;

	private float _fadeInDueTimeOriginal;
	private float _fadeInDueTime;

	private bool _IsOnWarning = false;

	// Use this for initialization
	void Start () {
		_warningHUD = GetComponent<Image> ();
		_current_color = _warningHUD.color;
		_mainGame = GameObject.FindGameObjectWithTag ("MainGame").GetComponent<MainGame> ();
		_her = GameObject.FindGameObjectWithTag ("Her");
		_warningMsg = GameObject.Find ("WarningMSG").GetComponent<WarningMsg> ();

	}

	// Update is called once per frame
	void Update () {
		if (!_her)
			return;

		// execute coloring the warning HUD
		AnimateWarningHUD();
	}

	void AnimateWarningHUD(){
		if (Time.timeSinceLevelLoad < _fadeInDueTime) {
			// fade in
			float currentAlpha = Mathf.Clamp(Time.deltaTime / FadeInDuration, 0f, 0.7f);
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

			_warningMsg.ActivateMsgWithText ("Enemy is too close to her!");
		}
	}

	public void StopWarning(){
		_IsOnWarning = false;
		_fadeInDueTime = 0;
		_current_color.a = 0f;
		_warningHUD.color = _current_color;

		_warningMsg.DeactivateMsg ();
	}

	void EndGameLose(){
		if (!_mainGame.IsGameEnd) {
			_current_color.a = 0.4f;
			_warningHUD.color = _current_color;

			_mainGame.IsGameEnd = true;
			_mainGame.TriggerLoseCondition ();
		}
	}


}
