using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 静态特效逻辑类[2/4/2015 Zmy]
/// </summary>
public class EffectStatic : EffectProperty 
{
    private float time = 0f;
    public override void OnUpdateState()
    {
        if (GetIsActivation() == false)
            return;
        
        time += Time.deltaTime;
        if (time > GetLiftTime())
        {
            time = 0f;
            SetActivation(false);
        }
    }

    public override void SetGameObject(GameObject obj)
    {
        base.SetGameObject(obj);

        SetActivation(true);
    }
    public void  ResetTime()
    {
        time = 0f;
    }
}
