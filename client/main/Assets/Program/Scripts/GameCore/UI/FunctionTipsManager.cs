using System;
using System.Collections.Generic;
using UnityEngine;

using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameEventSystem;
using GNET;

public class FunctionTipsManager
{


    const int AttributeTrainArrayLength = 4;
    const int SkillLvUpResultArrayLength = 3;

    delegate bool CheckLogic(ObjectCard hero);

    private static FunctionTipsManager Inst;


    private ObjectSelf m_ObjectSelf;
    private ObjectCard m_CurrentSelectedHero;
    private HeroContainer m_HeroContainer;

    private List<ShopTemplate> m_GiftSetList;           //表中所有礼包数据
    private List<MonthcardTemplate> m_MonthCardList;    //表中所有月卡数据

    TableReader m_AttributeTrainTable;
    TableReader m_SkillTable;
    TableReader m_SkillupcostTable;
    TableReader m_HeroExpTable;

    //世界BOSS
    WorldBossManager m_WorldBossManager;
    //缪斯奏曲的时间数据
    TimeInfoHM m_TimeNow = new TimeInfoHM();
    TimeInfoHM m_TimeNoonMin = new TimeInfoHM();
    TimeInfoHM m_TimeNoonMax = new TimeInfoHM();
    TimeInfoHM m_TimeNightMin = new TimeInfoHM();
    TimeInfoHM m_TimeNightMax = new TimeInfoHM();
    //活跃度模块，获取宝箱所需的活跃值
    int[] m_LivenessLvArray;

    //空闲符文
    public bool m_HaveIdleRune = false;
    public bool m_HaveIdleSpecialRune = false;
    //英雄4项属性的检测结果
    public bool[] m_AttributeTrainResultArray;
    //英雄3个技能的检测结果
    public bool[] m_SkillLvUpResultArray;

    //探险任务中的4个小队
    public bool[] m_ExploreTeamResultArray;

    //需要标记的商店商品id
    public List<int> m_NonPurchasedGiftSetList;

    private int m_ExpCrystalFactor;   //一颗经验结晶可以转化成多少经验

    public static FunctionTipsManager GetInstance()
    {
        return Inst;
    }
    public FunctionTipsManager()
    {
        m_AttributeTrainResultArray = new bool[AttributeTrainArrayLength];
        m_SkillLvUpResultArray = new bool[SkillLvUpResultArrayLength];
        m_ExploreTeamResultArray = new bool[4];

        m_ObjectSelf = ObjectSelf.GetInstance();
        m_HeroContainer = m_ObjectSelf.HeroContainerBag;

        var _tableSet = DataTemplate.GetInstance();
        m_AttributeTrainTable = _tableSet.m_AttributetrainTable;
        m_SkillTable = _tableSet.m_SkillTable;
        m_SkillupcostTable = _tableSet.m_SkillupcostTable;
        m_HeroExpTable = _tableSet.m_HeroExpTable;

        m_NonPurchasedGiftSetList = new List<int>();

        m_ExpCrystalFactor = _tableSet.m_GameConfig.getJingyanjiejing_to_jingyan();
        m_LivenessLvArray = _tableSet.m_GameConfig.getActivitymission_reward_level();
        
        m_GiftSetList = new List<ShopTemplate>();
        List<ShopTemplate> allItems = _tableSet.GetAllShopTemplates();
        for (int i = 0; i < allItems.Count; i++)
        {
            if (allItems[i].getTabID() == 2)        //tabId为2的属于礼包
            {
                m_GiftSetList.Add(allItems[i]);
            }
        }
        m_MonthCardList = _tableSet.GetAllMonthCardTemplates();

        var _timeNoonArr = _tableSet.m_GameConfig.getAp_get_time()[0].Split('-');
        var _timeNightArr = _tableSet.m_GameConfig.getAp_get_time()[1].Split('-');
        string[] _timeNoonStart = _timeNoonArr[0].Split(':');
        string[] _timeNoonEnd = _timeNoonArr[1].Split(':');
        string[] _timeNightStart = _timeNightArr[0].Split(':');
        string[] _timeNightEnd = _timeNightArr[1].Split(':');
        //缪斯奏曲
        DateTime dt = m_ObjectSelf.ServerDateTime;
        m_TimeNow.hour = dt.Hour;
        m_TimeNow.minute = dt.Minute;
        m_TimeNoonMin.hour = int.Parse(_timeNoonStart[0]);
        m_TimeNoonMin.minute = int.Parse(_timeNoonStart[1]);
        m_TimeNoonMax.hour = int.Parse(_timeNoonEnd[0]);
        m_TimeNoonMax.minute = int.Parse(_timeNoonEnd[1]);
        m_TimeNightMin.hour = int.Parse(_timeNightStart[0]);
        m_TimeNightMin.minute = int.Parse(_timeNightStart[1]);
        m_TimeNightMax.hour = int.Parse(_timeNightEnd[0]);
        m_TimeNightMax.minute = int.Parse(_timeNightEnd[1]);

        //世界BOSS
        m_WorldBossManager = ObjectSelf.GetInstance().WorldBossMgr;

        Inst = this;
    }
    public void Init()
    {
        AddEventListener();

    }

    public void Release()
    {
        Inst = null;
        RemoveEventListener();
    }

    private void AddEventListener()
    {
        GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);
    }
    private void RemoveEventListener()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);
    }


    /// <summary>
    /// 使用指定逻辑遍历当前队伍列表中的英雄
    /// </summary>
    /// <param name="checkLogic"></param>
    /// <returns></returns>
    private bool TeamCheck(CheckLogic checkLogic)
    {
        bool _result = false;
        int _groupCount = m_ObjectSelf.Teams.GetDefaultGroup();
        int _heroCount = m_ObjectSelf.Teams.m_Matrix.GetLength(1);
        for (int i = 0; i < _heroCount; ++i)
        {
            ObjectCard temp = m_HeroContainer.FindHero(m_ObjectSelf.Teams.m_Matrix[_groupCount, i]);
            if (temp == null)
                continue;
            else
            {
                _result |= checkLogic(temp);
                if (_result)
                    break;
            }

        }
        return _result;
    }
    public bool CheckHeroIsInDefaultTeam()
    {
        if (m_CurrentSelectedHero == null)
            return false;

        bool _result = false;
        int _groupCount = m_ObjectSelf.Teams.GetDefaultGroup();
        int _heroCount = m_ObjectSelf.Teams.m_Matrix.GetLength(1);
        for (int i = 0; i < _heroCount; ++i)
        {
            if (m_CurrentSelectedHero.GetHeroData().GUID.Equals(m_ObjectSelf.Teams.m_Matrix[_groupCount, i]))
            {
                _result = true;
                break;
            }
        }
        return _result;
    }

    #region CheckLogic

    #region Hero

    /**************英雄升级*************/
    /// <summary>
    /// 检测当前队伍中是否存在可升级英雄
    /// </summary>
    /// <returns></returns>
    public bool CheckUpgradableHeroInTeam()
    {
        return TeamCheck(CheckUpgradable);
    }

    /// <summary>
    /// 检测英雄背包界面，玩家当前选中英雄是否可升级
    /// </summary>
    /// <returns></returns>
    public bool CheckCurrentUpgradableHero()
    {
        return CheckHeroIsInDefaultTeam() && CheckUpgradable(m_CurrentSelectedHero);
    }

    /// <summary>
    /// 可升级检测逻辑
    /// </summary>
    /// <param name="hero"></param>
    /// <returns></returns>
    private bool CheckUpgradable(ObjectCard hero)
    {
        if (hero == null)
            return false;

        HeroTemplate _heroItem = hero.GetHeroRow();
        IExcelBean _data = null;
        HeroexpTemplate _heroExp = null;
        var _dataMap = m_HeroExpTable.getData();
        if (_dataMap.TryGetValue(hero.GetHeroData().Level,out _data))
        {
            _heroExp = _data as HeroexpTemplate;
        }

        if (_heroExp != null)
        {
            return m_ObjectSelf.ExpFruit * m_ExpCrystalFactor >= _heroExp.getExp()[_heroItem.getExpNum() - 1]
                && hero.GetHeroData().Level < _heroItem.getMaxLevel();
        }
        else
        {
            return false;
        }
    
    }
    /****************英雄进阶******************/

    public bool CheckAdvancedHeroInTeam()
    {
        return TeamCheck(CheckAdvancedHero);
    }
    public bool CheckCurrentAdvancedHero()
    {
        return CheckHeroIsInDefaultTeam() && CheckAdvancedHero(m_CurrentSelectedHero);
    }
    private bool CheckAdvancedHero(ObjectCard hero)
    {
        if (hero == null)
            return false;

        bool _result;
        HeroTemplate _heroTemplate = hero.GetHeroRow();
        
        long _resourceCount = 0;

        if (m_ObjectSelf.TryGetResourceCountById(_heroTemplate.getStageUpCostType1(), ref _resourceCount))
        {
            _result = hero.GetHeroData().Level >= _heroTemplate.getMaxLevel() && _resourceCount > _heroTemplate.getStageUpCost1();
        }
        else 
        {
            _result = false;
        }

        if (!_result && m_ObjectSelf.TryGetResourceCountById(_heroTemplate.getStageUpCostType2(), ref _resourceCount))
        {
            _result |= _resourceCount > _heroTemplate.getStageUpCost2();
        }

        return _result;
    }


    /******************符文************************/

    public bool CheckHeroRuneInTeam()
    {
        return TeamCheck(CheckHeroRune);
    }
    public bool CheckCurrentHeroRune()
    {
        return CheckHeroRune(m_CurrentSelectedHero);
    }

    private bool CheckHeroRune(ObjectCard hero)
    {
        if (hero == null)
        {
            return false;
        }
        bool _result = false;

        CheakIdleRune();

        _result = m_HaveIdleRune | m_HaveIdleSpecialRune;

        if (_result)
        {
            _result = false;
            var heroData = hero.GetHeroData();

            if (!_result && m_HaveIdleRune)
            {
                for (int i = (int)EM_RUNE_POINT.EM_RUNE_POINT_COMMON1; i < (int)EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL; i++)
                {
                    _result |= heroData.IsRuneNull((EM_RUNE_POINT)i);
                    if (_result)
                    {
                        break;
                    }
                }
            }


            _result |= heroData.IsRuneNull(EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL);


        }

        return _result;
    }

    private void CheakIdleRune()
    {
        var runeList = m_ObjectSelf.CommonItemContainer.GetItemList(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP);
        m_HaveIdleRune = false;
        m_HaveIdleSpecialRune = false;
        for (int i = 0; i < runeList.Count; i++)
        {
            if (runeList[i].GetItemType() == (int)EM_ITEM_TYPE.EM_ITEM_TYPE_RUNE)
            {
                ItemEquip rune = runeList[i] as ItemEquip;
                if (rune != null && !rune.IsEquip())
                {
                    if (rune.GetItemRowData().getRune_type() < (int)EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL)
                    {
                        m_HaveIdleRune = true;
                    }
                    else if (rune.GetItemRowData().getRune_type() == (int)EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL)
                    {
                        m_HaveIdleSpecialRune = true;
                    }
                }
                if (m_HaveIdleRune && m_HaveIdleSpecialRune)
                    break;
            }
        }
    }

    /*******************属性培养*************************/

    /// <summary>
    /// 这里并没有对当前英雄是否属于默认队伍做检测
    /// </summary>
    /// <returns></returns>
    public bool[] CheckEveryAttributeTrain()
    {
        CheckCurrentAttributeTrain();
        return m_AttributeTrainResultArray;
    }

    public bool CheckAttributeTrainInTeam()
    {
        return TeamCheck(CheckAttributeTrain);
    }

    private bool CheckCurrentAttributeTrain()
    {
        bool _result = CheckAttributeTrain(m_CurrentSelectedHero, m_AttributeTrainResultArray);
        return _result;
    }
    /// <summary>
    /// 遍历某个英雄的4种属性，当已经找到某项属性满足要求时立刻返回
    /// </summary>
    /// <param name="hero"></param>
    /// <returns>是否最少有一个属性有足够的资源培养并且还没有到达培养上限</returns>
    private bool CheckAttributeTrain(ObjectCard hero)
    {
        bool _result = false;

        for (int i = 0; i < AttributeTrainArrayLength; i++)
        {
            int _attributetrainId;
            bool _isNotMaxTrainCount = !GetAttributeTrainId(hero, i, out _attributetrainId);
            if (_isNotMaxTrainCount)
            {

                AttributetrainTemplate attr = (AttributetrainTemplate)m_AttributeTrainTable.getTableData(_attributetrainId);
                long _resourceCount = 0;
                m_ObjectSelf.TryGetResourceCountById(attr.getCostType(), ref _resourceCount);
                _result = _resourceCount >= attr.getCost();
                if (_result)
                    break;
            }
        }

        return _result;
    }
    /// <summary>
    /// 遍历某个英雄的4种属性，返回各属性是否【到达培养上限】以及是否【有足够的资源升级】
    /// </summary>
    /// <param name="hero"></param>
    /// <param name="resultArr"></param>
    /// <returns>是否最少有一个属性有足够的资源培养并且还没有到达培养上限</returns>
    private bool CheckAttributeTrain(ObjectCard hero,bool[] resultArr)
    {
        bool _result = false;

        for (int i = 0; i < AttributeTrainArrayLength; i++)
        {
            int _attributetrainId;
            resultArr[i] = !GetAttributeTrainId(hero, i, out _attributetrainId);
            if (resultArr[i])
            {
                AttributetrainTemplate attr = (AttributetrainTemplate)m_AttributeTrainTable.getTableData(_attributetrainId);
                long _resourceCount = 0;
                m_ObjectSelf.TryGetResourceCountById(attr.getCostType(), ref _resourceCount);
                resultArr[i] = _resourceCount >= attr.getCost();
                _result |= resultArr[i];
            }
        }

        return _result;
    }

    /// <summary>
    /// 检查某一项能力是否到达训练上限
    /// </summary>
    /// <param name="attributeId">对应第几项能力，ObjectCard.GetHeroData().GetTrainCount()[attributeId]</param>
    /// <param name="attributetrainID">返回当前当前32表id（能力升级相关）</param>
    /// <returns>返回是否已经到达训练上限</returns>
    private bool GetAttributeTrainId(ObjectCard hero,int attributeId, out int attributetrainID)
    {
        if (attributeId >= AttributeTrainArrayLength || hero == null)
        {
            attributetrainID = -1;
            return true;
        }
        Func<int> GetTrainSlotX;
        Func<int> GetMaxTrainCount;
        HeroTemplate heroTemplate = hero.GetHeroRow();

        switch (attributeId)
        { 
            case 0:
                GetTrainSlotX = heroTemplate.getTrainSlot1;
                GetMaxTrainCount = heroTemplate.getTrainMaximum1;
                break;
            case 1:
                GetTrainSlotX = heroTemplate.getTrainSlot2;
                GetMaxTrainCount = heroTemplate.getTrainMaximum2;
                break;
            case 2:
                GetTrainSlotX = heroTemplate.getTrainSlot3;
                GetMaxTrainCount = heroTemplate.getTrainMaximum3;
                break;
            case 3:
                GetTrainSlotX = heroTemplate.getTrainSlot4;
                GetMaxTrainCount = heroTemplate.getTrainMaximum4;
                break;
            default:
                GetTrainSlotX = null;
                GetMaxTrainCount = null;
                break;
        }


        int _id = hero.GetHeroData().GetTrainCount()[attributeId];
        if (_id != 0)
            attributetrainID = _id;
        else
            attributetrainID = GetTrainSlotX() * 1000 + 1;

        AttributetrainTemplate _dataTemplate = (AttributetrainTemplate)m_AttributeTrainTable.getTableData(attributetrainID);
        int allAttributetrainID = GetTrainSlotX() * 1000 + GetMaxTrainCount() - 1;

        return attributetrainID >= allAttributetrainID;
    }

    /***********技能升级************/
    /// <summary>
    /// 检测当前队伍中是否有可进行技能升级的英雄,返回每个技能的检测结果
    /// </summary>
    /// <returns></returns>
    public bool[] CheckEverySkillUpgrade()
    {
        CheckCurrentSkillUpgrade();
        return m_SkillLvUpResultArray;
    }
    /// <summary>
    /// 检测当前队伍中是否有可进行技能升级的英雄
    /// </summary>
    /// <returns></returns>
    public bool CheckSkillUpgradeInTeam()
    {
        return TeamCheck(CheckSkillUpgrade);
    }
    private bool CheckCurrentSkillUpgrade()
    {
        return CheckSkillUpgrade(m_CurrentSelectedHero, m_SkillLvUpResultArray);
    }

    private bool CheckSkillUpgrade(ObjectCard hero,bool[] resultArr)
    {
        if (hero == null)
            return false;

        bool _result = false;
        HeroTemplate _heroTemplate = hero.GetHeroRow();
        int _heroQuality = _heroTemplate.getQuality();

        SpellData[] SkillDataList = hero.GetHeroData().SpellDataList;
        for (int i = 0; i < SkillDataList.Length; i++)
        {
            if (SkillDataList[i].SpellID < 0)
                continue;

            bool _canUpgrade = false;
            int[] _upgradeCostArr;
            int[] _upgradeCostNumArr;
            SkillTemplate _skill = m_SkillTable.getTableData(SkillDataList[i].SpellID) as SkillTemplate; //_SkillData.SpellID为技能ID
            SkillupcostTemplate _skillUpCost = m_SkillupcostTable.getTableData(_skill.getId()) as SkillupcostTemplate;

            _canUpgrade = _skillUpCost.getUpgradeSkillID() > 0 && _heroQuality >= _skillUpCost.getUpgradeStarCondition();

            _upgradeCostArr = _skillUpCost.getUpgradeCostId();
            _upgradeCostNumArr = _skillUpCost.getUpgradeCostNum();

            if (_canUpgrade)
            {
                for (int j = 0; j < _upgradeCostArr.Length; j++)
                {
                    long _upgradeResource = 0;
//                    m_ObjectSelf.TryGetResourceCountById(_upgradeCostArr[j], ref _upgradeResource);
                    _upgradeResource = ReturnItemCount(_upgradeCostArr[j]);
                    _canUpgrade &= _upgradeCostNumArr[j] <= _upgradeResource;
                    if (!_canUpgrade)
                        break;
                }
            }
            resultArr[i] = _canUpgrade;
            _result |= _canUpgrade;
        }

        return _result;
    }
    private bool CheckSkillUpgrade(ObjectCard hero)
    {
        if (hero == null)
            return false;

        bool _result = false;
        HeroTemplate _heroTemplate = hero.GetHeroRow();
        int _heroQuality = _heroTemplate.getQuality();

        SpellData[] SkillDataList = hero.GetHeroData().SpellDataList;
        for (int i = 0; i < SkillDataList.Length; i++)
        {
            if (SkillDataList[i].SpellID < 0)
                continue;

            bool _canUpgrade = false;
            int[] _upgradeCostArr;
            int[] _upgradeCostNumArr;
            SkillTemplate _skill = m_SkillTable.getTableData(SkillDataList[i].SpellID) as SkillTemplate; //_SkillData.SpellID为技能ID
            SkillupcostTemplate _skillUpCost = m_SkillupcostTable.getTableData(_skill.getId()) as SkillupcostTemplate;

            _canUpgrade = _skillUpCost.getUpgradeSkillID() > 0 && _heroQuality >= _skillUpCost.getUpgradeStarCondition();

            _upgradeCostArr = _skillUpCost.getUpgradeCostId();
            _upgradeCostNumArr = _skillUpCost.getUpgradeCostNum();

            if (_canUpgrade)
            {
                for(int j = 0;j<_upgradeCostArr.Length;j++)
                {
                    long _upgradeResource = 0;
//                    m_ObjectSelf.TryGetResourceCountById(_upgradeCostArr[j], ref _upgradeResource);
                    _upgradeResource = ReturnItemCount(_upgradeCostArr[j]);
                    _canUpgrade &= _upgradeCostNumArr[j] <= _upgradeResource;
                    if (!_canUpgrade)
                        break;
                }
            }
            if(_canUpgrade)
            {
                _result = _canUpgrade;
                break;
            }
        }

        return _result;
    }

    private int ReturnItemCount(int id)
    {
        List<BaseItem> _itemList = ObjectSelf.GetInstance().CommonItemContainer.GetItemList(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON);
        int baseItemCount = _itemList.Count;
        for (int i = 0; i < baseItemCount; ++i)
        {
            if (_itemList[i].GetItemTableID() == id)
            {
                return _itemList[i].GetItemCount();
            }
        }

        return 0;
    }
    #endregion Hero

    /**************背包中有未打开的礼包**************/
    /// <summary>
    /// 背包中有未打开的礼包
    /// </summary>
    /// <returns></returns>
    public bool CheckUnopenedGiftSet()
    {
        var _giftList = m_ObjectSelf.CommonItemContainer.
            ReturnItemType((int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, EM_ITEM_TYPE.EM_ITEM_TYPE_GIFT);

        return _giftList.Count > 0;
    }
    /// <summary>
    /// 检测是否有新的符文获得
    /// </summary>
    /// <returns></returns>
   public bool CheckIsNewRune()
   {
       int count = 0;
       foreach (X_GUID item in m_ObjectSelf.CommonItemContainer.m_NewGetItems)
       {
           if (m_ObjectSelf.CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP,item)!=null)
           {
               count++;
           }
       }
       return count > 0 ? true : false;
    }
    /// <summary>
    /// 检测是否有新的道具获得
    /// </summary>
    /// <returns></returns>
    public bool CheckIsNewCommon()
   {

       int count = 0;
       foreach (X_GUID item in m_ObjectSelf.CommonItemContainer.m_NewGetItems)
       {
           if (m_ObjectSelf.CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, item) != null)
           {
               count++;
           }
       }
       return count > 0 ? true : false;
   }

    /**********阵型***********/

    /// <summary>
    /// 当默认队伍中无英雄时返回真
    /// </summary>
    /// <returns>当默认队伍中无英雄时返回真</returns>
    public bool CheckTeamNoMember()
    {
        bool _result = true;
        Team _team = m_ObjectSelf.Teams;
        int _teamCount = _team.GetDefaultGroup();
        //遍历判空
        int _heroCount = m_ObjectSelf.Teams.m_Matrix.GetLength(1);
        for (int i = 0; i < _heroCount; ++i)
        {
            var _guid = m_ObjectSelf.Teams.m_Matrix[_teamCount, i];
            ObjectCard temp = null;
            if (_guid.IsValid())
                temp = m_HeroContainer.FindHero(_guid);

            if (temp != null)
            {
                _result = false;
                break;
            }
        }
        return _result;
    }

    /****************检测商店的可购买礼包*****************/
    /// <summary>
    /// 检测商店的可购买礼包
    /// </summary>
    /// <returns></returns>
    public bool CheckNonPurchasedGiftSet()
    {
        bool _result = false;
        m_NonPurchasedGiftSetList.Clear();

        for (int i = 0; i < m_GiftSetList.Count; i++)
        {
            bool _timesResult = true;
            ShopTemplate _shopT = m_GiftSetList[i];
            if (_shopT.getVipLimit() > m_ObjectSelf.VipLevel)
                continue;

            int _maxTimes = _shopT.getDailyMaxBuy();
            int _maxTotalTimes = _shopT.getShelveMaxBuy();

            var _shopBuy = m_ObjectSelf.GetShopBuyInfoByShopId(_shopT.GetID());

            if (_maxTimes > 0)
            {
                _timesResult &= _maxTimes > _shopBuy.todaynum;
            }

            if (_maxTotalTimes > 0)
            {
                _timesResult &= _maxTotalTimes > _shopBuy.buyallnum;
            }
            if (_timesResult)
            {
                _result |= true;
                m_NonPurchasedGiftSetList.Add(_shopT.GetID());
            }
        }

        return _result;
    }


    /******************检测是否有未领取的每日月卡奖励****************************/
//需要监听事件，另外和时间也有关系
//    GameEventDispatcher.Inst.addEventListener(GameEventID.UI_RefreshMonthCard, OnMonthCardDataChange);
    /// <summary>
    /// 检测是否有未领取的每日月卡奖励，该逻辑还应注意事件监听和时间，存在问题
    /// </summary>
    /// <returns></returns>
    public bool CheckUnclaimedMonthCard()
    {
        bool _result = false;

        for(int i = 0;i < m_MonthCardList.Count;i++)
        {
            MonthcardTemplate _monthCardTemp = m_MonthCardList[i];
            //获取月卡持续天数
            int _duration = _monthCardTemp.getDuration();
            var _cardData = m_ObjectSelf.GetMontCardInfoById(_monthCardTemp.GetID());

            if (_duration < 0)  //永久时间
            {
                if(_cardData == null || _cardData.istodayget != 1)
                {
                    _result = true;
                    break;
                }
            }
            else//限时月卡
            { 
                if(_cardData != null)
                {
                    DateTime _dt = TimeUtils.ConverMillionSecToDateTime(_cardData.overtime, m_ObjectSelf.ServerTimeZone);
                    if (_cardData.istodayget != 1 && m_ObjectSelf.ServerDateTime < _dt)//未过期;
                    {
                        _result = true;
                        break;
                    }
                }
            }

        }
        return _result;
    }


    /********************检测是否本日尚未进入极限试炼***********************/

    /// <summary>
    /// 检测是否本日尚未进入极限试炼
    /// </summary>
    /// <returns></returns>
    public bool CheckLimitTest()
    {
        return m_ObjectSelf.LimitFightMgr.Activate;
    }

    /******************世界BOSS******************~ToT~*/
    /// <summary>
    /// 世界BOSS开启时(有BOSS开启且尚未被击杀)
    /// </summary>
    /// <returns></returns>
    public bool CheckWorldBoss()
    {
        return CheckActiveBoss();
    }

    private bool CheckActiveBoss()
    {
        bool _result = false;
        foreach (var data in m_WorldBossManager.m_BossDataMap.Values)
        {
            if (data.m_IsOpen > 0 && data.m_IsKilled == 0)//已开启且并没有被杀掉
            {
                _result = true;
                break;
            }
        }
        return _result;
    }
    private bool CheckChalleged()//玩家已经挑战过BOSS
    {
        return m_WorldBossManager.m_MyTotalDamage > 0;
    }

    /********************探险任务完成有奖励尚未领取************************~ToT~*/

    /// <summary>
    /// 探险任务完成有奖励尚未领取
    /// </summary>
    /// <returns></returns>
    public bool CheckExploreAward()
    {
        bool _result = false;
        var _exploreTaskMap = m_ObjectSelf.GetAllExploreTaskData();
        foreach (var stageList in _exploreTaskMap.Values)
        {
            foreach (var stageData in stageList.stagetx)
            {
                _result = UI_ExploreModule.GetExploreTaskState(stageData) == EXPLORE_TASK_STATE.ExploringOver;
                if (_result)
                    break;
            }
            if (_result)
                break;
        }
        return _result;
    }

    public bool[] CheckEveryExploreTeamAward()
    {
        for (int i = 0; i < m_ExploreTeamResultArray.Length; i++)
        {
            m_ExploreTeamResultArray[i] = false;
        }
        Dictionary<int, teamtanxian> teamDatas = ObjectSelf.GetInstance().GetExploreTeamData();
        foreach (var data in teamDatas)
        {
            //空闲小队;
            if (data.Value.team == null || data.Value.team.Count <= 0)
            {
                continue;
            }
            tanxianinit tx = ObjectSelf.GetInstance().GetExploreTaskDataById(data.Value.tanxianid);
            if (tx == null)
            {
                Debug.LogError("探险任务数据为NULL exploreid=" + data.Value.tanxianid);
                continue;
            }

            m_ExploreTeamResultArray[data.Key-1] = UI_ExploreModule.GetExploreTaskState(tx) == EXPLORE_TASK_STATE.ExploringOver;
        }
        return m_ExploreTeamResultArray;
    }
    /*******************缪斯奏曲期间未领取活力*************************/
    /// <summary>
    /// 缪斯奏曲期间未领取活力
    /// </summary>
    /// <returns></returns>
    public bool CheckInGetPowerTime()
    {
        bool _result = false;
        DateTime dt = m_ObjectSelf.ServerDateTime;
        m_TimeNow.hour = dt.Hour;
        m_TimeNow.minute = dt.Minute;

        int _typeNum = m_ObjectSelf.IsGetPower;
        int _noonNum = _typeNum % 10;
        int _nightNum = _typeNum / 10;

        if (_noonNum != 1)//尚未聆听
        {
            _result |= TimeUtils.IsInHourTimeDuration(m_TimeNow, m_TimeNoonMin, m_TimeNoonMax);
        }

        if (_nightNum != 1)//尚未聆听
        {
            _result |= TimeUtils.IsInHourTimeDuration(m_TimeNow, m_TimeNightMin, m_TimeNightMax);
        }

        return _result;
    }

    /****************未进行神圣祭坛的签到***********************/
    /// <summary>
    /// 未进行神圣祭坛的签到
    /// </summary>
    /// <returns></returns>
    public bool CheckSacredAltar()
    {
        return m_ObjectSelf.ScaredAltarTypeNum % 10 == 0;   //细节在SRefreshSweep中
    }

    /*********************图鉴有可领取奖励*****************************~ToT~*/
    /// <summary>
    /// 图鉴有可领取奖励
    /// </summary>
    /// <returns></returns>
    public bool CheckHandbookAward()
    {
        return UI_HandBookManager.CheckNewMedalReard();
    }

    /*********************任意成就类别有可领取奖励*****************************~ToT~*/
    /// <summary>
    /// 任意成就类别有可领取奖励
    /// </summary>
    /// <returns></returns>
    public bool CheckAchievementAward()
    {
        return false;
    }
    /*********************遗迹宝藏界面有免费购买次数时*****************************~ToT~*/
    /// <summary>
    /// 遗迹宝藏界面有免费购买次数时
    /// </summary>
    /// <returns></returns>
    public bool CheckRelicFreeCount()
    {
        return m_ObjectSelf.ishavefree == 0;
    }

    /*********************当未读邮件数量大于等于1*****************************~ToT~*/
    /// <summary>
    /// 当未读邮件数量大于等于
    /// </summary>
    /// <returns></returns>
    public bool CheckUnreadMail()
    {
        return m_ObjectSelf.GetManager().m_HaveNewMail;
    }

    /*********************有活跃度奖励未领取******************************/
    /// <summary>
    /// 有活跃度奖励未领取
    /// </summary>
    /// <returns></returns>
    public bool CheckLivenessAward()
    {
        bool _result = false;
        int _liveness = m_ObjectSelf.Liveness;
        int _livenessClaimNum = m_ObjectSelf.LivenessClaimNum;
        for (int i = 0; i < m_LivenessLvArray.Length; i++)
        {
            if (_liveness >= m_LivenessLvArray[i])
            {
                _result |= (_livenessClaimNum % 10) != 1;
            }
            else
            {
                break;
            }
            if (_result)
                break;
            _livenessClaimNum /= 10;
        }
        return _result;
    }

    /*********************英雄招募界面有免费购买次数时******************************/
    /// <summary>
    /// 英雄招募界面有免费购买次数时
    /// </summary>
    /// <returns></returns>
    public bool CheckHeroRecruitFree()
    {
        bool _result = m_ObjectSelf.freetime <= 0;      //招募英雄的免费倒计时

        if (!_result)
        {
            // 梦想值是否充足
            int _dreamValue = DataTemplate.GetInstance().m_GameConfig.getDream_need_value();
            _result |= m_ObjectSelf.dreamexp >= _dreamValue;
        }
        return _result;
    }


    #endregion

    /***************Callback***************/

    //GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);
    //GameEventID.U_HeroChangeTarget事件响应（当用户在英雄背包面板切换选中英雄时）
    void OnSelectCardHeroChanged(GameEvent ev)
    {
        if (ev == null || ev.data == null)
        {
            return;
        }

        ObjectCard card = ev.data as ObjectCard;

        if (card == null)
        {
            return;
        }
        m_CurrentSelectedHero = card;
        bool[] resultArr = new bool[4];
        
    }
}
