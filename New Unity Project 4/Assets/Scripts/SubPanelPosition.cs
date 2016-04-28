using UnityEngine;
using System.Collections;

public class SubPanelPosition : MonoBehaviour {
	//horizontal表示水平滑动；vertical表示垂直滑动。
	public enum ScreenDirection
	{
		horizontal,
		vertical
	}
	public ScreenDirection screenDirection = ScreenDirection.vertical;
	private Transform parent;
	private Transform child;
	private float ScaleSize;
	private float rateX;
	private float rateY;
	UIPanel PanelScript;
	IEnumerator Start()
	{
		parent = transform.parent;
		child = transform.GetChild(0);
		PanelScript = transform.GetComponent<UIPanel>();
		yield return new WaitForSeconds(0.1f);
		SetPanel();
	}
	void SetPanel()
	{      
		transform.parent = null;
		child.parent = null;
		
		
		if(screenDirection == ScreenDirection.vertical)
		{
			ScaleSize = transform.localScale.y;
			rateX = ScaleSize/transform.localScale.x;
			rateY = 1;
		}
		else if(screenDirection == ScreenDirection.horizontal)
		{
			ScaleSize = transform.localScale.x;
			rateX = 1;
			rateY = ScaleSize/transform.localScale.y;
		}
		
		transform.localScale = new Vector4(ScaleSize,ScaleSize,ScaleSize,ScaleSize);   
		transform.parent = parent;
		child.parent = transform;
        PanelScript.baseClipRegion = new Vector4(PanelScript.baseClipRegion.x, PanelScript.baseClipRegion.y, PanelScript.baseClipRegion.z / transform.localScale.x, PanelScript.baseClipRegion.w / transform.localScale.y);
	}
	public static void AddSelf(GameObject go,ScreenDirection sd = ScreenDirection.vertical)
	{
		SubPanelPosition spp = go.GetComponent<SubPanelPosition>();
		if(spp==null)
			spp = go.AddComponent<SubPanelPosition>();
		spp.screenDirection = sd;
	}
}