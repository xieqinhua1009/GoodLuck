using UnityEngine;
using System.Collections;

public class Lucky_View_MainHelpwin1 : MonoBehaviour {

	public Lucky_View_Main main;
	void Start () {
		GameObject go = this.transform.FindChild("UI_CloseBtn").gameObject;
		UIEventListener.Get(go).onClick = OnClose;
	}
	void OnClose(GameObject go)
	{
		this.gameObject.SetActive(false);
		main.ShowMain();
	}
}
	