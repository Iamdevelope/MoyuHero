using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using DG.Tweening;
using DreamFaction.UI.Core;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using GNET;
using DreamFaction.LogSystem;

public class UI_CaptionManager : BaseUI
{
    #region InnerClass
    /// <summary>
    /// 跑马灯模块所有动态生成的游戏物体的基类
    /// </summary>
    private abstract class ChainItem : DynamicItem
    {
        public RectTransform m_SelfTrans;

        protected abstract void Resize();
        public abstract void ReadyToRun(Transform captionPanel, Transform clickPanel);

        public float ComponentLength
        {
            get { return m_SelfTrans.rect.width; }
        }
        public virtual void Recycle(Transform layout)
        {
            m_SelfTrans.gameObject.SetActive(false);
            Replace(layout);
        }

        protected void Replace(Transform layout)
        {
            m_SelfTrans.SetParent(layout);
            m_SelfTrans.anchoredPosition = Vector2.zero;
        }

        protected void Init(Transform layout)
        {
            m_SelfTrans.gameObject.SetActive(true);
            Replace(layout);
            Resize();
        }
    }
    /// <summary>
    /// 跑马灯英雄/符文星级 预制件的控制逻辑
    /// </summary>
    private class StarItem : ChainItem
    {
        public GameObject[] m_StarArr;
        public GridLayoutGroup m_GridLayout;
        private float m_Width;
        protected StarItem() 
        {
            m_StarArr = new GameObject[5];
        }
        public static StarItem GenerateItem(GameObject originalItem, Transform layout)
        {
            StarItem _item = null;
            GameObject _go = InstantiateObject(originalItem, layout);
            if (_go != null)
            {
                _item = new StarItem();
                _item.m_StarArr[0] = _go.transform.FindChild("Layout/Star1").gameObject;
                _item.m_StarArr[1] = _go.transform.FindChild("Layout/Star2").gameObject;
                _item.m_StarArr[2] = _go.transform.FindChild("Layout/Star3").gameObject;
                _item.m_StarArr[3] = _go.transform.FindChild("Layout/Star4").gameObject;
                _item.m_StarArr[4] = _go.transform.FindChild("Layout/Star5").gameObject;
                _item.m_SelfTrans = _go.GetComponent<RectTransform>();
                _item.m_SelfTrans.anchoredPosition = Vector2.zero;

                _go.SetActive(false);
            }
            return _item;
        }


        public void SetInfo(int starLv, Transform layout)
        {
            m_Width = 0;
            for (int i = 0; i < m_StarArr.Length; i++)
            {
                m_StarArr[i].SetActive(i < starLv);
                if(i < starLv)
                {
                    m_Width += m_StarArr[i].GetComponent<RectTransform>().rect.width;
                }
            }
            Init(layout);
        }
        protected override void Resize()
        {
            var _vector = m_SelfTrans.sizeDelta;
            _vector.x = m_Width;
//            _vector.y = m_SelfTrans.parent.GetComponent<RectTransform>().rect.height;
            m_SelfTrans.sizeDelta = _vector;
        }

        public override void ReadyToRun(Transform captionPanel, Transform clickPanel)
        {
 	        m_SelfTrans.SetParent(captionPanel);
        }
    }
    /// <summary>
    /// 不可点击的普通文字 预制件的控制逻辑
    /// </summary>
    private class CaptionText : ChainItem
    {
        protected ContentSizeFitter m_ContentSizeFitter;
        public Text m_CaptionText;
        protected CaptionText() { }
        public static CaptionText GenerateItem(GameObject originalText, Transform layout)
        {
            CaptionText _item = null;
            GameObject _go = InstantiateObject(originalText, layout);
            if(_go != null)
            {
                _item = new CaptionText();
                _item.m_CaptionText = _go.GetComponent<Text>();
                _item.m_SelfTrans = _go.GetComponent<RectTransform>();
                _item.m_SelfTrans.anchoredPosition = Vector2.zero;
                _item.m_ContentSizeFitter = _go.GetComponent<ContentSizeFitter>();
                _go.SetActive(false);
            }
            return _item;
        }
        public virtual void SetInfo(string content,Transform layout,Color color)
        {
            m_CaptionText.text = content;
            m_CaptionText.color = color;
            Init(layout);
        }

        protected override void Resize()
        {
            m_ContentSizeFitter.SetLayoutHorizontal();
            m_ContentSizeFitter.SetLayoutVertical();
        }

        public override void ReadyToRun(Transform captionPanel, Transform clickPanel)
        {
 	        m_SelfTrans.SetParent(captionPanel);
        }

        public override void Recycle(Transform layout)
        {
            m_CaptionText.text = null;
            base.Recycle(layout);
        }
    }
    /// <summary>
    /// 可点击文字预制件的控制逻辑
    /// </summary>
    private class ClickableText : CaptionText
    {
        protected Image m_UnderlineImage;
        protected int m_ItemId;
        protected Button m_ClickableAera;
        protected Action m_OnClickCallback;
        private ClickableText() { }
        public static ClickableText GenerateItem(GameObject originalText, Transform layout)
        {
            ClickableText _item = null;
            GameObject _go = InstantiateObject(originalText, layout);
            if (_go != null)
            {
                _item = new ClickableText();
                _item.m_UnderlineImage = _go.transform.FindChild("UnderlineImage").GetComponent<Image>();
                _item.m_CaptionText = _go.GetComponent<Text>();
                _item.m_SelfTrans = _go.GetComponent<RectTransform>();
                _item.m_SelfTrans.anchoredPosition = Vector2.zero;
                _item.m_ContentSizeFitter = _go.GetComponent<ContentSizeFitter>();
                _item.m_ClickableAera = _go.GetComponent<Button>();
                _item.m_ClickableAera.onClick.AddListener(_item.OnAeraClick);
                _go.SetActive(false);
            }
            return _item;
        }
        public override void SetInfo(string content, Transform layout, Color color)
        {
            m_UnderlineImage.color = color;
            m_ItemId = int.Parse(content);
            if (GameUtils.GetObjectClassById(m_ItemId) == EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO)
                DreamFaction.GameCore.AssetLoader.Inst.DynamicLoadHeroCardRes(m_ItemId);
            base.SetInfo(GetItemName(m_ItemId), layout, color);
        }

        public void SetClickCallback(Action clickCallback)
        {
            m_OnClickCallback = clickCallback;
        }

        public override void Recycle(Transform layout)
        {
            m_OnClickCallback = null;
            base.Recycle(layout);
        }
        protected void OnAeraClick()
        {
            if (m_OnClickCallback != null)
                m_OnClickCallback();

            ShopModule.ShowItemPreviewUIHandler(m_ItemId);
        }
        public override void ReadyToRun(Transform captionPanel, Transform clickPanel)
        {
 	        m_SelfTrans.SetParent(clickPanel);
        }
    }

    /// <summary>
    /// 所有已经实例化的游戏物体的对象池
    /// </summary>
    private class ChainItemPool
    {
        private class ItemPack
        {
            public ChainItem m_Item;
            public bool isUseing;

            public ItemPack(ChainItem item)
            {
                m_Item = item;
                isUseing = false;
            }
        }
        private GameObject m_OriginalStarItem;
        private GameObject m_OriginalCaptionText;
        private GameObject m_OriginalClickableText;
        private Transform m_PoolLayout;

        //由于池子不大，所以没有使用字典，而是用遍历查找
        private List<ItemPack> m_StarItemBuffer;
        private List<ItemPack> m_CaptionTextBuffer;
        private List<ItemPack> m_ClickableTextBuffer;

        public ChainItemPool(int initialSize, GameObject originalStarItem, GameObject originalCaptionText, GameObject originalClickableText, Transform poolLayout)
        {
            if (originalStarItem == null || originalCaptionText == null || originalClickableText == null || poolLayout == null || initialSize < 0)
            {
                throw new Exception("参数不能为null或者小于0");
            }
            m_OriginalStarItem = originalStarItem;
            m_OriginalCaptionText = originalCaptionText;
            m_OriginalClickableText = originalClickableText;
            m_PoolLayout = poolLayout;
            m_StarItemBuffer = new List<ItemPack>();
            m_CaptionTextBuffer = new List<ItemPack>(initialSize);
            m_ClickableTextBuffer = new List<ItemPack>();
            for (int i = 0; i < initialSize; i++)
            {
                m_CaptionTextBuffer.Add(new ItemPack(CaptionText.GenerateItem(m_OriginalCaptionText, m_PoolLayout)));
                
            }
            m_StarItemBuffer.Add(new ItemPack(StarItem.GenerateItem(m_OriginalStarItem, m_PoolLayout)));
            m_ClickableTextBuffer.Add(new ItemPack(ClickableText.GenerateItem(m_OriginalClickableText, m_PoolLayout)));
        }

        public StarItem GetStarItem()
        {
            ItemPack _pack = GetIdlePackage(m_StarItemBuffer);
            if (_pack == null)
            {
                _pack = new ItemPack(StarItem.GenerateItem(m_OriginalStarItem, m_PoolLayout));
                m_StarItemBuffer.Add(_pack);
            }

            _pack.isUseing = true;
            return (StarItem)_pack.m_Item;
        }
        public CaptionText GetCaptionText()
        {
            ItemPack _pack = GetIdlePackage(m_CaptionTextBuffer);
            if (_pack == null)
            {
                _pack = new ItemPack(CaptionText.GenerateItem(m_OriginalCaptionText, m_PoolLayout));
                m_CaptionTextBuffer.Add(_pack);
            }

            _pack.isUseing = true;
            return (CaptionText)_pack.m_Item;
        }
        public ClickableText GetClickableText()
        {
            ItemPack _pack = GetIdlePackage(m_ClickableTextBuffer);
            if (_pack == null)
            {
                _pack = new ItemPack(ClickableText.GenerateItem(m_OriginalClickableText, m_PoolLayout));
                m_ClickableTextBuffer.Add(_pack);
            }

            _pack.isUseing = true;
            return (ClickableText)_pack.m_Item;
        }

        public void Release(ChainItem item)
        {
            ItemPack _pack = null;
            if (item.GetType() == typeof(CaptionText))//ClickableText是CaptionText的子类，不能用is判断
            {
                _pack = FindPackage(item, m_CaptionTextBuffer);
            }
            else if (item is ClickableText)
            {
                _pack = FindPackage(item, m_ClickableTextBuffer);
            }
            else if (item is StarItem)
            {
                _pack = FindPackage(item, m_StarItemBuffer);
            }

            if (_pack == null)
                Debug.Log("正在释放未知的CaptionText");
            else
                RecyclePackage(_pack);
        }
        public void Release(CaptionText item)
        {
            ItemPack _pack;
            if (item is ClickableText)
            {
                _pack = FindPackage(item, m_ClickableTextBuffer);
            }
            else
            {
                _pack = FindPackage(item, m_CaptionTextBuffer);
            }

            if (_pack == null)
                Debug.Log("正在释放未知的CaptionText");
            else
                RecyclePackage(_pack);
        }
        public void Release(ClickableText item)
        {
            ItemPack _pack = FindPackage(item, m_ClickableTextBuffer);
            if (_pack == null)
                Debug.Log("正在释放未知的ClickableText");
            else
                RecyclePackage(_pack);
        }


        private void RecyclePackage(ItemPack package)
        {
            if (package == null)
                return;
            package.isUseing = false;
            package.m_Item.Recycle(m_PoolLayout);
        }

        private ItemPack GetIdlePackage(List<ItemPack> list)
        { 
            ItemPack _pack = null;
            for(int i = 0;i<list.Count;i++)
            {
                ItemPack _temp = list[i];
                if(_temp.isUseing == false)
                {
                    _pack = _temp;
                    break;
                }
            }
            return _pack;
        }
        private ItemPack FindPackage(ChainItem item, List<ItemPack> list)
        {
            ItemPack _pack = null;
            for (int i = 0; i < list.Count; i++)
            {
                ItemPack _temp = list[i];
                if (_temp.m_Item.Equals(item))
                {
                    _pack = _temp;
                    break;
                }
            }
            return _pack;
        }

    }

    /// <summary>
    /// 一条跑马灯文字的数据包，用于排序处理
    /// </summary>
    private class CaptionInfoPackage
    {
        public byte m_LifeTime;
        public RunhorselightTemplate m_InfoTable;
        public string[] m_ParaArr;
        public bool IsSelfInfo;

        public CaptionInfoPackage()
        {
            m_LifeTime = 60;
        }
        //需求瞎变动太烦了，针对7号跑马灯进行硬编码处理：隔壁老王成功的将VIP等级提升到了15级[玩家名#vip等级]
        //
        public int GetSortKey(UIMark uiMark)
        {
            int _key = -1;
            if (IsSelfInfo)
                _key = 0;
            else
            {
                switch (uiMark)
                {
                    case UIMark.DefaultMark:
                        _key = m_InfoTable.getSort1() * 100;
                        break;
                    case UIMark.HeroUpgrade:
                        _key = m_InfoTable.getSort2() * 100;
                        break;
                    case UIMark.HeroRecruit:
                        _key = m_InfoTable.getSort3() * 100;
                        break;
                    case UIMark.RelicTreasure:
                        _key = m_InfoTable.getSort4() * 100;
                        break;
                    case UIMark.Artifact:
                        _key = m_InfoTable.getSort5() * 100;
                        break;
                    case UIMark.PlayerBag:
                        _key = m_InfoTable.getSort6() * 100;
                        break;
                }
            }

            if (_key > 0)
            {
                _key += m_InfoTable.getDataclass()*100000;
            }
            //针对7号跑马灯进行特殊处理
            if (m_InfoTable.GetID() == 7)
            { 
                int _lv;
                if (int.TryParse(m_ParaArr[1], out _lv))
                {
                    _key += _lv;
                }
            }
            return _key;
        }
    }
    #endregion

    private readonly static string Path = "UI_Caption/UI_CaptionPanel_1_1";
    private static UI_CaptionManager Inst = null;

    private RectTransform m_SelfRectTrans;
    private bool isRunning = false;
    private string m_PlayerName;
    private UIMark m_CurrentUI;
    private ChainItemPool m_TextPool;
    private GameObject m_OriginalStarItem;
    private GameObject m_OriginalCaptionText;
    private GameObject m_OriginalClickableText;

    private RectTransform m_Background;
    private RectTransform m_TittleImage;
    private RectTransform m_TittleMask;
    private RectTransform m_LeftPoint;
    private RectTransform m_RightPoint;
    private Text m_TittleText;

    private RectTransform m_Caption;
    private Transform m_CaptionPanel;//不会遮挡用户操作的控件挂载位置
    private Transform m_ClickPanel;//需要响应点击的挂载位置

    private TableReader m_CaptionTable;
    private List<ChainItem> m_CaptionTextList = new List<ChainItem>();
    private List<CaptionInfoPackage> m_EventNotice = new List<CaptionInfoPackage>();

    private List<Transform> m_ParentList;

    public float SpeedFactor = 10f;

    private float m_CurrentSpeed = -1f;
    public override void InitUIData()
    {
        base.InitUIData();
        m_ParentList = new List<Transform>();
        m_ParentList.Add(selfTransform.FindChild("LayoutPoint"));

        m_SelfRectTrans = GetComponent<RectTransform>();//selfTransform.FindChild("LayoutPoint");

        m_OriginalStarItem = selfTransform.FindChild("OriginalPanel/OriginalStarItem").gameObject;
        m_OriginalCaptionText = selfTransform.FindChild("OriginalPanel/OriginalCaptionText").gameObject;
        m_OriginalClickableText = selfTransform.FindChild("OriginalPanel/OriginalClickableText").gameObject;

        m_Background = selfTransform.FindChild("LayoutPoint/Background").GetComponent<RectTransform>();
        m_TittleImage = selfTransform.FindChild("LayoutPoint/Background/TittleImage").GetComponent<RectTransform>();
        m_TittleMask = selfTransform.FindChild("LayoutPoint/Background/TittleMask").GetComponent<RectTransform>();
        m_LeftPoint = selfTransform.FindChild("LayoutPoint/Background/TittleMask/LeftPoint").GetComponent<RectTransform>();
        m_RightPoint = selfTransform.FindChild("LayoutPoint/Background/TittleMask/RightPoint").GetComponent<RectTransform>();
        m_TittleText = selfTransform.FindChild("LayoutPoint/Background/TittleImage/TittleText").GetComponent<Text>();
        m_TittleText.text = GameUtils.getString("System_setting_content37");

        m_Caption = selfTransform.FindChild("LayoutPoint/Background/TittleMask/Caption").GetComponent<RectTransform>();
        m_CaptionPanel = m_Caption.FindChild("CaptionPanel");
        m_ClickPanel = m_Caption.FindChild("ClickPanel");

        m_TextPool = new ChainItemPool(4, m_OriginalStarItem,m_OriginalCaptionText, m_OriginalClickableText, m_RightPoint);

        m_CaptionTable = DataTemplate.GetInstance().m_CaptionTable;

        m_PlayerName = ObjectSelf.GetInstance().Name;

        m_Background.gameObject.SetActive(false);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_InterfaceChange, OnInterfaceChange);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_ReceiveCaptionMessage, LoadServerData);

        Inst = this;
    }

    public override void InitUIView()
    {
        base.InitUIView();
        InvokeRepeating("EventNoticeLifeTimeHandler", 1, 1);
        m_Caption.position = m_RightPoint.position;
        OnInterfaceChange();
//        CreatDummyPackage();

//        OnArriveLeftLayoutPoint();
    }

    public void OnDestroy()
    {
        Inst = null;
        CancelInvoke("EventNoticeLifeTimeHandler");
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_InterfaceChange, OnInterfaceChange);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_ReceiveCaptionMessage, LoadServerData);
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();
        MoveTheCaption();
    }
    private void MoveTheCaption()
    {
        if (m_CurrentSpeed <= 0)
            return;

//        (m_Caption.anchoredPosition.x - m_LeftPoint.anchoredPosition.x) / 
        var vector = m_Caption.anchoredPosition;
        vector.x -= m_CurrentSpeed * Time.deltaTime;
        m_Caption.anchoredPosition = vector;

        if (vector.x <= m_LeftPoint.anchoredPosition.x)
        {
            m_CurrentSpeed = -1;
            OnArriveLeftLayoutPoint();
        }
    }

    /// <summary>
    /// 当播放完一条跑马灯文字后调用，释放控件资源，读取下一条文字。也可单独调用，启动文字播放
    /// </summary>
    private void OnArriveLeftLayoutPoint()
    {

        ReleaseCaption();
        m_Caption.position = m_RightPoint.position;
        isRunning = m_EventNotice.Count > 0;
        if (isRunning)
        {
            CreatCaption(m_EventNotice[m_EventNotice.Count - 1]);
            m_EventNotice.RemoveAt(m_EventNotice.Count - 1);
            RunTheCaption(SpeedFactor);
        }
        else
        {
            if (m_ParentList.Count <= 1)
                m_Background.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// GameEventID.UI_InterfaceChange回调，在加载或删除UI界面的时候刷新m_CurrentUI。
    /// 事件抛出逻辑见UI_HomeControler.FrameDelayControl()
    /// </summary>
    private void OnInterfaceChange()
    {
        BaseUI _currentPanel = UI_HomeControler.Inst.GetCanvas2FirstChildBaseUI();
        UIMark newMark;
        if (_currentPanel != null)
        {
            newMark = _currentPanel.UIMark;
        }
        else
        {
            newMark = UIMark.DefaultMark;
        }
        if (m_CurrentUI != newMark)
        {
            m_CurrentUI = newMark;
            m_EventNotice.Sort(SortHandler);
        }

    }

    //参数为字幕左端从屏幕左端到屏幕右端所需要的时间
    private void RunTheCaption(float duration)
    {
        if (m_CaptionTextList.Count <= 0)
            return;

        for (int i = 0; i < m_CaptionTextList.Count; i++)
        {
            m_CaptionTextList[i].ReadyToRun(m_CaptionPanel, m_ClickPanel);
        }

        float _timeInc = ResetLeftLayoutPoint()/((m_RightPoint.anchoredPosition.x - m_LeftPoint.anchoredPosition.x) / duration);

        m_CurrentSpeed = m_RightPoint.anchoredPosition.x / duration;
        //Sequence mySequence = DOTween.Sequence();
        //Tweener _tweener = m_Caption.DOMove(m_LeftPoint.position, duration + _timeInc, false);
        //_tweener.SetEase(Ease.Linear);
        //mySequence.Append(_tweener);
        //mySequence.AppendCallback(OnArriveLeftLayoutPoint);
        //mySequence.SetUpdate(true);

        m_Background.gameObject.SetActive(true);
    }

    private void CreatCaption(CaptionInfoPackage pack)
    {
        string[] _stringArr = GameUtils.getString(pack.m_InfoTable.getText()).Split('#');
        int[] _typeArr = pack.m_InfoTable.getDatatype();
        string[] _colorArr = pack.m_InfoTable.getColor().Split('#');
        Transform _lastTrans = m_Caption;
        
        for (int i = 0; i < _stringArr.Length - 1; i++)
        {
            if (_stringArr[i] != string.Empty)//普通文字
            {
                _lastTrans = LoadCaptionText(_stringArr[i], _lastTrans, Color.white);
            }

            switch (_typeArr[i])
            { 
                case 1: //参数为ID
                    int _num = -1;
                    if (int.TryParse(pack.m_ParaArr[i], out _num))
                    {
                        int _level = GetItemStarLv(_num);
                        if (_level > 0)
                        {
                            _lastTrans = LoadStarItem(_level, _lastTrans);
                        }
                        _lastTrans = LoadClickableText(pack.m_ParaArr[i], _lastTrans, StringToColor(_colorArr[i]));
                    }
                    else
                    {
                        LogManager.LogError(string.Format("解析失败:{0}无法转换成ID@参数:{1}", pack.m_ParaArr[i],i));
                        Debug.Log(string.Format("解析失败:{0}无法转换成ID@参数:{1}", pack.m_ParaArr[i], i));
                    }
                    break;
                case 2://参数为05表索引
                    string _name = GameUtils.getString(pack.m_ParaArr[i]);
                    if (_name != null)
                        _lastTrans = LoadCaptionText(_name, _lastTrans, StringToColor(_colorArr[i]));
                    else
                    {
                        Debug.Log(string.Format("索引{0}在05表中无效.name={1}", pack.m_ParaArr[i], _name));
                        LogManager.LogError(string.Format("索引{0}在05表中无效", pack.m_ParaArr[i]));
                    }
                    break;
                default://参数为普通字符串
                    _lastTrans = LoadCaptionText(pack.m_ParaArr[i], _lastTrans, StringToColor(_colorArr[i]));
                    break;
                
            }
        }
        if (_stringArr[_stringArr.Length - 1] != string.Empty)//普通文字
        {
            _lastTrans = LoadCaptionText(_stringArr[_stringArr.Length - 1], _lastTrans, Color.white);
        }
    }

    private void ReleaseCaption()
    {
        for (int i = 0; i < m_CaptionTextList.Count; i++)
        {
            m_TextPool.Release(m_CaptionTextList[i]);
        }
        m_CaptionTextList.Clear();
    }

    private CaptionInfoPackage CreatInfoPackage(int captionID,string[] paraArray,bool isSelfInfo)
    {
        CaptionInfoPackage temp = null;
        RunhorselightTemplate _table = m_CaptionTable.getTableData(captionID) as RunhorselightTemplate;
        
        if(_table != null)
        {
            temp = new CaptionInfoPackage();
            temp.m_InfoTable = _table;
            temp.m_ParaArr = paraArray;
            temp.IsSelfInfo = isSelfInfo;

            m_EventNotice.Add(temp);

        }
        return temp;
    }

    public void LoadServerData(GameEvent eventData)
    {
        bool _isSelfInfo = false;
        SSendMsgNotify _data = (SSendMsgNotify)eventData.data;
        string[] _paraArray = new string[_data.parameters.Count];
        for (int i = 0; i < _data.parameters.Count;i++)
        {
            _paraArray[i] = _data.parameters[i].getString();
            if (!_isSelfInfo && _paraArray[i] == m_PlayerName)//遍历参数列表，匹配是否有与自己名字相符的字符串
            {
                _isSelfInfo = true;
            }
        }
        if(CreatInfoPackage(_data.msgid, _paraArray, _isSelfInfo) != null)
            m_EventNotice.Sort(SortHandler);
        if(!isRunning)
            OnArriveLeftLayoutPoint();
    }

    private Transform LoadCaptionText(string content, Transform layout, Color color)
    {
        CaptionText _temp;
        _temp = m_TextPool.GetCaptionText();
        _temp.SetInfo(content, layout, color);
        m_CaptionTextList.Add(_temp);
        return _temp.m_SelfTrans;
    }
    private Transform LoadClickableText(string content, Transform layout, Color color)
    {
        ClickableText _cTemp = m_TextPool.GetClickableText();
        _cTemp.SetInfo(content, layout, color);
        m_CaptionTextList.Add(_cTemp);
        return _cTemp.m_SelfTrans;
    }
    private Transform LoadStarItem(int lv, Transform layout)
    {
        StarItem _sTemp = m_TextPool.GetStarItem();
        _sTemp.SetInfo(lv, layout);
        m_CaptionTextList.Add(_sTemp);
        return _sTemp.m_SelfTrans;
    }

    private float GetCaptionLength()
    {
        float _xOffset = 0f;
        for (int i = 0; i < m_CaptionTextList.Count;i++ )
        {
            _xOffset += m_CaptionTextList[i].ComponentLength;
        }
        return _xOffset;
    }
    private float ResetLeftLayoutPoint()
    {
        float _offset = GetCaptionLength();
        m_LeftPoint.anchoredPosition = new Vector2(-_offset, 0);
        return _offset;
    }

    private void EventNoticeLifeTimeHandler()
    {
        for (int i = 0; i < m_EventNotice.Count; i++)
        {
            m_EventNotice[i].m_LifeTime--;
            if (m_EventNotice[i].m_LifeTime == 0)
            {
                m_EventNotice.RemoveAt(i);
                i--;
            }
        }
    }

    private int GetItemStarLv(int itemID)
    {
        int _result = -1;
        var _classType = GameUtils.GetObjectClassById(itemID);

        if (_classType == EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE)
        {
            ItemTemplate _itemTable = DataTemplate.GetInstance().m_ItemTable.getTableData(itemID) as ItemTemplate;
            if (_itemTable != null)
            {
                _result = _itemTable.getQuality();
            }
        }
        else if (_classType == EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO)
        {
            HeroTemplate _heroTable = DataTemplate.GetInstance().m_HeroTable.getTableData(itemID) as HeroTemplate;
            if (_heroTable != null)
            {
                _result = _heroTable.getMaxQuality();
            }
        }
        return _result;
    
    }
    //降序排序
    private int SortHandler(CaptionInfoPackage pack1,CaptionInfoPackage pack2)
    {
        return pack2.GetSortKey(m_CurrentUI) - pack1.GetSortKey(m_CurrentUI);
    }
    private Color32 StringToColor(string colorString, byte alpha = 0xff)
    {
        uint _rgbaNum = Convert.ToUInt32(colorString,16);
        Color32 _color = new Color32();
        _color.r = (byte)((_rgbaNum >> 16) & 0xFF);
        _color.g = (byte)((_rgbaNum >> 8) & 0xFF);
        _color.b = (byte)(_rgbaNum & 0xFF);
        _color.a = alpha;
        return _color;
    }

    private void ChangeParent()
    {
        if (m_ParentList.Count <= 0)
            return;
        m_Background.gameObject.SetActive(m_ParentList.Count > 1 || isRunning);
        m_Background.SetParent(m_ParentList[m_ParentList.Count - 1]);
        m_Background.localScale = Vector3.one;
        m_Background.anchoredPosition = Vector2.zero;

    }
    public static string GetPath()
    {
        return Path;
    }
    public static UI_CaptionManager GetInstance()
    {
        return Inst;
    }

    /******************Debug**********************/
    private void CreatDummyPackage()
    {

        CreatInfoPackage(1, new string[2] { "Alice1", "1401100025" }, false);
        CreatInfoPackage(2, new string[2] { "Alice2", "1401100025" }, false);
        CreatInfoPackage(3, new string[2] { "Alice3", "1401100025" }, false);
        OnArriveLeftLayoutPoint();

        //SSendMsgNotify dummyMsg = new SSendMsgNotify();
        //dummyMsg.msgid = 1;
        //dummyMsg.parameters = new List<Octets>();
        //dummyMsg.parameters.Add(m_PlayerName);
        //dummyMsg.parameters.Add("1401100025");
        //GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MsgNotify, dummyMsg);

        CreatInfoPackage(4, new string[4] { "Alice4", "普通难度", "动感超人", "1401100025" }, false);
        CreatInfoPackage(5, new string[2] { "Alice5", "1403100053" }, false);
    }
    /// <summary>
    /// 跑马灯面板Y轴锚点在屏幕中心，该函数会修改RectTransform的Pos Y
    /// </summary>
    /// <param name="value"></param>
    private void SetOffsetY(float value)
    {
        var _vecter = m_SelfRectTrans.anchoredPosition;
        _vecter.y = value;
        m_SelfRectTrans.anchoredPosition = _vecter;
    }
    /// <summary>
    /// 跑马灯面板Y轴锚点在屏幕中心，该函数会将transform.position.y修改至trans.position.y
    /// </summary>
    /// <param name="trans"></param>
    private void SetOffsetY(Transform trans)
    {
        var _vecter = m_SelfRectTrans.position;
        _vecter.y = trans.position.y;
        m_SelfRectTrans.anchoredPosition = _vecter;
    }
    /// <summary>
    /// 完全修改transform.position
    /// </summary>
    /// <param name="position"></param>
    private void SetNewPos(Vector3 position)
    { 
        m_SelfRectTrans.position = position;
    }
    /// <summary>
    /// 将跑马灯面板复位，如果在其他面板代码中修改过跑马灯位置，面板关闭时记得复位
    /// </summary>
    private void ResetPos()
    {
        m_SelfRectTrans.anchoredPosition3D = Vector3.zero;
    }
    /*********************************************/

    /// <summary>
    /// 需要公告常驻界面时调用，唤醒跑马灯，此时跑马灯的现实部分会暂时成为参数Transform的子物体
    /// 参数Transform为一个点，不要有长宽，公告面板左上角会与该点对齐
    /// 在关闭界面时，务必调用Release()释放跑马灯，否则可能会引起访问已经销毁的游戏物体，这样的错误
    /// </summary>
    /// <param name="parent"></param>
    public void AwakeUp(Transform parent)
    {
        if (parent == null || m_ParentList.Contains(parent))
            return;

        m_ParentList.Add(parent);

        ChangeParent();
    }
    /// <summary>
    /// 界面关闭，不需要跑马灯常驻时调用，参数为之前唤醒是所传入的参数
    /// </summary>
    /// <param name="parent"></param>
    public void Release(Transform parent)
    {
        if (parent != null && m_ParentList.Remove(parent))
            ChangeParent();
    }
}
