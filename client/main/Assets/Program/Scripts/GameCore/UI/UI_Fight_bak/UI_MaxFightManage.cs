using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DG.Tweening;
using UnityEngine.Events;
using DreamFaction.Utils;
using GNET;
public class UI_MaxFightManage : BaseUI 
{
    private Text powerBuyNum;                                                            //剩余购买次数
    private Text needGold;                                                               //购买所需钻石
    private Text allGold;                                                                //所持有的钻石
    BattleStage list = null;
    private Transform MsgBoxGroup;     
    public static string UI_ResPath = "UI_Home/UI_MaxFight_1_3";
    private int needGoldNum;
    ObjectSelf info=null;
    StageData data;
    public override void InitUIData()
    {
        base.InitUIData();
        MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
        selfTransform.FindChild("ContinueButton").GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAddFightNum));
        selfTransform.FindChild("ReturnButton").GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(OnCloseFightBuy));
        powerBuyNum =selfTransform.FindChild("BuyNum").GetComponent<Text>();
        needGold = selfTransform.FindChild("NeedGold/Text").GetComponent<Text>();
        allGold = selfTransform.FindChild("AllGold/Text").GetComponent<Text>();
        info = ObjectSelf.GetInstance();
        
        //if (info.GetIsPrompt())
        //{
        //    list = info.BattleStageData.m_BattleStageList[1001];
        //}
        //else
        //{
        //    if (info.BattleStageData.m_BattleStageList.ContainsKey(info.GetCurChapterID()))
        //    {
        //        list = info.BattleStageData.m_BattleStageList[info.GetCurChapterID()];
        //    }

        //}

        BattleStage bs = StageModule.GetPlayerBattleStageInfo();

        if (bs != null)
        {
            list = bs;
        }

        data = list == null ? null : list.GetStageData(info.CurStageID);
        //VipTemplate vip = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(info.VipLevel);
        //StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(info.CurStageID);
        VipTemplate vip = DataTemplate.GetInstance().GetVipTemplateById(info.VipLevel);
        StageTemplate stageinfo = StageModule.GetStageTemplateById(info.CurStageID);
        int[] needGoldList = stageinfo.m_resetCost;
        if (needGoldList.Length > 0)
        {
            if (vip.getStageResetBuyTimes() > 0)
            {

                if (data.m_BuyBattleNum == vip.getStageResetBuyTimes())
                {
                    needGoldNum = needGoldList[needGoldList.Length - 1];
                }
                else
                {
                    if (needGoldList.Length < data.m_BuyBattleNum)
                    {
                        needGoldNum= needGoldList[needGoldList.Length - 1];
                        
                    }
                    else
                    {
                        needGoldNum = needGoldList[data.m_BuyBattleNum];
                    }
                }

            }
            else
            {
                needGoldNum = needGoldList[0];
            }
        }
        powerBuyNum.text = (vip.getStageResetBuyTimes() - data.m_BuyBattleNum).ToString();
        needGold.text =needGoldNum.ToString();
        allGold.text = info.Gold.ToString();
    }

    public void OnClickAddFightNum()
    {
        //VipTemplate vip = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(info.VipLevel);
        VipTemplate vip = DataTemplate.GetInstance().GetVipTemplateById(info.VipLevel);
        if (vip.getStageResetBuyTimes() > 0)
        {

            if (data.m_BuyBattleNum == vip.getStageResetBuyTimes())
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_stagepurchase_tip1"), MsgBoxGroup);
            }
            else
            {
                if (needGoldNum <=info.Gold)
                {
                    CBuyStateBattleNum cbuy = new CBuyStateBattleNum();
                    cbuy.buytype = 2;
                    cbuy.battleid = info.CurStageID;
                    IOControler.GetInstance().SendProtocol(cbuy);
                    if (UI_HomeControler.Inst!=null)
                    {
                        UI_HomeControler.Inst.ReMoveUI(UI_MaxFightManage.UI_ResPath);
                    }
                    else
                    {
                        UI_FightControler.Inst.ReMoveUI(UI_MaxFightManage.UI_ResPath);
                    }
                }
                else
                {
                    InterfaceControler.GetInst().ShowGoldNotEnougth(this.transform);
                }
            }

        }
        else
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("fight_stagepurchase_tip1"), MsgBoxGroup);
        }
    }

    private void OnCloseFightBuy()
    {
        if (UI_HomeControler.Inst!=null)
        {
            UI_HomeControler.Inst.ReMoveUI(UI_MaxFightManage.UI_ResPath);
        }
        else
        {
            UI_FightControler.Inst.ReMoveUI(UI_MaxFightManage.UI_ResPath);
        }
    }
}
