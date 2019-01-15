using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using DreamFaction;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.LogSystem;
using DreamFaction.GameEventSystem;

/// <summary>
/// ---------------------掉落小包公用UI-----------------------
/// </summary>

public class UI_NormalDrop : BaseUI
{
    private static UI_NormalDrop m_Inst = null;
    public static readonly string UI_ResPath = "UI_Explore/UI_NormalDrop_1_2";

    Text titleTxt;
    GameObject listObj;
    GameObject itemObj;
    Button closeBtn;
    Text closeBtnTxt;

    private List<UniversalItemCell> itemsList = new List<UniversalItemCell>();

    public static UI_NormalDrop Inst
    {
        get
        {
            return m_Inst;
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();

        m_Inst = this;

        titleTxt = transform.FindChild("Image/Text").GetComponent<Text>();
        listObj = transform.FindChild("Attris/DetailObj").gameObject;
        itemObj = transform.FindChild("Items/Item").gameObject;
        closeBtn = transform.FindChild("CloseBtn").GetComponent<Button>();
        closeBtnTxt = transform.FindChild("CloseBtn/Text").GetComponent<Text>();

        closeBtn.onClick.AddListener(OnCloseBtnClick);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        titleTxt.text = GameUtils.getString("common_rewardsget");
        closeBtnTxt.text = GameUtils.getString("common_button_close");
    }

    private void OnDestroy()
    {
        base.OnReadyForClose();

        itemsList = null;
        m_Inst = null;
    }

    public void ShowNormalDrop(int dropId)
    {
        NormaldropTemplate normalT = DataTemplate.GetInstance().GetNormaldropTemplateById(dropId);
        if (normalT == null)
        {
            LogManager.LogToFile("战斗奖励界面失败---ChapterinfoTemplate is NULL. id=" + dropId);
            return;
        }

        ShowInnerDrop(normalT.getInnerdrop());
    }

    public void ShowInnerDrop(int[] innerdropIds)
    {
        List<int> innerdropIDList = new List<int>(innerdropIds);

        ShowInnerDrop(innerdropIDList);
    }

    public void ShowInnerDrop(List<int> innerdropIDList)
    {
        for (int i = 0; i < innerdropIDList.Count; i++)
        {
            UniversalItemCell m_Cell = UniversalItemCell.GenerateItem(listObj.transform);

            InnerdropTemplate value = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(innerdropIDList[i]);
            if (value == null) return;

            int itemid = value.getObjectid();//掉落物ID
            int type = value.getObjectid() / 1000000;
            m_Cell.AddClickListener(OnItemClick);

            switch (type)
            {
                case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                    ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                    if (_temp_res != null)
                    {
                        m_Cell.InitByID(itemid, value.getDropnum());
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
                            m_Cell.InitByID(itemid, value.getDropnum());
                            m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                        }
                    }
                    break;

                default:
                    break;
            }
        } 
    }

    void OnItemClick(int param)
    {
        ShopModule.ShowItemPreviewUIHandler(param);
    }

    void CloseUI()
    {
        OnReadyForClose();

        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    void OnCloseBtnClick()
    {
        CloseUI();
    }
}
