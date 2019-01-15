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

public class UI_ArtifactAddInfo : BaseUI
{
	public static UI_ArtifactAddInfo inst;
	public static string UI_ResPath = "UI_Artifact/UI_ArtifactInfo_2_1";

	private GameObject _attributeList;
	private Button _closeBtn;
	private Text _tipsText; 

	private List<Artifact> _artifactList = new List<Artifact>();

	public override void InitUIData()
	{
		inst = this;

		_attributeList = selfTransform.FindChild("Attribute/AttributeList").gameObject;
		_closeBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
		_closeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
		_tipsText = selfTransform.FindChild("TipsText").GetComponent<Text>();
		_tipsText.text = GameUtils.getString("relics_content7");
		_tipsText.gameObject.SetActive(false);
	}

	public override void InitUIView()
	{
		base.InitUIView();

		Dictionary<int, Artifact> _map = ObjectSelf.GetInstance().ArtifactContainerBag.GetArtifactMap();
		for (int i = 0; i <= _map.Count; i++)
		{
			if (_map.ContainsKey(i))
			{
				_artifactList.Add(_map[i]);
			}
		}

		ShowInfo();
	}

	// 显示神器的所有属性
	public void ShowInfo()
	{
		bool ret = true;
		for (int i = 1; i < 10; i++)
		{
			_attributeList.transform.GetChild(i - 1).gameObject.SetActive(true);
			Text attrNameText = _attributeList.transform.GetChild(i - 1).FindChild("AttrName").GetComponent<Text>();
			Text attrValueText = _attributeList.transform.GetChild(i -1).FindChild("AttrValue").GetComponent<Text>();
			switch (i)
			{
				case 1:
					{
						// 生命值
						attrNameText.text = GameUtils.getString("baseattribute1des");
						int count = 0;
						for (int j = 0; j < _artifactList.Count; j++)
						{
							count += _artifactList[j].GetMaxHP();
						}

						if (count > 0)
						{
							ret = false;
						}
						attrValueText.text = "+" + count;
					}
					break;
				case 2:
					{
						// 物攻
						attrNameText.text = GameUtils.getString("hero_train_type2");
						int count = 0;
						for (int j = 0; j < _artifactList.Count; j++)
						{
							count += _artifactList[j].GetPhysicalAttack();
						}
						if (count > 0)
						{
							ret = false;
						}
						attrValueText.text = "+" + count;
					}
					break;
				case 3:
					{
						// 物防
						attrNameText.text = GameUtils.getString("hero_train_type4");
						int count = 0;
						for (int j = 0; j < _artifactList.Count; j++)
						{
							count += _artifactList[j].GetPhysicalDefence();
						} 
						if (count > 0)
						{
							ret = false;
						}
						attrValueText.text = "+" + count;
					}
					break;
				case 4:
					{
						// 法攻
						attrNameText.text = GameUtils.getString("hero_train_type3");
						int count = 0;
						for (int j = 0; j < _artifactList.Count; j++)
						{
							count += _artifactList[j].GetMagicAttack();
						}
						if (count > 0)
						{
							ret = false;
						}
						attrValueText.text = "+" + count;
					}
					break;
				case 5:
					{
						// 法防
						attrNameText.text = GameUtils.getString("hero_train_type5");
						int count = 0;
						for (int j = 0; j < _artifactList.Count; j++)
						{
							count += _artifactList[j].GetMagicDefence();
						}
						if (count > 0)
						{
							ret = false;
						}
						attrValueText.text = "+" + count;
					}
					break;
				case 6:
					{
						// 命中
						attrNameText.text = GameUtils.getString("baseattribute6des");
						int count = 0;
						for (int j = 0; j < _artifactList.Count; j++)
						{
							count += _artifactList[j].GetHit();
						}
						if (count > 0)
						{
							ret = false;
						}
						attrValueText.text = "+" + count;
					}
					break;
				case 7:
					{
						// 闪避
						attrNameText.text = GameUtils.getString("baseattribute7des");
						int count = 0;
						for (int j = 0; j < _artifactList.Count; j++)
						{
							count += _artifactList[j].GetDodge();
						}
						if (count > 0)
						{
							ret = false;
						}
						attrValueText.text = "+" + count;
					}
					break;
				case 8:
					{
						// 暴击
						attrNameText.text = GameUtils.getString("baseattribute8des");
						int count = 0;
						for (int j = 0; j < _artifactList.Count; j++)
						{
							count += _artifactList[j].GetCritical();
						}
						if (count > 0)
						{
							ret = false;
						}
						attrValueText.text = "+" + count;
					}
					break;
				case 9:
					{
						// 韧性
						attrNameText.text = GameUtils.getString("baseattribute9des");
						int count = 0;
						for (int j = 0; j < _artifactList.Count; j++)
						{
							count += _artifactList[j].GetTenacity();
						}
						if (count > 0)
						{
							ret = false;
						}
						attrValueText.text = "+" + count;
					}
					break;
				default:
					break;
			}

			if (ret)
			{
				_tipsText.gameObject.SetActive(true);
				_attributeList.SetActive(false);

                UI_HomeControler.Inst.AddUI(UI_ArtifactAddInfoNull.UI_ResPath);
                UI_HomeControler.Inst.ReMoveUI(gameObject);
			}
		}
	}
	
	void OnClickCloseBtn()
	{
		UI_HomeControler.Inst.ReMoveUI(gameObject);
	}
}
