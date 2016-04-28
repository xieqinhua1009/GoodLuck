using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lucky_ItemSVs : MonoBehaviour {
	
	public List<float> endPoints = new List<float>(){0,-75,-150};

	public float maxY = -300;
	public float heightSpace = 75;

	private float currentMinY=0;

	private int listLengh = 0;
	private List<Lucky_P_Item> list ;//数据.
	private List<Lucky_P_Item> endList;//停止Item数组.
	public float _speed=15;
	private List<int> endInts;//停止类型数组.
	private int endIndex;
	private bool isBegin=false;//是否开始.
	private bool startEnd=false;//是否开始停止.
	public bool isDone=false;//是否完成.

	private float _delay;//延迟.

	Lucky_P_Item firstlyItem = null;
	Lucky_P_Item lastItem =null;
	void Start () {
		list = new List<Lucky_P_Item> ();
		int i = 0;
		Lucky_P_Item tempItem=null;
		//形成一个循环.前面跟随前面.
		foreach (Transform tf in transform) {
			Lucky_P_Item lp = tf.GetComponent<Lucky_P_Item>();
			if(lp==null)
				lp = tf.gameObject.AddComponent<Lucky_P_Item>();
			list.Add(lp);
			tf.localPosition = new Vector3(0,i*-75,0);
			tf.gameObject.name = i.ToString();
			i++;
			if(tempItem!=null)
			{
				tempItem.first = lp;
				lp.back = tempItem;
			}
			else
				firstlyItem = lp;
			if(i>=tf.childCount)//确认第一个和最后一个.
				lastItem = lp;

			tempItem = lp;
		}
		listLengh = list.Count;

//		Begin ();

	}

	public void SetSpeed(float value)
	{
		_speed = value;
	}

	public void Begin()
	{
		startEnd = false;
		isBegin = true;
		isDone = false;
		for (int i=0; i<list.Count; i++) {
			list[i].endPoint=-1;
			list[i].isEnd = false;
		}
		InvokeRepeating("MyUpdate",0, 0.01f);
	}
	public void End(List<int> ends,float delay =0){
		endList = new List<Lucky_P_Item> ();
		endInts = ends;
		startEnd = true;
		endIndex = endInts.Count - 1;
		_delay = delay;
	}
	public void RestDelay()
	{
		_delay=0;
	}

	public List<Lucky_P_Item> GetEndList()
	{
		return endList;
	}

	private Lucky_P_Item item;
	private Vector3 itemV3;
	void MyUpdate(){
		if (isBegin&&listLengh>0) {
			item = null;
			for(int i=0;i<listLengh;i++)
			{
				if(item==null)
					item = lastItem;
				else
					item = item.back;
				itemV3 = item.transform.localPosition;


				if(item.first==null)
				{
					itemV3.y-=_speed;
				}else{
					itemV3.y = item.first.transform.localPosition.y+heightSpace;
				}
				//判断是否到了奖励的终点.
				if(startEnd&&item.endPoint!=-1&&item.endPoint>=itemV3.y)
				{
					itemV3.y = item.endPoint;
					item.isEnd = true;
					if(!endList.Contains(item))
						endList.Add(item);
					if(endList.Count==endInts.Count)
					{
						//完成.
						isBegin=false;
						isDone = true;
						CancelInvoke("MyUpdate");
					}
				}
				item.transform.localPosition = itemV3;
			}
			if(lastItem.transform.localPosition.y<maxY)
			{
				//最后一个变成第一个，倒数第二个变成最后一个.
				firstlyItem.back = lastItem;
				lastItem.first = firstlyItem;
				item = lastItem.back;
				item.first = null;

				firstlyItem = lastItem;
				lastItem = item;
				firstlyItem.back = null;
				if(startEnd&&_delay<=0)//开始停止.
				{
					if(endIndex>=0)
					{
						if(endInts.Count!=endPoints.Count)
						{
							Debug.LogError("奖励数组与终点数组长度不一");
							return;
						}
						//设置图标后.
						firstlyItem.SetIcon(endInts[endIndex]);
						firstlyItem.endPoint = endPoints[endIndex];
						endIndex--;
					}
				}else{
					if(_delay>0)
						_delay -=Time.deltaTime;
					firstlyItem.ChangeTest();
				}
			}
		}
	}

	//1-9;
//	public void SetIcon(int type)
//	{
//		icon.spriteName = "iconr"+type;
//	}
//	private void ChangeTest()
//	{
//		SetIcon(NGUITools.RandomRange(1,9));
//	}
}
