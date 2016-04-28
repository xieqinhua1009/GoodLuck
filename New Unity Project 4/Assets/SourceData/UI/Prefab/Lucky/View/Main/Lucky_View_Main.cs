using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lucky_View_Main : MonoBehaviour {
	private GameObject bottomWin;
	private GameObject btn_go;
	private GameObject btn_bs;
	private UILabel label_go;
	private UISprite sp_go;
	private BoxCollider box_go;
	private GameObject btn_help1;
	private GameObject btn_help2;

	private UILabel label_huaFei;
	private UILabel label_jiangLi;
	private UILabel label_luXian;
	private UILabel label_zongHuaFei;

	private GameObject luXian_btnAdd;
	private GameObject luXian_btnSubt;
	private GameObject huaFei_btnAdd;
	private GameObject huaFei_btnSubt;

	//两个帮助界面.
	private GameObject helpWin1;
	private GameObject helpWin2;
	private Lucky_View_MainHelpwin1 sc_helpwin1;
	private Lucky_View_MainHelpwin2 sc_helpwin2;

	private UIScrollView scrollView;
	private UIGrid grid;
	private GameObject btn_close;

	private Lucky_ItemSVs itemSv1;
	private Lucky_ItemSVs itemSv2;
	private Lucky_ItemSVs itemSv3;
	private Lucky_ItemSVs itemSv4;
	private Lucky_ItemSVs itemSv5;

	private int luXianNum=1;
	private int huaFeiNum=0;

	private float autoEndTime=-1f;//等-1则不判断
	private bool isCheckDone=false;

	private float changeEffectTime=0f;
//	KBetting_base data ;

	// Use this for initialization
	void Start () {
		btn_close = this.transform.FindChild("UI_CloseBtn").gameObject;
		bottomWin = this.transform.FindChild("bottomR").gameObject;
		btn_go = bottomWin.transform.FindChild("btn_go").gameObject;
		btn_bs = bottomWin.transform.FindChild("btn_bs").gameObject;
		box_go = btn_go.GetComponent<BoxCollider> ();
		label_go = btn_go.transform.FindChild ("Label").GetComponent<UILabel> ();
		sp_go = btn_go.transform.FindChild ("Background").GetComponent<UISprite> ();
		btn_help1 = bottomWin.transform.FindChild("help1").gameObject;
		btn_help2 = bottomWin.transform.FindChild("help2").gameObject;


		label_huaFei = bottomWin.transform.FindChild("Label_huaFei").GetComponent<UILabel>();
		label_jiangLi = bottomWin.transform.FindChild("Label_JiangLi").GetComponent<UILabel>();
		label_luXian = bottomWin.transform.FindChild("Label_luXian").GetComponent<UILabel>();
		label_zongHuaFei = bottomWin.transform.FindChild("Label_zongHuaFei").GetComponent<UILabel>();

		luXian_btnAdd = label_luXian.transform.FindChild("btn_add").gameObject;
		luXian_btnSubt = label_luXian.transform.FindChild("btn_subt").gameObject;
		huaFei_btnAdd = label_huaFei.transform.FindChild("btn_add").gameObject;
		huaFei_btnSubt = label_huaFei.transform.FindChild("btn_subt").gameObject;

		helpWin1 = this.transform.FindChild("helpWin1").gameObject;
		helpWin2 = this.transform.FindChild("helpWin2").gameObject;
		sc_helpwin1 = helpWin1.AddComponent<Lucky_View_MainHelpwin1>();
		sc_helpwin2 = helpWin2.AddComponent<Lucky_View_MainHelpwin2>();
		sc_helpwin1.main = this;
		sc_helpwin2.main = this;

		scrollView= this.transform.FindChild("Scroll View").gameObject.GetComponent<UIScrollView>();
		grid = scrollView.transform.FindChild("Grid").GetComponent<UIGrid>();
		scrollView.panel.depth =10;
		itemSv1 = grid.transform.FindChild ("items1").GetComponent<Lucky_ItemSVs> ();
		itemSv2 = grid.transform.FindChild ("items2").GetComponent<Lucky_ItemSVs> ();
		itemSv3 = grid.transform.FindChild ("items3").GetComponent<Lucky_ItemSVs> ();
		itemSv4 = grid.transform.FindChild ("items4").GetComponent<Lucky_ItemSVs> ();
		itemSv5 = grid.transform.FindChild ("items5").GetComponent<Lucky_ItemSVs> ();
		if(itemSv1==null)
		{
			itemSv1 = grid.transform.FindChild ("items1").gameObject.AddComponent<Lucky_ItemSVs> ();
			itemSv2 = grid.transform.FindChild ("items2").gameObject.AddComponent<Lucky_ItemSVs> ();
			itemSv3 = grid.transform.FindChild ("items3").gameObject.AddComponent<Lucky_ItemSVs> ();
			itemSv4 = grid.transform.FindChild ("items4").gameObject.AddComponent<Lucky_ItemSVs> ();
			itemSv5 = grid.transform.FindChild ("items5").gameObject.AddComponent<Lucky_ItemSVs> ();
		}
		InitEvent();
		InitData();
	}
	private void InitEvent()
	{
		UIEventListener.Get(btn_close).onClick = ClickHandle;
		UIEventListener.Get(btn_go).onClick = ClickHandle;
		UIEventListener.Get(btn_bs).onClick = ClickHandle;
		UIEventListener.Get(btn_help1).onClick = ClickHandle;
		UIEventListener.Get(btn_help2).onClick = ClickHandle;
		UIEventListener.Get(luXian_btnAdd).onClick = LuXianChange;
		UIEventListener.Get(luXian_btnSubt).onClick = LuXianChange;
		UIEventListener.Get(huaFei_btnAdd).onClick = HuaFeiChange;
		UIEventListener.Get(huaFei_btnSubt).onClick = HuaFeiChange;
	}
	private void InitData()
	{
//		data = KConfigFileManager.Instance.KBettingbase.getData(PlayerManager.instance.player.level_int.ToString());
//		huaFeiNum = data.unitBetting_int;
//		label_huaFei.text=data.unitBetting_int.ToString()+"x"+luXianNum;
		label_luXian.text=luXianNum.ToString();
		label_zongHuaFei.text = (huaFeiNum*luXianNum).ToString();
	}
	private void ClickHandle(GameObject go)
	{
		switch(go.name)
		{
		case "UI_CloseBtn":
//			UIWIndowsManager.Instance.closeWin(typeof(Lucky_Load));
			break;
		case "btn_go":
			label_go.effectStyle = UILabel.Effect.None;
			label_go.color = new Color(0.7f,0.7f,0.7f,1);
			sp_go.color = Color.black;
			if(label_go.text=="开始寻宝")
			{
				StartScroll();
				box_go.enabled=false;
				Invoke("BtnGoChangeStop",2);
			}
			else
			{
				box_go.enabled=false;
				EndScroll(0);
				Invoke("BtnGoChangeStop",2);
			}

			break;
		case "btn_bs":
			Lucky_Controller.Instance().GetView().ShowBiss();
			break;
		case "help1":
			ShowHelp(helpWin1);
			break;
		case "help2":
			ShowHelp(helpWin2);
			break;
		}
	}
	//开始转动.
	private void StartScroll()
	{
		Lucky_MainWinCheck.instance.checkDone=false;
		Lucky_MainWinCheck.instance.HideAllRect();
		autoEndTime=3;
		itemSv1.Begin ();
		itemSv2.Begin ();
		itemSv3.Begin ();
		itemSv4.Begin ();
		itemSv5.Begin ();
	}
	//停止转动.
	private void EndScroll(float delay)
	{
		if(autoEndTime==-1)
		{
			itemSv1.RestDelay();
			itemSv2.RestDelay();
			itemSv3.RestDelay();
			itemSv4.RestDelay();
			itemSv5.RestDelay();
			return;
		}
		List<int> results = new List<int> (){1,2,1};
		itemSv1.End (results,0f);
		results = new List<int> (){1,9,2};
		itemSv2.End (results,delay);
		results = new List<int> (){1,9,3};
		itemSv3.End (results,delay*2);
		results = new List<int> (){1,9,9};
		itemSv4.End (results,delay*3);
		results = new List<int> (){1,4,3};
		itemSv5.End (results,delay*4);

		isCheckDone=true;
		autoEndTime=-1;
	}
	void Update()
	{
		if(isCheckDone)
		{
			if(itemSv1.isDone&&itemSv2.isDone&&itemSv3.isDone&&itemSv4.isDone&&itemSv5.isDone)
			{
				List<List<Lucky_P_Item>> temp = new List<List<Lucky_P_Item>>();
				temp.Add(itemSv1.GetEndList());
				temp.Add(itemSv2.GetEndList());
				temp.Add(itemSv3.GetEndList());
				temp.Add(itemSv4.GetEndList());
				temp.Add(itemSv5.GetEndList());
				Lucky_MainWinCheck.instance.Begin(temp,luXianNum);
				isCheckDone=false;
				BtnGoChangeStop();

			}
		}
		if(Lucky_MainWinCheck.instance.checkDone&&Lucky_MainWinCheck.instance.IsResult())
		{
			changeEffectTime-=Time.deltaTime;
			if(changeEffectTime<=0)
			{
				Lucky_MainWinCheck.instance.ShowEffect();
				changeEffectTime=2f;
			}
		}

		if(autoEndTime!=-1)
		{
			if(autoEndTime>0)
				autoEndTime-=Time.deltaTime;
			else{
				EndScroll(0.5f);
				autoEndTime=-1;
			}
		}

	}
	//更改按钮形式.
	private void BtnGoChangeStop()
	{
		sp_go.color = Color.white;
		label_go.color = new Color(66/255f,88/255f,1/255f,1);
		label_go.effectStyle = UILabel.Effect.Outline;
		if (label_go.text == "开始寻宝"&&autoEndTime!=-1) {
			label_go.text = "停止";

		} else {
			label_go.text = "开始寻宝";
		}
		box_go.enabled = true;
	}
	//路线更改按钮事件.
	private void LuXianChange(GameObject go)
	{
		if(go.name=="btn_add")
		{
			if(luXianNum<9)
				luXianNum+=1;
		}else{
			if(luXianNum>0)
			luXianNum--;
		}
		label_luXian.text=luXianNum.ToString();
		label_huaFei.text=huaFeiNum.ToString()+"x"+luXianNum;
		label_zongHuaFei.text = (huaFeiNum*luXianNum).ToString();
	}
	//投注更改按钮事件.
	private void HuaFeiChange(GameObject go)
	{

//		if(go.name=="btn_add")
//		{
//			huaFeiNum+=data.unitBetting_int;
//			if(PlayerManager.instance.player.money.val< luXianNum*huaFeiNum)
//			{
//				PopMessage.Create("铜钱不足");
//				huaFeiNum-=data.unitBetting_int;
//			}
//		}else{
//			if(huaFeiNum>=data.unitBetting_int*2)
//				huaFeiNum-=data.unitBetting_int;
//		}
		label_huaFei.text=huaFeiNum.ToString()+"x"+luXianNum;
		label_zongHuaFei.text = (huaFeiNum*luXianNum).ToString();
	}
	private void ShowHelp(GameObject go)
	{
		scrollView.gameObject.SetActive(false);
		helpWin1.SetActive(false);
		helpWin2.SetActive(false);
		go.SetActive(true);
	}
	public void ShowMain()
	{
		scrollView.gameObject.SetActive(true);
	}
}
