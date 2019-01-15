using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;

public class UI_RuneStrenthMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Rune/UI_RuneStrenth_1_1";
    private static UI_RuneStrenthMgr mInst = null;
    private static X_GUID mRuneGUID = null;

    protected Text mStrenthTitleTxt = null;
    protected Text[] mRuneNameTxt = null;
    protected Text[] mSpeHeroNameTxt = null;                //专属符文英雄名字;
    protected GameObject[] mRuneObj = null;                 //符文信息展示obj;
    //protected RuneIconItem[] mStrenthRuneItem = null;
    protected Button[] mStrenthBtn = null;
    protected Text[] mStrenthBtnTxt = null;
    protected Button[] mCloseBtn = null;
    protected Text[] mCloseBtnTxt = null;
    protected GameObject[] mCostObj = null;
    protected Text[] mCostGoldTxt = null;
    protected Image mNorCostItemImg = null;
    protected Image mSpecCostItemImg = null;
    protected Text mCostItemCountTxt = null;
    protected GameObject mStrenthObj = null;
    protected Text mStrenthTxt = null;
    protected GameObject mAttriDetailTxt = null;
    protected GameObject mStrenthFullObj1 = null;
    protected Image mStrenthFullImg1 = null;
    protected Text mStrenthFullText1 = null;
    
    protected GameObject mStrenthFullObj2 = null;
    protected Image mStrenthFullImg2 = null;
    protected Text mStrenthFullText2 = null;
    
    //右上角金钱信息显示;
    protected GameObject mCostObj1 = null;
    protected Image mCostGoldImg = null; //金币;
    protected Text mCostTxt1 = null;
    protected GameObject mCostObj2 = null;
    protected Image mCost2Img1 = null;
    protected Image mCost2Img2 = null; // 钻石;
    protected Text mCost2Txt1 = null;
    protected Text mCost2Txt2 = null;
    protected Text mHintTxt = null;
    protected GameObject mAttriTitleTxt = null;
    protected GameObject mRuneAttriObj = null;
    protected GameObject mAddRuneAttriObj = null;

    protected RuneItemCommon[] mItemCommons = null;
    protected Transform[] mDetailTransPos = null;               //属性根节点obj;
    protected RuneDetailCommon[] mDetailCommons = null;

    //private GameObject mGo;

    private bool mInitDone = false;
    //private X_GUID mGUID = null;

    ItemEquip ItemEquipInfo
    {
        get
        {
            if (mRuneGUID == null)
                return null;
            return (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, mRuneGUID);
        }
    }

    public static UI_RuneStrenthMgr Ins
    {
        get
        {
            return mInst;
        }
    }

    //public UI_RuneStrenthMgr(Transform trans)
    //{
    //    if (trans == null)
    //        return;

    //    mGo = trans.gameObject;

    //    InitUIData();
    //}

    public static void SetShowRuneGUID(X_GUID guid)
    {
        mRuneGUID = guid;
    }

    private void ShowUI()
    {
        if (mRuneGUID == null)
            return;

        if (ItemEquipInfo == null) return;

        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshItem, OnItemRefresh);

        mDetailCommons = new RuneDetailCommon[3];
        mDetailCommons[0] = new RuneDetailCommon(mDetailTransPos[0], mRuneGUID, 450f);
        mDetailCommons[1] = new RuneDetailCommon(mDetailTransPos[1], mRuneGUID, 450f, 1);
        mDetailCommons[2] = new RuneDetailCommon(mDetailTransPos[2], mRuneGUID, 450f);

        UpdateUI();

        //if (mGo != null)
        //    mGo.SetActive(true);
    }

    void OnItemRefresh(GameEvent ge)
    {
        if (ge == null) return;

        Item it = ge.data as Item;
        if (it == null) return;

        if (mRuneGUID.GUID_value == it.key)
            UpdateUI();
    }

    

    void UpdateUI()
    {
        mStrenthObj.SetActive(false);

        //刷新右上角消耗显示;
        long count1 = 0;
        ObjectSelf.GetInstance().TryGetResourceCountById(1400000002, ref count1);
        mCostTxt1.text = count1.ToString();

        ItemTemplate itemT = ItemEquipInfo.GetItemRowData();
        int strengthLv = ItemEquipInfo.GetStrenghLevel();

        //是否是特殊符文;
        EM_RUNE_TYPE runeType = (EM_RUNE_TYPE)itemT.getRune_type();
        bool isUnique = runeType == EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL || runeType == EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE;

        mStrenthBtn[0].gameObject.SetActive(!isUnique);
        mStrenthBtn[1].gameObject.SetActive(isUnique);
        mCloseBtn[0].gameObject.SetActive(!isUnique);
        mCloseBtn[1].gameObject.SetActive(isUnique);


        //判断符文是否满级了;
        bool isFullLv = DataTemplate.GetInstance().IsRuneStrenthFullLevel(itemT, strengthLv);

        mRuneObj[0].SetActive(!isFullLv);
        mRuneObj[1].SetActive(!isFullLv);
        mRuneObj[2].SetActive(isFullLv);

        if(isFullLv)
        {
            //mStrenthRuneItem[i].SetIsSpecial(isUnique);
            RuneItemCommonData ricd = new RuneItemCommonData();
            ricd.IsShowMaxEffect = true;
            ricd.ItemT = itemT;
            ricd.RuneLevel = strengthLv;
            ricd.EquipedHeroName = RuneModule.GetItemEuipHeroName(ItemEquipInfo);
            mItemCommons[2].SetRuneItemData(ricd, RuneItemCommon.RuneItemShowType.IconWithRightName);
        }
        else
        {
            RuneItemCommonData ricd = new RuneItemCommonData();
            ricd.IsShowMaxEffect = true;
            ricd.ItemT = itemT;
            ricd.RuneLevel = strengthLv;
            ricd.EquipedHeroName = RuneModule.GetItemEuipHeroName(ItemEquipInfo);
            mItemCommons[0].SetRuneItemData(ricd, RuneItemCommon.RuneItemShowType.IconWithRightName);

            RuneItemCommonData ricd1 = new RuneItemCommonData();
            ricd1.IsShowMaxEffect = true;
            ricd1.ItemT = itemT;
            ricd1.RuneLevel = strengthLv + 1;
            ricd1.EquipedHeroName = RuneModule.GetItemEuipHeroName(ItemEquipInfo);
            mItemCommons[1].SetRuneItemData(ricd1, RuneItemCommon.RuneItemShowType.IconWithRightName);
        }
        
        mCostObj[0].SetActive(!isFullLv);
        mCostObj[1].SetActive(!isFullLv);

        GameUtils.SetBtnSpriteGrayState(mStrenthBtn[0], isFullLv);
        GameUtils.SetBtnSpriteGrayState(mStrenthBtn[1], isFullLv);
        
        mStrenthFullObj1.SetActive(isFullLv);
        mStrenthFullObj2.SetActive(isFullLv);

        if (isFullLv)
        {
            
        }
        else // 没满级判断资源消耗;
        {
            if (isUnique)
            {
                mStrenthTitleTxt.text = GameUtils.getString("rune_title2");

                //消耗资源处刷新;
                RunecostTemplate rt = DataTemplate.GetInstance().GetRuneCostTemplate(itemT.getRune_strengthenId(), strengthLv + 1);

                if (rt.getAttriType1() == 1400000002)//是金币;
                {
                    long countTmp = 0;
                    ObjectSelf.GetInstance().TryGetResourceCountById(rt.getAttriType1(), ref countTmp);
                    mCost2Txt1.text = countTmp.ToString();
                    ObjectSelf.GetInstance().TryGetResourceCountById(rt.getAttriType2(), ref countTmp);
                    mCost2Txt2.text = countTmp.ToString();

                    TEXT_COLOR tc = countTmp >= rt.getAttriValue2() ? TEXT_COLOR.WHITE : TEXT_COLOR.RED;
                    mCostItemCountTxt.text = GameUtils.StringWithColor(rt.getAttriValue2().ToString(), tc);
                    tc = ObjectSelf.GetInstance().Money >= rt.getAttriValue1() ? TEXT_COLOR.WHITE : TEXT_COLOR.RED;
                    mCostGoldTxt[1].text = GameUtils.StringWithColor(rt.getAttriValue1().ToString(), tc);
                }
                else//不是金币;
                {
                    long countTmp = 0;
                    ObjectSelf.GetInstance().TryGetResourceCountById(rt.getAttriType2(), ref countTmp);
                    mCost2Txt1.text = countTmp.ToString();
                    ObjectSelf.GetInstance().TryGetResourceCountById(rt.getAttriType1(), ref countTmp);
                    mCost2Txt2.text = countTmp.ToString();

                    TEXT_COLOR tc = countTmp >= rt.getAttriValue1() ? TEXT_COLOR.WHITE : TEXT_COLOR.RED;
                    mCostItemCountTxt.text = GameUtils.StringWithColor(rt.getAttriValue1().ToString(), tc);
                    tc = ObjectSelf.GetInstance().Money >= rt.getAttriValue2() ? TEXT_COLOR.WHITE : TEXT_COLOR.RED;
                    mCostGoldTxt[1].text = GameUtils.StringWithColor(rt.getAttriValue2().ToString(), tc);
                }

                mNorCostItemImg.sprite = GameUtils.GetSpriteByResourceType(rt.getAttriType1());
                mSpecCostItemImg.sprite = GameUtils.GetSpriteByResourceType(rt.getAttriType2());

                mCostGoldImg.sprite = GameUtils.GetSpriteByResourceType(rt.getAttriType1());
                mCost2Img1.sprite = GameUtils.GetSpriteByResourceType(rt.getAttriType1());
                mCost2Img2.sprite = GameUtils.GetSpriteByResourceType(rt.getAttriType2());

                mCostObj1.SetActive(false);
                mCostObj2.SetActive(true);
            }
            else
            {
                mStrenthTitleTxt.text = GameUtils.getString("rune_title1");

                RunecostTemplate rt = DataTemplate.GetInstance().GetRuneCostTemplate(itemT.getRune_strengthenId(), strengthLv + 1);

                if (rt.getAttriType1() == 1400000002)//是金币;
                {
                    TEXT_COLOR tc = ObjectSelf.GetInstance().Money >= rt.getAttriValue1() ? TEXT_COLOR.WHITE : TEXT_COLOR.RED;
                    mCostGoldTxt[0].text = GameUtils.StringWithColor(rt.getAttriValue1().ToString(), tc);
                }
                else//不是金币;
                {
                    TEXT_COLOR tc = ObjectSelf.GetInstance().Money >= rt.getAttriValue2() ? TEXT_COLOR.WHITE : TEXT_COLOR.RED;
                    mCostGoldTxt[0].text = GameUtils.StringWithColor(rt.getAttriValue2().ToString(), tc);
                }

                mCostObj1.SetActive(true);
                mCostObj2.SetActive(false);
            }
        }

        mStrenthBtn[0].GetComponent<Button>().enabled = true;
        mStrenthBtn[1].GetComponent<Button>().enabled = true;

        //RuneData runeData = ItemEquipInfo.GetRuneData();
        ////基础属性;
        //bool titleDone1 = false;
        //foreach (int id in runeData.BaseAttributeID)
        //{
        //    if (id == -1)
        //        continue;

        //    if (!titleDone1)
        //    {
        //        titleDone1 = true;
        //        if (isFullLv)
        //        {
        //            CreateTitle(mDetailTransPos[2], GameUtils.getString("hero_rune_content8"));
        //        }
        //        else
        //        {
        //            CreateTitle(mDetailTransPos[0], GameUtils.getString("hero_rune_content8"));
        //            CreateTitle(mDetailTransPos[1], GameUtils.getString("hero_rune_content8"));
        //        }
        //    }

        //    BaseruneattributeTemplate bt1 = DataTemplate.GetInstance().GetBaseruneattributeTemplate(id);
        //    if (isFullLv)
        //    {
        //        if (bt1.getNumshow() == 0)
        //        {
        //            //CreateTitle(mAttriList[2], GameUtils.getString(bt1.getAttriDes()));
        //            CreateDetailTxts(mDetailTransPos[2], GameUtils.getString(bt1.getAttriDes()));
        //        }
        //        else
        //        {
        //            CreateBaseAttriObj(mDetailTransPos[2], GameUtils.getString(bt1.getAttriDes()), "+" + bt1.getAttriValue().ToString());
        //        }
        //        //CreateBaseAttriObj(mAttriList[2], GameUtils.GetAttriName(bt1.getAttriType()), "+" + bt1.getAttriValue().ToString());
        //    }
        //    else
        //    {
        //        if (bt1.getNumshow() == 0)
        //        {
        //            //CreateTitle(mAttriList[0], GameUtils.getString(bt1.getAttriDes()));
        //            CreateDetailTxts(mDetailTransPos[0], GameUtils.getString(bt1.getAttriDes()));
        //        }
        //        else
        //        {
        //            CreateBaseAttriObj(mDetailTransPos[0], GameUtils.getString(bt1.getAttriDes()), "+" + bt1.getAttriValue().ToString());
        //        }
        //        //CreateBaseAttriObj(mAttriList[0], GameUtils.GetAttriName(bt1.getAttriType()), "+" + bt1.getAttriValue().ToString());
        //        BaseruneattributeTemplate bt2 = DataTemplate.GetInstance().GetBaseruneattributeTemplate(id + 1);
        //        if (bt2.getNumshow() == 0)
        //        {
        //            //CreateTitle(mAttriList[1], GameUtils.getString(bt2.getAttriDes()));
        //            CreateDetailTxts(mDetailTransPos[1], GameUtils.getString(bt2.getAttriDes()));
        //        }
        //        else
        //        {
        //            CreateBaseAttriObj(mDetailTransPos[1], GameUtils.getString(bt2.getAttriDes()), "+" + bt2.getAttriValue().ToString());
        //        }
        //        //CreateBaseAttriObj(mAttriList[1], GameUtils.GetAttriName(bt2.getAttriType()), "+" + bt2.getAttriValue().ToString());
        //    }
        //}

        ////附加属性;
        //bool titleDone2 = false;
        //int count = DataTemplate.GetInstance().GetRuneMaxRedefineTimes(itemT);
        //int m = 0;
        //bool curIsGray = false, nextIsGray = false;

        //foreach (int id in runeData.AppendAttribute)
        //{
        //    m++;

        //    curIsGray = m * 3 > strengthLv;
        //    nextIsGray = m * 3 > strengthLv + 1;

        //    if (id == -1)
        //    {
        //        if (m <= count)
        //        {
        //            if (!titleDone2)
        //            {
        //                titleDone2 = true;

        //                if (isFullLv)
        //                {
        //                    CreateTitle(mDetailTransPos[2], GameUtils.getString("hero_rune_content9"));
        //                }
        //                else
        //                {
        //                    CreateTitle(mDetailTransPos[0], GameUtils.getString("hero_rune_content9"));
        //                    CreateTitle(mDetailTransPos[1], GameUtils.getString("hero_rune_content9"));
        //                }
        //            }

        //            //位置属性，未鉴定;
        //            if (isFullLv)
        //            {
        //                CreateAddAttriObj(mDetailTransPos[2], GameUtils.getString("rune_content2"), "", GameUtils.getString("rune_content3"), curIsGray);
        //            }
        //            else
        //            {
        //                CreateAddAttriObj(mDetailTransPos[0], GameUtils.getString("rune_content2"), "", GameUtils.getString("rune_content3"), curIsGray);
        //                CreateAddAttriObj(mDetailTransPos[1], GameUtils.getString("rune_content2"), "", GameUtils.getString("rune_content3"), nextIsGray);
        //            }
        //        }

        //        continue;
        //    }

        //    if (!titleDone2)
        //    {
        //        titleDone2 = true;
        //        if (isFullLv)
        //        {
        //            CreateTitle(mDetailTransPos[2], GameUtils.getString("hero_rune_content9"));
        //        }
        //        else
        //        {
        //            CreateTitle(mDetailTransPos[0], GameUtils.getString("hero_rune_content9"));
        //            CreateTitle(mDetailTransPos[1], GameUtils.getString("hero_rune_content9"));
        //        }
        //    }

        //    AddruneattributeTemplate bt = DataTemplate.GetInstance().GetAddruneattributeTemplate(id);
        //    bool isPercent = bt.getIspercentage() > 0;
        //    string val = isPercent ? ((float)bt.getAttriValue() / (float)10f + "%") : bt.getAttriValue().ToString();
        //    if (isFullLv)
        //    {
        //        CreateAddAttriObj(mDetailTransPos[2], GameUtils.getString(bt.getAttriDes1()), GameUtils.getString(bt.getAttriDes2()), bt.getSymbol() + val, curIsGray);
        //    }
        //    else
        //    {
        //        CreateAddAttriObj(mDetailTransPos[0], GameUtils.getString(bt.getAttriDes1()), GameUtils.getString(bt.getAttriDes2()), bt.getSymbol() + val, curIsGray);
        //        CreateAddAttriObj(mDetailTransPos[1], GameUtils.getString(bt.getAttriDes1()), GameUtils.getString(bt.getAttriDes2()), bt.getSymbol() + val, nextIsGray);
        //    }
        //}
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
        GameObject go = (GameObject)GameObject.Instantiate(mAttriTitleTxt.gameObject);

        go.transform.parent = parent.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

        go.GetComponent<Text>().text = str;
    }

    /// <summary>
    /// 创建属性标题;--基础属性;
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="str"></param>
    void CreateBaseAttriObj(GameObject parent, string str1, string str2)
    {
        GameObject go = GameObject.Instantiate(mRuneAttriObj) as GameObject;

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
    }
    public void OnDisable()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshItem, OnItemRefresh);

        if(UI_HeroRuneManager._instance != null)
        {
            UI_HeroRuneManager._instance.UpdateRuneDetailUI();
        }
    }

    public void OnDestroy()
    {
        base.OnReadyForClose();

        if (mDetailCommons != null)
        {
            for (int i = 0; i < 3; i++ )
            {
                if (mDetailCommons[i] != null)
                {
                    mDetailCommons[i].Destroy();
                    mDetailCommons[i] = null;
                }
            }
            mDetailCommons = null;
        }

        for (int i = 0; i < 2; i++ )
        {
            mCloseBtn[i].onClick.RemoveListener(OnCloseBtnClick);
            mStrenthBtn[i].onClick.RemoveListener(OnStrengthBtnClick);
        }

        mRuneObj = null;
        mRuneNameTxt = null;
        mSpeHeroNameTxt = null;
        //mStrenthRuneItem = null;
        mDetailTransPos = null;
        mStrenthBtn = null;
        mStrenthBtnTxt = null;
        mCloseBtn = null;
        mCloseBtnTxt = null;
        mCostObj = null;
        mCostGoldTxt = null;

        mItemCommons = null;
    }

    public override void InitUIData()
    {
        //if (mGo == null) return;
        mInst = transform.GetComponent<UI_RuneStrenthMgr>();

        if(!mInitDone)
        {
            mInitDone = true;

            mStrenthTitleTxt = transform.FindChild("Title/Text").GetComponent<Text>();
            mRuneObj = new GameObject[3];
            mRuneNameTxt = new Text[3];
            mSpeHeroNameTxt = new Text[3];
            //mStrenthRuneItem = new RuneIconItem[3];
            mDetailTransPos = new Transform[3];
            mStrenthBtn = new Button[2];
            mStrenthBtnTxt = new Text[2];
            mCloseBtn = new Button[2];
            mCloseBtnTxt = new Text[2];
            mCostObj = new GameObject[2];
            mNorCostItemImg = transform.FindChild("StrenthBtn2/CostObj/Gold/Text/Image").GetComponent<Image>();
            mSpecCostItemImg = transform.FindChild("StrenthBtn2/CostObj/Diamond/Text/Image").GetComponent<Image>();
            mCostGoldTxt = new Text[2];

            for (int i = 0; i < 3; i++)
            {
                string title = "RuneDetail" + (i + 1);
                mRuneObj[i] = transform.FindChild(title).gameObject;
                mRuneNameTxt[i] = transform.FindChild(title + "/RuneName_txt/Name_txt").GetComponent<Text>();
                mSpeHeroNameTxt[i] = transform.FindChild(title + "/SpecialHeroName").GetComponent<Text>();
                //mStrenthRuneItem[i] = new RuneIconItem(transform.FindChild(title + "/RunItem1"));
                mDetailTransPos[i] = transform.FindChild(title + "/Attris");
            }

            for (int i = 0; i < 2; i++)
            {
                mStrenthBtn[i] = transform.FindChild("StrenthBtn" + (i + 1)).GetComponent<Button>();
                mStrenthBtn[i].onClick.AddListener(OnStrengthBtnClick);
                mStrenthBtnTxt[i] = transform.FindChild("StrenthBtn" + (i + 1) + "/Text").GetComponent<Text>();
                mCloseBtn[i] = transform.FindChild("CloseBtn" + (i + 1)).GetComponent<Button>();
                mCloseBtn[i].onClick.AddListener(OnCloseBtnClick);
                mCloseBtnTxt[i] = transform.FindChild("CloseBtn" + (i + 1) + "/Text").GetComponent<Text>();
                mCostObj[i] = transform.FindChild("StrenthBtn" + (i + 1) + "/CostObj").gameObject;
                mCostGoldTxt[i] = transform.FindChild("StrenthBtn" + (i + 1) + "/CostObj/Gold/Text").GetComponent<Text>();
            }

            mCostItemCountTxt = transform.FindChild("StrenthBtn2/CostObj/Diamond/Text").GetComponent<Text>();
            mStrenthObj = transform.FindChild("StrenthObj").gameObject;
            mStrenthTxt = transform.FindChild("StrenthObj/Text").GetComponent<Text>();

            mStrenthFullObj1 = transform.FindChild("StrenthBtn1/FullObj").gameObject;
            mStrenthFullImg1 = transform.FindChild("StrenthBtn1/FullObj/Image").GetComponent<Image>();
            mStrenthFullText1 = transform.FindChild("StrenthBtn1/FullObj/Text").GetComponent<Text>();

            mStrenthFullObj2 = transform.FindChild("StrenthBtn2/FullObj").gameObject;
            mStrenthFullImg2 = transform.FindChild("StrenthBtn2/FullObj/Image").GetComponent<Image>();
            mStrenthFullText2 = transform.FindChild("StrenthBtn2/FullObj/Text").GetComponent<Text>();

            mCostObj1 = transform.FindChild("MoneyObj/One").gameObject;
            mCostGoldImg = transform.FindChild("MoneyObj/One/Image").GetComponent<Image>();
            mCostTxt1 = transform.FindChild("MoneyObj/One/Text").GetComponent<Text>();
            mCostObj2 = transform.FindChild("MoneyObj/Two").gameObject;
            mCost2Img1 = transform.FindChild("MoneyObj/Two/Money1/Image").GetComponent<Image>();
            mCost2Txt1 = transform.FindChild("MoneyObj/Two/Money1/Text").GetComponent<Text>();
            mCost2Img2 = transform.FindChild("MoneyObj/Two/Money2/Image").GetComponent<Image>();
            mCost2Txt2 = transform.FindChild("MoneyObj/Two/Money2/Text").GetComponent<Text>();
            mHintTxt = transform.FindChild("HintObj/Bottom/Text").GetComponent<Text>();
            mAttriTitleTxt = transform.FindChild("Items/AttriTitle").gameObject;
            mRuneAttriObj = transform.FindChild("Items/AttriPair").gameObject;
            mAddRuneAttriObj = transform.FindChild("Items/AddAttriPair").gameObject;
            mAttriDetailTxt = transform.FindChild("Items/LineTxt").gameObject;

            mItemCommons = new RuneItemCommon[3];
            mItemCommons[0] = RuneFactory.CreateRuneItemCommom(selfTransform.FindChild("RuneDetail1/RunItem1"));
            mItemCommons[1] = RuneFactory.CreateRuneItemCommom(selfTransform.FindChild("RuneDetail2/RunItem1"));
            mItemCommons[2] = RuneFactory.CreateRuneItemCommom(selfTransform.FindChild("RuneDetail3/RunItem1"));

            initString();
        }

    }

    public override void InitUIView()
    {
        base.InitUIView();


        GameUtils.SetImageGrayState(mStrenthFullImg1, true);
        GameUtils.SetImageGrayState(mStrenthFullImg2, true);

        ShowUI();
    }

    void OnStrengthBtnClick()
    {
        ItemTemplate itemT = ItemEquipInfo.GetItemRowData();
        int strengthLv = ItemEquipInfo.GetStrenghLevel();
        bool isFullLv = DataTemplate.GetInstance().IsRuneStrenthFullLevel(itemT, strengthLv);

        //已满级;
        if(isFullLv)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip2"), transform);
            return;
        }

        //费用是否足够;
        RunecostTemplate rt = DataTemplate.GetInstance().GetRuneCostTemplate(itemT.getRune_strengthenId(), strengthLv + 1);
        if(rt.getAttriType1() != -1)
        {
            long count = 0;
            if(ObjectSelf.GetInstance().TryGetResourceCountById(rt.getAttriType1(), ref count))
            {
                if(count < rt.getAttriValue1())
                {
                    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip1"), transform);
                    return;
                }
            }
            else
            {
                LogManager.LogError("错误的资源id:" + rt.getAttriType1());
            }
        }
        if(rt.getAttriType2() != -1)
        {
            long count = 0;
            if (ObjectSelf.GetInstance().TryGetResourceCountById(rt.getAttriType2(), ref count))
            {
                if (count < rt.getAttriValue2())
                {
                    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip5"), transform);
                    return;
                }
            }
            else
            {
                LogManager.LogError("错误的资源id:" + rt.getAttriType2());
            }
        }

        //成功后逻辑;
        CRefineEquip msg = new CRefineEquip();
        msg.equipkey = (int)(mRuneGUID.GUID_value);
        IOControler.GetInstance().SendProtocol(msg);

        mStrenthBtn[0].GetComponent<Button>().enabled = false;
        mStrenthBtn[1].GetComponent<Button>().enabled = false;
    }

    void OnCloseBtnClick()
    {
        HideUI();
    }

    void HideUI()
    {
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, UI_ResPath);

        if(UI_HeroRuneManager._instance != null)
        {
            UI_HeroRuneManager._instance.RunOnFront();
        }
    }

    void initString()
    {
        //mStrenthTitleTxt.text = GameUtils.getString("hero_rune_identifyform_title");
        mHintTxt.text = GameUtils.getString("rune_content1");
        string strenthBtnStr = GameUtils.getString("common_button_strengthen");
        string closeBtnStr = GameUtils.getString("common_button_close");
        for(int i = 0; i < 2; i++)
        {
            mStrenthBtnTxt[i].text = strenthBtnStr;
            mCloseBtnTxt[i].text = closeBtnStr;
        }

        mStrenthFullText1.text = GameUtils.getString("common_button_strengthen");
        mStrenthFullText2.text = GameUtils.getString("common_button_strengthen");
    }
}
