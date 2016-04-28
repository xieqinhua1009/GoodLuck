using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Lucky_BS_Saizi : MonoBehaviour {
	private UISprite sp;
	private List<string> nameList = new List<string>(){"icon1","icon2","icon3","icon4","icon5","icon6"};
	private bool isDone=false;
	private bool isBegin=false;
	public float speed=10;
	private float rotationSpeed=30;
	private float frame=0;
	public int point;
	void Start () {
		sp = this.GetComponent<UISprite>();
	}

	// Update is called once per frame
	void Update () {
		if(isBegin)
		{
			if(frame<=0)
			{
				sp.spriteName = nameList[NGUITools.RandomRange(0,nameList.Count-1)];

				frame=100;
			}else{
				frame-=speed;
			}
			sp.transform.localEulerAngles += new Vector3(0,0,rotationSpeed);
		}
	}

	public void Begin()
	{
		isBegin=true;
		isDone=false;
	}
	public void End(int type)
	{
		isBegin=false;
		sp.transform.localEulerAngles = Vector3.zero;
		sp.spriteName = nameList[type-1];
		point = type;
	}
}
