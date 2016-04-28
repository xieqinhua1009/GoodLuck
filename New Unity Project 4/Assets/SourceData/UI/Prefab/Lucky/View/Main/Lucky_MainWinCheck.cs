using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Lucky_MainWinCheck {
	private static Lucky_MainWinCheck _instance;
	public static Lucky_MainWinCheck instance{
		get{
			if(_instance==null)
				_instance = new Lucky_MainWinCheck();
			return _instance;
		}
		set{
			_instance=value;
		}
	}
	private List<List<Lucky_P_Item>> effectList;
	private int GetIndex=0;

	private List<List<Lucky_P_Item>> dataList;
	private int luxianNum=1;
	public bool checkDone=false;

	public bool IsResult()
	{
		if(effectList==null||effectList.Count==0)
			return false;
		return true;
	}
	public void Begin(List<List<Lucky_P_Item>> list,int luxian)
	{
		checkDone=false;
		dataList = list;
		luxianNum = luxian;
		Check();
	}

	public void ShowEffect()
	{
		HideAllRect();
		List<Lucky_P_Item> list = NextGetResult();
		for(int i=0;i<list.Count;i++)
		{
			list[i].ShowRect(true);
		}
	}
	//隐藏所有特效rect.
	public void HideAllRect()
	{
		if(dataList==null)
			return;
		for(int i=0;i<dataList.Count;i++)
		{
			for(int j=0;j<dataList[i].Count;j++)
			{
				dataList[i][j].ShowRect(false);
			}
		}
	}

	//获取下一组显示的图标特效.
	private List<Lucky_P_Item> NextGetResult()
	{
		if(effectList==null||effectList.Count==0)
			return null;
		if(effectList.Count<=GetIndex)
			GetIndex=0;
		List<Lucky_P_Item> temp = effectList[GetIndex];
		GetIndex++;
		return temp;
	}


	private void Check()
	{
		effectList = new List<List<Lucky_P_Item>>();
		List<Lucky_P_Item>  list =CheckAllType();
		if(list.Count>0)
		{
			Debug.Log("全部同一种图标.大奖.");
			//全部同一种图标.大奖.
			effectList.Add(list);
			return;
		}
		List<Lucky_P_Item>  otherList1 = CheckOtherType(new List<int>(){1,2,3,9});//全部为某三种图标.
		List<Lucky_P_Item>  otherList2 = CheckOtherType(new List<int>(){4,5,6,9});//全部为某三种图标.

		List<List<string>> lists = new List<List<string>>();
		lists.Add(new List<string>(){"1,0","1,1","1,2","1,3","1,4"});//路线1.
		if(luxianNum>1)
			lists.Add(new List<string>(){"0,0","0,1","0,2","0,3","0,4"});//路线2.
		if(luxianNum>2)
			lists.Add(new List<string>(){"2,0","2,1","2,2","2,3","2,4"});//路线3.
		if(luxianNum>3)
			lists.Add(new List<string>(){"0,0","1,1","2,2","1,3","0,4"});//路线4.
		if(luxianNum>4)
			lists.Add(new List<string>(){"2,0","1,1","0,2","1,3","2,4"});//路线5.
		if(luxianNum>5)
			lists.Add(new List<string>(){"0,0","0,1","1,2","0,3","0,4"});//路线6.
		if(luxianNum>6)
			lists.Add(new List<string>(){"2,0","2,1","1,2","2,3","2,4"});//路线7.
		if(luxianNum>7)
			lists.Add(new List<string>(){"1,0","2,1","2,2","2,3","1,4"});//路线8.
		if(luxianNum>8)
			lists.Add(new List<string>(){"1,0","0,1","0,2","0,3","1,4"});//路线9.


		for(int i=0;i<lists.Count;i++)
		{
			List<List<Lucky_P_Item>>  temp = CheckType(lists[i],1);
			if(temp.Count>0)
				effectList.AddRange(temp);
		}
		GetIndex=0;
		checkDone=true;
	}
	private List<Lucky_P_Item> CheckAllType()
	{
		List<Lucky_P_Item> list = new List<Lucky_P_Item>();
		Lucky_P_Item one=null;
		for(int i=0;i<dataList.Count;i++)
		{
			for(int j=0;j<dataList[i].Count;j++)
			{
				if(one==null&&!dataList[i][j].IsAllR())
					one = dataList[i][j];
				if(one!=null&&!dataList[i][j].IsSame(one))
				{
					return new List<Lucky_P_Item>();
				}
				list.Add(dataList[i][j]);
			}
		}
		return list;
	}
	private List<Lucky_P_Item> CheckOtherType(List<int> con)
	{
		List<Lucky_P_Item> list = new List<Lucky_P_Item>();
		for(int i=0;i<dataList.Count;i++)
		{
			for(int j=0;j<dataList[i].Count;j++)
			{
				if(!con.Contains(dataList[i][j].type))
				{
					return new List<Lucky_P_Item>();
				}
				list.Add(dataList[i][j]);
			}
		}
		return list;
	}

	private List<List<Lucky_P_Item>> CheckType(List<string> dic,int num)
	{
		List<Lucky_P_Item> list = new List<Lucky_P_Item>();
		List<List<Lucky_P_Item>> lists = new List<List<Lucky_P_Item>>();
		Lucky_P_Item first=null;
		Lucky_P_Item last=null;
		for(int i=0;i<dic.Count;i++)
		{
			string[] strs = dic[i].Split(',');
			int key = int.Parse(strs[0]);
			int value = int.Parse(strs[1]);
			list.Add(dataList[value][key]);
			if(!dataList[value][key].IsAllR())
			{
				last = dataList[value][key];
				if(first==null)
					first = last;
			}
		}
		if(first==null&&last==null)
		{
			lists.Add(list);
			return lists;
		}

		List<Lucky_P_Item> tempList = new List<Lucky_P_Item>();
		if(first.IsSame(list[0])&&first.IsSame(list[1])&&first.IsSame(list[2]))//左3连.
		{
			tempList.Add(list[0]);
			tempList.Add(list[1]);
			tempList.Add(list[2]);
			if(first.IsSame(list[3]))//4连
			{
				tempList.Add(list[3]);
				if(first.IsSame(list[4]))//5连.
				{
					tempList.Add(list[4]);
					lists.Add(tempList);
					return lists;
				}
			}
			lists.Add(tempList);
		}

		//判断右边
		tempList = new List<Lucky_P_Item>();
		if(last.IsSame(list[4])&&last.IsSame(list[3])&&last.IsSame(list[2]))//左3连.
		{
			tempList.Add(list[4]);
			tempList.Add(list[3]);
			tempList.Add(list[2]);
			if(first.IsSame(list[1]))//4连
			{
				tempList.Add(list[1]);
			}
			lists.Add(tempList);
		}
		return lists;
	}
}
