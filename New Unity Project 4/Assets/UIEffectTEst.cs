using UnityEngine;
using System.Collections;

public class UIEffectTEst : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash("delay",0.6f,"y", gameObject.transform.position.y+1.8f, "time", 1.5f,"easetype", iTween.EaseType.easeInQuint));
		iTween.ScaleTo(gameObject,iTween.Hash("delay",0.1f, "time", 1.5f,"x",1,"y",1,"easetype",iTween.EaseType.easeOutElastic));//iTween.EaseType.easeOutCirc));
		iTween.ScaleTo(gameObject,iTween.Hash("delay",2.1f, "time", 0.8f,"x",0f,"y",0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
