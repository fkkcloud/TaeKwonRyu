using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LimitBreak : MonoBehaviour {

	private float _limitBreakValue;
	private Image _gauge;
	private Color _flashColor = Color.white;
	private Color _gaugeColor;

	private Player _player;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		_gauge = GetComponent<Image> ();
		_gaugeColor = _gauge.color;
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AddHit(){
		if (_limitBreakValue < 1.0f) {
			_limitBreakValue += 0.05f;
			//_gauge.transform.localScale = new Vector3 (_limitBreakValue, _gauge.transform.localScale.y, _gauge.transform.localScale.z);
			LeanTween.scaleX (_gauge.gameObject, _limitBreakValue, 0.3f).setEase(LeanTweenType.easeOutQuad);
			FlashGaugeAnimation();
		} else if (_limitBreakValue >= 0.95f) {
			_player.IsLimitBreakReady = true;
			FlashGaugeAnimation();
		}
	}

	public void Reset(){
		_limitBreakValue = 0f;
		//_gauge.transform.localScale = new Vector3(_limitBreakValue, _gauge.transform.localScale.y, _gauge.transform.localScale.z);
		LeanTween.scaleX (_gauge.gameObject, _limitBreakValue, 0.5f).setEase(LeanTweenType.easeInCubic);
	}

	private void FlashGaugeAnimation(){
		//LeanTween.color (gameObject, _flashColor, 0.2f).setEase(LeanTweenType.easeInCubic);
		LeanTween.value (gameObject, _gaugeColor, _flashColor, 0.125f).setOnUpdate ((Color val) => { 
			_gauge.color = val;
		}).setEase(LeanTweenType.easeOutQuad).setOnComplete(BackToOriginColor);
	}

	private void BackToOriginColor(){
		//LeanTween.color (gameObject, _flashColor, 0.2f).setEase(LeanTweenType.easeInCubic);
		LeanTween.value (gameObject, _flashColor, _gaugeColor, 0.125f).setOnUpdate ((Color val) => { 
			_gauge.color = val;
		}).setEase(LeanTweenType.easeOutQuad);
		if (_player.IsLimitBreakReady) {
			Invoke ("FlashGaugeAnimation", 0.125f);
		}
	}


}
