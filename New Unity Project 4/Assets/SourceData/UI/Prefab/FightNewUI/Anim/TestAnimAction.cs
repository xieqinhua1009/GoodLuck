using UnityEngine;
using System.Collections;

public class TestAnimAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	Animator am = this.GetComponent<Animator>();
		am.CrossFade("right",0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
