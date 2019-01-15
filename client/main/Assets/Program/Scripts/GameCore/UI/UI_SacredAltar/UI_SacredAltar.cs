using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using System;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.UI;
using System.Text;
using DreamFaction.Utils;
using GNET;
using DreamFaction.LogSystem;
public class UI_SacredAltar : UI_SacredAltarManage
{
    public static string UI_ResPath = "UI_Home/UI_SacredAltar_2_2";
    public static UI_SacredAltar _instance;
    public List<int> m_FallItemList = new List<int>();
    public int m_IsDisconnect;//是否断开过
    public int m_isSacredAltar;
    public GameObject m_Rewards;
    private bool IsFallItemTipsShow;
    private float m_time;
    ObjectSelf m_info;
    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        m_info = ObjectSelf.GetInstance();
        int _TypeNum = m_info.ScaredAltarTypeNum;
        m_IsDisconnect = _TypeNum / 10;
        IsFallItemTipsShow = false;
        UpdateShowUI();

        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.AwakeUp(M_CapPos);

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_SacredAltarSuccend, FallItemTipsShow);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_SacredAltarTips, TipsShow);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_SacredAltarUIShow, UpdateShowUI);
    }

    protected void OnDestroy()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(M_CapPos);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_SacredAltarSuccend, FallItemTipsShow);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_SacredAltarTips, TipsShow);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_SacredAltarUIShow, UpdateShowUI);
    }
    public override void UpdateUIData()
    {
        base.UpdateUIData();
        if (IsFallItemTipsShow)
        {
            m_time += Time.deltaTime;
            if (m_time>=1f)
            {
                m_time = 0;
                IsFallItemTipsShow = false;
                FallItemShow();
               
            }
            
        }
    }

    /// <summary>
    /// 更新UI显示
    /// </summary>
    public void UpdateShowUI()
    {
        m_Text.text = GameUtils.getString("prayer_content3");
        ObjectSelf m_info = ObjectSelf.GetInstance();
        StringBuilder str = new StringBuilder();
        str.Append("<color=#ffff00>" + m_info.SacredAltarNum + "</color>");
        str.Append("/");
        str.Append( m_info.ScaredAltarNumMax);
        m_SacredAltarNum.text = str.ToString();
        int _TypeNum = m_info.ScaredAltarTypeNum;
        m_isSacredAltar = _TypeNum % 10;
        if (m_isSacredAltar == 1)
        {
            GameUtils.SetBtnSpriteGrayState(m_SacredAltarBtn, true);
        }
        else
        {
            GameUtils.SetBtnSpriteGrayState(m_SacredAltarBtn, false);
        }
    }
    /// <summary>
    /// 祈愿满是掉落物品的显示
    /// </summary>
    public void FallItemTipsShow()
    {
        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("prayer_bubble2"), MsgBoxGroup);
        IsFallItemTipsShow = true;
    }
    public void FallItemShow()
    {
        if (m_FallItemList.Count>1)
        {
            m_Rewards.SetActive(true);
        }
        else
        {
            InnerdropTemplate inner = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(m_FallItemList[0]);
            int itemid = inner.getObjectid() / 1000000;
            ShowItemPreviewUIHandler(itemid);
        }
    }

    public static void ShowItemPreviewUIHandler(int tableID)
    {
        EM_OBJECT_CLASS eoc = GameUtils.GetObjectClassById(tableID);
        switch (eoc)
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                ItemTemplate runeItemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                if (runeItemT == null)
                {
                    LogManager.LogError("item表格中缺少物品id=" + tableID);
                }
                UI_RuneInfo.SetShowRuneDate(runeItemT);
                UI_HomeControler.Inst.AddUI(UI_RuneInfo.UI_ResPath);
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                if (itemT == null)
                {
                    LogManager.LogError("item表格中缺少物品id=" + tableID);
                }
                UI_Item.SetItemTemplate(itemT);
                UI_HomeControler.Inst.AddUI(UI_Item.UI_ResPath);
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                ArtresourceTemplate artT = DataTemplate.GetInstance().GetArtResourceTemplate(tableID);
                if (artT == null)
                {
                    LogManager.LogError("ArtResource时装表格中缺少物品id=" + tableID);
                }
                UI_SkinPreviewMgr.SetShowArtTemplate(artT);
                UI_HomeControler.Inst.AddUI(UI_SkinPreviewMgr.UI_ResPath);
                UI_SkinPreviewMgr.inst.SetTitleText(GameUtils.getString("clone_window1"));
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(tableID);
                if (heroT == null)
                {
                    LogManager.LogError("hero表格中缺少物品id=" + tableID);
                }
                UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
                HeroInfoPop.inst.SetShowData(heroT);
                HeroInfoPop.inst.SetTitleText(GameUtils.getString("clone_window1"));
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                //资源类型点击无响应;
                break;
            default:
                LogManager.LogError("未处理的商城物品预览类型");
                break;
        }
    }
    /// <summary>
    /// 提示消息的弹窗
    /// </summary>
    public void TipsShow()
    {
        if(m_IsDisconnect==1)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("prayer_bubble3"), MsgBoxGroup);
        }
        else
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("prayer_bubble1"), MsgBoxGroup);
        }
    }
    /// <summary>
    /// 点击祈愿
    /// </summary>
    protected override void OnClickSacredAltarBtn()
    {
        base.OnClickSacredAltarBtn();
        if (m_isSacredAltar==1)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("prayer_bubble4"), MsgBoxGroup);
        }
        else
        {
            CGetQiYuan cSacredAltar = new CGetQiYuan();
            IOControler.GetInstance().SendProtocol(cSacredAltar);
        }
    }
    /// <summary>
    /// 返回按钮
    /// </summary>
    protected override void OnClickBackBtn()
    {
        base.OnClickBackBtn();
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

}
