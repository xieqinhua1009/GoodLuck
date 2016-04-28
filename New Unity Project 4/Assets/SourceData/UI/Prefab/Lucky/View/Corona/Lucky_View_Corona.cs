using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lucky_View_Corona : MonoBehaviour {

	private List<Lucky_Corona_IconChange> iconSpList;
	private Luck_Corona_GridDis gridDis;

	private GameObject btn_help;
	private GameObject btn_close;
	private GameObject grid;
	private GameObject icons;
	private GameObject labels;
	private GameObject win_help;
	private GameObject btn_winHelpClose;

	private UILabel Label_SumBet;//总押注.
	private UILabel Label_sum;//次数.
	private UILabel Label_CurrentSumGold;//当局所赢.
	private UILabel Label_dec1;//中奖描述.
	private UILabel Label_dec2;//中奖描述.
	private UILabel Label_dec3;//中奖描述.
	
	private int mulNum =1;
	private int goldNum=99999;
	// Use this for initialization
	void Start () {
		btn_help = this.transform.FindChild("btn_help").gameObject;
		btn_close = this.transform.FindChild("UI_CloseBtn").gameObject;
		grid = this.transform.FindChild("Grid").gameObject;
		icons = this.transform.FindChild("icons").gameObject;
		labels = this.transform.FindChild("labels").gameObject;
		win_help = this.transform.FindChild("helpWin1").gameObject;
		btn_winHelpClose = win_help.transform.FindChild("UI_CloseBtn").gameObject;

		Label_SumBet = labels.transform.FindChild ("Label_SumBet").GetComponent<UILabel>();
		Label_sum = labels.transform.FindChild ("Label_sum").GetComponent<UILabel>();
		Label_CurrentSumGold = labels.transform.FindChild ("Label_CurrentSumGold").GetComponent<UILabel>();
		Label_dec1 = labels.transform.FindChild ("Label_dec1").GetComponent<UILabel>();
		Label_dec2 = labels.transform.FindChild ("Label_dec2").GetComponent<UILabel>();
		Label_dec3 = labels.transform.FindChild ("Label_dec3").GetComponent<UILabel>();

		gridDis = grid.AddComponent<Luck_Corona_GridDis> ();

		//保存中间四个SP索引.
		iconSpList = new List<Lucky_Corona_IconChange> ();
		for (int i=1; i<=4; i++) {
			iconSpList.Add(icons.transform.FindChild("icon"+i).gameObject.AddComponent<Lucky_Corona_IconChange>());
		}

		InitEvent();
		InitData();
		Invoke ("Begin", 1);
		Invoke ("End", 5);
	}
	private void InitEvent()
	{
		UIEventListener.Get(btn_help).onClick = ClickHandle;
		UIEventListener.Get(btn_winHelpClose).onClick = HideHelpWin;
		UIEventListener.Get(btn_close).onClick = ClickHandle;
	}
	private void HideHelpWin(GameObject value)
	{
		win_help.SetActive (false);
	}
	private void InitData()
	{
		Label_SumBet.text = "9899999";
		Label_sum.text = "98";
		Label_CurrentSumGold.text = "0";
		Label_dec1.text = "3个相同[ffff00]x20倍";
		Label_dec2.text = "4个相同[ffff00]x500倍";
		Label_dec3.text = "3个相同[ffff00]x20倍";
	}
	
	private void ClickHandle(GameObject go)
	{
		switch(go.name)
		{
		case "btn_help":
			win_help.SetActive (true);
			break;
		case "UI_CloseBtn":
			
			break;
		}
	}
	private void Begin(){
		ScrollIcons ();
		ScrollGrid ();
	}
	private void End()
	{
		EndScrollIcons ();
		EndScrollGrid ();
	}

	private void ScrollIcons()
	{
		for(int i=0;i<iconSpList.Count;i++)
		{
			iconSpList[i].Begin();
		}
	}
	private void ScrollGrid()
	{
		gridDis.Begin ();
	}
	private void EndScrollIcons()
	{
		for(int i=0;i<iconSpList.Count;i++)
		{
			iconSpList[i].End(1);
		}
	}
	private void EndScrollGrid()
	{
		gridDis.End (1);
	}
}
