using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using System.Text;
using System;
using GNET;


public class WorldBossCallbackParaPackage
{
    public int m_Result;
    public int m_BossShopID;
    public int m_BossID;
}
public abstract class DynamicItem
{
    public static GameObject InstantiateObject(GameObject originObj, Transform parent)
    {
        GameObject _go = GameObject.Instantiate(originObj, parent.position, parent.rotation) as GameObject;
        _go.transform.SetParent(parent);
        _go.transform.localScale = Vector3.one;
        return _go;
    }

    //如果填入英雄ID 返回英雄称号
    public static string GetItemName(int itemID)
    {
        string _result = null;

        EM_OBJECT_CLASS _awardClass = GameUtils.GetObjectClassById(itemID);
        switch (_awardClass)
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                ResourceindexTemplate _resTable = DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemID) as ResourceindexTemplate;
                if (_resTable != null)
                {
                    _result = _resTable.getName();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                ItemTemplate _itemTable = DataTemplate.GetInstance().m_ItemTable.getTableData(itemID) as ItemTemplate;
                if (_itemTable != null)
                {
                    _result = _itemTable.getName();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                ItemTemplate _runeTable = DataTemplate.GetInstance().m_ItemTable.getTableData(itemID) as ItemTemplate;
                if (_runeTable != null)
                {
                    _result = _runeTable.getName();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                HeroTemplate _heroTable = DataTemplate.GetInstance().m_HeroTable.getTableData(itemID) as HeroTemplate;
                if (_heroTable != null)
                {
                    _result = _heroTable.getTitleID();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                ArtresourceTemplate _atrResTable1 = DataTemplate.GetInstance().m_ArtresourceTable.getTableData(itemID) as ArtresourceTemplate;
                if (_atrResTable1 != null)
                {
                    _result = _atrResTable1.getNameID();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER:
                MonsterTemplate _monster = DataTemplate.GetInstance().m_MonsterTable.getTableData(itemID) as MonsterTemplate;
                if (_monster != null)
                {
                    _result = _monster.getMonstername();
                }
                break;
        }
        if (_result != null)
            _result = GameUtils.getString(_result);

        return _result;
    }
    public static string GetSpriteName(int itmeID, bool isLargeIcon = false)
    {
        string _result = null;

        EM_OBJECT_CLASS _awardClass = GameUtils.GetObjectClassById(itmeID);
        switch (_awardClass)
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                ResourceindexTemplate _resTable = DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itmeID) as ResourceindexTemplate;
                if (_resTable != null)
                {
                    _result = _resTable.getIcon3();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                ItemTemplate _itemTable = DataTemplate.GetInstance().m_ItemTable.getTableData(itmeID) as ItemTemplate;
                if (_itemTable != null)
                {
                    if (isLargeIcon)
                        _result = _itemTable.getIcon();
                    else
                        _result = _itemTable.getIcon_s();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                ItemTemplate _runeTable = DataTemplate.GetInstance().m_ItemTable.getTableData(itmeID) as ItemTemplate;
                if (_runeTable != null)
                {
                    if (isLargeIcon)
                        _result = _runeTable.getIcon();
                    else
                        _result = _runeTable.getIcon_s();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                HeroTemplate _heroTable = DataTemplate.GetInstance().m_HeroTable.getTableData(itmeID) as HeroTemplate;

                if (_heroTable != null)
                {
                    ArtresourceTemplate _atrResTable = DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_heroTable.getArtresources()) as ArtresourceTemplate;
                    if (isLargeIcon)
                        _result = _atrResTable.getHeadartresource();
                    else
                        _result = _atrResTable.getHeadiconresource();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                ArtresourceTemplate _atrResTable1 = DataTemplate.GetInstance().m_ArtresourceTable.getTableData(itmeID) as ArtresourceTemplate;
                if (_atrResTable1 != null)
                {
                    if (isLargeIcon)
                        _result = _atrResTable1.getHeadartresource();
                    else
                        _result = _atrResTable1.getHeadiconresource();
                }
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER:
                MonsterTemplate _monster = DataTemplate.GetInstance().m_MonsterTable.getTableData(itmeID) as MonsterTemplate;
                if (_monster != null)
                {
                    ArtresourceTemplate _atrResTable = DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_monster.getArtresources()) as ArtresourceTemplate;
                    if (isLargeIcon)
                        _result = _atrResTable.getHeadartresource();
                    else
                        _result = _atrResTable.getHeadiconresource();
                }
                break;
        }

        return _result;
    }

    public static Sprite GetSprite(int itmeID, bool isLargeIcon = false)
    {
        Sprite _image = null;
        string _spriteName = GetSpriteName(itmeID, isLargeIcon);
        if (_spriteName != null)
        {
            StringBuilder _path = new StringBuilder();
            if (isLargeIcon)
            {
                _path.Append(common.defaultPath);
            }
            else
            {
                _path.Append(common.defaultPath);
            }
            _path.Append(_spriteName);
            _image = UIResourceMgr.LoadSprite(_path.ToString());
        }
        return _image;
    }

    /// <summary>
    /// 从一个掉落表ID （15表）获取到包内所有的物品ID（140X....）
    /// </summary>
    /// <returns></returns>
    public static List<InnerdropTemplate> UnpackDorpPack(int packageID)
    {
        List<InnerdropTemplate> _result = new List<InnerdropTemplate>();
        NormaldropTemplate _packageData = DataTemplate.GetInstance().m_NormaldropTable.getTableData(packageID) as NormaldropTemplate;
        var _packArrTable = DataTemplate.GetInstance().m_InnerdropTable.getData();

        int[] _packArr = _packageData.getInnerdrop();
        foreach (InnerdropTemplate innerPack in _packArrTable.Values)
        {
            bool _InArr = false;
            int _id = innerPack.getInnerdropid();
            for (int i = 0; i < _packArr.Length; i++)
            {
                if (_id == _packArr[i])
                {
                    _InArr = true;
                    break;
                }
            }
            if (_InArr)
            {
                _result.Add(innerPack);
            }
        }
        return _result;
    }
}

public class UI_WorldBoss : UI_BaseWorldBoss
{
    //传说盛典左侧按钮需要使用的参数
    private readonly Vector2 ActiveToggleSize = new Vector2(330,175);   //激活时的尺寸
    private readonly Vector2 DisableToggleSize = new Vector2(260, 175); //按钮没有激活时的尺寸
    private readonly Color DisableColor = new Color(0.545f, 0.545f, 0.6196f);//按钮没有激活时文字的颜色
    //排行榜文字颜色参数
    private readonly Color RankingYellow = new Color32(254,231,17,255);
//    private readonly Color RankingGray = new Color32();
    private enum ActiveUIPanel
    {
        SelectBossPanel,
        FightPanel,
        BloodPanel,
        ItemPanel,
        AwardCheckPanel
    }
    private enum ActiveToggle
    {
        Blood,
        Rune,
        Hunter
    }
    [System.Flags]
    private enum BossOverFlag
    { 
        NONE = 0,
        ONE = 1,
        TWO = 1<<1,
        THREE = 1<<2,
        FORE = 1<<3,
        ALL = 15
    }
    #region InnerClass
    private class RankingItem : DynamicItem
    {
        private Text m_PlayerNameText;
        private Text m_Ranking;
        private Text m_PlayerDamageText;
        private GameObject m_RankingName;
        private GameObject m_PlayerDamage;
        private Transform m_MaskPoint;

        public Vector3 MaskPositon
        {
            get { return m_MaskPoint.position; }
        }
        public void ReceiveChild(RectTransform child)
        {
            child.SetParent(m_MaskPoint);
            child.anchoredPosition = Vector2.zero;
        }
        public void SetRankingInfo(int ranking,string playerName,long damage)
        {
            m_Ranking.text = ranking.ToString();
            m_PlayerNameText.text = playerName;
            m_PlayerDamageText.text = damage.ToString();
        }
        public void SetRankingColor(Color color)
        {
            m_Ranking.color = color;
        }
        public void EnableSelf(bool value)
        {
            m_RankingName.SetActive(value);
            m_PlayerDamage.SetActive(value);
        }

        private RankingItem() { }

        /// <summary>
        /// 接收两个预制件信息（没实例化）和一个挂载点，完成实例化游戏物体，并返回一个实例化的RankingItem类
        /// </summary>
        /// <param name="originalRankingName"></param>
        /// <param name="originalPlayerDamage"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static RankingItem GenerateItem(GameObject originalRankingName, GameObject originalPlayerDamage,Transform layout)
        {
            if (originalRankingName == null || originalPlayerDamage == null || layout == null)
                return null;

            RankingItem _item = new RankingItem();
            _item.m_RankingName = InstantiateObject(originalRankingName, layout);
            _item.m_PlayerNameText = _item.m_RankingName.transform.FindChild("RankingNameText").GetComponent<Text>();
            _item.m_Ranking = _item.m_RankingName.transform.FindChild("RankText").GetComponent<Text>();
            _item.m_PlayerDamage = InstantiateObject(originalPlayerDamage, layout);
            _item.m_PlayerDamageText = _item.m_PlayerDamage.GetComponent<Text>();
            _item.m_MaskPoint = _item.m_RankingName.transform.FindChild("MaskPosition");
            return _item;
        }

    }

    private class BossItem : DynamicItem
    {
        private GameObject m_SelectMask;
        private GameObject m_KillMask;

        private Image m_BossImage;
        private Text m_BossBottomText;
        private Text m_BossTopText;
        private Text m_BossTimeText;

        private WorldBossData m_BossData;

        public void SetBossInfo(WorldBossData bossData,int timeOffset)
        {
            m_BossData = bossData;
            var _sprite = GetSprite(m_BossData.m_BossTableID,true);
            if (_sprite != null)
            {
                m_BossImage.sprite = _sprite;
                m_BossImage.preserveAspect = true;
            }

            if (IsGuard)
            {
                m_BossBottomText.text = GameUtils.getString("legend_of_the_war_content32");
            }
            else
            {
                m_BossBottomText.text = string.Format(GameUtils.getString("legend_of_the_war_content33"),GetBossName(bossData));
            }


            RefreshInfo(timeOffset);
//            RefreshTimeText();
        }

        public void RefreshInfo(int timeOffset)
        {
            

            if (m_BossData.m_IsOpen > 0)//已开启
            {
                m_BossTimeText.gameObject.SetActive(true);
                GameUtils.SetImageGrayState(m_BossImage, false);
                if (m_BossData.m_IsKilled > 0)//被击杀
                {
                    m_KillMask.SetActive(true);
                    StringBuilder _stringBuilder = new StringBuilder(GameUtils.getString("legend_of_the_war_content31"));
                    _stringBuilder.Append('\n');
                    _stringBuilder.Append(m_BossData.m_KillName);
                    m_BossTimeText.text = _stringBuilder.ToString();

                    m_BossTopText.text = GameUtils.getString("legend_of_the_war_content31");
                    //MonsterTemplate _monster = DataTemplate.GetInstance().m_MonsterTable.getTableData(m_BossData.m_BossTableID) as MonsterTemplate;
                    //if (_monster != null)
                    //{
                    //    m_BossTopText.text = GameUtils.getString(_monster.getMonstername());
                    //}
                }
                else
                {
                    m_BossTimeText.text = string.Format("{0}:{1}", m_BossData.m_TimeCount / 60, m_BossData.m_TimeCount % 60);

                    m_KillMask.SetActive(false);
                    m_SelectMask.SetActive(true);

                    m_BossTopText.text = GameUtils.getString("legend_of_the_war_content25");
                }
            }
            else//未开启
            {
                m_SelectMask.SetActive(false);
                m_BossTimeText.gameObject.SetActive(false);
                if (timeOffset < 0) //尚未到达
                {
                    GameUtils.SetImageGrayState(m_BossImage, false);
                    switch(m_BossData.m_BossType)
                    { 
                        case 1:
                            m_BossTopText.text = GameUtils.getString("legend_of_the_war_bubble4");
                            break;
                        case 2:
                            m_BossTopText.text = GameUtils.getString("legend_of_the_war_bubble8");
                            break;
                        case 3: 
                            m_BossTopText.text = GameUtils.getString("legend_of_the_war_bubble9");
                            break;
                        case 4:
                            m_BossTopText.text = GameUtils.getString("legend_of_the_war_bubble10");
                            break;
                    
                    }
                }
                else//已经过去
                {
                    GameUtils.SetImageGrayState(m_BossImage,true);
                    m_BossTopText.text = GameUtils.getString("legend_of_the_war_bubble3");
                }
      
            }
        }

        public void RefreshTimeText()
        {
            if (m_BossData.m_IsKilled > 0)//被击杀
            {
                m_KillMask.SetActive(true);
                //StringBuilder _stringBuilder = new StringBuilder(GameUtils.getString("legend_of_the_war_content31"));
                //_stringBuilder.Append('\n');
                //_stringBuilder.Append(m_BossData.m_KillName);
                //m_BossTimeText.text = _stringBuilder.ToString();
                m_BossTimeText.text = m_BossData.m_KillName;
            }
            else
                m_BossTimeText.text = UI_WorldBoss.SecondToString(m_BossData.m_TimeCount);
        }

        private static string GetBossName(WorldBossData bossData)
        {
            string _name = null;
            MonsterTemplate _monster = DataTemplate.GetInstance().m_MonsterTable.getTableData(bossData.m_BossTableID) as MonsterTemplate;
            if (_monster != null)
            {
                _name = GameUtils.getString(_monster.getMonstername());
            }
            return _name;
        }
        private bool IsGuard
        {
            get{return m_BossData.m_BossType == 1 || m_BossData.m_BossType == 3;}
        }
        private BossItem() { }

        /// <summary>
        /// 输入一个已经实例化的游戏物体，返回BossItem类
        /// </summary>
        /// <returns></returns>
        public static BossItem GenerateItem(GameObject go)
        {
            if(go == null)
                return null;

            BossItem _item = new BossItem();
            Transform _trans = go.transform;
            _item.m_SelectMask = _trans.FindChild("SelectMark").gameObject;
            _item.m_KillMask = _trans.FindChild("Background/KillMask").gameObject;
            _item.m_BossImage = _trans.FindChild("Background").GetComponent<Image>();
            _item.m_BossBottomText = _trans.FindChild("Background/BossBottomText").GetComponent<Text>();
            _item.m_BossTopText = _trans.FindChild("Background/Layout/BossTopText").GetComponent<Text>();
            _item.m_BossTimeText = _trans.FindChild("Background/Layout/BossTimeText").GetComponent<Text>();

            return _item;
        }
    }

    private class AwardItem
    {
        private UniversalItemCell m_Cell; 

        public void SetAwardItemInfo(int itemID,int count)
        {
            m_Cell.InitByID(itemID,count);
        }
        private AwardItem(){}

        public static AwardItem GenerateItem(Transform layout)
        {
            if (layout == null)
                return null;

            AwardItem _item =  new AwardItem();
            _item.m_Cell = UniversalItemCell.GenerateItem(layout);
            _item.m_Cell.transform.localScale = Vector3.one * 0.77f;

            return _item;
        }

        
    }

    private class BloodItem : DynamicItem
    {
        private bool m_IsListening = false;
        private bool m_HaveThisItem = false;
        private LegendexchargeTemplate m_ItemData;

        private Image m_BloodItemImage;
        private Text m_BloodItemNameText;
        private Text m_ExchangedText;
        private GameObject m_NotActiveMask;
        private Text m_NotActiveMaskText;
        private Button m_BloodExchangeBtn;
        private Text m_BloodExchangeBtnText;
        private Image m_ExchangeResImage;
        private Text m_ExchangeResCountText;

        private Action<GameEvent> m_ButtonCallBack;

        public void SetItemInfo(LegendexchargeTemplate data,Action<GameEvent> buttonCallBack)
        {
            m_ButtonCallBack = buttonCallBack;
            m_ItemData = data;
            Sprite _sprite = GetSprite(int.Parse(data.getShow()));
            if(_sprite != null)
            {
                m_BloodItemImage.sprite = _sprite;
                m_BloodItemImage.preserveAspect = true;
            }
            m_BloodItemNameText.text = GameUtils.getString(data.getName());
            m_BloodExchangeBtnText.text = GameUtils.getString("legend_of_the_war_button10");
            m_ExchangeResCountText.text = data.getCost().ToString();

            if (m_IsListening)
            {
                GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyBossShop, ButtonCallBack);
                m_IsListening = false;
            }

            var _item = UnpackDorpPack(data.getReward())[0];
            m_HaveThisItem = ObjectSelf.GetInstance().IsGetTheHeroBlood(_item.getObjectid());
            if ( m_HaveThisItem )//已经拥有该物品
            {
                m_ExchangedText.text = GameUtils.getString("legend_of_the_war_content34");
                m_ExchangedText.gameObject.SetActive(true);
            }
            else
            {
//                m_ExchangedText.text = GameUtils.getString("legend_of_the_war_button10");
                m_ExchangedText.gameObject.SetActive(false);
            }
        }
        public void OnRelease()
        {
            if (m_IsListening)
            {
                GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyBossShop, ButtonCallBack);
                m_IsListening = false;
            }
        }
        public static BloodItem GenerateItem(GameObject originalBloodItem,Transform layout)
        {
            if (originalBloodItem == null || layout == null)
                return null;

            GameObject _go = InstantiateObject(originalBloodItem, layout);
            BloodItem _item = new BloodItem();
            Transform _trans = _go.transform;

            _item.m_BloodItemImage = _trans.FindChild("BloodItemImage").GetComponent<Image>();
            _item.m_BloodItemNameText = _trans.FindChild("BloodItemImage/BloodItemNameImage/BloodItemNameText").GetComponent<Text>();
            _item.m_ExchangedText = _trans.FindChild("BloodItemImage/BloodItemNameImage/ExchangedText").GetComponent<Text>();
            _item.m_NotActiveMask = _trans.FindChild("BloodItemImage/NotActiveMask").gameObject;
            _item.m_NotActiveMaskText = _trans.FindChild("BloodItemImage/NotActiveMask/NotActiveMaskText").GetComponent<Text>();
            _item.m_BloodExchangeBtn = _trans.FindChild("BloodExchangeBtn").GetComponent<Button>();
            _item.m_BloodExchangeBtnText = _trans.FindChild("BloodExchangeBtn/BloodExchangeBtnText").GetComponent<Text>();
            _item.m_ExchangeResImage = _trans.FindChild("BloodExchangeBtn/ExchangeResImage").GetComponent<Image>();
            _item.m_ExchangeResCountText = _trans.FindChild("BloodExchangeBtn/ExchangeResImage/ExchangeResCountText").GetComponent<Text>();

            _item.m_BloodExchangeBtn.onClick.AddListener(_item.OnClickBloodExchangeBtn);

            return _item;
        }

        private void OnClickBloodExchangeBtn()
        {
            if (m_HaveThisItem)//已经拥有该物品
                return;

            var _objSelf = ObjectSelf.GetInstance();
            if (_objSelf.WorldBossMgr.m_ChuanShuoZS < m_ItemData.getCost())    //无足够货币购买
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("legend_of_the_war_bubble6"), UI_WorldBoss.GetInst().transform);
                return;
            }

            var _heroBag = _objSelf.HeroContainerBag;
            if (_heroBag.GetHeroList().Count >= _heroBag.GetHeroBagSizeMax())    //背包内无足够空间
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble10"), UI_WorldBoss.GetInst().transform);
            }

            CBuyBossShop _protocol = new CBuyBossShop();
            _protocol.bossshopid = m_ItemData.getId();
            IOControler.GetInstance().SendProtocol(_protocol);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_SBuyBossShop, ButtonCallBack);
            m_IsListening = true;
        }

        private void ButtonCallBack(GameEvent eventData)
        {
            WorldBossCallbackParaPackage _package = (WorldBossCallbackParaPackage)eventData.data;
            if (_package.m_BossShopID != m_ItemData.getId())
                return;

            if (m_ButtonCallBack != null)
                m_ButtonCallBack(eventData);
            if (m_IsListening)
            {
                GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyBossShop, ButtonCallBack);
                m_IsListening = false;
            }
        }
    }

    private class ShopItem : DynamicItem
    {
        private bool m_IsListening = false;
        private LegendexchargeTemplate m_ItemData;

        private Image m_ItemImage;
        private Button m_ExchangeBtn;
        private Text m_ExchangeBtnText;
        private Image m_ExchangeResImage;
        private Text m_ExchangeResCountText;
        private Text m_ItemNameText;
        private Action<GameEvent> m_ButtonCallBack;

        private RuneIconItem m_RuneIconItem;
        private Transform m_RuneTrans;

        public GameObject m_SelfObject;

        public void SetItemInfo(LegendexchargeTemplate data, Action<GameEvent> buttonCallBack)
        {
            m_ItemData = data;
            m_ButtonCallBack = buttonCallBack;
            int _itemId = -1;
            Sprite _sprite;
            if(data.getType() == 3)
            {
                StringBuilder _path = new StringBuilder();
                _path.Append(common.defaultPath);
                _path.Append(data.getShow());
                _sprite = UIResourceMgr.LoadSprite(_path.ToString());
            }
            else
            {
                _itemId = int.Parse(data.getShow());
                _sprite = GetSprite(_itemId);
            }
            if (_sprite != null)
            {
                m_ItemImage.sprite = _sprite;
                m_ItemImage.preserveAspect = true;
            }
            else
            {
                Debug.Log(string.Format("无法找到图片资源{0}：57表ID {1}",data.getShow(),data.getId()));
            }

            ItemTypeProcess(_itemId, _sprite);
            m_ItemNameText.text = GameUtils.getString(data.getName());
            if (m_ItemNameText.text == null || m_ItemNameText.text.Length == 0)
                Debug.Log(string.Format("无法找到物品名称文字数据：57表ID{0},05表索引 {1}", data.getId(), data.getName()));
            m_ExchangeBtnText.text = GameUtils.getString("legend_of_the_war_button10");
            m_ExchangeResCountText.text = data.getCost().ToString();


            if (m_IsListening)
            {
                GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyBossShop, ButtonCallBack);
                m_IsListening = false;
            }
        }
        private void ItemTypeProcess(int itemid,Sprite icon)
        {
            if (m_ItemData.getType() != 2 && m_ItemData.getType() != 3)
            {
                m_ItemImage.enabled = true;
                if (icon != null)
                    m_ItemImage.sprite = icon;
                if (m_RuneTrans != null)
                    m_RuneTrans.gameObject.SetActive(false);
                return;
            }

            if (m_RuneTrans == null)
            {
                GameObject go = UIResourceMgr.LoadPrefab(common.prefabPath + "UI_Rune/RuneIconItem") as GameObject;
                go = GameObject.Instantiate(go, m_ItemImage.transform.position, m_ItemImage.transform.rotation) as GameObject;
                m_RuneTrans = go.transform;
                m_RuneTrans.SetParent(m_ItemImage.transform);
                m_RuneTrans.localScale = Vector3.one * 1.5f;
                m_RuneIconItem = new RuneIconItem(m_RuneTrans);
            }
            m_RuneTrans.gameObject.SetActive(true);
            m_ItemImage.enabled = false;

//            rune.SetRuneType(1);
//            m_RuneIconItem.SetIsSpecial(data.getType() == 3);
            m_RuneIconItem.SetLevelInfoActive(false);
            if (icon != null)
                m_RuneIconItem.SetIcon(icon);
            if (itemid > 0)
            {
                int type = DataTemplate.GetInstance().GetItemTemplateById(itemid).getRune_type();
                if (type == 5 || type == 6)
                    m_RuneIconItem.SetIsSpecial(true);
                else
                    m_RuneIconItem.SetRuneType(type);
            }
            

        }
        public void OnRelease()
        {
            if (m_IsListening)
            {
                GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyBossShop, ButtonCallBack);
                m_IsListening = false;
            }
        }

        private ShopItem() { }

        public static ShopItem GenerateItem(GameObject originalShopItem,Transform layout)
        {
            if (originalShopItem == null || layout == null)
                return null;

            GameObject _go = InstantiateObject(originalShopItem, layout);

            Transform _trans = _go.transform;
            ShopItem _item = new ShopItem();
            _item.m_SelfObject = _go;
            _item.m_ItemImage = _trans.FindChild("ItemImage").GetComponent<Image>();
            _item.m_ExchangeBtn = _trans.FindChild("ExchangeBtn").GetComponent<Button>();
            _item.m_ExchangeBtnText = _trans.FindChild("ExchangeBtn/ExchangeBtnText").GetComponent<Text>();
            _item.m_ExchangeResImage = _trans.FindChild("ExchangeBtn/ExchangeResImage").GetComponent<Image>();
            _item.m_ExchangeResCountText = _trans.FindChild("ExchangeBtn/ExchangeResImage/ExchangeResCountText").GetComponent<Text>();
            _item.m_ItemNameText = _trans.FindChild("ItemNameImage/ItemNameText").GetComponent<Text>();

            _item.m_ExchangeBtn.onClick.AddListener(_item.OnClickExchangeBtn);
            return _item;
        }

        private void OnClickExchangeBtn()
        {
            var _objSelf = ObjectSelf.GetInstance();
            if(_objSelf.WorldBossMgr.m_ChuanShuoZS<m_ItemData.getCost())    //无足够货币购买
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("legend_of_the_war_bubble6"), UI_WorldBoss.GetInst().transform);
                return;
            }

            if (m_ItemData.getType() == 4 &&
                DataTemplate.GetInstance().GetGameConfig().getLegend_excharge_max_num() <= _objSelf.WorldBossMgr.m_ShopExchangeNum)//兑换次数不足
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("legend_of_the_war_bubble7"), UI_WorldBoss.GetInst().transform);
                return;
            }
            var _itemBag = _objSelf.CommonItemContainer;
            if (_itemBag.GetBagItemSum() >= _itemBag.GetBagItemSizeMax())    //背包内无足够空间
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble9"), UI_WorldBoss.GetInst().transform);
            }

            CBuyBossShop _protocol = new CBuyBossShop();
            _protocol.bossshopid = m_ItemData.getId();
            IOControler.GetInstance().SendProtocol(_protocol);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_SBuyBossShop, ButtonCallBack);
            m_IsListening = true;
        }


        private void ButtonCallBack(GameEvent eventData)
        {
            WorldBossCallbackParaPackage _package = (WorldBossCallbackParaPackage)eventData.data;
            if (_package.m_BossShopID != m_ItemData.getId())
                return;

            if (m_ButtonCallBack != null)
                m_ButtonCallBack(eventData);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyBossShop, ButtonCallBack);
            m_IsListening = false;
        }
    }
    #endregion

    private static readonly string Path = "UI_WorldBoss/UI_WorldBoss_2_11";
    private static UI_WorldBoss Inst;
    ActiveUIPanel m_CurrentPanel;

    private Sprite m_BossSelectBackground;
    private Sprite m_HpFillGreen;
    private Sprite m_HpFillYellow;
    private Sprite m_HpFillRed;
    //需要动态改变的图片，图片文件不在Resource文件夹下，暂时依靠手动拖拽初始化
    public Font m_Font;
    public Font m_Font2;
 
    public Sprite m_BossBackground;

    public Sprite m_ToggleImage;    //传说盛典左侧三个Toggle，被选中时的背景图（BloodCheckmark/RuneCheckmark/HunterCheckmark）
    public Sprite m_DisableToggleImage;//没被选中时的背景图

    public Sprite m_BloodMark2;     //Mark2是中心的图标
    public Sprite m_BloodMark2Disable;
    public Sprite m_RuneMark2;
    public Sprite m_RuneMark2Disable;
    public Sprite m_HunterMark2;
    public Sprite m_HunterMark2Disable;
    //需要动态加载的GameObject资源
    private GameObject m_OriginalRankingName;
    private GameObject m_OriginalPlayerDamage;
    private GameObject m_OriginalBloodItem;
    private GameObject m_OriginalAwardItem;
    private GameObject m_OriginalShopItem;

    #region Component
    private Image m_BackgroundImage;
    private Transform m_CaptionLayoutPoint;
    //UI的三个主要面板父节点
    private GameObject m_BossPanel;
    private GameObject m_LegendTemplePanel;
    private GameObject m_AwardCheckPanel;

    //BossPanel面板下的三个子面板
    private GameObject m_SelectBossPanel;
    private GameObject m_RankingPanel;
    private GameObject m_FightPanel;

    //RankingPanel下
    private Transform m_RankingPanelLayout;
    private RectTransform m_SelfMask;

    //FightPanel下
    private Slider m_BossHPBar;
    private Image m_HpImage;
    private GameObject m_BlessingTipsPanel;
    private GameObject m_SoulTipsPanel;
    private GameObject m_FightBtnBg;
    private GameObject m_PayFightBtnBg;

    //购买守望之灵
    private Image m_SoulSubButtonIamge;
    private Image m_SoulAddButtonIamge;
    private EventTriggerListener m_SoulAddBtnListener;
    private EventTriggerListener m_SoulSubBtnListener;

    private bool m_IsPressingAddBtn = false;
    private bool m_IsPressingSubBtn = false;
    private bool m_PressedAddBtn = false;
    private bool m_PressedSubBtn = false;
    private GameObject m_WatcherSoulPanel;
    private float StartPressInterval = 0.5f;
    private float MinPressInterval = 0.05f;
    private float MaxDelta = 20f;
    private int m_CurrentSoulCount; //购买守望之灵面板当前要购买的数量







    private GameObject m_BlessingPanel;

    //LegendTemplePanel下
    private bool m_IsInitLegendTemplePanel;
    private Toggle m_BloodToggle;
    private RectTransform m_BloodToggleTrans;
    private Image m_BloodToggleImage;
    private Image m_BloodToggleImage2;
    private Toggle m_RuneToggle;
    private RectTransform m_RuneToggleTrans;
    private Image m_RuneToggleImage;
    private Image m_RuneToggleImage2;
    private bool m_RuneToggleLastState;
    private Toggle m_HunterToggle;
    private RectTransform m_HunterToggleTrans;
    private Image m_HunterToggleImage;
    private Image m_HunterToggleImage2;
    private bool m_HunterToggleLastState;
    private GameObject m_BloodPanel;
    private GameObject m_ItemPanel;
    private RectTransform m_BloodPanelLayout;
    private RectTransform m_ItemPanelLayout;
    private Vector2 m_BPLayoutBackup;      //m_BloodPanelLayout的初始位置备份
    private Vector2 m_IPLayoutBackup;      //m_ItemPanelLayout的初始位置备份

    private GameObject m_BPScrollbar;       //古神血脉界面底部的滑动条
    private GameObject m_IPScrollbar;       //其他两个界面的滑动条

    //AwardCheckPanel下
    private Transform m_AwardCheckPanelLayout;
    private Vector2 m_ACPLayoutBackup;      //m_AwardCheckPanelLayout的初始位置备份
    private Transform[] m_AwardRankLayoutArray;
    #endregion

    //数据
    private List<RankingItem> m_RankingList;
    private List<BossItem> m_BossList;
    private List<AwardItem> m_AwardItemList;
    private List<BloodItem> m_BloodItemList;
    private List<ShopItem> m_ShopItemList;

    private SortedList<int, LegendexchargeTemplate> m_BloodItemDataList;
    private SortedList<int, LegendexchargeTemplate> m_RuneItemDataList;
    private SortedList<int, LegendexchargeTemplate> m_HunterItemDataList;

    private InterfaceControler m_MessageBox;
    private ObjectSelf m_ObjectSelf;
    private WorldBossManager m_WorldBossManager;
    private GameConfig m_Config;

    private bool m_ComebackFormBattle = false;

    private int m_CurrentBossID;

    //当祝福所需守望之灵不足时，需要购买的守望之灵数量
    private int m_SoulNeedToBuy = 0;

    private void LoadSprites()
    {
        m_HpFillGreen = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_tilizhi");
        m_HpFillYellow = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_tilizhihuang"); ;
        m_HpFillRed = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_tilizhihong"); ;
        //m_ToggleImage = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_tilizhi"); ;
        //m_DisableToggleImage = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_tilizhi"); ;

//        m_Font = UIResourceMgr.lo
    }
    public static string GetPath()
    {
        return Path;
    }
    public static UI_WorldBoss GetInst()
    {
        return Inst;
    }
    public void ComebackFormBattle()
    {
        m_ComebackFormBattle = true;
    }
    private void LoadAllComponent()
    {
        m_BackgroundImage = selfTransform.FindChild("Background").GetComponent<Image>();
        m_BossSelectBackground = m_BackgroundImage.sprite;

        m_OriginalRankingName = selfTransform.FindChild("OriginalPanel/OriginalRankingName").gameObject;
        m_OriginalPlayerDamage = selfTransform.FindChild("OriginalPanel/OriginalPlayerDamage").gameObject;
        m_OriginalBloodItem = selfTransform.FindChild("OriginalPanel/OriginalBloodItem").gameObject;
        m_OriginalAwardItem = selfTransform.FindChild("OriginalPanel/OriginalAwardItem").gameObject;
        m_OriginalShopItem = selfTransform.FindChild("OriginalPanel/OriginalShopItem").gameObject;

        m_BossPanel = selfTransform.FindChild("BossPanel").gameObject;
        m_LegendTemplePanel = selfTransform.FindChild("LegendTemplePanel").gameObject;
        m_AwardCheckPanel = selfTransform.FindChild("AwardCheckPanel").gameObject;

        m_SelectBossPanel = selfTransform.FindChild("BossPanel/SelectBossPanel").gameObject;
        m_RankingPanel = selfTransform.FindChild("BossPanel/RankingPanel").gameObject;
        m_FightPanel = selfTransform.FindChild("BossPanel/FightPanel").gameObject;

        m_RankingPanelLayout = selfTransform.FindChild("BossPanel/RankingPanel/Layout");
        m_SelfMask = selfTransform.FindChild("BossPanel/RankingPanel/SelfMask").GetComponent<RectTransform>();

        m_BossHPBar = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/HPBar").GetComponent<Slider>();
        m_HpImage = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/HPBar/Fill Area/Fill").GetComponent<Image>();
        m_BlessingTipsPanel = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/BlessingTips").gameObject;
        m_SoulTipsPanel = selfTransform.FindChild("BossPanel/FightPanel/SoulTipsPanel").gameObject;
        m_FightBtnBg = selfTransform.FindChild("BossPanel/FightPanel/FightBtn/FightBtnBg").gameObject;
        m_PayFightBtnBg = selfTransform.FindChild("BossPanel/FightPanel/PayFightBtn/PayFightBtnBg").gameObject;

        m_SoulSubButtonIamge = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/SoulImage/SoulCountImage/SoulSubButton").GetComponent<Image>();
        m_SoulAddButtonIamge = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/SoulImage/SoulCountImage/SoulAddButton").GetComponent<Image>();
        m_BlessingPanel = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel").gameObject;
        m_WatcherSoulPanel = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel").gameObject;

        m_BloodToggle = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/BloodToggle").GetComponent<Toggle>();
        m_BloodToggleTrans = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/BloodToggle").GetComponent<RectTransform>();
        m_BloodToggleImage = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/BloodToggle/BloodCheckmark").GetComponent<Image>();
        m_BloodToggleImage2 = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/BloodToggle/BloodCheckmark2").GetComponent<Image>();
        m_RuneToggle = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/RuneToggle").GetComponent<Toggle>();
        m_RuneToggleTrans = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/RuneToggle").GetComponent<RectTransform>();
        m_RuneToggleImage = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/RuneToggle/RuneCheckmark").GetComponent<Image>();
        m_RuneToggleImage2 = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/RuneToggle/RuneCheckmark2").GetComponent<Image>();
        m_HunterToggle = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/HunterToggle").GetComponent<Toggle>();
        m_HunterToggleTrans = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/HunterToggle").GetComponent<RectTransform>();
        m_HunterToggleImage = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/HunterToggle/HunterCheckmark").GetComponent<Image>();
        m_HunterToggleImage2 = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/HunterToggle/HunterCheckmark2").GetComponent<Image>();

        m_BloodToggle.onValueChanged.AddListener(OnBloodToggleChange);
        m_RuneToggle.onValueChanged.AddListener(OnRuneToggleChange);
        m_HunterToggle.onValueChanged.AddListener(OnHunterToggleChange);
        m_BloodPanel = selfTransform.FindChild("LegendTemplePanel/BloodPanel").gameObject;
        m_ItemPanel = selfTransform.FindChild("LegendTemplePanel/ItemPanel").gameObject;
        m_BloodPanelLayout = selfTransform.FindChild("LegendTemplePanel/BloodPanel/BloodItemList/Layout").GetComponent<RectTransform>();
        m_ItemPanelLayout = selfTransform.FindChild("LegendTemplePanel/ItemPanel/ShopItemList/Layout").GetComponent<RectTransform>();


        m_AwardCheckPanelLayout = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout");
        m_ACPLayoutBackup = m_AwardCheckPanelLayout.position;
        m_AwardRankLayoutArray = new Transform[5];
        m_AwardRankLayoutArray[0] = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank1/BackImage/Layout");
        m_AwardRankLayoutArray[1] = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank2/BackImage/Layout");
        m_AwardRankLayoutArray[2] = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank3/BackImage/Layout");
        m_AwardRankLayoutArray[3] = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank4_5/BackImage/Layout");
        m_AwardRankLayoutArray[4] = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank6_10/BackImage/Layout");

        m_BPScrollbar = selfTransform.FindChild("LegendTemplePanel/BloodPanel/BloodItemList/Scrollbar").gameObject;
        m_IPScrollbar = selfTransform.FindChild("LegendTemplePanel/ItemPanel/ShopItemList/Scrollbar").gameObject;


    }

    public override void InitUIData()
    {
        base.InitUIData();
        LoadSprites();
        Inst = this;
        m_MessageBox = InterfaceControler.GetInst();
        m_ObjectSelf = ObjectSelf.GetInstance();
        m_WorldBossManager = m_ObjectSelf.WorldBossMgr;
        m_Config = DataTemplate.GetInstance().GetGameConfig();
        m_CurrentSoulCount = 1;

        m_CaptionLayoutPoint = selfTransform.FindChild("Background/LayoutPoint");
        LoadAllComponent();


        m_RankingList = new List<RankingItem>(16);
        m_BossList = new List<BossItem>(4);
        m_AwardItemList = new List<AwardItem>(16);
        m_BloodItemList = new List<BloodItem>(8);
        m_ShopItemList = new List<ShopItem>(4);
        m_BloodItemDataList = new SortedList<int,LegendexchargeTemplate>();
        m_RuneItemDataList = new SortedList<int, LegendexchargeTemplate>();
        m_HunterItemDataList = new SortedList<int, LegendexchargeTemplate>();


        // ------------------>>将商店物品按TypeID分类
        var _shopList = m_WorldBossManager.m_ShopList;
        var _legendExTable = DataTemplate.GetInstance().m_LegendexchargeTable;
        for (int i = 0; i < _shopList.Count; i++)
        {
            LegendexchargeTemplate _data = _legendExTable.getTableData(_shopList[i]) as LegendexchargeTemplate;
            SortedList<int, LegendexchargeTemplate> _list = null;
            switch (_data.getType())
            { 
                case 1:
                    _list = m_BloodItemDataList;
                    break;
                case 2:
                    _list = m_RuneItemDataList;
                    break;
                case 3:
                    _list = m_RuneItemDataList;
                    break;
                case 4:
                    _list = m_HunterItemDataList;
                    break;
            }
            if (_list != null)
            {
                _list.Add(GetSortKey(_data),_data);
            }
        }

        m_SoulAddBtnListener = EventTriggerListener.Get(m_SoulAddButton.gameObject);
        m_SoulSubBtnListener = EventTriggerListener.Get(m_SoulSubButton.gameObject);
        m_SoulAddBtnListener.onPress = OnPressSoulAddButton;
        m_SoulAddBtnListener.onDown = EnterPressSoulAddButton;
        m_SoulSubBtnListener.onPress = OnPressSoulSubButton;
        m_SoulSubBtnListener.onDown = EnterPressSoulSubButton;
        m_SoulAddBtnListener.needResetInterval = true;
        m_SoulSubBtnListener.needResetInterval = true;
        m_SoulAddBtnListener.InitPressInterval = StartPressInterval;
        m_SoulSubBtnListener.InitPressInterval = StartPressInterval;

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_GetMyWorldBoss, RefreshBossHpBar);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_SBuyWatcherSoul, SBuyWatcherSoulHandler);


    }

    public override void InitUIView()
    {
        base.InitUIView();
        InitPanelState();


        InitBossSelectPanel();
        InitRankingPanel();
        InitFightPanel();
        InitAwardCheckPanel();

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_SGetBossRank, SGetBossRankHandler);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_GetWorldBoss, OnGetWorldBossData);
        InvokeRepeating("OneSecHandler", 1f, 1f);
        InvokeRepeating("ThreeSecHandler", 0.1f, 3f);
        OneSecHandler();

        if (m_ComebackFormBattle)
        {
            m_ComebackFormBattle = false;
            WorldBossData _boss = GetCurrentActiveBoss();
            if (_boss != null && _boss.m_IsKilled == 0 && !IsGuard(_boss))
            {
                ChangePanel(ActiveUIPanel.FightPanel);
            }
        }

        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.AwakeUp(m_CaptionLayoutPoint);
    }

    public void OnDestroy()
    {
        Inst = null;

        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.Release(m_CaptionLayoutPoint);

        for (int i = 0; i < m_BloodItemList.Count; i++)
        {
            m_BloodItemList[i].OnRelease();
        }
        for (int i = 0; i < m_ShopItemList.Count; i++)
        {
            m_ShopItemList[i].OnRelease();
        }

        CancelInvoke("OneSecHandler");
        CancelInvoke("ThreeSecHandler");
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GetMyWorldBoss, EnableFightPanel);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyWatcherSoul, SBuyWatcherSoulHandler);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SGetBossRank, SGetBossRankHandler);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GetMyWorldBoss, RefreshBossHpBar);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GetWorldBoss, OnGetWorldBossData);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyWatcherSoul, OnClickPayForBlessingBtn);
    }

    private void InitPanelState()
    {
        m_BossPanel.SetActive(true);
        m_SelectBossPanel.SetActive(true);
        m_RankingPanel.SetActive(true);
        m_FightPanel.SetActive(false);
        m_BlessingPanel.SetActive(false);
        m_WatcherSoulPanel.SetActive(false);
        m_SoulTipsPanel.SetActive(false);

        m_IsInitLegendTemplePanel = false;
        m_LegendTemplePanel.SetActive(false);
        m_BloodPanel.SetActive(true);
        m_ItemPanel.SetActive(false);

        m_AwardCheckPanel.SetActive(false);
        m_CurrentPanel = ActiveUIPanel.SelectBossPanel;
    }
    private void InitBossSelectPanel()
    {
        m_SelectBossTopTittleText.text = GameUtils.getString("legend_of_the_war_content29");//----------------------?
        m_SelectBossBackText.text = GameUtils.getString("common_button_return");
        m_DescriptionTittleText.text = GameUtils.getString("legend_of_the_war_content29"); //----------------------?
        m_DescriptionText1.text = GameUtils.getString("legend_of_the_war_content26");
        m_DescriptionText2.text = GameUtils.getString("legend_of_the_war_content27");
        m_DescriptionText3.text = GameUtils.getString("legend_of_the_war_content28");
        m_ChallengeBtnText.text = GameUtils.getString("legend_of_the_war_button3");
        m_AwardCheckText.text = GameUtils.getString("legend_of_the_war_button1");
        m_LegendTempleText.text = GameUtils.getString("legend_of_the_war_button2");

        BossItem _boss;
        GameObject _go;
        _go = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/SubPanel/Boss1").gameObject;
        _boss = BossItem.GenerateItem(_go);
        m_BossList.Add(_boss);
        _go = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/SubPanel/Boss2").gameObject;
        _boss = BossItem.GenerateItem(_go);
        m_BossList.Add(_boss);
        _go = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/SubPanel/Boss3").gameObject;
        _boss = BossItem.GenerateItem(_go);
        m_BossList.Add(_boss);
        _go = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/SubPanel/Boss4").gameObject;
        _boss = BossItem.GenerateItem(_go);
        m_BossList.Add(_boss);

        DateTime _date = m_ObjectSelf.ServerDateTime;
        int _cur = _date.Hour * 3600 + _date.Minute * 60 + _date.Second;
        int _start = 0;
        int _end = 0;

        StringToSecond(m_Config.getLegend_watcher_open_time()[0], out _start, out _end);
        m_BossList[0].SetBossInfo(m_WorldBossManager.m_BossDataMap[(int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_1],
            TimeUtils.TimeDurationCompare(_cur, _start, _end));

        StringToSecond(m_Config.getLegend_fight_open_time()[0], out _start, out _end);
        m_BossList[1].SetBossInfo(m_WorldBossManager.m_BossDataMap[(int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_2],
            TimeUtils.TimeDurationCompare(_cur, _start, _end));

        StringToSecond(m_Config.getLegend_watcher_open_time()[1], out _start, out _end);
        m_BossList[2].SetBossInfo(m_WorldBossManager.m_BossDataMap[(int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_3],
            TimeUtils.TimeDurationCompare(_cur, _start, _end));

        StringToSecond(m_Config.getLegend_fight_open_time()[1], out _start, out _end);
        m_BossList[3].SetBossInfo(m_WorldBossManager.m_BossDataMap[(int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_4],
            TimeUtils.TimeDurationCompare(_cur, _start, _end));

    }
    private void InitRankingPanel()
    {
        m_RankingTittleText.text = GameUtils.getString("legend_of_the_war_bubble2");
        for (int i = 0; i < 10; i++)
        {
            RankingItem _item = RankingItem.GenerateItem(m_OriginalRankingName, m_OriginalPlayerDamage, m_RankingPanelLayout);
            m_RankingList.Add(_item);
        }
        SGetBossRankHandler();
    }
    private void InitFightPanel()
    {
        m_FightTittleText.text = GameUtils.getString("legend_of_the_war_content29");
        m_FightBackText.text = GameUtils.getString("common_button_return");
        m_MyDamageText.text = GameUtils.getString("legend_of_the_war_bubble1");
        m_BlessingBtnText.text = GameUtils.getString("legend_of_the_war_button6");
        m_FightBtnText.text = GameUtils.getString("legend_of_the_war_button4");
        m_PayFightBtText.text = GameUtils.getString("legend_of_the_war_button5");
        m_CostText.text = m_Config.getLegend_fight_cd_cost().ToString();

        //BlessingPanel
        m_ConfirmTittleText.text = GameUtils.getString("legend_of_the_war_content8");
        m_BlessingEffectTittleText.text = GameUtils.getString("legend_of_the_war_content10");
        m_PayForBlessingBtnText.text = GameUtils.getString("legend_of_the_war_button6");
        m_BlessingCloseBtnText.text = GameUtils.getString("common_button_close");
        m_BlessingBottomTipsText.text = string.Format(GameUtils.getString("legend_of_the_war_content13"),m_Config.getLegend_wish_cost().Length);
//        m_BlessingTipsPanel.SetActive(false);

        //WatcherSoulPanel
        m_WatcherSoulTittleText.text = GameUtils.getString("common_purchaseform_title");
        m_WatcherSoulTopTipsText.text = GameUtils.getString("resource_name_11");
        m_PayForSoulBtnText.text = GameUtils.getString("common_button_purchase");
        m_SoulCloseBtnText.text = GameUtils.getString("common_button_close");

        //SoulTipsPanel
        m_SoulTipsTitleText.text = GameUtils.getString("common_form_title");
        m_SoulTipsText.text = GameUtils.getString("legend_of_the_war_content35");
        m_SoulTipsPayBtnText.text = GameUtils.getString("common_button_purchase1");
        m_SoulTipsCancelBtnText.text = GameUtils.getString("heromelt_button6");

        RefreshWarcherSoulPanel();
    }
    private void InitAwardCheckPanel()
    {
        m_AwardCheckText.text = GameUtils.getString("legend_of_the_war_content21");
        m_AwardPanelRankingText.text = GameUtils.getString("legend_of_the_war_content22");
        m_AwardCloseBtnText.text = GameUtils.getString("common_button_close");
        m_AwardCheckBottomTipsText.text = GameUtils.getString("legend_of_the_war_content23");

        List<InnerdropTemplate>[] _itemMap = new List<InnerdropTemplate>[5];
        _itemMap[0] = DynamicItem.UnpackDorpPack(m_Config.getLegend_rank_reward1());
        _itemMap[1] = DynamicItem.UnpackDorpPack(m_Config.getLegend_rank_reward2());
        _itemMap[2] = DynamicItem.UnpackDorpPack(m_Config.getLegend_rank_reward3());
        _itemMap[3] = DynamicItem.UnpackDorpPack(m_Config.getLegend_rank_reward4());
        _itemMap[4] = DynamicItem.UnpackDorpPack(m_Config.getLegend_rank_reward5());

        for (int i = 0; i < _itemMap.Length; i++)
        {
            var _itemList = _itemMap[i];
            for (int j = 0; j < _itemList.Count; j++)
            {
                InnerdropTemplate _dropPack = _itemList[j];
                AwardItem _item = AwardItem.GenerateItem(m_AwardRankLayoutArray[i]);
                _item.SetAwardItemInfo(_dropPack.getObjectid(), _dropPack.getDropnum());
                m_AwardItemList.Add(_item);
            }
        }

    }

    private void InitLegendTemplePanelText()
    {
        m_LegendTempleText.text = GameUtils.getString("legend_of_the_war_content1");
        m_BloodText.text = GameUtils.getString("legend_of_the_war_button7");
        m_RuneText.text = GameUtils.getString("legend_of_the_war_button8");
        m_HunterText.text = GameUtils.getString("legend_of_the_war_button9");
        m_BloodBottomTipsText.text = GameUtils.getString("legend_of_the_war_content18");


    }

    //当商店物品小于3个时中心对齐，否则左对齐
    private void InitShopLayout(RectTransform trans, bool isLeftType)
    {
        Vector2 vector;
        if (isLeftType)
        {
            vector = new Vector2(0,0.5f);
        }
        else
        {
            vector = Vector2.one * 0.5f;
        }
        trans.pivot = vector;
        trans.anchorMax = vector;
        trans.anchorMin = vector;
        trans.anchoredPosition = Vector2.zero;
    }
    private void InitLegendTemplePanel()
    {
        InitLegendTemplePanelText();

        ResetToggleState();
        InitShopLayout(m_BloodPanelLayout,m_BloodItemDataList.Count > 3);
        m_BPLayoutBackup = m_BloodPanelLayout.anchoredPosition;
        m_BPScrollbar.SetActive(m_BloodItemDataList.Count > 3);

        foreach(var data in m_BloodItemDataList.Values)
        {
            BloodItem _bloodItem = BloodItem.GenerateItem(m_OriginalBloodItem, m_BloodPanelLayout.transform);
            _bloodItem.SetItemInfo(data, SBuyBossShopHandler);
            m_BloodItemList.Add(_bloodItem);
        }

        int _maxCount = m_RuneItemDataList.Count > m_HunterItemDataList.Count ? m_RuneItemDataList.Count : m_HunterItemDataList.Count;
        InitShopLayout(m_ItemPanelLayout, _maxCount > 3);
        m_IPLayoutBackup = m_ItemPanelLayout.anchoredPosition;
        for (int i = 0; i < _maxCount; i++)
        {
            ShopItem _item = ShopItem.GenerateItem(m_OriginalShopItem,m_ItemPanelLayout.transform);
            m_ShopItemList.Add(_item);
        }



        m_IsInitLegendTemplePanel = true;
        LoadBloodPanel();
    }
    /*******************各子面板的刷新************************/
    private void ChangePanel(ActiveUIPanel nextPanel)
    {
        if (m_CurrentPanel != nextPanel)
        {
            m_CurrentPanel = nextPanel;
            switch (nextPanel)
            { 
                case ActiveUIPanel.SelectBossPanel:
                    m_BossPanel.SetActive(true);
                    m_LegendTemplePanel.SetActive(false);
                    m_AwardCheckPanel.SetActive(false);

                    m_SelectBossPanel.SetActive(true);
                    m_RankingPanel.SetActive(true);
                    m_FightPanel.SetActive(false);
                    m_BackgroundImage.sprite = m_BossSelectBackground;
                    break;
                case ActiveUIPanel.FightPanel:
                    m_BossPanel.SetActive(true);
                    m_LegendTemplePanel.SetActive(false);
                    m_AwardCheckPanel.SetActive(false);

                    m_SelectBossPanel.SetActive(false);
                    m_RankingPanel.SetActive(true);
                    m_FightPanel.SetActive(true);
                    m_BackgroundImage.sprite = m_BossBackground;
                    LoadFightPanel();
                    break;
                case ActiveUIPanel.AwardCheckPanel:
                    m_LegendTemplePanel.SetActive(false);
                    m_AwardCheckPanel.SetActive(true);
                    m_BackgroundImage.sprite = m_BossSelectBackground;
                    break;
                case ActiveUIPanel.BloodPanel:
                    m_BossPanel.SetActive(false);
                    m_LegendTemplePanel.SetActive(true);
                    m_AwardCheckPanel.SetActive(false);

                    m_BloodPanel.SetActive(true);
                    m_ItemPanel.SetActive(false);
                    m_BackgroundImage.sprite = m_BossSelectBackground;
                    break;
                case ActiveUIPanel.ItemPanel:
                    m_BossPanel.SetActive(false);
                    m_LegendTemplePanel.SetActive(true);
                    m_AwardCheckPanel.SetActive(false);

                    m_BloodPanel.SetActive(false);
                    m_ItemPanel.SetActive(true);
                    m_BackgroundImage.sprite = m_BossSelectBackground;
                    LoadItemPanel(m_HunterToggle.isOn);
                    break;
            }
        }
    }

    private void LoadFightPanel()
    {
        if (m_CurrentBossID < 0)
            return;

        var _bossData = GetCurrentActiveBoss();
        int _bossTableID = _bossData.m_BossTableID;

        MonsterTemplate _monster = DataTemplate.GetInstance().m_MonsterTable.getTableData(_bossTableID) as MonsterTemplate;
        if (_monster != null)
        {
            foreach (HeroTemplate dateTemplate in DataTemplate.GetInstance().m_HeroTable.getData().Values)
            {
                if (dateTemplate.getTitleID() == _monster.getMonstername())
                {
                    m_BossNameText.text = GameUtils.getString(dateTemplate.getNameID());
                    break;
                }
            }
 //           m_BossNameText.text = GameUtils.getString(_monster.getMonstername());
        }

        SetGrayPayFightBtn(CurrentBossIsWatcher());

        m_MyTotalDamage.text = _bossData.m_BossRoleDB.m_AddupDamage.ToString();
        RefreshFightPanel(_bossData);
        UpdateBossHpBar(_bossData);
        OneSecHandler();
    }

    private void RefreshBossList()
    {
        var _date = m_ObjectSelf.ServerDateTime;
        int _cur = _date.Hour * 3600 + _date.Minute * 60 + _date.Second;
        int _start = 0;
        int _end = 0;
        StringToSecond(m_Config.getLegend_watcher_open_time()[0], out _start, out _end);
        m_BossList[0].RefreshInfo(TimeUtils.TimeDurationCompare(_cur, _start, _end));
        StringToSecond(m_Config.getLegend_fight_open_time()[0], out _start, out _end);
        m_BossList[1].RefreshInfo(TimeUtils.TimeDurationCompare(_cur, _start, _end));
        StringToSecond(m_Config.getLegend_watcher_open_time()[1], out _start, out _end);
        m_BossList[2].RefreshInfo(TimeUtils.TimeDurationCompare(_cur, _start, _end));
        StringToSecond(m_Config.getLegend_fight_open_time()[1], out _start, out _end);
        m_BossList[3].RefreshInfo(TimeUtils.TimeDurationCompare(_cur, _start, _end));
    }
    private void RefreshFightPanel(WorldBossData bossData)
    {
//        string _attString = GameUtils.StringWithColor(FloatToString(bossData.m_BossRoleDB.m_BlessNum * m_Config.getLegend_wish_damage_up()), TEXT_COLOR.RED);
//        string _defString = GameUtils.StringWithColor(FloatToString(bossData.m_BossRoleDB.m_BlessNum * m_Config.getLegend_wish_hurt_down()), TEXT_COLOR.RED);
        StringBuilder _stringBuilder = new StringBuilder(GameUtils.getString("legend_of_the_war_content11"));
        _stringBuilder.AppendFormat("     <color=#F9431E>{0}</color>", FloatToString(bossData.m_BossRoleDB.m_BlessNum * m_Config.getLegend_wish_damage_up()));
        m_AttackText.text = _stringBuilder.ToString();
        _stringBuilder.Remove(0,_stringBuilder.Length);
        _stringBuilder.Append(GameUtils.getString("legend_of_the_war_content12"));
        _stringBuilder.AppendFormat("     <color=#32D824>{0}</color>", FloatToString(bossData.m_BossRoleDB.m_BlessNum * m_Config.getLegend_wish_hurt_down()));
        m_DefenseText.text = _stringBuilder.ToString();

        m_BlessingCountText.text = bossData.m_BossRoleDB.m_BlessNum.ToString();

        m_ResouseText1.text = m_WorldBossManager.m_ShouWangZL.ToString();
        m_ResouseText2.text = m_ObjectSelf.Gold.ToString();  

        RefreshBlessingPanel(bossData);

    }
    //神圣祝福面板的数据刷新
    private void RefreshBlessingPanel(WorldBossData bossData)
    {
        //string _attString = GameUtils.StringWithColor(FloatToString(bossData.m_BossRoleDB.m_BlessNum * m_Config.getLegend_wish_damage_up()), TEXT_COLOR.RED);
        //string _defString = GameUtils.StringWithColor(FloatToString(bossData.m_BossRoleDB.m_BlessNum * m_Config.getLegend_wish_hurt_down()), TEXT_COLOR.RED);

        m_BlessingTopTipsText.text = string.Format(GameUtils.getString("legend_of_the_war_content9"),
                                FloatToString(m_Config.getLegend_wish_damage_up()), 
                                FloatToString(m_Config.getLegend_wish_hurt_down()));
        m_BlessingPanelAttackText.text = GameUtils.getString("legend_of_the_war_content11");//string.Format(GameUtils.getString("legend_of_the_war_content11"), _attString);
        m_BlessingPanelDefenseText.text = GameUtils.getString("legend_of_the_war_content12");//string.Format(GameUtils.getString("legend_of_the_war_content12"), _defString);
        m_BlessingPanelAttackPercentText.text = FloatToString(bossData.m_BossRoleDB.m_BlessNum * m_Config.getLegend_wish_damage_up());
        m_BlessingPanelDefensePercentText.text = FloatToString(bossData.m_BossRoleDB.m_BlessNum * m_Config.getLegend_wish_hurt_down());
        int[] _costArr = m_Config.getLegend_wish_cost();

        if (bossData.m_BossRoleDB.m_BlessNum < _costArr.Length)
        {
            m_PayForBlessingCostText.text = _costArr[bossData.m_BossRoleDB.m_BlessNum].ToString();
        }
        else
        {
            //m_PayForBlessingBtn
            GameUtils.SetBtnSpriteGrayState(m_PayForBlessingBtn, true);
            m_PayForBlessingCostText.gameObject.SetActive(false);
        }
        m_PayForBlessingCountText.text = string.Format(GameUtils.getString("legend_of_the_war_content14"), _costArr.Length - bossData.m_BossRoleDB.m_BlessNum);
        m_PlayerWalletText.text = m_WorldBossManager.m_ShouWangZL.ToString();
    }
    private void RefreshWarcherSoulPanel()
    {
        m_PayForSoulCostText.text = (m_Config.getLegend_watcher_soul_cost() * m_CurrentSoulCount).ToString();
        m_SoulCountText.text = m_CurrentSoulCount.ToString();
        m_PlayerDiamondText.text = m_ObjectSelf.Gold.ToString();
        GameUtils.SetBtnSpriteGrayState(m_SoulAddButton, !HaveEnoughResForSoul(m_CurrentSoulCount + 1));
        GameUtils.SetBtnSpriteGrayState(m_SoulSubButton, m_CurrentSoulCount <= 1);
    }
    private void RefreshBossHpBar()
    {
        var _data = GetCurrentActiveBoss();
        if (_data != null)
        {
            UpdateBossHpBar(_data);
        }
    }

    private void RefreshSoulTipsPanel()
    {
        StringBuilder _stringBuilder = new StringBuilder("×");
        _stringBuilder.Append(m_SoulNeedToBuy.ToString());
        m_SoulTipsText.text = string.Format(GameUtils.getString("legend_of_the_war_content35"), _stringBuilder.ToString());
        m_SoulTipsPayCount.text = (m_Config.getLegend_watcher_soul_cost() * m_SoulNeedToBuy).ToString();
        m_SoulTipsPlayerDiamondText.text = m_ObjectSelf.Gold.ToString();
    }

    private void UpdateBossHpBar(WorldBossData bossData)
    {
        m_BossHPBar.value = ((float)bossData.m_BossRoleDB.m_CurBossHp) / bossData.m_BossRoleDB.m_BossMaxHp;
        RefreshBossHpFillImage();
    }


    //LegendTemplePanel
    private void LoadBloodPanel()
    {
        m_BloodPanelLayout.anchoredPosition = m_BPLayoutBackup;
        m_ResourceCountText.text = m_WorldBossManager.m_ChuanShuoZS.ToString();
    }
    /// <summary>
    /// 刷新LegendTemplePanel下的ItemPanel面板数据
    /// </summary>
    /// <param name="isHunterMarket">true刷新猎人集市，否则刷新为稀世符文</param>
    private void LoadItemPanel(bool isHunterMarket)
    {
        m_ResourceCountText.text = m_WorldBossManager.m_ChuanShuoZS.ToString();
        m_ItemPanelLayout.anchoredPosition = m_IPLayoutBackup;
        SortedList<int, LegendexchargeTemplate> _list = null;
        if (isHunterMarket)
        {
            //刷新为猎人集市
            m_ItemBottomTipsText.text = GameUtils.getString("legend_of_the_war_content20");
            int _maxExchangeCount = m_Config.getLegend_excharge_max_num();
            m_ItemExchangeCountText.text = string.Format(GameUtils.getString("legend_of_the_war_content2"),
                        string.Format(" <color=#FEDC1F>{0}</color>",
                        (m_Config.getLegend_excharge_max_num() - m_WorldBossManager.m_ShopExchangeNum)));

            _list = m_HunterItemDataList;
            InitShopLayout(m_ItemPanelLayout, m_HunterItemDataList.Count > 3);
        }
        else
        {
            //刷新为稀世符文
            m_ItemBottomTipsText.text = GameUtils.getString("legend_of_the_war_content19");

            _list = m_RuneItemDataList;
            InitShopLayout(m_ItemPanelLayout, m_RuneItemDataList.Count > 3);
        }

        int idx = 0;
        
        m_IPScrollbar.SetActive(_list.Count>3);
        foreach (var data in _list.Values)
        {
            var _item = m_ShopItemList[idx];
            _item.SetItemInfo(data, SBuyBossShopHandler);
            _item.m_SelfObject.SetActive(true);
            idx++;
        }
        if (idx < m_ShopItemList.Count)
        {
            for (int i = idx; i < m_ShopItemList.Count; i++)
            {
                m_ShopItemList[i].m_SelfObject.SetActive(false);
            }
        }
    }

    //private void LoadToggleImage(Image toggleImage,bool isOn)
    //{
    //    if (isOn)
    //        toggleImage.sprite = m_ToggleImage;
    //    else
    //        toggleImage.sprite = m_DisableToggleImage;
    //}

    private void RefreshToggle(ActiveToggle activeToggle,bool isOn)
    {
        switch (activeToggle)
        {
            case ActiveToggle.Blood:
                if (isOn)
                {
                    m_BloodToggleImage.sprite = m_ToggleImage;
                    m_BloodToggleImage2.sprite = m_BloodMark2;
                    m_BloodToggleTrans.sizeDelta = ActiveToggleSize;
                    m_BloodText.color = Color.white;
                    m_BloodText.font = m_Font;
                }
                else
                {
                    m_BloodToggleImage.sprite = m_DisableToggleImage;
                    m_BloodToggleImage2.sprite = m_BloodMark2Disable;
                    m_BloodToggleTrans.sizeDelta = DisableToggleSize;
                    m_BloodText.color = DisableColor;
                    m_BloodText.font = m_Font2;
                }
                break;
            case ActiveToggle.Rune:
                if (isOn)
                {
                    m_RuneToggleImage.sprite = m_ToggleImage;
                    m_RuneToggleImage2.sprite = m_RuneMark2;
                    m_RuneToggleTrans.sizeDelta = ActiveToggleSize;
                    m_RuneText.color = Color.white;
                    m_RuneText.font = m_Font;
                }
                else
                {
                    m_RuneToggleImage.sprite = m_DisableToggleImage;
                    m_RuneToggleImage2.sprite = m_RuneMark2Disable;
                    m_RuneToggleTrans.sizeDelta = DisableToggleSize;
                    m_RuneText.color = DisableColor;
                    m_RuneText.font = m_Font2;
                }
                break;
            case ActiveToggle.Hunter:
                if (isOn)
                {
                    m_HunterToggleImage.sprite = m_ToggleImage;
                    m_HunterToggleImage2.sprite = m_HunterMark2;
                    m_HunterToggleTrans.sizeDelta = ActiveToggleSize;
                    m_HunterText.color = Color.white;
                    m_HunterText.font = m_Font;
                }
                else
                {
                    m_HunterToggleImage.sprite = m_DisableToggleImage;
                    m_HunterToggleImage2.sprite = m_HunterMark2Disable;
                    m_HunterToggleTrans.sizeDelta = DisableToggleSize;
                    m_HunterText.color = DisableColor;
                    m_HunterText.font = m_Font2;
                }
                break;
        }
    }


    /************callback******************/
    private void OnBloodToggleChange(bool value)
    {
        m_BloodPanel.SetActive(value);
        m_ItemPanel.SetActive(!value);
        RefreshToggle(ActiveToggle.Blood, value);
     }
    private void OnRuneToggleChange(bool value)
    {
        RefreshToggle(ActiveToggle.Rune, value);
        m_ItemExchangeCountText.gameObject.SetActive(!value);
        if (value && (m_RuneToggleLastState == false))
        {
            LoadItemPanel(false);
        }
        m_RuneToggleLastState = value;
    }
    private void OnHunterToggleChange(bool value)
    {
        RefreshToggle(ActiveToggle.Hunter, value);
        m_ItemExchangeCountText.gameObject.SetActive(value);
        if (value && (m_HunterToggleLastState == false) )
        {
            LoadItemPanel(true);
        }
        m_HunterToggleLastState = value;
    }


    protected override void OnClickAwardCheckBtn()
    {
        ChangePanel(ActiveUIPanel.AwardCheckPanel);
    }
    protected override void OnClickLegendTempleBtn()
    {
        if (!m_IsInitLegendTemplePanel)
            InitLegendTemplePanel();
        else
            ResetToggleState();
        ChangePanel(ActiveUIPanel.BloodPanel);
    }
    protected override void OnClickChallengeBtn()
    {
        var _boosData = GetCurrentActiveBoss();
        if (_boosData != null)
        {
            if (_boosData.m_IsKilled == 1)
            {
                m_MessageBox.AddMsgBox(
                    string.Format(GameUtils.getString("legend_of_the_war_content4"), _boosData.m_KillName),
                    transform);
            }
            else
            {
                CGetMyWordBoss _myWordBoss = new CGetMyWordBoss();
                _myWordBoss.bossid = m_CurrentBossID;
                IOControler.GetInstance().SendProtocol(_myWordBoss);
                GameEventDispatcher.Inst.addEventListener(GameEventID.G_GetMyWorldBoss, EnableFightPanel);
            }
        }
        else
        {
            BossOverFlag _flag = EventOver();
            if((_flag & BossOverFlag.ONE) != BossOverFlag.ONE)
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("legend_of_the_war_bubble11"), selfTransform);
            else if ((_flag & BossOverFlag.TWO) != BossOverFlag.TWO)
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("legend_of_the_war_bubble11"), selfTransform);
            else if ((_flag & BossOverFlag.THREE) != BossOverFlag.THREE)                            
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("legend_of_the_war_bubble11"), selfTransform);
            else if ((_flag & BossOverFlag.FORE) != BossOverFlag.FORE)                              
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("legend_of_the_war_bubble11"), selfTransform);
            else
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("legend_of_the_war_bubble12"), selfTransform);
            
        }
       
    }
    protected void EnableFightPanel()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_GetMyWorldBoss, EnableFightPanel);
        if (CurrentBossIsWatcher())
        {
            OnClickFightBtn();
        }
        else
        {
            ChangePanel(ActiveUIPanel.FightPanel);
        }
    }
    protected override void OnClickFightBackBtn()
    {
        ChangePanel(ActiveUIPanel.SelectBossPanel);
    }
    protected override void OnClickLegendTempleCloseBtn()
    {
        ChangePanel(ActiveUIPanel.SelectBossPanel);
    }
    protected override void OnClickBlessingBtn()
    {
        var _bossData = GetCurrentActiveBoss();
        if (_bossData == null)
            return;

        RefreshBlessingPanel(_bossData);
        m_BlessingPanel.SetActive(true);
    }
    protected override void OnClickBlessingCloseBtn()
    {
        m_BlessingPanel.SetActive(false);
    }
    protected override void OnClickAwardCloseBtn()
    {
        ChangePanel(ActiveUIPanel.SelectBossPanel);
    }

    protected override void OnClickSelectBossBackBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(Path);
    }

    protected override void OnClickFightBtn()
    {
        var _boosData = GetCurrentActiveBoss();
        if (_boosData == null)
            return;
        if (!IsEnoughHero())
        {
            m_MessageBox.AddMsgBox(GameUtils.getString("fight_fightprepare_tip1"), transform);
            return;
        }
        if (_boosData.m_BossRoleDB.m_BattleCoolDown <= 0) //CD已经结束
        {
            if (_boosData.m_IsKilled == 1)
            {
                m_MessageBox.AddMsgBox(
                    string.Format(GameUtils.getString("legend_of_the_war_content4"), _boosData.m_KillName), 
                    transform);
            }
            else
            {
                CBeginBoss _beginBoss = new CBeginBoss();
                _beginBoss.bossid = _boosData.m_BossType;
                _beginBoss.iscost = 0;  //免费进入
                _beginBoss.troopid = (short)m_ObjectSelf.Teams.GetDefaultGroup();
                IOControler.GetInstance().SendProtocol(_beginBoss);
            }
        }
        else
        {
            if (m_MessageBox != null)
            {
                m_MessageBox.AddMsgBox(GameUtils.getString("legend_of_the_war_bubble5"), transform);
            }
        }
    }
    protected override void OnClickPayFightBtn()
    {
        var _boosData = GetCurrentActiveBoss();
        if (_boosData == null)
            return;


        if (!IsEnoughHero())
        {
            return;
        }
        if (_boosData.m_BossRoleDB.m_BattleCoolDown <= 0) //CD已经结束
        {
            return;
        }
        if (CurrentBossIsWatcher())   //进入BOSS守门人
        {
            return;
        }

        if (m_ObjectSelf.Gold<m_Config.getLegend_fight_cd_cost())  //没有足够的魔钻
        {
            if (m_MessageBox != null)
            {
                m_MessageBox.AddMsgBox(GameUtils.getString("common_diamondenough_content"), transform);
            }
            return;
        }

        CBeginBoss _beginBoss = new CBeginBoss();
        _beginBoss.bossid = m_CurrentBossID;
        _beginBoss.iscost = 1;  //  付费进入接口
        _beginBoss.troopid = (short)m_ObjectSelf.Teams.GetDefaultGroup();
        IOControler.GetInstance().SendProtocol(_beginBoss);
    }
    protected override void OnClickPayForBlessingBtn()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyWatcherSoul, OnClickPayForBlessingBtn);
        var _bossData = GetCurrentActiveBoss();
        if (_bossData == null)//世界BOSS已关闭
        {
            m_MessageBox.AddMsgBox(GameUtils.getString("legend_of_the_war_bubble3"), transform);
            return;
        }

        int[] _costArr = m_Config.getLegend_wish_cost();

        if (_bossData.m_BossRoleDB.m_BlessNum < _costArr.Length)    //尚未达到最大祝福数
        {
            m_SoulNeedToBuy = _costArr[_bossData.m_BossRoleDB.m_BlessNum] - m_WorldBossManager.m_ShouWangZL;
            if (m_SoulNeedToBuy <= 0)//有足够货币购买(守望之灵)
            {
                CBossBuyZhufu _protocol = new CBossBuyZhufu();
                GetCurrentActiveBoss();
                _protocol.bossid = m_CurrentBossID;
                IOControler.GetInstance().SendProtocol(_protocol);
                GameEventDispatcher.Inst.addEventListener(GameEventID.G_SBuyBossBlessing, SBossBuyBlessingHandler);
            }
            else
            {
//                m_MessageBox.AddMsgBox("需求更改，面板未做", transform);
                //m_CurrentSoulCount = 1;
                //RefreshWarcherSoulPanel();
                //m_WatcherSoulPanel.SetActive(true);
                RefreshSoulTipsPanel();
                m_SoulTipsPanel.SetActive(true);
            }
        }
        else        //已到达最大祝福数
        {
//            m_MessageBox.AddMsgBox("已到达祝福上限(05)", transform);
        }
    }


    //WatcherSoulPanel
    protected override void OnClickSoulCloseBtn()
    {
        m_WatcherSoulPanel.SetActive(false);
    }

    //--------------------->>
    protected override void OnClickAddResouseBtn1()
    {
        m_CurrentSoulCount = 1;
        RefreshWarcherSoulPanel();
        m_WatcherSoulPanel.SetActive(true);
    }
    protected override void OnClickAddResouseBtn2()
    {
        UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
    }
    protected override void OnClickSoulAddButton()
    {

        if (!m_PressedAddBtn || !m_IsPressingAddBtn)
        {
            if (HaveEnoughResForSoul(m_CurrentSoulCount + 1))
            {
                m_CurrentSoulCount++;
                RefreshWarcherSoulPanel();
            }
        }
        m_PressedAddBtn = false;
        m_IsPressingAddBtn = false;

    }
    protected override void OnClickSoulSubButton()
    {
        if (!m_PressedSubBtn || !m_IsPressingSubBtn)
        {
            if (m_CurrentSoulCount > 1)
            {
                m_CurrentSoulCount--;
                RefreshWarcherSoulPanel();
            }
        }
        m_PressedSubBtn = false;
        m_IsPressingSubBtn = false;
    }

    protected void EnterPressSoulAddButton(GameObject go)
    {
        m_PressedAddBtn = true;
    }

    protected void OnPressSoulAddButton(GameObject go)
    {
        if (m_PressedAddBtn)
        {
            m_IsPressingAddBtn = true;

            if (HaveEnoughResForSoul(m_CurrentSoulCount + 1))
            {
                m_CurrentSoulCount++;
                RefreshWarcherSoulPanel();
                m_SoulAddBtnListener.pressInterval = Mathf.MoveTowards(m_SoulAddBtnListener.pressInterval,
                    MinPressInterval, MaxDelta * Time.deltaTime);
 //               Debug.Log((MaxDelta).ToString());
            }
        }
    }
    protected void EnterPressSoulSubButton(GameObject go)
    {
        m_PressedSubBtn = true;
    }
    protected void OnPressSoulSubButton(GameObject go)
    {
        if (m_PressedSubBtn)
        {
            m_IsPressingSubBtn = true;

            if (m_CurrentSoulCount > 1)
            {
                m_CurrentSoulCount--;
                RefreshWarcherSoulPanel();
                m_SoulSubBtnListener.pressInterval = Mathf.MoveTowards(m_SoulSubBtnListener.pressInterval,
                    MinPressInterval, MaxDelta * Time.deltaTime);
            }
        }
    }

    protected override void OnClickPayForSoulBtn()
    {
        CBuyShouwangzl _protocol = new CBuyShouwangzl();
        _protocol.num = m_CurrentSoulCount;
        IOControler.GetInstance().SendProtocol(_protocol);
        m_WatcherSoulPanel.SetActive(false);
    }
    protected override void OnClickBlessingTipsBtn()
    {
        m_BlessingTipsPanel.SetActive(!m_BlessingTipsPanel.activeSelf);
    }

    protected override void OnClickSoulTipsCancelBtn()
    {
        m_SoulTipsPanel.SetActive(false);
    }

    protected override void OnClickSoulTipsPayBtn()
    {
        if (HaveEnoughResForSoul(m_SoulNeedToBuy))
        {
            CBuyShouwangzl _protocol = new CBuyShouwangzl();
            _protocol.num = m_SoulNeedToBuy;
            IOControler.GetInstance().SendProtocol(_protocol);
            m_SoulTipsPanel.SetActive(false);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_SBuyWatcherSoul, OnClickPayForBlessingBtn);
            
        }
        else
        {
            m_MessageBox.AddMsgBox(GameUtils.getString("common_diamondenough_content"));
        }
    }

    //SBuyBossShop的处理回调
    protected void SBuyBossShopHandler(GameEvent eventData)
    {
        bool _result = ((WorldBossCallbackParaPackage)eventData.data).m_Result == SBuyBossShop.END_OK;
        if (_result)
        {
            m_MessageBox.AddMsgBox(GameUtils.getString("legend_of_the_war_bubble13"), transform);
            m_ItemExchangeCountText.text = string.Format(GameUtils.getString("legend_of_the_war_content2"),
                                    string.Format(" <color=#FEDC1F>{0}</color>",
                                    (m_Config.getLegend_excharge_max_num() - m_WorldBossManager.m_ShopExchangeNum))
                                    );//刷新兑换次数
            m_ResourceCountText.text = m_WorldBossManager.m_ChuanShuoZS.ToString(); //刷新货币

        }
        else
        {
            m_MessageBox.AddMsgBox(GameUtils.getString("legend_of_the_war_bubble6"), transform);
        }

    }

    //SBossBuyZhufu的回调
    protected void SBossBuyBlessingHandler(GameEvent eventData)
    {
        WorldBossCallbackParaPackage _pack = (WorldBossCallbackParaPackage)eventData.data;
        bool _result = _pack.m_Result == SBossBuyZhufu.END_OK;
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SBuyBossBlessing, SBossBuyBlessingHandler);
        if (_result)
        {
            m_MessageBox.AddMsgBox(GameUtils.getString("legend_of_the_war_bubble13"), transform);
//            var _bossData = GetCurrentActiveBoss();
            var _bossData = m_WorldBossManager.m_BossDataMap[_pack.m_BossID];
            if (_bossData != null)
            {
                RefreshFightPanel(_bossData);
            }
        }
        else
        {
            m_MessageBox.AddMsgBox(GameUtils.getString("legend_of_the_war_bubble6"), transform);
        }

    }

    //SBuyShouwangzl的回调
    protected void SBuyWatcherSoulHandler()
    {
        var _bossData = GetCurrentActiveBoss();
        if(_bossData != null)
            RefreshFightPanel(_bossData);
    }

    protected void SGetBossRankHandler()
    {
        int _count = 0;
        for (int i = 0; i < m_WorldBossManager.m_RankingList.Count; i++)
        {
            LoadRankingData(i, m_WorldBossManager.m_RankingList[i]);
            if (m_WorldBossManager.m_MyRanking < 1 || m_WorldBossManager.m_MyRanking > 10 || m_WorldBossManager.m_MyRanking > i + 1)
                m_RankingList[i].SetRankingColor(Color.white);
            else
                m_RankingList[i].SetRankingColor(RankingYellow);
            m_RankingList[i].EnableSelf(true);
            _count++;
        }
        for (int i = _count; i < m_RankingList.Count; i++)
        {
            m_RankingList[i].EnableSelf(false);
        }
        m_MyTotalDamage.text = m_WorldBossManager.m_MyTotalDamage.ToString();
        
        m_MyRank.gameObject.SetActive(m_WorldBossManager.m_MyRanking > 0);
        if (m_WorldBossManager.m_MyRanking > 0)
        {
            m_MyRank.text = m_WorldBossManager.m_MyRanking.ToString();
        }
        MoveSelfMask(m_WorldBossManager.m_MyRanking);
    }

    //当各收到服务器GetWorldBoss数据
    protected void OnGetWorldBossData()
    {
        RefreshBossList();
    }

    /***************周期处理**********************/
    private void OneSecHandler()
    {
//        int _lastBossID = m_CurrentBossID;
        var _boosData = GetCurrentActiveBoss();

        if (m_CurrentPanel == ActiveUIPanel.SelectBossPanel)
        {
            //if (_lastBossID != m_CurrentBossID)
            //{

            //}

            if (m_CurrentBossID > 0 )
            {
                m_BossList[m_CurrentBossID-1].RefreshTimeText();
            }

            if (CurrentBossIsWatcher()&&_boosData.m_BossRoleDB.m_BattleCoolDown > 0)
            {
                m_ChallengeBtnCDText.text = string.Format("CD {0}", SecondToString(_boosData.m_BossRoleDB.m_BattleCoolDown));
                if (!m_ChallengeBtnCDText.gameObject.activeSelf)
                {
                    m_ChallengeBtnCDText.gameObject.SetActive(true);
                }
            }
            else
            {
                if (m_ChallengeBtnCDText.gameObject.activeSelf)
                {
                    m_ChallengeBtnCDText.gameObject.SetActive(false);
                    //GameEventDispatcher.Inst.addEventListener(GameEventID.G_GetWorldBoss, TimeUpHandler);
                    //IOControler.GetInstance().SendProtocol(new CGetWordBoss());
                }
            }
        }
        else if (m_CurrentPanel == ActiveUIPanel.FightPanel)
        {
            if (_boosData != null)
            {
                if (_boosData.m_TimeCount == 0)
                {
                    ChangePanel(ActiveUIPanel.SelectBossPanel);
                }
                if (_boosData.m_BossRoleDB.m_BattleCoolDown > 0)
                {
                    m_FightBtnCDText.text = string.Format("CD {0}", SecondToString(_boosData.m_BossRoleDB.m_BattleCoolDown));
                    if (!m_FightBtnCDText.gameObject.activeSelf)
                    {
                        m_FightBtnCDText.gameObject.SetActive(true);
                        m_FightBtnBg.SetActive(true);
                    }
                }
                else
                {
                    if (m_FightBtnCDText.gameObject.activeSelf)
                    {
                        m_FightBtnCDText.gameObject.SetActive(false);
                        m_FightBtnBg.SetActive(false);
                        //GameEventDispatcher.Inst.addEventListener(GameEventID.G_GetWorldBoss, TimeUpHandler);
                        //IOControler.GetInstance().SendProtocol(new CGetWordBoss());
                    }
                }

                if (CurrentBossIsWatcher())
                {
                    SetGrayPayFightBtn(true); 
                }
                else
                {
                    SetGrayPayFightBtn(_boosData.m_BossRoleDB.m_BattleCoolDown <= 0); 
                }
                StringBuilder _TimeString = new StringBuilder(GameUtils.getString("legend_of_the_war_content25"));
                _TimeString.Append(SecondToString(_boosData.m_TimeCount));
                m_CountDownText.text = _TimeString.ToString();

            }
        }


    }
    private void ThreeSecHandler()
    {
        IOControler.GetInstance().SendProtocol(new CGetBossRank());
        if (m_CurrentPanel == ActiveUIPanel.FightPanel)
        {
            if (m_CurrentBossID > 0)
            {
                CGetMyWordBoss _myWordBoss = new CGetMyWordBoss();
                _myWordBoss.bossid = m_CurrentBossID;
                IOControler.GetInstance().SendProtocol(_myWordBoss);
            }
        }
    }

    /************************************/

    private void ResetToggleState()
    {
        m_BloodToggle.isOn = true;
        m_RuneToggle.isOn = false;
        m_HunterToggle.isOn = false;
        m_RuneToggleLastState = false;
        m_HunterToggleLastState = false;

        //LoadToggleImage(m_BloodToggleImage, m_BloodToggle.isOn);
        //LoadToggleImage(m_RuneToggleImage, m_RuneToggleLastState);
        //LoadToggleImage(m_HunterToggleImage, m_HunterToggleLastState);
        RefreshToggle(ActiveToggle.Blood, m_BloodToggle.isOn);
        RefreshToggle(ActiveToggle.Rune, m_RuneToggleLastState);
        RefreshToggle(ActiveToggle.Hunter, m_HunterToggleLastState);
    }
    /// <summary>
    /// rank=0代表第一名
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="data"></param>
    private void LoadRankingData(int rank,BossRankInfo data)
    {
        m_RankingList[rank].SetRankingInfo(rank + 1,data.rolename, data.num);
    }
    private void SetGrayPayFightBtn(bool value)
    {
        m_CostText.gameObject.SetActive(!value);
        m_FightBtnBg.SetActive(!value);
        GameUtils.SetBtnSpriteGrayState(m_PayFightBtn,value);
    }
    private bool CurrentBossIsWatcher()
    {
        return m_CurrentBossID == 1 || m_CurrentBossID == 3;
    }
    private WorldBossData GetCurrentActiveBoss()
    {
        m_CurrentBossID = -1;
        WorldBossData _data = null;
        foreach (var data in m_WorldBossManager.m_BossDataMap.Values)
        {
            if (data.m_IsOpen > 0)//已开启
            {
                m_CurrentBossID = data.m_BossType;
                _data = m_WorldBossManager.m_BossDataMap[m_CurrentBossID];
                break;
            }
        }
        return _data;
    }

    //0代表排名无效
    private void MoveSelfMask(int myRanking)
    {
        if (myRanking < 1 || myRanking > m_RankingList.Count)   //0代表排名无效，大于m_RankingList.Count代表10名开外
        {
            m_SelfMask.gameObject.SetActive(false);
        }
        else
        {
            m_RankingList[myRanking - 1].ReceiveChild(m_SelfMask);
            m_SelfMask.gameObject.SetActive(true);
        }
    }

    //
    private void RefreshBossHpFillImage()
    {
        float _hpFactor = m_BossHPBar.value;
        if(_hpFactor>0.5f)
        {
            m_HpImage.sprite = m_HpFillGreen;
        }
        else if(_hpFactor<0.25f)
        {
            m_HpImage.sprite = m_HpFillRed;
        }
        else
        {
            m_HpImage.sprite = m_HpFillYellow;
        }
    }

    //config表中的时间字符串解析 18：00-18：10转换成秒
    private void StringToSecond(string timeString,out int start,out int end)
    {
        var _tempString = timeString.Split('-');
        string[] _resultString = _tempString[0].Split(':');
        start = int.Parse(_resultString[0]) * 3600 + int.Parse(_resultString[1]) * 60;
        _resultString = _tempString[1].Split(':');
        end = int.Parse(_resultString[0]) * 3600 + int.Parse(_resultString[1]) * 60;
    }
    private static string SecondToString(int second)
    {
        int _second = second % 60;
        if (_second<10)
            return string.Format("{0}:0{1}", second / 60, _second);
        else
            return string.Format("{0}:{1}", second / 60, _second);
    }
    private string FloatToString(float value)
    {
        int temp = (int)(value*100);
        return string.Format(@"{0}%", temp);
    }
    private int GetSortKey(LegendexchargeTemplate data)
    { 
        return data.getSort() << 14 + data.getId();
    }

    private bool HaveEnoughResForSoul(int count)
    {
        return m_ObjectSelf.Gold >= m_Config.getLegend_watcher_soul_cost() * count;
    }

    private bool IsGuard(WorldBossData boss)
    {
        return boss.m_BossType == 1 || boss.m_BossType == 3;
    }
    //队伍中英雄数量判断
    private bool IsEnoughHero()
    {
        bool result = false;
        int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
        for (int i = 0; i < HeroCount; ++i)
        {
            ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);
            if (temp == null)
                continue;
            else
            {
                result = true;
                break;
            }
        }

        return result;
    }

    private bool IsAllEventOver()
    {

        DateTime _date = m_ObjectSelf.ServerDateTime;
        int _cur = _date.Hour * 3600 + _date.Minute * 60 + _date.Second;
        int _start = 0;
        int _end = 0;
        StringToSecond(m_Config.getLegend_fight_open_time()[1], out _start, out _end);
        return TimeUtils.TimeDurationCompare(_cur, _start, _end) > 0;
    
    }
    private BossOverFlag EventOver()
    {
        BossOverFlag result = BossOverFlag.NONE;

        DateTime _date = m_ObjectSelf.ServerDateTime;
        int _cur = _date.Hour * 3600 + _date.Minute * 60 + _date.Second;
        int _start = 0;
        int _end = 0;

        StringToSecond(m_Config.getLegend_watcher_open_time()[0], out _start, out _end);
        if(TimeUtils.TimeDurationCompare(_cur, _start, _end) > 0)
        {
            result |= BossOverFlag.ONE;
        }
        StringToSecond(m_Config.getLegend_fight_open_time()[0], out _start, out _end);
        if (TimeUtils.TimeDurationCompare(_cur, _start, _end) > 0)
        {
            result |= BossOverFlag.TWO;
        }
        StringToSecond(m_Config.getLegend_watcher_open_time()[1], out _start, out _end);
        if (TimeUtils.TimeDurationCompare(_cur, _start, _end) > 0)
        {
            result |= BossOverFlag.THREE;
        }
        StringToSecond(m_Config.getLegend_fight_open_time()[1], out _start, out _end);
        if (TimeUtils.TimeDurationCompare(_cur, _start, _end) > 0)
        {
            result |= BossOverFlag.FORE;
        }
        return result;
    }

}
