using UnityEngine;
using System.Collections;

public class Lucky_View {

	private Lucky_View_Main main;
	private Lucky_View_BigSmall biss;
	private Lucky_View_Corona corona;


	public void HideAll()
	{
		biss.gameObject.SetActive(false);
		main.gameObject.SetActive(false);
		corona.gameObject.SetActive(false);
	}
	public void ShowCorona()
	{
		HideAll();
		corona.gameObject.SetActive(true);
	}
	public void ShowMain()
	{
		HideAll();
		main.gameObject.SetActive(true);
	}
	public void ShowBiss()
	{
		HideAll();
		biss.gameObject.SetActive(true);
	}

}
