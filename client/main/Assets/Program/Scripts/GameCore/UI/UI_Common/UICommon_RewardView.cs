using UnityEngine;
using System.Collections;

using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using UnityEngine.UI;
using System.Collections.Generic;

public struct UIItemData
{
    public int count;
    public string icon;
};

public class UICommon_RewardView : UICommon_RewardViewBase, UICommonInterface
{
    protected Transform mListTrans = null;
    protected GameObject mItemObj = null;
    
    private float mCloseTime = 1.5f;

    //private List<RewardViewItemUI> mItemsList = new List<RewardViewItemUI>();
    private List<UniversalItemCell> mItemsList = new List<UniversalItemCell>();

    public override void InitUIData()
    {
        base.InitUIData();

        mListTrans = selfTransform.FindChild("Panel/ListObj/ItemList/ListLayOut");
        mItemObj = selfTransform.FindChild("Items/Item").gameObject;

        Invoke("OnClose", mCloseTime);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        CancelInvoke();
    }

    public void SetData(int dropId)
    {
        NormaldropTemplate normalT = DataTemplate.GetInstance().GetNormaldropTemplateById(dropId);
        if (normalT == null)
        {
            LogManager.LogToFile("战斗奖励界面失败---ChapterinfoTemplate is NULL. id=" + dropId);
            return;
        }

        SetData(normalT.getInnerdrop());
    }

    public void SetData(int[] innerDropIds)
    {
        ClearItems();

        //for (int i = 0, j = innerDropIds.Length; i < j;  i++)
        //{
        //    InnerdropTemplate value = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(innerDropIds[i]);
        //    if (value == null) continue;

        //    Sprite icon = DynamicItem.GetSprite(value.getObjectid());

        //    RewardViewItemUI ui = GenerateItemUI(value.getDropnum(), icon);
        //    if (ui != null)
        //    {
        //        mItemsList.Add(ui);
        //    }
        //}

        for (int i = 0, j = innerDropIds.Length; i < j; i++)
        {
            foreach (int k in DataTemplate.GetInstance().m_InnerdropTable.GetDataKeys())
            {
                InnerdropTemplate _it = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(k);

                if (_it == null) continue;

                if (_it.getInnerdropid() == innerDropIds[i])
                {
                    //Sprite icon = DynamicItem.GetSprite(_it.getObjectid());

                    //RewardBoxItemUI ui = GenerateItemUI(_it.getDropnum(), icon);
                    //if (ui != null)
                    //{
                    //    mItemsList.Add(ui);
                    //}

                    UniversalItemCell cell = UniversalItemCell.GenerateItem(mListTrans);
                    cell.InitByID(_it.getObjectid(), _it.getDropnum());
                    if (cell != null)
                    {
                        mItemsList.Add(cell);
                    }
                }

            }
        }
    }

    //public void SetData(List<UIItemData> datas)
    //{
    //    ClearItems();

    //    for (int i = 0; i < datas.Count; i++ )
    //    {
    //        RewardViewItemUI ui = GenerateItemUI(datas[i].count, datas[i].icon);
    //        if (ui != null)
    //        {
    //            mItemsList.Add(ui);
    //        }
    //    }
    //}

    private void OnClose()
    {
        UICommonManager.Inst.RemoveUI(UICommonType.CommonRewardView, this);
    }


    void ClearItems()
    {
        for (int i = 0; i < mItemsList.Count; i++ )
        {
            mItemsList[i].Destroy();
        }

        mItemsList.Clear();
    }

    RewardViewItemUI GenerateItemUI(int count, string icon)
    {
        return GenerateItemUI(count, UIResourceMgr.LoadSprite(icon));
    }

    RewardViewItemUI GenerateItemUI(int count, Sprite iconSpr)
    {
        try
        {
            GameObject go = Instantiate(mItemObj) as GameObject;

            RewardViewItemUI ui = new RewardViewItemUI(go.transform, mListTrans);
            ui.SetImg(iconSpr);
            ui.SetCount(count);

            return ui;
        }
        catch (System.Exception ex)
        {
            LogManager.LogError(ex);
        }

        return null;
    }
}


public class RewardViewItemUI
{
    private Transform mTrans = null;

    protected Image icon = null;
    protected Text count = null;

    public RewardViewItemUI(Transform trans, Transform parent)
    {
        mTrans = trans;
        trans.SetParent(parent, false);

        icon = trans.FindChild("iconImg").GetComponent<Image>();
        count = trans.FindChild("count").GetComponent<Text>();
    }

    public void SetImg(Sprite spr)
    {
        icon.sprite = spr;
    }
    
    public void SetCount(int num)
    {
        count.text = num.ToString();
    }

    public void Destroy()
    {
        GameObject.Destroy(mTrans);
    }
}
