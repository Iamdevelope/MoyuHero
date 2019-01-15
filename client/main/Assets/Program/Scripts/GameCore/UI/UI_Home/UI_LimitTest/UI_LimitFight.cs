using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using UnityEngine.UI;
using DreamFaction.GameEventSystem;
using GNET;
using DreamFaction.UI.Core;

public class UI_LimitFight : UI_LimitFightBase
{
    private ObjectSelf m_Self;
    private GameConfig m_Config;
    private int m_CosBraveCount = 0;
    private int m_BaeaveCount = 0;
    public static bool isBuy = false;

    public override void InitUIData()
    {
        base.InitUIData();
        GameEventDispatcher.Inst.addEventListener(GameEventID.F_LimitBoutEnd,UpdateUIShow);
        GameEventDispatcher.Inst.addEventListener(GameEventID.F_LimitAddEnd,UpdateUIShow);
        GameEventDispatcher.Inst.addEventListener(GameEventID.F_LimitFightEnd, LimitFightEndClearing);
        GameEventDispatcher.Inst.addEventListener(GameEventID.F_EnemyOnDie, ShowBraveSimulateCount);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_Self = ObjectSelf.GetInstance();
        m_Config = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
        m_BraveNumTxt.text = m_Self.LimitFightMgr.m_DropNum.ToString();
        ShowCurLevel();
        ShowPowerSuplusCount();
        ShowCosBraveCount();
        ShowBraveCount();
        ShowYetCount();
        ShowAttriButeDes();
        ShowBuyBtnState();
    }

    /// <summary>
    /// 更新UI刷新显示
    /// </summary>
    private void UpdateUIShow()
    {
        ShowCurLevel();
        ShowPowerSuplusCount();
        ShowBraveCount();
        ShowCosBraveCount();
        ShowYetCount();
        ShowAttriButeDes();
        ShowBuyBtnState();
    }

    /// <summary>
    /// 显示当前关卡
    /// </summary>
    private void ShowCurLevel()
    {
        string _text = string.Format(GameUtils.getString("ultimatetrial_content1"), m_Self.LimitFightMgr.m_RoundNum);
        m_LevelNameTxt.text = _text;
    }
    /// <summary>
    /// 强者之约剩余显示
    /// </summary>
    private void ShowPowerSuplusCount()
    {
        if (m_Self.LimitFightMgr.m_Pact == -1)
        {
            m_PowerSuplusTxt.gameObject.SetActive(false);
            return;
        }

        int _temp = m_Config.getUltimatetrial_appointment_wave()[m_Self.LimitFightMgr.m_Pact] - m_Self.LimitFightMgr.m_RoundNum;
        if (_temp <= 0)
        {
            m_PowerSuplusTxt.text = GameUtils.getString("ultimatetrial_content33");
        }
        else
        {
            string _text = string.Format(GameUtils.getString("ultimatetrial_content2"), _temp);
            m_PowerSuplusTxt.text = _text;
        }
    }

    /// <summary>
    /// 显示勇者证明总数
    /// </summary>
    private void ShowBraveCount()
    {
        if (isBuy)
        {
            m_BaeaveCount = m_BaeaveCount - m_Config.getUltimatetrial_honestdiploma_cost();
            if (m_BaeaveCount < 0)
                m_BaeaveCount = 0;
            m_BraveNumTxt.text = m_BaeaveCount.ToString();
            isBuy = false;            
        }
        else
        {
            m_BraveNumTxt.text = m_Self.LimitFightMgr.m_DropNum.ToString();
            m_BaeaveCount = m_Self.LimitFightMgr.m_DropNum;
        }
        
    }
    /// <summary>
    /// 用于模拟显示极限试炼没杀死一个单位增长一个勇者证明显示
    /// </summary>
    private void ShowBraveSimulateCount()
    {
        m_BaeaveCount += 1;
        m_BraveNumTxt.text = m_BaeaveCount.ToString();
    }
    /// <summary>
    /// 消耗勇者证明的个数
    /// </summary>
    private void ShowCosBraveCount()
    {
        m_CosBraveCount = m_Config.getUltimatetrial_honestdiploma_cost();
        string _text = string.Format(GameUtils.getString("ultimatetrial_content3"), m_CosBraveCount);
        m_CosBraveTxt.text = _text;
    }
    /// <summary>
    /// 显示属性增加的次数  
    /// </summary>
    private void ShowYetCount()
    {
        m_AngerSuplusCountTxt.text = m_Self.LimitFightMgr.m_AttrAdd1.ToString();
        m_PhyDefSuplusCountTxt.text = m_Self.LimitFightMgr.m_AttrAdd2.ToString();
        m_PhyAttatkSuplusCountTxt.text = m_Self.LimitFightMgr.m_AttrAdd3.ToString();
        //m_ReAllBeingSuplusCountTxt.text = m_Self.LimitFightMgr.m_AttrAdd4.ToString();
    }
    /// <summary>
    /// 显示属性加成描述
    /// </summary>
    private void ShowAttriButeDes()
    {
        string _text1 = string.Format(GameUtils.getString("ultimatetrial_content7"), m_Config.getUltimatetrial_honestdiploma_num4() * 100);
        string _text2 = string.Format(GameUtils.getString("ultimatetrial_content5"), m_Config.getUltimatetrial_honestdiploma_num2() * 100);
        string _text3 = string.Format(GameUtils.getString("ultimatetrial_content6"), m_Config.getUltimatetrial_honestdiploma_num3() * 100);
        string _text4 = string.Format(GameUtils.getString("ultimatetrial_content4"), m_Config.getUltimatetrial_honestdiploma_num1() * 100);

        m_AngerDesTxt.text = _text1;
        m_PhyDefDesTxt.text = _text2;
        m_PhyAttackDesTxt.text = _text3;
        m_ReAllBeingDesText.text = _text4;
    
    }
    /// <summary>
    /// 显示按钮的状态
    /// </summary>
    private void ShowBuyBtnState()
    {
        if (m_Self.LimitFightMgr.m_DropNum < m_CosBraveCount)
        {
            SetBuyBtnState(false);
        }
        else
        {
            SetBuyBtnState(true);
        }
    }
    /// <summary>
    /// 设置按钮状态
    /// </summary>
    /// <param name="disbled">是否需要Disbled</param>
    private void SetBuyBtnState(bool disbled)
    {
        m_AngerBtn.interactable = disbled;
        m_PhyDefBtn.interactable = disbled;
        m_PhyAttackBtn.interactable = disbled;
        m_ReAllBeingBtn.interactable = disbled;
    }


    /// <summary>
    /// 购买怒气
    /// </summary>
    protected override void OnClickAngerBtn()
    {
        //Debug.Log("is OnClickAngerBtn !");
        SendMsg(1);
    }

    /// <summary>
    /// 购买物防
    /// </summary>
    protected override void OnClickPhyDefBtn()
    {
        SendMsg(2);
    }

    /// <summary>
    /// 股购买物攻
    /// </summary>
    protected override void OnClickPhyAttackBtn()
    {
        SendMsg(3);
    }

    /// <summary>
    /// 购买全体生命恢复
    /// </summary>
    protected override void OnClickReAllBeingBtn()
    {
        SendMsg(4);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="addNum">1怒气，2防御，3攻击，4全体恢复</param>
    private void SendMsg(int addNum)
    {
        CEndlessBuyadd _ceb = new CEndlessBuyadd();
        _ceb.addnum = addNum;
        IOControler.GetInstance().SendProtocol(_ceb);
    }


    /// <summary>
    /// 加载试炼结算框
    /// </summary>
    private void LimitFightEndClearing()
    {
        if (UI_FightControler.Inst != null)
        {
            UI_FightControler.Inst.AddUI(UI_TestClearingObj.UI_ResPath);
        }
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.F_LimitBoutEnd,UpdateUIShow);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.F_LimitAddEnd, UpdateUIShow);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.F_LimitFightEnd, LimitFightEndClearing);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.F_EnemyOnDie, ShowBraveSimulateCount);
    }
    
    

}
