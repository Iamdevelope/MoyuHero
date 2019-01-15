using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;

public class BossPassDataPack
{
    public const int END_OK = 1; // 成功
    public const int END_ERROR = 2; // 失败
    public const int END_ISKILL = 3; // BOSS被别人击杀
    public const int END_KILLBOSS = 4; // 击杀BOSS

    public int m_Result;
    public int m_BossID; // 值为1234，代表第几个boss
    public string m_BossKillName; // 击杀者名称
}

public class UI_WorldBossEndTips : UI_BaseWorldBossEndTips
{
    private class AwardItem : DynamicItem
    {
        private UniversalItemCell m_Cell;
        private AwardItem(){}

        public static AwardItem GenerateItem(Transform layout)
        { 
            if (layout == null)
                return null;
            AwardItem _item = new AwardItem();
            _item.m_Cell = UniversalItemCell.GenerateItem(layout);
            return _item;
        }

        public void SetItemInfo(int itemID, int itemCount)
        {
            m_Cell.InitByID(itemID,itemCount);
        }
    }
    private static readonly string Path = "UI_MsgBox/UI_WorldBossBox_0_4";
    public static string GetPath()
    {
        return Path;
    }
    private Transform m_AwardListLayout;
    private GameObject m_OriginalAwardItem;
    private WorldBossManager m_WorldBossManager;
    private BossPassDataPack m_BossPassDataPack;
    private FightState m_FightState;
    public override void InitUIData()
    {
        base.InitUIData();
        m_WorldBossManager = ObjectSelf.GetInstance().WorldBossMgr;
        m_FightState = FightControler.Inst.GetFightState();
        m_AwardListLayout = selfTransform.FindChild("AwardPanel/AwardList/Layout");
        m_OriginalAwardItem = selfTransform.FindChild("OriginalPanel/OriginalAwardItem").gameObject;

    }
    public override void InitUIView()
    {
        base.InitUIView();

        m_BossText.text = "It's only a Accident";
        m_DamageText.text = GameUtils.getString("legend_of_the_war_content6");
        m_DamagePointText.text = SceneObjectManager.GetInstance().WorldBossDamageSum.ToString();
        m_AwardText.text = GameUtils.getString("legend_of_the_war_content7");

        if (m_FightState == FightState.FightWin)
        {
            m_KillText.gameObject.SetActive(true);
            if (m_BossPassDataPack.m_Result == BossPassDataPack.END_KILLBOSS)
            {
                m_KillText.text = GameUtils.getString("legend_of_the_war_content3");
            }
            else if (m_BossPassDataPack.m_Result == BossPassDataPack.END_ISKILL)
            {
                m_KillText.text = string.Format(GameUtils.getString("legend_of_the_war_content4"), m_BossPassDataPack.m_BossKillName);
            }
        }
        else
        {
            m_KillText.gameObject.SetActive(false);
        }
        LoadAwardItem();
    }

    private void LoadAwardItem()
    {
        var _itemMap = m_WorldBossManager.m_DropItemMap;

        foreach(var data in _itemMap)
        {
            if (data.Value == 0)
                continue;

            AwardItem _item = AwardItem.GenerateItem(m_AwardListLayout);
            if (_item != null)
            {
                _item.SetItemInfo(data.Key,data.Value);
            }
        }
    }

    public void SetDataPack(BossPassDataPack package)
    {
        m_BossPassDataPack = package;
    }

    protected override void OnClickBackBtn()
    {
        DreamFaction.UI.Core.UI_HomeControler.NeedShowWorldBossPanel = true;
        SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
    }
}
