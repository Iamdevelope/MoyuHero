using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using System.Collections.Generic;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameEventSystem;
using UnityEngine.UI;
using DreamFaction.GameNetWork.Data;

public class UI_MysticPopWindow : HeroAttrPanel
{
    public static string UI_ResPath = "HeroStrengthen/UI_MysticPopWin_1_2";

    protected Button m_PopCloseButton;
    protected Button m_UpgradeButton;
    protected Image m_Pop_icon;
    protected Text m_Pop_iconName;
    protected Text m_PopLevelText;
    protected Text m_PopInfoText;
    protected Text m_PopInfoValueText;
    protected Text m_UpgradeInfoText;
    protected Text m_ShengJiText;
    protected LoopLayout m_ItemBannerLayout;
    protected Slider m_ExpSlider;
    protected Text m_CostText;
    protected Text m_LvAddText;
    protected Text m_InfoAddText;
    protected Text m_SliderTopText;
    protected Text m_MaxLevelInfoText;

    private ObjectCard m_objectCard;
    private HeroData m_HeroData;
    private HeroTemplate m_HeroDataT;
    private MsTemplate m_CurMysticTData;
    private int m_MysticId = -1;//选中的是第几个秘籍 从0 开始
    private int m_LevelIndex;//选中秘籍中 指定等级的下标
    public List<BaseItem> item = new List<BaseItem>();
    public List<MysticItemData> items = new List<MysticItemData>();
    private BaseItem SelectItem; //所选中的item

    private int m_AddItemNum = 0;//添加物品的数量  初始为0
    private int m_FashCurExpValue;//当前的经验值
    private bool ClearBaseValue = false;//升级后 再计算经验值 要清除基础的经验值

    private int m_CostNum;//花费时等级的增加
    private int m_CostTotalMoney;
    private int m_CostTotalExp;
    private int LevelMaxExp;//等级的最大经验值
    private int m_AllExp;//只累加 不参与计算

    private Dictionary<int, int> m_AddItemData = new Dictionary<int, int>();//合并后添加的item数据
    private LinkedList<int> m_ItemTableKeyList = new LinkedList<int>();
    private LinkedList<int> m_ItemNumList = new LinkedList<int>();

    public override void InitUIData()
    {
        base.InitUIData();
        m_PopCloseButton = selfTransform.FindChild("Window/closeButton").GetComponent<Button>();
        m_PopCloseButton.onClick.AddListener(OnClickPopButton);
        m_Pop_icon = selfTransform.FindChild("Window/Pop_icon/Image").GetComponent<Image>();
        m_Pop_iconName = selfTransform.FindChild("Window/Pop_icon/Text").GetComponent<Text>();
        m_PopLevelText = selfTransform.FindChild("Window/LvText").GetComponent<Text>();
        m_PopInfoText = selfTransform.FindChild("Window/InfoText").GetComponent<Text>();
        m_PopInfoValueText = selfTransform.FindChild("Window/InfoValueText").GetComponent<Text>();
        m_UpgradeInfoText = selfTransform.FindChild("Window/Button/UpgradeInfoText").GetComponent<Text>();
        m_MaxLevelInfoText = selfTransform.FindChild("Window/Button/MaxLevelInfoText").GetComponent<Text>();
        m_ShengJiText = selfTransform.FindChild("Window/Button/UpgradeButton/Text").GetComponent<Text>();
        m_ItemBannerLayout = selfTransform.FindChild("Window/wuPingList/wuPingLayout").GetComponent<LoopLayout>();
        m_ExpSlider = selfTransform.FindChild("Window/LivenessSlider").GetComponent<Slider>();
        m_CostText = selfTransform.FindChild("Window/Button/UpgradeButton/num").GetComponent<Text>();
        m_LvAddText = selfTransform.FindChild("Window/LvAddText").GetComponent<Text>();
        m_InfoAddText = selfTransform.FindChild("Window/InfoAddText").GetComponent<Text>();
        m_SliderTopText = selfTransform.FindChild("Window/Slider2/cur").GetComponent<Text>();
        m_UpgradeButton = selfTransform.FindChild("Window/Button/UpgradeButton").GetComponent<Button>();
        m_UpgradeButton.onClick.AddListener(OnClickUpgradeButton);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_MysticSuccess, MysticSuccess);
    }

    public override void InitUIView()
    {
         base.InitUIView();
         m_MaxLevelInfoText.text = GameUtils.getString("ui_yingxiongqianghua_mishu7");
         m_ShengJiText.text = GameUtils.getString("ui_yingxiongqianghua_mishu4");
    }
    public override void ShowHeroInfo(ObjectCard _objectCard)
    {
        m_objectCard = _objectCard;
        m_HeroData = _objectCard.GetHeroData();
        m_HeroDataT = _objectCard.GetHeroRow();
        PopMysticWindow(m_MysticId, _objectCard);
    }

    public void PopMysticWindow(int mysticId, ObjectCard _objectCard)
    {
        m_objectCard = _objectCard;
        m_HeroData = _objectCard.GetHeroData();
        m_HeroDataT = _objectCard.GetHeroRow();
        if (m_MysticId != mysticId)
        {
            m_MysticId = mysticId;

            MsTemplate MysticDataT = (MsTemplate)DataTemplate.GetInstance().m_MsTable.getTableData(m_HeroData.HeroCabalaDB.CabalaList[mysticId].TableID);
            m_CurMysticTData = MysticDataT;           
        }
        m_LevelIndex = GetLevelIndex(m_HeroData.HeroCabalaDB.CabalaList[mysticId].IntensifyLev);

        GreatItem();
        ShowPopWindInfo();
        ShowExpValue();
    }

    protected  void OnClickUpgradeButton()
    {
        m_ItemTableKeyList.Clear();
        m_ItemNumList.Clear();
        if (ObjectSelf.GetInstance().Money < m_CostTotalMoney)
        {
            UI_HomeControler.Inst.AddUI(UI_QuikBuyGoldMgr.UI_ResPath);
            return;
        }

        foreach (KeyValuePair<int, int> kvp in m_AddItemData)
        {
            m_ItemTableKeyList.AddLast(kvp.Key);
            m_ItemNumList.AddLast(kvp.Value);
        }

        if (m_ItemTableKeyList.Count == 0 || m_ItemNumList.Count == 0)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_mishu8"), this.gameObject.transform);
            return;
        }

        CHeroMSExp _CHeroMSExp = new CHeroMSExp();
        _CHeroMSExp.herokey = (int)m_HeroData.GUID.GUID_value;
        _CHeroMSExp.mslocation = m_MysticId + 1;
        _CHeroMSExp.itemidlist = m_ItemTableKeyList;
        _CHeroMSExp.itemnumlist = m_ItemNumList;
        IOControler.GetInstance().SendProtocol(_CHeroMSExp);
    }

    /// <summary>
    /// 创建背包中所有的道具
    /// </summary>
    private void GreatItem()
    {
        item.Clear();

        items.Clear();
        item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL);
        for (int i = item.Count - 1; i >= 0; i--)//移除经验小于一的道具
        {
            ItemTemplate _item = item[i].GetItemRowData();
            int _num = _item.getImprovexperience();
            if (_num < 1)
            {
                item.Remove(item[i]);
            }
        }
        //找到固定的道具id,获取数据，然后移除
        GameConfig _cofig = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
        for (int i = 0; i < _cofig.getMs_exp().Length; i++)
        {
            for (int j = item.Count - 1; j >= 0; j--)
            {
                if (_cofig.getMs_exp()[i] == item[j].GetItemTableID())
                {
                    ItemTemplate _itemTemplate = item[j].GetItemRowData();
                    int _num = item[j].GetItemCount();
                    MysticItemData _MysticItemData = new MysticItemData(_itemTemplate,_num);
                    items.Add(_MysticItemData);
                    item.Remove(item[j]);
                }
            }
            
            ItemTemplate itemTemplate = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(_cofig.getMs_exp()[i]);
            MysticItemData MysticItemData = new MysticItemData(itemTemplate,0);
            items.Add(MysticItemData);
        }

        //对背包中剩余的物品按经验值排序 然后添加到列表中
        item.Sort(Compare);

        for (int i = 0; i < item.Count; i++)
        {
            ItemTemplate _itemTemplate = item[i].GetItemRowData();
            int _num = item[i].GetItemCount();
            MysticItemData _MysticItemData = new MysticItemData(_itemTemplate, _num);
            items.Add(_MysticItemData);
        }


        m_ItemBannerLayout.cellCount = items.Count;
        m_ItemBannerLayout.updateCellEvent = UpdateItemList;
        m_ItemBannerLayout.Reload();

    }

    private int Compare(BaseItem Left, BaseItem Right)
    {
        ItemTemplate _item = Left.GetItemRowData();
        int _num_1 = _item.getImprovexperience();
        ItemTemplate _item2 = Right.GetItemRowData();
        int _num_2 = _item2.getImprovexperience();
        if (_num_1 < _num_2)
            return 1;
        else if (_num_1 == _num_2)
            return 0;
        else
            return -1;
    }

    public class MysticItemData
    {
        private ItemTemplate m_itemTemplate;
        private int m_itemNum;

         public MysticItemData(ItemTemplate _itemTemplate,int _itemNum)
        {
            m_itemTemplate = _itemTemplate;
            m_itemNum = _itemNum;
        }
        public ItemTemplate GetItemTemplate
        {
            get { return m_itemTemplate; }
        }
        public int GetItemNum
        {
            get { return m_itemNum; }
        }

    }

    private void UpdateItemList(int index, RectTransform cell)
    {
        UI_MysticItem _bannerItemData = cell.GetComponent<UI_MysticItem>();
        if (_bannerItemData == null)
        {
            _bannerItemData = cell.gameObject.AddComponent<UI_MysticItem>();
        }
        _bannerItemData.index = index;
        _bannerItemData.ArticleItem(_bannerItemData.gameObject.transform, m_MysticId);
        _bannerItemData.SetInfo(items[index]);
        _bannerItemData.SetExpItemClick(ClickHandle);
    }

    private void ClickHandle(ItemTemplate itemtable)
    {
        if (m_AddItemData.ContainsKey(itemtable.getId()))
        {
            int _temp = m_AddItemData[itemtable.getId()];
            _temp++;
            m_AddItemData[itemtable.getId()] = _temp;
        }
        else
        {
            int _temp = 0;
            _temp++;
            m_AddItemData.Add(itemtable.getId(), _temp);
        }

        m_AllExp = itemtable.getImprovexperience();
        m_CostTotalExp = m_AllExp;

        HandleExp();

        //绿色的小字提示
        if (m_CostNum < 0)
        {
            m_LvAddText.text = "";
            m_InfoAddText.text = "";
        }

    }
    private void HandleExp()
    {
        if (m_LevelIndex + 1 + m_CostNum > m_CurMysticTData.getConsumexpevalue().Length - 1)
        {
            m_ExpSlider.maxValue = 1;
            m_ExpSlider.value = 1;
            m_SliderTopText.text = "";
            m_MaxLevelInfoText.gameObject.SetActive(true);
            ObjectSelf.GetInstance().GetIsMysticMaxLevel = true;
            return;
        }
        LevelMaxExp = m_CurMysticTData.getConsumexpevalue()[m_LevelIndex + 1 + m_CostNum];
        m_ExpSlider.maxValue = LevelMaxExp;
        int _Surplus = LevelMaxExp - m_FashCurExpValue;
        if (m_CostTotalExp - _Surplus >= 0)
        {
            m_CostNum++;
            m_LvAddText.text = "+" + m_CostNum.ToString();
            m_CostTotalMoney += m_CurMysticTData.getConsumnb()[m_LevelIndex + m_CostNum] / m_CurMysticTData.getConsumexpevalue()[m_LevelIndex + m_CostNum] * _Surplus;

            if (m_LevelIndex < 0)
            {
                m_InfoAddText.text = "+" + (m_CurMysticTData.getValue()[m_LevelIndex + m_CostNum]).ToString();
            }
            else
            {
                m_InfoAddText.text = "+" + (m_CurMysticTData.getValue()[m_LevelIndex + m_CostNum] - m_CurMysticTData.getValue()[m_LevelIndex]).ToString();
            }

            if (m_LevelIndex + 1 + m_CostNum > m_CurMysticTData.getConsumexpevalue().Length - 1)
            {
                m_ExpSlider.maxValue = m_CurMysticTData.getConsumexpevalue()[m_CurMysticTData.getConsumexpevalue().Length - 1];
                m_ExpSlider.value = m_CurMysticTData.getConsumexpevalue()[m_CurMysticTData.getConsumexpevalue().Length - 1];
                m_SliderTopText.text = (LevelMaxExp + " / " + LevelMaxExp).ToString();
            }
            else
            {
                m_ExpSlider.value = m_CostTotalExp - _Surplus;
                m_ExpSlider.maxValue = m_CurMysticTData.getConsumexpevalue()[m_LevelIndex + 1 + m_CostNum];
                m_SliderTopText.text = (m_CostTotalExp - _Surplus + " / " + LevelMaxExp).ToString();
            }

            m_FashCurExpValue = m_CostTotalExp - _Surplus;
            m_CostTotalExp = 0;
            
            HandleExp();
        }
        else
        {
            m_FashCurExpValue = m_FashCurExpValue + m_CostTotalExp;
            if (m_FashCurExpValue != 0)
            {
                m_CostTotalMoney += m_CurMysticTData.getConsumnb()[m_LevelIndex + 1 + m_CostNum] / m_CurMysticTData.getConsumexpevalue()[m_LevelIndex + 1 + m_CostNum] * m_CostTotalExp;
                m_ExpSlider.value = m_FashCurExpValue;
            }
            m_SliderTopText.text = (m_FashCurExpValue + " / " + LevelMaxExp).ToString();
        }
        
        m_CostText.text = m_CostTotalMoney.ToString();
    }


    private void ShowExpValue()
    {
        if (m_LevelIndex < 4)
        {
            m_ExpSlider.maxValue = m_CurMysticTData.getConsumexpevalue()[m_LevelIndex + 1];
        }       
        m_FashCurExpValue = m_HeroData.HeroCabalaDB.CabalaList[m_MysticId].CurExp;
        m_ExpSlider.value = m_FashCurExpValue;
        HandleExp();
    }

    private void ShowPopWindInfo()
    {
        m_Pop_icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_CurMysticTData.getIcon());
        m_Pop_iconName.text = GameUtils.getString(m_CurMysticTData.getMsname());
        m_PopLevelText.text = "Lv." + m_HeroData.HeroCabalaDB.CabalaList[m_MysticId].IntensifyLev.ToString();
        m_PopInfoText.text = GameUtils.getString(m_CurMysticTData.getDdes());
        if (m_LevelIndex < 0)
        {
            m_PopInfoValueText.text = "+" + 0;
        }
        else
        {
            m_PopInfoValueText.text = "+" + m_CurMysticTData.getValue()[m_LevelIndex].ToString();
        }

        m_MaxLevelInfoText.gameObject.SetActive(false);
        m_UpgradeInfoText.text = GameUtils.getString("ui_yingxiongqianghua_mishu5"); 
    }

    /// <summary>
    /// 关闭弹框按钮
    /// </summary>
    protected void OnClickPopButton()
    {
        ClearUp();
        ObjectSelf.GetInstance().GetIsMysticMaxLevel = false;
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    private void MysticSuccess()
    {
        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_mishu10"), this.gameObject.transform);
        ClearUp();
        m_AddItemData.Clear();
        ObjectSelf.GetInstance().GetIsMysticMaxLevel = false;
        PopMysticWindow(m_MysticId, m_objectCard);
    }

    /// <summary>
    /// 返回当前等级在数据表中的下标
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    protected int GetLevelIndex(int level)
    {
        for (int i = 0; i < m_CurMysticTData.getLevel().Length; i++)
        {
            if (m_CurMysticTData.getLevel()[i] == level)
            {
                return i;
            }
        }
        return -1;
    }

    private void ClearUp()
    {
        m_CostNum = 0;
        m_CostTotalMoney = 0;
        m_AllExp = 0;
        m_FashCurExpValue = 0;
        m_CostTotalExp = 0;
        m_InfoAddText.text = "";
        m_PopLevelText.text = "";
        m_LvAddText.text = "";
        m_SliderTopText.text = "";
        m_CostText.text = "";
    }
    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_MysticSuccess, MysticSuccess);
    }
}
