using UnityEngine;
using System.Collections;

public class Lucky_P_Item : MonoBehaviour {
	public int type;
	public static string iconName = "iconr";
	public Lucky_P_Item first;
	public Lucky_P_Item back;

	private UISprite icon;
	private GameObject rect;

	public float endPoint = -1;
	public bool isEnd=false; 
	// Use this for initialization
	void Start () {
		icon = transform.FindChild("icon").GetComponent<UISprite>();
		rect = transform.FindChild("rect").gameObject;
		ChangeTest();
	}

	//1-9;
	public void SetIcon(int type)
	{
		this.type = type;
		icon.spriteName = iconName+type;
	}
	public void ChangeTest()
	{
		SetIcon(NGUITools.RandomRange(1,9));
	}
	public void ShowRect(bool value)
	{
		rect.SetActive (value);
	}
	public bool IsSame(Lucky_P_Item item)
	{
		if(type==item.type||item.type==9||type==9)
			return true;
		return false;
	}
	public bool IsAllR()
	{
		if(type==9)
			return true;
		return false;
	}
}
