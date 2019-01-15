using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UI_EffectStatic :EffectProperty 
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
    public void ResetTime()
    {
        time = 0f;
    }
	
}
