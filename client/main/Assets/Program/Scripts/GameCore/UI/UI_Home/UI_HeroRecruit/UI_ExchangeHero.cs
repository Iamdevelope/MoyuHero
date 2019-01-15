using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using System.Collections.Generic;
using GNET;

public class UI_ExchangeHero : UI_ExchangeHeroBase
{
	public static UI_ExchangeHero inst;
	public static string UI_ResPath = "UI_Recruit/UI_ExchageHero_2_1";

	GameObject m_Center;
	GameObject m_HeroItem = null;

	public override void InitUIData()
	{
		base.InitUIData();
		inst = this;

		if (ObjectSelf.GetInstance().dreamfree == 0)
		{
			m_FreeTipsText.text = GameUtils.getString("hero_recruit_content11");
		}
		else
		{
			m_FreeTipsText.text = "X" + DataTemplate.GetInstance().m_GameConfig.getDream_refresh_cost();
		}

		m_Center = selfTransform.FindChild("UI_Center").gameObject;
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	public void ShowHeroData(int heroID)
	{
		//Debug.Log("HeroID" + heroID);

        //if (m_HeroItem == null)
        //{
        //    m_HeroItem = Instantiate(Resources.Load("UI/Prefabs/UI_Home/UI_HeroItem")) as GameObject;
        //    m_HeroItem.transform.FindChild("Parent").localPosition = Vector3.zero;
        //    m_HeroItem.AddComponent<Button>();

        //    ObjectCard objHero = GetObjectCard(heroID);
        //    UI_HeroListItem uiIt = m_HeroItem.AddComponent<UI_HeroListItem>();

        //    uiIt.tableId = objHero.GetHeroData().TableID;
        //    uiIt.Initialize(objHero);

        //    uiIt.SetSelectClick();

        //    m_HeroItem.transform.parent = m_Center.transform;
        //    m_HeroItem.transform.localPosition = new Vector3(0, 0, 0);
        //    m_HeroItem.transform.localScale = new Vector3(1, 1, 1);
        //}
        //else
        //{
        //    ObjectCard objHero = GetObjectCard(heroID);
        //    UI_HeroListItem uiIt = m_HeroItem.AddComponent<UI_HeroListItem>();
        //    uiIt.tableId = objHero.GetHeroData().TableID;
        //    uiIt.Initialize(objHero);

        //    uiIt.SetSelectClick();
        //}
	}

	protected override void OnClickRefreshBtn()
	{
		// 金币不足
		int cost = DataTemplate.GetInstance().m_GameConfig.getDream_refresh_cost();
		if (UI_HeroRecruit.inst.TipsGoldValue(cost))
		{
			return;
		}

		CChangeDream proto = new CChangeDream();
		IOControler.GetInstance().SendProtocol(proto);
	}

	protected override void OnClickExchangeBtn()
	{
		CGetDream proto = new CGetDream();
		IOControler.GetInstance().SendProtocol(proto);
	}

	public void SuccessExchangeHero()
	{
		UI_HeroRecruit.inst.RefreshDreamValue();
		UI_HomeControler.Inst.ReMoveUI(gameObject);
	}

	public void SuccessRefresh()
	{
		m_FreeTipsText.text = "X" + DataTemplate.GetInstance().m_GameConfig.getDream_refresh_cost();
	}

	ObjectCard GetObjectCard(int heroID)
	{
		HeroTemplate heroT = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroID);

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
}
