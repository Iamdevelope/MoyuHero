using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.UI;
using System.Text;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;

public class UI_StoreGoodsMgr : BaseUI
{
    public static string UI_ResPath = "UI_Shop/UI_StoreGoods_2_6";

    private BaseStore m_Store = null;
    private ShangdianTemplate m_StoreT = null;

    private GameObject m_Prefab = null;
    private Transform m_Grid = null;

    private Text m_StoreNameTxt = null;
    private Button m_BackBtn = null;

    private GameObject m_RefleshOBJ = null;
    private Text m_RefleshTimeTxt = null;
    private Image m_CosMoneyImg = null;
    private Text m_CosMoenyNumTxt = null;
    private Button m_RefleshBtn = null;

    private int m_CosNum = -1;

    public override void InitUIData()
    {
        base.InitUIData();

        m_Grid = selfTransform.FindChild("Content/LayoutList");
        m_Prefab = UIResourceMgr.LoadPrefab(common.prefabPath + "UI_Shop/UI_GoodsItem") as GameObject;

        m_RefleshOBJ = selfTransform.FindChild("RefleshPanel").gameObject;
        m_StoreNameTxt = selfTransform.FindChild("TopPanel/TopTittle/Title").GetComponent<Text>();
        m_RefleshTimeTxt = selfTransform.FindChild("RefleshPanel/Text_Time").GetComponent<Text>();
        m_CosMoneyImg = selfTransform.FindChild("RefleshPanel/Img_Ruby9").GetComponent<Image>();
        m_CosMoenyNumTxt = selfTransform.FindChild("RefleshPanel/Text_Number11").GetComponent<Text>();

        m_BackBtn = selfTransform.FindChild("TopPanel/TopTittle/BackBtn").GetComponent<Button>();
        m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onBackBtnClick));
        m_RefleshBtn = selfTransform.FindChild("RefleshPanel/Btn_Reflesh").GetComponent<Button>();
        m_RefleshBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onRefleshBtnClick));

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_SGetStore, UpdateUIShow);
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();

        if (m_Store.MRefreshTime > 0)
        {
            int hour = m_Store.MRefreshTime / 3600;
            int minute = m_Store.MRefreshTime % 3600 / 60;
            int second = m_Store.MRefreshTime % 3600 % 60;
            string hourStr = hour <= 9 ? "0" + hour : hour.ToString();
            string minuteStr = minute <= 9 ? "0" + minute : minute.ToString();
            string secondStr = second <= 9 ? "0" + second : second.ToString();

            m_RefleshTimeTxt.text = string.Format("{0}:{1}:{2}", hourStr,minuteStr, secondStr);
        }
        else
        {
            CGetNewShop cgns = new CGetNewShop();
            IOControler.GetInstance().SendProtocol(cgns);
        }
       
    }

    public void InitStoreDta(BaseStore store)
    {
        m_Store = store;
        m_StoreT = store.GetStoreRow();

        ShowText();
        CreateItemOBJ();
        ShowReflesh();
    }

    private void UpdateUIShow()
    {
        ShowText();
        CreateItemOBJ();
        ShowReflesh();
    }

    private void ShowText()
    {
        m_StoreNameTxt.text = GameUtils.getString(m_StoreT.getStoreName());
    }

    private void ShowReflesh()
    {
        if (m_StoreT.getCurrencyType() == -1)
        {
            m_RefleshOBJ.SetActive(false);
        }
        else
        {
            m_RefleshOBJ.SetActive(true);
            m_CosMoneyImg.sprite = GameUtils.GetSpriteByResourceType(m_StoreT.getStoreCurrencyType());
            if (m_Store.MRefreshCount < m_StoreT.getConsumption().Length)
            {
                m_CosNum = m_StoreT.getConsumption()[m_Store.MRefreshCount];
            }
            else
            {
                m_CosNum = m_StoreT.getConsumption()[m_StoreT.getConsumption().Length - 1];
            }
            m_CosMoenyNumTxt.text = m_CosNum.ToString();
        }
    }

    private void CreateItemOBJ()
    {
        ClearItemOBJ();

        for (int i = 0; i < m_Store.MGoodsList.Count; i++)
        {
            Goods goods = m_Store.MGoodsList[i];
            GameObject go = Instantiate(m_Prefab) as GameObject;
            go.transform.parent = m_Grid;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            UI_GoodsItem uiGoodsItem = go.GetComponent<UI_GoodsItem>();
            uiGoodsItem.InitItemData(goods,m_Store.MStoreId);
            uiGoodsItem.SetTuiJianActive(m_StoreT.getRecommend()[i] == 1);
        }
    }

    private void ClearItemOBJ()
    {
        for (int i = 0; i < m_Grid.childCount; i++)
        {
            Destroy(m_Grid.GetChild(i).gameObject);
        }
    }


    private void onClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    private void onBackBtnClick()
    {
        onClose();
    }

    private void onRefleshBtnClick()
    {
        if (GameUtils.GetResourceCountByID(m_StoreT.getStoreCurrencyType()) >= m_CosNum)
        {
            CGetNewShopItem cgnsi = new CGetNewShopItem();
            cgnsi.shopid = m_Store.MStoreId;
            IOControler.GetInstance().SendProtocol(cgnsi);
        }
        else
        {
            InterfaceControler.GetInst().AddMsgBox("钱不够", transform);
        }
    }

    private void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SGetStore, UpdateUIShow);
    }
    
}
