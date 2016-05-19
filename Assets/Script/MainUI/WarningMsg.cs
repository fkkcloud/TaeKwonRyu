using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningMsg : MonoBehaviour {

	private Text _warningMsg;

	private Color _warningMsgColorAlpha;
	private Color _warningMsgColorNoAlpha;

	// Use this for initialization
	void Start () {
		_warningMsg = GetComponent<Text> ();

		_warningMsg.text = "";
		_warningMsgColorAlpha = _warningMsg.color;
		_warningMsgColorNoAlpha = new Color(_warningMsg.color.r, _warningMsg.color.g, _warningMsg.color.b, 0f);
		_warningMsg.color = _warningMsgColorNoAlpha;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateMsgWithText(string txt){
		_warningMsg.text = txt;
		LeanTween.colorText (_warningMsg.rectTransform, _warningMsgColorAlpha, 0.25f).setEase(LeanTweenType.easeInOutCubic);
	}

	public void DeactivateMsg(){
		LeanTween.colorText (_warningMsg.rectTransform, _warningMsgColorNoAlpha, 0.1f);
	}
}
