using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.GameEventSystem;

/// <summary>
/// 记录当前对象所在界面
/// </summary>
public enum CurUI
{ 
    Reparto,
    SelectHero
}
public class UI_TeamBtnItem : BaseUI
{
    private Button m_AddBtn = null;
    private Button m_HeroBtn = null;
    private GameObject m_SelectBg = null;
    private GameObject m_HeroCellItemOBJ = null;
    private HeroCellItem m_HeroCellItem = null;

    private ObjectCard m_Card = null;
    public int m_CurPos = -1;                                                 //当前选择的位置0是前排 1是后排
    public int m_CurNo = -1;
    private CurUI m_CurUI = CurUI.Reparto;                                       //当前所在界面

    public override void InitUIData()
    {
        base.InitUIData();

        m_SelectBg = selfTransform.FindChild("SelectBg").gameObject;
        m_HeroCellItemOBJ = selfTransform.FindChild("HeroCellItem").gameObject;
        m_HeroCellItem = m_HeroCellItemOBJ.GetComponent<HeroCellItem>();
        m_HeroBtn = m_HeroCellItemOBJ.GetComponent<Button>();
        m_HeroBtn.onClick.AddListener(onHeroBtnClick);
        m_AddBtn = selfTransform.FindChild("btn").GetComponent<Button>();
        m_AddBtn.onClick.AddListener(onAddBtnClick);
    }


    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="card"></param>
    public void InitData(ObjectCard card, int selectNo, CurUI curUI)
    {
        m_CurUI = curUI;

        if (card != null)
        {
            m_Card = card;

            m_AddBtn.gameObject.SetActive(false);
            m_HeroCellItemOBJ.SetActive(true);

            m_HeroCellItem.UpdateHeroShow(card);
        }
        else
        {
            m_AddBtn.gameObject.SetActive(true);
            m_HeroCellItemOBJ.SetActive(false);
        }

        SetSelectState(selectNo == m_CurNo);
    }

    /// <summary>
    /// 设置是否是选中状态
    /// </summary>
    /// <param name="active"></param>
    public void SetSelectState(bool active)
    {
        m_SelectBg.SetActive(active);
    }

    /// <summary>
    /// 空位加号按钮回调
    /// </summary>
    private void onAddBtnClick()
    {
        if (m_CurUI == CurUI.Reparto)
        {
            UI_RepartoMgr.Inst.SetSelectHeoData(m_Card, m_CurPos, m_CurNo);
            UI_RepartoMgr.Inst.onChangeHeroBtnClick();
        }
        else
        {
            UI_SelectHeroMgr.Inst.SetSelectHeoData(m_Card, m_CurPos, m_CurNo);
            UI_SelectHeroMgr.Inst.UpdateUIShow();
        }
    }

    /// <summary>
    /// 英雄Item按钮回调
    /// </summary>
    private void onHeroBtnClick()
    {
        if (m_CurUI == CurUI.Reparto)
        {
            UI_RepartoMgr.Inst.SetSelectHeoData(m_Card, m_CurPos, m_CurNo);
            UI_RepartoMgr.Inst.UpdateUIShow();
        }
        else
        {
            UI_SelectHeroMgr.Inst.SetSelectHeoData(m_Card, m_CurPos, m_CurNo);
            UI_SelectHeroMgr.Inst.UpdateUIShow();
/*            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Formation_Update);*/
        }
    }

    
}

