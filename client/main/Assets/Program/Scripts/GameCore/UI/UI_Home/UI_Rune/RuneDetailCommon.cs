using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GNET;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction;
using DreamFaction.LogSystem;

public enum RuneAttriShowType
{
    BaseAttriOnly,
    BaseContent,
    All,
}

public class RuneDetailCommon
{
    public class RuneDetailCommonObjData
    {
        public IRuneDetailObj obj = null;
        public RuneDetailObjType objType;
        public bool isInUse = false;

        public void SetObjActive(bool active)
        {
            obj.SetActive(active);
        }
    }

    protected Transform mParent = null;
    protected List<RuneDetailCommonObjData> mObjPool = new List<RuneDetailCommonObjData>();
    protected X_GUID mRuneGUID = null;
    protected int mRuneTableId = -1;
    protected ItemEquip data = null;
    protected int mStrenLvAdder = 0;

    private Transform layoutTrans = null;
    private bool mIsActive = true;
    //private bool mIsDirty = true;

    ItemEquip ItemEquipInfo
    {
        get
        {
            //if (data == null || mIsDirty)
            //{
            //    mIsDirty = false;
            //    if (mRuneGUID == null)
            //        return null;
            //    data = (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, mRuneGUID);
            //}
            //return data;
            if (data == null)
            {
                if (mRuneGUID == null)
                    return null;
                data = (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, mRuneGUID);
            }
            return data;
        }
    }

    public RuneDetailCommon(Transform parent, X_GUID runeId, float scrollRectHeight = -1f, int strenLvAdder = 0)
    {
        mParent = parent;
        mRuneGUID = runeId;
        mStrenLvAdder = strenLvAdder;

        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshItem, OnItemRefresh);

        RuneDetailScrollRect scrollRect = RuneFactory.Create(RuneDetailObjType.ScrollRect, parent) as RuneDetailScrollRect;
        layoutTrans = scrollRect.GetLayoutObj().transform;

        if (scrollRectHeight > 0f)
        {
            scrollRect.SetScrollRectWidth(scrollRectHeight);
        }

        UpdateData(RuneAttriShowType.All);
    }

    public RuneDetailCommon(Transform parent, int runeTableId, float scrollRectHeight = -1f)
    {
        mParent = parent;
        mRuneTableId = runeTableId;
        ItemTemplate itemT = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(runeTableId);
        SetRuneTemplateDate(itemT);

        RuneDetailScrollRect scrollRect = RuneFactory.Create(RuneDetailObjType.ScrollRect, parent) as RuneDetailScrollRect;
        layoutTrans = scrollRect.GetLayoutObj().transform;

        if (scrollRectHeight > 0f)
        {
            scrollRect.SetScrollRectWidth(scrollRectHeight);
        }

        bool isSpec = RuneModule.IsSpecialRune(runeTableId);

        UpdateData(isSpec ? RuneAttriShowType.BaseContent : RuneAttriShowType.BaseAttriOnly);
    }

    public void SetShowData(X_GUID runeId)
    {

        mRuneTableId = -1;
        mRuneGUID = runeId;
        data = null;

        UpdateData(RuneAttriShowType.All);
    }

    protected void UpdateData(RuneAttriShowType type = RuneAttriShowType.All)
    {
        //Debug.LogError("RuneDetailCommon Update data");

        SetObjAllUseless();
        RuneData runeData = ItemEquipInfo.GetRuneData();

        bool titleDone1 = false;
        foreach (int id in runeData.BaseAttributeID)
        {
            if (id == -1)
                continue;
            
            int fullLv = DataTemplate.GetInstance().GetRuneStrenthMaxLevel(ItemEquipInfo.GetItemRowData());

            if (ItemEquipInfo.GetStrenghLevel() + mStrenLvAdder > fullLv)
            {
                return;
            }

            if (!titleDone1)
            {
                titleDone1 = true;
                //CreateTitle(mAttriList, GameUtils.getString("hero_rune_content8"));
                RuneDetailTitle title = GetOrCreateRuneDetailObj(RuneDetailObjType.Title, layoutTrans) as RuneDetailTitle;
                title.SetTxt(GameUtils.getString("hero_rune_content8"));
                title.SetActive(true);
            }

            BaseruneattributeTemplate bt = DataTemplate.GetInstance().GetBaseruneattributeTemplate(id + mStrenLvAdder);
            if (bt.getNumshow() == 0 && type != RuneAttriShowType.BaseAttriOnly)
            {
                //CreateDetailTxts(mAttriList, GameUtils.getString(bt.getAttriDes()));
                RuneDetailContent content = GetOrCreateRuneDetailObj(RuneDetailObjType.Content, layoutTrans) as RuneDetailContent;
                content.SetTxt(GameUtils.getString(bt.getAttriDes()));
                content.SetActive(true);
            }
            else
            {
                //CreateBaseAttriObj(mAttriList, GameUtils.getString(bt.getAttriDes()), "+" + bt.getAttriValue().ToString());
                RuneDetailTwoAttri twoAttri = GetOrCreateRuneDetailObj(RuneDetailObjType.TwoAttri, layoutTrans) as RuneDetailTwoAttri;
                twoAttri.SetLeftTxt(GameUtils.getString(bt.getAttriDes()));
                twoAttri.SetRightTxt("+" + bt.getAttriValue().ToString());
                twoAttri.SetActive(true);
            }
        }

        if (type == RuneAttriShowType.All)
        {
            //--------附加属性;
            int count = DataTemplate.GetInstance().GetRuneMaxRedefineTimes(ItemEquipInfo.GetItemRowData());
            bool titleDone2 = false;
            int i = 0;
            bool isGray = false;

            foreach (int id in runeData.AppendAttribute)
            {
                i++;

                isGray = i * 3 > ItemEquipInfo.GetStrenghLevel() + mStrenLvAdder;

                if (id == -1)
                {
                    if (i <= count)
                    {
                        if (!titleDone2)
                        {
                            titleDone2 = true;
                            //CreateTitle(mAttriList, GameUtils.getString("hero_rune_content9"));
                            RuneDetailTitle title = GetOrCreateRuneDetailObj(RuneDetailObjType.Title, layoutTrans) as RuneDetailTitle;
                            title.SetTxt(GameUtils.getString("hero_rune_content9"));
                            title.SetActive(true);
                        }
                        //未知属性，未鉴定;
                        //CreateAddAttriObj(mAttriList, GameUtils.getString("rune_content2"), "", GameUtils.getString("rune_content3"), isGray);
                        RuneDetailThreeAtrri threeAttri = GetOrCreateRuneDetailObj(RuneDetailObjType.ThreAttri, layoutTrans) as RuneDetailThreeAtrri;
                        threeAttri.SetLeftTxt(GameUtils.getString("rune_content2"), isGray);
                        threeAttri.SetMidTxt("", isGray);
                        threeAttri.SetRightTxt(GameUtils.getString("rune_content3"), isGray);
                        threeAttri.SetActive(true);
                    }

                    continue;
                }

                if (!titleDone2)
                {
                    titleDone2 = true;
                    //CreateTitle(mAttriList, GameUtils.getString("hero_rune_content9"));
                    RuneDetailTitle title = GetOrCreateRuneDetailObj(RuneDetailObjType.Title, layoutTrans) as RuneDetailTitle;
                    title.SetTxt(GameUtils.getString("hero_rune_content9"));
                    title.SetActive(true);
                }

                AddruneattributeTemplate bt = DataTemplate.GetInstance().GetAddruneattributeTemplate(id);
                bool isPercent = bt.getIspercentage() > 0;
                string val = isPercent ? ((float)bt.getAttriValue() / (float)10f + "%") : bt.getAttriValue().ToString();
                //CreateAddAttriObj(mAttriList, GameUtils.getString(bt.getAttriDes1()), GameUtils.getString(bt.getAttriDes2()), bt.getSymbol() + val, isGray);
                RuneDetailThreeAtrri threeAttri1 = GetOrCreateRuneDetailObj(RuneDetailObjType.ThreAttri, layoutTrans) as RuneDetailThreeAtrri;
                threeAttri1.SetLeftTxt(GameUtils.getString(bt.getAttriDes1()), isGray);
                threeAttri1.SetMidTxt(GameUtils.getString(bt.getAttriDes2()), isGray);
                threeAttri1.SetRightTxt(bt.getSymbol() + val, isGray);
                threeAttri1.SetActive(true);
            }
        }

    }

    void SetRuneTemplateDate(ItemTemplate itemT)
    {
        if (itemT == null)
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
        SetRuneEquipDate(data);
    }

    void SetRuneEquipDate(ItemEquip datas)
    {

        data = datas;
    }

    void SetObjAllUseless()
    {
        for (int i = 0; i < mObjPool.Count; i++ )
        {
            mObjPool[i].isInUse = false;
            mObjPool[i].SetObjActive(false);
        }
    }

    void OnItemRefresh(GameEvent ge)
    {
        if (ge == null) return;

        Item it = ge.data as Item;
        if (it == null) return;

        if (mRuneGUID.GUID_value == it.key)
            UpdateData();
    }

    protected IRuneDetailObj GetOrCreateRuneDetailObj(RuneDetailObjType type, Transform parent)
    {
        if (mObjPool != null && mObjPool.Count > 0)
        {
            for (int i = 0; i < mObjPool.Count; i++ )
            {
                if (mObjPool[i].isInUse)
                    continue;

                if (mObjPool[i].objType != type)
                    continue;

                mObjPool[i].isInUse = true;
                return mObjPool[i].obj;
            }
        }

        IRuneDetailObj detailObj = RuneFactory.Create(type, parent);
        
        RuneDetailCommonObjData objData = new RuneDetailCommonObjData();
        objData.isInUse = true;
        objData.obj = detailObj;
        objData.objType = type;

        mObjPool.Add(objData);

        return detailObj;
    }

    public void Destroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshItem, OnItemRefresh);

        for (int i = 0; i < mObjPool.Count; i++ )
        {
            mObjPool[i].obj.Destroy();
        }

        data = null;
        mParent = null;
        mObjPool = null; 
        mRuneGUID = null;
        mRuneTableId = -1;
        layoutTrans = null;
    }

    public void SetActive(bool isActive)
    {
        if (mIsActive != isActive)
        {
            layoutTrans.gameObject.SetActive(isActive);
            mIsActive = isActive;
        }
    }
}
