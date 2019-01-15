using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using System.Text;
using DreamFaction.GameEventSystem;
using UnityEngine.Events;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using DreamFaction.UI;
using System.Collections.Generic;
using GNET;
using DreamFaction.LogSystem;

public class UI_PowersAdd : BaseUI 
{
    public static UI_PowersAdd _inst;
    public static string UI_ResPath = "UI_Home/UI_PowersAdd_2_2";

    private Button _CloseBtn;           //关闭按钮
    private Button _AddBaoShi;          //宝石按钮
    private Text _DiamondConNumTxt;     //钻石数量
    private Text _CurrentPowerTxt;      //当前体力
    private Text _MaxPowerTxt;          //最大体力
    //今日剩余使用次数
    private Text _SurplusNumTxt_0;      
    private Text _SurplusNumTxt_1;
    private Text _SurplusNumTxt_2;
    private Text _SurplusNumTxt_3;  
    //活力Item图标
    private Image _Icon_0;
    private Image _Icon_1;
    private Image _Icon_2;
    private Image _Icon_3;

    //使用按钮
    private Button _UserBtn_0;
    private Button _UserBtn_1;
    private Button _UserBtn_2;
    private Button _UserBtn_3;
    //需要消耗的钻石个数
    private Text _ConDiamNumTxt;

    //拥有道具的个数
    private Text _OwnNumTxt_1;
    private Text _OwnNumTxt_2;
    private Text _OwnNumTxt_3;

    //当前使用次数
    private int m_curSurpNum_0;
    private int m_curSurpNum_1;
    private int m_curSurpNum_2;
    private int m_curSurpNum_3;

    //道具个数
    private int m_conItem_1;
    private int m_conItem_2;
    private int m_conItem_3;
    private int goods;
    private int m_conDiamNum = 10;

    //道具名字
    private Text m_NameTxt_0;
    private Text m_NameTxt_1;
    private Text m_NameTxt_2;
    private Text m_NameTxt_3;

    //道具描述
    private Text m_DesTxt_0;
    private Text m_DesTxt_1;
    private Text m_DesTxt_2;
    private Text m_DesTxt_3;

    //top 活力补充 Text
    private Text m_TopName;

    private Text m_ShengYu_1; //今日剩余
    private Text m_ShengYu_2;
    private Text m_ShengYu_3;
    private Text m_ShengYu_4;

    private Text m_YongYou_2;//拥有
    private Text m_YongYou_3;
    private Text m_YongYou_4;

    private Text m_GouMai;//购买
    private Text m_Use_2;//使用
    private Text m_Use_3;
    private Text m_Use_4;

    private X_GUID m_guid_1;
    private X_GUID m_guid_2;
    private X_GUID m_guid_3;
    private VipTemplate vipData;
    private GameConfig config;

    private Transform M_CapPos;
    public override void InitUIData()
    {
        base.InitUIData();
        _DiamondConNumTxt = selfTransform.FindChild("TopPanel/UI_Diamond/Text").GetComponent<Text>();
        _AddBaoShi = selfTransform.FindChild("TopPanel/UI_Diamond/AddBtn").GetComponent<Button>();
        _CurrentPowerTxt = selfTransform.FindChild("TopPanel/UI_Powers/CurrentpowTxt").GetComponent<Text>();
        _MaxPowerTxt = selfTransform.FindChild("TopPanel/UI_Powers/MaxpowTxt").GetComponent<Text>();
        _CloseBtn = selfTransform.FindChild("TopPanel/CloseBtn").GetComponent<Button>();
        //剩余次数
        _SurplusNumTxt_0 = selfTransform.FindChild("PowersAddBox/PowersItem_1/SurplusNum").GetComponent<Text>();
        _SurplusNumTxt_1 = selfTransform.FindChild("PowersAddBox/PowersItem_2/SurplusNum").GetComponent<Text>();
        _SurplusNumTxt_2 = selfTransform.FindChild("PowersAddBox/PowersItem_3/SurplusNum").GetComponent<Text>();
        _SurplusNumTxt_3 = selfTransform.FindChild("PowersAddBox/PowersItem_4/SurplusNum").GetComponent<Text>();
        //使用按钮
        _UserBtn_0 = selfTransform.FindChild("PowersAddBox/UseBtn_1").GetComponent<Button>();
        _UserBtn_1 = selfTransform.FindChild("PowersAddBox/UseBtn_2").GetComponent<Button>();
        _UserBtn_2 = selfTransform.FindChild("PowersAddBox/UseBtn_3").GetComponent<Button>();
        _UserBtn_3 = selfTransform.FindChild("PowersAddBox/UseBtn_4").GetComponent<Button>();
        //活力Item图标
        _Icon_0 = selfTransform.FindChild("PowersAddBox/PowersItem_1/itemicon").GetComponent<Image>();
        _Icon_1 = selfTransform.FindChild("PowersAddBox/PowersItem_2/itemicon").GetComponent<Image>();
        _Icon_2 = selfTransform.FindChild("PowersAddBox/PowersItem_3/itemicon").GetComponent<Image>();
        _Icon_3 = selfTransform.FindChild("PowersAddBox/PowersItem_4/itemicon").GetComponent<Image>();
        //消耗钻石
        _ConDiamNumTxt = selfTransform.FindChild("PowersAddBox/ConDiamondIcon/Text").GetComponent<Text>();
        //活力道具拥有个数
        _OwnNumTxt_1 = selfTransform.FindChild("PowersAddBox/PowersItem_2/OwnNum").GetComponent<Text>();
        _OwnNumTxt_2 = selfTransform.FindChild("PowersAddBox/PowersItem_3/OwnNum").GetComponent<Text>();
        _OwnNumTxt_3 = selfTransform.FindChild("PowersAddBox/PowersItem_4/OwnNum").GetComponent<Text>();

        //道具名字text
        m_NameTxt_0 = selfTransform.FindChild("PowersAddBox/PowersItem_1/Image/Text").GetComponent<Text>();
        m_NameTxt_1 = selfTransform.FindChild("PowersAddBox/PowersItem_2/Image/Text").GetComponent<Text>();
        m_NameTxt_2 = selfTransform.FindChild("PowersAddBox/PowersItem_3/Image/Text").GetComponent<Text>();
        m_NameTxt_3 = selfTransform.FindChild("PowersAddBox/PowersItem_4/Image/Text").GetComponent<Text>();

        //道具描述text
        m_DesTxt_0 = selfTransform.FindChild("PowersAddBox/PowersItem_1/Image/DesText").GetComponent<Text>();
        m_DesTxt_1 = selfTransform.FindChild("PowersAddBox/PowersItem_2/Image/DesText").GetComponent<Text>();
        m_DesTxt_2 = selfTransform.FindChild("PowersAddBox/PowersItem_3/Image/DesText").GetComponent<Text>();
        m_DesTxt_3 = selfTransform.FindChild("PowersAddBox/PowersItem_4/Image/DesText").GetComponent<Text>();

        m_TopName = selfTransform.FindChild("TopPanel/TitleButton_0/Text").GetComponent<Text>();

        m_ShengYu_1 = selfTransform.FindChild("PowersAddBox/PowersItem_1/Text").GetComponent<Text>();
        m_ShengYu_2 = selfTransform.FindChild("PowersAddBox/PowersItem_2/Text").GetComponent<Text>();
        m_ShengYu_3 = selfTransform.FindChild("PowersAddBox/PowersItem_3/Text").GetComponent<Text>();
        m_ShengYu_4 = selfTransform.FindChild("PowersAddBox/PowersItem_4/Text").GetComponent<Text>();

        m_YongYou_2 = selfTransform.FindChild("PowersAddBox/PowersItem_2/Text_2").GetComponent<Text>();
        m_YongYou_3 = selfTransform.FindChild("PowersAddBox/PowersItem_3/Text_2").GetComponent<Text>();
        m_YongYou_4 = selfTransform.FindChild("PowersAddBox/PowersItem_4/Text_2").GetComponent<Text>();

        m_GouMai = selfTransform.FindChild("PowersAddBox/UseBtn_1/Text").GetComponent<Text>();
        m_Use_2 = selfTransform.FindChild("PowersAddBox/UseBtn_2/Text").GetComponent<Text>();
        m_Use_3 = selfTransform.FindChild("PowersAddBox/UseBtn_3/Text").GetComponent<Text>();
        m_Use_4 = selfTransform.FindChild("PowersAddBox/UseBtn_4/Text").GetComponent<Text>();

        M_CapPos = selfTransform.FindChild("pos");
        //
        _CloseBtn.onClick.AddListener(new UnityAction(OnClose));
        _AddBaoShi.onClick.AddListener(new UnityAction(OnAddBaoShi));
        _UserBtn_0.onClick.AddListener(new UnityAction(OnclickUserBtn_0));
        _UserBtn_1.onClick.AddListener(new UnityAction(OnclickUserBtn_1));
        _UserBtn_2.onClick.AddListener(new UnityAction(OnclickUserBtn_2));
        _UserBtn_3.onClick.AddListener(new UnityAction(OnclickUserBtn_3));

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ActionPoint_Update, UpdatePower);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Gold_Update, UpdateDiamond);
        GameEventDispatcher.Inst.addEventListener(GameEventID.KE_ModItemNum, InitPowerItemUI);
        GameEventDispatcher.Inst.addEventListener(GameEventID.KE_KnapsackUpdateShow, InitPowerItemUI);
        GameEventDispatcher.Inst.addEventListener(GameEventID.U_RefreshShopInfo, InitPowerShopUI);
        
    }


    public override void InitUIView()
    {
        base.InitUIView();

        m_ShengYu_1.text = GameUtils.getString("vigour_supplement_content2");
        m_ShengYu_2.text = GameUtils.getString("vigour_supplement_content2");
        m_ShengYu_3.text = GameUtils.getString("vigour_supplement_content2");
        m_ShengYu_4.text = GameUtils.getString("vigour_supplement_content2");

        m_YongYou_2.text = GameUtils.getString("vigour_supplement_content3");
        m_YongYou_3.text = GameUtils.getString("vigour_supplement_content3");
        m_YongYou_4.text = GameUtils.getString("vigour_supplement_content3");

        m_GouMai.text = GameUtils.getString("common_button_purchase");
        m_Use_2.text = GameUtils.getString("common_button_use2");
        m_Use_3.text = GameUtils.getString("common_button_use2");
        m_Use_4.text = GameUtils.getString("common_button_use2");

        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.AwakeUp(M_CapPos);

        vipData = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(ObjectSelf.GetInstance().VipLevel);
        config = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
        UpdatePower();
        UpdateDiamond();
        InitPowerShopUI();
        InitPowerItemUI();
    }



    //体力
    private void UpdatePower()
    {
        int CurrentPower = ObjectSelf.GetInstance().ActionPoint;
        int MaxPower = ObjectSelf.GetInstance().ActionPointMax;
        StringBuilder StrTmp = new StringBuilder();
        StrTmp.Append("/");
        StrTmp.Append(MaxPower);
        _CurrentPowerTxt.text = CurrentPower.ToString();
        _MaxPowerTxt.text = StrTmp.ToString();
        if (CurrentPower >= 200)
        {
            _CurrentPowerTxt.color = Color.red;
        }
        else if (CurrentPower >= MaxPower)
        {
            _CurrentPowerTxt.color = Color.yellow;
        }
        else
        {
            _CurrentPowerTxt.color = Color.white;
        }
    }

    //钻石
    private void UpdateDiamond()
    {
        int Diam = ObjectSelf.GetInstance().Gold;
        _DiamondConNumTxt.text = Diam.ToString();
    }


    /// <summary>
    /// 活力商品显示
    /// </summary>
    private void InitPowerShopUI()
    {
        int surplusNum_0 = 0;//可用次数
        goods = config.getAp_supplement_goods();//活力补满商品的ID
        ShopTemplate shopDate = (ShopTemplate)DataTemplate.GetInstance().m_ShopTable.getTableData(goods);
        if (shopDate == null)
        {
            LogManager.Log("is Shop null !!!!");
            return;
        }
        m_NameTxt_0.text = GameUtils.getString(shopDate.getCommodityName());
        m_DesTxt_0.text = GameUtils.getString(shopDate.getCommodityDes());

        //商品使用次数+Vip的使用次数
        surplusNum_0 = shopDate.getDailyMaxBuy() + vipData.getMaxBuyAp();
        //剩余使用次数
        Shopbuy shop =  ObjectSelf.GetInstance().GetShopBuyInfoByShopId(goods);
        m_curSurpNum_0 = surplusNum_0 - shop.todaynum;

        _SurplusNumTxt_0.text = m_curSurpNum_0.ToString();
        _Icon_0.sprite = UIResourceMgr.LoadSprite(common.defaultPath + shopDate.getResourceName());
        
        //消耗钻石显示  
        m_conDiamNum = DataTemplate.GetInstance().GetShopBuyCost(shopDate, shop.todaynum);
        _ConDiamNumTxt.text = m_conDiamNum.ToString();

        SetBtnColor(m_curSurpNum_0, _UserBtn_0, ObjectSelf.GetInstance().Gold);
    }

    /// <summary>
    /// 活力道具显示
    /// </summary>
    private void InitPowerItemUI()
    {
        m_guid_1 = null;
        m_guid_2 = null;
        m_guid_3 = null;
        m_conItem_1 = 0;
        m_conItem_2 = 0;
        m_conItem_3 = 0;
        int surplusNum_1 = 0, surplusNum_2 = 0, surplusNum_3 = 0;//道具可使用次数
        int conNum1 = 0, conNum2 = 0, conNum3 = 0;//使用次数

        //三种消耗道具
        int[] ConItems = config.getAp_supplement_item();
        ItemTemplate item1 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(ConItems[0]);
        ItemTemplate item2 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(ConItems[1]);
        ItemTemplate item3 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(ConItems[2]);
        m_NameTxt_1.text = GameUtils.getString(item1.getName());
        m_DesTxt_1.text = GameUtils.getString(item1.getDes());
        m_NameTxt_2.text = GameUtils.getString(item2.getName());
        m_DesTxt_2.text = GameUtils.getString(item2.getDes());
        m_NameTxt_3.text = GameUtils.getString(item3.getName());
        m_DesTxt_3.text = GameUtils.getString(item3.getDes());
        _Icon_1.sprite = UIResourceMgr.LoadSprite(common.defaultPath + item1.getIcon());
        _Icon_2.sprite = UIResourceMgr.LoadSprite(common.defaultPath + item2.getIcon());
        _Icon_3.sprite = UIResourceMgr.LoadSprite(common.defaultPath + item3.getIcon());
        List<BaseItem> ItemList = ObjectSelf.GetInstance().CommonItemContainer.GetItemList(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON);
        for (int i = 0; i < ItemList.Count; ++i)
        {
            int baseItemId = ItemList[i].GetItemTableID();
            if (baseItemId == ConItems[0])
            {
                conNum1 = ItemList[i].GetItemTimesCount();
                m_guid_1 = ItemList[i].GetItemGuid();
                int tempNum = ItemList[i].GetItemCount();
                m_conItem_1 += tempNum;
            }
            if (baseItemId == ConItems[1])
            {
                conNum2 = ItemList[i].GetItemTimesCount();
                m_guid_2 = ItemList[i].GetItemGuid();
                int tempNum = ItemList[i].GetItemCount();
                m_conItem_2 += tempNum;
            }
            if (baseItemId == ConItems[2])
            {
                conNum3 = ItemList[i].GetItemTimesCount();
                m_guid_3 = ItemList[i].GetItemGuid();
                int tempNum = ItemList[i].GetItemCount();
                m_conItem_3 += tempNum;
            }            
        }
        //道具数量
        _OwnNumTxt_1.text = SetOwnShow(m_conItem_1);
        _OwnNumTxt_2.text = SetOwnShow(m_conItem_2);
        _OwnNumTxt_3.text = SetOwnShow(m_conItem_3);
        //道具可以使用的次数  道具使用次数+Vip的使用次数
        surplusNum_1 = item1.getIfUse() + vipData.getMaxUseApPotion();
        surplusNum_2 = item2.getIfUse() + vipData.getMaxUseApPotion();
        surplusNum_3 = item3.getIfUse() + vipData.getMaxUseApPotion();        
        //当前剩余次数
        m_curSurpNum_1 = surplusNum_1 - conNum1;
        m_curSurpNum_2 = surplusNum_2 - conNum2;
        m_curSurpNum_3 = surplusNum_3 - conNum3;
        _SurplusNumTxt_1.text = m_curSurpNum_1.ToString();
        _SurplusNumTxt_2.text = m_curSurpNum_2.ToString();
        _SurplusNumTxt_3.text = m_curSurpNum_3.ToString();
        //设置按钮是否置灰
        SetBtnColor(m_curSurpNum_1, _UserBtn_1, m_conItem_1);
        SetBtnColor(m_curSurpNum_2, _UserBtn_2, m_conItem_2);
        SetBtnColor(m_curSurpNum_3, _UserBtn_3, m_conItem_3);
    }


    //使用按钮1
    private void OnclickUserBtn_0()
    {
        int curPower = ObjectSelf.GetInstance().ActionPoint;
        int maxPower = ObjectSelf.GetInstance().ActionPointMax;
        if (curPower >= maxPower)
        {
            string text = GameUtils.getString("vigour_supplement_tip2");
            InterfaceControler.GetInst().AddMsgBox(text, transform);
        }
        else
        {
            if (m_curSurpNum_0 > 0 && ObjectSelf.GetInstance().Gold > m_conDiamNum)
            {
                CShopBuy cshop = new CShopBuy();
                cshop.shopid = goods;
                cshop.num = 1;
                cshop.isdiscount = 0;
                IOControler.GetInstance().SendProtocol(cshop);
            }
            NotSurplus(m_curSurpNum_0, _UserBtn_0, ObjectSelf.GetInstance().Gold,true);
        }
    }
    //使用按钮2
    private void OnclickUserBtn_1()
    {
        if (m_curSurpNum_1 > 0 && m_guid_1 !=null)
        {
            SendMsg(m_guid_1,1);
        }
        NotSurplus(m_curSurpNum_1, _UserBtn_1, m_conItem_1);
    }
    //使用按钮3
    private void OnclickUserBtn_2()
    {
        if (m_curSurpNum_2 > 0 && m_guid_2 !=null)
        {
            SendMsg(m_guid_2, 1);
        }
        NotSurplus(m_curSurpNum_2, _UserBtn_2, m_conItem_2);
    }
    //使用按钮4
    private void OnclickUserBtn_3()
    {
        if (m_curSurpNum_3 > 0 && m_guid_3 != null)
        {
            SendMsg(m_guid_3, 1);
        }
        NotSurplus(m_curSurpNum_3, _UserBtn_3, m_conItem_3);
    }


    //没有剩余,或者道具为空时 弹出提示
    private void NotSurplus(int num, Button btn, int itemCount,bool isItemOrShop=false)
    {
        SetBtnColor(num,btn,itemCount);
        if (num <= 0)
        {
            string text = GameUtils.getString("vigour_supplement_tip1");
            InterfaceControler.GetInst().AddMsgBox(text, transform);
        }
        if(itemCount <= 0)
        {
            if (isItemOrShop)
            {
                //弹出充值窗口
                InterfaceControler.GetInst().ShowGoldNotEnougth(transform);
            }
            else
            {
                string text = GameUtils.getString("vigour_supplement_tip3");
                InterfaceControler.GetInst().AddMsgBox(text, transform);
            }
        }
    }

    //设置按钮颜色
    private void SetBtnColor(int num, Button btn,int itemCount)
    {
        if (num <= 0 || itemCount <= 0)
        {
            GameUtils.SetBtnSpriteGrayState(btn,true);
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="count"></param>
    private void SendMsg(X_GUID guid,int count)
    {
        CUseItem msg = new CUseItem();
        msg.bagid = (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON;
        msg.itemkey = (int)guid.GUID_value;
        msg.num = (short)count;
        IOControler.GetInstance().SendProtocol(msg);
    }





    ///处理当前拥有道具个数是否大于999
    private string SetOwnShow(int OwnNum)
    {
        if (OwnNum <= 999)
        {
            return OwnNum.ToString();
        }
        else
        {
            return "999+";
        }
    }

    //关闭按钮
    private void OnClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
    /// <summary>
    /// 弹出快捷充值界面
    /// </summary>
    private void OnAddBaoShi()
    {
        UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
    }
    void OnDestroy()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(M_CapPos);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ActionPoint_Update, UpdatePower);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Gold_Update, UpdateDiamond);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_RefreshShopInfo, InitPowerShopUI);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.KE_ModItemNum, InitPowerItemUI);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.KE_KnapsackUpdateShow, InitPowerItemUI);
    }
}
