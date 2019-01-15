using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using System.Collections;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using UnityEngine.EventSystems;
using DreamFaction.GameEventSystem;

public class UI_EmailManager : BaseUI
{
    protected Button m_BackBtn; // 关闭窗口按钮
    protected Button m_ReceiveBtn;// 领取按钮
    protected Button m_DelBtn;// 删除已读按钮
    protected Text EmailSystem;

    //RightWindow 显示内容
    protected Text EmailTitleText;
    protected Text EmailSenderText;
    protected Text EmailInfoText;
    protected Text EmailSafeDaysText;

    public Text SenderText;// 发件人 文字
    public Text TitleText;// 标题 文字
    public Text ReceiveText;// 领取附件 文字

    public Text NoMailText;// 冒险家，还没有新的邮件哦 文字

    //LiftWindow 显示内容
    protected Text ShangLaText;
    protected Text DeleteReadEmailText;
    private Text EmailTotalText;
    private Text EmailTotalNumText;

    //附件关闭&显示父物体
    private GameObject AwardIcon;
    private GameObject ReceiveButton;
    private GameObject RightWindow;
    private GameObject LoadImage;

    //有没有附件显示时的父物体
    private GameObject HaveMail;
    private GameObject NoMail;

    //加载更多，获得物体上滑动条的值
    private GameObject VerticalList;

    public static UI_EmailManager Inst;//单例
    public static string UI_ResPath = "Email/UI_Email_2_1";
    public GameObject EmailListItem; // 列表Item
    private RectTransform EmailList; //左侧列表控件 的显示对象
    private LoopLayout m_MailBannerLayout;
    private ScrollRect GridLayout;

    //附件
    private RectTransform EmailFuJianList; //右侧附件列表控件 的显示对象
    private ContentSizeFitter m_FuJianContentSizeFitter;
    private ScrollRect m_GoodList;
    private List<MailData> MailListData = new List<MailData>(); //服务器得到的列表数据
    private UniversalItemCell m_Cell;

    public MailData SelectItemMail = new MailData(); // 选中的Item的数据 Mail
    private bool IsReceive = false; //判段是不是在领取


    public override void InitUIData()
    {
        base.InitUIData();
        if (Inst == null)
            Inst = this;
        ObjectSelf.GetInstance().GetManager().RequestSeverListData(0);

        m_BackBtn = selfTransform.FindChild("UI_Top/BackBtn").GetComponent<Button>();
        m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));

        m_ReceiveBtn = selfTransform.FindChild("haveMail/UI_IconList/Button").GetComponent<Button>();
        m_ReceiveBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickReceiveBtn));

        m_DelBtn = selfTransform.FindChild("haveMail/leftBottom/Button").GetComponent<Button>();
        m_DelBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickDelBtn));

        EmailSystem = selfTransform.FindChild("UI_Top/Name").GetComponent<Text>();

        ShangLaText = selfTransform.FindChild("haveMail/LeftWindow/MoreText").GetComponent<Text>();
        DeleteReadEmailText = selfTransform.FindChild("haveMail/leftBottom/Button/Text").GetComponent<Text>();
        EmailTotalText = selfTransform.FindChild("haveMail/leftBottom/mailTotal").GetComponent<Text>();
        EmailTotalNumText = selfTransform.FindChild("haveMail/leftBottom/mailTotalNum").GetComponent<Text>();

        EmailList = selfTransform.FindChild("haveMail/LeftWindow/VerticalList/ListLayOut").GetComponent<RectTransform>();//左侧 邮件列表的父节点 
        GridLayout = selfTransform.FindChild("haveMail/LeftWindow/VerticalList").GetComponent<ScrollRect>();
        m_MailBannerLayout = selfTransform.FindChild("haveMail/LeftWindow/VerticalList/ListLayOut").GetComponent<LoopLayout>();

        EmailFuJianList = selfTransform.FindChild("haveMail/UI_IconList/GoodList/GoodsLayout").GetComponent<RectTransform>();//右侧 附件列表的父节点
        m_FuJianContentSizeFitter = selfTransform.FindChild("haveMail/UI_IconList/GoodList/GoodsLayout").GetComponent<ContentSizeFitter>();
        m_GoodList = selfTransform.FindChild("haveMail/UI_IconList/GoodList").GetComponent<ScrollRect>();

        //邮件内容Lable
        EmailTitleText = selfTransform.FindChild("haveMail/RightWindow/HeadText").GetComponent<Text>();
        EmailSenderText = selfTransform.FindChild("haveMail/RightWindow/SenderText").GetComponent<Text>();
        SenderText = selfTransform.FindChild("haveMail/RightWindow/Sender").GetComponent<Text>();
        EmailInfoText = selfTransform.FindChild("haveMail/UI_IconList/info").GetComponent<Text>();
        EmailSafeDaysText = selfTransform.FindChild("haveMail/RightWindow/PromptOBJ/Text").GetComponent<Text>();
        ReceiveText = selfTransform.FindChild("haveMail/UI_IconList/Button/Text").GetComponent<Text>();

        NoMailText = selfTransform.FindChild("NoMail/LeftWindow/Text").GetComponent<Text>();

        AwardIcon = selfTransform.FindChild("haveMail/UI_IconList").gameObject;
        ReceiveButton = selfTransform.FindChild("haveMail/UI_IconList/Button").gameObject;
        RightWindow = selfTransform.FindChild("haveMail/RightWindow").gameObject;
        VerticalList = selfTransform.FindChild("haveMail/LeftWindow/VerticalList").gameObject;
        LoadImage = selfTransform.FindChild("haveMail/LeftWindow/JiaZaiText").gameObject;

        HaveMail = selfTransform.FindChild("haveMail").gameObject;
        NoMail = selfTransform.FindChild("NoMail").gameObject;

        EventTriggerListener.Get(GridLayout.gameObject).onEndDrag = OnGridLayoutEndDrag;
        EventTriggerListener.Get(GridLayout.gameObject).onDrag = OnGridLayoutDrag;
       // EventTriggerListener.Get(GridLayout.gameObject).onExit = OnGridLayoutExit;

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_MailRefresh, RefreshItemInfo);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_MailReceiveListData, ReceiveListData);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_MailReceiveMore, AddMoreItem);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_MailDel, ClearSelectItemData);
    }
     // 2：初始化UI显示内容
    public override void InitUIView()
    {
        base.InitUIView();
        EmailSystem.text = GameUtils.getString("mail_content6");//系统邮件
        ShangLaText.text = GameUtils.getString("mail_content9");// 松开加载更多 文字
        DeleteReadEmailText.text = GameUtils.getString("mail_content3");// 删除邮件 文字 
        EmailTotalText.text = GameUtils.getString("mail_content17").Replace("{0}","");//邮件总数
        SenderText.text = GameUtils.getString("mail_content10");//发件人 文字
        EmailSafeDaysText.text = GameUtils.getString("mail_content4");//保存天数
        ReceiveText.text = GameUtils.getString("mail_content2");
        NoMailText.text = GameUtils.getString("mail_content7");
    }

    private void OnGridLayoutDrag(GameObject go, PointerEventData data = null)
    {
        if (GridLayout.verticalNormalizedPosition < 3000/EmailList.rect.height/-100f)
        {
            GridLayout.inertia = false;
            ShangLaText.gameObject.SetActive(true);
        }
        else
        {
            ShangLaText.gameObject.SetActive(false);
        }
    }

    private void OnGridLayoutEndDrag(GameObject go, PointerEventData data = null)
    {
        if (GridLayout.verticalNormalizedPosition < 3000 / EmailList.rect.height / -100f)
        {
            GridLayout.inertia = false;
            ObjectSelf.GetInstance().CurGetDataType = EM_GETMAIL_TYPE.GETMORE;
            ShangLaText.gameObject.SetActive(false);
            LoadImage.SetActive(true);
            ObjectSelf.GetInstance().GetManager().RequestSeverListData(ObjectSelf.GetInstance().GetManager().m_MailList.Count);
        }
        else
        {
            GridLayout.inertia = true;
        }
        Invoke("OnGridLayoutExit", 0.3f);
    }

    private void OnGridLayoutExit()
    {
        GridLayout.inertia = true;
    }

    public void CloseLoad()
    {
        ShangLaText.gameObject.SetActive(false);
        LoadImage.SetActive(false);
    }


    public void ReceiveListData()
    {
        //得到服务器列表数据；

        MailListData = ObjectSelf.GetInstance().GetManager().m_MailList;
        MailItemSort(ref MailListData);
        if (MailListData.Count == 0)
        {
            HaveMail.SetActive(false);
            NoMail.SetActive(true);
        }
        else
        {
            NoMail.SetActive(false);
            HaveMail.SetActive(true);
            CreatListItem();

        }

        EmailTotalNumText.text = ObjectSelf.GetInstance().GetManager().mailallsize.ToString();//邮件总数
    }


    //已拥有的邮件排序
    private void MailItemSort(ref List<MailData> list)
    {
        int count = list.Count;
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = i + 1; j < count; j++)
            {
                if (list[j].m_endtime > list[i].m_endtime)
                {
                    MailData tempobj = list[i];
                    list[i] = list[j];
                    list[j] = tempobj;
                }
            }
        }
    }

    public void AddMoreItem(GameEvent ge)
    {
        int id = (int)ge.data;
        EmailTotalNumText.text = ObjectSelf.GetInstance().GetManager().mailallsize.ToString();
        CloseLoad();
        m_MailBannerLayout.cellCount = MailListData.Count;
        m_MailBannerLayout.ReLoadMore(id);
      
    }
    private void CreatListItem()
    {
        for (int i = 0; i < EmailList.transform.childCount; ++i)
        {
            if (EmailList.GetChild(i).gameObject.name == "EmailItem(Clone)")
            {
                Destroy(EmailList.GetChild(i).gameObject);
            }
        }

        if (MailListData.Count > 5)
        {
            GridLayout.movementType = ScrollRect.MovementType.Elastic;
        }
        else
        {
            GridLayout.movementType = ScrollRect.MovementType.Clamped;
        }

        m_MailBannerLayout.cellCount = MailListData.Count;
        m_MailBannerLayout.updateCellEvent = UpdateBannerItem;
        m_MailBannerLayout.Reload();  
        RefreshEndCloseWindow();

        
    }

    //创建列表中的Item
    private void UpdateBannerItem(int index, RectTransform cell)
    {
        UI_EmailItem _bannerItemData = cell.GetComponent<UI_EmailItem>();
        if (_bannerItemData == null)
        {
            _bannerItemData = cell.gameObject.AddComponent<UI_EmailItem>();
        }

        _bannerItemData.SetEmailDate(MailListData[index]);
        _bannerItemData.SetOnClickHandle(SetItemImageLight);

        if (SelectItemMail.m_key == MailListData[index].m_key)
        {
            _bannerItemData.SetImageLight(true);
            ShowEmailInfo(MailListData[index]);
        }
        else
        {
            _bannerItemData.SetImageLight(false);
        }       
    }

    //找到对应的列表并刷新
    public void RefreshItemInfo()
    {
        if (IsReceive)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("mail_content22"), this.gameObject.transform);
            IsReceive = false;
        }

        m_MailBannerLayout.UpdateCell();
    }

    public void SetItemImageLight(MailData _MailData)
    {
        SelectItemMail = _MailData;
        m_MailBannerLayout.UpdateCell();
    }

    public bool IsHaveFuJian()
    {

        if (SelectItemMail.m_innerdropidlist.Count != 0 || SelectItemMail.m_items.Count != 0) //有附件
        {
            AwardIcon.SetActive(true);
            ReceiveButton.SetActive(true);
            RightWindow.SetActive(true);
            return true;
        }
        else
        {
            AwardIcon.SetActive(false);
            ReceiveButton.SetActive(false);
            RightWindow.SetActive(true);
            return false;
        }
    }

    /// <summary>
    /// 根据邮件数据 显示邮件内容
    /// </summary>
    /// <param name="_MailData"></param>
    public void ShowEmailInfo(MailData _MailData)
    {
        if (_MailData.m_title != null)
        {
            EmailTitleText.text = GameUtils.getString(_MailData.m_title);
        }
        if (_MailData.m_sender != null)
        {
            EmailSenderText.text = GameUtils.getString(_MailData.m_sender);
            SenderText.gameObject.SetActive(true);
        }
        else
        {
           SenderText.gameObject.SetActive(false);
        }
        if (_MailData.m_msg != null)
        {
            string _str = GameUtils.getString(_MailData.m_msg);
            if (_MailData.m_strlist.Count >= 1)
            {
                EmailInfoText.text = _str.Replace("{0}", _MailData.m_strlist[0]);
            }
            else
            {
                EmailInfoText.text = _str;
            }
        }

        if (!IsHaveFuJian())
            return;

        if (_MailData.m_isopen.ToString().Length == 1)
        {
            ShowIconItem(_MailData, 0);
            AwardIcon.SetActive(true);
            ReceiveButton.SetActive(true);
        }

        int _isReceive = int.Parse(_MailData.m_isopen.ToString().Substring(0, 1));//是否领取  0否  1是
        if (_MailData.m_isopen.ToString().Length >= 2)
        {
            if (SelectItemMail.m_innerdropidlist.Count != 0 || SelectItemMail.m_items.Count != 0) //有附件
            {
                AwardIcon.SetActive(true);
                if (_isReceive == 0)// 没有领取
                {
                    ReceiveButton.SetActive(true);
                    ShowIconItem(SelectItemMail, 0);

                }
                else //领取了
                {
                    ShowIconItem(SelectItemMail, 1);
                    ReceiveButton.SetActive(false);
                }
            }
            else
            {
                AwardIcon.SetActive(false);
                ReceiveButton.SetActive(false);
            }
        }
    }

    private void ShowIconItem(MailData _MailData, int isReceive)
    {
        for (int i = 0; i < EmailFuJianList.transform.childCount; ++i)
        {
            Destroy(EmailFuJianList.GetChild(i).gameObject);
        }

        if (_MailData.m_innerdropidlist.Count + _MailData.m_items.Count > 4)
        {
            m_GoodList.movementType = ScrollRect.MovementType.Elastic;
            m_FuJianContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            EmailFuJianList.sizeDelta = new Vector2(1139, 272);
            EmailFuJianList.anchoredPosition = new Vector2(0, EmailFuJianList.anchoredPosition.y);
        }
        else
        {
            m_GoodList.movementType = ScrollRect.MovementType.Clamped;
            m_FuJianContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            EmailFuJianList.sizeDelta = new Vector2(1139, 272);
            EmailFuJianList.anchoredPosition = new Vector2(0, EmailFuJianList.anchoredPosition.y);
        }

        for (int i = 0; i < _MailData.m_innerdropidlist.Count; i++)
        {
            //newFuJianItem = Instantiate(EmailFuJianListItem) as UI_EmailFuJianItem;
            //newFuJianItem.transform.SetParent(EmailFuJianList, false);
            //newFuJianItem.gameObject.SetActive(true);
            //newFuJianItem.Init(_MailData.m_innerdropidlist[i], isReceive); // 填充初始化数据
            GreatItem(_MailData.m_innerdropidlist[i], isReceive);
        }

        for (int i = 0; i < _MailData.m_items.Count; i++)
        {
            //newFuJianItem = Instantiate(EmailFuJianListItem) as UI_EmailFuJianItem;
            //newFuJianItem.transform.SetParent(EmailFuJianList, false);
            //newFuJianItem.gameObject.SetActive(true);
            //newFuJianItem.Init(_MailData.m_items[i], isReceive); // 填充初始化数据
            GreatItem(_MailData.m_items[i], isReceive);
        }

    }

    private void GreatItem(int id, int isReceive)
    {
        m_Cell = UniversalItemCell.GenerateItem(EmailFuJianList.transform);

        InnerdropTemplate value = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(id);
        if (value == null) return;

        int itemid = value.getObjectid();//掉落物ID
        int type = value.getObjectid() / 1000000;
        switch (type)
        {
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                if (_temp_res != null)
                {
                    m_Cell.SetCheckClaim(isReceive == 1,"");
                    m_Cell.InitByID(itemid, value.getDropnum());
                    m_Cell.SetText(GameUtils.getString(_temp_res.getName()), "", "");
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE: //符文
                {
                    ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                    if (itemTable != null)
                    {
                        m_Cell.SetCheckClaim(isReceive == 1, "");
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
                        m_Cell.SetCheckClaim(isReceive == 1, "");
                        m_Cell.InitByID(itemid, value.getDropnum());
                        m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                    }
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                {
                    HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(itemid);
                    if (hero != null)
                    {
                        m_Cell.SetCheckClaim(isReceive == 1, "");
                        m_Cell.InitByID(itemid, value.getDropnum());
                        m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                    }
                }
                break;

            default:
                break;
        }
    }

    private void GreatItem(MailItemData mailItem, int isReceive)
    {
        m_Cell = UniversalItemCell.GenerateItem(EmailFuJianList.transform);

        int type = mailItem.m_objectid / 1000000;
        int itemid = mailItem.m_objectid;//掉落物ID
        m_Cell.SetCheckClaim(isReceive == 1, "");

        switch (type)
        {
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                if (_temp_res != null)
                {                
                    m_Cell.InitByID(itemid, mailItem.m_dropnum);
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
                        m_Cell.InitByID(itemid, mailItem.m_dropnum);
                        m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                    }
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                {
                    HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(itemid);
                    if (hero != null)
                    {
                        m_Cell.InitByID(itemid, mailItem.m_dropnum);
                        m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                    }
                }
                break;

            default:
                break;
        }
    }

    private void OnClickBackBtn()
    {
        ObjectSelf.GetInstance().GetManager().m_MailList.Clear();
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
    protected void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_MailRefresh, RefreshItemInfo);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_MailReceiveListData, ReceiveListData);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_MailReceiveMore, AddMoreItem);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_MailDel, ClearSelectItemData);
        ObjectSelf.GetInstance().CurGetDataType = EM_GETMAIL_TYPE.GETNEW;
        Inst = null;
    }

    private void OnClickReceiveBtn()
    {
        int HeroNum = 0;
        int DaoJuNum = 0;
        for (int i = 0; i < SelectItemMail.m_innerdropidlist.Count; i++)
        {
            InnerdropTemplate item = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(SelectItemMail.m_innerdropidlist[i]);
            int _goid = item.getObjectid();//掉落物ID
            int itemid = item.getObjectid() / 1000000;

            switch (itemid)
            {
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO: //英雄
                    HeroNum++;
                    break;
                default:
                    DaoJuNum++;
                    break;
            }
        }

        for (int i = 0; i < SelectItemMail.m_items.Count; i++)
        {
            int itemid = SelectItemMail.m_items[i].m_objectid / 1000000;

            switch (itemid)
            {
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO: //英雄
                    HeroNum++;
                    break;
                default:
                    DaoJuNum++;
                    break;
            }
        }

        if (ObjectSelf.GetInstance().GetBagSurplus() < DaoJuNum && DaoJuNum != 0)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("mail_content15"), this.gameObject.transform);
            return;
        }

        if (ObjectSelf.GetInstance().GetHeroBagSurplus() < HeroNum && HeroNum != 0)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("mail_content14"), this.gameObject.transform);
            return;
        }

        CReceiveMail _CReceiveMail = new CReceiveMail();
        _CReceiveMail.mailkey = SelectItemMail.m_key;
        _CReceiveMail.isget = 1;
        IOControler.GetInstance().SendProtocol(_CReceiveMail);
        IsReceive = true;

    }

    public void NoMoreEmail()
    {
        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("mail_bubble1"), this.gameObject.transform);
    }

    private void OnClickDelBtn()
    {
        ObjectSelf.GetInstance().CurGetDataType = EM_GETMAIL_TYPE.GETDEL;
        ObjectSelf.GetInstance().GetManager().CheckHaveRead();
    }

    public void ClearSelectItemData()
    {      
        SelectItemMail.m_key = -100;
        SelectItemMail.m_innerdropidlist.Clear();
        SelectItemMail.m_items.Clear();
        SelectItemMail.m_sender = null;
        SelectItemMail.m_title = "";
        SelectItemMail.m_msg = "";
        SenderText.gameObject.SetActive(false);
        ReceiveListData();
    }

    public void RefreshEndCloseWindow()
    {
        AwardIcon.SetActive(false);
        ReceiveButton.SetActive(false);
        EmailTitleText.text = "";
        EmailSenderText.text = "";
        SenderText.gameObject.SetActive(false);
        EmailInfoText.text = "";
    }

}
