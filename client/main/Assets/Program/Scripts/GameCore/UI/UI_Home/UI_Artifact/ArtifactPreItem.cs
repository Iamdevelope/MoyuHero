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
public class ArtifactPreItem : BaseUI
{
	public int index;

	private Text _artifactName;
	private Text _artifactState;
	private GameObject _heroLayout;
	private GameObject _attributeList;

	public override void InitUIData()
	{
		_artifactName = selfTransform.FindChild("Top/AtrifactName").GetComponent<Text>();
		_artifactState = selfTransform.FindChild("Top/ArtifactState").GetComponent<Text>();
		_heroLayout = selfTransform.FindChild("Center/HeroList/HeroLayout").gameObject;
		_attributeList = selfTransform.FindChild("Right/AttributeList").gameObject;
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	public void ShowInfoLevel(Artifact artifact, int level)
	{
		if (level < 5)
		{
			_artifactState.text = GameUtils.getString("relics_content9");
		}
		else
		{
			_artifactState.text = GameUtils.getString("relics_content8");
		}
		
		_artifactName.text = GameUtils.getString(artifact.GetArtifactRow().getName());

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
			HeroCountItem item = cell.AddComponent<HeroCountItem>();
			item.ShowInfo(heroData, artifact, heroNumber[i], i);
		}

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
					}
					break;
				default:
					break;
			}
		}
	}

	public void ShowInfoReach(ArtifactTemplate artifact, bool isReach = false)
	{
		_artifactName.text = GameUtils.getString(artifact.getName());

		if (isReach)
		{
			_artifactState.text = GameUtils.getString("relics_content8");
		}
		else
		{
			ArtifactTemplate temp = (ArtifactTemplate)DataTemplate.GetInstance().m_ArtifactTable.getTableData(artifact.getId() - 1);
			string str = GameUtils.getString("relics_content10");
			str = string.Format(str, GameUtils.getString(temp.getName()));
			_artifactState.text = str;
		}

		// 英雄 ID
		int[] heroID = artifact.getHeroID();
		int[] heroNumber = artifact.getHeroNum();
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
			item.ShowInfo(artifact, i, isReach);
		}

		Show(artifact, isReach);
	}

	void Show(ArtifactTemplate artifact, bool isReach)
	{

		// 英雄 ID
		int[] heroID = artifact.getHeroID();
		int[] heroNumber = artifact.getHeroNum();
		
		// 初始化属性数据
		int[] attrType = artifact.getAttriType();
		int[] attrValue = artifact.getAttriValue();
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
			if (isReach)
			{
				attrValueText.text = attrValue[i].ToString();
			}
			else
			{
				attrValueText.text = "0";
				string str = GameUtils.getString("relics_content5");
				attrStateText.text = string.Format(str, attrValue[i].ToString());
			}
			switch (attrType[i])
			{
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP:
					{
						attrNameText.text = GameUtils.getString("baseattribute1des");
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK:
					{
						attrNameText.text = GameUtils.getString("hero_train_type2");
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE:
					{
						attrNameText.text = GameUtils.getString("hero_train_type4");
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK:
					{
						attrNameText.text = GameUtils.getString("hero_train_type3");
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE:
					{
						attrNameText.text = GameUtils.getString("hero_train_type5");
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT:
					{
						attrNameText.text = GameUtils.getString("baseattribute6des");
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE:
					{
						attrNameText.text = GameUtils.getString("baseattribute7des");
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL:
					{
						attrNameText.text = GameUtils.getString("baseattribute8des");
					}
					break;
                case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY:
					{
						attrNameText.text = GameUtils.getString("baseattribute9des");
					}
					break;
				default:
					break;
			}
		}
	}

}
