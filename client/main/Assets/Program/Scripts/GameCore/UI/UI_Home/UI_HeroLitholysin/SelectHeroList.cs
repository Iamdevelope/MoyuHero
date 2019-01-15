using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI;
using System.Collections.Generic;
using DreamFaction.Utils;

public class SelectHeroList : ItemCell
{
	public int index;
	public int tableId;
	public ObjectCard objHero;
	private HeroTemplate _HeroItem;                              //英雄表数据
	private Image _icon;
	public int m_heroStar; //英雄星级
    private string url = common.defaultPath;

	public override void InitUIData()
	{
		base.InitUIData();
		_icon = selfTransform.FindChild("Icon").GetComponent<Image>();
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	public void Initialize(ObjectCard objHero)
	{
		this.objHero = objHero;
		if (tableId == null) return;
		_HeroItem = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(tableId);
		//卡牌图标
        ArtresourceTemplate _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(objHero.GetHeroData().GetHeroViewID());
		Sprite _img = UIResourceMgr.LoadSprite(url + _Artresourcedata.getHeadiconresource());
		_icon.sprite = _img;
        _icon.SetNativeSize();

		//星级
		m_heroStar = _HeroItem.getQuality();
		for (int i = 5; i < 5 + m_heroStar; ++i)
		{
			Image temp = selfTransform.FindChild("Star_Image").GetChild(i).GetComponent<Image>();
			temp.enabled = true;
            temp.gameObject.SetActive(true);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
