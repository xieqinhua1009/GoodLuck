using UnityEngine;
using System.Collections;

public class Lucky_Corona_IconChange : MonoBehaviour {
	
	private UISprite self;
	private bool isBegin=false;
	// Use this for initialization
	void Start () {
		self = this.GetComponent<UISprite> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isBegin) {
			self.spriteName = Lucky_P_Item.iconName + NGUITools.RandomRange(1,9);
		}
	}

	public void Begin()
	{
		isBegin = true;
	}
	public void End(int type)
	{
		isBegin = false;
		self.spriteName = Lucky_P_Item.iconName + type;
	}
}
