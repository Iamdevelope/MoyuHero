using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;

public class ImageText :BaseUI
{
    public Image[] imgs;

    public override void InitUIData()
    {
        base.InitUIData();
    }

    public override void InitUIView()
    {
        base.InitUIView();
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();
    }

    void OnDestroy()
    {
        UIState = UIStateEnum.ReadyForClose;
    }
}
