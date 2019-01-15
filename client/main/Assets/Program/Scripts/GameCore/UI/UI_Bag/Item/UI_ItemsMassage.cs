using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using DG.Tweening;
using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
public class UI_ItemsMassage : BaseUI
{
    public static UI_ItemsMassage _instance;
    public GameObject uiSell;
    public Button useBtn;
    private Button sellBtn;
    private Text nameText;
    private Image iconImage;
    private Text desText;
    private Text priceText;
    private GameObject price;
    private Text surplusText;
    private GameObject surplus;
    private Text numberText;
    private Image sellImage;
    private GameObject unSell;
    private GameObject sell;

    public int objID;
    private BaseItem baseIrem;
    public ItemTemplate item;
    private int surplusNum;
    private GameObject use_count;
    private GameObject use_normal;
    private GameObject unuse_normal;
    private GameObject unuse_count;
    private GameObject[] bts;
    private int[] m_SpecialID=new int[6];//需要特殊处理的itemId
    //*************10.24日改动*********
    private Transform m_New;
    private Transform m_NoUseWarring; //不可使用时警告文本
    private Image m_SellImage; //出售按钮背景
    private Button m_IconButton; //点击图标能弹出物品信息
    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        m_New = selfTransform.FindChild("new/Left");
        nameText = m_New.FindChild("Skillname/Text_Essencewater").GetComponent<Text>();
        iconImage = m_New.FindChild("Skillname/Rect_Icon").GetComponent<Image>();
        desText = m_New.FindChild("Propcontent/Text_Propdetails").GetComponent<Text>();
        m_NoUseWarring = m_New.FindChild("Sell/Text_Nosale");
        m_NoUseWarring.gameObject.SetActive(false);
        m_SellImage = m_New.FindChild("Sell/Sell").GetComponent<Image>();
        m_IconButton = m_New.FindChild("Skillname/Rect_Icon").GetComponent<Button>();
        m_IconButton.onClick.AddListener(OnClickIcon);
        priceText = selfTransform.FindChild("UI_Btn_Intensify/SellPrice/Text").GetComponent<Text>();
        price = selfTransform.FindChild("UI_Btn_Intensify/SellPrice").gameObject;
        surplusText = selfTransform.FindChild("UI_Btn_Authenticate/UI_Text_Surplus/Text").GetComponent<Text>();
        surplus = selfTransform.FindChild("UI_Btn_Authenticate/UI_Text_Surplus").gameObject;
        sellBtn = m_New.FindChild("Sell/Sell").GetComponent<Button>();
        sellBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnSellClick));
        useBtn = m_New.FindChild("Sell/Use").GetComponent<Button>();
        useBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnUseClick));
        numberText = m_New.FindChild("Skillname/count").GetComponent<Text>();
        sellImage = selfTransform.FindChild("UI_Btn_Intensify").GetComponent<Image>();
        unSell = selfTransform.FindChild("UI_Btn_Intensify/unSell").gameObject;
        sell = selfTransform.FindChild("UI_Btn_Intensify/Sell").gameObject;
        use_count = selfTransform.FindChild("UI_Btn_Authenticate/Use(count)").gameObject;
        use_normal = selfTransform.FindChild("UI_Btn_Authenticate/Use(normal)").gameObject;
        unuse_normal = selfTransform.FindChild("UI_Btn_Authenticate/unUse(normal)").gameObject;
        unuse_count = selfTransform.FindChild("UI_Btn_Authenticate/unUse(count)").gameObject;


        //UpdateShow(0,UI_ItemsManage._instance.itemList[0].transform.GetComponent<UI_ItemsButtonMassage>().item);    
        bts = new GameObject[4];
        bts[0] = unuse_normal;
        bts[1] = unuse_count;
        bts[2] = use_normal;
        bts[3] = use_count;
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_SpecialID[0] = 1402030001;
        m_SpecialID[1] = 1402030002;
        m_SpecialID[2] = 1402030003;
        m_SpecialID[3] = 1402030005;
        m_SpecialID[4] = 1402030006;
        m_SpecialID[5] = 1402030007;

    }
    public void UpdateShow(BaseItem itemtable)
    {
        //更新显示
        baseIrem = itemtable;
        item = baseIrem.GetItemRowData();
        nameText.text = GameUtils.getString(item.getName());
        SetTextColorByQuilt(baseIrem.GetItemRowData().getQuality());
        iconImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
        desText.text = GameUtils.getString(item.getDes());
        numberText.text = "拥有数量：" + baseIrem.GetItemCount().ToString();
        if (item.getIfSell() == 0)
        {
            price.SetActive(false);
            unSell.SetActive(true);
            sell.SetActive(false);
            GameUtils.SetImageGrayState(m_SellImage,true);
            m_NoUseWarring.gameObject.SetActive(true);

        }
        else
        {
            price.SetActive(true);
            unSell.SetActive(false);
            sell.SetActive(true);
            m_NoUseWarring.gameObject.SetActive(false);
            GameUtils.SetImageGrayState(m_SellImage, false);
            priceText.text = item.getSellPrice().ToString();
        }

        Surplus();
    }
    /// <summary>
    /// 是否是需要是特殊处理的
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool IsSpeciaclID(int id)
    { 
        bool resut=false;
        for (int i = 0; i < m_SpecialID.Length; i++)
        {
            if (id == m_SpecialID[i])
            {
                resut = true; 
            }
        }
        return resut;
    }
    public void Surplus()
    {
        //不限制使用次数  但是 活力药水和体力药剂 要经过特殊处理
        if (item.getIfUse() == 0 && !IsSpeciaclID(item.GetID()))   //不限制使用次数
        {
           //surplus.SetActive(false);
            surplus.SetActive(false);
            SetBtShow(2);
        }
        else
        {
            if (objID >= 0 || objID < UI_ItemsManage._instance.item.Count)
            {
                surplus.SetActive(true);
                int maxUseCount;
                if (IsSpeciaclID(item.GetID()))
                {
                    VipTemplate vipData = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(ObjectSelf.GetInstance().VipLevel);
                    maxUseCount = vipData.getMaxUseApPotion() + item.getIfUse();
                }
                else
                {
                    maxUseCount = item.getIfUse();
                }
                surplusNum = maxUseCount - baseIrem.GetItemTimesCount();
                //surplusText.text = surplusNum.ToString();
                surplus.SetActive(true);
                SetBtShow(3);


                if (surplusNum <= 0)
                {
                    surplusText.text = "<color=#B0B0B0>0</color>";
                    SetBtShow(1);

                }
                else
                {
                    surplusText.text = string.Format("<color=#76EE00>{0}</color>", surplusNum);
                    SetBtShow(3);
                }
                if (item.getType() == 1)
                {
                    surplus.SetActive(false);
                    SetBtShow(0);
                }
                else
                {
                    surplus.SetActive(true);
                }
                if (item.getType() == 0)
                {
                    surplus.SetActive(false);
                    SetBtShow(2);
                }          
            }
        }
      
  
    }

    private void OnUseClick()
    {
        if (((ItemFragment)baseIrem).GetItemType() == (int)EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT) //是碎片的话 需要跳转界面
        {
            JumpUI();
            return;
        }
        UI_ItemsManage._instance.recordX_GUID = baseIrem.GetItemGuid();
        //UI_ItemsManage._instance.recordGrid = UI_ItemsManage._instance.mGrid;
        if (item.GetID() != 1402030009) //不是改名卡 yao 7/2/2015
        {
            //useBtn.interactable = false;
        }
        UseItem();
    }

    private void UseItem()
    {
        if (item.GetID() == 1402030009) //添加改名卡 yao 7/2/2015
        {
            if (item.getType() == 1)
            {
                //材料类道具，不可使用
                // UI_Bag._instance.AddMsgBox(GameUtils.getString("bag_item_tip1"));
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("bag_item_tip1"));

            }
            else if (item.getIfUse() != 0)//当日是否可用
            {
                if (surplusNum == 0)
                {
                    //UI_Bag._instance.AddMsgBox(GameUtils.getString("bag_item_tip3"));
                    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("bag_item_tip3"));
                }
                else if (item.getUseTips() == "-1")//使用后提示
                {
                    Surplus();
                }
                else
                {
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, ChangeNameCard.UI_ResPath);
                    return;
                }
            }
        }
        else
        {
            if (item.getType() == 1)
            {
                //材料类道具，不可使用
                // UI_Bag._instance.AddMsgBox(GameUtils.getString("bag_item_tip1"));
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("bag_item_tip1"));
                useBtn.interactable = true;
            }
            else
            {
                if (item.getIfUse() == 0 && !IsSpeciaclID(item.GetID()))//不限制使用次数
                {
                    UI_ItemsManage._instance.isShowItemGetWindow = false;
                    baseIrem.OnUseItem(1);
                    string des = GameUtils.getString(item.getUseTips());
                    // UI_Bag._instance.AddMsgBox(des);
                    InterfaceControler.GetInst().AddMsgBox(des);
                    Surplus();
                }
                else //限制使用次数包括需要特殊工处理的活力药剂和体力药剂
                {
                    if (surplusNum <= 0)
                    {

                        //UI_Bag._instance.AddMsgBox(GameUtils.getString("bag_item_tip3"));
                        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("bag_item_tip3"));
                        useBtn.interactable = true;
                    }
                    else if (string.IsNullOrEmpty(item.getUseTips()))//使用后提示 -1
                    {
                        UI_ItemsManage._instance.isShowItemGetWindow = true;
                        baseIrem.OnUseItem(1);
                        Surplus();
                    }
                    else
                    {
                        UI_ItemsManage._instance.isShowItemGetWindow = false;
                        baseIrem.OnUseItem(1);
                        string des = GameUtils.getString(item.getUseTips());
                        // UI_Bag._instance.AddMsgBox(des);
                        InterfaceControler.GetInst().AddMsgBox(des);
                        Surplus();
                    }
                }
            }
        }
    }

    private void OnSellClick()
    {
        if (item.getIfSell() == 0)
        {
           // UI_Bag._instance.AddMsgBox(GameUtils.getString("bag_item_tip4"));
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("bag_item_tip4"));
        }
        else
        {
            //uiSell= UIResourceMgr.LoadPrefab(common.prefabPath + UI_SellManage.ui_ResPath) as GameObject;
            uiSell= UI_HomeControler.Inst.AddUI(UI_SellManage.ui_ResPath);
            uiSell.GetComponent<UI_SellManage>().UpdateShow(objID, baseIrem);
            //UI_Bag._instance.SetMoneyBarShowOrHide(false);
        
        }

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void SetBtShow(int index)
    {
        for (int i = 0; i < bts.Length; i++)
        {
            if (i == index)
                bts[i].SetActive(true);
            else
                bts[i].SetActive(false);
        }
    }
    void OnClickIcon()
    {
        UICommonManager.Inst.ShowHeroObtain(baseIrem.GetItemTableID());
    }
    void SetTextColorByQuilt(int quilt)
    {
        switch (quilt)
        {
            case 1:
            case 0:
                nameText.color = new Color(1, 1, 1);
                break;
            case 2:
                nameText.color = new Color(15 / 255f, 208 / 255f, 0);
                break;
            case 3:
                nameText.color = new Color(0, 155 / 255f, 1);
                break;
            case 4:
                nameText.color = new Color(174 / 255f, 0, 1);
                break;
            case 5:
                nameText.color = new Color(1, 144 / 255f, 0);
                break;
            case 6:
                nameText.color = new Color(1, 0, 0);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 跳转界面
    /// </summary>
    void JumpUI()
    { 
      ObjectCard heroCard= ObjectSelf.GetInstance().HeroContainerBag.FindHero(((ItemFragment)baseIrem).GetComposeHeoid());
      if(heroCard==null)
      {
         InterfaceControler.GetInst().AddMsgBox("尚未获得此英雄");
          return;
      }
       ItemTemplate _item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(baseIrem.GetItemTableID());
       PropsjumpuiTemplate _jump=(PropsjumpuiTemplate) DataTemplate.GetInstance().m_PropsjumpuiTable.getTableData(_item.getUsejumpType());
       HeroStrengthen _panel = UI_HomeControler.Inst.AddUI(_jump.getJumpUIpath()).GetComponent<HeroStrengthen>();
       if (_panel != null)
       {
           Debug.Log("打开品质提升界面");
           _panel.OnClickHeroIcon(heroCard);
           _panel.ClickSwitchBtn("UI_QualityProUI", 1);
       }
       else
       {
           Debug.LogError("加载界面失败:"+_jump.getJumpUIpath());
       }

    }
}
