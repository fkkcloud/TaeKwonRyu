using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Belt
{
	public int level; // current level
	public float[] damages; // json controlled for balance
	public int[] costs; //  json controlled for balance
	public string itemname;
	public Color color;
}
