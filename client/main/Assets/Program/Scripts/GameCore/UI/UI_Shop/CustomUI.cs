using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;

public class CustomUI : BaseUI
{

    /// <summary>
    /// 是否唤醒跑马灯在界面InitUIView时候;
    /// </summary>
    protected string captionPath = string.Empty;
    private Transform captionParent = null;
    
    public override void InitUIView()
    {
        base.InitUIView();

        if (!string.IsNullOrEmpty(captionPath))
        {
            UI_CaptionManager cap = UI_CaptionManager.GetInstance();
            if (cap != null)
            {
                captionParent = selfTransform.FindChild(captionPath);

                if (captionParent != null) cap.AwakeUp(captionParent);
            }
        }
    }


    public void OnDestroy()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
        {
            if (captionParent != null) cap.Release(captionParent);
        }
        captionParent = null;
    }
}
