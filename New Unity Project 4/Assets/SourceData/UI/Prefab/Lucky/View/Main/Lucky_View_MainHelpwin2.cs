using UnityEngine;
using System.Collections;

public class Lucky_View_MainHelpwin2 : MonoBehaviour {

	public Lucky_View_Main main;
	void Start () {
		GameObject go = this.transform.FindChild("UI_CloseBtn").gameObject;
		UIEventListener.Get(go).onClick = OnClose;

		initData();
	}
	void OnClose(GameObject go)
	{
		this.gameObject.SetActive(false);
		main.ShowMain();
	}

	private void initData()
	{

//		for(int i=1;i<10;i++)
//		{
//			KSlotAward_base data = KConfigFileManager.Instance.KSlotAwardbase.getData("1"+(i-1).ToString());
//			this.transform.FindChild("Label_all_"+i).GetComponent<UILabel>().text = "x"+data.oddsFifteen_int.ToString();
//			if(i<9)
//			{
//				string str = "5连[ffff00]"+data.oddsFive_int.ToString()+"倍[-]\n";
//				str+="4连[ffff00]"+data.oddsFour_int.ToString()+"倍[-]\n";
//				str+="3连[ffff00]"+data.oddsTree_int.ToString()+"倍[-]\n";
//				this.transform.FindChild("Label_one_"+i).GetComponent<UILabel>().text = str;
//			}
//
//		}
//		KSlotAward_base data1 = KConfigFileManager.Instance.KSlotAwardbase.getData("22");
//		KSlotAward_base data2 = KConfigFileManager.Instance.KSlotAwardbase.getData("23");
//		this.transform.FindChild("Label_allOther_123").GetComponent<UILabel>().text = "x"+data1.oddsFifteen_int.ToString();
//		this.transform.FindChild("Label_allOther_456").GetComponent<UILabel>().text = "x"+data2.oddsFifteen_int.ToString();

	}
}
