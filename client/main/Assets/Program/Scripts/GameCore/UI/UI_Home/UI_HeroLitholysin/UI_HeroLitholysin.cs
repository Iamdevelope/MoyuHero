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

public class UI_HeroLitholysin : BaseUI
{
	public static UI_HeroLitholysin inst;
	public static string UI_ResPath = "UI_Home/UI_HeroLitholysin_2_0";

	public int _curSelectCount;                    // 现在选择的数量
	public int _gainCount = 0;                 // 已经选择的总的能够得到的熔灵值

	private LoopLayout _curHeroLayout;        // 现在英雄的布局
	private Button _backBtn;                    // 返回按钮
	private Text _curSelectCountText;              // 现在已经选择的英雄数量
	private Button _lithBtn;                   // 熔灵按钮
	private Text _gainNumber;                  // 能够得到的熔灵值数量
	private Text _curLithvalue;                // 现在的熔灵值
	private UI_SlideBtn _sortSlide;             //滑动组件
	private Text _teamSortType;                 //手动排序类型显示
	private int _sortType = 3;              // 1为品质排序 2为等级排序 3 为默认排序
	private GameObject _heroknapsack;   // 英雄背包的整個對象
	private GameObject _emptyHero;      // 整個空英雄對象
	private Text _emptyTips;            // 沒有英雄的提示文字
	private GameObject _gainTips;       // 下方提示能夠得到多少經驗
	private Button _empytLeftBtn;                               // 空的时候左边的 button
	private Button _empytRightBtn;                               // 空的时候右边的 button
	private Text _bagNumberText;                         // 背包的数量显示
	private Text _dissolveText;                            // 熔灵文字
	private GameObject _heroObject;                // 英雄的对象
    private GameObject _selectHeroExp;
    private GameObject _selectHeroItem;

    bool isAdd = false;

    public List<HeroTempData> _heroList = new List<HeroTempData>();
	public List<X_GUID> _heroRune = new List<X_GUID>(); // 英雄已经装配的符文
    public List<SelectHeroList> _selectHeroList = new List<SelectHeroList>();
    public GameObject m_Caption;

	public override void InitUIData()
	{
		inst = this;
		_heroknapsack = selfTransform.FindChild("HeroKnapsack").gameObject;
		_emptyHero = selfTransform.FindChild("EmptyHero").gameObject;
		_emptyTips = selfTransform.FindChild("EmptyHero/Text").GetComponent<Text>();
		_backBtn = selfTransform.FindChild("TopPanel/BackBtn").GetComponent<Button>();
		_backBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
		_lithBtn = selfTransform.FindChild("DissolveBtn").GetComponent<Button>();
		_lithBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLitholysin));
		_gainTips = selfTransform.FindChild("GiveTips").gameObject;
		_gainNumber = selfTransform.FindChild("GiveTips/Number").GetComponent<Text>();
		_gainNumber.text = "0";
		_curSelectCountText = selfTransform.FindChild("NumberTips/CurCount").GetComponent<Text>();
        _teamSortType = selfTransform.FindChild("HeroKnapsack/SortObj/MainBagBtn/Text").GetComponent<Text>();
        _sortSlide = selfTransform.FindChild("HeroKnapsack/SortObj/MainBagBtn").GetComponent<UI_SlideBtn>();
        selfTransform.FindChild("HeroKnapsack/SortObj/QualitysortBtn").GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(OnQualitysortBtn));
        selfTransform.FindChild("HeroKnapsack/SortObj/LevelsortBtn").GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(OnLevelsortBtn));
		//selfTransform.FindChild("HeroKnapsack/DefaultBtn").GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(OnDefaultBtn));
        selfTransform.FindChild("HeroKnapsack/SortObj/QualitysortBtn/Text").GetComponent<Text>().text = GameUtils.getString("heromelt_button2");
        selfTransform.FindChild("HeroKnapsack/SortObj/LevelsortBtn/Text").GetComponent<Text>().text = GameUtils.getString("heromelt_button3");
        //selfTransform.FindChild("HeroKnapsack/DefaultBtn/Text").GetComponent<Text>().text = GameUtils.getString("heromelt_button10");
        _curHeroLayout = selfTransform.FindChild("HeroKnapsack/HeroList/ListLayOut").GetComponent<LoopLayout>();
		_curLithvalue = selfTransform.FindChild("TopPanel/Text").GetComponent<Text>();
		_curLithvalue.text = ObjectSelf.GetInstance().ExpFruit.ToString();
		_empytLeftBtn = selfTransform.FindChild("EmptyHero/LeftBtn").GetComponent<Button>();
		_empytRightBtn = selfTransform.FindChild("EmptyHero/RightBtn").GetComponent<Button>();
		_empytLeftBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLeftBtn));
		_empytRightBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRightBtn));
		_emptyTips.text = GameUtils.getString("heromelt_content3");
		_bagNumberText = selfTransform.FindChild("HeroKnapsack/SortObj/NumberNoBg/Number").GetComponent<Text>();
		_dissolveText = selfTransform.FindChild("DissolveBtn/Text").GetComponent<Text>();
		_dissolveText.text = GameUtils.getString("heromelt_button1");
		_heroObject = selfTransform.FindChild("HeroKnapsack/HeroList").gameObject;
        _selectHeroExp = selfTransform.FindChild("SelectHeroExp").gameObject;
        _selectHeroItem = selfTransform.FindChild("SelectHeroItem").gameObject;
        m_Caption = selfTransform.FindChild("caption").gameObject;
        HomeControler.Inst.PushFunly(5, 101);

        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.AwakeUp(m_Caption.transform);
	}

	public override void InitUIView()
	{
		base.InitUIView();

		_teamSortType.text = GameUtils.getString("heromelt_button10");
		_gainTips.SetActive(false);

		ReloadHero();
	}

    //void UpdateHeroItem(int index, RectTransform cell)
    //{
    //    if(index < _heroList.Count)
    //    {
    //        UI_HeroListItem uiIt = cell.gameObject.GetComponent<UI_HeroListItem>();
    //        if (uiIt == null)
    //        {
    //            cell.gameObject.AddComponent<Button>();
    //            uiIt = cell.gameObject.AddComponent<UI_HeroListItem>();
    //            uiIt.AddSelectBtn();
    //        }

    //        ObjectCard objHero = _heroList[index].card;
    //        uiIt.index = index;
    //        uiIt.m_id = index;
    //        uiIt.tableId = objHero.GetHeroData().TableID;

    //        uiIt.Initialize(objHero);            
    //        uiIt.ShowStar();
    //        uiIt.ShowInfo();

    //        // 是否探险中
    //        if (ObjectSelf.GetInstance().IsInExploring(objHero.GetHeroData().GUID))
    //        {
    //            uiIt.SetExplore(true);
    //        }
    //        else
    //        {
    //            uiIt.SetExplore(false);

    //            // 是否已上阵
    //            if (isUpFront(objHero))
    //            {
    //                uiIt.ShowUpFront(true);
    //            }
    //            else
    //            {
    //                uiIt.ShowUpFront(false);
    //            }
    //        }
            

    //        if (_selectHeroList.Count == 12)
    //        {
    //            if(isAdd)
    //            {
    //                if(_heroList[index].isSelect)
    //                {
    //                    uiIt.SetSelectState(true);
    //                    uiIt.updateSelectBtnState(false);
    //                }
    //                else
    //                {
    //                    uiIt.SetSelectState(false);
    //                    uiIt.updateSelectBtnState(true);
    //                }
    //            }
    //            else
    //            {
    //                if (_heroList[index].isSelect)
    //                {
    //                    uiIt.SetSelectState(true);
    //                    uiIt.updateSelectBtnState(false);
    //                }
    //                else
    //                {
    //                    uiIt.SetSelectState(false);
    //                    uiIt.updateSelectBtnState(false);
    //                }                    
    //            }
    //        }
    //        else
    //        {
    //            uiIt.SetSelectState(_heroList[index].isSelect);
    //            uiIt.updateSelectBtnState(false);
    //        }
    //    }
    //}

	private void SetSortType(int type)
	{
		_sortType = type;
		if (_sortType == 1)
		{
			_teamSortType.text = GameUtils.getString("hero_info_sort_quality");
		}
		else if (_sortType == 2)
		{
			_teamSortType.text = GameUtils.getString("hero_info_sort_level");
		}
		else
		{
			_teamSortType.text = GameUtils.getString("heromelt_button10");
		}
	}

	void SendMessage()
	{
		_heroRune.Clear();

		LinkedList<int> heroKey = new LinkedList<int>();
		for (int i = 0; i < _selectHeroList.Count; ++i)
		{
			SelectHeroList item = _selectHeroList[i];
			heroKey.AddLast((int)item.objHero.GetHeroData().GUID.GUID_value);

			for (int j = 0; j < item.objHero.GetHeroData().GetEquipItems().Count; ++j)
			{
				if (item.objHero.GetHeroData().GetEquipItems()[j].IsValid())
					_heroRune.Add(item.objHero.GetHeroData().GetEquipItems()[j]);
			}
		}

		CSplitHero proto = new CSplitHero();
		proto.herokeylist = heroKey;
		IOControler.GetInstance().SendProtocol(proto);
	}

    void GetHeroTempData()
    {
        _heroList.Clear();
        List<ObjectCard> cards = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList();
        for (int i = 0; i < cards.Count; ++i)
        {
            HeroTempData temp = new HeroTempData();
            temp.card = cards[i];
            temp.isGrey = false;
            temp.isSelect = false;
            _heroList.Add(temp);
        }
    }

	public void ReloadHero()
	{
        //// 清理显示数据
        //ClearSelectItem();
        //_heroObject.SetActive(true);
        //UpdateShow(0);

        //// 排序
        //SortHero(_sortType);

        //if (_heroList.Count <= 0)
        //{
        //    _emptyHero.SetActive(true);
        //    _heroObject.SetActive(false);
        //}
        //else
        //{
        //    _emptyHero.SetActive(false);
        //    _heroObject.SetActive(true);

        //    // 重新加载数据
        //    int count = _heroList.Count;
        //    if(count <= 9)
        //    {
        //        _curHeroLayout.cellCount = count;
        //        _curHeroLayout.emptyCellCount = 9 - count;
        //    }
        //    else
        //    {
        //        if(count % _curHeroLayout.columns == 0)
        //        {
        //            _curHeroLayout.cellCount = count;
        //            _curHeroLayout.emptyCellCount = 0;
        //        }
        //        else
        //        {
        //            _curHeroLayout.cellCount = count;
        //            _curHeroLayout.emptyCellCount = (count / _curHeroLayout.columns + 1) * _curHeroLayout.columns - count;
        //        }
        //    }
        //    _curHeroLayout.updateCellEvent = UpdateHeroItem;
        //    _curHeroLayout.Reload();
        //}

        //UpdateBagItem();
	}

	// 更新背包数量显示
	void UpdateBagItem()
	{
		var player = ObjectSelf.GetInstance();
		if (player.HeroContainerBag.GetHeroBagSizeMax() <= player.HeroContainerBag.GetHeroBagSizeMax())
		{
			_bagNumberText.text = "<color=#f3863a>" + _heroList.Count + "</color>/" + ObjectSelf.GetInstance().HeroContainerBag.GetHeroBagSizeMax();
		}
		else
		{
			_bagNumberText.text = "<color=red>" + _heroList.Count + "</color>/" + ObjectSelf.GetInstance().HeroContainerBag.GetHeroBagSizeMax();
		}
	}

    void UpdateSelectItem()
    {
        for (int i = 0; i < _selectHeroList.Count; ++i)
        {
            SelectHeroList item = _selectHeroList[i];
            RectTransform rect = item.GetComponent<RectTransform>();
            rect.anchoredPosition3D = _selectHeroExp.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition3D;
        }
    }

    void ClearSelectItem()
    {
        _selectHeroList.Clear();
        for (int i = _selectHeroItem.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_selectHeroItem.transform.GetChild(i).gameObject);
        }
    }


	void SortHero(int type)
	{
		List<ObjectCard> norHero = new List<ObjectCard>();   // 普通英雄
        List<ObjectCard> upHero = new List<ObjectCard>();    // 上阵英雄
        List<ObjectCard> exHero = new List<ObjectCard>();    // 探险英雄

        List<ObjectCard> heroList = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList();
        for (int i = 0; i < heroList.Count; ++i)
		{
            if (ObjectSelf.GetInstance().IsInExploring(heroList[i].GetHeroData().GUID))
            {
                exHero.Add(heroList[i]);
                continue;
            }
            if (isUpFront(heroList[i]))
			{
                upHero.Add(heroList[i]);
			}
			else
			{
                norHero.Add(heroList[i]);
			}
		}

		if (type == 1)
		{
			SortHeroWithQuailty(ref norHero);
			SortHeroWithQuailty(ref upHero);
            SortHeroWithQuailty(ref exHero);
		}
		else if (type == 2)
		{
			SortHeroWithLevel(ref norHero);
			SortHeroWithLevel(ref upHero);
            SortHeroWithLevel(ref exHero);
		}
		else if (type == 3)
		{
			SortHeroWithDefault(ref norHero);
			SortHeroWithDefault(ref upHero);
            SortHeroWithDefault(ref exHero);
		}
		else
		{

		}

		_heroList.Clear();
		for (int i = 0; i < norHero.Count; ++i)
		{
            HeroTempData temp = new HeroTempData();
            temp.card = norHero[i];
            _heroList.Add(temp);
		}
		for (int i = 0; i < upHero.Count; ++i)
		{
            HeroTempData temp = new HeroTempData();
            temp.card = upHero[i];
            _heroList.Add(temp);
		}
        for(int i =0; i< exHero.Count; ++i)
        {
            HeroTempData temp = new HeroTempData();
            temp.card = exHero[i];
            _heroList.Add(temp);
        }

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

		_gainNumber.text = _gainCount.ToString();
	}

	////////////////////////  按钮回调  ///////////////////////
	//品质排序
	private void OnQualitysortBtn()
	{
		_sortSlide.OnClose();
		if (_sortType != 1)
		{
			SetSortType(1);
			ReloadHero();
		}
	}
	//等级排序
	private void OnLevelsortBtn()
	{
		_sortSlide.OnClose();
		if (_sortType != 2)
		{
			SetSortType(2);
			ReloadHero();
		}
	}

	//默认排序
	private void OnDefaultBtn()
	{
		_sortSlide.OnClose();
		if (_sortType != 3)
		{
			SetSortType(3);
			ReloadHero();
		}
	}

	//返回按钮 
	private void OnClickBackBtn()
	{
		UI_HomeControler.Inst.ReMoveUI(gameObject);
	}

	// 熔灵按钮回调
	private void OnClickLitholysin()
	{
		// 是否已经选择物品
		if (_selectHeroList.Count <= 0)
		{
			InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("heromelt_bubble4"), selfTransform.transform.parent);
			return;
		}

		// 是否有高品质英雄
		for (int i = 0; i < _selectHeroList.Count; i++)
		{
            SelectHeroList item = _selectHeroList[i];
			if (item.m_heroStar >= 5 || isUpFront(item.objHero))
			{
				// 弹窗提示
				UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
				box.SetIsNeedDescription(false);
				box.SetDescription_text(GameUtils.getString("heromelt_window1"));
				box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
				box.SetLeftClick(OnClickConfirmBtn);
				box.SetRightBtn_text(GameUtils.getString("common_button_cancel"));
				box.SetRightClick(OnClickCancelBtn);
				return;
			}
		}

		SendMessage();
	}

	// 添加一个选择的英雄
	public bool AddSelectHero(int index, ObjectCard objHero, int lithCount)
	{
		GameObject cell = Instantiate(Resources.Load("UI/Prefabs/UI_Home/SelectHeroInfo")) as GameObject;
		SelectHeroList select = cell.AddComponent<SelectHeroList>();
		select.index = index;
		select.tableId = objHero.GetHeroData().TableID;
		select.Initialize(objHero);
        cell.transform.parent = _selectHeroItem.transform;
        cell.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        _selectHeroList.Add(select);
        UpdateSelectItem();

		_gainCount += lithCount;
		UpdateShow(_selectHeroList.Count);

        for (int i = 0; i < _heroList.Count; ++i)
        {
            if(_heroList[i].card == objHero)
            {
                _heroList[i].isSelect = true;
                break;
            }
        }

        // 更新灰图显示
        if (_selectHeroList.Count == 12)
        {
            isAdd = true;
        }
        _curHeroLayout.UpdateCell();		
		return true;
	}

	// 移除已经选择的英雄
	public void RemoveSelectHero(int index, ObjectCard objHero, int lithCount)
	{
        for (int i = 0; i < _selectHeroList.Count; ++i)
        {
            SelectHeroList item = _selectHeroList[i];
            if (item.index == index)
            {
                _selectHeroList.Remove(item);
                DestroyImmediate(item.gameObject);
                break;
            }
        }
        UpdateSelectItem();
		_gainCount -= lithCount;
		UpdateShow(_selectHeroList.Count);

        for (int i = 0; i < _heroList.Count; ++i)
        {
            if (_heroList[i].card == objHero)
            {
                _heroList[i].isSelect = false;
                break;
            }
        }

        // 更新灰图显示
        if (_selectHeroList.Count == 12)
        {
            isAdd = false;
        }

        _curHeroLayout.UpdateCell();		
	}

	// 熔炼成功，重新加载界面
	public void SmeltSuccess()
	{
		// 泡泡提示
		string str = GameUtils.getString("runemelt_bubble3");
		string text = string.Format(str, _gainCount);
        RichText rich = RichText.GetRichText(text);
        InterfaceControler.GetInst().AddMsgBox(rich.transform);
		_curSelectCountText.text = "0";

		// 提示将符文放入背包
		if (_heroRune.Count > 0)
			InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("heromelt_bubble1"), selfTransform.transform.parent);

		// 清理数据
        ClearSelectItem();
		_curLithvalue.text = ObjectSelf.GetInstance().ExpFruit.ToString();
		_gainCount = 0;
		UpdateShow(0);

		ReloadHero();
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

	// 
	void OnClickLeftBtn()
	{
        UI_HomeControler.Inst.ReMoveUI(gameObject);
        UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
	}

	// 
	void OnClickRightBtn()
	{
		UI_HomeControler.Inst.ReMoveUI(gameObject);
        UI_HomeControler.Inst.AddUI(UI_SelectLevelMgrNew.UI_ResPath);
        //UI_HomeControler.Inst.AddUI(UI_SelectFightArea.UI_ResPath);
	}

    /// <summary>
    ///  是否上阵
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
	bool isUpFront(ObjectCard card)
	{
		X_GUID guid = card.GetHeroData().GUID;
		int index = 0;
		if (ObjectSelf.GetInstance().Teams.IsHeroInTeam(guid, ref index))
		{
			return true;
		}
		return false;
	}

	void SortHeroWithDefault(ref List<ObjectCard> heroList)
	{
		for (int i = 0; i < heroList.Count - 1; ++i)
		{
			ObjectCard item = heroList[i];
			for (int j = i; j < heroList.Count; ++j)
			{
				int count1 = GetLithCount(heroList[i]);
				int count2 = GetLithCount(heroList[j]);
				if (count2 > count1)
				{
					item = heroList[j];
					heroList[j] = heroList[i];
					heroList[i] = item;
				}
				else if (count2 == count1)
				{
					HeroTemplate hero3 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroList[i].GetHeroData().TableID);
					int quality1 = hero3.getQuality();

					HeroTemplate hero4 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroList[j].GetHeroData().TableID);
					int quality2 = hero4.getQuality();

					if (quality2 > quality1)
					{
						item = heroList[j];
						heroList[j] = heroList[i];
						heroList[i] = item;
					}
					else if (quality2 == quality1)
					{
						int level1 = heroList[i].GetHeroData().Level;
						int level2 = heroList[j].GetHeroData().Level;

						if (level2 < level1)
						{
							item = heroList[j];
							heroList[j] = heroList[i];
							heroList[i] = item;
						}
                        else if (count2 == count1)
                        {
                            int id1 = heroList[i].GetHeroData().TableID;
                            int id2 = heroList[j].GetHeroData().TableID;
                            if (id2 > id1)
                            {
                                item = heroList[j];
                                heroList[j] = heroList[i];
                                heroList[i] = item;
                            }
                        }
					}
				}
			}
		}
	}

	void SortHeroWithQuailty(ref List<ObjectCard> heroList)
	{
		for (int i = 0; i < heroList.Count - 1; ++i)
		{
			ObjectCard item = heroList[i];
			for (int j = i; j < heroList.Count; ++j)
			{
				HeroTemplate hero1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroList[i].GetHeroData().TableID);
				int quality1 = hero1.getQuality();

				HeroTemplate hero2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroList[j].GetHeroData().TableID);
				int quality2 = hero2.getQuality();

				if (quality2 < quality1)
				{
					item = heroList[j];
					heroList[j] = heroList[i];
					heroList[i] = item;
				}
				else if (quality2 == quality1)
				{
					int level1 = heroList[i].GetHeroData().Level;
					int level2 = heroList[j].GetHeroData().Level;

					if (level2 < level1)
					{
						item = heroList[j];
						heroList[j] = heroList[i];
						heroList[i] = item;
					}
					else if (level2 == level1)
					{
						int count1 = GetLithCount(heroList[i]);
						int count2 = GetLithCount(heroList[j]);
						if (count2 > count1)
						{
							item = heroList[j];
							heroList[j] = heroList[i];
							heroList[i] = item;
						}
                        else if (count2 == count1)
                        {
                            int id1 = heroList[i].GetHeroData().TableID;
                            int id2 = heroList[j].GetHeroData().TableID;
                            if (id2 > id1)
                            {
                                item = heroList[j];
                                heroList[j] = heroList[i];
                                heroList[i] = item;
                            }
                        }
					}
				}
			}
		}
	}

	void SortHeroWithLevel(ref List<ObjectCard> heroList)
	{
		for (int i = 0; i < heroList.Count - 1; ++i)
		{
			ObjectCard item = heroList[i];
			for (int j = i; j < heroList.Count; ++j)
			{
				int level1 = heroList[i].GetHeroData().Level;
				int level2 = heroList[j].GetHeroData().Level;

				if (level2 < level1)
				{
					item = heroList[j];
					heroList[j] = heroList[i];
					heroList[i] = item;
				}
				else if (level2 == level1)
				{
					HeroTemplate hero1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroList[i].GetHeroData().TableID);
					int quality1 = hero1.getQuality();

					HeroTemplate hero2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroList[j].GetHeroData().TableID);
					int quality2 = hero2.getQuality();

					if (quality2 < quality1)
					{
						item = heroList[j];
						heroList[j] = heroList[i];
						heroList[i] = item;
					}
					else if (quality2 == quality1)
					{
						int count1 = GetLithCount(heroList[i]);
						int count2 = GetLithCount(heroList[j]);
						if (count2 < count1)
						{
							item = heroList[j];
							heroList[j] = heroList[i];
							heroList[i] = item;
						}
                        else if (count2 == count1)
                        {
                            int id1 = heroList[i].GetHeroData().TableID;
                            int id2 = heroList[j].GetHeroData().TableID;
                            if (id2 > id1)
                            {
                                item = heroList[j];
                                heroList[j] = heroList[i];
                                heroList[i] = item;
                            }
                        }
					}
				}
			}
		}
	}

    void SortHeroWithID(ref List<ObjectCard> heroList)
    {
        for (int i = 0; i < heroList.Count - 1; ++i)
        {
            ObjectCard item = heroList[i];
            for (int j = i; j < heroList.Count; ++j)
            {
                int id1 = heroList[i].GetHeroData().TableID;
                int id2 = heroList[j].GetHeroData().TableID;
                if(id2 > id1)
                {
                    item = heroList[j];
                    heroList[j] = heroList[i];
                    heroList[i] = item;
                }
            }
        }
    }
	
	int GetLithCount(ObjectCard card)
	{
		HeroTemplate temp = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card.GetHeroData().TableID);
		int lithCount = temp.getReturnBack();
		int expNum = temp.getExpNum();
		int level = card.GetHeroData().Level;
		int allExp = 0;
		for (int i = 1; i < level; i++)
		{
			HeroexpTemplate heroExpTemp = (HeroexpTemplate)DataTemplate.GetInstance().m_HeroExpTable.getTableData(i);
			allExp += heroExpTemp.getExp()[expNum - 1];
		}

		lithCount += (int)((allExp + card.GetHeroData().Exp) * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

		return lithCount;
	}

    public class HeroTempData
    {
        public ObjectCard card;
        public bool isSelect = false;
        public bool isGrey = false;
    }

    void OnDestroy()
    {
        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.Release(m_Caption.transform);
    }

	// 无用代码
	//private static int SortHeroListWithDefault(ObjectCard card1, ObjectCard card2)
	//{
	//	HeroTemplate hero1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetHeroData().TableID);
	//	int count1 = hero1.getReturnBack();
	//	count1 += (int)((float)hero1.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//	HeroTemplate hero2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetHeroData().TableID);
	//	int count2 = hero2.getReturnBack();
	//	count2 += (int)((float)hero2.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//	if (count1 > count2)
	//	{
	//		return 1;
	//	}
	//	else if (count1 < count2)
	//	{
	//		return -1;
	//	}
	//	else
	//	{
	//		HeroTemplate hero11 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetHeroData().TableID);
	//		int quality1 = hero11.getQuality();

	//		HeroTemplate hero22 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetHeroData().TableID);
	//		int quality2 = hero22.getQuality();

	//		if (quality1 > quality2)
	//		{
	//			return -1;
	//		}
	//		else if (quality1 < quality2)
	//		{
	//			return 1;
	//		}
	//		else
	//		{
	//			int level1 = card1.GetHeroData().Level;
	//			int level2 = card2.GetHeroData().Level;

	//			if (level1 > level2)
	//			{
	//				return -1;
	//			}
	//			else
	//			{
	//				return 1;
	//			}

	//			return 0;
	//		}
	//	}

	//	return 0;
	//}

	//private static int SortHeroListWithLevel(ObjectCard card1, ObjectCard card2)
	//{
	//	int level1 = card1.GetHeroData().Level;
	//	int level2 = card2.GetHeroData().Level;

	//	if (level1 > level2)
	//	{
	//		return -1;
	//	}
	//	else if (level1 < level2)
	//	{
	//		return 1;
	//	}
	//	else
	//	{
	//		HeroTemplate hero1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetHeroData().TableID);
	//		int quality1 = hero1.getQuality();

	//		HeroTemplate hero2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetHeroData().TableID);
	//		int quality2 = hero2.getQuality();

	//		if (quality1 > quality2)
	//		{
	//			return -1;
	//		}
	//		else if (quality1 < quality2)
	//		{
	//			return 1;
	//		}
	//		else
	//		{
	//			HeroTemplate hero11 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetHeroData().TableID);
	//			int count1 = hero11.getReturnBack();
	//			count1 += (int)((float)hero1.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//			HeroTemplate hero22 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetHeroData().TableID);
	//			int count2 = hero22.getReturnBack();
	//			count2 += (int)((float)hero2.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//			if (count1 > count2)
	//			{
	//				return 1;
	//			}
	//			else
	//			{
	//				return -1;
	//			}
	//		}
	//	}

	//	return 0;
	//}

	//private static int SortHeroListWithQuailty(ObjectCard card1, ObjectCard card2)
	//{
	//	HeroTemplate hero1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetHeroData().TableID);
	//	int quality1 = hero1.getQuality();

	//	HeroTemplate hero2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetHeroData().TableID);
	//	int quality2 = hero2.getQuality();

	//	if (quality1 > quality2)
	//	{
	//		return 1;
	//	}
	//	else if (quality1 < quality2)
	//	{
	//		return -1;
	//	}
	//	else
	//	{
	//		int level1 = card1.GetHeroData().Level;
	//		int level2 = card2.GetHeroData().Level;

	//		if (level1 > level2)
	//		{
	//			return -1;
	//		}
	//		else if (level1 < level2)
	//		{
	//			return 1;
	//		}
	//		else
	//		{
	//			HeroTemplate hero11 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetHeroData().TableID);
	//			int count1 = hero11.getReturnBack();
	//			count1 += (int)((float)hero1.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//			HeroTemplate hero22 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetHeroData().TableID);
	//			int count2 = hero22.getReturnBack();
	//			count2 += (int)((float)hero2.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//			if (count1 > count2)
	//			{
	//				return 1;
	//			}
	//			else
	//			{
	//				return -1;
	//			}
	//		}
	//	}

	//	return 0;
	//}

	//private static int SortHeroListWithDefaultItem(UI_HeroListItem card1, UI_HeroListItem card2)
	//{
	//	HeroTemplate hero1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetSelectedObjectCard().GetHeroData().TableID);
	//	int count1 = hero1.getReturnBack();
	//	count1 += (int)((float)hero1.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//	HeroTemplate hero2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetSelectedObjectCard().GetHeroData().TableID);
	//	int count2 = hero2.getReturnBack();
	//	count2 += (int)((float)hero2.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//	if (count1 > count2)
	//	{
	//		return 1;
	//	}
	//	else if (count1 < count2)
	//	{
	//		return -1;
	//	}
	//	else
	//	{
	//		HeroTemplate hero11 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetSelectedObjectCard().GetHeroData().TableID);
	//		int quality1 = hero11.getQuality();

	//		HeroTemplate hero22 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetSelectedObjectCard().GetHeroData().TableID);
	//		int quality2 = hero22.getQuality();

	//		if (quality1 > quality2)
	//		{
	//			return -1;
	//		}
	//		else if (quality1 < quality2)
	//		{
	//			return 1;
	//		}
	//		else
	//		{
	//			int level1 = card1.GetSelectedObjectCard().GetHeroData().Level;
	//			int level2 = card2.GetSelectedObjectCard().GetHeroData().Level;

	//			if (level1 > level2)
	//			{
	//				return -1;
	//			}
	//			else
	//			{
	//				return 1;
	//			}

	//			return 0;
	//		}
	//	}

	//	return 0;
	//}

	//private static int SortHeroListWithLevelItem(UI_HeroListItem card1, UI_HeroListItem card2)
	//{
	//	int level1 = card1.GetSelectedObjectCard().GetHeroData().Level;
	//	int level2 = card2.GetSelectedObjectCard().GetHeroData().Level;

	//	if (level1 > level2)
	//	{
	//		return -1;
	//	}
	//	else if (level1 < level2)
	//	{
	//		return 1;
	//	}
	//	else
	//	{
	//		HeroTemplate hero1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetSelectedObjectCard().GetHeroData().TableID);
	//		int quality1 = hero1.getQuality();

	//		HeroTemplate hero2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetSelectedObjectCard().GetHeroData().TableID);
	//		int quality2 = hero2.getQuality();

	//		if (quality1 > quality2)
	//		{
	//			return -1;
	//		}
	//		else if (quality1 < quality2)
	//		{
	//			return 1;
	//		}
	//		else
	//		{
	//			HeroTemplate hero11 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetSelectedObjectCard().GetHeroData().TableID);
	//			int count1 = hero11.getReturnBack();
	//			count1 += (int)((float)hero1.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//			HeroTemplate hero22 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetSelectedObjectCard().GetHeroData().TableID);
	//			int count2 = hero22.getReturnBack();
	//			count2 += (int)((float)hero2.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//			if (count1 > count2)
	//			{
	//				return 1;
	//			}
	//			else
	//			{
	//				return -1;
	//			}
	//		}
	//	}

	//	return 0;
	//}

	//private static int SortHeroListWithQuailtyItem(UI_HeroListItem card1, UI_HeroListItem card2)
	//{
	//	HeroTemplate hero1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetSelectedObjectCard().GetHeroData().TableID);
	//	int quality1 = hero1.getQuality();

	//	HeroTemplate hero2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetSelectedObjectCard().GetHeroData().TableID);
	//	int quality2 = hero2.getQuality();

	//	if (quality1 > quality2)
	//	{
	//		return 1;
	//	}
	//	else if (quality1 == quality2)
	//	{
	//		int level1 = card1.GetSelectedObjectCard().GetHeroData().Level;
	//		int level2 = card2.GetSelectedObjectCard().GetHeroData().Level;

	//		if (level1 > level2)
	//		{
	//			return -1;
	//		}
	//		else if (level1 == level2)
	//		{
	//			HeroTemplate hero11 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card1.GetSelectedObjectCard().GetHeroData().TableID);
	//			int count1 = hero11.getReturnBack();
	//			count1 += (int)((float)hero1.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//			HeroTemplate hero22 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(card2.GetSelectedObjectCard().GetHeroData().TableID);
	//			int count2 = hero22.getReturnBack();
	//			count2 += (int)((float)hero2.getExpNum() * DataTemplate.GetInstance().m_GameConfig.getRongling_conversion_rate() / (float)DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan());

	//			if (count1 > count2)
	//			{
	//				return 1;
	//			}
	//			else if (count1 == count2)
	//			{
	//				return 0;
	//			}
	//			else
	//				return -1;
	//		}
	//		else
	//		{
	//			return -1;
	//		}
	//	}
	//	else
	//	{
	//		return -1;
	//	}
	//}
}
