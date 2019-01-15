using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;

public class UI_TeamHeroItem : CellItem
{
    public enum FormationState
    {
        Upstate,
        DownState,
        ChangeState,
        MoveState
    }
    private ObjectCard m_Card = null;
    private HeroTemplate m_HeroT = null;

    //UI控件
    private Text m_HeroTitleTxt = null;
    private Text m_HeroTypeTxt = null;
    private Text m_PowerTxt = null;
    private Image m_HeroTypeImg = null;
    private Button m_StateBtn = null;
    private Text m_StateBtnTxt = null;

    private Transform m_HeroCellItemTrans = null;
    private HeroCellItem m_HeroCellItem = null;

    private FormationState m_State;
    private GameObject m_YetUpStateOBJ = null;

    public override void InitUIData()
    {
        base.InitUIData();

        m_YetUpStateOBJ = selfTransform.FindChild("YetUpState").gameObject;
        m_HeroTitleTxt = selfTransform.FindChild("HeroTilteTxt").GetComponent<Text>();
        m_HeroTypeTxt = selfTransform.FindChild("HeroTypeTxt").GetComponent<Text>();
        m_PowerTxt = selfTransform.FindChild("PowerTxt").GetComponent<Text>();
        m_HeroTypeImg = selfTransform.FindChild("HeroTypeIcon").GetComponent<Image>();
        m_StateBtnTxt = selfTransform.FindChild("StateBtn/Text").GetComponent<Text>();

        m_HeroCellItemTrans = selfTransform.FindChild("HeroCellItem");
        if (m_HeroCellItemTrans.GetComponent<HeroCellItem>() == null)
            m_HeroCellItem = m_HeroCellItemTrans.gameObject.AddComponent<HeroCellItem>();
        else
            m_HeroCellItem = m_HeroCellItemTrans.GetComponent<HeroCellItem>();


        m_StateBtn = selfTransform.FindChild("StateBtn").GetComponent<Button>();
        m_StateBtn.onClick.AddListener(new UnityAction(onStateBtnClick));

    }

    /// <summary>
    /// 初始化英雄卡牌UI
    /// </summary>
    /// <param name="card"></param>
    public void InitHeroCardUI(ObjectCard card)
    {
        this.m_Card = card;
        this.m_HeroT = card.GetHeroRow();

        m_HeroCellItem.UpdateHeroShow(card);
        ShowStaticText();
        ShowPower();
        ShowBtnState();
    }

    /// <summary>
    /// 初始化05表文本
    /// </summary>
    private void InitStr()
    {

    }

    /// <summary>
    /// 显示静态文本UI Icon
    /// </summary>
    private void ShowStaticText()
    {
        m_HeroTitleTxt.text = string.Format(GameUtils.GetHeroNameFontColor(m_Card.GetHeroData().QualityLev), GameUtils.getString(m_HeroT.getTitleID())); 
        InterfaceControler.GetInst().ShowTypeIcon(m_HeroT, m_HeroTypeImg, m_HeroTypeTxt);
    }

    /// <summary>
    /// 战力显示
    /// </summary>
    private void ShowPower()
    {
        m_PowerTxt.text = m_Card.GetHeroData().FightVigor.ToString();
    }


    /// <summary>
    /// 显示StateBtn 文字状态
    /// </summary>
    private void ShowBtnState()
    {
        m_State = FormationState.Upstate;

        int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        if (nGroup < 0 && nGroup >= GlobalMembers.MAX_MATRIX_COUNT)
            return;

        if (UI_SelectHeroMgr.Inst.GetCard() == null)
        {
            for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
            {
                X_GUID pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i];
                //移动状态
                if (pMemberGuiD.GUID_value == m_Card.GetGuid().GUID_value)
                {
                    m_State = FormationState.MoveState;
                    break;
                }
            }
        }
        else
        {
            //下阵状态
            if (UI_SelectHeroMgr.Inst.GetCard().GetGuid().GUID_value == m_Card.GetGuid().GUID_value)
            {
                m_State = FormationState.DownState;
            }
            else
            {
                for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
                {
                    X_GUID pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i];
                    //移动状态
                    if (pMemberGuiD.GUID_value == m_Card.GetGuid().GUID_value)
                    {
                        m_State = FormationState.MoveState;
                        break;
                    }
                    else//更换状态
                    {
                        m_State = FormationState.ChangeState;
                    }
                }
            }
        }

        m_StateBtnTxt.text = StateBtnStr(m_State);

        m_StateBtn.interactable = true;
        m_StateBtn.GetComponent<Image>().color = Color.white;
/*        m_StateBtn.gameObject.SetActive(true);*/
        if (m_State == FormationState.MoveState || m_State == FormationState.DownState)
            m_YetUpStateOBJ.SetActive(true);
        else
            m_YetUpStateOBJ.SetActive(false);

        if (UI_SelectHeroMgr.Inst.SelectPos == 0)//选择位置是前排
        {
            if (UI_SelectHeroMgr.Inst.GetCard() != null && 
                UI_SelectHeroMgr.Inst.GetCard().GetHeroRow().getQosition() == 1)//该位置英雄为防御型
            {
                if (m_State == FormationState.MoveState)
                {
                    int count = UI_FormMgr.Inst.GetBackHeroGuids().Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (UI_FormMgr.Inst.GetBackHeroGuids().Contains(m_Card.GetGuid().GUID_value))//当前英雄站在后排
                        {
                            /*m_StateBtn.gameObject.SetActive(false);*/
                            m_StateBtn.interactable = false;
                            m_StateBtn.GetComponent<Image>().color = Color.gray;
                        }
                    }
                }
            }
        }
        else
        {
            if (m_HeroT.getQosition() == 1)
            {
               /* m_StateBtn.gameObject.SetActive(false);*/
                m_StateBtn.interactable = false;
                m_StateBtn.GetComponent<Image>().color = Color.gray;
            }
        }
    }

    /// <summary>
    /// 返回状态按钮文字
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    private string StateBtnStr(FormationState state)
    {
        switch (state)
        {
            case FormationState.Upstate:
                return "上 阵";
            case FormationState.DownState:
                return "下 阵";
            case FormationState.ChangeState:
                return "更 换";
            case FormationState.MoveState:
                return "移 动";
            default:
                return "";
        }

    }


    //状态按钮回调
    private void onStateBtnClick()
    {
        switch (m_State)
        {
//             case FormationState.Upstate:
//                 break;
//             case FormationState.ChangeState:                
//                 break;
//             case FormationState.MoveState:              
//                 break;
            case FormationState.DownState:
                UI_SelectHeroMgr.Inst.SendDownProtocol(ObjectSelf.GetInstance().Teams.GetDefaultGroup());
                UI_SelectHeroMgr.Inst.SetCard(null);
                break;
            default:
                UI_SelectHeroMgr.Inst.SendProtocol(ObjectSelf.GetInstance().Teams.GetDefaultGroup(), (int)m_Card.GetGuid().GUID_value);
                UI_SelectHeroMgr.Inst.SetCard(m_Card);
                break;
        }
    }


    void OnDestroy()
    {
        m_HeroTitleTxt = null;
        m_HeroTypeTxt = null;
        m_PowerTxt = null;
        m_HeroTypeImg = null;
        m_StateBtn = null;
        m_StateBtnTxt = null;
    }



}
