using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using DG.Tweening;
using GNET;
public class UI_GIFT : BaseUI
{
    public Image icon;
    public Text Name;
    public Text Num;
    public override void InitUIData()
    {
        base.InitUIData();
        icon = selfTransform.FindChild("icon").GetComponent<Image>();
        Name = selfTransform.FindChild("Name").GetComponent<Text>();
        Num = selfTransform.FindChild("Num").GetComponent<Text>();
    }

}
