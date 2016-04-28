using UnityEngine;
using System.Collections;

public class CheckEffectSizeM : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		int length	= transform.childCount;
		Transform tf;

		string str="";
		for(int i =0;i<length;i++)
		{
			yield return new WaitForFixedUpdate();
			tf = transform.GetChild(i);
			ParticleSystem[] pss = tf.GetComponentsInChildren<ParticleSystem>();
			if(pss.Length>10)
			{
				str +="prefab名称:"+tf.gameObject.name+","+"粒子数量:"+pss.Length+",详细粒子信息:\n";
				foreach(ParticleSystem ps in pss)
				{
					str+=",粒子名称:"+ps.name+"   ,粒子数量:"+ps.particleCount+"   ,粒子最大尺寸:"+ps.maxParticles+"   ,贴图名称:"+ps.renderer.material+"   ,贴图数据:"+ps.renderer.materials.Length+"\n";
				}
			}
		}
		Debug.Log(str);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
