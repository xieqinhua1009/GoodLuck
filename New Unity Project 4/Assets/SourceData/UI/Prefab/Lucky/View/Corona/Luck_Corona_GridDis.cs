using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Luck_Corona_GridDis : MonoBehaviour {

	private List<Lucky_P_Item> dataList; 
	private int _speed;
	private int _accSp=1;
	private int maxSp=0;
	private int minSp=30;
	private int frame=0;
	private Lucky_P_Item current;
	
	// Use this for initialization
	void Start () {

		//四周格子初始化.
		dataList = new List<Lucky_P_Item> ();
		Lucky_P_Item item;
		for (int i=1; i<=this.transform.childCount; i++) {
//			KTurntablePos_base poss = KConfigFileManager.Instance.KTurntablePosbase.getData(i.ToString());
//			KSlotTreasure_base baseData = KConfigFileManager.Instance.KSlotTreasurebase.getData(poss.treasureID_int.ToString());
			item = this.transform.FindChild("item"+i).GetComponent<Lucky_P_Item>();
			dataList.Add(item);
			if(i==this.transform.childCount)
			{
				dataList[0].back = item;
				item.first = dataList[0];
			}
			if(i>1)
			{
				dataList[i-2].first = item;
				item.back = dataList[i-2];
			}
//			item.SetIcon(baseData.subclass_int+1);
		}
	}

	public void Begin()
	{
		_speed = minSp-1;
		InvokeRepeating ("MyUpdate", 0, 0.01f);
		_accSp = -1;
		if (current == null)
			current = dataList [0];
	}

	private int endType = 0;
	public void End(int type)
	{
		_accSp = 1;
		_speed = maxSp + 1;
		endType = type;
	}

	private void MyUpdate()
	{
		frame++;
		if (frame >= _speed) {
			if(_speed>maxSp&&_speed<minSp)
				_speed+=_accSp;
			frame=0;
			current.ShowRect(false);
			current = current.first;
			current.ShowRect(true);
			if(_accSp>0&&_speed==minSp&&current.type==endType)
			{
				//降低速度后，找到对应的类型停止.
				CancelInvoke("MyUpdate");
			}
		}

	}
}
