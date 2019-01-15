using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;

public class UI_RuneInfo : BaseUI
{
	public static UI_RuneInfo inst;
	public static string UI_ResPath = "UI_Rune/UI_RuneInfo_1_3";

	static readonly int MaxStarCount = 5;
    private static X_GUID mRuneGUID = null;

    public static ItemEquip data;
	//private int mAttriCount = 0;

	protected Text mDefineTitleTxt = null;
	protected Text mRuneNameTxt = null;
	//protected RuneIconItem mDefineRuneItem = null;
	protected GameObject mUserObj = null;
	protected Text mUserNameTxt = null;
	protected Text mDetailTxt = null;
	protected GameObject mAttriList = null;              //属性根节点obj;
	protected Button mDefineBtn = null;
	protected Text mDefineBtnTxt = null;
	protected Image mCostItemImg = null;
	protected Text mCostItemNum = null;
	protected Button mCloseBtn = null;
	protected Text mCloseBtnTxt = null;
	protected Text mHintTxt = null;
	protected Text mSpeHeroTxt = null;

	//右上角金钱信息显示;
	protected GameObject mCostObj1 = null;
	protected GameObject mCostObj2 = null;
	protected Text mCostTxt1 = null;
	protected Text mCostTxt2 = null;
	protected Image mCostImg1 = null;
	//protected Image mCostImg2 = null;

	//消费金币处;
	protected Text mConsumeGoldTxt = null;
	protected GameObject mConsumeGoldObj = null;

	protected GameObject mAttriTitleTxt = null;
	protected GameObject mAttriDetailTxt = null;
	protected GameObject mRuneAttriObj = null;
	protected GameObject mAddRuneAttriObj = null;

    protected RuneDetailCommon mDetailCommon = null;
    protected Transform m_RuneAttriPos = null;
    protected RuneItemCommon mItemCommon = null;
    protected Transform m_RuneItemPos = null;

	//private GameObject mObj = null;
	//private X_GUID mGUID = null;

	//public UI_RuneIdentifyMgr(GameObject go)
	//{
	//    if (go == null)
	//        return;

	//    mObj = go;

	//    InitUIData();
	//}
    
	ItemEquip ItemEquipInfo
	{
		get
		{
            if (data==null)
            {
                if (mRuneGUID == null)
                    return null;
                data= (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, mRuneGUID);
            }
            return data;
		}
	}

	public override void InitUIData()
	{
		inst = this;

		//Transform transform = mObj.transform;
        m_RuneAttriPos = selfTransform.FindChild("RuneAttriPos");

        mDefineTitleTxt = transform.FindChild("TitlePanel/TitleObj/Text").GetComponent<Text>();
		mRuneNameTxt = transform.FindChild("RuneDetail/RuneName/Name_txt").GetComponent<Text>();
        m_RuneItemPos = transform.FindChild("RuneDetail/RunItem");
		//mDefineRuneItem = new RuneIconItem(tmpTrans);
		mUserObj = transform.FindChild("RuneDetail/UserName").gameObject;
		mUserNameTxt = transform.FindChild("RuneDetail/UserName/UserName_txt").GetComponent<Text>();
		mDetailTxt = transform.FindChild("RuneDetail/detailTxt").GetComponent<Text>();
		mAttriList = transform.FindChild("RuneDetail/Attris/AttriList").gameObject;              //属性根节点obj;
		mDefineBtn = transform.FindChild("RedefineBtn").GetComponent<Button>();
		mDefineBtnTxt = transform.FindChild("RedefineBtn/Text").GetComponent<Text>();
		mCostItemImg = transform.FindChild("RedefineBtn/Gold/Text/Image").GetComponent<Image>();
		mCostItemNum = transform.FindChild("RedefineBtn/Gold/Text").GetComponent<Text>();
		mCloseBtn = transform.FindChild("CloseBtn").GetComponent<Button>();
		mCloseBtnTxt = transform.FindChild("CloseBtn/Text").GetComponent<Text>();
		mHintTxt = transform.FindChild("Bottom/Text").GetComponent<Text>();
		mSpeHeroTxt = transform.FindChild("RuneDetail/SpecialHeroName").GetComponent<Text>();

		//右上角金钱信息显示;
		mCostObj1 = transform.FindChild("MoneyBar/Gold").gameObject;
		mCostTxt1 = transform.FindChild("MoneyBar/Gold/bg/Text").GetComponent<Text>();
		//mDiamondObj = trans.FindChild("").GetComponent<Text>();
		//mDiamondTxt = trans.FindChild("").GetComponent<Text>();
		mCostImg1 = transform.FindChild("MoneyBar/Gold/Image").GetComponent<Image>();
		//消费金币处;
		mConsumeGoldTxt = transform.FindChild("").GetComponent<Text>();
		mConsumeGoldObj = transform.FindChild("").gameObject;

		mAttriTitleTxt = transform.FindChild("Items/AttriTitle").gameObject;
		mAttriDetailTxt = transform.FindChild("Items/LineTxt").gameObject;
		mRuneAttriObj = transform.FindChild("Items/AttriPair").gameObject;
		mAddRuneAttriObj = transform.FindChild("Items/AddAttriPair").gameObject;

		mDefineBtn.onClick.AddListener(OnDefineBtnClick);
		mCloseBtn.onClick.AddListener(OnCloseBtnClick);
       // data = null;
		initString();
	}

	public override void InitUIView()
	{
		base.InitUIView();

		ShowUI();
	}

	public static void SetShowRuneGUID(X_GUID guid)
	{
        data = null;
		mRuneGUID = guid;
	}

    public static void SetShowRuneDate(ItemTemplate itemT)
    {
        if(itemT == null)
        {
            LogManager.LogError("Open UI_RuneInfo error, ItemTemplate is null");
            return;
        }

        ItemEquip data = new ItemEquip();
        //data.SetItemGuid(110);
        data.SetItemTableID(itemT.getId());
        int[] runeID = new int[GlobalMembers.MAX_RUNE_BASE_ATTRIBUTE_COUNT];
        if (itemT.getRune_baseAttri1() != -1)
        {
            runeID[0] = itemT.getRune_baseAttri1() * 100;
        }
        else
        {
            runeID[0] = -1;
        }
        if (itemT.getRune_baseAttri2() != -1)
        {
            runeID[1] = itemT.getRune_baseAttri2() * 100;
        }
        else
        {
            runeID[1] = -1;
        }
        if (itemT.getRune_baseAttri3() != -1)
        {
            runeID[2] = itemT.getRune_baseAttri3() * 100;
        }
        else
        {
            runeID[2] = -1;
        }
        int[] runAddID = new int[GlobalMembers.MAX_RUNE_APPEND_ATTRIBUTE_COUNT];
        for (int i = 0; i < GlobalMembers.MAX_RUNE_APPEND_ATTRIBUTE_COUNT; i++)
        {
            runAddID[i] = -1;
        }
        data.GetRuneData().SetBaseAttributeID(runeID);
        data.GetRuneData().SetAppendAtttibute(runAddID);
        data.SetEquipState(false);
        //UI_RuneInfo.SetShowRuneGUID(data.GetItemGuid());
        SetShowRuneDate(data);
    }

    public static void SetShowRuneDate(ItemEquip datas)
    {
        mRuneGUID = null;
        data = datas;
    }
	private void ShowUI()
	{
        if (data == null)
        {
            if (mRuneGUID == null)
                return;

            GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshItem, OnItemRefresh);

            if (ItemEquipInfo == null) return;

            mDetailCommon = new RuneDetailCommon(m_RuneAttriPos, mRuneGUID, 450f);
        }
        else
        {
            mDetailCommon = new RuneDetailCommon(m_RuneAttriPos, ItemEquipInfo.GetItemRowData().getId(), 450f);

        }

		UpdateUI();

		//mObj.SetActive(true);
	}

	void OnItemRefresh(GameEvent ge)
	{
		if (ge == null) return;

		Item it = ge.data as Item;
		if (it == null) return;

		if (mRuneGUID.GUID_value == it.key)
			UpdateUI();
	}

	//void ResetAttriCount()
	//{
	//    mAttriCount = 0;
	//}

	void UpdateUI()
	{
		//ResetAttriCount();

		ItemTemplate itemT = ItemEquipInfo.GetItemRowData();

        if (mItemCommon == null)
        {
            mItemCommon = RuneFactory.CreateRuneItemCommom(m_RuneItemPos);
        }

        RuneItemCommonData ricd = new RuneItemCommonData();
        ricd.IsShowMaxEffect = true;
        ricd.ItemT = ItemEquipInfo.GetItemRowData();
        ricd.RuneLevel = ItemEquipInfo.GetStrenghLevel();
        ricd.EquipedHeroName = RuneModule.GetItemEuipHeroName(ItemEquipInfo);
        mItemCommon.SetRuneItemData(ricd, RuneItemCommon.RuneItemShowType.IconWithRightName);

        mUserObj.SetActive(false);

        //mRuneNameTxt.text = GameUtils.getString(itemT.getName());

        //mSpeHeroTxt.text = GameUtils.getString(itemT.getSpecialHeroDes());

        //mDefineRuneItem.SetIcon(common.defaultPath + itemT.getIcon());
        //mDefineRuneItem.SetStarsNum(itemT.getRune_quality());
        //mDefineRuneItem.SetLevel(ItemEquipInfo.GetStrenghLevel());
        ////bool isSpecial = itemT.getRune_type() == 5 || itemT.getRune_type() == 6;        
        //bool isSpecial = RuneModule.IsSpecialRune(itemT);
        //mDefineRuneItem.SetIsSpecial(isSpecial);

        //if (!isSpecial)
        //{
        //    mDefineRuneItem.SetRuneType(itemT.getRune_type());
        //}

        //if (mRuneGUID !=null)
        //{
        //    ObjectCard oc = ObjectSelf.GetInstance().HeroContainerBag.GetItemUser(ItemEquipInfo);
        //    if (oc == null)
        //    {
        //        mUserObj.SetActive(false);
        //    }
        //    else
        //    {
        //        HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(oc.GetHeroData().TableID);
        //        if (heroT != null)
        //            mUserNameTxt.text = GameUtils.getString(heroT.getTitleID()) + "    " + GameUtils.getString("hero_rune_content7");
        //        mUserObj.SetActive(true);
        //    }
        //}
        //else
        //{
        //    mUserObj.SetActive(false);
        //}

        //mDetailTxt.text = "";
        //GameUtils.DestroyChildsObj(mAttriList);
        int count = DataTemplate.GetInstance().GetRuneMaxRedefineTimes(itemT);

		//--------基础属性;
        //RuneData runeData = ItemEquipInfo.GetRuneData();
        //bool titleDone1 = false;
        //foreach (int id in runeData.BaseAttributeID)
        //{
        //    if (id == -1)
        //        continue;

        //    if (!titleDone1)
        //    {
        //        titleDone1 = true;
        //        CreateTitle(mAttriList, GameUtils.getString("hero_rune_content8"));
        //    }

        //    BaseruneattributeTemplate bt = DataTemplate.GetInstance().GetBaseruneattributeTemplate(id);
        //    if (bt.getNumshow() == 0)
        //    {
        //        //CreateTitle(mAttriList, GameUtils.getString(bt.getAttriDes()));
        //        CreateDetailTxts(mAttriList, GameUtils.getString(bt.getAttriDes()));
        //    }
        //    else
        //    {
        //        //CreateBaseAttriObj(mAttriList, GameUtils.GetAttriName(bt.getAttriType()), "+" + bt.getAttriValue().ToString());
        //        CreateBaseAttriObj(mAttriList, GameUtils.getString(bt.getAttriDes()), "+" + bt.getAttriValue().ToString());
        //    }
        //}

        ////--------附加属性;
        //bool titleDone2 = false;
        //int i = 0;
        //bool isGray = false;

        //foreach (int id in runeData.AppendAttribute)
        //{
        //    i++;

        //    isGray = i * 3 > ItemEquipInfo.GetStrenghLevel();

        //    if (id == -1)
        //    {
        //        if (i <= count)
        //        {
        //            if (!titleDone2)
        //            {
        //                titleDone2 = true;
        //                CreateTitle(mAttriList, GameUtils.getString("hero_rune_content9"));
        //            }
        //            //未知属性，未鉴定;
        //            CreateAddAttriObj(mAttriList, GameUtils.getString("rune_content2"), "", GameUtils.getString("rune_content3"), isGray);
        //        }

        //        continue;
        //    }

        //    if (!titleDone2)
        //    {
        //        titleDone2 = true;
        //        CreateTitle(mAttriList, GameUtils.getString("hero_rune_content9"));
        //    }

        //    AddruneattributeTemplate bt = DataTemplate.GetInstance().GetAddruneattributeTemplate(id);
        //    bool isPercent = bt.getIspercentage() > 0;
        //    string val = isPercent ? ((float)bt.getAttriValue() / (float)10f + "%") : bt.getAttriValue().ToString();
        //    CreateAddAttriObj(mAttriList, GameUtils.getString(bt.getAttriDes1()), GameUtils.getString(bt.getAttriDes2()), bt.getSymbol() + val, isGray);
        //}

		//是否鉴定满级;
		bool isFullIdentify = ItemEquipInfo.GetDefineTimes() >= count;
		GameUtils.SetBtnSpriteGrayState(mDefineBtn, isFullIdentify);

		//刷新消耗;
		int itemId = -1;              //消耗道具id
		int itemCount = -1;           //消耗道具数量;
		int curLv = ItemEquipInfo.GetDefineTimes();
		switch (curLv)
		{
			case 0:
				itemId = itemT.getRune_exposeCostType1();
				itemCount = itemT.getRune_exposeCostValue1();
				break;
			case 1:
				itemId = itemT.getRune_exposeCostType2();
				itemCount = itemT.getRune_exposeCostValue2();
				break;
			case 2:
				itemId = itemT.getRune_exposeCostType3();
				itemCount = itemT.getRune_exposeCostValue3();
				break;
			case 3:
				itemId = itemT.getRune_exposeCostType4();
				itemCount = itemT.getRune_exposeCostValue4();
				break;
			default:
				break;
		}

		if (!isFullIdentify)
		{
			if (itemId == -1 || itemCount == -1)
			{
				//LogManager.LogError("错误的道具id：" + itemId + "或者是错误的道具数量:" + itemCount);
				LogManager.LogError("符文当前:" + curLv + "级，还可以鉴定，但是鉴定道具id" + itemId + "或者道具数量：" + itemCount + "不对");
			}
			ItemTemplate costItemT = DataTemplate.GetInstance().GetItemTemplateById(itemId);
			mCostItemImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + costItemT.getIcon());
			mCostImg1.sprite = mCostItemImg.sprite;
			int itemTotal = -1;
			ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, itemId, ref itemTotal);
			mCostTxt1.text = itemTotal.ToString();
			int haveCount = 0;
			ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, itemId, ref haveCount);
			TEXT_COLOR tc = itemCount > haveCount ? TEXT_COLOR.RED : TEXT_COLOR.WHITE;
			mCostItemNum.text = GameUtils.StringWithColor(itemCount.ToString(), tc);
		}
	}

	void CreateDetailTxts(GameObject parent, string detail)
	{
        //int totalCount = detail.Length;
        //int tmp = totalCount % GlobalMembers.MAX_RUNE_COUNT_PER_LINE;
        //int lineNum = 0;

        //if (tmp == 0)
        //    lineNum = totalCount / GlobalMembers.MAX_RUNE_COUNT_PER_LINE;
        //else
        //    lineNum = totalCount / GlobalMembers.MAX_RUNE_COUNT_PER_LINE + 1;

        //int startIdx = -1, endIdx = -1;
        //for (int i = 0; i < lineNum; i++)
        //{
        //    startIdx = GlobalMembers.MAX_RUNE_COUNT_PER_LINE * i;
        //    endIdx = GlobalMembers.MAX_RUNE_COUNT_PER_LINE * (i + 1);
        //    if (i == lineNum - 1)
        //    {
        //        CreateDetailTxt(parent, detail.Substring(startIdx));
        //    }
        //    else
        //    {
        //        CreateDetailTxt(parent, detail.Substring(startIdx, endIdx));
        //    }
        //}

        string[] contents = detail.SplitByLength(GlobalMembers.MAX_RUNE_COUNT_PER_LINE);

        if (contents == null)
        {
            return;
        }

        int count = contents.Length;

        if (count <= 0)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            CreateDetailTxt(parent, contents[i]);
        }
    }

	void CreateDetailTxt(GameObject parent, string detail)
	{
		GameObject go = (GameObject)GameObject.Instantiate(mAttriDetailTxt.gameObject);

		go.transform.parent = parent.transform;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

		go.transform.FindChild("Text").GetComponent<Text>().text = detail;
	}

	/// <summary>
	/// 创建属性标题;--基础属性、附加属性;
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="str"></param>
	void CreateTitle(GameObject parent, string str)
	{
		//mAttriCount++;

		GameObject go = (GameObject)GameObject.Instantiate(mAttriTitleTxt.gameObject);

		go.transform.parent = parent.transform;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

		go.GetComponent<Text>().text = str;
		//go.transform.SetSiblingIndex(mAttriCount);
	}

	/// <summary>
	/// 创建属性标题;--基础属性;
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="str"></param>
	void CreateBaseAttriObj(GameObject parent, string str1, string str2)
	{
		//mAttriCount++;

		GameObject go = GameObject.Instantiate(mRuneAttriObj) as GameObject;

		go.transform.parent = parent.transform;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

		go.transform.FindChild("Left_txt").GetComponent<Text>().text = str1;
		go.transform.FindChild("Right_txt").GetComponent<Text>().text = str2;
		//go.transform.SetSiblingIndex(mAttriCount);
	}

	/// <summary>
	/// 创建属性列;--附加属性;
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="str1"></param>
	/// <param name="str2"></param>
	void CreateAddAttriObj(GameObject parent, string str1, string str2, string str3, bool isGray)
	{
		//mAttriCount++;

		GameObject go = GameObject.Instantiate(mAddRuneAttriObj) as GameObject;

		go.transform.parent = parent.transform;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

		if (isGray)
		{
			str1 = GameUtils.StringWithGrayColor(str1);
			str2 = GameUtils.StringWithGrayColor(str2);
			str3 = GameUtils.StringWithGrayColor(str3);
		}

		go.transform.FindChild("Left_txt").GetComponent<Text>().text = str1;
		go.transform.FindChild("Mid_txt").GetComponent<Text>().text = str2;
		go.transform.FindChild("Right_txt").GetComponent<Text>().text = str3;

		//go.transform.SetSiblingIndex(mAttriCount);
	}

	public void OnDisable()
	{
		GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshItem, OnItemRefresh);

		if (UI_HeroRuneManager._instance != null)
		{
			UI_HeroRuneManager._instance.UpdateRuneDetailUI();
		}
	}

	public void OnDestroy()
	{
		mDefineBtn.onClick.RemoveListener(OnDefineBtnClick);
		mCloseBtn.onClick.RemoveListener(OnCloseBtnClick);

        if (mDetailCommon != null)
        {
            mDetailCommon.Destroy();
            mDetailCommon = null;
        }

        data = null;
        mRuneGUID = null;
        inst = null;
	}

	void OnDefineBtnClick()
	{
		if (mRuneGUID == null || ItemEquipInfo == null)
			return;

		ItemTemplate itemT = ItemEquipInfo.GetItemRowData();

		//一星符文没法鉴定;
		if (itemT.getRune_quality() <= 1)
		{
			InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip3"), transform);
			return;
		}

		int count = DataTemplate.GetInstance().GetRuneMaxRedefineTimes(itemT);

		//是否鉴定满级;
		if (ItemEquipInfo.GetDefineTimes() >= count)
		{
			InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip4"), transform);
			return;
		}

		///鉴定消耗物品是否足够;
		int itemId = -1;              //消耗道具id
		int itemCount = -1;           //消耗道具数量;
		switch (ItemEquipInfo.GetDefineTimes())
		{
			case 0:
				itemId = itemT.getRune_exposeCostType1();
				itemCount = itemT.getRune_exposeCostValue1();
				break;
			case 1:
				itemId = itemT.getRune_exposeCostType2();
				itemCount = itemT.getRune_exposeCostValue2();
				break;
			case 2:
				itemId = itemT.getRune_exposeCostType3();
				itemCount = itemT.getRune_exposeCostValue3();
				break;
			case 3:
				itemId = itemT.getRune_exposeCostType4();
				itemCount = itemT.getRune_exposeCostValue4();
				break;
			default:
				break;
		}
		if (itemId != -1 && itemCount > 0)
		{
			int haveCount = 0;
			ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, itemId, ref haveCount);

			//道具不足提示;
			if (haveCount < itemCount)
			{
				ItemTemplate costItemT = DataTemplate.GetInstance().GetItemTemplateById(itemId);
				UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
				box.SetIsNeedDescription(true);
				Sprite sp = UIResourceMgr.LoadSprite(common.defaultPath + costItemT.getIcon());
				box.SetConsume_Image(sp);
				box.SetConNum(itemCount.ToString());
				string content = string.Format(GameUtils.getString("hero_rune_identifyform_tip1"), GameUtils.getString(costItemT.getName(), TEXT_COLOR.YELLOW));
				box.SetDescription_text(content);
				box.SetLeftBtn_text(GameUtils.getString("common_button_purchase1"));
				box.SetLeftClick(OnMsgBoxYesClick);
			}
			else
			{
				SendIndentifyProtocol();
			}
		}
		else
		{
			LogManager.LogError("错误的道具id：" + itemId + "或者是错误的道具数量:" + itemCount);
		}


	}

	void SendIndentifyProtocol()
	{
		CIdentifyEquip msg = new CIdentifyEquip();
		msg.equipkey = (int)mRuneGUID.GUID_value;
		IOControler.GetInstance().SendProtocol(msg);
	}

	/// <summary>
	/// 点击立刻购买;
	/// </summary>
	void OnMsgBoxYesClick()
	{
		UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
		InterfaceControler.GetInst().AddMsgBox("等商城完成后开放", transform);

		//去商城购买物品-----如果钻石不足弹出钻石不足弹窗;
	}

	void OnCloseBtnClick()
	{
		HideUI();
	}

	void HideUI()
	{
		GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, UI_ResPath);

		if (UI_HeroRuneManager._instance != null)
		{
			UI_HeroRuneManager._instance.RunOnFront();
		}
	}

	void initString()
	{
        if (UI_SacredAltar._instance != null)
        {
            mDefineTitleTxt.text = GameUtils.getString("clone_window1");
        }
        else
        {
            mDefineTitleTxt.text = GameUtils.getString("rune_info");
        }
		mHintTxt.text = GameUtils.getString("rune_content1");
		mDefineBtnTxt.text = GameUtils.getString("common_button_identify");
		mCloseBtnTxt.text = GameUtils.getString("common_button_close");
	}
}
