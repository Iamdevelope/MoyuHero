using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using UnityEngine.Events;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using System.Collections.Generic;

public class UI_PowerPactManager : BaseUI 
{
    public static UI_PowerPactManager Init;
    private Button m_PowerFuCloseBtn;              //关闭按钮    
    private Text m_AllCosCountTxt;                 //全部钻石个数
    private Text m_PowerTliteTxt;                  //强者之约标题
    private Text m_PromptTxt;                      //提示文本
    private Text m_DesTxt1;                        //预约1描述
    private Text m_DesTxt2;                        //预约2描述
    private Text m_DesTxt3;                        //预约3描述
    private Button m_PactBtn1;                     //预约1按钮
    private Button m_PactBtn2;                     //预约2按钮
    private Button m_PactBtn3;                     //预约3按钮
    private Text m_CosCountTxt1;                   //预约1消耗数量
    private Text m_CosCountTxt2;                   //预约2消耗数量
    private Text m_CosCountTxt3;                   //预约3消耗数量
    private Text m_PactDayTxt1;                    //预约1关数
    private Text m_PactDayTxt2;                    //预约2关数
    private Text m_PactDayTxt3;                    //预约3关数
    //private GameObject m_PactStateObj1;            //预约1状态显示
    //private GameObject m_PactStateObj2;            //预约2状态显示
    //private GameObject m_PactStateObj3;            //预约3状态显示
    private Text m_PactStateTxt1;                  //预约1状态显示文本
    private Text m_PactStateTxt2;                  //预约2状态显示文本
    private Text m_PactStateTxt3;                  //预约3状态显示文本

    private GameConfig m_Config;
    private int[] m_PactLevel;                    //预约关数
    private int[] m_Reward;                       //预约圣灵之泉
    private int[] m_CosCounts;                    //消耗魔钻个数

    private GameObject item_1;
    private GameObject item_2;
    private GameObject item_3;

    public override void InitUIData()
    {
        base.InitUIData();
        Init = this;
        m_PowerFuCloseBtn = selfTransform.FindChild("TopPanel/CloseBtn").GetComponent<Button>();
        m_AllCosCountTxt = selfTransform.FindChild("TopPanel/CosIcon/CosCountTxt").GetComponent<Text>();
        m_PowerTliteTxt = selfTransform.FindChild("PowerTiliteTxt").GetComponent<Text>();
        m_PromptTxt = selfTransform.FindChild("HintObj/Bottom/Text").GetComponent<Text>();
        m_DesTxt1 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem1/DesTxt").GetComponent<Text>();
        m_DesTxt2 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem2/DesTxt").GetComponent<Text>();
        m_DesTxt3 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem3/DesTxt").GetComponent<Text>();
        m_PactBtn1 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem1/PactBtn").GetComponent<Button>();
        m_PactBtn2 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem2/PactBtn").GetComponent<Button>();
        m_PactBtn3 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem3/PactBtn").GetComponent<Button>();
        m_CosCountTxt1 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem1/PactBtn/CosNumTxt").GetComponent<Text>();
        m_CosCountTxt2 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem2/PactBtn/CosNumTxt").GetComponent<Text>();
        m_CosCountTxt3 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem3/PactBtn/CosNumTxt").GetComponent<Text>();
        m_PactDayTxt1 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem1/PactBtn/PactDayTxt").GetComponent<Text>();
        m_PactDayTxt2 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem2/PactBtn/PactDayTxt").GetComponent<Text>();
        m_PactDayTxt3 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem3/PactBtn/PactDayTxt").GetComponent<Text>();
        m_PactStateTxt1 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem1/PactState/PactStateTxt").GetComponent<Text>();
        m_PactStateTxt2 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem2/PactState/PactStateTxt").GetComponent<Text>();
        m_PactStateTxt3 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem3/PactState/PactStateTxt").GetComponent<Text>();
        item_1 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem1").gameObject;
        item_2 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem2").gameObject;
        item_3 = selfTransform.FindChild("ItemList/ListLayOut/PowerfulItem3").gameObject;

        m_PowerFuCloseBtn.onClick.AddListener(new UnityAction(OnPowerFuObjClose));
        m_PactBtn1.onClick.AddListener(new UnityAction(OnClickPactBtn1));
        m_PactBtn2.onClick.AddListener(new UnityAction(OnClickPactBtn2));
        m_PactBtn3.onClick.AddListener(new UnityAction(OnClickPactBtn3));

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Gold_Update, ShowDiament);
        GameEventDispatcher.Inst.addEventListener(GameEventID.F_LimitPactOk, JudegPactState);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        if (item_1.GetComponentInChildren<PowerFulItem>() == null)
            item_1.AddComponent<PowerFulItem>();
        if (item_2.GetComponentInChildren<PowerFulItem>() == null)
            item_2.AddComponent<PowerFulItem>();
        if (item_3.GetComponentInChildren<PowerFulItem>() == null)
            item_3.AddComponent<PowerFulItem>();
        m_Config = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
        m_PactLevel = m_Config.getUltimatetrial_appointment_wave();
        m_Reward = m_Config.getUltimatetrial_appointment_reward();
        m_CosCounts = m_Config.getUltimatetrial_appointment_cost();
        InitPowerPactUI();
        JudegPactState();

    }

    /// <summary>
    /// 控制按钮显示状态  是否置灰
    /// </summary>
    /// <param name="active"></param>
    private void SetBtnShowState(bool active)
    {
        GameUtils.SetBtnSpriteGrayState(m_PactBtn1, active);
        GameUtils.SetBtnSpriteGrayState(m_PactBtn2, active);
        GameUtils.SetBtnSpriteGrayState(m_PactBtn3, active);
    }


    /// <summary>
    /// 显示预约状态Img
    /// </summary>
    private void JudegPactState()
    {
        SetBtnShowState(false);
        if (ObjectSelf.GetInstance().LimitFightMgr.m_Pact == -1 
            && !ObjectSelf.GetInstance().LimitFightMgr.Activate)//没有预约试炼完成
        {
            SetBtnShowState(true);
        }

        if (ObjectSelf.GetInstance().LimitFightMgr.m_Pact == -1)//没有预约
            return;

        //已预约
        SetBtnShowState(true);
        if (ObjectSelf.GetInstance().LimitFightMgr.Activate)//没有试炼完成
        {
            string _text = GameUtils.getString("ultimatetrial_content39");
            switch (ObjectSelf.GetInstance().LimitFightMgr.m_Pact)
            {
                case 0:
                    m_PactStateTxt1.text = _text;
                    item_1.GetComponent<PowerFulItem>().SetPactState(PowerFulItem.PactState.Pacted,_text);
                    break;
                case 1:
                    m_PactStateTxt2.text = _text;
                    item_2.GetComponent<PowerFulItem>().SetPactState(PowerFulItem.PactState.Pacted, _text);
                    break;
                case 2:
                    m_PactStateTxt3.text = _text;
                    item_3.GetComponent<PowerFulItem>().SetPactState(PowerFulItem.PactState.Pacted, _text);
                    break;
            }
        }
        else
        {
            //if (ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum >= m_PactLevel[ObjectSelf.GetInstance().LimitFightMgr.m_Pact])
            if (ObjectSelf.GetInstance().LimitFightMgr.m_PactIspass == 1)//预约完成
            {
                string _text = GameUtils.getString("ultimatetrial_content9");
                switch (ObjectSelf.GetInstance().LimitFightMgr.m_Pact)
                {
                    case 0:
                        m_PactStateTxt1.text = _text;
                        item_1.GetComponent<PowerFulItem>().SetPactState(PowerFulItem.PactState.Complte, _text);
                        break;
                    case 1:
                        m_PactStateTxt2.text = _text;
                        item_2.GetComponent<PowerFulItem>().SetPactState(PowerFulItem.PactState.Complte, _text);
                        break;
                    case 2:
                        m_PactStateTxt3.text = _text;
                        item_3.GetComponent<PowerFulItem>().SetPactState(PowerFulItem.PactState.Complte, _text);
                        break;
                }
            }
            else//未完成
            {
                string _text = GameUtils.getString("ultimatetrial_content8");
                switch (ObjectSelf.GetInstance().LimitFightMgr.m_Pact)
                {
                    case 0:
                        m_PactStateTxt1.text = _text;
                        item_1.GetComponent<PowerFulItem>().SetPactState(PowerFulItem.PactState.UnComplet, _text);
                        break;
                    case 1:
                        m_PactStateTxt2.text = _text;
                        item_2.GetComponent<PowerFulItem>().SetPactState(PowerFulItem.PactState.UnComplet, _text);
                        break;
                    case 2:
                        m_PactStateTxt3.text = _text;
                        item_3.GetComponent<PowerFulItem>().SetPactState(PowerFulItem.PactState.UnComplet, _text);
                        break;
                }
            }
        }

    }


    /// <summary>
    /// 初始化UI视图
    /// </summary>
    private void InitPowerPactUI()
    {
        m_PowerTliteTxt.text = GameUtils.getString("ultimatetrial_content36");
        m_PromptTxt.text = GameUtils.getString("ultimatetrial_content42");
        ShowDiament();
        ShowPactItem();

    }
    /// <summary>
    /// 显示钻石
    /// </summary>
    private void ShowDiament()
    {
        m_AllCosCountTxt.text = ObjectSelf.GetInstance().Gold.ToString();
    }
    /// <summary>
    /// 显示强者预约30
    /// </summary>
    private void ShowPactItem()
    {
        string _text1 = string.Format(GameUtils.getString("ultimatetrial_content37"), m_PactLevel[0], m_PactLevel[0]);
        string[] text_1=  SpliteDescText(_text1);
        item_1.GetComponent<PowerFulItem>().SetDesText(text_1[0],m_Reward[0],text_1[1],text_1[2]);       
        m_PactDayTxt1.text = string.Format(GameUtils.getString("ultimatetrial_content54"), m_PactLevel[0]);

        string _text2 = string.Format(GameUtils.getString("ultimatetrial_content37"), m_PactLevel[1], m_PactLevel[1]);
        string[] text_2 = SpliteDescText(_text2);
        item_2.GetComponent<PowerFulItem>().SetDesText(text_2[0], m_Reward[1], text_2[1], text_2[2]);
        m_PactDayTxt2.text = string.Format(GameUtils.getString("ultimatetrial_content54"), m_PactLevel[1]);

        string _text3 = string.Format(GameUtils.getString("ultimatetrial_content37"), m_PactLevel[2], m_PactLevel[2]);
        string[] text_3 = SpliteDescText(_text3);
        item_3.GetComponent<PowerFulItem>().SetDesText(text_3[0], m_Reward[2], text_3[1], text_3[2]);
        m_PactDayTxt3.text = string.Format(GameUtils.getString("ultimatetrial_content54"), m_PactLevel[2]);

        if (ObjectSelf.GetInstance().LimitFightMgr.m_IsHalfCostPact == 1)
        {
            m_CosCountTxt1.text = (m_CosCounts[0] / 2).ToString();
            m_CosCountTxt2.text = (m_CosCounts[1] / 2).ToString();
            m_CosCountTxt3.text = (m_CosCounts[2] / 2).ToString();
        }
        else
        {
            m_CosCountTxt1.text = m_CosCounts[0].ToString();
            m_CosCountTxt2.text = m_CosCounts[1].ToString();
            m_CosCountTxt3.text = m_CosCounts[2].ToString();
        }
    }
    private string[] SpliteDescText(string text)
    {  
        string[] str=new string[3];
        str[0] = text.Split('#')[0];
        str[1] = text.Split('#')[1];
        str[2] = text.Split('#')[2];
        return str;
    }

    /// <summary>
    /// 强者预约关闭按钮
    /// </summary>
    private void OnPowerFuObjClose()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 30预约按钮
    /// </summary>
    private void OnClickPactBtn1()
    {
        SendPactMeg(0);
    }
    /// <summary>
    /// 50预约按钮
    /// </summary>
    private void OnClickPactBtn2()
    {
        SendPactMeg(1);
    }
    /// <summary>
    /// 70预约按钮
    /// </summary>
    private void OnClickPactBtn3()
    {
        SendPactMeg(2);
    }
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="id">预约的ID</param>
    private void SendPactMeg(int id)
    {
        if (ObjectSelf.GetInstance().LimitFightMgr.m_Pact == -1 && !ObjectSelf.GetInstance().LimitFightMgr.Activate)
        {
            string _text = GameUtils.getString("ultimatetrial_content41");
            InterfaceControler.GetInst().AddMsgBox(_text, transform);
            return;
        }

        if (ObjectSelf.GetInstance().LimitFightMgr.m_Pact == -1)//没有预
        {
            if (id == 0 && ObjectSelf.GetInstance().Gold < m_CosCounts[id])
            {
                InterfaceControler.GetInst().ShowGoldNotEnougth(transform);
            }
            else if (id == 1 && ObjectSelf.GetInstance().Gold < m_CosCounts[id])
            {
                InterfaceControler.GetInst().ShowGoldNotEnougth(transform);
            }
            else if (id == 2 && ObjectSelf.GetInstance().Gold < m_CosCounts[id])
            {
                InterfaceControler.GetInst().ShowGoldNotEnougth(transform);
            }
            else
            {
                CBuyPact _cbp = new CBuyPact();
                _cbp.pactid = id;
                IOControler.GetInstance().SendProtocol(_cbp);
            }
        }
        else
        {
            if (ObjectSelf.GetInstance().LimitFightMgr.Activate)//没有完成试炼
            {
                string _text = GameUtils.getString("ultimatetrial_content38");
                InterfaceControler.GetInst().AddMsgBox(_text, transform);
            }
            else
            {
                //预约完成
                if (ObjectSelf.GetInstance().LimitFightMgr.m_PactIspass == 1)
                {
                    string _text = GameUtils.getString("ultimatetrial_content41");
                    InterfaceControler.GetInst().AddMsgBox(_text, transform);
                }
            }
        }
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Gold_Update, ShowDiament);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.F_LimitPactOk, JudegPactState);
    }



}
public class PowerFulItem:BaseUI
{
    private Text text_top;  //顶部
    private Text text_rewardnum; //奖励数量
    private Text text_shengling; //圣灵
    private Text text_bottom;  //底部
    private GameObject m_pacted; //已经预约
    private GameObject m_complte; //已完成预约
    private GameObject m_uncomplte; //未完成预约
    private Text m_pactText;    //预约状态文本描述

    public enum PactState
    {
       Pacted,
       Complte,
       UnComplet,
    }
    public override void InitUIData()
    {
        base.InitUIData();
        text_top = transform.FindChild("Text(up)").GetComponent<Text>();
        text_rewardnum = transform.FindChild("rewardNum").GetComponent<Text>();
        text_shengling = transform.FindChild("Text(shengling)").GetComponent<Text>();
        text_bottom = transform.FindChild("Text(bottom)").GetComponent<Text>();
        m_pacted= transform.FindChild("PactState/Image(pacted)").gameObject;
        m_complte = transform.FindChild("PactState/Image(complte)").gameObject;
        m_uncomplte = transform.FindChild("PactState/Image(uncomplte)").gameObject;
        m_pactText = transform.FindChild("PactState/PactStateTxt").GetComponent<Text>();
        m_pacted.SetActive(false);
        m_complte.SetActive(false);
        m_uncomplte.SetActive(false);
        m_pactText.gameObject.SetActive(false);
    }
    public void SetDesText(string topStr,int rewardNum,string shenglingStr,string bottomStr)
    {
        text_top.text = topStr;
        text_rewardnum.text = rewardNum.ToString();
        text_shengling.text = shenglingStr;
        text_bottom.text = bottomStr;
    }

    public void SetPactState(PactState state,string text)
    {
        m_pactText.text = text;
        m_pactText.gameObject.SetActive(true);
        switch (state)
        {
            case PactState.Pacted:
                m_pacted.SetActive(true);
                m_complte.SetActive(false);
                m_uncomplte.SetActive(false);
                break;
            case PactState.Complte:
                m_pacted.SetActive(false);
                m_complte.SetActive(true);
                m_uncomplte.SetActive(false);
                break;
            case PactState.UnComplet:
                m_pacted.SetActive(false);
                m_complte.SetActive(false);
                m_uncomplte.SetActive(true);
                break;
            default:
                break;
        }
    }
}
