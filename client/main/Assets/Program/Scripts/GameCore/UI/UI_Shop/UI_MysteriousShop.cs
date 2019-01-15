using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameNetWork;

using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.LogSystem;
using DreamFaction.UI;
using DreamFaction.UI.Core;


public class UI_MysteriousShop : UI_BaseMysteriousShop 
{
    private delegate void IdHandler(int itemId);
    private class ItemData 
    {
        public int id; // id
        
        public int isopen; // 是否开启（1开启，0未开启）
        public int price; // 价格

        public MysteriousshopTemplate mysteriousshopTemplate;
        public ItemTemplate itemTemplate;
        public MysteriousItem m_Item;

        public IdHandler SetCurrentId;

        private ItemData(){ }
        //此处生成的ItemData，除item未初始化以外其他均以初始化完成
        public static ItemData GenerateItemData(GNET.Smshopdata data,IdHandler handler)
        {
            ItemData itemTemp = new ItemData();
            itemTemp.SetCurrentId = handler;
            itemTemp.id = data.id;
            itemTemp.isopen = data.isopen;
            itemTemp.price = data.price;

            itemTemp.mysteriousshopTemplate = DataTemplate.GetInstance().GetMysteriousShopItemTemplateById(data.id);//43表一条数据
            itemTemp.itemTemplate = DataTemplate.GetInstance().GetItemTemplateById(itemTemp.mysteriousshopTemplate.getCommodityid());//26表一条数据

            return itemTemp;
        }


        public void InitSelfItem(MysteriousItem item)
        {
            this.m_Item = item;

            InitItem(item);

            if (SetCurrentId != null)
            {
                item.buyBtn.gameObject.SetActive(true);
                item.buyBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBuyBtn));
            }
        }

        public void InitItem(MysteriousItem item)
        {
            item.buyBtnText.text = GameUtils.getString("common_button_purchase");

            string stringTemp = mysteriousshopTemplate.getCommodityName();//获得商品名称描述在05表中的索引 != "-1"
            if (stringTemp != string.Empty)
            {
                item.tittleText.text = GameUtils.getString(stringTemp);
            }
            else
            {
                item.tittleText.text = "表中无名";
            }
            stringTemp = mysteriousshopTemplate.getCommodityDes();
            if (stringTemp != string.Empty)
                item.m_DescriptionText.text = GameUtils.getString(stringTemp);
            else
                item.m_DescriptionText.text = "表中无描述";

            stringTemp = mysteriousshopTemplate.getCommodityresource();
            Sprite sprite;
            if (stringTemp != string.Empty)
            {
                sprite = UIResourceMgr.LoadSprite(common.defaultPath + stringTemp);
            }
            else
            {
                sprite = UIResourceMgr.LoadSprite(common.defaultPath + itemTemplate.getIcon());
            }

            ItemTypeProcess(item,sprite);

            sprite = GameUtils.GetSpriteByResourceType(mysteriousshopTemplate.getCostType());
            if (sprite != null)
            {
                item.m_OldResourceImage.sprite = sprite;
                item.m_ResourceImage.sprite = sprite;
            }
            item.m_PriceText.text = price.ToString();
            item.m_OldPriceText.text = price.ToString();

            bool _havePay = isopen != 0;
            item.buyBtn.enabled = !_havePay;
            item.m_HavePay.SetActive(_havePay);
            item.discountObject.SetActive(!_havePay);
            GameUtils.SetBtnSpriteGrayState(item.buyBtn, _havePay);

            item.m_OldPrice.SetActive(false);
            item.buyBtn.gameObject.SetActive(false);
        }

        public void OnClickBuyBtn()
        {
            if (SetCurrentId != null)
                SetCurrentId(id);
        }

        private void ItemTypeProcess(MysteriousItem mysteriousItem, Sprite icon)
        {
            if (GameUtils.GetObjectClassById(itemTemplate.getId()) != EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE)
            {
                mysteriousItem.itemImage.enabled = true;
                if (icon != null)
                    mysteriousItem.itemImage.sprite = icon;
                if (mysteriousItem.m_RuneTrans != null)
                    mysteriousItem.m_RuneTrans.gameObject.SetActive(false);
                return;
            }

            if (mysteriousItem.m_RuneTrans == null)
            {
                GameObject go = UIResourceMgr.LoadPrefab(common.prefabPath + "UI_Rune/RuneIconItem") as GameObject;
                go = GameObject.Instantiate(go, mysteriousItem.itemImage.transform.position, mysteriousItem.itemImage.transform.rotation) as GameObject;
                mysteriousItem.m_RuneTrans = go.transform;
                mysteriousItem.m_RuneTrans.SetParent(mysteriousItem.itemImage.transform);
                mysteriousItem.m_RuneTrans.localScale = Vector3.one * 2;
                mysteriousItem.m_RuneIconItem = new RuneIconItem(mysteriousItem.m_RuneTrans);
            }
            mysteriousItem.m_RuneTrans.gameObject.SetActive(true);
            mysteriousItem.itemImage.enabled = false;

            int _runeType = itemTemplate.getRune_type();
            if (_runeType == 5 || _runeType == 6)
                mysteriousItem.m_RuneIconItem.SetIsSpecial(true);
            else
                mysteriousItem.m_RuneIconItem.SetRuneType(_runeType);
//            item.m_RuneIconItem.SetRuneType();
//            item.m_RuneIconItem.SetIsSpecial(data.getType() == 3);
            mysteriousItem.m_RuneIconItem.SetLevelInfoActive(false);
            if (icon != null)
                mysteriousItem.m_RuneIconItem.SetIcon(icon);

        }
    }
    private class MysteriousItem
    {
        public GameObject itemObject;
        public Button buyBtn;
        public Text buyBtnText;
        public Text tittleText;
        public Image itemImage;

        public Text discountText;
        public GameObject discountObject;

        public GameObject m_HavePay;
        public Text m_HavePayText;
        public GameObject m_OldPrice;
        public Image m_OldResourceImage;
        public Text m_OldPriceText;
        public Image m_ResourceImage;
        public Text m_PriceText;

        public Text m_DescriptionText;

        public RuneIconItem m_RuneIconItem;
        public Transform m_RuneTrans;

        private MysteriousItem()
        { 

        }



        public static MysteriousItem GenerateItem(GameObject go)
        {
            MysteriousItem tempItem = new MysteriousItem();
            tempItem.itemObject = go;

            tempItem.buyBtn = go.transform.FindChild("BuyBtn").GetComponent<Button>();
            tempItem.buyBtnText = go.transform.FindChild("BuyBtn/BuyBtnText").GetComponent<Text>();
            tempItem.tittleText = go.transform.FindChild("TittleImage/TittleText").GetComponent<Text>();
            tempItem.itemImage = go.transform.FindChild("ItemImage").GetComponent<Image>();

            tempItem.discountText = go.transform.FindChild("DiscountImage/DiscountText").GetComponent<Text>();
            tempItem.discountObject = go.transform.FindChild("DiscountImage").gameObject;
            tempItem.discountObject.SetActive(false);

            tempItem.m_HavePay = go.transform.FindChild("HavePayImage").gameObject;
            tempItem.m_HavePayText = go.transform.FindChild("HavePayImage/HavePayText").GetComponent<Text>();
            tempItem.m_OldPrice = go.transform.FindChild("PriceLayout/OldPrice").gameObject;
            tempItem.m_OldResourceImage = go.transform.FindChild("PriceLayout/OldPrice/OldResourceImage").GetComponent<Image>();
            tempItem.m_OldPriceText = go.transform.FindChild("PriceLayout/OldPrice/OldPriceText").GetComponent<Text>();
            tempItem.m_ResourceImage = go.transform.FindChild("PriceLayout/Price/ResourceImage").GetComponent<Image>();
            tempItem.m_PriceText = go.transform.FindChild("PriceLayout/Price/PriceText").GetComponent<Text>();
            tempItem.m_DescriptionText = go.transform.FindChild("DescriptionText").GetComponent<Text>();
            return tempItem;
        }
    }

    private BattleStageMgr battleStageMgr;
    private StringBuilder timeTextBuilder;
    private string timeTextBuffer;
    private GameObject originItem;
    private GameObject itemListLayout;
    private GameObject confirmPanel;

    private int m_Time;
    //ConfirmPanel面板上的图标
    private Image m_CostImage;
    private MysteriousItem m_ConfirmItem;

    private List<ItemData> itemDataList = new List<ItemData>();
    private int currentItemId = -1;    // 当点击购买按钮是，会修改该值表示是哪个商品的购买按钮触发确认面板
    private ItemData currentItemData;

    private Transform m_CaptionLayoutPoint;
    public static string Path = "UI_Shop/UI_MysteriousShop_2_7";

    public override void InitUIData()
    {
        base.InitUIData();

        timeTextBuilder = new StringBuilder();
        m_ConfirmItem = MysteriousItem.GenerateItem(selfTransform.FindChild("ConfirmPanel/Item").gameObject);
        m_CostImage = selfTransform.FindChild("ConfirmPanel/PlayerWalletText/CostImage").GetComponent<Image>();
        originItem = selfTransform.FindChild("OriginPanel/OriginItem").gameObject;
        itemListLayout = selfTransform.FindChild("ItemList/ListLayout").gameObject;
        confirmPanel = selfTransform.FindChild("ConfirmPanel").gameObject;
        m_CaptionLayoutPoint = selfTransform.FindChild("CaptionLayout");
        battleStageMgr = ObjectSelf.GetInstance().BattleStageData;
        
        m_Time = battleStageMgr.m_SpecialStage.m_Time;

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_MysteriousShopBuyReplay, PayHandler);

    }
    private void OnDestroy()
    {
        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.Release(m_CaptionLayoutPoint);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_MysteriousShopBuyReplay, PayHandler);

    }
    public override void InitUIView()
    {
        base.InitUIView();
        
        //m_CostText.text = "99";
        //m_ConfirmItemNameText.text = "-Grimoire of Alice-";
        //m_PlayerWalletText.text = "250";

        timeTextBuffer = GameUtils.getString("fight_mysteriousshop_content1");
        m_ConfirmTittleText.text = GameUtils.getString("common_purchaseform_title");
        m_PayBtnText.text = GameUtils.getString("common_button_purchase");
        m_CloseBtnText.text = GameUtils.getString("common_button_close");
        m_TopTittleText.text = GameUtils.getString("fight_mysteriousshop_title");


        RefreshItemDataList();
        confirmPanel.SetActive(false);

        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.AwakeUp(m_CaptionLayoutPoint);
    }



    public override void UpdateUIView()
    {
        base.UpdateUIView();

        int time = battleStageMgr.m_SpecialStage.m_Time;

        if (time > 0 && m_Time>time)
        {
            m_Time = time;
            byte hour = (byte)(time / 60);
            byte minute = (byte)(time % 60);
 
            timeTextBuilder.Remove(0, timeTextBuilder.Length);
            if (minute>9)
                timeTextBuilder.AppendFormat(GameUtils.StringWithColor("<size=31>{0}:{1}</size> ", TEXT_COLOR.GREEN), hour, minute);
            else
                timeTextBuilder.AppendFormat(GameUtils.StringWithColor("<size=31>{0}:0{1}</size> ", TEXT_COLOR.GREEN), hour, minute);
            timeTextBuilder.Append(timeTextBuffer);

            m_TimePanelText.text = timeTextBuilder.ToString();
        }
        
        if (time <= 0 && gameObject != null)
        {
            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }
    }

    private void RefreshItemDataList()
    {
        if (itemDataList.Count > 0)
        {
            for(int i = 0;i<itemDataList.Count;i++)
            {
                Destroy(itemDataList[i].m_Item.itemObject);
            }
            itemDataList.Clear();
        }
        List<GNET.Smshopdata> itemData = ObjectSelf.GetInstance().BattleStageData.m_MysteriousShopItemCollection;

        if (itemData == null || itemData.Count <= 0)
        {
            Debug.LogError("神秘商店物品数据为null");
            LogManager.LogToFile("神秘商店物品数据为null");

            return;
        }

        for (int i = 0; i < itemData.Count; i++)
        {
            ItemData itemTemp = ItemData.GenerateItemData(itemData[i], ShowConfirmPannel);
            itemDataList.Add(itemTemp);
        }
        itemDataList.Sort((a,b) => a.mysteriousshopTemplate.getSorting() - b.mysteriousshopTemplate.getSorting());
        for (int i = 0; i < itemDataList.Count; i++)
        {
            var temp = CreatItem();
            if (temp != null)
            {
                itemDataList[i].InitSelfItem(temp);

            }
        }

    }
    //private int SortHandler(ItemData leftItem,ItemData rightItem)
    //{
    //    return leftItem.mysteriousshopTemplate.getSorting() - rightItem.mysteriousshopTemplate.getSorting();
    //}

    private MysteriousItem CreatItem()
    {
        if (itemListLayout == null || originItem == null)
        {
            //Debug.Log("神秘商店面板：无法找到子物体：itemListLayout和originItem");
            return null;
        }

        GameObject go = Instantiate(originItem, itemListLayout.transform.position, itemListLayout.transform.rotation) as GameObject;

        go.transform.SetParent(itemListLayout.transform);
        go.transform.localScale = Vector3.one;

        var item = MysteriousItem.GenerateItem(go);
        item.buyBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBuyBtn));
        return item;
    }




    /**********************CallBack*******************************/

    public void PayHandler(GameEvent eventData)
    {
        int result = (int)eventData.data;

        var temp = DreamFaction.GameCore.InterfaceControler.GetInst();
        if (temp != null)
        {
            if (result == 1)
            {

                RefreshItemDataList();
                temp.AddMsgBox(GameUtils.getString("shop_content12"), transform);
                confirmPanel.SetActive(false);
            }
            else if(result == 2)
            {
               // temp.AddMsgBox("购买失败", transform);
            }
        
        }
    }

    public void ShowConfirmPannel(int currentItemId)
    {
        this.currentItemId = currentItemId;
        ItemData item = null;

        for (int i = 0; i < itemDataList.Count; i++)
        {
            if (itemDataList[i].id == currentItemId)
            {
                item = itemDataList[i];
                break;
            }
        }
        if(item != null)
        {
            m_PlayerWalletText.text = GameUtils.GetResourceCountByID(item.mysteriousshopTemplate.getCostType()).ToString();
            m_CostImage.sprite = GameUtils.GetSpriteByResourceType(item.mysteriousshopTemplate.getCostType());
            item.InitItem(m_ConfirmItem);
            
        }

        confirmPanel.SetActive(true);
    }


    protected override void OnClickCloseBtn()
    {
        //gameObject.SetActive(false);
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
    protected override void OnClickBuyBtn()
    {
        confirmPanel.SetActive(true);
    }
    protected override void OnClickConfirmPayBtn()
    {
//        print("物品："+currentItemId);

        GNET.CBuySmShop buyProtocol = new GNET.CBuySmShop();
        buyProtocol.smshopid = currentItemId;
        IOControler.GetInstance().SendProtocol(buyProtocol);

        //foreach (var temp in itemDataList.Values)
        //{
        //    if (temp.id == currentItemId)
        //    {
        //        GNET.CBuySmShop buyProtocol = new GNET.CBuySmShop();
        //        buyProtocol.smshopid = 
        //    }
        
        //}
    }
    protected override void OnClickConfirmCloseBtn()
    {
        confirmPanel.SetActive(false);
    }



}
