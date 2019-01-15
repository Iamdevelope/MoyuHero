using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using System.Collections.Generic;

public class UICommon_GodSoul : UICommon_GoldSoulBase, UICommonInterface
{

    protected Image icon = null;
    protected Image typeIcon = null;

    public override void InitUIData()
    {
        base.InitUIData();

        icon = selfTransform.FindChild("Panel/ItemInfo/Icon").GetComponent<Image>();
        typeIcon = selfTransform.FindChild("Panel/ItemInfo/Type/Image").GetComponent<Image>();
    }

    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();

        UICommonManager.Inst.RemoveUI(UICommonType.CommonGodSoul, this);
    }

    public void SetData(int tableId)
    {

    }
}
