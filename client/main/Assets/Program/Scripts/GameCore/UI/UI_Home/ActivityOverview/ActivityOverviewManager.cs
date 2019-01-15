using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using System;
using DreamFaction.UI;
using GNET;
using DreamFaction.GameEventSystem;

public class ActivityOverviewManager : BaseUI
{
    public static string UI_ResPath = "UI_ActivityOverview/UI_ActivityOverview_2_1";
    public static ActivityOverviewManager Inst;//单例
    private Text m_ActivityOverviewText;
    private Text m_TitleText;
    private Text m_TimeText;

    private GameObject m_TwoButton;
    private GameObject m_OneButton;

    private Text m_LeftButtonText;
    private Text m_RightButtonText;
    private Text m_MiddleButtonText;
    private Text m_LeftButtonInfoText;
    private Text m_MiddleButtonInfoText;

    private Button m_ReturnButton;
    private Button m_LeftButton;
    private Button m_RightButton;
    private Button m_MiddleButton;

    //预设物体
    public GameObject m_ActivityItem;
    private GameObject m_IconItem;
    private GameObject m_TextItem;

    //右侧图标的背景图片
    private GameObject m_IconItemBg;

    //预设物的父物体
    private GameObject m_ActivityItemParent;
    private GameObject m_IconItemParent;
    private GameObject m_TextItemParent;
    private Transform M_CapPos;

    //显示礼包item部分
    private GameObject m_AwardWindow;
    private GameObject m_itemParent;
    private Button m_CloseLBShowWindow;
    private Text m_ConfirmAwardText;// 确认奖励字
    private LoopLayout m_AwardIconLayout;
    List<AwardIconData> m_AwardIconData = new List<AwardIconData>();
     private UniversalItemCell m_Cell;

    List<ActivityBannerData> m_ActivityBannerData = new List<ActivityBannerData>();
    private LoopLayout m_BannerLayout;

    //自动排列所需参数 居中 靠左
    private ContentSizeFitter m_ContentSizeFitter;
    private RectTransform m_RectTransform;
    private ScrollRect m_IconList;

    private ObjectSelf m_ObjectSelf;
    private ActivityOverviewMar m_ActivityOverviewMar;

    private List<ActivityItem> m_ActivityItems = new List<ActivityItem>(); //当前所实例化的活动
    private int m_TeamId = -1000; //当前展示所有活动组id
    private List<int> m_OpenActivityID = new List<int>();//读配置表 找到需要打开的活动id
    private List<int> m_TeamKeyList = new List<int>();//team 组的key
    private Dictionary<int, List<int>> m_MergeData = new Dictionary<int, List<int>>();//合并类型以后的数据
    private int m_IconItemNum; //每个活动中图标的个数
    public  int m_PopupData_Num = -1;
    public override void InitUIData()
    {
        base.InitUIData();
        if (Inst == null)
            Inst = this;
        m_ObjectSelf = ObjectSelf.GetInstance();
        m_ActivityOverviewMar = ObjectSelf.GetInstance().GetActivityOverviewMar();

        m_ActivityOverviewText = selfTransform.FindChild("UI_BG_Top/UI_Btn_Binding/Text").GetComponent<Text>();
        m_TitleText = selfTransform.FindChild("RightWindow/HeadText").GetComponent<Text>();
        m_TimeText = selfTransform.FindChild("RightWindow/TimeText").GetComponent<Text>();

        m_TwoButton = selfTransform.FindChild("RightWindow/TwoButton").gameObject;
        m_OneButton = selfTransform.FindChild("RightWindow/MiddleButton").gameObject;

        m_IconItemBg = selfTransform.FindChild("RightWindow/Image").gameObject;
        M_CapPos = selfTransform.FindChild("pos");

        m_LeftButtonText = selfTransform.FindChild("RightWindow/TwoButton/LeftButton/Text").GetComponent<Text>();
        m_RightButtonText = selfTransform.FindChild("RightWindow/TwoButton/RightButton/Text").GetComponent<Text>();
        m_MiddleButtonText = selfTransform.FindChild("RightWindow/MiddleButton/Text").GetComponent<Text>();

        m_LeftButtonInfoText = selfTransform.FindChild("RightWindow/TwoButton/LeftButton/Image/MasText").GetComponent<Text>();
        m_MiddleButtonInfoText = selfTransform.FindChild("RightWindow/MiddleButton/Image/MasText").GetComponent<Text>();

        m_LeftButton = selfTransform.FindChild("RightWindow/TwoButton/LeftButton").GetComponent<Button>();
        m_LeftButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLeftBtn));
        m_RightButton = selfTransform.FindChild("RightWindow/TwoButton/RightButton").GetComponent<Button>();
        m_RightButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRightBtn));
        m_MiddleButton = selfTransform.FindChild("RightWindow/MiddleButton").GetComponent<Button>();
        m_MiddleButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickMiddleBtn));

        m_ReturnButton = selfTransform.FindChild("UI_BG_Top/UI_Btn_Back").GetComponent<Button>();
        m_ReturnButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickReturnBtn));

        //m_ActivityItem = selfTransform.FindChild("LeftWindow/VerticalList/ListLayOut/ActivityItem").gameObject;
        m_TextItem = selfTransform.FindChild("RightWindow/TextList/ListLayOut/item").gameObject;

        m_ActivityItemParent = selfTransform.FindChild("LeftWindow/VerticalList/ListLayOut").gameObject;
        m_TextItemParent  = selfTransform.FindChild("RightWindow/TextList/ListLayOut").gameObject;
        m_IconItemParent = selfTransform.FindChild("RightWindow/IconList/GoodsLayout").gameObject;

        m_ContentSizeFitter = selfTransform.FindChild("RightWindow/IconList/GoodsLayout").GetComponent<ContentSizeFitter>();
        m_RectTransform = selfTransform.FindChild("RightWindow/IconList/GoodsLayout").GetComponent<RectTransform>();
        m_IconList = selfTransform.FindChild("RightWindow/IconList").GetComponent<ScrollRect>();

        //弹框奖励确认部分  ----------------------------
        m_AwardWindow = selfTransform.FindChild("AwardWindow").gameObject;
        m_itemParent = selfTransform.FindChild("AwardWindow/UI_moreItem/Grid").gameObject;
        m_ConfirmAwardText = selfTransform.FindChild("AwardWindow/Image/Text").GetComponent<Text>();
        m_AwardIconLayout = selfTransform.FindChild("AwardWindow/UI_moreItem/Grid").GetComponent<LoopLayout>();
        m_BannerLayout = selfTransform.FindChild("LeftWindow/VerticalList/ListLayOut").GetComponent<LoopLayout>();

        m_CloseLBShowWindow = selfTransform.FindChild("AwardWindow/UI_Top/BackBtn").GetComponent<Button>();
        m_CloseLBShowWindow.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseLBShowWindow));

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_ActivityPointShow, RefreshItemRedPoint);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_ActivityRefreshSingle, RefreshSingleItem);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_ActivityMoneyChange, InstantiateIcon);
    }


    // 初始化UI显示内容
    public override void InitUIView()
    {
        base.InitUIView();

        m_LeftButtonText.text = GameUtils.getString("activity_button_n2");// 领取
        m_MiddleButtonText.text = GameUtils.getString("activity_button_n2");// 领取 
        m_ConfirmAwardText.text = "确认奖励";

        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.AwakeUp(M_CapPos);

        //foreach (KeyValuePair<int, ActivityOverviewData> kvp in ObjectSelf.GetInstance().GetActivityOverviewMar().m_ActivityOverviewData)
        //{
        //    Debug.Log("serverdata" + kvp.Key);
        //}

        ReadTableDateFindOpenActivity();
        SelectTeam();
        InstantiateItem();
        
    }

    /// <summary>
    /// 读取表数据 找到开启的活动的ID
    /// </summary>
    public void ReadTableDateFindOpenActivity()
    {
        foreach (int k in DataTemplate.GetInstance().m_GameactivityTable.GetDataKeys())
        {
            GameactivityTemplate _gt = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(k);

            DateTime _date = m_ObjectSelf.ServerDateTime;
            DateTime dt1 = GameUtils.ConvertStringToDateTime(_gt.getBeginday());
            DateTime dt2 = GameUtils.ConvertStringToDateTime(_gt.getDeadline());

            if (_date > dt1 && _date < dt2)
            {
                m_OpenActivityID.Add(k);
                
            }
        }
        //Debug.Log("m_OpenActivityID.Count : " + m_OpenActivityID.Count);

    }

    /// <summary>
    /// 遍历比较好的id 合并Ieam相同的id
    /// 所有在开启时间内的活动都要显示 不管有没有数据
    /// 服务器返回的数据 只是开启活动的数据 没有开启的也要显示 所以有了 m_ActivityBannerData 
    /// </summary>
    public void SelectTeam()
    {
        m_ActivityBannerData.Clear();
        

        for (int i = 0; i < m_OpenActivityID.Count; i++)
        {
            ActivityBannerData temp = new ActivityBannerData();
            if (m_ActivityOverviewMar.m_ActivityOverviewData.ContainsKey(m_OpenActivityID[i]))
            {
                temp._ActivityOverviewData = m_ActivityOverviewMar.m_ActivityOverviewData[m_OpenActivityID[i]];
                temp.m_Key = m_OpenActivityID[i];
            }
            else
            {
                ActivityOverviewData _ActivityOverviewData = new ActivityOverviewData();
                _ActivityOverviewData.m_id = m_OpenActivityID[i];
                temp.m_Key = m_OpenActivityID[i];
                temp._ActivityOverviewData = _ActivityOverviewData;
            }

            m_ActivityBannerData.Add(temp);

            GameactivityTemplate _gt = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(m_OpenActivityID[i]);
            if (m_MergeData.ContainsKey(_gt.getTeam()))
            {
                List<int> _temp = m_MergeData[_gt.getTeam()];
                _temp.Add(_gt.getId());
                m_MergeData[_gt.getTeam()] = _temp;
            }
            else
            {
                List<int> _temp = new List<int>();
                _temp.Add(_gt.getId());
                m_MergeData.Add(_gt.getTeam(), _temp);
            }
        }

        //foreach (KeyValuePair<int, List<int>> kvp in m_MergeData)
        //{
        //     Debug.Log("m_MergeData.key" + kvp.Key);
        //}
    }

    public class ActivityBannerData
    {
        public int m_Key;
        public ActivityOverviewData _ActivityOverviewData;
    }
    /// <summary>
    /// 实例化活动Item
    /// </summary>
    public void InstantiateItem()
    {
        int _bannerNum = 0;
        foreach (KeyValuePair<int, List<int>> kvp in m_MergeData)
        {
            m_TeamKeyList.Add(kvp.Key);
            _bannerNum++; 
        }
        m_TeamKeyList.Sort(Compare);

        m_BannerLayout.cellCount = _bannerNum;
        m_BannerLayout.updateCellEvent = UpdateBannerItem;
        m_BannerLayout.Reload();    
    }

    private int Compare(int Left,int Right)
    {
        List<int> _List = m_MergeData[Left];
        GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(_List[0]);
        int _num_1 = _Data.getSort();
        List<int> _List_2 = m_MergeData[Right];
        GameactivityTemplate _Data_2 = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(_List_2[0]);
        int _num_2 = _Data_2.getSort();
        if (_num_1 > _num_2)
            return 1;
        else
            return -1;
    }
    /// <summary>
    /// 用每组的最后一条数据 生成banner
    /// </summary>
    /// <param name="index">活动的key</param>
    /// <param name="cell"></param>
    private void UpdateBannerItem(int index, RectTransform cell)
    {
        ActivityItem _bannerItemData = cell.GetComponent<ActivityItem>();
        if (_bannerItemData == null)
        {
            _bannerItemData = cell.gameObject.AddComponent<ActivityItem>();
        }
        _bannerItemData.SetOnClick(SetItemImageLight);

        List<int> _List = m_MergeData[m_TeamKeyList[index]];

        for (int j = 0; j < m_ActivityBannerData.Count; j++)
        {
            if (m_ActivityBannerData[j]._ActivityOverviewData.m_id == _List[_List.Count - 1])
            {
                ActivityBannerData temp = m_ActivityBannerData[j];
                _bannerItemData.SetActivityDate(temp._ActivityOverviewData, temp.m_Key);
                GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(temp._ActivityOverviewData.m_id);
                if (m_TeamId == _Data.getTeam())
                {
                    _bannerItemData.SetImageLight(true);
                }
                else
                {
                    _bannerItemData.SetImageLight(false);
                }
                RefreshItem(_bannerItemData, temp._ActivityOverviewData.m_id);
            }
        }
    }
    /// <summary>
    /// 刷新BannerItem 是否显示已领取字样
    /// </summary>
    /// <param name="_ActivityItem"></param>
    /// <param name="_id"></param>
    public void RefreshItem(ActivityItem _ActivityItem,int _id)
    {

        GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(_id);
        int _allnum = 0;
        int _todaynum = 0;
        int _cangetnum = 0;
        if (m_ActivityOverviewMar.m_ActivityOverviewData.ContainsKey(_id))
        {
            _allnum = m_ActivityOverviewMar.m_ActivityOverviewData[_id].m_allnum;
            _todaynum = m_ActivityOverviewMar.m_ActivityOverviewData[_id].m_todaynum;
            _cangetnum = m_ActivityOverviewMar.m_ActivityOverviewData[_id].m_cangetnum;
        }
        int _Periodmax = _Data.getPeriodmax();
        int _Daymax = _Data.getDaymax();
        if (_Periodmax == -1)
        {
            _Periodmax = 1000000;
        }
        if (_Daymax == -1)
        {
            _Daymax = 1000000;
        }
        if (_todaynum == _Daymax)
        {
            if (_cangetnum == 0)
            {
                _ActivityItem.SetReceiveOverShow(true);
            }
            else
            {
                _ActivityItem.SetReceiveOverShow(false);
            }
        }
        else
        {
            _ActivityItem.SetReceiveOverShow(false);
        }
    }
    /// <summary>
    /// 点击后收到服务器的数据回调 是否显示红N提示
    /// </summary>
    public void RefreshItemRedPoint()
    {
        RefreshSingleItemData();
        m_BannerLayout.UpdateCell();
    }

    public void RefreshSingleItem()
    {
        InstantiateIcon();
        InterfaceControler.GetInst().AddMsgBox("领取成功", this.transform);
        m_BannerLayout.UpdateCell();
    }
    /// <summary>
    /// 刷新选中组中的每个活动的数据
    /// </summary>
    public void RefreshSingleItemData()
    {
        List<int> _tempList = m_MergeData[m_TeamId];
        for (int i = 0; i < _tempList.Count; i++)
        {
            for (int j = 0; j < m_ActivityBannerData.Count; j++)
            {
                if (_tempList[i] == m_ActivityBannerData[j]._ActivityOverviewData.m_id && m_ActivityOverviewMar.m_ActivityOverviewData.ContainsKey(_tempList[i]))
                {
                    m_ActivityBannerData[j]._ActivityOverviewData = m_ActivityOverviewMar.m_ActivityOverviewData[_tempList[i]];
                }
            }
        }
    }

    public void SetItemImageLight(int teamId)
    {
        if (m_TeamId == teamId)
            return;

        m_TeamId = teamId;
        InstantiateIcon();
        m_BannerLayout.UpdateCell();
    }

    /// <summary>
    /// 实例化图标
    /// </summary>
    public void InstantiateIcon()
    {
        m_IconItemNum = 0;
        WhetherOpenPopupWindow();
        RefreshSingleItemData();

        for (int k = 0; k < m_IconItemParent.transform.childCount; k++)
        {
                Destroy(m_IconItemParent.transform.GetChild(k).gameObject);
        }


        List<int> _tempList = m_MergeData[m_TeamId];
        for (int i = 0; i < _tempList.Count; i++)
        {
             GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(_tempList[i]);
 
            for (int L = 0; L < m_ActivityBannerData.Count; L++)
            {
                if (m_ActivityBannerData[L]._ActivityOverviewData.m_id == _tempList[i])
                {
                    GreatItemPrepare(_Data,m_ActivityBannerData[L]._ActivityOverviewData);
                    if (i == _tempList.Count - 1)
                    {
                        GreatTextItem(_Data,m_ActivityBannerData[L]._ActivityOverviewData);
                    }
                }
            }     
        }
        SetArrangeStyle();
        ReceiveButtonShow();
        m_IconItemBg.SetActive(true);
    }

    private void GreatItemPrepare(GameactivityTemplate _Data,ActivityOverviewData _ActivityOverviewData)
    {
        string[] _dropArray = _Data.getDropdes().Split('#');
        for (int j = 0; j < _dropArray.Length; j++)
        {
            GreatItem(j, _Data, _ActivityOverviewData);
            m_IconItemNum += 1;
        } 
    }
    /// <summary>
    /// 实例化Text Item
    /// </summary>
    /// <param name="_Data">表数据</param>
    /// <param name="_ActivityOverviewData">服务器数据</param>
    private void GreatTextItem(GameactivityTemplate _Data, ActivityOverviewData _ActivityOverviewData)
    {
        for (int k = 0; k < m_TextItemParent.transform.childCount; k++)
        {
            if (m_TextItemParent.transform.GetChild(k).gameObject.name == "item(Clone)")
            {
                Destroy(m_TextItemParent.transform.GetChild(k).gameObject);
            }
        }

        GameObject _TextItem = Instantiate(m_TextItem) as GameObject;
        _TextItem.transform.SetParent(m_TextItemParent.transform, true);
        _TextItem.transform.localScale = new Vector3(1, 1, 1);
        _TextItem.transform.localPosition = new Vector3(0, 0, 0);
        _TextItem.gameObject.SetActive(true);
        _TextItem.GetComponent<TextItem>().SetTextData(GameUtils.getString(_Data.getContentdes()));

        //右界面显示的内容（标题；时间；按钮字）
        //显示已经完成的条件（花了多少金币；完成多少任务--- 按钮上面的字）
        m_TitleText.text = GameUtils.getString(_Data.getTitledes());
        string dt1 = GameUtils.ConvertStringToDate(_Data.getBeginday());
        string dt2 = GameUtils.ConvertStringToDate(_Data.getDeadline());
        m_TimeText.text = dt1 + "-" + dt2;

        TotalNumStyleText(_Data.getType(), _Data.getParameter2(), _ActivityOverviewData.m_allactivitynum, _ActivityOverviewData.m_activitynum); 
    }

    private void GreatItem(int _key, GameactivityTemplate _GameactivityTemplate, ActivityOverviewData _ActivityOverviewData)
    {
        m_Cell = UniversalItemCell.GenerateItem(m_IconItemParent.transform);
        
        int _Daymax = _GameactivityTemplate.getDaymax();
        if (_Daymax == -1)
        {
            _Daymax = 1000000;
        }

        if (_ActivityOverviewData.m_todaynum < _Daymax)
        {
            m_Cell.SetCheckClaim(false, "");
        }
        else
        {
            if (_ActivityOverviewData.m_cangetnum == 0)
            {
                m_Cell.SetCheckClaim(true, "");
            }
            else
            {
                m_Cell.SetCheckClaim(false, "");
            }
        }

        int[] dropdestypeArray = _GameactivityTemplate.getDropdestype();
        string[] DropdesArray = _GameactivityTemplate.getDropdes().Split('#');
        int[] numdesArray = _GameactivityTemplate.getNumdes(); ;
        string[] TextdesArray = _GameactivityTemplate.getTextdes().Split('#');

        int _num = -1;
        if (numdesArray.Length != 0)
        {
            if (numdesArray[_key] != -1)
            {
                _num = numdesArray[_key];
            }
        }

        if (dropdestypeArray[_key] == 1)
        {
            int itemid = int.Parse(DropdesArray[_key]);

            int type = itemid / 1000000;

            switch (type)
            {
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                    ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                    if (_temp_res != null)
                    {
                        m_Cell.InitByID(itemid, _num);
                        m_Cell.SetText(GameUtils.getString(_temp_res.getName()), "", "");
                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE: //符文
                    {
                        ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                        if (itemTable != null)
                        {
                            m_Cell.InitByID(itemid, -1);
                            m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                        }
                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                    {
                        ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                        if (itemTable != null)
                        {
                            m_Cell.InitByID(itemid, _num);
                            m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                        }
                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                    {
                        HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(itemid);
                        if (hero != null)
                        {
                            m_Cell.InitByID(itemid, _num);
                            m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                        }
                    }
                    break;

                default:
                    break;
            }
        }
        if (dropdestypeArray[_key] == 0)
        {
            m_Cell.SetText(GameUtils.getString(TextdesArray[_key]), "", "");
            if (TextdesArray[_key] != string.Empty)
            {
                Sprite sprite = UIResourceMgr.LoadSprite(common.defaultPath + TextdesArray[_key]);
                m_Cell.InitBySprite(sprite);
            }
        }
    }

public class AwardIconData
{
    public int m_index;
    public GameactivityTemplate m_GameactivityTemplate;
}

    /// <summary>
    /// 判断是否需要打开奖励显示窗口
    /// </summary>
    private void WhetherOpenPopupWindow()
    {
        int _iconNum = 0;

        if (m_PopupData_Num < 0)
        {
            return;
        }

        m_AwardIconData.Clear();
        m_AwardWindow.SetActive(true);
        foreach (Transform child in m_itemParent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int k = 0; k < m_PopupData_Num; k++)
        {
            List<int> _tempList = m_MergeData[m_TeamId];
            GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(_tempList[0]);

            string[] dropArray = _Data.getDropdes().Split('#');
            for (int j = 0; j < _Data.getDropdestype().Length; j++)
            {
                _iconNum++;
                AwardIconData _temp = new AwardIconData();
                _temp.m_index = j;
                _temp.m_GameactivityTemplate = _Data;
                m_AwardIconData.Add(_temp);
            }
        }
        m_AwardIconLayout.cellCount = _iconNum;
        m_AwardIconLayout.updateCellEvent = UpdateAwardIconItem;
        m_AwardIconLayout.Reload();
        m_PopupData_Num = -1;
    }

    private void UpdateAwardIconItem(int index, RectTransform cell)
    {
        AwardIconData temp = m_AwardIconData[index];
        UniversalItemCell _UniversalItemCell = cell.GetComponent<UniversalItemCell>();
        if (_UniversalItemCell == null)
        {
            _UniversalItemCell = cell.gameObject.AddComponent<UniversalItemCell>();
        }
        GreatAwardItem(temp.m_index, temp.m_GameactivityTemplate, _UniversalItemCell);

    }

    private void GreatAwardItem(int _key, GameactivityTemplate _GameactivityTemplate,UniversalItemCell m_Cell)
    {
        int _num = -1;

        int[] dropdestypeArray = _GameactivityTemplate.getDropdestype();
        string[] DropdesArray = _GameactivityTemplate.getDropdes().Split('#');
        int[] numdesArray = _GameactivityTemplate.getNumdes(); ;
        string[] TextdesArray = _GameactivityTemplate.getTextdes().Split('#');

        if (numdesArray.Length != 0)
        {
            if (numdesArray[0] != -1)
            {
                _num = numdesArray[_key];
            }           
        }

        if (dropdestypeArray[_key] == 1)
        {
            int itemid = int.Parse(DropdesArray[_key]);

            int type = itemid / 1000000;

            switch (type)
            {
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                    ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                    if (_temp_res != null)
                    {
                        m_Cell.InitByID(itemid, _num);
                        m_Cell.SetText(GameUtils.getString(_temp_res.getName()), "", "");
                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE: //符文
                    {
                        ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                        if (itemTable != null)
                        {
                            m_Cell.InitByID(itemid, -1);
                            m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                        }
                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                    {
                        ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                        if (itemTable != null)
                        {
                            m_Cell.InitByID(itemid, _num);
                            m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                        }
                    }
                    break;
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                    {
                        HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(itemid);
                        if (hero != null)
                        {
                            m_Cell.InitByID(itemid, _num);
                            m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                        }
                    }
                    break;

                default:
                    break;
            }
        }
        if (dropdestypeArray[_key] == 0)
        {
            m_Cell.SetText(GameUtils.getString(TextdesArray[_key]), "", "");
            Sprite sprite = UIResourceMgr.LoadSprite(common.defaultPath + TextdesArray[_key]);
            m_Cell.InitBySprite(sprite);
        }
    }

    /// <summary>
    /// 关闭物品显示窗口
    /// </summary>
    public void OnClickCloseLBShowWindow()
    {
        m_AwardWindow.SetActive(false);
    }

    /// <summary>
    /// 图标的排列方式 小于 3 居中； 大于 3 靠右
    /// </summary>
    public void SetArrangeStyle()
    {
        if (m_IconItemNum > 4)
        {
            m_IconList.movementType = ScrollRect.MovementType.Elastic;
            m_IconItemParent.GetComponent<GridLayoutGroup>().padding.left = 100;
            m_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            m_RectTransform.sizeDelta = new Vector2(1224, 307);
            m_RectTransform.anchoredPosition = new Vector2(0, m_RectTransform.anchoredPosition.y);
        }
        else
        {
            m_IconList.movementType = ScrollRect.MovementType.Clamped;
            m_IconItemParent.GetComponent<GridLayoutGroup>().padding.left = 0;
            m_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            m_RectTransform.sizeDelta = new Vector2(1224, 307);
            m_RectTransform.anchoredPosition = new Vector2(0, m_RectTransform.anchoredPosition.y);
        }
    }

    private void TotalNumStyleText(int _style, int _Parameter2, int _allactivitynum, int _activitynum)
    {
        m_MiddleButtonInfoText.transform.parent.gameObject.SetActive(true);
        m_LeftButtonInfoText.transform.parent.gameObject.SetActive(true);
        if (_style == 6)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion1") + "  " + "<color=#ffff00>" + _activitynum + GameUtils.getString("61gameactivity_yuan") + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 5 || _style == 7)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion1") + "  " + "<color=#ffff00>" + _allactivitynum + GameUtils.getString("61gameactivity_yuan") + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 8 || _style == 9 || _style == 10 || _style == 11 || _style == 12 || _style == 13)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion2") + "  " + "<color=#ffff00>" + _allactivitynum + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 17)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion3") + "  " + "<color=#ffff00>" + _allactivitynum + "/" + _Parameter2 + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 18)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion4") + "  " + "<color=#ffff00>" + _allactivitynum + "/" + _Parameter2 + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 28 || _style == 29 || _style == 30)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion5") + "  " + "<color=#ffff00>" + _allactivitynum + "/" + _Parameter2 + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 31 || _style == 32 || _style == 33 || _style == 34)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion6") + "  " + "<color=#ffff00>" + _allactivitynum + GameUtils.getString("61gameactivity_ge") + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if ( _style == 45 || _style == 46 || _style == 47 || _style == 48)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion6") + "  " + "<color=#ffff00>" + _allactivitynum + GameUtils.getString("61gameactivity_ge") + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 35 || _style == 36 || _style == 37)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion7") + "  " + "<color=#ffff00>" + _allactivitynum + GameUtils.getString("61gameactivity_ci") + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 38 || _style == 39 || _style == 40)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion8") + "  " + "<color=#ffff00>" + _allactivitynum + GameUtils.getString("61gameactivity_ci") + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 41 || _style == 42 || _style == 43 || _style == 44)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion9") + "  " + "<color=#ffff00>" + _allactivitynum + GameUtils.getString("61gameactivity_ci") + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }
        if (_style == 53)
        {
            m_MiddleButtonInfoText.text = GameUtils.getString("61gameactivity_completion10") + " " + "<color=#ffff00>" + _allactivitynum + GameUtils.getString("61gameactivity_ci") + "</color>";
            m_LeftButtonInfoText.text = m_MiddleButtonInfoText.text;
            return;
        }

        m_MiddleButtonInfoText.text = "";
        m_LeftButtonInfoText.text = "";
        m_MiddleButtonInfoText.transform.parent.gameObject.SetActive(false);
        m_LeftButtonInfoText.transform.parent.gameObject.SetActive(false);
        return;
       
    }
    /// <summary>
    /// 按钮的显示
    /// </summary>
   // public void ReceiveButtonShow(ActivityOverviewData _ActivityOverviewData, GameactivityTemplate _GameactivityTemplate)
    public void ReceiveButtonShow()
    {
        List<int> _tempList = m_MergeData[m_TeamId];
        for (int i = 0; i < _tempList.Count; i++)
        {
            GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(_tempList[i]);

                string[] jumpTypeArray = _Data.getJumpstype().Split('#');
                int _allnum = 0;
                int _todaynum = 0;
                int _cangetnum = 0;
                if (m_ActivityOverviewMar.m_ActivityOverviewData.ContainsKey(_tempList[i]))
                {
                    _allnum = m_ActivityOverviewMar.m_ActivityOverviewData[_tempList[i]].m_allnum;
                    _todaynum = m_ActivityOverviewMar.m_ActivityOverviewData[_tempList[i]].m_todaynum;
                    _cangetnum = m_ActivityOverviewMar.m_ActivityOverviewData[_tempList[i]].m_cangetnum;
                }


                int _Periodmax = _Data.getPeriodmax();
                int _Daymax = _Data.getDaymax();
                if (_Periodmax == -1)
                {
                    _Periodmax = 1000000;
                }
                if (_Daymax == -1)
                {
                    _Daymax = 1000000;
                }

                if (string.IsNullOrEmpty(jumpTypeArray[0]))
                {
                    m_MiddleButton.gameObject.SetActive(true);
                    m_LeftButton.gameObject.SetActive(false);
                    m_RightButton.gameObject.SetActive(false);

                    if (_todaynum <= _Daymax)
                    {
                        if (_cangetnum > 0)
                        {
                            MiddleButtonLight();
                            return;
                        }
                        else
                        {
                            MiddleButtonGrey();
                        }
                    }
                    else
                    {
                        MiddleButtonGrey();
                    }
                }
                else if (int.Parse(jumpTypeArray[0]) == 1)
                {
                    m_RightButtonText.text = GameUtils.getString("activity_button_n1");// common_button_recharge  充值 
                }
                else
                {
                    m_RightButtonText.text = GameUtils.getString("liveness_content5"); // 前 往
                }

                 if (!string.IsNullOrEmpty(jumpTypeArray[0]))
                {
                    m_MiddleButton.gameObject.SetActive(false);
                    m_LeftButton.gameObject.SetActive(true);
                    m_RightButton.gameObject.SetActive(true);
                    if (_todaynum <= _Daymax)
                    {
                        if (_cangetnum > 0)
                        {
                            LeftButtonLight();
                            return;
                        }
                        else
                        {
                            LeftButtonGrey();
                        }
                    }
                    else
                    {
                        LeftButtonGrey();
                    }
                }
        }
    }

    public void ForReceiveAward()
    {
        List<int> _tempList = m_MergeData[m_TeamId];
        for (int i = 0; i < _tempList.Count; i++)
        {
            if (m_ActivityOverviewMar.m_ActivityOverviewData[_tempList[i]].m_cangetnum > 0)
            {
                CGetGameAct _CGetGameAct = new CGetGameAct();
                _CGetGameAct.actid = _tempList[i];
                //Debug.Log(_tempList[i]);
                IOControler.GetInstance().SendProtocol(_CGetGameAct);
            }
        }

        //获取领取的所有图标的ID 用于弹框显示用
        GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(_tempList[0]);
        if (_Data.getDaymax() == -1 && _Data.getPeriodmax() == -1)
        {
            m_PopupData_Num = m_ActivityOverviewMar.m_ActivityOverviewData[_tempList[0]].m_cangetnum;//当天，期间无限制的组中都只有一个活动
        }

    }

    private void OnClickLeftBtn()
    {
        ForReceiveAward();
    }

    private void OnClickRightBtn()
    {
        List<int> _tempList = m_MergeData[m_TeamId];
        GameactivityTemplate _Data = (GameactivityTemplate)DataTemplate.GetInstance().m_GameactivityTable.getTableData(_tempList[0]);
        string[] jumpTypeArray = _Data.getJumpstype().Split('#');

        if (int.Parse(jumpTypeArray[0]) == 1)
        {
            //弹出充值窗口
            UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
        }
        if (int.Parse(jumpTypeArray[0]) == 3)
        {
            //跳转到世界Boss
            GameObject go = UI_HomeControler.Inst.AddUI(UI_TestPanel.GetPath());
            if (go)
            {
                go.GetComponent<UI_TestPanel>().GotoWorldBoss();
            }
            OnClickReturnBtn();
        }
        if (int.Parse(jumpTypeArray[0]) == 4)
        {
            //跳转到英雄招募
            UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
            OnClickReturnBtn();
        }
        if (int.Parse(jumpTypeArray[0]) == 5)
        {
            //跳转到遗迹宝藏
            UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
            if (UI_Recruit.inst != null)
            {
                UI_Recruit.inst.OpenRelicBtn();
            } 
            OnClickReturnBtn();
        }
        if (int.Parse(jumpTypeArray[0]) == 6)
        {
            UI_HomeControler.Inst.AddUI(UI_ShopMgr.UI_ResPath);
            UI_ShopMgr.SetCurShowTab(SHOP_TAB.SKIN);
            //跳转到商城时装
            //UI_HomeControler.Inst.AddUI(UI_ShopMgr.UI_ResPath);
            //if (UI_ShopMgr.inst != null)
            //{
            //    UI_ShopMgr.inst.OnSkinToggleValueChanged(true);
            //}
            OnClickReturnBtn();
        }
        if (int.Parse(jumpTypeArray[0]) == 7)
        {
            //跳转到商城礼包
            UI_HomeControler.Inst.AddUI(UI_ShopMgr.UI_ResPath);
            UI_ShopMgr.SetCurShowTab(SHOP_TAB.GIFT);
            OnClickReturnBtn();
        }
        if (int.Parse(jumpTypeArray[0]) == 8)
        {
            //跳转到商城道具
            UI_HomeControler.Inst.AddUI(UI_ShopMgr.UI_ResPath);
        }
        if (int.Parse(jumpTypeArray[0]) == 9)
        {
            //跳转到神器
            UI_HomeControler.Inst.AddUI(UI_Artifact.UI_ResPath);
            OnClickReturnBtn();
        }
        if (int.Parse(jumpTypeArray[0]) == 10)
        {
            //跳转到符文熔炼
            UI_HomeControler.Inst.AddUI(UI_RuneExp.UI_ResPath);
            OnClickReturnBtn();
        }
        if (int.Parse(jumpTypeArray[0]) == 11)
        {
            //跳转到熔灵
            UI_HomeControler.Inst.AddUI(UI_HeroLitholysin.UI_ResPath);
            OnClickReturnBtn();
        }
        // 2 跳转到关卡章节
        if (int.Parse(jumpTypeArray[0]) == 2)
        {
            //当前章节Id
            int _CurChapterId = StageModule.GetPlayerLastChapterID();
            if (_CurChapterId < int.Parse(jumpTypeArray[1]))
            {
                InterfaceControler.GetInst().AddMsgBox("指定章节未开放", this.transform);
                return;
            }
            if (jumpTypeArray.Length >= 2)
           {
               if (int.Parse(jumpTypeArray[1]) == 1001)
               {
                   ObjectSelf.GetInstance().SetIsPrompt(true);
                   ObjectSelf.GetInstance().Week = ObjectSelf.GetInstance().GetWeek();
                   UI_HomeControler.Inst.AddUI(UI_PromptFightArea.UI_ResPath);
                   OnClickReturnBtn();
               }
               else
               {
                   UI_SelectLevelMgrNew.InitChapterId = int.Parse(jumpTypeArray[1]);
                   UI_HomeControler.Inst.AddUI(UI_SelectLevelMgrNew.UI_ResPath);              
                   OnClickReturnBtn();
               }
           }
        }
    }

    protected void OnDestroy()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(M_CapPos);
        ObjectSelf.GetInstance().SetIsPrompt(false);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_ActivityPointShow, RefreshItemRedPoint);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_ActivityRefreshSingle, RefreshSingleItem);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_ActivityMoneyChange, InstantiateIcon);
        Inst = null;
    }
    private void OnClickMiddleBtn()
    {
        ForReceiveAward();
    }

    private void MiddleButtonLight()
    {
        GameUtils.SetBtnSpriteGrayState(m_MiddleButton, false);
        m_MiddleButton.enabled = true;
    }
    private void MiddleButtonGrey()
    {
        GameUtils.SetBtnSpriteGrayState(m_MiddleButton, true);
        m_MiddleButton.enabled = false;
    }
    private void LeftButtonLight()
    {
        GameUtils.SetBtnSpriteGrayState(m_LeftButton, false);
        m_LeftButton.enabled = true;
    }
    private void LeftButtonGrey()
    {
        GameUtils.SetBtnSpriteGrayState(m_LeftButton, true);
        m_LeftButton.enabled = false;
    }
    private void RightButtonLight()
    {
        GameUtils.SetBtnSpriteGrayState(m_RightButton, false);
        m_RightButton.enabled = true;
    }
    private void RightButtonGrey()
    {
        GameUtils.SetBtnSpriteGrayState(m_RightButton, true);
        m_RightButton.enabled = false;
    }

    private void OnClickReturnBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

}
