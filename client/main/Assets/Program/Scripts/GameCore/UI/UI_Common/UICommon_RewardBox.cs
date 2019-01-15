using UnityEngine;
using System.Collections;

using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using UnityEngine.UI;
using System.Collections.Generic;

public class UICommon_RewardBox : UICommon_RewardBoxBase, UICommonInterface
{
    public class RewardBoxItemUI
    {
        private Transform mTrans = null;

        protected Image icon = null;
        protected Text count = null;

        public RewardBoxItemUI(Transform trans, Transform parent)
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


    protected Transform mListTrans = null;
    protected GameObject mItemObj = null;
    protected RichText mDetailTxt = null;

    //private List<RewardBoxItemUI> mItemsList = new List<RewardBoxItemUI>();
    private List<UniversalItemCell> mItemsList = new List<UniversalItemCell>();

    public override void InitUIData()
    {
        base.InitUIData();

        mListTrans = selfTransform.FindChild("Panel/ListObj/ItemList/ListLayOut");
        mDetailTxt = selfTransform.FindChild("Panel/DetailTxt").GetComponent<RichText>();
        mItemObj = selfTransform.FindChild("Items/Item").gameObject;
    }

    public void SetData(int dropId, int starNum)
    {
        NormaldropTemplate normalT = DataTemplate.GetInstance().GetNormaldropTemplateById(dropId);
        if (normalT == null)
        {
            LogManager.LogToFile("战斗奖励界面失败---ChapterinfoTemplate is NULL. id=" + dropId);
            return;
        }

        SetData(normalT.getInnerdrop(), starNum);
    }

    public void SetData(int[] innerDropIds, int starNum)
    {
        ClearItems();

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

        mDetailTxt.ShowRichText(string.Format(GameUtils.getString("maoxianguanka7"), starNum));
    }

    //public void SetData(List<UIItemData> datas)
    //{
    //    ClearItems();

    //    for (int i = 0; i < datas.Count; i++)
    //    {
    //        RewardBoxItemUI ui = GenerateItemUI(datas[i].count, datas[i].icon);
    //        if (ui != null)
    //        {
    //            mItemsList.Add(ui);
    //        }
    //    }
    //}


    void ClearItems()
    {
        for (int i = 0; i < mItemsList.Count; i++)
        {
            mItemsList[i].Destroy();
        }

        mItemsList.Clear();
    }

    RewardBoxItemUI GenerateItemUI(int count, string icon)
    {
        return GenerateItemUI(count, UIResourceMgr.LoadSprite(icon));
    }

    RewardBoxItemUI GenerateItemUI(int count, Sprite iconSpr)
    {
        try
        {
            GameObject go = Instantiate(mItemObj) as GameObject;

            RewardBoxItemUI ui = new RewardBoxItemUI(go.transform, mListTrans);
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

    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();

        UICommonManager.Inst.RemoveUI(UICommonType.CommonRewardBox, this);
    }

    protected override void OnClickiconImg()
    {
        base.OnClickiconImg();
    }
}
