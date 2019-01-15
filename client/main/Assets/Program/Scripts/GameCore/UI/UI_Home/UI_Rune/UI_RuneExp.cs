using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameCore;

public class UI_RuneExp : BaseUI
{
    public static UI_RuneExp inst;
    public static string UI_ResPath = "UI_Home/UI_RuneExp_2_0";

    public int _curExp;                               // 现在的经验值
    public int _curSelectCount;                       // 现在已经选择的符文的数量
    public int _gainExp;                              // 能够得到的经验值
    public int _gainGold;                             // 能够得到的金币
    public int _runeTypeIndex = 1;                        // 符文类型的索引
    public int _curMoney;                            // 现在的金币
    public int _curRuneMoney;                        // 现在的符文值

    private Button _backBtn;					// 返回按钮
    private Text _curExpText;						// 现在的经验值
    private Text _curSelectCountText;                   // 现在已经选择的符文的数量
    private Button _dissolveBtn;                         // 熔炼按钮
    private GameObject _gainTips;                       // 熔炼后能够得到的提示
    private Text _gainExpText;                            // 熔炼后能够得到的提示数字
    private Text _gainGlodText;                            // 熔炼后能够得到的金币提示数字
    private GameObject _emptyRune;                        // 空符文
    private GameObject _runeList;                         // 整个符文列表
    private LoopLayout _runeListLayout;                 // 整个符文列表的布局
    private Text _runeTypeText;                            // 符文类型显示
    private UI_SlideBtn _slideTypeBtn;                     // 选择类型的按钮
    private Text _emptyTips;                               // 没有符文的提示
    //private Text _goldText;                                // 显示金币的文本
    private Button _empytLeftBtn;                               // 空的时候左边的 button
    private Button _empytRightBtn;                               // 空的时候右边的 button
    private Text _bagNumberText;                         // 背包的数量显示
    private GameObject _runeGameObject;                  // 符文的对象

    private Button _blueBtn;
    private Button _purpleBtn;
    private Button _greenBtn;
    private Button _redBtn;
    private Button _spectialBtn;
    private Button _allBtn;
    private GameObject _selectRuneExp;
    private GameObject _selectRuneItem;

    public List<RuneTempData> _curRuneList = new List<RuneTempData>();                // 现在的符文列表

    bool isAdd = false;
    protected List<SelectRune> _selectRuneList = new List<SelectRune>();

    public GameObject m_Caption;

    public override void InitUIData()
    {
        inst = this;

        _backBtn = selfTransform.FindChild("TopPanel/BackBtn").GetComponent<Button>();
        _backBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
        _curExpText = selfTransform.FindChild("TopPanel/MoneyBarUI/Image/CurExp").GetComponent<Text>();
        _curSelectCountText = selfTransform.FindChild("NumberTips/CurCount").GetComponent<Text>();
        _dissolveBtn = selfTransform.FindChild("DissolveBtn").GetComponent<Button>();
        _dissolveBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickDissolve));
        _gainTips = selfTransform.FindChild("GainTips").gameObject;
        _gainExpText = selfTransform.FindChild("GainTips/Exp/GainExp").GetComponent<Text>();
        _gainGlodText = selfTransform.FindChild("GainTips/Gold/Text").GetComponent<Text>();
        _emptyRune = selfTransform.FindChild("EmptyRune").gameObject;
        _runeList = selfTransform.FindChild("RuneList").gameObject;
        _runeListLayout = selfTransform.FindChild("RuneList/RuneList/ListLayout").GetComponent<LoopLayout>();
        _runeTypeText = selfTransform.FindChild("RuneList/SortObj/MainBagBtn/Text").GetComponent<Text>();
        _slideTypeBtn = selfTransform.FindChild("RuneList/SortObj/MainBagBtn").GetComponent<UI_SlideBtn>();
        _blueBtn = selfTransform.FindChild("RuneList/SortObj/BlueBtn").GetComponent<Button>();
        _blueBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBule));
        _purpleBtn = selfTransform.FindChild("RuneList/SortObj/PurpleBtn").GetComponent<Button>();
        _purpleBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPurple));
        _greenBtn = selfTransform.FindChild("RuneList/SortObj/GreenBtn").GetComponent<Button>();
        _greenBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGreen));
        _redBtn = selfTransform.FindChild("RuneList/SortObj/RedBtn").GetComponent<Button>();
        _redBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRed));
        _spectialBtn = selfTransform.FindChild("RuneList/SortObj/SpectialBtn").GetComponent<Button>();
        _spectialBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSpectial));
        _allBtn = selfTransform.FindChild("RuneList/SortObj/AllBtn").GetComponent<Button>();
        _allBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAll));
        _emptyTips = selfTransform.FindChild("EmptyRune/Text").GetComponent<Text>();
        _emptyTips.text = GameUtils.getString("runemelt_content1");
       // _goldText = selfTransform.FindChild("UI_BG_Top/Gold/Text").GetComponent<Text>();
        _empytLeftBtn = selfTransform.FindChild("EmptyRune/LeftBtn").GetComponent<Button>();
        _empytRightBtn = selfTransform.FindChild("EmptyRune/RightBtn").GetComponent<Button>();
        _empytLeftBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLeftBtn));
        _empytRightBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRightBtn));
        _bagNumberText = selfTransform.FindChild("RuneList/SortObj/NumberNoBg/Number").GetComponent<Text>();
        _runeGameObject = selfTransform.FindChild("RuneList/RuneList").gameObject;
        _selectRuneExp = selfTransform.FindChild("SelectRuneExp").gameObject;
        _selectRuneItem = selfTransform.FindChild("SelectRuneItem").gameObject;
        m_Caption = selfTransform.FindChild("caption").gameObject;
        HomeControler.Inst.PushFunly(6, 97);
        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.AwakeUp(m_Caption.transform);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        // 更新数据
        UpdateRuneType(_runeTypeIndex);

        // 更新显示
        if (_curRuneList.Count <= 0)
        {
            _emptyRune.SetActive(true);
            _runeGameObject.SetActive(false);
        }

        _gainTips.SetActive(false);

        // 初始化数据
        _curRuneMoney = ObjectSelf.GetInstance().RuneMoney;
        _curExpText.text = ObjectSelf.GetInstance().RuneMoney.ToString();

        //_goldText.text = ObjectSelf.GetInstance().Money.ToString();
        _curMoney = (int)ObjectSelf.GetInstance().Money;
    }

    void UpdateRuneItem(int index, RectTransform cell)
    {
        if (index < _curRuneList.Count)
        {
            RuneItem item = cell.gameObject.GetComponent<RuneItem>();
            if (item == null)
            {
                item = cell.gameObject.AddComponent<RuneItem>();
            }

            item.tableID = _curRuneList[index].item.GetItemTableID();
            item.guid = _curRuneList[index].item.GetItemGuid();
            item.index = index;
            item.ShowInfo();

            if (_selectRuneList.Count == 12)
            {
                if (isAdd)
                {
                    if (_curRuneList[index].isSelect == false)
                    {
                        item.SetSelectBtnState(false);
                        item.updateSelectBtn(true);
                    }
                    else
                    {
                        item.SetSelectBtnState(true);
                        item.updateSelectBtn(false);
                    }
                }
                else
                {
                    if (_curRuneList[index].isSelect)
                    {
                        item.SetSelectBtnState(true);
                        item.updateSelectBtn(false);
                    }
                    else
                    {
                        item.SetSelectBtnState(false);
                        item.updateSelectBtn(false);
                    }
                }
            }
            else
            {
                item.SetSelectBtnState(_curRuneList[index].isSelect);
                item.updateSelectBtn(false);
            }
        }
    }

    public void UpdateRuneType(int itemTypeID)
    {
        switch (itemTypeID)
        {
            case 1:
                _runeTypeText.text = GameUtils.getString("hero_rune_content14");
                SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL);
                break;
            case 2:
                _runeTypeText.text = GameUtils.getString("hero_rune_content15");
                SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL);
                break;
            case 3:
                _runeTypeText.text = GameUtils.getString("hero_rune_content16");
                SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED);
                break;
            case 4:
                _runeTypeText.text = GameUtils.getString("hero_rune_content17");
                SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN);
                break;
            case 5:
                _runeTypeText.text = GameUtils.getString("hero_rune_content18");
                SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE);
                break;
            case 6:
                _runeTypeText.text = GameUtils.getString("hero_rune_content19");
                SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE);
                break;
            default:
                break;
        }
    }

    public void SelectRune(EM_SORT_RUNE_ITEM nType)
    {
        _curRuneList.Clear();
        List<BaseItem> runeList = new List<BaseItem>();
        switch (nType)
        {
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL:
                runeList = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL:
                runeList = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED:
                runeList = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN:
                runeList = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE:
                runeList = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE);
                break;
            case EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE:
                runeList = ObjectSelf.GetInstance().CommonItemContainer.SortRuneItemByType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE);
                break;
            default:
                break;
        }

        for (int i = 0; i < runeList.Count; ++i)
        {
            RuneTempData rune = new RuneTempData();
            rune.item = runeList[i];
            rune.isSelect = false;
            _curRuneList.Add(rune);
        }

        RelodRune();
    }

    public void RelodRune()
    {
        // 清理显示数据
        _selectRuneList.Clear();
        for (int i = _selectRuneItem.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_selectRuneItem.transform.GetChild(i).gameObject);
        }

        _emptyRune.SetActive(false);
        _runeGameObject.SetActive(true);

        UpdateShow(0);

        // 排序
        SortRune();

        if (_curRuneList.Count <= 0)
        {
            _emptyRune.SetActive(true);
            _runeGameObject.SetActive(false);
        }
        else
        {
            _emptyRune.SetActive(false);
            _runeGameObject.SetActive(true);

            // 重新加载数据
            // 重新加载数据
            int count = _curRuneList.Count;
            if (count <= 9)
            {
                _runeListLayout.cellCount = count;
                _runeListLayout.emptyCellCount = 9 - count;
            }
            else
            {
                if (count % _runeListLayout.columns == 0)
                {
                    _runeListLayout.cellCount = count;
                    _runeListLayout.emptyCellCount = 0;
                }
                else
                {
                    _runeListLayout.cellCount = count;
                    _runeListLayout.emptyCellCount = (count / _runeListLayout.columns + 1) * _runeListLayout.columns - count;
                }
            }
            _runeListLayout.updateCellEvent = UpdateRuneItem;
            _runeListLayout.Reload();
        }

        UpdateBagItem();
    }

    // 更新背包数量显示
    void UpdateBagItem()
    {
        var player = ObjectSelf.GetInstance();
        if (player.CommonItemContainer.GetBagItemSum() <= player.CommonItemContainer.GetBagItemSizeMax())
        {
            _bagNumberText.text = "<color=#f3863a>" + _curRuneList.Count + "</color>/" + player.CommonItemContainer.GetBagItemSizeMax();
        }
        else
        {
            _bagNumberText.text = "<color=red>" + _curRuneList.Count + "</color>/" + player.CommonItemContainer.GetBagItemSizeMax();
        }
    }

    void SortRune()
    {
        List<BaseItem> norRuneList = new List<BaseItem>();
        List<BaseItem> equipRuneList = new List<BaseItem>();

        // 筛选是否已经装备
        for (int i = 0; i < _curRuneList.Count; ++i)
        {
            int tableID = _curRuneList[i].item.GetItemTableID();
            ItemEquip equip = (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, _curRuneList[i].item.GetItemGuid());

            if (equip.IsEquip())
            {
                equipRuneList.Add(_curRuneList[i].item);
            }
            else
            {
                norRuneList.Add(_curRuneList[i].item);
            }
        }

        // 进行排序
        SortItem(ref norRuneList);
        SortItem(ref equipRuneList);

        // 重新读取数据
        _curRuneList.Clear();
        for (int i = 0; i < norRuneList.Count; ++i)
        {
            ItemTemplate item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(norRuneList[i].GetItemTableID());
            RuneTempData rune = new RuneTempData();
            rune.item = norRuneList[i];
            rune.isSelect = false;
            _curRuneList.Add(rune);
        }

        for (int i = 0; i < equipRuneList.Count; ++i)
        {
            ItemTemplate item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(equipRuneList[i].GetItemTableID());
            RuneTempData rune = new RuneTempData();
            rune.item = equipRuneList[i];
            rune.isSelect = false;
            _curRuneList.Add(rune);
        }
    }

    // 先进行排序
    void SortItem(ref List<BaseItem> runeList)
    {
        for (int i = 0; i < runeList.Count - 1; ++i)
        {
            BaseItem item = runeList[i];
            for (int j = i; j < runeList.Count; ++j)
            {
                ItemTemplate temp = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(runeList[i].GetItemTableID());
                ItemTemplate buffer = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(runeList[j].GetItemTableID());

                if (buffer.getRune_smelt() > temp.getRune_smelt())
                {
                    item = runeList[j];
                    runeList[j] = runeList[i];
                    runeList[i] = item;
                }
            }
        }
    }

    // 添加一个选择的符文
    public bool AddSelectRune(int index, int tableID, int smelt, int money, X_GUID guid)
    {
        // 判断是否超出了范围
        if (_selectRuneList.Count >= 12)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("heromelt_bubble2"), selfTransform.transform.parent);
            return false;
        }

        GameObject cell = Instantiate(Resources.Load("UI/Prefabs/UI_Rune/UI_RuneIcon")) as GameObject;
        SelectRune select = cell.AddComponent<SelectRune>();
        select.index = index;
        select.tableID = tableID;
        select.guid = _curRuneList[index].item.GetItemGuid();
        select.ShowInfo();
        cell.transform.parent = _selectRuneItem.transform;
        cell.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        _selectRuneList.Add(select);
        UpdateSelectItem();

        _gainExp += smelt;
        _gainGold += money;
        UpdateShow(_selectRuneList.Count);

        for (int i = 0; i < _curRuneList.Count; ++i)
        {
            if (_curRuneList[i].item.GetItemGuid() == guid)
            {
                _curRuneList[i].isSelect = true;
                break;
            }
        }

        // 更新灰图显示
        if (_selectRuneList.Count == 12)
        {
            isAdd = true;
        }
        _runeListLayout.UpdateCell();
        return true;
    }

    // 移除一个选择的符文
    public void RemoveSelectRune(int index, int tableID, int smelt, int money, X_GUID guid)
    {
        for (int i = 0; i < _selectRuneList.Count; ++i)
        {
            SelectRune item = _selectRuneList[i];
            if (item.index == index)
            {
                _selectRuneList.Remove(item);
                DestroyImmediate(item.gameObject);
                break;
            }
        }

        UpdateSelectItem();
        _gainExp -= smelt;
        _gainGold -= money;
        UpdateShow(_selectRuneList.Count);

        for (int i = 0; i < _curRuneList.Count; ++i)
        {
            if (_curRuneList[i].item.GetItemGuid() == guid)
            {
                _curRuneList[i].isSelect = false;
                break;
            }
        }

        // 更新灰图显示
        if (_selectRuneList.Count == 12)
        {
            isAdd = false;
        }

        _runeListLayout.UpdateCell();
    }

    // 更新界面数据
    void UpdateShow(int count)
    {
        if (count > 0)
            _gainTips.SetActive(true);
        else
            _gainTips.SetActive(false);

        _curSelectCount = count;
        _curSelectCountText.text = _curSelectCount.ToString();

        _gainExpText.text = _gainExp.ToString();
        _gainGlodText.text = _gainGold.ToString();
    }

    void UpdateSelectItem()
    {
        for (int i = 0; i < _selectRuneList.Count; ++i)
        {
            SelectRune item = _selectRuneList[i];
            RectTransform rect = item.GetComponent<RectTransform>();
            rect.anchoredPosition3D = _selectRuneExp.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition3D;
        }
    }


    // 熔炼成功，重新加载界面
    public void SmeltSuccess()
    {
        // 加载提示信息
        int money = (int)(ObjectSelf.GetInstance().Money - _curMoney);
        int runeMonty = ObjectSelf.GetInstance().RuneMoney - _curRuneMoney;

        if (money <= 0)
        {
            string str = GameUtils.getString("runemelt_bubble3");
            string text = string.Format(str, _gainExp);
            RichText rich = RichText.GetRichText(text);
            InterfaceControler.GetInst().AddMsgBox(rich.transform);
        }
        else
        {
            string str = GameUtils.getString("runemelt_bubble2");
            string text = string.Format(str, _gainExp, money);
            RichText rich = RichText.GetRichText(text);
            InterfaceControler.GetInst().AddMsgBox(rich.transform);
        }

        _curRuneMoney = ObjectSelf.GetInstance().RuneMoney;
        _curMoney = (int)ObjectSelf.GetInstance().Money;

        // 更新显示数据
        _gainExp = 0;
        _gainGold = 0;
        UpdateShow(0);

        _curRuneMoney = ObjectSelf.GetInstance().RuneMoney;
        _curExpText.text = ObjectSelf.GetInstance().RuneMoney.ToString();

        //_goldText.text = ObjectSelf.GetInstance().Money.ToString();
        _curMoney = (int)ObjectSelf.GetInstance().Money;

        if (_curRuneList.Count <= 0)
        {
            ClearSelectItem();
            _emptyRune.SetActive(true);
            _runeGameObject.SetActive(false);
            _gainTips.SetActive(false);
        }
        else
        {
            UpdateRuneType(UI_RuneExp.inst._runeTypeIndex);
        }


    }

    void SendMessage()
    {
        CSplitEquip proto = new CSplitEquip();
        LinkedList<int> keys = new LinkedList<int>();
        for (int i = 0; i < _selectRuneList.Count; ++i)
        {
            int index = _selectRuneList[i].index;
            int guid = (int)(_curRuneList[index].item.GetItemGuid().GUID_value);
            keys.AddLast(guid);
        }
        proto.equipkeylist = keys;
        IOControler.GetInstance().SendProtocol(proto);
    }

    void ClearSelectItem()
    {
        _selectRuneList.Clear();
        for (int i = _selectRuneItem.transform.childCount; i > 0; i--)
        {
            DestroyImmediate(_selectRuneItem.transform.GetChild(i));
        }
    }

    ////////////////////////// 按钮回调 //////////////////
    public void OnClickBule()
    {
        _slideTypeBtn.OnClose();
        if (_runeTypeIndex == 6)
        {
            return;
        }
        ClearSelectItem();
        _runeTypeIndex = 6;
        UpdateRuneType(_runeTypeIndex);
    }
    public void OnClickPurple()
    {
        _slideTypeBtn.OnClose();
        if (_runeTypeIndex == 5)
        {
            return;
        }
        _runeTypeIndex = 5;
        UpdateRuneType(_runeTypeIndex);
    }
    public void OnClickGreen()
    {
        _slideTypeBtn.OnClose();
        if (_runeTypeIndex == 4)
        {
            return;
        }
        _runeTypeIndex = 4;
        UpdateRuneType(_runeTypeIndex);
    }
    public void OnClickRed()
    {
        _slideTypeBtn.OnClose();
        if (_runeTypeIndex == 3)
        {
            return;
        }
        _runeTypeIndex = 3;
        UpdateRuneType(_runeTypeIndex);
    }
    public void OnClickSpectial()
    {
        _slideTypeBtn.OnClose();
        if (_runeTypeIndex == 2)
        {
            return;
        }
        _runeTypeIndex = 2;
        UpdateRuneType(_runeTypeIndex);
    }
    public void OnClickAll()
    {
        _slideTypeBtn.OnClose();
        if (_runeTypeIndex == 1)
        {
            return;
        }
        _runeTypeIndex = 1;
        UpdateRuneType(_runeTypeIndex);
    }

    // 返回按钮
    private void OnClickBackBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    // 熔炼按钮回调
    private void OnClickDissolve()
    {
        // 没有选择符文
        if (_selectRuneList.Count <= 0)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("runemelt_bubble4"), selfTransform.transform.parent);
            return;
        }

        // 检测高品质和是否已经装配
        for (int i = 0; i < _selectRuneList.Count; i++)
        {
            SelectRune item = _selectRuneList[i];
            if (item._rune.getRune_quality() >= 5 || item._data.IsEquip())
            {
                // 弹窗提示
                UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
                box.SetIsNeedDescription(false);
                box.SetDescription_text(GameUtils.getString("runemelt_window1"));
                box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
                box.SetLeftClick(OnClickConfirmBtn);

                box.SetRightBtn_text(GameUtils.getString("common_button_cancel"));
                box.SetRightClick(OnClickCancelBtn);

                return;
            }

        }

        SendMessage();
    }

    void OnClickLeftBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
        UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
        UI_Recruit.inst.OpenRelicBtn();
    }

    void OnClickRightBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
        //UI_HomeControler.Inst.AddUI(UI_SelectFightArea.UI_ResPath);
        UI_HomeControler.Inst.AddUI(UI_SelectLevelMgrNew.UI_ResPath);
    }

    void OnClickConfirmBtn()
    {
        SendMessage();
        UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
    }

    void OnClickCancelBtn()
    {
        // TODO...
        UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
    }

    public class RuneTempData
    {
        public bool isSelect = false;
        public BaseItem item;
    }

    void OnDestroy()
    {
        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.Release(m_Caption.transform);
    }
}
