using UnityEngine;
using System.Collections;

public class Lucky_View_BigSmall : MonoBehaviour {
	private enum BSEnum{
		Small=6,
		Sum=7,
		Big=12,
	}
	private GameObject btn_add;
	private GameObject btn_sub;
	private GameObject btn_small;
	private GameObject btn_sum;
	private GameObject btn_big;
	private GameObject btn_goBack;
	private GameObject btn_close;
	private UILabel label_JIangLi;
	private Lucky_BS_Saizi Sp_saizi1;
	private Lucky_BS_Saizi Sp_saizi2;
	private UISprite rect;

	private int mulNum =1;
	private int goldNum=99999;
	private BSEnum currentType;
	private float endTime=-1;

	// Use this for initialization
	void Start () {
		btn_add = this.transform.FindChild("bottomR/btn_add").gameObject;
		btn_sub = this.transform.FindChild("bottomR/btn_sub").gameObject;
		btn_small = this.transform.FindChild("bottomR/btn_small").gameObject;
		btn_sum = this.transform.FindChild("bottomR/btn_sum").gameObject;
		btn_big = this.transform.FindChild("bottomR/btn_big").gameObject;
		btn_goBack = this.transform.FindChild("bottomR/btn_goBack").gameObject;
		btn_close = this.transform.FindChild("UI_CloseBtn").gameObject;
		label_JIangLi = this.transform.FindChild("bottomR/Label_JiangLi").GetComponent<UILabel>();
		Sp_saizi1 = this.transform.FindChild("Sp_saizi1").gameObject.AddComponent<Lucky_BS_Saizi>();
		Sp_saizi2 = this.transform.FindChild("Sp_saizi2").gameObject.AddComponent<Lucky_BS_Saizi>();
		rect = this.transform.FindChild("bottomR/rect").GetComponent<UISprite>();

		InitEvent();
		InitData();
	}
	void Update () {
		if(endTime!=-1)
		{
			if(endTime>0)
				endTime-=Time.deltaTime;
			else{
				End();
				endTime=-1;
			}
		}
	}
	private void InitEvent()
	{
		UIEventListener.Get(btn_add).onClick = ClickHandle;
		UIEventListener.Get(btn_sub).onClick = ClickHandle;
		UIEventListener.Get(btn_small).onClick = ClickHandle;
		UIEventListener.Get(btn_sum).onClick = ClickHandle;
		UIEventListener.Get(btn_big).onClick = ClickHandle;
		UIEventListener.Get(btn_goBack).onClick = ClickHandle;
		UIEventListener.Get(btn_close).onClick = ClickHandle;


	}
	private void InitData()
	{
		label_JIangLi.text = goldNum.ToString();
//		foreach(KTaiSai_base kt in KConfigFileManager.Instance.KTaiSaibase.getAllData().Values)
//		{
//			if(kt.maxLv_int>PlayerManager.instance.player.level_int)
//			{
//				data = kt;
//				return;
//			}
//		}
	}

	private void ClickHandle(GameObject go)
	{
		switch(go.name)
		{
		case "btn_add":
			mulNum++;
			SetMul();
			break;
		case "btn_sub":
			if(mulNum>1)
				mulNum--;
			SetMul();
			break;
		case "btn_small":
			currentType = BSEnum.Small;
			Begin();
			break;
		case "btn_sum":
			currentType = BSEnum.Sum;
			Begin();
			break;
		case "btn_big":
			currentType = BSEnum.Big;
			Begin();
			break;
		case "btn_goBack":
			Lucky_Controller.Instance().GetView().ShowMain();
			break;
		case "UI_CloseBtn":
			Lucky_Controller.Instance().GetView().ShowMain();
			break;
		}
	}
	private void Begin()
	{
		SetBtnEnable(false);
		Sp_saizi1.Begin();
		Sp_saizi2.Begin();
		endTime=3f;
		rect.gameObject.SetActive(false);
	}
	private void End()
	{
		Sp_saizi1.End(NGUITools.RandomRange(1,6));
		Sp_saizi2.End(NGUITools.RandomRange(1,6));
		SetBtnEnable(true);
		int sum = Sp_saizi1.point+Sp_saizi2.point;
		if(sum<7)
		{
			Debug.Log("结果：小"+sum +"  是否同点:"+(Sp_saizi1.point==Sp_saizi2.point));
			rect.transform.localPosition = new Vector3(-143,133,0);
			rect.width = 136;
			rect.height = 116;
		}else if(sum==7)
		{
			Debug.Log("结果：和"+sum);
			rect.transform.localPosition = new Vector3(1,133,0);
			rect.width = 114;
			rect.height = 96;
		}else{
			Debug.Log("结果：大"+sum +"  是否同点:"+(Sp_saizi1.point==Sp_saizi2.point));
			rect.transform.localPosition = new Vector3(140,133,0);
			rect.width = 136;
			rect.height = 116;
		}
		rect.gameObject.SetActive(true);
	}

	private void SetBtnEnable(bool value)
	{
		btn_add.GetComponent<BoxCollider>().enabled=value;
		btn_sub.GetComponent<BoxCollider>().enabled=value;
		btn_small.GetComponent<BoxCollider>().enabled=value;
		btn_sum.GetComponent<BoxCollider>().enabled=value;
		btn_big.GetComponent<BoxCollider>().enabled=value;
		btn_goBack.GetComponent<BoxCollider>().enabled=value;
		btn_close.GetComponent<BoxCollider>().enabled=value;
	}

	private void SetMul()
	{
//		if(data.maxCoin_int<goldNum*mulNum)
//		{
//			mulNum--;
//			PopMessage.Create("已超过最大额度");
//		}
		if(mulNum<=1)
			label_JIangLi.text = goldNum.ToString();
		else
			label_JIangLi.text = (goldNum*mulNum) +"(x"+mulNum+")";

	}
	

}
