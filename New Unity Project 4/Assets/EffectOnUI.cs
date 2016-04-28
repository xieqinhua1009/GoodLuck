using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class EffectOnUI : MonoBehaviour
{
    public int renderQueue = 4000;
    [SerializeField]
    bool runOnlyOnce = true;
    void Start() //在Start里面要执行一次，要不然可能材质的渲染深度和Inpector上显示的会不一样
    {
        Debug.Log("queue   " + renderer.material.renderQueue);
        ResetRenderQueue();
       
    }
    void Update()
    {
        if (runOnlyOnce == true)
        {
            ResetRenderQueue();
        }
     
    }
    

    void ResetRenderQueue() {
       // if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
          if (Application.isPlaying) {  
            if (renderer != null && renderer.material != null) {
                renderer.material.renderQueue = renderQueue;
               
            }
          }
          else {  //编辑状态用sharedMaterial属性的话可能会造成内存泄漏，所有这个设置要判断状态
            if (renderer != null && renderer.sharedMaterial != null) {
                // print("3000==" + renderQueue);
                renderer.sharedMaterial.renderQueue = renderQueue;
            }
        }

        runOnlyOnce = false;
    }
}
