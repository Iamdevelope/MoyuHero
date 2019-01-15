
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

public class UI_YueKaBuyMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Shop/UI_YueKaBuy_1_3";

    private static MonthcardTemplate _monthCardT = null;

    Text titleTxt;
    Text nameTxt;
    Image iconBg;
    Image iconImg;
    GameObject listObj;
    Button closeBtn;
    Text closeBtnTxt;
    Button buyBtn;
    Text buyBtnTxt;
    Text costTxt;
    GameObject itemObj;

    public override void InitUIData()
    {
        base.InitUIData();

        titleTxt = transform.FindChild("Image/Text").GetComponent<Text>();
        nameTxt = transform.FindChild("ItemDetail/Name").GetComponent<Text>();
        iconBg = transform.FindChild("ItemDetail/IconBg").GetComponent<Image>();
        iconImg = transform.FindChild("ItemDetail/Icon").GetComponent<Image>();
        listObj = transform.FindChild("ItemDetail/DetailObj").gameObject;
        closeBtn = transform.FindChild("CloseBtn").GetComponent<Button>();
        closeBtnTxt = transform.FindChild("CloseBtn/Text").GetComponent<Text>();
        buyBtn = transform.FindChild("BuyBtn").GetComponent<Button>();
        buyBtnTxt = transform.FindChild("BuyBtn/Text").GetComponent<Text>();
        costTxt = transform.FindChild("BuyBtn/MoneyCost/Text").GetComponent<Text>();
        itemObj = transform.FindChild("Items/Item").gameObject;

        closeBtn.onClick.AddListener(OnCloseBtnClick);
        buyBtn.onClick.AddListener(OnBuyBtnClick);
    }

    public static void SetMonthTemplate(MonthcardTemplate monthcardT)
    {
        _monthCardT = monthcardT;
    }

    public override void InitUIView()
    {
        base.InitUIView();

        titleTxt.text = GameUtils.getString("common_purchaseform_title");
        closeBtnTxt.text = GameUtils.getString("common_button_close");
        buyBtnTxt.text = GameUtils.getString("common_button_purchase");

        if (_monthCardT != null)
        {
            SetShowData(_monthCardT);
        }
    }

    public void SetShowData(ShopTemplate shopT)
    {
        if(shopT == null)
        {
            LogManager.LogError("YueKaPreview ShopTemplate is null");
            return;
        }

        nameTxt.text = GameUtils.getString(shopT.getCommodityName());
        iconBg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + shopT.getBaseicon());
        iconBg.SetNativeSize();
        iconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + shopT.getResourceName());
        string content = shopT.getPreviewContent();
        if(string.IsNullOrEmpty(content))
        {
            LogManager.LogError("ShopTemplate表格预览内容描述填写错误！id=" + shopT.getId());
            return;
        }

        string[] detailTxt = GameUtils.getString(content).Split(new string[] { "#" }, StringSplitOptions.None);
        if(detailTxt == null || detailTxt.Length == 0)
        {
            LogManager.LogError("ShopTemplate表格预览内容描述解析数据为空！id=" + shopT.getId());
            return;
        }

        for(int i = 0, j = detailTxt.Length; i < j; i++)
        {
            CreateDetail(detailTxt[i]);
        }
    }

    public void SetShowData(MonthcardTemplate monthT)
    {
        nameTxt.text = GameUtils.getString(monthT.getName());
        iconBg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + monthT.getBaseicon());
        iconBg.SetNativeSize();
        iconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + monthT.getIcon());
        iconImg.preserveAspect = true;
        ExchangeTemplate ex = DataTemplate.GetInstance().GetExchangeTemplateByMonthCardId(monthT.getId());
        costTxt.text = ex.getPrice().ToString();
        //iconImg.SetNativeSize();

        string[] detailTxt = monthT.getDes().Split(new string[] { "#" }, StringSplitOptions.None);
        if (detailTxt == null || detailTxt.Length == 0)
        {
            LogManager.LogError("ExchangeTemplate表格预览内容描述解析数据为空！id=" + monthT.getId());
            return;
        }

        for (int i = 0, j = detailTxt.Length; i < j; i++)
        {
            CreateDetail(GameUtils.getString(detailTxt[i]));
        }
    }

    void OnCloseBtnClick()
    {
        CloseUI();
    }

    void OnBuyBtnClick()
    {
        ExchangeTemplate ex = DataTemplate.GetInstance().GetExchangeTemplateByMonthCardId(_monthCardT.getId());
        ExchangeModule.ChargeMoney(ex.getId());
        CloseUI();
    }

    void CloseUI()
    {
        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    void CreateDetail(string content)
    {
        GameObject go = GameObject.Instantiate(itemObj) as GameObject;

        if(go == null)
        {
            LogManager.LogError("月卡预览描述obj创建失败了");
            return;
        }

        Text txt = go.transform.FindChild("Text").GetComponent<Text>();
        go.transform.parent = listObj.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

        txt.text = content;
    }

    void OnDestroy()
    {
        OnReadyForClose();
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        closeBtn.onClick.RemoveAllListeners();

        GameUtils.DestroyChildsObj(listObj);

        _monthCardT = null;
    }
}
