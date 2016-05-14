using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Belt
{
	public int level; // current level
	public float[] damages; // json controlled for balance
	public int[] costs; //  json controlled for balance
	public Image image;
	public string itemname;
}
