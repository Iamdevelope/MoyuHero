using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork.Data;
using DreamFaction.LogSystem;

public class ShopModule
{
    public static readonly string ShopAssetBundleName = "ShopAd";

    public static StringBuilder GetRemindTxtStr(StringBuilder sb,string title, int times)
    {
        sb.Clear();
        return sb.Append(title).Append(times.WithColor("#F7DA3D"));
    }

    /// <summary>
    /// 购买物品接口
    /// </summary>
    /// <param name="shopId">shop表id</param>
    /// <param name="count">购买数量</param>
    /// <param name="isdiscount">是否在打折</param>
    /// <param name="errStr">返回的错误信息</param>
    /// <returns></returns>
    public static bool BuyItem(int shopId, int count, bool isdiscount, out string errStr)
    {
        errStr = ";";

        CShopBuy csb = new CShopBuy();
        csb.isdiscount = System.Convert.ToByte(isdiscount);
        csb.shopid = shopId;
        csb.num = count;

        IOControler.GetInstance().SendProtocol(csb);

        UI_ShopMgr.CanBuyItem = false;

        return true;
    }

    /// <summary>
    /// 购买物品接口
    /// </summary>
    /// <param name="shopId">shop表id</param>
    /// <param name="count">购买数量</param>
    /// <param name="isdiscount">是否在打折</param>
    /// <param name="errStr">返回的错误信息</param>
    /// <returns></returns>
    public static void BuyItem(int shopId, int count, bool isdiscount)
    {
        UI_ShopMgr.CanBuyItem = false;

        ShopTemplate shopT = DataTemplate.GetInstance().GetShopTemplateByID(shopId);
        if (shopT.getTabID() == (int)SHOP_TAB.CHARGE)
        {
            CRequestExchangeBill bill = new CRequestExchangeBill();
            bill.goodid = shopT.getId();
            bill.goodnum = count;

            IOControler.GetInstance().SendProtocol(bill);
        }
        else
        {
            CShopBuy csb = new CShopBuy();
            csb.isdiscount = System.Convert.ToByte(isdiscount);
            csb.shopid = shopId;
            csb.num = count;

            IOControler.GetInstance().SendProtocol(csb);
        }
    }
   
    public static void ShopBuyResult(int shopId, int result)
    {
     
            if(result == SShopBuy.END_OK)
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("shop_content12"));
       
    }

    /// <summary>
    /// 获得最大购买次数---注意不会有阶梯价格的物品;
    /// </summary>
    /// <param name="shopId"></param>
    /// <param name="perPrice"></param>
    /// <returns></returns>
    public static int GetMaxBuyCount(int shopId, int perPrice)
    {
        ShopTemplate ShopT = DataTemplate.GetInstance().GetShopTemplateByID(shopId);
        if(ShopT == null)
        {
            LogManager.LogError("ShopTemplate is null id=" + shopId);
            return -1;
        }

        List<int> times = new List<int>();
        //-----根据人物当前资源最大购买次数--------
        int countByMoney = -1;
        long count = -1;
        if (ObjectSelf.GetInstance().TryGetResourceCountById(ShopT.getCostType(), ref count))
        {
            countByMoney = (int)(count / perPrice);
            times.Add(countByMoney);
        }

        int vipLv = ObjectSelf.GetInstance().VipLevel;
        Shopbuy sb = ObjectSelf.GetInstance().GetShopBuyInfoByShopId(ShopT.getId());
        //-----根据人物当前vip每日限购最大购买次数--
        int countByDaily = -1;
        //-----根据人物当前vip总限购最大购买次数----
        int countByTotal = -1;
        if (IsShopBuyLimited(ShopT))
        {
            countByDaily = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(ShopT, vipLv) - sb.todaynum;
            countByTotal = DataTemplate.GetInstance().GetShopItemDailyBuyTimes(ShopT, vipLv) - sb.buyallnum;
        
            times.Add(countByDaily);
            times.Add(countByTotal);

        }

        //默认最大购买次数;
        times.Add(999);

        return Mathf.Max(0, Mathf.Min(times.ToArray()));
    }

    public static SHOP_LIMIT_TYPE GetShopLimitType(ShopTemplate shopT)
    {
        if (shopT == null)
            return SHOP_LIMIT_TYPE.NONE;

        int daily = shopT.getDailyMaxBuy();
        int total = shopT.getShelveMaxBuy();
        
        if (daily != -1)
            return SHOP_LIMIT_TYPE.DAILY;

        if (daily == -1 && total != -1)
            return SHOP_LIMIT_TYPE.TOTAL;

        if (daily != -1 && total != -1)
        {
            LogManager.LogError("策划说不会有每天限购且永久限购类型的商品");
            return SHOP_LIMIT_TYPE.NONE;
        }

        return SHOP_LIMIT_TYPE.NONE;
    }

    /// <summary>
    /// 该物品是否限购;
    /// </summary>
    /// <param name="shopT"></param>
    /// <returns></returns>
    public static bool IsShopBuyLimited(ShopTemplate shopT)
    {
        if (shopT == null)
            return false;

        return shopT.getDailyMaxBuy() > 0 || shopT.getShelveMaxBuy() > 0;
    }

    /// <summary>
    /// 商城道具图标点击处理函数;
    /// </summary>
    /// <param name="shopT"></param>
    /// <returns></returns>
    public static bool OnShopItemIconClickHandler(ShopTemplate shopT)
    {
        if(shopT == null)
        {
            LogManager.LogError("ShopTemplate is NULL");
            return false;
        }

        switch(shopT.getPreviewType())
        {
            case -1:
                break;
            case 1:
                int tableId = GameUtils.StringToInt(shopT.getPreviewContent());
                
                return ShowItemPreviewUIHandler(tableId);
            case 2:
                ShowGroupItemPreviewUI(shopT);
                break;
            case 3:
                ShowYUEKAPreviewUI(shopT);
                break;
            default:
                LogManager.LogError("没有处理的商品点击预览类型");
                return false;
        }

        return true;
    }

    /// <summary>
    /// 根据id展示不同的物品展示界面;
    /// </summary>
    /// <param name="tableID"></param>
    public static bool ShowItemPreviewUIHandler(int tableID)
    {
        EM_OBJECT_CLASS eoc = GameUtils.GetObjectClassById(tableID);
        switch (eoc)
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                ItemTemplate runeItemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                if (runeItemT == null)
                {
                    LogManager.LogError("item表格中缺少物品id=" + tableID);
                    return false;
                }
                UI_RuneInfo.SetShowRuneDate(runeItemT);
                UI_HomeControler.Inst.AddUI(UI_RuneInfo.UI_ResPath);
                return true;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                if (itemT == null)
                {
                    LogManager.LogError("item表格中缺少物品id=" + tableID);
                    return false;
                }
                UI_Item.SetItemTemplate(itemT);
                UI_HomeControler.Inst.AddUI(UI_Item.UI_ResPath);
                return true;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                ArtresourceTemplate artT = DataTemplate.GetInstance().GetArtResourceTemplate(tableID);
                if (artT == null)
                {
                    LogManager.LogError("ArtResource时装表格中缺少物品id=" + tableID);
                    return false;
                }
                UI_SkinPreviewMgr.SetShowArtTemplate(artT);
                UI_HomeControler.Inst.AddUI(UI_SkinPreviewMgr.UI_ResPath);
                return true;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(tableID);
                if (heroT == null)
                {
                    LogManager.LogError("hero表格中缺少物品id=" + tableID);
                    return false;
                }
                UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
                HeroInfoPop.inst.SetShowData(heroT);
                return true;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                //资源类型点击无响应;
                return true;
            default:
                LogManager.LogError("未处理的商城物品预览类型");
                return false;
        }
    }

    /// <summary>
    /// 显示物品组预览UI;
    /// </summary>
    /// <param name="content">预览显示配置中填以#分割的预览串：
    /// 预览方式-ID/美术资源名-数量-名称文本
    /// 预览方式1时为ID
    /// 预览方式2时为美术资源名
    /// 预览方式为1时要有2次预览弹窗，弹窗同预览类型1显示</param>
    /// <returns></returns>
    public static bool ShowGroupItemPreviewUI(ShopTemplate shopT)
    {
        UI_ItemGroupMgr.SetData(shopT);
        UI_HomeControler.Inst.AddUI(UI_ItemGroupMgr.UI_ResPath);

        return true;
    }

    public static bool ShowYUEKAPreviewUI(ShopTemplate shopT)
    {
        UI_YueKaPreviewMgr.SetShopTemplate(shopT);
        UI_HomeControler.Inst.AddUI(UI_YueKaPreviewMgr.UI_ResPath);

        return true;
    }

    /// <summary>
    /// 根据当前时间判断物品是否在上架;
    /// </summary>
    /// <param name="shopT"></param>
    /// <returns></returns>
    public static bool IsShopItemInSaling(ShopTemplate shopT)
    {
        DateTime dt1 = GameUtils.ConvertStringToDateTime(shopT.getOnShelve());
        DateTime dt2 = GameUtils.ConvertStringToDateTime(shopT.getOffShelve());

        if (ObjectSelf.GetInstance().ServerDateTime > dt1 && ObjectSelf.GetInstance().ServerDateTime < dt2)
            return true;

        return false;
    }

    /// <summary>
    /// 根据当前时间判断物品是否在打折;
    /// </summary>
    /// <param name="shopT"></param>
    /// <returns></returns>
    public static bool IsShopItemInDiscount(ShopTemplate shopT)
    {
        DateTime dt1 = GameUtils.ConvertStringToDateTime(shopT.getDiscountOn());
        DateTime dt2 = GameUtils.ConvertStringToDateTime(shopT.getDiscountOff());

        if (ObjectSelf.GetInstance().ServerDateTime > dt1 && ObjectSelf.GetInstance().ServerDateTime < dt2)
            return true;

        return false;
    }

    /// <summary>
    /// 距离下架的时间;
    /// </summary>
    /// <param name="shopT"></param>
    /// <returns></returns>
    public static TimeSpan GetTimeSpanToOffShelve(ShopTemplate shopT)
    {
        if(shopT == null)
        {
            return TimeSpan.MaxValue;
        }

        return GameUtils.ConvertStringToDateTime(shopT.getOffShelve()) - ObjectSelf.GetInstance().ServerDateTime;
    }

    public static TimeSpan GetTimeSpanToDiscountOff(ShopTemplate shopT)
    {
        if(shopT == null)
        {
            return  TimeSpan.MaxValue;
        }

        return GameUtils.ConvertStringToDateTime(shopT.getDiscountOff()) - ObjectSelf.GetInstance().ServerDateTime;
    }

    public static string GetShopItemTitle(ShopTemplate shopT)
    {
        if(shopT == null)
        {
            LogManager.LogError("");
            return "";
        }

        StringBuilder sb = new StringBuilder();
        switch(shopT.getTagtype())
        {
            case -1:
                return "";
            case 1:
                TimeSpan ts = GetTimeSpanToOffShelve(shopT);
                if (ts.Days >= 365)
                {
                    return "";
                }

                sb.Append(GameUtils.getString(shopT.getTagtext1()));
                sb.Append(FillWith2Char(ts.Hours + ts.Days * 24));
                sb.Append(":");
                sb.Append(FillWith2Char(ts.Minutes));
                sb.Append(":");
                sb.Append(FillWith2Char(ts.Seconds));
                return sb.ToString();
            case 2:
                TimeSpan ts1 = GetTimeSpanToDiscountOff(shopT);
                if (ts1.Days >= 365)
                {
                    return "";
                }
                sb.Append(GameUtils.getString(shopT.getTagtext1()));
                sb.Append(FillWith2Char(ts1.Hours + ts1.Days * 24));
                sb.Append(":");
                sb.Append(FillWith2Char(ts1.Minutes));
                sb.Append(":");
                sb.Append(FillWith2Char(ts1.Seconds));
                return sb.ToString();
            case 3:
                sb.Append(GameUtils.getString(shopT.getTagtext1()));
                return sb.ToString();
            case 4:
                if(IsShopItemInSaling(shopT))
                {
                    if(IsShopItemInDiscount(shopT))
                    {
                        TimeSpan ts2 = GetTimeSpanToDiscountOff(shopT);
                        if (ts2.Days >= 365)
                        {
                            return "";
                        }
                        sb.Append(GameUtils.getString(shopT.getTagtext1()));
                        sb.Append(FillWith2Char(ts2.Hours + ts2.Days * 24));
                        sb.Append(":");
                        sb.Append(FillWith2Char(ts2.Minutes));
                        sb.Append(":");
                        sb.Append(FillWith2Char(ts2.Seconds));
                    }
                    else
                    {
                        TimeSpan ts3 = GetTimeSpanToOffShelve(shopT);
                        if (ts3.Days >= 365)
                        {
                            return "";
                        }
                        sb.Append(GameUtils.getString(shopT.getTagtext2()));
                        sb.Append(FillWith2Char(ts3.Hours + ts3.Days * 24));
                        sb.Append(":");
                        sb.Append(FillWith2Char(ts3.Minutes));
                        sb.Append(":");
                        sb.Append(FillWith2Char(ts3.Seconds));
                    }
                    return sb.ToString();
                }
                return "";
        }

        return "";
    }

    static string FillWith2Char(int num)
    {
        return GameUtils.FillWithChar(num, 2, '0');
    }


    public static int GetMoneyCountToNextVipLv(int curVipLv, int curVipExp)
    {
        VipTemplate vipT = DataTemplate.GetInstance().GetVipTemplateById(curVipLv);

        if (vipT == null)
        {
            LogManager.LogError("VIP 表格中不存在的VIP等级lv=" + curVipLv);
            return -2;
        }

        if (vipT.getVipExp() == -1)
        {
            //Debug.Log("已满级");
            return 0;
        }

        int needExp = vipT.getVipExp() - curVipExp;

        if (needExp <= 0)
        {
            //Debug.Log("Vip可以升级了");
            return -1;
        }

        GameConfig config = DataTemplate.GetInstance().GetGameConfig();

        int fen = config.getRealmoney_to_vipexp() * needExp; //总共花多少分钱;

        return fen / 100 + (fen % 100 == 0 ? 0 : 1);
    }

    /// <summary>
    /// 判断当前VIP是否满级;
    /// </summary>
    /// <returns></returns>
    public static bool IsVipFullLv(int curVipLv)
    {
        VipTemplate vipT = DataTemplate.GetInstance().GetVipTemplateById(curVipLv);

        return (vipT == null) || (vipT.getVipExp() == -1);
    }
}