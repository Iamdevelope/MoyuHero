using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;
using System.Collections.Generic;
using System.Text;

using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameCore;



/// <summary>
/// 符文组合处;
/// </summary>
public class UI_HeroRuneGroupItem
{
    protected GameObject[] mEffect = null;
    protected Image mIcon = null;

    private Transform mTrans = null;

    public UI_HeroRuneGroupItem(Transform trans)
    {
        if(trans == null)
            return ;

        mTrans = trans;

        mEffect = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            mEffect[i] = trans.FindChild("EffectObj" + i).gameObject;
        }

        mIcon = trans.FindChild("Image").GetComponent<Image>();
    }

    public void SetActive(bool isActive)
    {
        mTrans.gameObject.SetActive(isActive);
    }

    public void SetIcon(string iconName)
    {
        //mIcon.sprite = UIResourceMgr.LoadSprite(iconName);
        //mIcon.SetNativeSize();
        SetIcon(UIResourceMgr.LoadSprite(iconName));
    }

    public void SetIcon(Sprite sprite)
    {
        mIcon.sprite = sprite;
        mIcon.SetNativeSize();
    }

    public void SetEffectShow(EM_RUNE_TYPE type)
    {
        for (int i = 0; i < 4; i++)
        {
            mEffect[i].SetActive(false);
        }
        
        switch(type)
        {
            case EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID:
                return;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_BLUE:
                mEffect[0].SetActive(true);
                return;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_PURPLE:
                mEffect[1].SetActive(true);
                return;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_GREEN:
                mEffect[2].SetActive(true);
                return;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_RED:
                mEffect[3].SetActive(true);
                return;
        }
    }

    public void Destroy()
    {
        mEffect = null;
        mTrans = null;
    }
}

public class Pair
{
    public int a;
    public int b;

    public Pair(int num1, int num2)
    {
        a = num1;
        b = num2;
    }
}

public class UI_HeroRuneManager : BaseUI
{
    //readonly string UI_ResPath = "UI_Home/UI_HeroInfo/Rune";

    public static UI_HeroRuneManager _instance;

    //public string AddSpriteName = "";
    public Sprite AddSprite = null;
    //public string[] RuneTypeImages = null;    
    public Sprite[] RuneTypeImages = null;
    public Sprite[] RuneAttrImgs = null;

    protected RuneIconItem[] mRunes = null;
    protected UI_HeroRuneGroupItem[] mRuneGoupItems = null;
    protected Button mRuneBounsBtn = null;
    protected GameObject mRuneEffObj = null;

    protected Image[] mRuneAttrImgs = null;
    protected Text[] mRuneDetails = null;
    protected Text[] mRuneValues = null;

    protected Transform mMsgBoxTrans = null;

    /// <summary>
    /// 基础属性--附加属性，符文属性展示处公用对象;
    /// </summary>
    protected Text mAttriTitleTxt = null;             //基础属性/附加属性标题;
    protected GameObject mRuneAttriObj = null;        //属性名称还有属性值;
    protected GameObject mAddRuneAttriObj = null;      //附加符文属性obj;

    /// <summary>
    /// 装备的符文展示界面;
    /// </summary>
    protected GameObject mRuneDetailObj = null;
    //protected RuneIconItem mRuneDetailItem = null;
    protected GameObject mUserObj = null;
    protected Text mSpecHeroName = null;
    protected Text mRuneName = null;
    protected Text mUserName = null;
    protected Button mCloseDetailBtn = null;
    protected Button mStrenthBtn = null;
    protected Text mStrenthTxt = null;
    protected Button mIdentifyBtn = null;
    protected Text mIdentifyTxt = null;
    protected Button mChangeBtn = null;
    protected Text mChangeTxt = null;
    protected GameObject mAttriList = null;
    protected GameObject mAttriDetailTxt = null;
    
    /// <summary>
    /// 符文加成界面;
    /// </summary>
    protected GameObject mPropRuneObj = null;
    protected GameObject mPropNoRuneObj = null;
    protected Text mPropNoRuneTxt = null;
    protected Button mPropCloseBtn = null;
    protected Text mPropCloseBtnTxt = null;
    protected Transform mPropListTrans = null;
    protected List<UI_RuneAttriItem> mPropRuneQueue = null;
    protected Transform mPropItemTrans = null;

    //protected GameObject mRuneIdentityObj = null;
    //private UI_RuneIdentifyMgr mRuneIdentifyMgr = null;

    //protected GameObject mRuneStrenthObj = null;
    //private UI_RuneStrenthMgr mRuneStrenthMgr = null;

    private EM_RUNE_POINT mRunePoint = EM_RUNE_POINT.EM_RUNE_POINT_INVALID;
    private ObjectCard mObjectCard = null;

    private UI_RuneChangePageManager mUIHeroRuneChangeMgr = null;
    protected RuneDetailCommon mDetailCommon = null;
    protected Transform m_RuneAttriPos = null;
    protected RuneItemCommon mItemCommon = null;
    protected Transform m_RuneItemPos = null;

    //功能提示
    private GameObject[] m_TipsImageArray;
    private FunctionTipsManager m_FunctionTipsManager;
    UI_RuneChangePageManager HRCMgr
    {
        get
        {
            if (mUIHeroRuneChangeMgr == null)
            {
                GameObject go = (GameObject)Instantiate(UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_HeroInfo/RuneChangePage"));
                if (go != null)
                {
                    go.transform.parent = this.transform.parent;
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localScale = Vector3.one;
                    mUIHeroRuneChangeMgr = go.GetComponent<UI_RuneChangePageManager>();
                }

            }
            return mUIHeroRuneChangeMgr;
        }
    }


    ObjectCard _ObjectCard
    {
        get
        {
            return mObjectCard;
        }
        set
        {
            if (mObjectCard != null && mObjectCard.Equals(value))
                return;

            mObjectCard = value;

            UpdateUIForm();
            //HideRunePropUI();
            if (mPropRuneObj.activeSelf)
                ShowRunePropUI();
            HideRuneDetailUI();
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;

        m_RuneAttriPos = selfTransform.FindChild("RuneDetail/RuneAttriPos");
        m_RuneItemPos = selfTransform.FindChild("RuneDetail/RuneInfo/Panel/RunItem1");

        mRunes = new RuneIconItem[(int)EM_RUNE_POINT.EM_RUNE_POINT_NUMBER];
        for (int i = 0, j = (int)EM_RUNE_POINT.EM_RUNE_POINT_NUMBER; i < j; i++)
        {
            Transform trans = transform.FindChild("RuneGroup/RunItem" + i);
            mRunes[i] = new RuneIconItem(trans);
            mRunes[i].SetIsSpecial(i == (int)EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL);
        }

        mRuneGoupItems = new UI_HeroRuneGroupItem[4];
        for (int i = 0; i < 4; i++)
        {
            Transform trans = transform.FindChild("RuneRewards/runesGrid/RunRewardsItem" + i);
            mRuneGoupItems[i] = new UI_HeroRuneGroupItem(trans);
        }
      
        mRuneBounsBtn = transform.FindChild("Rune bonusBtn").GetComponent<Button>();
        mRuneEffObj = transform.FindChild("MagicArray01").gameObject;

        mRuneAttrImgs = new Image[3];
        mRuneDetails = new Text[3];
        mRuneValues = new Text[3];

        for (int i = 0; i < 3; i++ )
        {
            mRuneAttrImgs[i] = transform.FindChild("RuneRewards/attriGrid/RunReward" + i + "_txt/Image").GetComponent<Image>();
            mRuneDetails[i] = transform.FindChild("RuneRewards/attriGrid/RunReward" + i + "_txt/Left_Txt").GetComponent<Text>();
            mRuneValues[i] = transform.FindChild("RuneRewards/attriGrid/RunReward" + i + "_txt/Right_Txt").GetComponent<Text>();
        }
        mMsgBoxTrans = transform.FindChild("MsgBoxObj");
        mAttriTitleTxt = transform.FindChild("Items/AttriTitle").GetComponent<Text>();
        mRuneAttriObj = transform.FindChild("Items/AttriPair").gameObject;
        mAddRuneAttriObj = transform.FindChild("Items/AddAttriPair").gameObject;

        mRuneDetailObj = transform.FindChild("RuneDetail").gameObject;
        //Transform temp = transform.FindChild("RuneDetail/RuneInfo/Panel/RunItem1");
        //mRuneDetailItem = new RuneIconItem(temp);
        mUserObj = transform.FindChild("RuneDetail/RuneInfo/Panel/UserName").gameObject;
        mSpecHeroName = transform.FindChild("RuneDetail/RuneInfo/Panel/SpecialHeroName").GetComponent<Text>();
        mRuneName = transform.FindChild("RuneDetail/RuneInfo/Panel/RuneName_txt/Name_txt").GetComponent<Text>();
        mUserName = transform.FindChild("RuneDetail/RuneInfo/Panel/UserName/UserName_txt").GetComponent<Text>();
        mCloseDetailBtn = transform.FindChild("RuneDetail/RuneInfo/CloseBtn/Image").GetComponent<Button>();
        mStrenthBtn = transform.FindChild("RuneDetail/RuneInfo/StrenthBtn").GetComponent<Button>();
        mStrenthTxt = transform.FindChild("RuneDetail/RuneInfo/StrenthBtn/Text").GetComponent<Text>();
        mIdentifyBtn = transform.FindChild("RuneDetail/RuneInfo/IdentifyBtn").GetComponent<Button>();
        mIdentifyTxt = transform.FindChild("RuneDetail/RuneInfo/IdentifyBtn/Text").GetComponent<Text>();
        mChangeBtn = transform.FindChild("RuneDetail/RuneInfo/ChangeBtn").GetComponent<Button>();
        mChangeTxt = transform.FindChild("RuneDetail/RuneInfo/ChangeBtn/Text").GetComponent<Text>();
        mAttriList = transform.FindChild("RuneDetail/RuneInfo/Panel/Attris/AttriList").gameObject;
        mAttriDetailTxt = transform.FindChild("Items/LineTxt").gameObject;

        mPropRuneQueue = new List<UI_RuneAttriItem>();
        mPropRuneObj = transform.FindChild("RuneProp").gameObject;
        mPropNoRuneObj = transform.FindChild("RuneProp/NoRuneObj").gameObject;
        mPropNoRuneTxt = transform.FindChild("RuneProp/NoRuneObj/Text").GetComponent<Text>();
        mPropCloseBtn = transform.FindChild("RuneProp/CloseBtn").GetComponent<Button>();
        mPropCloseBtnTxt = transform.FindChild("RuneProp/CloseBtn/Text").GetComponent<Text>();
        mPropListTrans = transform.FindChild("RuneProp/Panel/AttriList");
        mPropItemTrans = transform.FindChild("RuneProp/Items/PropItem");

        //mRuneIdentityObj = transform.FindChild("RuneIdentify").gameObject;
        //mRuneStrenthObj = transform.FindChild("RuneStrenth").gameObject;

        mRunes[0].AddIconClickListener(OnRuneBtnClick0);
        mRunes[1].AddIconClickListener(OnRuneBtnClick1);
        mRunes[2].AddIconClickListener(OnRuneBtnClick2);
        mRunes[3].AddIconClickListener(OnRuneBtnClick3);
        mRunes[4].AddIconClickListener(OnRuneBtnClick4);
        mRunes[5].AddIconClickListener(OnRuneBtnClick5);

        mRuneBounsBtn.onClick.AddListener(OnRunePropBtnClick);
        mCloseDetailBtn.onClick.AddListener(OnCloseRuneDetailBtnClick);
        mStrenthBtn.onClick.AddListener(OnStrenthBtnClick);
        mIdentifyBtn.onClick.AddListener(OnIdentifyBtnClick);
        mChangeBtn.onClick.AddListener(OnChangeBtnClick);
        mPropCloseBtn.onClick.AddListener(OnRunePropCloseBtnClick);


        //功能提示
        m_FunctionTipsManager = FunctionTipsManager.GetInstance();
        m_TipsImageArray = new GameObject[(int)EM_RUNE_POINT.EM_RUNE_POINT_NUMBER];
        m_TipsImageArray[(int)EM_RUNE_POINT.EM_RUNE_POINT_COMMON1] = selfTransform.FindChild("RuneGroup/RunItem0/TipsImage").gameObject;
        m_TipsImageArray[(int)EM_RUNE_POINT.EM_RUNE_POINT_COMMON2] = selfTransform.FindChild("RuneGroup/RunItem1/TipsImage").gameObject;
        m_TipsImageArray[(int)EM_RUNE_POINT.EM_RUNE_POINT_COMMON3] = selfTransform.FindChild("RuneGroup/RunItem2/TipsImage").gameObject;
        m_TipsImageArray[(int)EM_RUNE_POINT.EM_RUNE_POINT_COMMON4] = selfTransform.FindChild("RuneGroup/RunItem3/TipsImage").gameObject;
        m_TipsImageArray[(int)EM_RUNE_POINT.EM_RUNE_POINT_COMMON5] = selfTransform.FindChild("RuneGroup/RunItem4/TipsImage").gameObject;
        m_TipsImageArray[(int)EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL] = selfTransform.FindChild("RuneGroup/RunItem5/TipsImage").gameObject;

        GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshItem, OnItemRefresh);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshHero, OnCardHeroDataChanged);

        GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, RefreshTips);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshItem, RefreshTips);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshHero, RefreshTips);

        InitUIString();

        //mStrenthTxt.gameObject.layer = LayerMask.NameToLayer("UI");
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Guide_Continue, ShowNewGuide);
        
    }

    public override void InitUIView()
    {
        base.InitUIView();

        ObjectCard cardInfo = UI_HeroInfoManager._instance.GetCurCard();
        if (cardInfo != null)
            _ObjectCard = cardInfo;

        //if (GuideManager.GetInstance().IsContentGuideID(200102))
        if (GuideManager.GetInstance().GetBackCount(200101))
        {
            GuideManager.GetInstance().ShowGuideWithIndex(200102);
        }

        RefreshTips();
    }

    /// <summary>
    /// 监测新手引导 点击继续
    /// </summary>
    /// <param name="e"></param>
    private void ShowNewGuide(GameEvent e)
    {
        int _id = (int)e.data;
        if (_id != -1)
        {
            //新手引导相关 【点击继续】装配符文
            GuideManager.GetInstance().ShowGuideWithIndex(_id);      
        }
        else
        {
            GuideManager.GetInstance().StopGuide();
        }
    }

    void InitUIString()
    {
        mChangeTxt.text = GameUtils.getString("common_button_change");
        mIdentifyTxt.text = GameUtils.getString("common_button_identify");
        mStrenthTxt.text = GameUtils.getString("common_button_strengthen");
        mPropCloseBtnTxt.text = GameUtils.getString("common_button_close");
        mPropNoRuneTxt.text = GameUtils.getString("rune_addition_content1");
    }

    void OnDisable()
    {
        HideRuneDetailUI();
        HideRunePropUI();

        //if(mRuneIdentifyMgr != null)
        //{
        //    mRuneIdentifyMgr.OnDisable();
        //}

        //if(mRuneStrenthMgr != null)
        //{
        //    mRuneStrenthMgr.OnDisable();
        //}
    }

    void OnDestroy()
    {
        OnReadyForClose();
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Guide_Continue, ShowNewGuide);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, RefreshTips);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshItem, RefreshTips);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshHero, RefreshTips);
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        mRunes[0].RemoveIconClickListener(OnRuneBtnClick0);
        mRunes[1].RemoveIconClickListener(OnRuneBtnClick1);
        mRunes[2].RemoveIconClickListener(OnRuneBtnClick2);
        mRunes[3].RemoveIconClickListener(OnRuneBtnClick3);
        mRunes[4].RemoveIconClickListener(OnRuneBtnClick4);
        mRunes[5].RemoveIconClickListener(OnRuneBtnClick5);

        mRuneBounsBtn.onClick.RemoveListener(OnRunePropBtnClick);
        mCloseDetailBtn.onClick.RemoveListener(OnCloseRuneDetailBtnClick);
        mStrenthBtn.onClick.RemoveListener(OnStrenthBtnClick);
        mIdentifyBtn.onClick.RemoveListener(OnIdentifyBtnClick);
        mChangeBtn.onClick.RemoveListener(OnChangeBtnClick);
        mPropCloseBtn.onClick.RemoveListener(OnRunePropCloseBtnClick);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshItem, OnItemRefresh);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshHero, OnCardHeroDataChanged);

        for (int i = 0, j = mRunes.Length; i < j; i++ )
        {
            mRunes[i].Destroy();
        }

        for (int i = 0, j = mRuneGoupItems.Length; i < j; i++)
        {
            mRuneGoupItems[i].Destroy();
        }

        mItemCommon = null;
        if (mDetailCommon != null)
        {
            mDetailCommon.Destroy();
            mDetailCommon = null;
        }
        mRuneAttrImgs = null;
        mRuneDetails = null;
        mRuneValues = null;
        mRunes = null;
        mRuneGoupItems = null;
        mObjectCard = null;
        _instance = null;

        foreach(UI_RuneAttriItem attrItem in mPropRuneQueue)
        {
            attrItem.Destroy();
        }
        mPropRuneQueue.Clear();
        mPropRuneQueue = null;
        //System.GC.Collect();

        //if(mRuneIdentifyMgr != null)
        //{
        //    mRuneIdentifyMgr.OnDestroy();
        //}

        //if(mRuneStrenthMgr != null)
        //{
        //    mRuneStrenthMgr.OnDestroy();
        //}
    }

    void RunOnBack()
    {
        mRuneEffObj.SetActive(false);
        for(int i = 0, j = mRuneGoupItems.Length; i < j; i++)
        {
            mRuneGoupItems[i].SetEffectShow(EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID);
        }
    }

    public void RunOnFront()
    {
        //mRuneEffObj.SetActive(true);
        UpdateUIForm();
    }

    void OnRuneBtnClick0()
    {
        RuneBtnClickHandler(EM_RUNE_POINT.EM_RUNE_POINT_COMMON1); 
    }
    void OnRuneBtnClick1()
    {
        RuneBtnClickHandler(EM_RUNE_POINT.EM_RUNE_POINT_COMMON2);
    }
    void OnRuneBtnClick2()
    {
        RuneBtnClickHandler(EM_RUNE_POINT.EM_RUNE_POINT_COMMON3);
    }
    void OnRuneBtnClick3()
    {
        RuneBtnClickHandler(EM_RUNE_POINT.EM_RUNE_POINT_COMMON4);
        //新手引导相关 点击【符文槽】
        if (GuideManager.GetInstance().GetBackCount(200104))
        {
            GuideManager.GetInstance().ShowGuideWithIndex(200105);
        }
    }
    void OnRuneBtnClick4()
    {
        RuneBtnClickHandler(EM_RUNE_POINT.EM_RUNE_POINT_COMMON5);
    }

    void OnRuneBtnClick5()
    {
        RuneBtnClickHandler(EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL);
    }

    /// <summary>
    /// 符文加成按钮点击;
    /// </summary>
    void OnRunePropBtnClick()
    {
        ShowRunePropUI();
    }

    /// <summary>
    /// 符文加成关闭按钮点击;
    /// </summary>
    void OnRunePropCloseBtnClick()
    {
        HideRunePropUI();
    }

    void ShowRunePropUI()
    {
        //_ObjectCard
        //TODO：：得到符文属性List
        foreach (UI_RuneAttriItem attrItem in mPropRuneQueue)
        {
            attrItem.Destroy();
        }
        mPropRuneQueue.Clear();

        Dictionary<int, Pair> attrisDic = HeroRuneModule.CompareAttriDic(_ObjectCard);

        mPropNoRuneObj.SetActive(attrisDic == null || attrisDic.Count == 0);

        for (int i = 0; i < attrisDic.Count; i++)
        {
            mPropRuneQueue.Add(CreateNullRuneAttriUI());
        }

        UI_RuneAttriItem ui_item = null;

        int key = -1;

        for (int i = 0; i < mPropRuneQueue.Count; i++ )
        {
            bool isShow = i < attrisDic.Count;

            ui_item = mPropRuneQueue[i];

            if (ui_item == null)
                continue;

            ui_item.SetActive(isShow);

            if(isShow)
            {
                key = GameUtils.GetKeyByIdx(attrisDic, i);
                ui_item.SetInfo(GameUtils.GetAttriName(key), "+" + attrisDic[key].b);
            }
        }

        mPropRuneObj.SetActive(true);
    }

    UI_RuneAttriItem CreateNullRuneAttriUI()
    {
        Transform trans = (Transform)GameObject.Instantiate(mPropItemTrans);
        if (trans == null)
            return null;

        trans.parent = mPropListTrans;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0f);
        return new UI_RuneAttriItem(trans);
    }

    void HideRunePropUI()
    {
        mPropRuneObj.SetActive(false);
    }

    void RuneBtnClickHandler(EM_RUNE_POINT runeIdx)
    {
        ObjectCard cardInfo = UI_HeroInfoManager._instance.GetCurCard();
        if(cardInfo == null)
        {
            LogManager.LogError("HeroInfoManager GetCurCard返回为null");
            return;
        }
        
        //没装备着符文;
        if (cardInfo.GetHeroData().IsRuneNull(runeIdx))
        {
            HRCMgr.ShowUI(runeIdx);
            RunOnBack();
            HideRuneDetailUI();
        }
        else
        {
            mRunePoint = runeIdx;
            ItemEquip itemE = cardInfo.GetHeroData().GetRuneItemInfo(runeIdx);
            ShowRuneDetailUI(itemE);
        }

        HideRunePropUI();
    }

    public void UpdateRuneDetailUI()
    {
        if (!mRuneDetailObj.activeSelf)
            return;

        ObjectCard cardInfo = UI_HeroInfoManager._instance.GetCurCard();
        ItemEquip itemE = cardInfo.GetHeroData().GetRuneItemInfo(mRunePoint);
        ShowRuneDetailUI(itemE);
    }

    void ShowRuneDetailUI(ItemEquip itemE)
    {
        if (itemE == null)
            return;

        ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(itemE.GetItemTableID());

        mSpecHeroName.text = "";

        HideAllRuneSelectEffect();
        mRunes[(int)mRunePoint].SetEffectShow((EM_RUNE_TYPE)(itemT.getRune_type()));

//         mRuneDetailItem.SetIcon(common.defaultPath + itemT.getIcon());
//         mRuneDetailItem.SetStarsNum(itemT.getRune_quality());
//         mRuneDetailItem.SetLevel(itemE.GetStrenghLevel());
//         mRuneDetailItem.SetLevelInfoActive(true);
//         //bool isSpecial = itemT.getRune_type() == 5 || itemT.getRune_type() == 6;
//         bool isSpecial = RuneModule.IsSpecialRune(itemT);
//         mRuneDetailItem.SetIsSpecial(isSpecial);
//         if (!isSpecial)
//         {
//             mRuneDetailItem.SetRuneType(itemT.getRune_type());
//         }

        if (mItemCommon == null)
        {
            mItemCommon = RuneFactory.CreateRuneItemCommom(m_RuneItemPos);
        }

        RuneItemCommonData ricd = new RuneItemCommonData();
        ricd.IsShowMaxEffect = true;
        ricd.ItemT = itemE.GetItemRowData();
        ricd.RuneLevel = itemE.GetStrenghLevel();
        ricd.EquipedHeroName = RuneModule.GetItemEuipHeroName(itemE);
        mItemCommon.SetRuneItemData(ricd, RuneItemCommon.RuneItemShowType.IconWithRightName);

        //mRuneName.text = GameUtils.getString(itemT.getName());
        mUserObj.SetActive(false);

        if (mDetailCommon == null)
        {
            mDetailCommon = new RuneDetailCommon(m_RuneAttriPos, itemE.GetItemGuid(), 450f);
        }
        else
        {
            mDetailCommon.SetShowData(itemE.GetItemGuid());
        }
//        GameUtils.DestroyChildsObj(mAttriList);
        
//         //基础属性;
//         bool titleDone1 = false;
//         RuneData runeData = itemE.GetRuneData();
//         foreach(int id in runeData.BaseAttributeID)
//         {
//             if(id == -1)
//                 continue;
// 
//             if(!titleDone1)
//             {
//                 titleDone1 = true;
//                 CreateTitle(mAttriList, GameUtils.getString("hero_rune_content8"));
//             }
// 
//             BaseruneattributeTemplate bt = DataTemplate.GetInstance().GetBaseruneattributeTemplate(id);
//             if(bt.getNumshow() == 0)
//             {
//                 //CreateTitle(mAttriList, GameUtils.getString(bt.getAttriDes()));
//                 CreateDetailTxts(mAttriList, GameUtils.getString(bt.getAttriDes()));
//             }
//             else
//             {
//                 //CreateBaseAttriObj(mAttriList, GameUtils.GetAttriName(bt.getAttriType()), "+" + bt.getAttriValue().ToString());
//                 CreateBaseAttriObj(mAttriList, GameUtils.getString(bt.getAttriDes()), "+" + bt.getAttriValue().ToString());
//             }
//         }
// 
//         //附加属性-------激活等级分别为强化等级达到3/6/9/12;
//         bool titleDone2 = false;
         int count = DataTemplate.GetInstance().GetRuneMaxRedefineTimes(itemT);
//         int i = 0;
//         int strenthLv = itemE.GetStrenghLevel();
//         bool isGray = false;
// 
//         foreach (int id in runeData.AppendAttribute)
//         {
//             i++;
//             isGray = i * 3 > strenthLv;
//             if (id == -1)
//             {
//                 if (i <= count)
//                 {
//                     if (!titleDone2)
//                     {
//                         titleDone2 = true;
//                         CreateTitle(mAttriList, GameUtils.getString("hero_rune_content9"));
//                     }
// 
//                     //位置属性，未鉴定;
//                     CreateAddAttriObj(mAttriList, GameUtils.getString("rune_content2"), "", GameUtils.getString("rune_content3"), isGray);
//                 }
//                 
//                 continue;
//             }
// 
//             if(!titleDone2)
//             {
//                 titleDone2 = true;
//                 CreateTitle(mAttriList, GameUtils.getString("hero_rune_content9"));
//             }
// 
//             AddruneattributeTemplate bt = DataTemplate.GetInstance().GetAddruneattributeTemplate(id);
//             bool isPercent = bt.getIspercentage() > 0;
//             string val = isPercent ? ((float)bt.getAttriValue() / (float)10f + "%") : bt.getAttriValue().ToString();
//             CreateAddAttriObj(mAttriList, GameUtils.getString(bt.getAttriDes1()), GameUtils.getString(bt.getAttriDes2()), bt.getSymbol() + val, isGray);
//        }

        //设置按钮状态;
        GameUtils.SetBtnSpriteGrayState(mStrenthBtn, DataTemplate.GetInstance().IsRuneStrenthFullLevel(itemT, itemE.GetStrenghLevel()));
        GameUtils.SetBtnSpriteGrayState(mIdentifyBtn, itemE.GetDefineTimes() >= count);

        mRuneDetailObj.SetActive(true);
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
        //for(int i = 0; i < lineNum; i++)
        //{
        //    startIdx = GlobalMembers.MAX_RUNE_COUNT_PER_LINE * i;
        //    endIdx = GlobalMembers.MAX_RUNE_COUNT_PER_LINE * (i + 1);
        //    if(i == lineNum - 1)
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
    /// 创建属性标题;
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="str"></param>
    void CreateTitle(GameObject parent,string str)
    {
        GameObject go = (GameObject)GameObject.Instantiate(mAttriTitleTxt.gameObject);

        go.transform.parent = parent.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

        go.GetComponent<Text>().text = str;
    }

    /// <summary>
    /// 创建属性;--基础属性;
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="str"></param>
    void CreateBaseAttriObj(GameObject parent, string str1, string str2)
    {
        GameObject go = Instantiate(mRuneAttriObj) as GameObject;

        go.transform.parent = parent.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

        go.transform.FindChild("Left_txt").GetComponent<Text>().text = str1;
        go.transform.FindChild("Right_txt").GetComponent<Text>().text = str2;
    }

    /// <summary>
    /// 创建属性列;--附加属性;
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    void CreateAddAttriObj(GameObject parent, string str1, string str2, string str3, bool isGray)
    {
        GameObject go = Instantiate(mAddRuneAttriObj) as GameObject;

        go.transform.parent = parent.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

        if(isGray)
        {
            str1 = GameUtils.StringWithGrayColor(str1);
            str2 = GameUtils.StringWithGrayColor(str2);
            str3 = GameUtils.StringWithGrayColor(str3);
        }

        go.transform.FindChild("Left_txt").GetComponent<Text>().text = str1;
        go.transform.FindChild("Mid_txt").GetComponent<Text>().text = str2;
        go.transform.FindChild("Right_txt").GetComponent<Text>().text = str3;
    }

    void HideRuneDetailUI()
    {
        mRuneDetailObj.SetActive(false);

        HideAllRuneSelectEffect();
    }

    void HideAllRuneSelectEffect()
    {
        for (int i = 0, j = mRunes.Length; i < j; i++)
        {
            mRunes[i].SetEffectShow(EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID);
        }
    }

    void OnChangeBtnClick()
    {
        HideRuneDetailUI();
        HRCMgr.ShowUI(mRunePoint);
        RunOnBack();
    }


    bool initRuneIdentifyUIDone = false;
    void OnIdentifyBtnClick()
    {
        //HideRuneDetailUI();
        ItemEquip itemE =  _ObjectCard.GetHeroData().GetRuneItemInfo(mRunePoint);
        ItemTemplate itemT = itemE.GetItemRowData();
        //一星符文没法鉴定;
        if(itemT.getRune_quality() <= 1)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip3"), mMsgBoxTrans);
            return;
        }

        //是否鉴定满级;
        int count = DataTemplate.GetInstance().GetRuneMaxRedefineTimes(itemT);

        if (itemE.GetDefineTimes() >= count)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip4"), mMsgBoxTrans);
            return;
        }

        //if (!initRuneIdentifyUIDone)
        //{
        //    initRuneIdentifyUIDone = true;
        //    mRuneIdentifyMgr = new UI_RuneIdentifyMgr(mRuneIdentityObj);
        //}

        //mRuneIdentifyMgr.ShowUI(itemE.GetItemGuid());
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_RuneIdentifyMgr.UI_ResPath);
        UI_RuneIdentifyMgr.SetShowRuneGUID(itemE.GetItemGuid());
        RunOnBack();
    }

    bool initRuneStrenthUIDone = false;
    void OnStrenthBtnClick()
    {
        //HideRuneDetailUI();
        //判断强化是否满级;
        ItemEquip itemE = _ObjectCard.GetHeroData().GetRuneItemInfo(mRunePoint);
        if(itemE == null)
        {
            LogManager.LogError("fuck");
        }
        ItemTemplate itemT = itemE.GetItemRowData();
        int maxLv = DataTemplate.GetInstance().GetRuneStrenthMaxLevel(itemT);

        if(maxLv == -1)
        {
            LogManager.LogError("fuck");
        }

        if (itemE.GetStrenghLevel() >= maxLv)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip2"), mMsgBoxTrans);
            return;
        }
        ShowRuneStrenthUI(itemE);
        RunOnBack();
    }

    void OnCloseRuneDetailBtnClick()
    {
        HideRuneDetailUI();
    }

    /// <summary>
    /// 打开符文强化界面;
    /// </summary>
    void ShowRuneStrenthUI(ItemEquip itemEquip)
    {
        ItemEquip itemE = _ObjectCard.GetHeroData().GetRuneItemInfo(mRunePoint);

        //if (!initRuneStrenthUIDone)
        //{
        //    initRuneStrenthUIDone = true;
        //    mRuneStrenthMgr = new UI_RuneStrenthMgr(mRuneStrenthObj.transform);
        //}

        //mRuneStrenthMgr.ShowUI(itemE.GetItemGuid());
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_RuneStrenthMgr.UI_ResPath);
        UI_RuneStrenthMgr.SetShowRuneGUID(itemE.GetItemGuid());
    }

    /// <summary>
    /// 关闭符文强化界面;
    /// </summary>
    void HideRuneStrenthUI()
    {
        
    }

    public void ShowUI()
    {
       
        this.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        if (this.gameObject != null )
        {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 当卡牌英雄数据发生改变的时候;
    /// </summary>
    /// <param name="ev"></param>
    void OnCardHeroDataChanged(GameEvent ev)
    {
        if (mObjectCard == null)
        {
            return;
        }

        if (ev == null || ev.data == null)
        {
            LogManager.LogError("卡牌数据变动信息为null");
            return;
        }

        int heroKey = (int)ev.data;

        if((int)(mObjectCard.GetGuid().GUID_value) == heroKey)
        {
            //刷新当前界面;
            UpdateUIForm();
        }
    }

    void OnSelectCardHeroChanged(GameEvent ev)
    {
        if(ev == null || ev.data == null)
        {
            LogManager.LogError("当前选中的英雄为空");
            return;
        }

        ObjectCard card = ev.data as ObjectCard;

        if(card == null)
        {
            LogManager.LogError("当前选中的英雄为空");
            return;
        }

        _ObjectCard = card;

        //card.GetHeroData().GetRuneItemInfo()
        //System.Array array = System.Enum.GetValues(typeof(EM_RUNE_POINT));

        
    }

    /// <summary>
    /// 刷新所有有关符文装配界面的信息;
    /// </summary>
    void UpdateUIForm()
    {
        List<int> equipedTypes = new List<int>();
        for (int i = 0, j = (int)(EM_RUNE_POINT.EM_RUNE_POINT_NUMBER); i < j; i++)
        {
            EM_RUNE_POINT erp = (EM_RUNE_POINT)i;
            ItemEquip runeInfo = mObjectCard.GetHeroData().GetRuneItemInfo(erp);

            mRunes[i].SetRuneType(EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID);
            if (runeInfo == null)
            {
                //mRunes[i].SetIcon(common.defaultPath + AddSpriteName);
                mRunes[i].SetIcon(AddSprite);
                mRunes[i].SetLevelInfoActive(false);
            }
            else
            {
                ItemTemplate runeTemp = DataTemplate.GetInstance().GetItemTemplateById(runeInfo.GetItemTableID());
                if (runeTemp != null)
                {
                    mRunes[i].SetIcon(common.defaultPath + runeTemp.getIcon());
                    //mRunes[i].SetLevel(runeInfo.)
                    mRunes[i].SetStarsNum(runeTemp.getRune_quality());
                    mRunes[i].SetLevel(runeInfo.GetStrenghLevel());
                    int runeType = runeTemp.getRune_type();
                    //if (!equipedTypes.Contains(runeType))
                    equipedTypes.Add(runeType);
                    
                    bool isSpecial = i == (int)(EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL);

                    if (!isSpecial)
                    {
                          mRunes[i].SetRuneType(runeType);
                    }
                }
                
                mRunes[i].SetLevelInfoActive(true);
            }
        }

        int heroTableID = mObjectCard.GetHeroData().TableID;
        HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(heroTableID);
        if(heroT == null)
        {
            LogManager.LogError("hero表中缺少数据 heroid = " + heroTableID);
            return;
        }

        List<int> runeGroup = DataTemplate.GetInstance().GetHeroRuneGroup(heroT);
        if (runeGroup == null)
        {
            LogManager.LogError("获取英雄符文组合失败 heroid = " + heroTableID);
            return;
        }

        if(runeGroup.Count > 4)
        {
            LogManager.LogError("符文组合最多4个符文");
            return;
        }

        for (int i = 0, j = mRuneGoupItems.Length; i < j; i++ )
        {
            mRuneGoupItems[i].SetActive(false);
        }

        for (int i = 0, j = runeGroup.Count; i < j; i++)
        {
            //mRuneGoupItems[i].SetIcon(GetIconByRuneType(runeGroup[i]));
            mRuneGoupItems[i].SetIcon(GetSpriteByRuneFlagType(runeGroup[i]));
            mRuneGoupItems[i].SetEffectShow(EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID);
            mRuneGoupItems[i].SetActive(true);
        }

        int activeNum = 0;

        for(int i = 0; i < runeGroup.Count ; i++)
        {
            int idx = equipedTypes.IndexOf(runeGroup[i]);

            if (idx == -1)
            {
                mRuneGoupItems[i].SetEffectShow(EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID);
                continue;
            }
            else
            {
                mRuneGoupItems[i].SetEffectShow((EM_RUNE_TYPE)runeGroup[i]);
                equipedTypes.RemoveAt(idx);
                activeNum++;
            }
        }

        bool isActive = activeNum >= runeGroup.Count; //是否激活符文组合条件;

        RunepassiveTemplate runPassT = DataTemplate.GetInstance().GetRunepassiveTemplate(heroT.getRunePassive());
        if(runPassT != null)
        {
            string str1 = "";
            bool isShow1 = GetRuneGroupValueStr(runPassT, 0, out str1);
            mRuneValues[0].text = isActive ? str1 : "<color=#acacac>" + str1 + "</color>";
            mRuneValues[0].transform.parent.gameObject.SetActive(isShow1);
            mRuneDetails[0].text = isShow1 ? GameUtils.getString(runPassT.getDes1()) : "";
            mRuneAttrImgs[0].sprite = RuneAttrImgs[isActive ? 1 : 0];

            string str2 = "";
            bool isShow2 = GetRuneGroupValueStr(runPassT, 1, out str2);
            mRuneValues[1].text = isActive ? str2 : "<color=#acacac>" + str2 + "</color>";
            mRuneValues[1].transform.parent.gameObject.SetActive(isShow2);
            mRuneDetails[1].text = isShow2 ? GameUtils.getString(runPassT.getDes2()) : "";
            mRuneAttrImgs[1].sprite = RuneAttrImgs[isActive ? 1 : 0];

            string str3 = "";
            bool isShow3 = GetRuneGroupValueStr(runPassT, 2, out str3);
            mRuneValues[2].text = isActive ? str3 : "<color=#acacac>" + str3 + "</color>";
            mRuneValues[2].transform.parent.gameObject.SetActive(isShow3);
            mRuneDetails[2].text = isShow3 ? GameUtils.getString(runPassT.getDes3()) : "";
            mRuneAttrImgs[2].sprite = RuneAttrImgs[isActive ? 1 : 0];
        }
    }

    void OnItemRefresh(GameEvent ge)
    {
        if (ge == null || ge.data == null)
            return;

        Item i = ge.data as Item;

        if (i == null)
            return;

        X_GUID tmp = new X_GUID();
        tmp.GUID_value = i.key;
        
        if(_ObjectCard.GetHeroData().IsItemEquiped(tmp))
            UpdateAllRuneItems();
    }

    /// <summary>
    /// 刷新符文显示--符文强化成功监听;
    /// </summary>
    void UpdateAllRuneItems()
    {
        List<int> equipedTypes = new List<int>();
        for (int i = 0, j = (int)(EM_RUNE_POINT.EM_RUNE_POINT_NUMBER); i < j; i++)
        {
            EM_RUNE_POINT erp = (EM_RUNE_POINT)i;
            ItemEquip runeInfo = mObjectCard.GetHeroData().GetRuneItemInfo(erp);

            if (runeInfo == null)
            {
                //mRunes[i].SetIcon(common.defaultPath + AddSpriteName);
                mRunes[i].SetIcon(AddSprite);
                mRunes[i].SetLevelInfoActive(false);
            }
            else
            {
                ItemTemplate runeTemp = DataTemplate.GetInstance().GetItemTemplateById(runeInfo.GetItemTableID());
                if (runeTemp != null)
                {
                    mRunes[i].SetIcon(common.defaultPath + runeTemp.getIcon());
                    //mRunes[i].SetLevel(runeInfo.)
                    mRunes[i].SetStarsNum(runeTemp.getRune_quality());
                    mRunes[i].SetLevel(runeInfo.GetStrenghLevel());

                    int runeType = runeTemp.getRune_type();
                    //if (!equipedTypes.Contains(runeType))
                    equipedTypes.Add(runeType);

                    bool isSpecial = i == (int)(EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL);

                    //if (!isSpecial)
                    //{
                    //    mRuneDetailItem.SetRuneType(runeType);
                    //}
                }

                mRunes[i].SetLevelInfoActive(true);
            }
        }
    }

    bool isDetailEffect(string detail)
    {
        return !(string.IsNullOrEmpty(detail) || detail.Equals("-1"));
    }

    int GetValueNumInList(List<int> datas, int value)
    {
        if (datas == null || datas.Count == 0)
            return 0;

        int result = 0;
        for (int i = 0; i < datas.Count; i++ )
        {
            if (datas[i] == value)
                result++;
        }
            
        return result;
    }

    List<int> GetValueAllIdxInList(List<int> datas, int value)
    {
        if (datas == null || datas.Count == 0)
            return null;

        List<int> result = new List<int>();
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i] == value)
                result.Add(i);
        }

        return result;
    }

    bool GetRuneGroupValueStr(RunepassiveTemplate runeT, int idx, out string result)
    {
        result = "";
        int attri = -1;

        int isPercent = 1;
        string Symbol = "";
        int value = 0;

        switch(idx)
        {
            case 0:
                isPercent = runeT.getType1();
                Symbol = runeT.getSymbol1();
                value = runeT.getValue1();
                attri = runeT.getAttribute1();
                break;
            case 1:
                isPercent = runeT.getType2();
                Symbol = runeT.getSymbol2();
                value = runeT.getValue2();
                attri = runeT.getAttribute2();
                break;
            case 2:
                isPercent = runeT.getType3();
                Symbol = runeT.getSymbol3();
                value = runeT.getValue3();
                attri = runeT.getAttribute3();
                break;
        }

        if(isPercent == 1)
        {
            result = Symbol + ((float)(value) / 10f).ToString() + "%";
        }
        else
        {
            result = Symbol + value;
        }
        
        return attri != -1; 
    }

    //string GetIconByRuneType(int type)
    //{
    //    return common.defaultPath + RuneTypeImages[type - 1];
    //}

    //Sprite GetIconSpriteByRuneType(int type)
    //{
    //    return RuneTypeImages[type - 1];
    //}
    Sprite GetSpriteByRuneFlagType(int type)
    {
        return RuneTypeImages[type - 1];
    }
    
    private void RefreshTips()
    {
        if (mObjectCard == null)
            return;

        if (m_FunctionTipsManager == null)
        {
            m_FunctionTipsManager = FunctionTipsManager.GetInstance();
            if (m_FunctionTipsManager == null)
                return;
        }

        if (m_FunctionTipsManager.CheckHeroIsInDefaultTeam())
        {
            RefreshRuneTips();
        }
        else
        {
            CloseAllTips();
        }
    
    }
    private void RefreshRuneTips()
    {
        if (m_FunctionTipsManager.CheckCurrentHeroRune())
        {
            var _heroData = mObjectCard.GetHeroData();
            if (m_FunctionTipsManager.m_HaveIdleRune)
            {
                for (int i = 0; i < (int)EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL; i++)
                {
                    m_TipsImageArray[i].SetActive(_heroData.IsRuneNull((EM_RUNE_POINT)i));
                }
            }
            if (m_FunctionTipsManager.m_HaveIdleSpecialRune)
                m_TipsImageArray[(int)EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL].SetActive(_heroData.IsRuneNull(EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL));
            else
                m_TipsImageArray[(int)EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL].SetActive(false);

        }
        else
        {
            CloseAllTips();
        }
    }
    private void CloseAllTips()
    {
        for (int i = 0; i < m_TipsImageArray.Length; i++)
        {
            m_TipsImageArray[i].SetActive(false);
        }
    }
}
