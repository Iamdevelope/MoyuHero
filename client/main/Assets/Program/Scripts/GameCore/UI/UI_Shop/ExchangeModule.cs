using UnityEngine;
using System.Collections;
using GNET;
using System;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using DreamFaction.UI.Core;

public class ExchangeModule
{
    public static readonly string Money_Str = "RMB";

    public static void ChargeMoney(int exchangeTableId, int count = 1)
    {
        CRequestExchangeBill bill = new CRequestExchangeBill();
        bill.goodid = exchangeTableId;
        bill.goodnum = count;

        IOControler.GetInstance().SendProtocol(bill);
    }

    public static TimeSpan GetMonthCardToEnd(int monthCardId)
    {
        Monthcard mc = ObjectSelf.GetInstance().GetMontCardInfoById(monthCardId);
        if (mc == null)
        {
            return TimeSpan.Zero;
        }

        DateTime dt = TimeUtils.ConverMillionSecToDateTime(mc.overtime, ObjectSelf.GetInstance().ServerTimeZone);

        return dt - ObjectSelf.GetInstance().ServerDateTime;
    }

    public static void OnBuyMonthCardSucess(int monthCardId)
    {
        MonthcardTemplate mt = DataTemplate.GetInstance().GetMonthCardTemplateByID(monthCardId);

        int gold = mt.getDailygold();
        int diamond = mt.getDailydiamond();
        
        if ((gold > 0) && (diamond > 0))
        {
            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("monthcard_bubble1"), diamond, gold), UI_HomeControler.Inst.GetTopTransform());
        }
        else
        {
            InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("monthcard_bubble2"), diamond), UI_HomeControler.Inst.GetTopTransform());
        }
    }

    public static int GetTimeScaleInMonthCard(int monthCardId)
    {
        MonthcardTemplate mt = DataTemplate.GetInstance().GetMonthCardTemplateByID(monthCardId);

        return mt.getFightSpeed();
    }

    public static bool IsHaveMonthCardById(int monthCardId)
    {
        //第一个月卡为永久月卡;
        if (monthCardId == 1)
        {
            return true;
        }

        return GetMonthCardToEnd(monthCardId) > TimeSpan.Zero;
    }

    public static TimeScaleState GetMaxTimeScaleState()
    {
        return (TimeScaleState)GetTimeScaleInMonthCard(GetMaxMonthCard());
    }

    /// <summary>
    /// 显示当前拥有的最高级的月卡;
    /// </summary>
    /// <returns></returns>
    public static int GetMaxMonthCard()
    {
        if (IsHaveMonthCardById(3))
        {
            return 3;
        }

        if (IsHaveMonthCardById(2))
        {
            return 2;
        }

        return 1;
    }
}
