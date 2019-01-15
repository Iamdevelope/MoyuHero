using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameCore;

public class HeroCountItem : BaseUI
{
	public bool isReach;   // 是否已达到
	public int needSoulCount;  // 需要的铸魂数量
	public int soulCount;  // 已经铸魂的数量
	public int curSoulCount;   // 现在的铸魂数量
	public int possessCount;

	public int tableID;

	private HeroTemplate _heroData;                              //英雄表数据
	private Artifact _artifact;      // 神器数据
	private ObjectCard _card;

	private Image _icon;                // 英雄 icon
	private GameObject _star;          // 星级
	private GameObject _reach;        // 已达到
	private GameObject _count;       // 现在已经铸魂的数量
	private GameObject _possess;    // 拥有
	private Text _name;

	public override void InitUIData()
	{
		_icon = selfTransform.FindChild("Icon").GetComponent<Image>();
		_star = selfTransform.FindChild("Star_Image").gameObject;
		_reach = selfTransform.FindChild("Reach").gameObject;
		_count = selfTransform.FindChild("Count").gameObject;
		_possess = selfTransform.FindChild("Possess").gameObject;
		_name = selfTransform.FindChild("Name").GetComponent<Text>();
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	// 显示每一个英雄的数据        英雄的表数据          对应的神器         需要的英雄数量    索引
	public void ShowInfo(HeroTemplate heroData, Artifact artifact, int heroNumber, int index)
	{
		_heroData = heroData;
		_artifact = artifact;
		tableID = heroData.getId();

		// 星级
		int quality = heroData.getQuality();
		for (int i = 5; i < quality + 5; i++)
		{
			_star.transform.GetChild(i).gameObject.SetActive(true);
		}

		// icon 
		ArtresourceTemplate artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(heroData.getArtresources());
		Sprite _img = UIResourceMgr.LoadSprite(common.defaultPath + artresourcedata.getHeadiconresource());
		_icon.sprite = _img;//图片
        _icon.SetNativeSize();

		// 名称
		_name.text = GameUtils.getString(heroData.getTitleID());

		ShowCount(index, heroNumber);
	}

	// 显示需要的数量        索引          需要的英雄数量      是否已达成
	public void ShowCount(int index, int heroNumber, bool isReach = false)
	{
		if (isReach)
		{
			_reach.SetActive(true);
			_count.SetActive(false);
			_possess.SetActive(false);
			this.isReach = isReach;
			return;
		}
		else
		{
			_reach.SetActive(false);
			_count.SetActive(true);
			_possess.SetActive(true);
		}

		// 是否已达成
		if (_artifact.GetArtifactDB().m_IntoRecord[index] >= heroNumber)
		{
			_reach.SetActive(true);
			_count.SetActive(false);
			_possess.SetActive(false);
			this.isReach = true;
		}
		else
		{
			this.isReach = false;
			// 现在的英雄数量
			Text curCount = _count.transform.FindChild("CurCount").GetComponent<Text>();
			curCount.text = _artifact.GetArtifactDB().m_IntoRecord[index].ToString();
			curSoulCount = _artifact.GetArtifactDB().m_IntoRecord[index];

			// 需要的英雄的数量
			Text needCount = _count.transform.FindChild("NeedCount").GetComponent<Text>();
			needCount.text = "/" + heroNumber.ToString();
			needSoulCount = heroNumber;

			// 每一个英雄的 iD
			int heroID = _artifact.GetArtifactRow().getHeroID()[index];
			possessCount = GetHeroCount(heroID);
			_possess.transform.FindChild("Number").GetComponent<Text>().text = GetHeroCount(heroID).ToString();
			tableID = heroID / 10;  // 每一个英雄的 tableID
		}
	}

	public void ShowInfo(ArtifactTemplate artifact, int index, bool isReach = false)
	{
		if (isReach)
		{
			_reach.SetActive(true);
			_count.SetActive(false);
			_possess.SetActive(false);
		}

		HeroTemplate heroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(artifact.getHeroID()[index]);
		// 星级
		int quality = heroData.getQuality();
		for (int i = 5; i < quality + 5; i++)
		{
			_star.transform.GetChild(i).gameObject.SetActive(true);
		}

		// icon 
		ArtresourceTemplate artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(heroData.getArtresources());
		Sprite _img = UIResourceMgr.LoadSprite(common.defaultPath + artresourcedata.getHeadiconresource());
		_icon.sprite = _img;//图片

		// 名称
		_name.text = GameUtils.getString(heroData.getTitleID());

		// 现在的英雄数量
		Text curCount = _count.transform.FindChild("CurCount").GetComponent<Text>();
		curCount.text = "0";

		// 需要的英雄的数量
		Text needCount = _count.transform.FindChild("NeedCount").GetComponent<Text>();
		needCount.text = "/" + artifact.getHeroNum()[index].ToString();

		// 每一个英雄的 iD
		int heroID = artifact.getHeroID()[index];
		_possess.transform.FindChild("Number").GetComponent<Text>().text = GetHeroCount(heroID).ToString();
	}

	// 通过英雄 ID 获取现在的英雄有多少个
	int GetHeroCount(int heroID)
	{
		int heroid = heroID / 10;
		int level = heroID % 10;

		List<ObjectCard> heroList = new List<ObjectCard>();
		heroList = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList();

		int count = 0;
		for (int i = 0; i < heroList.Count; ++i)
		{
			int tableID = heroList[i].GetHeroData().TableID;
			if (tableID / 10 == heroid && tableID % 10 >= level)
			{
				count++;
				//Debug.Log("HeroCountItem TableID " + tableID.ToString());
			}
		}
		return count;
	}

	public void UpdateCount(int count)
	{
		curSoulCount += count;
		possessCount -= count;

		Text curCount = _count.transform.FindChild("CurCount").GetComponent<Text>();
		curCount.text = curSoulCount.ToString();

		_possess.transform.FindChild("Number").GetComponent<Text>().text = possessCount.ToString();
	}
}
