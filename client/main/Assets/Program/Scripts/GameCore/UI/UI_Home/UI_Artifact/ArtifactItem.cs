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

public class ArtifactItem : CellItem
{
	public int artiIndex;
	private Artifact _artifact;

	private Button _preview;  // 预览按钮
	public Button _soulBtn;    // 注魂按钮
	private Text _atrifactName;  // 名称
	private Text _tipsText;      // 全部达成可升级为
	private Text _nextName; // 下一级的名称
	private GameObject _heroLayout;  // 英雄布局
	private GameObject _attributeList;  // 属性对象
    private Image _artImage;

	List<HeroCountItem> heroCountList = new List<HeroCountItem>();
	public override void InitUIData()
	{
		_preview = selfTransform.FindChild("Top/Preview").GetComponent<Button>();

		_soulBtn = selfTransform.FindChild("Bottom/Soul").GetComponent<Button>();
		_atrifactName = selfTransform.FindChild("Top/AtrifactName").GetComponent<Text>();
		_tipsText = selfTransform.FindChild("Top/Text").GetComponent<Text>();
		_nextName = selfTransform.FindChild("Top/NextName").GetComponent<Text>();
		_heroLayout = selfTransform.FindChild("Center/HeroList/HeroLayout").gameObject;
		_attributeList = selfTransform.FindChild("Bottom/AttributeList").gameObject;
        _artImage = selfTransform.FindChild("Bottom/ArtiImage").GetComponent<Image>();
		_preview.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickArtifactPreview));
		_soulBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickArtifactSoul));
	}

	public override void InitUIView()
	{
		base.InitUIView();
		_atrifactName.text = GameUtils.getString(_artifact.GetArtifactRow().getName());
		int level = _artifact.GetArtifactRow().getLevel();
        ArtifactTemplate temp = (ArtifactTemplate)DataTemplate.GetInstance().m_ArtifactTable.getTableData(_artifact.GetArtifactRow().GetID() + 1);
		if (level < 5)
		{
			_nextName.text = GameUtils.getString(temp.getName());
		}
		else
		{
			_nextName.gameObject.SetActive(false);
		}
	}

	// 显示神器 Item 的所有属性，在神器界面调用
	public void ShowInfo(Artifact artifact)
	{
		_artifact = artifact;
		_tipsText.text = GameUtils.getString("relics_content1");

		if (artifact.GetArtifactDB().GetLevel() == 5)
		{
			bool ret = false;
			int[] recode = artifact.GetArtifactDB().m_IntoRecord;
			int[] heroNum = artifact.GetArtifactRow().getHeroNum();
			for (int i = 0; i < heroNum.Length; i++)
			{
				if (heroNum[i] > recode[i])
				{
					ret = true;
					break;
				}
			}

			if (ret)
			{
				_tipsText.text = GameUtils.getString("relics_content14");
			}
			else
			{
				_tipsText.text = GameUtils.getString("relics_content15");
			}				
		}

		_atrifactName.text = GameUtils.getString(_artifact.GetArtifactRow().getName());
		int level = _artifact.GetArtifactRow().getLevel();
        ArtifactTemplate temp = (ArtifactTemplate)DataTemplate.GetInstance().m_ArtifactTable.getTableData(_artifact.GetArtifactRow().GetID() + 1);
		if (level < 5)
		{
			_nextName.text = GameUtils.getString(temp.getName());
		}
		else
		{
			_nextName.gameObject.SetActive(false);
		}

		// 英雄 ID
		int[] heroID = artifact.GetArtifactRow().getHeroID();
		int[] heroNumber = artifact.GetArtifactRow().getHeroNum();

		// 每一个英雄数据
		for (int i = 0; i < heroID.Length; i++)
		{
			if (heroID[i] == -1)
			{
				continue;
			}

			HeroTemplate heroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroID[i]);
			GameObject cell = _heroLayout.transform.GetChild(i).gameObject;
			cell.SetActive(true);
            HeroCountItem item = cell.GetComponent<HeroCountItem>();
            if(item == null)
            {
                item = cell.AddComponent<HeroCountItem>();
            }
			item.ShowInfo(heroData, artifact, heroNumber[i], i);
			heroCountList.Add(item);
		}

		// 神器头像
        _artImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + temp.getResourceName());
        _artImage.SetNativeSize();

		// 初始化属性数据
		int[] attrType = artifact.GetArtifactRow().getAttriType();
		int[] attrValue = artifact.GetArtifactRow().getAttriValue();
		for (int i = 0; i < attrType.Length; i++)
		{
			if (attrType[i] == -1)
			{
				continue;
			}

			_attributeList.transform.GetChild(i).gameObject.SetActive(true);
			Text attrNameText = _attributeList.transform.GetChild(i).FindChild("AttrName").GetComponent<Text>();
			Text attrValueText = _attributeList.transform.GetChild(i).FindChild("AttrValue").GetComponent<Text>();
			Text attrStateText = _attributeList.transform.GetChild(i).FindChild("AttrState").GetComponent<Text>();
            attrStateText.color = new Color(133.0f / 255, 247.0f / 255, 32.0f / 255);
			switch (attrType[i])
			{
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP:
					{
						// 生命值
						attrNameText.text = GameUtils.getString("baseattribute1des");
						attrValueText.text = artifact.GetCurHP().ToString();
						if (artifact.GetCurHP() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK:
					{
						// 物攻
						attrNameText.text = GameUtils.getString("hero_train_type2");
						attrValueText.text = artifact.GetCurPhysicalAttack().ToString();
						if (artifact.GetCurPhysicalAttack() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE:
					{
						// 物防
						attrNameText.text = GameUtils.getString("hero_train_type4");
						attrValueText.text = artifact.GetCurPhysicalDefence().ToString();
						if (artifact.GetCurPhysicalDefence() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK:
					{
						// 法攻
						attrNameText.text = GameUtils.getString("hero_train_type3");
						attrValueText.text = artifact.GetCurMagicAttack().ToString();
						if (artifact.GetCurMagicAttack() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE:
					{
						// 法防
						attrNameText.text = GameUtils.getString("hero_train_type5");
						attrValueText.text = artifact.GetCurMagicDefence().ToString();
						if (artifact.GetCurMagicDefence() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT:
					{
						// 命中
						attrNameText.text = GameUtils.getString("baseattribute6des");
						attrValueText.text = artifact.GetCurHit().ToString();
						if (artifact.GetCurHit() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE:
					{
						// 闪避
						attrNameText.text = GameUtils.getString("baseattribute7des");
						attrValueText.text = artifact.GetCurDodge().ToString();
						if (artifact.GetCurDodge() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL:
					{
						// 暴击
						attrNameText.text = GameUtils.getString("baseattribute8des");
						attrValueText.text = artifact.GetCurCritical().ToString();
						if (artifact.GetCurCritical() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY:
					{
						// 韧性
						attrNameText.text = GameUtils.getString("baseattribute9des");
						attrValueText.text = artifact.GetCurTenacity().ToString();
						if (artifact.GetCurTenacity() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
				default:
					break;
			}
		}
	}


	// 显示神器 Item 的铸魂信息。在铸魂界面调用
	public void ShowInfoSoul(Artifact artifact, int count)
	{
		_artifact = artifact;
		_tipsText.text = GameUtils.getString("relics_content1");

		if (artifact.GetArtifactDB().GetLevel() == 5)
		{
			bool rets = false;
			int[] recode = artifact.GetArtifactDB().m_IntoRecord;
			int[] heroNum = artifact.GetArtifactRow().getHeroNum();
			for (int i = 0; i < heroNum.Length; i++)
			{
				if (heroNum[i] > recode[i])
				{
					rets = true;
					break;
				}
			}

			if (rets)
			{
				_tipsText.text = GameUtils.getString("relics_content14");
			}
			else
			{
				_tipsText.text = GameUtils.getString("relics_content15");
			}
		}

		_atrifactName.text = GameUtils.getString(_artifact.GetArtifactRow().getName());
		int level = _artifact.GetArtifactRow().getLevel();
        ArtifactTemplate temp = (ArtifactTemplate)DataTemplate.GetInstance().m_ArtifactTable.getTableData(_artifact.GetArtifactRow().GetID() + 1);
        if (level < 5)
		{
			_nextName.text = GameUtils.getString(temp.getName());
		}
		else
		{
			_nextName.gameObject.SetActive(false);
		}

		// 英雄 ID
		int[] heroID = artifact.GetArtifactRow().getHeroID();
		int[] heroNumber = artifact.GetArtifactRow().getHeroNum();

		int[] intoRecord = artifact.GetArtifactDB().m_IntoRecord;
		int record = 0;
		for (int i = 0; i < intoRecord.Length; ++i)
		{
			record += intoRecord[i];
		}


		int total = 0;
		for (int i = 0; i < heroNumber.Length; ++i)
		{
			if(heroNumber[i] != -1)
				total += heroNumber[i];
		}

		float scale = count * 1.0f / total;

		bool ret = false;
		if (record + count == total)
		{
			ret = true;
		}

		// 神器头像
        _artImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + temp.getResourceName());
        _artImage.SetNativeSize();

		// 初始化属性数据
		int[] attrType = artifact.GetArtifactRow().getAttriType();
		int[] attrValue = artifact.GetArtifactRow().getAttriValue();
		for (int i = 0; i < attrType.Length; i++)
		{
			if (attrType[i] == -1)
			{
				continue;
			}

			_attributeList.transform.GetChild(i).gameObject.SetActive(true);
			Text attrNameText = _attributeList.transform.GetChild(i).FindChild("AttrName").GetComponent<Text>();
			Text attrValueText = _attributeList.transform.GetChild(i).FindChild("AttrValue").GetComponent<Text>();
			Text attrStateText = _attributeList.transform.GetChild(i).FindChild("AttrState").GetComponent<Text>();
            attrStateText.color = new Color(133.0f / 255, 247.0f / 255, 32.0f / 255);
			Text attrPriText = _attributeList.transform.GetChild(i).FindChild("AttrPri").GetComponent<Text>();
			if (count == 0)
			{
				attrPriText.gameObject.SetActive(false);
			}
			else
			{
				attrPriText.gameObject.SetActive(true);
			}
			attrPriText.text = "+" + (int)(artifact.GetArtifactRow().getAttriValue()[i] * scale);
			switch (attrType[i])
			{
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP:
					{
						// 生命值
						attrNameText.text = GameUtils.getString("baseattribute1des");
						attrValueText.text = artifact.GetCurHP().ToString(); 
						if (artifact.GetCurHP() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK:
					{
						// 物攻
						attrNameText.text = GameUtils.getString("hero_train_type2");
						attrValueText.text = artifact.GetCurPhysicalAttack().ToString();
						if (artifact.GetCurPhysicalAttack() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE:
					{
						// 物防
						attrNameText.text = GameUtils.getString("hero_train_type4");
						attrValueText.text = artifact.GetCurPhysicalDefence().ToString();
						if (artifact.GetCurPhysicalDefence() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK:
					{
						// 法攻
						attrNameText.text = GameUtils.getString("hero_train_type3");
						attrValueText.text = artifact.GetCurMagicAttack().ToString();
						if (artifact.GetCurMagicAttack() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE:
					{
						// 法防
						attrNameText.text = GameUtils.getString("hero_train_type5");
						attrValueText.text = artifact.GetCurMagicDefence().ToString();
						if (artifact.GetCurMagicDefence() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT:
					{
						// 命中
						attrNameText.text = GameUtils.getString("baseattribute6des");
						attrValueText.text = artifact.GetCurHit().ToString();
						if (artifact.GetCurHit() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE:
					{
						// 闪避
						attrNameText.text = GameUtils.getString("baseattribute7des");
						attrValueText.text = artifact.GetCurDodge().ToString();
						if (artifact.GetCurDodge() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL:
					{
						// 暴击
						attrNameText.text = GameUtils.getString("baseattribute8des");
						attrValueText.text = artifact.GetCurCritical().ToString();
						if (artifact.GetCurCritical() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY:
					{
						// 韧性
						attrNameText.text = GameUtils.getString("baseattribute9des");
						attrValueText.text = artifact.GetCurTenacity().ToString();
						if (artifact.GetCurTenacity() < attrValue[i])
						{
							string str = GameUtils.getString("relics_content5");
							attrStateText.text = string.Format(str, attrValue[i].ToString());
						}
						else
						{
							attrStateText.text = GameUtils.getString("relics_content6");

						}
					}
					break;
				default:
					break;
			}

			if (ret)
			{
				attrStateText.text = GameUtils.getString("relics_content6");
			}
		}
	}


	// 显示每一个英雄的数据
	public void ShowHeroData()
	{
		heroCountList.Clear();
		int[] heroID = _artifact.GetArtifactRow().getHeroID();
		int[] heroNumber = _artifact.GetArtifactRow().getHeroNum();
		// 每一个英雄数据
		for (int i = 0; i < heroID.Length; i++)
		{
			if (heroID[i] == -1)
			{
				continue;
			}

			HeroTemplate heroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroID[i]);
			GameObject cell = _heroLayout.transform.GetChild(i).gameObject;
			cell.SetActive(true);
			HeroCountItem item = cell.AddComponent<HeroCountItem>();
			item.ShowInfo(heroData, _artifact, heroNumber[i], i);
			heroCountList.Add(item);
		}
	}

	/// <summary>
	/// 现在铸魂需要的数量和拥有的数量
	/// </summary>
	public void SoulCount(ObjectCard card, int count)
	{
		for (int i = 0; i < heroCountList.Count; ++i)
		{
			HeroCountItem item = heroCountList[i];
			int tableID = card.GetHeroData().TableID / 10;
			if (tableID == item.tableID)
			{
				item.UpdateCount(count);
			}
		}
	}
	
	///////////////////// 按钮回调
	// 神器界面回调
	void OnClickArtifactPreview()
	{
		UI_HomeControler.Inst.AddUI(UI_ArtifactPreview.UI_ResPath);
		UI_ArtifactPreview.inst.ShowInfo(_artifact);
	}

	// 当点击铸魂的时候
	void OnClickArtifactSoul()
	{
		if (_artifact.GetArtifactDB().GetLevel() == 5)
		{
			bool ret = false;
			int[] recode = _artifact.GetArtifactDB().m_IntoRecord;
			int[] heroNum = _artifact.GetArtifactRow().getHeroNum();
			for (int i = 0; i < heroNum.Length; i++)
			{
				if (heroNum[i] > recode[i])
				{
					ret = true;
					break;
				}
			}

			if (!ret)
			{
				InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("relics_bubble5"), selfTransform.transform.parent.parent);
				return;
			}
		}

		UI_HomeControler.Inst.AddUI(UI_ArtifactSoul.UI_ResPath);
        UI_ArtifactSoul.inst.index = artiIndex;
		UI_ArtifactSoul.inst.ShowInfo(_artifact);
		
	}



	// 铸魂界面回调
	// TODO...
}
