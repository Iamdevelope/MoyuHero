using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;

using GNET;

public enum RuneItemShowType
{
    Normal,
    Null,
    Locked
}

public class UIRuneChangeRuneItemShowData
{
    public BaseItem baseItem = null;
    public RuneItemShowType showType = RuneItemShowType.Normal;
}

#region 符文+星级+等级小UI;

public class UI_RuneItem : CellItem
{
    private Button RuneBtn = null;
    private GameObject LockedImg = null;
    private Image EquipedImg = null;
    private Image RuneImg = null;
    private GameObject RuneBgImgGo = null;
    private GameObject RuneSpecBgImgGo = null;
    private Text RuneName = null;
    private Text RuneLv = null;
    private GameObject RuneLvGo = null;
    private GameObject SelectedImg = null;
    private Image [] StarsImg = null;
    private GameObject[] StarsBgImgGo = null;
    private Image[] TypeImgs = null;

    private Transform mTrans = null;
    private X_GUID mGuid = null;
    private UnityAction runeClickAction = null;

    public UI_RuneItem ( Transform trans, X_GUID guid )
    {
        mTrans = trans;
        mGuid = guid;

        RuneBtn = trans.GetComponent<Button> ();
        LockedImg = trans.FindChild("Parent/LockImg").gameObject;
        EquipedImg = trans.FindChild ( "Parent/Equip" ).GetComponent<Image> ();
        RuneBgImgGo = trans.FindChild("Parent/BgIcon").gameObject;
        RuneSpecBgImgGo = trans.FindChild("Parent/BgIconSpec").gameObject;
        RuneImg = trans.FindChild ( "Parent/Icon" ).GetComponent<Image> ();
        RuneName = trans.FindChild ( "Parent/Name" ).GetComponent<Text> ();
        RuneLv = trans.FindChild ( "Parent/Level/Text" ).GetComponent<Text> ();
        RuneLvGo = trans.FindChild("Parent/Level").gameObject;
        SelectedImg = trans.FindChild ( "Parent/Border" ).gameObject;

        StarsImg = new Image [ UI_RuneChangePageManager.MaxStarCount ];
        StarsBgImgGo = new GameObject[UI_RuneChangePageManager.MaxStarCount];
        for ( int i = 0; i < UI_RuneChangePageManager.MaxStarCount; i++ )
        {
            StarsImg [ i ] = trans.FindChild ( "Parent/Star_" + ( i + 1 ) ).GetComponent<Image> ();
            StarsBgImgGo[i] = trans.FindChild("Parent/StarBg_" + (i + 1)).gameObject;
        }

        runeClickAction = new UnityAction ( OnRuneClick );
        RuneBtn.onClick.AddListener ( runeClickAction );
    }

    public void Init ( Transform trans, X_GUID guid )
    {
        mTrans = trans;
        mGuid = guid;

        RuneBtn = trans.GetComponent<Button>();
        LockedImg = trans.FindChild("Parent/LockImg").gameObject;
        EquipedImg = trans.FindChild("Parent/Equip").GetComponent<Image>();
        RuneBgImgGo = trans.FindChild("Parent/BgIcon").gameObject;
        RuneSpecBgImgGo = trans.FindChild("Parent/BgIconSpec").gameObject;
        RuneImg = trans.FindChild("Parent/Icon").GetComponent<Image>();
        RuneName = trans.FindChild("Parent/Name").GetComponent<Text>();
        RuneLv = trans.FindChild("Parent/Level/Text").GetComponent<Text>();
        RuneLvGo = trans.FindChild("Parent/Level").gameObject;
        SelectedImg = trans.FindChild("Parent/Border").gameObject;

        StarsImg = new Image[UI_RuneChangePageManager.MaxStarCount];
        StarsBgImgGo = new GameObject[UI_RuneChangePageManager.MaxStarCount];
        for (int i = 0; i < UI_RuneChangePageManager.MaxStarCount; i++)
        {
            StarsImg[i] = trans.FindChild("Parent/Star_" + (i + 1)).GetComponent<Image>();
            StarsBgImgGo[i] = trans.FindChild("Parent/StarBg_" + (i + 1)).gameObject;
        }

        TypeImgs = new Image[4];
        for (int i = 0; i < 4; i++ )
        {
            TypeImgs[i] = trans.FindChild("Parent/type" + (i + 1)).GetComponent<Image>();
        }

        runeClickAction = new UnityAction ( OnRuneClick );
        RuneBtn.onClick.AddListener ( runeClickAction );
    }

    public void SetClickable(bool interactable)
    {
        RuneBtn.interactable = interactable;
    }

    public void Destroy ()
    {
        RuneBtn.onClick.RemoveAllListeners ();
        GameObject.DestroyImmediate ( mTrans.gameObject );
    }

    public void SetEquiped ( bool equiped )
    {
        if ( EquipedImg.gameObject.activeSelf != equiped )
            EquipedImg.gameObject.SetActive ( equiped );
    }

    public void SetIsSpec(bool isSpec)
    {
        RuneBgImgGo.SetActive(!isSpec);
        RuneSpecBgImgGo.SetActive(isSpec);
    }

    public void SetSelected ( bool selected )
    {
        if ( SelectedImg.activeSelf != selected )
            SelectedImg.SetActive ( selected );
    }

    public void SetLocked(bool isLocked)
    {
        LockedImg.gameObject.SetActive(isLocked);
    }

    public void SetLevel ( int level )
    {
        if (level >= 0)
        {
            RuneLvGo.SetActive(true);
            RuneLv.text = level.ToString ();
        }
        else
        {
            RuneLvGo.SetActive(false);
        }
    }

    public void SetType(int runeType)
    {
        for (int i = 0; i < 4; i++ )
        {
            TypeImgs[i].gameObject.SetActive(false);
        }

        if (runeType >= 1 && runeType <= 4)
        {
            TypeImgs[runeType - 1].gameObject.SetActive(true);
        }
    }

    public void SetStarNum ( int starNum )
    {
        for ( int i = 0; i < StarsImg.Length; i++ )
        {
            StarsImg [ i ].gameObject.SetActive ( i < starNum );
        }
    }
    
    public void SetStarBgNum(int starBgNum)
    {
        for (int i = 0; i < StarsBgImgGo.Length; i++)
        {
            StarsBgImgGo[i].gameObject.SetActive(i < starBgNum);
        }
    }

    public void SetName ( string name )
    {
        RuneName.text = name;
    }

    public void SetIconImg ( string iconName )
    {
        if (string.IsNullOrEmpty(iconName))
        {
            RuneImg.gameObject.SetActive(false);
            RuneBgImgGo.SetActive(false);
        }
        else
        {
            RuneImg.gameObject.SetActive(true);
            RuneBgImgGo.SetActive(true);
            RuneImg.sprite = UIResourceMgr.LoadSprite ( iconName );
            RuneImg.SetNativeSize ();
        }
    }

    void OnRuneClick ()
    {
        if ( mGuid == null )
            return;
        UI_RuneChangePageManager._instance.SetSelectedRune ( mGuid );

        //新手引导相关 选择【符文】
        if ( GuideManager.GetInstance ().GetBackCount ( 200105 ) )
        {
            GuideManager.GetInstance ().ShowGuideWithIndex ( 200106 );
        }
    }

    public void SelectThis ()
    {
        OnRuneClick ();
    }
}
#endregion;

#region 符文属性小UI;
public class UI_RuneAttriItem
{
    protected Text title = null;
    protected Text number = null;

    private Transform mTrans = null;

    public UI_RuneAttriItem ( Transform trans )
    {
        if ( trans == null )
            return;

        mTrans = trans;

        title = mTrans.FindChild ( "Left_txt" ).GetComponent<Text> ();
        number = mTrans.FindChild ( "Right_text" ).GetComponent<Text> ();

        //title.supportRichText = true;
        //number.supportRichText = true;
    }

    public void SetInfo ( string str1, string str2 )
    {
        Clean ();
        title.text = str1;
        number.text = str2;
    }

    public void Destroy ()
    {
        GameObject.DestroyImmediate ( mTrans.gameObject );
    }

    void Clean ()
    {
        title.text = "";
        number.text = "";
    }

    public void SetActive ( bool active )
    {
        mTrans.gameObject.SetActive ( active );
    }
}

//有3个text 的属性描述;
public class UI_RuneAttriItemThr
{
    protected Text title = null;
    protected Text detail = null;
    protected Text number = null;

    private Transform mTrans = null;

    public UI_RuneAttriItemThr ( Transform trans )
    {
        if ( trans == null )
            return;

        mTrans = trans;

        title = mTrans.FindChild ( "Left_txt" ).GetComponent<Text> ();
        detail = mTrans.FindChild ( "Mid_txt" ).GetComponent<Text> ();
        number = mTrans.FindChild ( "Right_txt" ).GetComponent<Text> ();

        //title.supportRichText = true;
        //number.supportRichText = true;
    }

    public void SetInfo ( string str1, string str2, string str3 )
    {
        Clean ();
        title.text = str1;
        detail.text = str2;
        number.text = str3;
    }

    public void Destroy ()
    {
        GameObject.DestroyImmediate ( mTrans.gameObject );
    }

    void Clean ()
    {
        title.text = "";
        detail.text = "";
        number.text = "";
    }

    public void SetActive ( bool active )
    {
        mTrans.gameObject.SetActive ( active );
    }
}

public class UI_RuneAttriGroup
{
    protected Text title = null;

    protected List<UI_RuneAttriItem> attriItems = null;

    private Transform mTrans = null;
    public UI_RuneAttriGroup ( Transform trans )
    {
        if ( trans == null )
            return;

        mTrans = trans;

        title = mTrans.FindChild ( "txt" ).GetComponent<Text> ();
    }

    public void SetTitle ( string str )
    {
        title.text = str;
    }
}
#endregion



public class UI_RuneChangePageManager : BaseUI
{
    public static readonly int MaxStarCount = 5;

    public static UI_RuneChangePageManager _instance;

    public const int MaxPerLin = 3;     //一行有3个;
    public const int MaxPerPage = 9;    //当前页最多显示9个;

    protected Button mCloseBtn = null;
    protected Button mAddBtn = null;
    protected Button mSortBtn = null;
    protected Text mSortTxt = null;
    protected Button mSprtBtn_All = null;
    protected Button mSprtBtn_Orange = null;
    protected Button mSprtBtn_Red = null;
    protected Button mSprtBtn_Green = null;
    protected Button mSprtBtn_Purple = null;
    protected Button mSprtBtn_Blue = null;
    protected UI_SlideBtn mSlide = null;
    protected Text mAttriTitleTxt = null;
    protected Text mTitleTxt_1 = null;
    protected Text mTitleTxt_2 = null;
    protected GameObject mAttriDetailTxt = null;

    protected Text mCurCountTxt = null;
    protected Text mMaxCountTxt = null;
    protected Text mBagNumText = null;
    //protected Image mRuneImg = null;
    protected Text mRuneName = null;
    protected RuneIconItem mUIRuneItem = null;
    protected Text mSpeHeroTxt = null;
    protected GameObject mUserObj = null;
    protected Text mUserName = null;
    protected Text mNoneHint = null;
    protected Button mChangeBtn = null;
    protected Text mChangeBtnTxt = null;
    protected Button mEquipBtn = null;
    protected Text mEquipBtnTxt = null;
    protected GameObject mRuneItemObj = null;
    protected GameObject mRuneParentObj = null;
    protected GameObject mDetailObj = null;
    protected GameObject mAtrriListObj1 = null;                 //符文属性显示父obj;
    protected GameObject mAtrriListObj2 = null;                 //符文对比属性显示父obj;
    protected GameObject mAttriTitleObj = null;                 //符文标题属性;
    protected GameObject mRuneAttriObj = null;                  //符文属性展示;
    protected GameObject mRuneAttriObj1 = null;                 //符文对比属性展示列;
    protected GameObject mRuneAttriObj2 = null;                 //符文对比属性显示;
    protected LoopLayout mRuneLayout = null;                    //符文列表布局

    //没有符文提示页面;
    protected GameObject mBottomObj = null;
    protected GameObject mScrollRectObj = null;
    protected GameObject mNullObj = null;
    protected Text mNullHintTxt = null;
    protected Button mNullBtnLeft = null;
    protected Button mNullBtnRight = null;

    //人物属性没变化;
    protected GameObject mNoChangeObj = null;
    protected Text mNoChangeTxt = null;

    private GameObject m_BagAddPanel; //背包扩充界面

    //protected GridLayoutGroup mBaseAttriLayout = null;          //基础属性;
    //protected GridLayoutGroup mAddAttriLayout = null;           //附加属性;
    //protected GridLayoutGroup mAfterAttriLayout = null;         //更换后属性;

    private EM_RUNE_POINT mPoint = EM_RUNE_POINT.EM_RUNE_POINT_INVALID;
    private ObjectCard mObjCard = null;
    private EM_SORT_RUNE_ITEM mSort = EM_SORT_RUNE_ITEM.EM_SORT_RUNE_INVALID;
    //private Dictionary<X_GUID, UI_RuneItem> mRuneItems = new Dictionary<X_GUID, UI_RuneItem>();
    private Dictionary<X_GUID, RuneItemCommon> mRuneItems = new Dictionary<X_GUID, RuneItemCommon>();
    private X_GUID mSelectedRuneGUID = null;
    protected List<UI_RuneAttriItemThr> mPropRuneQueue = null;
    protected List<UIRuneChangeRuneItemShowData> mRuneList = new List<UIRuneChangeRuneItemShowData>();
    private bool initDataDone = false;
    private bool initViewDone = false;

    protected RuneItemCommon mItemCommon = null;
    protected RuneDetailCommon mDetailCommon = null;
    protected Transform m_RuneAttriPos = null;
    protected Transform m_RuneItemPos = null;

    X_GUID SelectedRuneGUID
    {
        get
        {
            return mSelectedRuneGUID;
        }
        set
        {
            if ( value == mSelectedRuneGUID )
                return;

            mSelectedRuneGUID = value;
            OnRuneItemChanged ();
        }
    }

    public override void InitUIData ()
    {
        if ( initDataDone )
            return;

        initDataDone = true;

        base.InitUIData ();
        _instance = this;

        m_RuneAttriPos = selfTransform.FindChild("RuneChange_Page/RuneInfo/Panel/RuneAttriPos");

        mCloseBtn = transform.FindChild("RuneChange_Page/TopPanel/BackBtn").GetComponent<Button>();
        mAddBtn = transform.FindChild("RuneChange_Page/Bottom/SortObj/NumberBg/add").GetComponent<Button>();
        mSortBtn = transform.FindChild ( "RuneChange_Page/Bottom/SortObj/MainBagBtn" ).GetComponent<Button> ();
        mSortTxt = transform.FindChild("RuneChange_Page/Bottom/SortObj/MainBagBtn/Text").GetComponent<Text>();
        mSprtBtn_All = transform.FindChild("RuneChange_Page/Bottom/SortObj/AllBtn").GetComponent<Button>();
        mSprtBtn_Orange = transform.FindChild("RuneChange_Page/Bottom/SortObj/SpectialBtn").GetComponent<Button>();
        mSprtBtn_Red = transform.FindChild("RuneChange_Page/Bottom/SortObj/RedBtn").GetComponent<Button>();
        mSprtBtn_Green = transform.FindChild("RuneChange_Page/Bottom/SortObj/GreenBtn").GetComponent<Button>();
        mSprtBtn_Purple = transform.FindChild("RuneChange_Page/Bottom/SortObj/PurpleBtn").GetComponent<Button>();
        mSprtBtn_Blue = transform.FindChild("RuneChange_Page/Bottom/SortObj/BlueBtn").GetComponent<Button>();
        mSlide = transform.FindChild("RuneChange_Page/Bottom/SortObj/MainBagBtn").GetComponent<UI_SlideBtn>();
        mAttriTitleTxt = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/Title/txt" ).GetComponent<Text> ();
        mTitleTxt_1 = transform.FindChild ( "RuneChange_Page/TopPanel/BtnGroup/TitleButton_0/NoSelect/Text" ).GetComponent<Text> ();
        mTitleTxt_2 = transform.FindChild("RuneChange_Page/TopPanel/BtnGroup/TitleButton_0/Image/Text").GetComponent<Text>();
        mAttriDetailTxt = transform.FindChild ( "Items/LineTxt" ).gameObject;
        mCurCountTxt = transform.FindChild("RuneChange_Page/Bottom/SortObj/NumberBg/CurPlayers_txt").GetComponent<Text>();
        mMaxCountTxt = transform.FindChild("RuneChange_Page/Bottom/SortObj/NumberBg/MaxPlayers_txt").GetComponent<Text>();
        mBagNumText = transform.FindChild("RuneChange_Page/Bottom/SortObj/NumberBg/BagCount_txt").GetComponent<Text>();
        //mRuneImg = transform.FindChild("RuneChange_Page/RuneInfo/Panel/RunItem1/RuneIconList/Empty_Icon/Image").GetComponent<Image>();
        m_RuneItemPos = transform.FindChild("RuneChange_Page/RuneInfo/Panel/RunItem");
        /*mRuneName = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/RuneName_txt/Name_txt" ).GetComponent<Text> (); */
        Transform runeItemTrans = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/RunItem" );
        mUIRuneItem = new RuneIconItem ( runeItemTrans );
        mSpeHeroTxt = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/SpecialHeroName" ).GetComponent<Text> ();
        mUserObj = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/RuneName" ).gameObject;
        mUserName = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/RuneName/UserName_txt" ).GetComponent<Text> ();
        mNoneHint = transform.FindChild ( "RuneChange_Page/RuneInfo/Text" ).GetComponent<Text> ();
        mChangeBtn = transform.FindChild ( "RuneChange_Page/RuneInfo/ChangeBtn" ).GetComponent<Button> ();
        mChangeBtnTxt = transform.FindChild ( "RuneChange_Page/RuneInfo/ChangeBtn/Text" ).GetComponent<Text> ();
        mEquipBtn = transform.FindChild ( "RuneChange_Page/RuneInfo/EquipBtn" ).GetComponent<Button> ();
        mEquipBtnTxt = transform.FindChild ( "RuneChange_Page/RuneInfo/EquipBtn/Text" ).GetComponent<Text> ();
        mRuneItemObj = transform.FindChild ( "Items/RuneItem" ).gameObject;
        mRuneParentObj = transform.FindChild ( "RuneChange_Page/RuneList/ListLayOut" ).gameObject;
        mDetailObj = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel" ).gameObject;
        mAtrriListObj1 = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/Attris1/AttriList" ).gameObject;
        mAtrriListObj2 = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/Attris2/AttriList" ).gameObject;
        mAttriTitleObj = transform.FindChild ( "Items/AttriTitle" ).gameObject;
        mRuneAttriObj = transform.FindChild ( "Items/AddAttriPair" ).gameObject;
        mRuneAttriObj1 = transform.FindChild ( "Items/AttriPair1" ).gameObject;
        //mRuneAttriObj2 = transform.FindChild("Items/AttriPair2").gameObject;
        mRuneAttriObj2 = transform.FindChild ( "Items/AddAttriPair" ).gameObject;
        mRuneLayout = transform.FindChild ( "RuneChange_Page/RuneList/ListLayOut" ).gameObject.GetComponent<LoopLayout> ();

        //没有符文提示;
        mBottomObj = transform.FindChild ( "RuneChange_Page/Bottom" ).gameObject;
        mScrollRectObj = transform.FindChild ( "RuneChange_Page/RuneList" ).gameObject;
        mNullObj = transform.FindChild ( "RuneChange_Page/NullObj" ).gameObject;
        mNullHintTxt = transform.FindChild ( "RuneChange_Page/NullObj/Hint/Text" ).GetComponent<Text> ();
        mNullBtnLeft = transform.FindChild ( "RuneChange_Page/NullObj/LeftBtn" ).GetComponent<Button> ();
        mNullBtnRight = transform.FindChild ( "RuneChange_Page/NullObj/RightBtn" ).GetComponent<Button> ();

        mNoChangeObj = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/NoChangeObj" ).gameObject;
        mNoChangeTxt = transform.FindChild ( "RuneChange_Page/RuneInfo/Panel/NoChangeObj/Text" ).GetComponent<Text> ();

        m_BagAddPanel = transform.FindChild("UI_BagAdd").gameObject;

        mPropRuneQueue = new List<UI_RuneAttriItemThr> ();

        mCloseBtn.onClick.AddListener ( OnCloseBtnClick );
        mAddBtn.onClick.AddListener ( OnAddBtnClick );
        mSortBtn.onClick.AddListener ( OnSortBtnClick );
        mChangeBtn.onClick.AddListener ( OnChangeBtnClick );
        mEquipBtn.onClick.AddListener ( OnEquipBtnClick );

        mSprtBtn_All.onClick.AddListener ( OnSortAllClick );
        mSprtBtn_Orange.onClick.AddListener ( OnSortOrangeClick );
        mSprtBtn_Red.onClick.AddListener ( OnSortRedClick );
        mSprtBtn_Green.onClick.AddListener ( OnSortGreenClick );
        mSprtBtn_Purple.onClick.AddListener ( OnSortPurpleClick );
        mSprtBtn_Blue.onClick.AddListener ( OnSortBlueClick );

        mNullBtnLeft.onClick.AddListener ( OnNullLeftBtnClick );
        mNullBtnRight.onClick.AddListener ( OnNullRightBtnClick );
        //GameEventDispatcher.Inst.addEventListener(GameEventID.KE_KnapsackUpdateShow, OnRefreshRuneItem);
        GameEventDispatcher.Inst.addEventListener ( GameEventID.G_Guide_Continue, ShowNewGuide );
        GameEventDispatcher.Inst.addEventListener(GameEventID.KE_BagItemSizeShow, BagItemSizeShow);
    }

    /// <summary>
    /// 监测新手引导 点击继续
    /// </summary>
    /// <param name="e"></param>
    private void ShowNewGuide ( GameEvent e )
    {
        int _id = ( int ) e.data;
        if ( _id != -1 )
        {
            //新手引导相关 【点击继续】200107
            GuideManager.GetInstance ().ShowGuideWithIndex ( _id );
        }
        else
        {
            GuideManager.GetInstance ().StopGuide ();
        }
    }

    public override void InitUIView ()
    {
        if ( initViewDone )
            return;

        initViewDone = true;

        base.InitUIView ();
        this.gameObject.SetActive ( false );
        //HideUI();

        InitStringData ();
        UpdateBagCountTxt ();
        BagItemSizeShow();

        //SetRuneListShowType(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL);
    }

    void InitStringData ()
    {
        mNoneHint.text = GameUtils.getString ( "hero_rune_content12" );
        mChangeBtnTxt.text = GameUtils.getString ( "common_button_change" );
        mEquipBtnTxt.text = GameUtils.getString ( "hero_rune_content11" );
        mSortTxt.text = GameUtils.getString ( "hero_rune_content14" );
        mSprtBtn_All.transform.FindChild ( "Text" ).GetComponent<Text> ().text = GameUtils.getString ( "hero_rune_content14" );
        mSprtBtn_Orange.transform.FindChild ( "Text" ).GetComponent<Text> ().text = GameUtils.getString ( "hero_rune_content15" );
        mSprtBtn_Red.transform.FindChild ( "Text" ).GetComponent<Text> ().text = GameUtils.getString ( "hero_rune_content16" );
        mSprtBtn_Green.transform.FindChild ( "Text" ).GetComponent<Text> ().text = GameUtils.getString ( "hero_rune_content17" );
        mSprtBtn_Purple.transform.FindChild ( "Text" ).GetComponent<Text> ().text = GameUtils.getString ( "hero_rune_content18" );
        mSprtBtn_Blue.transform.FindChild ( "Text" ).GetComponent<Text> ().text = GameUtils.getString ( "hero_rune_content19" );
        mNullHintTxt.text = GameUtils.getString ( "hero_rune_content20", true );
        mNoChangeTxt.text = GameUtils.getString ( "hero_rune_content24" );
        mNullBtnLeft.transform.FindChild ( "Text" ).GetComponent<Text> ().text = GameUtils.getString ( "hero_rune_content21" );
        mNullBtnRight.transform.FindChild ( "Text" ).GetComponent<Text> ().text = GameUtils.getString ( "hero_rune_content22" );

       
    }
    void BagItemSizeShow()
    {
        var player = ObjectSelf.GetInstance();
        if (player.CommonItemContainer.GetBagItemSum() <= player.CommonItemContainer.GetBagItemSizeMax())
        {
            mCurCountTxt.text = player.CommonItemContainer.GetBagItemSum().ToString();
            mMaxCountTxt.text = "/"+player.CommonItemContainer.GetBagItemSizeMax().ToString();
        }
        else
        {
            mCurCountTxt.text = "<color=red>" + player.CommonItemContainer.GetBagItemSum() + "</color>/";
            mMaxCountTxt.text = "/"+player.CommonItemContainer.GetBagItemSizeMax().ToString();
        }
    }

    void OnEnable ()
    {
        UpdateAllRuneItemsUI ();
    }

    void OnDestroy ()
    {
        //mCloseBtn.onClick.RemoveListener ( OnCloseBtnClick );
        //mAddBtn.onClick.RemoveListener ( OnAddBtnClick );
        //mSortBtn.onClick.RemoveListener ( OnSortBtnClick );
        //mChangeBtn.onClick.RemoveListener ( OnChangeBtnClick );
        //mEquipBtn.onClick.RemoveListener ( OnEquipBtnClick );

        //mSprtBtn_All.onClick.RemoveListener ( OnSortAllClick );
        //mSprtBtn_Orange.onClick.RemoveListener ( OnSortOrangeClick );
        //mSprtBtn_Red.onClick.RemoveListener ( OnSortRedClick );
        //mSprtBtn_Green.onClick.RemoveListener ( OnSortGreenClick );
        //mSprtBtn_Purple.onClick.RemoveListener ( OnSortPurpleClick );
        //mSprtBtn_Blue.onClick.RemoveListener ( OnSortBlueClick );

        //mNullBtnLeft.onClick.RemoveListener ( OnNullLeftBtnClick );
        //mNullBtnRight.onClick.RemoveListener ( OnNullRightBtnClick );

        mItemCommon = null;

        if (mDetailCommon != null)
        {
            mDetailCommon.Destroy();
            mDetailCommon = null;
        }

        foreach ( UI_RuneAttriItemThr attrItem in mPropRuneQueue )
        {
            attrItem.Destroy ();
        }
        mPropRuneQueue.Clear ();
        mPropRuneQueue = null;

        mRuneItems = null;
        _instance = null;
        GameEventDispatcher.Inst.removeEventListener ( GameEventID.G_Guide_Continue, ShowNewGuide );
        GameEventDispatcher.Inst.removeEventListener(GameEventID.KE_BagItemSizeShow, BagItemSizeShow);
    }

    public override void OnReadyForClose ()
    {
        base.OnReadyForClose ();

        //GameEventDispatcher.Inst.removeEventListener(GameEventID.KE_KnapsackUpdateShow, OnRefreshRuneItem);
    }

    /// <summary>
    /// 显示符文界面;
    /// </summary>
    /// <param name="runeSlotIdx">当前要展示的符文</param>
    public void ShowUI ( EM_RUNE_POINT runeSlotIdx )
    {
        if ( !initDataDone )
            InitUIData ();

        if ( !initViewDone )
            InitUIView ();

        //ObjectSelf.GetInstance().
        mPoint = runeSlotIdx;
        mObjCard = UI_HeroInfoManager._instance.GetCurCard ();

        this.gameObject.SetActive ( true );

        mSortBtn.gameObject.SetActive ( true );

        if ( runeSlotIdx == EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL )
        {
            SetSortType ( EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL );
        }
        else
        {
            mSortTxt.text = GameUtils.getString ( "hero_rune_content14" );
            SetSortType ( EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL );
        }
        UpdateUIObjActive ();
    }

    void UpdateAllRuneItemsUI ( bool needDestroy = false )
    {
        if ( needDestroy )
        {
            DestroyAllRuneItems ();
            SetRuneListShowType ( mSort );
        }
        else
        {
            foreach ( X_GUID ids in mRuneItems.Keys )
            {
                if ( ids == null )
                    continue;

                //UpdateRuneItemUI(ids);

                mRuneLayout.UpdateCell ();
            }
        }
    }

    public void HideUI ()
    {
        mSlide.OnClose ();
        this.gameObject.SetActive ( false );

        DestroyAllRuneItems ();

        if ( null != UI_HeroRuneManager._instance )
            UI_HeroRuneManager._instance.RunOnFront ();
    }

    void OnCloseBtnClick ()
    {
        SelectedRuneGUID = null;
        HideUI ();
    }

    void OnAddBtnClick ()
    {
        m_BagAddPanel.SetActive(true);
    }

    void OnSortBtnClick ()
    {

    }

    void OnSortAllClick ()
    {
        mSortTxt.text = GameUtils.getString ( "hero_rune_content14" );
        SetSortType ( EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL );
        mSlide.OnClose ();
    }
    void OnSortOrangeClick ()
    {
        mSortTxt.text = GameUtils.getString ( "hero_rune_content15" );
        SetSortType ( EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL );
        mSlide.OnClose ();

    }

    void OnSortRedClick ()
    {
        mSortTxt.text = GameUtils.getString ( "hero_rune_content16" );

        SetSortType ( EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED );
        mSlide.OnClose ();
    }
    void OnSortGreenClick ()
    {
        mSortTxt.text = GameUtils.getString ( "hero_rune_content17" );
        SetSortType ( EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN );
        mSlide.OnClose ();

    }
    void OnSortPurpleClick ()
    {
        mSortTxt.text = GameUtils.getString ( "hero_rune_content18" );

        SetSortType ( EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE );
        mSlide.OnClose ();

    }
    void OnSortBlueClick ()
    {
        mSortTxt.text = GameUtils.getString ( "hero_rune_content19" );
        SetSortType ( EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE );
        mSlide.OnClose ();

    }

    void OnNullLeftBtnClick ()
    {
        //InterfaceControler.GetInst().AddMsgBox("该功能尚未开放", transform);
        UI_HomeControler.Inst.AddUI ( UI_Recruit.UI_ResPath );
        UI_Recruit.inst.OpenRelicBtn ();
        UI_HomeControler.Inst.ReMoveUI ( gameObject );
        UI_HomeControler.Inst.ReMoveUI ( UI_HeroInfo.UI_ResPath );
    }

    void OnNullRightBtnClick ()
    {
        //InterfaceControler.GetInst().AddMsgBox("该功能尚未开放", transform);
        //UI_HomeControler.Inst.AddUI(UI_SelectFightArea.UI_ResPath);
        UI_HomeControler.Inst.AddUI ( UI_SelectLevelMgrNew.UI_ResPath );
        UI_HomeControler.Inst.ReMoveUI ( gameObject );
        UI_HomeControler.Inst.ReMoveUI ( UI_HeroInfo.UI_ResPath );
    }

    void OnChangeBtnClick ()
    {
        if ( EquipRuneHandler () )
        {
            SelectedRuneGUID = null;
            HideUI ();
        }
        else
        {
            InterfaceControler.GetInst ().AddMsgBox ( GameUtils.getString ( "hero_rune_content12" ), transform );

        }
    }

    void OnEquipBtnClick ()
    {
        if ( EquipRuneHandler () )
        {
            SelectedRuneGUID = null;
            HideUI ();
            //新手引导相关 点击【装配】
            if ( GuideManager.GetInstance ().GetBackCount ( 200106 ) )
            {
                GuideManager.GetInstance ().ShowGuideWithIndex ( 200107 );
            }
        }

        else
        {
            InterfaceControler.GetInst ().AddMsgBox ( GameUtils.getString ( "hero_rune_content12" ), transform );
        }
    }

    bool EquipRuneHandler ()
    {
        if ( mObjCard == null || mSelectedRuneGUID == null )
            return false;

        CUseItem msg = new CUseItem ();

        msg.bagid = ( byte ) EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP;
        msg.itemkey = ( int ) SelectedRuneGUID.GUID_value;
        msg.num = ( short ) ( mPoint + 1 );
        msg.dstkey = ( int ) mObjCard.GetGuid ().GUID_value;

        IOControler.GetInstance ().SendProtocol ( msg );

        return true;
    }

    public void SetSortType ( EM_SORT_RUNE_ITEM sortType )
    {
        //if (mSort == sortType)
        //    return false;

        SelectedRuneGUID = null;

        DestroyAllRuneItems ();

        mScrollRectObj.SetActive(true);

        bool isHave = SetRuneListShowType ( sortType );

        mNullObj.SetActive ( !isHave );
        //mBottomObj.SetActive(isHave);
        mScrollRectObj.SetActive ( isHave );
    }

    void DestroyAllRuneItems ()
    {
        //StopCoroutine("CreateUIObject");
        //StopAllCoroutines ();

        //foreach ( UI_RuneItem ui in mRuneItems.Values )
        //{
        //    if ( ui == null )
        //        continue;

        //    ui.Destroy ();
        //}

        mRuneLayout.Clear ();

        mRuneItems.Clear ();

        GameUtils.DestroyChildsObj ( mRuneParentObj );
    }



    bool SetRuneListShowType ( EM_SORT_RUNE_ITEM sortType )
    {
        List<BaseItem> items = ObjectSelf.GetInstance ().CommonItemContainer.SortRuneItemByType ( sortType );

        //特殊符文只显示可以穿戴的专属符文和非专属符文;
        if ( sortType == EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL )
        {
            List<BaseItem> result = new List<BaseItem> ();
            int heroTableId = UI_HeroInfoManager._instance.GetCurCard ().GetHeroRow ().getId ();
            for ( int i = 0; i < items.Count; i++ )
            {
                ItemEquip itemE = items [ i ] as ItemEquip;

                if ( itemE == null )
                    continue;

                ItemTemplate itemT = itemE.GetItemRowData ();

                if ( itemT.getRune_type () == ( int ) EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE )
                {
                    int [] heroIds = itemT.getRune_specialHeroId ();

                    for ( int m = 0, n = heroIds.Length; m < n; m++ )
                    {
                        if ( heroIds [ m ] == heroTableId )
                        {
                            result.Add ( items [ i ] );
                        }
                    }
                }
                else
                {
                    result.Add ( items [ i ] );
                }
            }

            mRuneList.Clear ();
            mRuneList = HandleRuneItems(result);
            mRuneLayout.cellCount = mRuneList.Count;
            mRuneLayout.updateCellEvent = UpdateRuneItem;
            mRuneLayout.Reload ();
            //StartCoroutine(CreateAllRuneItemUIObject(result));
        }
        else
        {
            List<BaseItem> result = new List<BaseItem> ();

            //是否新手引导  如新手引导需将2星符文排在第四个
            if ( GuideManager.GetInstance ().GetBackCount ( 200105 ) )
            {
                Debug.Log ( GuideManager.GetInstance ().GetBackCount ( 200105 ) );
                items = ObjectSelf.GetInstance ().CommonItemContainer.SortRuneItemByType ( EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_NEWGUIDE );
                BaseItem _temp = null;
                for ( int i = 0; i < items.Count; i++ )
                {
                    if ( items [ i ].GetItemClass () == 1401100032 )
                    {
                        _temp = items [ i ];
                        items [ i ] = items [ 3 ];
                        items [ 3 ] = _temp;
                    }
                }
            }


            for ( int i = 0; i < items.Count; i++ )
            {
                ItemEquip itemE = items [ i ] as ItemEquip;

                if ( itemE == null )
                    continue;

                ItemTemplate itemT = itemE.GetItemRowData ();

                //if (itemT.getRune_type() == (int)EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE || itemT.getRune_type() == (int)EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL)
                if (RuneModule.IsSpecialRune(itemT))
                {
                    continue;
                }
                else
                {
                    result.Add ( items [ i ] );
                }
            }

            mRuneList.Clear ();
            mRuneList = HandleRuneItems(result);
            mRuneLayout.cellCount = mRuneList.Count;
            mRuneLayout.updateCellEvent = UpdateRuneItem;
            mRuneLayout.Reload ();
            //StartCoroutine(CreateAllRuneItemUIObject(result));
            //StartCoroutine(CreateAllRuneItemUIObject(items));
        }

        mSort = sortType;

        return items.Count > 0;
    }

    /// <summary>
    /// 补齐显示，如果补齐的物品栏数没有超过背包最大总数，用空补齐，超过部分用锁定状态补齐;
    /// </summary>
    /// <param name="items"></param>
    List<UIRuneChangeRuneItemShowData> HandleRuneItems(List<BaseItem> items)
    {
        List<UIRuneChangeRuneItemShowData> result = new List<UIRuneChangeRuneItemShowData>();
        int count = items == null ? 0 : items.Count;

        for (int i = 0; i < count; i++ )
        {
            UIRuneChangeRuneItemShowData data = new UIRuneChangeRuneItemShowData();
            data.baseItem = items[i];
            data.showType = RuneItemShowType.Normal;
            result.Add(data);
        }

        int bagMax = ObjectSelf.GetInstance().GetItemBagSizeMax();

        int total = count % MaxPerLin == 0 ? count : (count / MaxPerLin + 1) * MaxPerLin;
        total = Mathf.Max(total, MaxPerPage);
   
        for (int i = count; i < total; i++ )
        {
            UIRuneChangeRuneItemShowData data = new UIRuneChangeRuneItemShowData();
            data.showType = i >= bagMax ? RuneItemShowType.Locked : RuneItemShowType.Null;
            data.baseItem = null;
            result.Add(data);
        }

        return result;
    }

    void UpdateRuneItem ( int index, RectTransform cell )
    {
        //if (!mScrollRectObj.activeSelf)
        //{
        //    return;
        //}

        if ( index < mRuneList.Count )
        {
            UIRuneChangeRuneItemShowData item = mRuneList [ index ];
            RuneItemCommon runeItem = cell.gameObject.GetComponent<RuneItemCommon>();
            if ( runeItem == null )
            {
                runeItem = cell.gameObject.AddComponent<RuneItemCommon>();
                if (runeItem == null)
                {
                    Debug.LogError("RuneItemCommon component 添加失败！");
                    return;
                }
            }

            runeItem.index = index;
            if (item.showType == RuneItemShowType.Normal)
            {
                int tableId = item.baseItem.GetItemTableID ();
                ItemTemplate runeTableItem = DataTemplate.GetInstance ().GetItemTemplateById ( tableId );
                X_GUID guid = item.baseItem.GetItemGuid ();
                ////UI_RuneItem runeItem = new UI_RuneItem ( cell.transform, guid );
                //runeItem.Init ( cell.transform, guid );
                //runeItem.SetName ( GameUtils.getString ( runeTableItem.getName () ) );
                //runeItem.SetIconImg ( common.defaultPath + runeTableItem.getIcon () );
                //runeItem.SetStarNum ( runeTableItem.getRune_quality () );
                //ItemEquip itemD = item.baseItem as ItemEquip;
                //runeItem.SetLevel ( itemD.GetStrenghLevel () );
                //runeItem.SetType(runeTableItem.getRune_type());
                //runeItem.SetClickable(true);
                //runeItem.SetLocked(false);
                //runeItem.SetStarBgNum(5);
                //runeItem.SetIsSpec(RuneModule.IsSpecialRune(runeTableItem));

                //runeItem.Init(cell.transform);
                runeItem.data = guid;
                RuneItemCommonData ricd = new RuneItemCommonData();
                ricd.ItemAction = OnRuneItemClick;
                ricd.ItemT = runeTableItem;
                ricd.RuneLevel = item.baseItem.GetStrenghLevel();
                runeItem.SetRuneItemData(ricd, RuneItemCommon.RuneItemShowType.IconWithBg);
                bool isEquiped = RuneModule.IsItemEquipEquiped(item.baseItem as ItemEquip);
                runeItem.SetEquiped(isEquiped);
                bool isMaxLv = DataTemplate.GetInstance().IsRuneStrenthFullLevel(runeTableItem, ricd.RuneLevel);
                runeItem.SetMaxLevelEffectActive(isMaxLv);

                if ( mRuneItems.ContainsKey ( guid ) )
                {
                    mRuneItems.Remove ( guid );
                }
                mRuneItems.Add ( guid, runeItem );

                //默认选中第一个;
                //if (i == 0)
                //    SetSelectedRune(item.GetItemGuid());

                UpdateRuneItemUI ( item.baseItem.GetItemGuid () );
            }
            else if (item.showType == RuneItemShowType.Null)
            {
                //runeItem.Init(cell.transform, null);
                //runeItem.SetClickable(false);
                //runeItem.SetType(-1);
                //runeItem.SetStarNum(-1);
                //runeItem.SetStarBgNum(-1);
                //runeItem.SetIsSpec(false);
                //runeItem.SetIconImg(null);
                //runeItem.SetEquiped(false);
                //runeItem.SetLevel(-1);
                //runeItem.SetName("");
                //runeItem.SetSelected(false);
                //runeItem.SetLocked(false);
                // 
                //runeItem.Init(cell.transform);
                runeItem.data = null;
                RuneItemCommonData ricd = new RuneItemCommonData();
                ricd.ItemAction = null;
                ricd.ItemT = null;
                runeItem.SetRuneItemData(ricd, RuneItemCommon.RuneItemShowType.Null);
            }
            else if (item.showType == RuneItemShowType.Locked)
            {
                //runeItem.Init(cell.transform, null);
                //runeItem.SetClickable(false);
                //runeItem.SetType(-1);
                //runeItem.SetStarNum(-1);
                //runeItem.SetStarBgNum(-1);
                //runeItem.SetIsSpec(false);
                //runeItem.SetIconImg(null);
                //runeItem.SetEquiped(false);
                //runeItem.SetLevel(-1);
                //runeItem.SetName("");
                //runeItem.SetSelected(false);
                //runeItem.SetLocked(true);

                //runeItem.Init(cell.transform);
                runeItem.data = null;
                RuneItemCommonData ricd = new RuneItemCommonData();
                ricd.ItemAction = null;
                ricd.ItemT = null;
                runeItem.SetRuneItemData(ricd, RuneItemCommon.RuneItemShowType.Locked);
            }
        }
    }

    void OnRuneItemClick(object data)
    {
        if (data == null)
            return;
        
        SetSelectedRune(data as X_GUID);

        //新手引导相关 选择【符文】
        if (GuideManager.GetInstance().GetBackCount(200105))
        {
            GuideManager.GetInstance().ShowGuideWithIndex(200106);
        }
    }

    //IEnumerator CreateAllRuneItemUIObject ( List<BaseItem> items )
    //{
    //    BaseItem item = null;
    //    for ( int i = 0, j = items.Count; i < j; i++ )
    //    {
    //        if ( items [ i ] == null )
    //            continue;

    //        item = items [ i ];

    //        int tableId = item.GetItemTableID ();
    //        //RuneTemplate runeTableItem = (RuneTemplate)DataTemplate.GetInstance().m_RuneTable.m_Data[tableId];

    //        ItemTemplate runeTableItem = DataTemplate.GetInstance ().GetItemTemplateById ( tableId );

    //        if ( runeTableItem == null )
    //            continue;

    //        //CreateRuneItemUI(runeTableItem, item.GetItemGuid());
    //        GameObject go = ( GameObject ) GameObject.Instantiate ( mRuneItemObj );

    //        if ( go == null )
    //        {
    //            LogManager.LogError ( "create rune item object error" );
    //            yield return null;
    //        }

    //        go.transform.parent = mRuneParentObj.transform;
    //        go.transform.localScale = Vector3.one;
    //        go.transform.localPosition = new Vector3 ( go.transform.localPosition.x, go.transform.localPosition.y, 0f );

    //        yield return new WaitForEndOfFrame ();

    //        X_GUID guid = item.GetItemGuid ();

    //        UI_RuneItem runeItem = new UI_RuneItem ( go.transform, guid );

    //        if ( runeItem == null )
    //            yield return null;

    //        runeItem.SetName ( GameUtils.getString ( runeTableItem.getName () ) );
    //        runeItem.SetIconImg ( common.defaultPath + runeTableItem.getIcon () );
    //        runeItem.SetStarNum ( runeTableItem.getRune_quality () );
    //        ItemEquip itemD = item as ItemEquip;
    //        runeItem.SetLevel ( itemD.GetStrenghLevel () );
    //        runeItem.SetType(runeTableItem.getRune_type());

    //        if ( mRuneItems.ContainsKey ( guid ) )
    //        {
    //            mRuneItems.Remove ( guid );
    //        }
    //        mRuneItems.Add ( guid, runeItem );

    //        //默认选中第一个;
    //        //if (i == 0)
    //        //    SetSelectedRune(item.GetItemGuid());

    //        //UpdateRuneItemUI ( item.GetItemGuid () );
    //    }
    //}

    void UpdateRuneItemUI ( X_GUID runeGUID )
    {
        if ( mRuneItems.Count == 0 || !mRuneItems.ContainsKey ( runeGUID ) )
        {
            return;
        }

        ItemEquip data = ( ItemEquip ) ObjectSelf.GetInstance ().CommonItemContainer.FindItem ( EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, runeGUID );
        if ( data == null )
        {
            //Debug.Log("rune item not found. runeid = " + runeGUID);
            return;
        }

        //mRuneItems[runeGUID].SetEquiped(data.IsEquip());
        bool isEquiped = data.IsEquip ();//ObjectSelf.GetInstance().HeroContainerBag.IsItemEquiped(data);
        mRuneItems [ runeGUID ].SetEquiped ( isEquiped );
    }

    void UpdateUIObjActive ()
    {
        #region 是否装备符文;
        //ObjectCard cardInfo = UI_HeroInfoManager._instance.GetCurCard();
        ////没装备着符文;
        //if(cardInfo.GetHeroData().IsRuneNull(mPoint))
        //{
        //    mNoneHint.gameObject.SetActive(true);
        //    mDetailObj.SetActive(false);
        //    mEquipBtn.gameObject.SetActive(true);
        //    mChangeBtn.gameObject.SetActive(false);
        //}
        ////装备着符文;
        //else
        //{
        //    mNoneHint.gameObject.SetActive(false);
        //    mDetailObj.SetActive(true);
        //    mEquipBtn.gameObject.SetActive(false);
        //    mChangeBtn.gameObject.SetActive(true);
        //}
        #endregion

        #region 是否选中符文;
        if ( SelectedRuneGUID == null )
        {
            mNoneHint.gameObject.SetActive ( true );
            mDetailObj.SetActive ( false );
        }
        else
        {
            mNoneHint.gameObject.SetActive ( false );
            mDetailObj.SetActive ( true );
        }
        #endregion

        #region 是否装备符文;
        ObjectCard cardInfo = UI_HeroInfoManager._instance.GetCurCard ();
        //没装备着符文;
        if ( cardInfo.GetHeroData ().IsRuneNull ( mPoint ) )
        {
            mEquipBtn.gameObject.SetActive ( true );
            mChangeBtn.gameObject.SetActive ( false );

            mAttriTitleTxt.text = GameUtils.getString ( "hero_rune_content23" );
            mTitleTxt_1.text = GameUtils.getString("hero_rune_title");
            mTitleTxt_2.text = GameUtils.getString("hero_rune_title");
          
        }
        //装备着符文;
        else
        {
            mEquipBtn.gameObject.SetActive ( false );
            mChangeBtn.gameObject.SetActive ( true );

            mAttriTitleTxt.text = GameUtils.getString ( "hero_rune_content10" );
            mTitleTxt_1.text = GameUtils.getString("hero_rune_content13");
            mTitleTxt_2.text = GameUtils.getString("hero_rune_content13");
        }
        #endregion

        if ( mPoint == EM_RUNE_POINT.EM_RUNE_POINT_SPECIAL )
        {
            mSortBtn.gameObject.SetActive ( false );
        }
    }

    void UpdateRightRuneDetail ()
    {

    }

    /// <summary>
    /// 物品列表信息改变，刷新符文物品列表;
    /// </summary>
    void OnRefreshRuneItem ()
    {
        UpdateAllRuneItemsUI ();
    }

    void OnRuneItemChanged ()
    {
        UpdateUIObjActive ();

        foreach ( RuneItemCommon uiitem in mRuneItems.Values )
        {
            uiitem.SetSelected ( false );
        }

        if ( SelectedRuneGUID != null )
        {
            if ( mRuneItems.ContainsKey ( SelectedRuneGUID ) )
            {
                mRuneItems [ SelectedRuneGUID ].SetSelected ( true );
            }


            ItemEquip itemE = ( ItemEquip ) ObjectSelf.GetInstance ().CommonItemContainer.FindItem ( EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, SelectedRuneGUID );
            if ( itemE == null )
            {
                LogManager.LogError ( "错误的符文GUID" );
                return;
            }
            else
            {

                ItemTemplate itemT = DataTemplate.GetInstance ().GetItemTemplateById ( itemE.GetItemTableID () );

                //mRuneImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + itemT.getIcon());
                //mRuneImg.SetNativeSize();
                //bool isSpec = RuneModule.IsSpecialRune(itemT);
                //mRuneName.text = GameUtils.getString ( itemT.getName () );
                //mUIRuneItem.SetIsSpecial ( isSpec );
                //mUIRuneItem.SetLevel ( itemE.GetStrenghLevel () );
                //mUIRuneItem.SetStarsNum ( itemT.getRune_quality () );
                //mUIRuneItem.SetIcon ( common.defaultPath + itemT.getIcon () );
                //mUIRuneItem.SetSize(RuneIconItemSize.Big);

                //if (!isSpec)
                //{
                //    mUIRuneItem.SetRuneType(itemT.getRune_type());
                //}

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

                mUserObj.SetActive(false);

                //mSpeHeroTxt.text = GameUtils.getString ( itemT.getSpecialHeroDes () );

                //ObjectCard oc = ObjectSelf.GetInstance ().HeroContainerBag.GetItemUser ( itemE );
                //if ( oc == null )
                //{
                //    mUserObj.SetActive ( false );
                //}
                //else
                //{
                //    HeroTemplate heroT = DataTemplate.GetInstance ().GetHeroTemplateById ( oc.GetHeroData ().TableID );
                //    if ( heroT != null )
                //        mUserName.text = GameUtils.getString ( heroT.getTitleID () ) + "    " + GameUtils.getString ( "hero_rune_content7" );
                //    mUserObj.SetActive ( true );
                //}

                if (mDetailCommon == null)
                {
                    mDetailCommon = new RuneDetailCommon(m_RuneAttriPos, SelectedRuneGUID, 450f);
                }
                else
                {
                    mDetailCommon.SetShowData(SelectedRuneGUID);
                }

                //符文属性;
                //GameUtils.DestroyChildsObj ( mAtrriListObj1 );

                ////基础属性;
                //bool titleDone1 = false;
                //RuneData runeData = itemE.GetRuneData ();
                //foreach ( int id in runeData.BaseAttributeID )
                //{
                //    if ( id == -1 )
                //        continue;

                //    if ( !titleDone1 )
                //    {
                //        titleDone1 = true;
                //        CreateTitle ( mAtrriListObj1, GameUtils.getString ( "hero_rune_content8" ) );
                //    }

                //    BaseruneattributeTemplate bt = DataTemplate.GetInstance ().GetBaseruneattributeTemplate ( id );
                //    //特殊符文属性描述;
                //    if ( bt.getNumshow () == 0 )
                //    {
                //        //CreateTitle(mAtrriListObj1, GameUtils.getString(bt.getAttriDes()));
                //        CreateDetailTxts ( mAtrriListObj1, GameUtils.getString ( bt.getAttriDes () ) );
                //    }
                //    else
                //    {
                //        CreateBaseAttriObj ( mAtrriListObj1, GameUtils.getString ( bt.getAttriDes () ), "+" + bt.getAttriValue ().ToString () );
                //    }
                //}

                ////附加属性;
                //bool titleDone2 = false;
                //int count = DataTemplate.GetInstance ().GetRuneMaxRedefineTimes ( itemT );
                //int i = 0;
                //bool isGray = false;

                //foreach ( int id in runeData.AppendAttribute )
                //{
                //    i++;

                //    isGray = i * 3 > itemE.GetStrenghLevel ();

                //    if ( id == -1 )
                //    {
                //        if ( i <= count )
                //        {
                //            if ( !titleDone2 )
                //            {
                //                titleDone2 = true;
                //                CreateTitle ( mAtrriListObj1, GameUtils.getString ( "hero_rune_content9" ) );
                //            }

                //            //位置属性，未鉴定;
                //            CreateAttriObj ( mAtrriListObj1, GameUtils.getString ( "rune_content2" ), "", GameUtils.getString ( "rune_content3" ), isGray );
                //        }

                //        continue;
                //    }

                //    if ( !titleDone2 )
                //    {
                //        titleDone2 = true;
                //        CreateTitle ( mAtrriListObj1, GameUtils.getString ( "hero_rune_content9" ) );
                //    }

                //    AddruneattributeTemplate bt = DataTemplate.GetInstance ().GetAddruneattributeTemplate ( id );
                //    bool isPercent = bt.getIspercentage () > 0;
                //    string val = isPercent ? ( ( float ) bt.getAttriValue () / ( float ) 10f + "%" ) : bt.getAttriValue ().ToString ();
                //    CreateAttriObj ( mAtrriListObj1, GameUtils.getString ( bt.getAttriDes1 () ), GameUtils.getString ( bt.getAttriDes2 () ), bt.getSymbol () + val, isGray );
                //}
                //根据符文的等级计算出更换后的效果;
                //GameUtils.DestroyChildsObj(mAtrriListObj2);
                foreach ( UI_RuneAttriItemThr attrItem in mPropRuneQueue )
                {
                    attrItem.Destroy ();
                }
                mPropRuneQueue.Clear ();

                ObjectCard cardInfo = UI_HeroInfoManager._instance.GetCurCard ();
                Dictionary<int, Pair> attrisDic = null;
                List<X_GUID> runeList = new List<X_GUID> ( ( int ) EM_RUNE_POINT.EM_RUNE_POINT_NUMBER );

                runeList = cardInfo.GetHeroData ().GetEquipItems ();

                runeList [ ( int ) mPoint ] = SelectedRuneGUID;

                attrisDic = HeroRuneModule.CompareAttriDic ( cardInfo, runeList );

                mNoChangeObj.SetActive ( attrisDic == null || attrisDic.Count <= 0 );

                for ( int m = 0; m < attrisDic.Count; m++ )
                {
                    mPropRuneQueue.Add ( CreateNullRuneAttriUI () );
                }

                UI_RuneAttriItemThr ui_item = null;

                int key = -1;
                StringBuilder sb = new StringBuilder ();
                for ( int n = 0; n < mPropRuneQueue.Count; n++ )
                {
                    bool isShow = n < attrisDic.Count;

                    ui_item = mPropRuneQueue [ n ];

                    if ( ui_item == null )
                        continue;

                    ui_item.SetActive ( isShow );

                    if ( isShow )
                    {
                        key = GameUtils.GetKeyByIdx ( attrisDic, n );
                        string str = "";
                        if ( attrisDic [ key ].b > 0 )
                        {
                            str = "#2FFF08"; //绿色;
                        }
                        else
                        {
                            str = "#F5282C"; //红色;
                        }

                        string flag = attrisDic [ key ].b > 0 ? "+" : "";

                        ui_item.SetInfo ( GameUtils.GetAttriName ( key ), attrisDic [ key ].a.ToString (), GameUtils.StringWithColor ( flag + attrisDic [ key ].b, str ) );
                    }
                }
            }

        }

        UpdateBagCountTxt ();
    }


    void CreateDetailTxts ( GameObject parent, string detail )
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

        string [] contents = detail.SplitByLength ( GlobalMembers.MAX_RUNE_COUNT_PER_LINE );

        if ( contents == null )
        {
            return;
        }

        int count = contents.Length;

        if ( count <= 0 )
        {
            return;
        }

        for ( int i = 0; i < count; i++ )
        {
            CreateDetailTxt ( parent, contents [ i ] );
        }
    }

    void CreateDetailTxt ( GameObject parent, string detail )
    {
        GameObject go = ( GameObject ) GameObject.Instantiate ( mAttriDetailTxt.gameObject );

        go.transform.parent = parent.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3 ( go.transform.localPosition.x, go.transform.localPosition.y, 0f );

        go.transform.FindChild ( "Text" ).GetComponent<Text> ().text = detail;
    }

    UI_RuneAttriItemThr CreateNullRuneAttriUI ()
    {
        GameObject go = ( GameObject ) GameObject.Instantiate ( mRuneAttriObj2 );
        if ( go == null )
            return null;

        Transform trans = go.transform;

        trans.parent = mAtrriListObj2.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3 ( trans.localPosition.x, trans.localPosition.y, 0f );
        return new UI_RuneAttriItemThr ( trans );
    }

    void CreateTitle ( GameObject parent, string str )
    {
        GameObject go = ( GameObject ) GameObject.Instantiate ( mAttriTitleObj.gameObject );

        go.transform.parent = parent.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3 ( go.transform.localPosition.x, go.transform.localPosition.y, 0f );

        go.GetComponent<Text> ().text = str;
    }

    void CreateBaseAttriObj ( GameObject parent, string str1, string str2 )
    {
        GameObject go = Instantiate ( mRuneAttriObj1 ) as GameObject;

        go.transform.parent = parent.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3 ( go.transform.localPosition.x, go.transform.localPosition.y, 0f );

        go.transform.FindChild ( "Left_txt" ).GetComponent<Text> ().text = str1;
        go.transform.FindChild ( "Right_txt" ).GetComponent<Text> ().text = str2;
    }

    /// <summary>
    /// 创建属性列;
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    void CreateAttriObj ( GameObject parent, string str1, string str2, string str3, bool isGray )
    {
        GameObject go = Instantiate ( mRuneAttriObj ) as GameObject;

        go.transform.parent = parent.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3 ( go.transform.localPosition.x, go.transform.localPosition.y, 0f );

        if ( isGray )
        {
            str1 = GameUtils.StringWithGrayColor ( str1 );
            str2 = GameUtils.StringWithGrayColor ( str2 );
            str3 = GameUtils.StringWithGrayColor ( str3 );
        }

        go.transform.FindChild ( "Left_txt" ).GetComponent<Text> ().text = str1;
        go.transform.FindChild ( "Mid_txt" ).GetComponent<Text> ().text = str2;
        go.transform.FindChild ( "Right_txt" ).GetComponent<Text> ().text = str3;
    }

    void UpdateBagCountTxt ()
    {
        ObjectSelf player = ObjectSelf.GetInstance ();
        if ( player.CommonItemContainer.GetBagItemSum () <= player.CommonItemContainer.GetBagItemSizeMax () )
        {
            mBagNumText.text = "<color=#f3863a>" + player.CommonItemContainer.GetBagItemSum () + "</color>/" + player.CommonItemContainer.GetBagItemSizeMax ();
        }
        else
        {
            mBagNumText.text = "<color=red>" + player.CommonItemContainer.GetBagItemSum () + "</color>/" + player.CommonItemContainer.GetBagItemSizeMax ();
        }
    }

    public void SetSelectedRune ( X_GUID runeGUID )
    {
        if ( runeGUID == null )
            return;

        if ( SelectedRuneGUID != null && runeGUID.Equals ( SelectedRuneGUID ) )
            return;

        SelectedRuneGUID = runeGUID;
    }

    
}