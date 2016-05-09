using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningHUD : MonoBehaviour {

	[Tooltip ("How fast the game will end as 'her' gets collided with attackers")]
	public float FadeInDuration;

	private bool _IsOnWarning = false;
	private bool _IsOnScreenWarning = false;
	private float _fadeInDueTime;
	private float _fadeInDueTimeOffset = 1.25f;
	private Color _current_color;
	private Image _warningHUD;
	private MainGame _mainGame;
	private GameObject _her;
	private Text _warningMsg;
	private float _xMin;
	private float _xMax;

	// Use this for initialization
	void Start () {
		_warningHUD = GetComponent<Image> ();
		_current_color = _warningHUD.color;
		_mainGame = GameObject.FindGameObjectWithTag ("MainGame").GetComponent<MainGame> ();
		_her = GameObject.FindGameObjectWithTag ("Her");
		_warningMsg = GetComponentInChildren<Text> ();
		_warningMsg.text = "";
		_warningMsg.gameObject.transform.position = new Vector3 (-100f, _warningMsg.gameObject.transform.position.y, _warningMsg.gameObject.transform.position.z);
	}

	// Update is called once per frame
	void Update () {

		if (!_her)
			return;

		// if she is near the edge, alarm!
		UpdateScreenSideX ();
		if (_xMin < _her.transform.position.x && _xMax > _her.transform.position.x) {
			StopScreenWarning ();
		} else {
			StartScreenWarning ();
		}

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

	void UpdateScreenSideX(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		float offset = 0.125f;
		_xMin = leftMost.x + offset;
		_xMax = rightMost.x - offset;
	}
		
	public void StartWarning (){
		if (_IsOnScreenWarning)
			return;
		
		if (!_IsOnWarning) {
			_fadeInDueTime = Time.timeSinceLevelLoad + FadeInDuration;
			_IsOnWarning = true;

			ActivateMsgWithText ("Enemy is too close to her!");
		}
	}

	public void StopWarning(){
		_IsOnWarning = false;
		_fadeInDueTime = 0;
		_current_color.a = 0f;
		_warningHUD.color = _current_color;

		DeactivateMsg ();
	}

	public void StartScreenWarning (){
		if (_IsOnScreenWarning)
			return;
		FadeInDuration += _fadeInDueTimeOffset;
		_fadeInDueTime = Time.timeSinceLevelLoad + FadeInDuration;
		_IsOnScreenWarning = true;

		ActivateMsgWithText ("You are too far away from her!");
	}

	public void StopScreenWarning(){
		if (!_IsOnScreenWarning)
			return;
		FadeInDuration -= _fadeInDueTimeOffset;
		_IsOnScreenWarning = false;
		_fadeInDueTime = 0;
		_current_color.a = 0f;
		_warningHUD.color = _current_color;

		DeactivateMsg ();
	}

	void EndGameLose(){
		if (!_mainGame.IsGameEnd) {
			_mainGame.IsGameEnd = true;

			_current_color.a = 0.4f;
			_warningHUD.color = _current_color;

			_mainGame.TriggerLoseCondition ();
		}
	}

	void ActivateMsgWithText(string txt){
		_warningMsg.text = txt;
		LeanTween.moveLocalX (_warningMsg.gameObject, 150f, 0.2f);
	}

	void DeactivateMsg(){
		LeanTween.moveLocalX (_warningMsg.gameObject, -100f, 0.2f);
	}
}
