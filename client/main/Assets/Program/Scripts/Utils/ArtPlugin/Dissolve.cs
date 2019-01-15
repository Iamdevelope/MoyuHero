using UnityEngine;
using System.Collections;

public class Dissolve : MonoBehaviour
{

    public Material dissolveMat;         //溶解贴图
    public float DiffuseInsienty;        //(0, 3)
    public float fadeSpeed = 2f;         //消散速度
    float blendFadeValue = 0f;           
    float insientyFadeValue = 0.3f;
    public float delayTime = 2.0f;       //延迟时间
    float curTime = 0.0f;                //当前时间

    public bool isPlaying = false;

    #region Main Methods
    void Start()
    {
        if (dissolveMat && dissolveMat.HasProperty("_BlendAmount"))
        {
            dissolveMat.SetFloat("_BlendAmount", 0.0f);
            dissolveMat.SetFloat("_DiffuseInsienty", 0.0f);
        }    
    }

    // Update is called once per frame
	void Update () 
    {       
        if(!isPlaying)
        {
            return;
        }

        curTime += Time.deltaTime;
        if(curTime > delayTime)
        {
            if (dissolveMat && dissolveMat.HasProperty("_BlendAmount"))
            {
                blendFadeValue = Mathf.Lerp(blendFadeValue, 2f, Time.deltaTime * fadeSpeed);

                insientyFadeValue = Mathf.Lerp(insientyFadeValue, 1.3f, Time.deltaTime * fadeSpeed);

                dissolveMat.SetFloat("_BlendAmount", blendFadeValue);

                dissolveMat.SetFloat("_DiffuseInsienty", insientyFadeValue);
            }
        }
    }
    #endregion
}
