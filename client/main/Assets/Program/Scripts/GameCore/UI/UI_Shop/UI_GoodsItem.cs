using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.UI;

public class UI_GoodsItem :  BaseUI
{
    public static string UI_ResPath = "UI_Shop/UI_GoodsItem";

    private Goods m_Goods = null;
    private ItemTemplate m_ItemT = null;
    private int m_StoreId= -1;

    private GameObject m_TuijianOBJ = null;
    private GameObject m_SoldOutOBJ = null;
    private Text m_GoodsNameTxt = null;
    private Image m_GoodsIcon = null;
    private Image m_CosIcon = null;
    private Text m_CosText = null;
    private Button m_GoodsBtn = null;

    public override void InitUIData()
    {
        base.InitUIData();

        m_TuijianOBJ = selfTransform.FindChild("Img_Slash").gameObject;
        m_SoldOutOBJ = selfTransform.FindChild("Sold1").gameObject;

        m_GoodsNameTxt = selfTransform.FindChild("Btn_Shading11/Text_Itemname").GetComponent<Text>();
        m_GoodsIcon = selfTransform.FindChild("Btn_Shading11/Img_Icon").GetComponent<Image>();
        m_CosIcon = selfTransform.FindChild("Btn_Shading11/Price1/Img_Gold11").GetComponent<Image>();
        m_CosText = selfTransform.FindChild("Btn_Shading11/Price1/Text_Price").GetComponent<Text>();
        m_GoodsBtn = selfTransform.FindChild("Btn_Shading11").GetComponent<Button>();
        m_GoodsBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onGoodsBtnClick));
    }

    public void InitItemData(Goods goods, int storeId)
    {
        m_Goods = goods;
        m_ItemT = goods.GetItemT();
        m_StoreId = storeId;

        m_SoldOutOBJ.SetActive(m_Goods.MIsbuy == 1);

        string str = GameUtils.getString(m_ItemT.getName());
        m_GoodsNameTxt.text = goods.MNumbar > 1 ? str + " ×" + goods.MNumbar : str;
        m_GoodsIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_ItemT.getIcon());
        m_CosIcon.sprite = GameUtils.GetSpriteByResourceType(goods.MCosId);
        m_CosText.text = goods.MPrice.ToString();
    }


    public void SetTuiJianActive(bool active)
    {
        m_TuijianOBJ.SetActive(active);
    }

    private void onGoodsBtnClick()
    {
        GameObject go = UI_HomeControler.Inst.AddUI(UI_StoreBuyMgr.UI_ResPath);
        UI_StoreBuyMgr uiStoreBuyMgr = go.GetComponent<UI_StoreBuyMgr>();
        uiStoreBuyMgr.InitBuyStoreData(m_Goods, m_StoreId);
    }

}
