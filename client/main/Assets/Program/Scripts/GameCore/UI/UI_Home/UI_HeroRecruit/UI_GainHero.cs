using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using System.Collections.Generic;
using GNET;
using DreamFaction.GameCore;

public class UI_GainHero : UI_GainHeroBase
{
	public static UI_GainHero inst;
	public static string UI_ResPath = "UI_Recruit/UI_GainHero_2_1";

	private LoopLayout m_HeroLayout;
	public List<ObjectCard> m_CardList = new List<ObjectCard>();

	public override void InitUIData()
	{
		base.InitUIData();

        m_HeroLayout = selfTransform.FindChild("HeroList/HeroLayout").GetComponent<LoopLayout>();

		inst = this;
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="type"></param>
	public void ShowHeroData(LinkedList<int> herolist)
	{
		m_CardList.Clear();
		foreach (var item in herolist)
		{
			m_CardList.Add(GetObjectCard(item));
		}

		m_HeroLayout.cellCount = m_CardList.Count;
        m_HeroLayout.updateCellEvent = UpdateHeroItem;
        m_HeroLayout.Reload();

        string number = string.Format(GameUtils.getString("recruit_bubble1"), 10);
        RichText richText = RichText.GetRichText(number);
        InterfaceControler.GetInst().AddMsgBox(richText.transform, selfTransform.transform.parent);
	}

    void UpdateHeroItem(int index, RectTransform hero)
    {
        //ObjectCard objHero = m_CardList[index];
        //UI_HeroListItem uiIt = hero.gameObject.GetComponent<UI_HeroListItem>();
        //if(uiIt == null)
        //{
        //    hero.gameObject.AddComponent<Button>();
        //    uiIt = hero.gameObject.AddComponent<UI_HeroListItem>();
        //}

        //uiIt.index = index;
        //uiIt.m_id = index;
        //uiIt.tableId = objHero.GetHeroData().TableID;
        //uiIt.Initialize(objHero);

        //uiIt.SetSelectClick();
    }

	// 加载英雄
    //GameObject LoadHero(int index)
    //{
    //    //GameObject cell = null;

    //    //if (index < m_CardList.Count)
    //    //{
    //    //    cell = Instantiate(Resources.Load("UI/Prefabs/UI_Home/UI_HeroItem")) as GameObject;
    //    //    cell.AddComponent<Button>();

    //    //    ObjectCard objHero = m_CardList[index];
    //    //    UI_HeroListItem uiIt = cell.AddComponent<UI_HeroListItem>();

    //    //    uiIt.m_id = index;
    //    //    uiIt.tableId = objHero.GetHeroData().TableID;
    //    //    uiIt.Initialize(objHero);

    //    //    uiIt.SetSelectClick();
    //    //}

    //    //return cell;
    //}

	ObjectCard GetObjectCard(int heroid)
	{
		HeroTemplate heroT = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroid);

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

    protected override void OnClickShipBtn()
    {

    }
}
