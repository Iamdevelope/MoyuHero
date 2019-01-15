using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.UI;
public class UI_Item : BaseUI
{
    public static UI_Item _instance;
    public Text titleName;
    public Text mName;
    public Image mIcon;
    public Text mDes;
    public Button mCloseBtn;
    public static string UI_ResPath = "UI_Home/UI_Item_1_3";
    private static ItemTemplate item;
    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        mCloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnCloseBtn));
        if (UI_SacredAltar._instance!=null)
        {
            titleName.text = GameUtils.getString("clone_window1");
        }
        else
        {
            titleName.text = GameUtils.getString("item_info_form_title");
        }
        UpdateShow();

    }
	
    public static void SetItemTemplate(ItemTemplate it)
    {
        item=it;
    }

    public void UpdateShow()
    {
        if (item == null)
        {
            return;
        }
        else
        {
            mName.text = GameUtils.getString( item.getName());
            mIcon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
            mDes.text = GameUtils.getString(item.getDes());
        }
    }

    public void OnCloseBtn()
    {
        if (UI_HomeControler.Inst == null)
        {
            UI_FightControler.Inst.ReMoveUI("UI_Home/UI_Item_1_3");
        }
        else
        {
            UI_HomeControler.Inst.ReMoveUI("UI_Home/UI_Item_1_3");
        }
    }

}
