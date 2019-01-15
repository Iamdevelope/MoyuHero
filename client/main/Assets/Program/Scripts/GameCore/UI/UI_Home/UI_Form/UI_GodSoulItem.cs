using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.UI;
using DreamFaction.Utils;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;

public class UI_GodSoulItem : CellItem
{
    private Image m_GodSoulImg = null;
    private Text m_GodSoulNameTxt = null;
    private Button m_StateBth = null;
    private ItemTemplate m_Item = null;
    private Text m_StateTxt = null;

    public override void InitUIData()
    {
        base.InitUIData();
        m_GodSoulImg = selfTransform.FindChild("GodSoulIcon").GetComponent<Image>();
        m_GodSoulNameTxt = selfTransform.FindChild("GodSoulNameTxt").GetComponent<Text>();
        m_StateTxt = selfTransform.FindChild("StateBtn/Text").GetComponent<Text>();

        m_StateBth = selfTransform.FindChild("StateBtn").GetComponent<Button>();
        m_StateBth.onClick.AddListener(new UnityAction(onStateBtnClick));

    }

    public override void InitUIView()
    {
        base.InitUIView();

        if (UI_GodSoulMgr.Inst.GetSelectItem() == null)
        {
            m_StateTxt.text = "添 加";
        }
        else
        {
            m_StateTxt.text = "更 换";
        }
    }


    public void InitData(ItemTemplate item)
    {
        m_Item = item;

        m_GodSoulImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
        m_GodSoulNameTxt.text = GameUtils.getString(item.getName());
    }

    private void onStateBtnClick()
    {
        SendProtocol(m_Item.getId());
        UI_GodSoulMgr.Inst.onClose();
        UI_FormMgr.Inst.SetGodSoulID(m_Item.getId());
/*        UI_HomeControler.Inst.AddUI(UI_FormMgr.UI_ResPath);*/
    }

//     private void onOpenForm()
//     {
//         UI_GodSoulMgr.Inst.onClose();
//         UI_FormMgr.Inst.SetGodSoulID(m_Item.getId());
//     }

    public void SendProtocol(/*, int FormationNum*/ int herokey)
    {
        CAddTroop battle = new CAddTroop();
        battle.trooptype = ObjectSelf.GetInstance().Teams.GetFormationType();
        battle.herokey = herokey;
        battle.locationid = UI_GodSoulMgr.Inst.GetSelectNo();
        IOControler.GetInstance().SendProtocol(battle);
    }


}
