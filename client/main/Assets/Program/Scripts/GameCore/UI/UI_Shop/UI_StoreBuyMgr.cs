using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.UI;
using GNET;

public class UI_StoreBuyMgr : BaseUI
{
    public static string UI_ResPath = "UI_Shop/UI_StoreBuy_1_20";

    private int m_StoreId = -1;
    private Goods m_Goods = null;
    private ItemTemplate m_ItemT = null;

    private Text m_StoreNameTxt = null;
    private Text m_OwnNumTxt = null;
    private Text m_StoreDesTxt = null;
    private Text m_CosTxt = null;
    private Image m_StoreIcon = null;
    private Image m_CosIcon = null;
    private Button m_BuyBtn = null;
    private Button m_CloseBtn = null;

    public override void InitUIData()
    {
        base.InitUIData();

        m_StoreNameTxt = selfTransform.FindChild("UI_BG_Main/UI_Text_Name").GetComponent<Text>();
        m_OwnNumTxt = selfTransform.FindChild("UI_BG_Main/UI_Text_Count").GetComponent<Text>();
        m_StoreDesTxt = selfTransform.FindChild("UI_BG_Main/UI_Text_Des").GetComponent<Text>();
        m_CosTxt = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Total/UI_Text_Total").GetComponent<Text>();
        m_StoreIcon = selfTransform.FindChild("UI_BG_Main/UI_Image_Icon").GetComponent<Image>();
        m_CosIcon = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Total/jinbi").GetComponent<Image>();

        m_BuyBtn = selfTransform.FindChild("UI_BG_Main/UI_Btn_Sell").GetComponent<Button>();
        m_BuyBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onBuyBtnClick));
        m_CloseBtn = selfTransform.FindChild("UI_BG_Main/UI_Btn_Close").GetComponent<Button>();
        m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onCloseBtnClick));
    }

    public void InitBuyStoreData(Goods goods,int storeId)
    {
        m_Goods = goods;
        m_ItemT = goods.GetItemT();
        m_StoreId = storeId;

        m_StoreNameTxt.text = GameUtils.getString(m_ItemT.getName());
        m_OwnNumTxt.text = InterfaceControler.GetInst().ReturnItemCount(goods.MTabelId).ToString();
        m_StoreDesTxt.text = GameUtils.getString(m_ItemT.getDes());
        m_CosTxt.text = goods.MPrice.ToString();
        m_StoreIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_ItemT.getIcon());
        m_CosIcon.sprite = GameUtils.GetSpriteByResourceType(goods.MCosId);
    }


    private void onCloseBtnClick()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    private void onBuyBtnClick()
    {
        if (GameUtils.GetResourceCountByID(m_Goods.MCosId) >= m_Goods.MPrice)
        {
            CBuyNewShop cbns = new CBuyNewShop();
            cbns.shopid = m_StoreId;
            cbns.itemid = m_Goods.MTabelId;
            cbns.costtype = m_Goods.MCosId;
            cbns.price = m_Goods.MPrice;
            cbns.num = m_Goods.MNumbar;
            IOControler.GetInstance().SendProtocol(cbns);

            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }
        else
        {
            InterfaceControler.GetInst().AddMsgBox("钱不够", transform);
        }
    }

}
