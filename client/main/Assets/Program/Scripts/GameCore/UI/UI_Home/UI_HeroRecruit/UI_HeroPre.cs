using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using System.Collections.Generic;
using GNET;
using DreamFaction.UI;

public class UI_HeroPre : UI_HeroPreBase
{
	public static UI_HeroPre inst;
	public static string UI_ResPath = "UI_Recruit/UI_HeroPre_2_1";

	private LoopLayout m_HeroLayout;

	public List<HerorecruitTemplate> m_HeroTemp = new List<HerorecruitTemplate>();

	GameObject m_HeroItem;

	public override void InitUIData()
	{
		base.InitUIData();

        m_HeroLayout = selfTransform.FindChild("HeroList/HeroLayout").GetComponent<LoopLayout>();
		m_HeroItem = UIResourceMgr.LoadPrefab(common.prefabPath + "UI_Home/UI_HeroItem") as GameObject;
		inst = this;
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	/// <summary>
	/// 显示英雄数据 根据不同的打开方式加载不同的数据
	/// </summary>
	/// <param name="type"></param>
	public void ShowHeroData(int type)
	{
		m_HeroTemp.Clear();

		Dictionary<int, IExcelBean> _data = DataTemplate.GetInstance().m_HeroRecruitTable.getData();
		// 单抽
		if (type == 1)
		{
			foreach (var _item in _data)
			{
				HerorecruitTemplate _temp = (HerorecruitTemplate)DataTemplate.GetInstance().m_HeroRecruitTable.getTableData(_item.Key);
				if (_temp.getInitialweight1() != 0)
				{
					m_HeroTemp.Add(_temp);
				}
			}
		}
		else if (type == 2)   // 十连抽
		{
			foreach (var _item in _data)
			{
				HerorecruitTemplate _temp = (HerorecruitTemplate)DataTemplate.GetInstance().m_HeroRecruitTable.getTableData(_item.Key);
				if (_temp.getInitialweight2() != 0)
				{
					m_HeroTemp.Add(_temp);
				}
			}
		}
		else if(type == 3)     // 梦想兑换
		{
			foreach (var _item in _data)
			{
				HerorecruitTemplate _temp = (HerorecruitTemplate)DataTemplate.GetInstance().m_HeroRecruitTable.getTableData(_item.Key);
				if (_temp.getInitialweight4() != 0)
				{
					m_HeroTemp.Add(_temp);
				}
			}
		}

		SortHeroWithQuailty(ref m_HeroTemp);
		m_HeroLayout.cellCount = m_HeroTemp.Count;
        m_HeroLayout.updateCellEvent = UpdateHeroItem;
        m_HeroLayout.Reload();
	}

    void UpdateHeroItem(int index, RectTransform cell)
    {
        //ObjectCard objHero = GetObjectCard(m_HeroTemp[index]);
        //UI_HeroListItem uiIt = cell.GetComponent<UI_HeroListItem>();
        //if(uiIt == null)
        //{
        //    cell.gameObject.AddComponent<Button>();

        //    uiIt = cell.gameObject.AddComponent<UI_HeroListItem>();
        //}

        //uiIt.index = index;
        //uiIt.m_id = index;
        //uiIt.tableId = objHero.GetHeroData().TableID;
        //uiIt.Initialize(objHero);

        //uiIt.SetSelectClick();
    }

    //// 加载英雄
    //GameObject LoadHero(int index)
    //{
    //    GameObject cell = null;

    //    if (index < m_HeroTemp.Count)
    //    {
    //        cell = Instantiate(m_HeroItem) as GameObject;
    //        cell.transform.FindChild("Parent").localPosition = Vector3.zero;
    //        cell.AddComponent<Button>();

    //        ObjectCard objHero = GetObjectCard(m_HeroTemp[index]);
    //        UI_HeroListItem uiIt = cell.AddComponent<UI_HeroListItem>();

    //        uiIt.m_id = index;
    //        uiIt.tableId = objHero.GetHeroData().TableID;
    //        uiIt.Initialize(objHero);

    //        uiIt.SetSelectClick();
    //    }

    //    return cell;
    //}

	ObjectCard GetObjectCard(HerorecruitTemplate _temp)
	{
		HeroTemplate heroT = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(_temp.GetID());

		ObjectCard obj = new ObjectCard();
        Hero hero = new Hero();
        hero.skill1 = heroT.getSkill1ID();
        hero.skill2 = heroT.getSkill2ID();
        hero.skill3 = heroT.getSkill3ID();
        hero.heroid = heroT.getId();
        hero.herolevel = 1;
        hero.heroviewid = heroT.getArtresources();
        obj.GetHeroData().Init(hero);

		return obj;
	}

    protected override void OnClickUI_Btn_Back()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
    
	void SortHeroWithQuailty(ref List<HerorecruitTemplate> heroList)
	{
		for (int i = 0; i < heroList.Count - 1; ++i)
		{
			HerorecruitTemplate item = heroList[i];
			for (int j = i; j < heroList.Count; ++j)
			{
				HeroTemplate hero1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroList[i].GetID());
				int quality1 = hero1.getQuality();

				HeroTemplate hero2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroList[j].GetID());
				int quality2 = hero2.getQuality();

				if (quality2 > quality1)
				{
					item = heroList[j];
					heroList[j] = heroList[i];
					heroList[i] = item;
				}
				else if (quality2 == quality1)
				{
					if (hero2.GetID() > hero1.GetID())
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
