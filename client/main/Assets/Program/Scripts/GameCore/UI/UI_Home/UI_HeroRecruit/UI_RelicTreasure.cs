using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using GNET;
using DreamFaction.UI;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;

public class UI_RelicTreasure : UI_RelicTreasureBase
{
	public static UI_RelicTreasure Inst;
	GridLayoutGroup m_ItemLayout;
	GridLayoutGroup m_RightLayout;

	int m_CurFlag = -1;  // 现在要发的消息的标志

	public override void InitUIData()
	{
		Inst = this;
		base.InitUIData();
		m_ItemLayout = selfTransform.FindChild("UI_Center/Center/ItemLayout").GetComponent<GridLayoutGroup>();
		m_RightLayout = selfTransform.FindChild("UI_Center/Right/Layout").GetComponentInParent<GridLayoutGroup>();

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Guide_Continue, ShowNewGuide);
	}

    /// <summary>
    /// 监测新手引导 点击继续
    /// </summary>
    /// <param name="e"></param>
    private void ShowNewGuide(GameEvent e)
    {
        int _id = (int)e.data;
        if (_id != -1)
        {
            //新手引导相关 【点击继续】开启遗迹宝藏（非强制）
            GuideManager.GetInstance().ShowGuideWithIndex(_id);
        }
        else
        {
            GuideManager.GetInstance().StopGuide();
        }
    }

	public override void InitUIView()
	{
		base.InitUIView();

		m_BuyOneText.text = GameUtils.getString("hero_recruit_content5");
		m_BuyTenText.text = GameUtils.getString("hero_recruit_content6");

		m_TopTipsText.text = GameUtils.getString("treasure_content5");

		InitTipsInfo();

		UpdateCenterItem();
		UpdateRightItem();
        if (GuideManager.GetInstance().GetBackCount(200301))
        {
            GuideManager.GetInstance().ShowGuideWithIndex(200302);
        }
	}

	void InitTipsInfo()
	{
        transform.FindChild("UI_Bottom/Tips/Text").GetComponent<Text>().text = GameUtils.getString("treasure_content6");

		// refresh
		m_RefreshTipsText.gameObject.SetActive(false);
		m_RefreshBtn.transform.FindChild("Image").gameObject.SetActive(true);
		m_ReDiamondNum.gameObject.SetActive(true);
		m_ReDiamondNum.text = DataTemplate.GetInstance().m_GameConfig.getTreasure_refresh_cost().ToString();

		if (ObjectSelf.GetInstance().ishavefree == 0)
		{
			// buy one
			m_BuyOneTipsText.gameObject.SetActive(true);
			m_BuyOneDiamondNum.gameObject.SetActive(false);
			m_BuyOneBtn.transform.FindChild("Image").gameObject.SetActive(false);
			m_BuyOneDiamondNum.text = DataTemplate.GetInstance().m_GameConfig.getTreasure_single_cost().ToString();
		}
		else
		{
			// buy one
			m_BuyOneTipsText.gameObject.SetActive(false);
			m_BuyOneDiamondNum.gameObject.SetActive(true);
			m_BuyOneBtn.transform.FindChild("Image").gameObject.SetActive(true);
			m_BuyOneDiamondNum.text = DataTemplate.GetInstance().m_GameConfig.getTreasure_single_cost().ToString();
		}

		// gain gold
		m_ReGoldNum.text = DataTemplate.GetInstance().m_GameConfig.getTreasure_refresh_reward().ToString();
		m_BuyOneGoldNum.text = DataTemplate.GetInstance().m_GameConfig.getTreasure_single_reward().ToString();
		m_BuyTenGoldNum.text = DataTemplate.GetInstance().m_GameConfig.getTreasure_ten_reward().ToString();

		// buy ten 
		m_BuyTenDiamondNum.text = DataTemplate.GetInstance().m_GameConfig.getTreasure_ten_cost().ToString();
	}

	// 更新界面中间 Item 显示
	void UpdateCenterItem()
	{
		for (int i = 1; i <= ObjectSelf.GetInstance().lotteryitemmap.Count; i++)
		{
			if (ObjectSelf.GetInstance().lotteryitemmap.ContainsKey(i))
			{
				LotteryItemlayer lotery = ObjectSelf.GetInstance().lotteryitemmap[i] as LotteryItemlayer;
				if (lotery != null)
				{
					GameObject item = m_ItemLayout.transform.GetChild(i - 1).gameObject;
					item.SetActive(true);
					string str = "treasure_content" + i.ToString();
					item.transform.FindChild("LayerText").GetComponent<Text>().text = GameUtils.getString(str);
					GameObject Goods = item.transform.FindChild("Goods").gameObject;  // goods 

					if (ObjectSelf.GetInstance().mapkey == i)
					{
						item.transform.FindChild("BgIndex").gameObject.SetActive(true);
					}
					else
					{
                        item.transform.FindChild("BgIndex").gameObject.SetActive(false);
					}

					int j = 0;
					foreach (var lotteryItem in lotery.lotteryitemlist)
					{
						LotteryItem lotItem = lotteryItem;

						RuintreasureTemplate temp = (RuintreasureTemplate)DataTemplate.GetInstance().m_RuintreasureTable.getTableData(lotteryItem.id);

						GameObject goods = Goods.transform.GetChild(lotItem.viewnum - 1).gameObject;
						goods.gameObject.SetActive(true);

						// 取消显示所有星级
						GameObject starLevel = goods.transform.Find("UI_Star_Level").gameObject;
						starLevel.SetActive(false);
						for (int k = 0; k < 5; ++k)
						{
							starLevel.transform.GetChild(k).gameObject.SetActive(false);
						}


						if (lotItem.isget == 1)   // 已领取
						{
							GameObject reached = goods.transform.FindChild("Reached").gameObject;
							reached.SetActive(true);

							// 特殊事件，显示特殊字
							if (lotItem.superid == 100001 || lotItem.superid == 100008 || lotItem.superid == 100009)
							{
								reached.transform.FindChild("TipsText").GetComponent<Text>().text = "X3";
							}
							else if (lotItem.superid == 100002)
							{
								reached.transform.FindChild("TipsText").GetComponent<Text>().text = "X2";
							}
							else
							{
								reached.transform.FindChild("TipsText").GetComponent<Text>().text = GameUtils.getString("shop_content29");
							}
						}
						else
						{
							GameObject reached = goods.transform.FindChild("Reached").gameObject;
							reached.SetActive(false);
						}

						// 事件
						if (temp.getType() == 2)
						{
							Image icon = goods.transform.FindChild("Icon").GetComponent<Image>();
                            icon.gameObject.SetActive(false);
                            goods.transform.FindChild("qiyu").gameObject.SetActive(true);
							//icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_baozang_6");
							//icon.sprite = Resources.Load<Sprite>(temp.getIcon());
							goods.transform.FindChild("NumberText").gameObject.SetActive(false);
						}
						else  // 物品
						{
                            goods.transform.FindChild("qiyu").gameObject.SetActive(false);
							Image icon = goods.transform.FindChild("Icon").GetComponent<Image>();
                            icon.gameObject.SetActive(true);
							//icon.sprite = Resources.Load<Sprite>("UI/Sprites/UI_baozang_6.png");

							int ID = temp.getParameter1();
							EM_OBJECT_CLASS type = GameUtils.GetObjectClassById(ID);

							if (type == EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON || type == EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE)  // 道具 对应数据表26
							{
								ItemTemplate itemTemp = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(ID);
								icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + itemTemp.getIcon_s());

								// 显示符文星级

								if (type == EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE)
								{
									int quality = itemTemp.getRune_quality();
									GameObject star = goods.transform.Find("UI_Star_Level").gameObject;
									star.SetActive(true);
									for (int k = 0; k < quality; ++k)
									{
										star.transform.GetChild(k).gameObject.SetActive(true);
									}
								}



								//Debug.Log(i + "   " + j + "   EM_OBJECT_CLASS_COMMON " + temp.getId() + "   " + itemTemp.getIcon_s() + "   " + lotteryItem.id);
							}
							else if (type == EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES)   // 资源 对应数据表53
							{
								ResourceindexTemplate resTemp = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(ID);

								icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + resTemp.getIcon3());

								//Debug.Log(i + "   " + j + "   EM_OBJECT_CLASS_RES " + temp.getId() + "   " + resTemp.getIcon3() + "   " + lotteryItem.id);
							}
							else
							{
								Debug.Log(i + "   " + j + "   other " + temp.getId() + " " + lotteryItem.id);
							}

							goods.transform.FindChild("NumberText").gameObject.SetActive(true);
							goods.transform.FindChild("NumberText").GetComponent<Text>().text = temp.getParameter2();
						}


						j++;
					}
				}
			}
		}
	}

	// 更新界面右边 Item 显示
	void UpdateRightItem()
	{
		for (int i = 0; i < m_RightLayout.transform.childCount; ++i)
		{
			m_RightLayout.transform.GetChild(i).gameObject.SetActive(false);
		}

		// 是否有月卡专属
		int count = 0;
		if (ObjectSelf.GetInstance().ismonthfirsthave == 1)
		{
			Text tipsText = m_RightLayout.transform.GetChild(count).GetComponent<Text>();
			tipsText.text = GameUtils.getString("treasure_event11_des");
			tipsText.gameObject.SetActive(true);
			count += 1;
		}

		bool ret = false;
		foreach (var item in ObjectSelf.GetInstance().superlist)
		{
			RuintreasureTemplate temp = (RuintreasureTemplate)DataTemplate.GetInstance().m_RuintreasureTable.getTableData(item);
			if (temp != null)
			{
				if (temp.getId() == 100007)
				{
					// 
					//TODO...
					ret = true;
				}

				Text tipsText = m_RightLayout.transform.GetChild(count).GetComponent<Text>();
				tipsText.text = GameUtils.getString(temp.getParameter2());
				tipsText.gameObject.SetActive(true);
			}

			count++;
		}

		// 本次免费
		if (ret)
		{
			m_RefreshTipsText.gameObject.SetActive(true);
			m_RefreshBtn.transform.FindChild("Image").gameObject.SetActive(false);
			m_ReDiamondNum.gameObject.SetActive(false);
		}
		else
		{
			m_RefreshTipsText.gameObject.SetActive(false);
			m_RefreshBtn.transform.FindChild("Image").gameObject.SetActive(true);
			m_ReDiamondNum.gameObject.SetActive(true);
		}
	}


	public override void UpdateUIData()
	{
		base.UpdateUIData();


	}

	protected override void OnClickRefreshBtn()
	{
		// 金币不足
		if (TipsGoldValue(DataTemplate.GetInstance().m_GameConfig.getTreasure_refresh_cost()))
		{
			return;
		}

		m_CurFlag = CLotteryItem.REFRESH;
		SendMessage();
	}

	protected override void OnClickBuyOneBtn()
	{
		if (ObjectSelf.GetInstance().ishavefree == 0)
		{
			m_CurFlag = CLotteryItem.FREE;
			SendMessage();
			return;

		}

		// 金币不足
		if (TipsGoldValue(DataTemplate.GetInstance().m_GameConfig.getTreasure_single_cost()))
		{
			return;
		}

		m_CurFlag = CLotteryItem.ONE;

		SendMessage();
	}

	protected override void OnClickBuyTenBtn()
	{
		// 金币不足
		if (TipsGoldValue(DataTemplate.GetInstance().m_GameConfig.getTreasure_ten_cost()))
		{
			return;
		}

		m_CurFlag = CLotteryItem.TEN;
		SendMessage();
	}

	public bool TipsGoldValue(int goldValue)
	{
		// 元宝是否足够
		if (ObjectSelf.GetInstance().Gold < goldValue)
		{
			UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
			box.SetIsNeedDescription(false);
			box.SetDescription_text(GameUtils.getString("quickly_pay"));
			box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
			box.SetLeftClick(OnClickRechargeBtn);
			box.SetRightBtn_text(GameUtils.getString("common_button_cancel"));
			box.SetRightClick(OnClickCancelBtn);

			return true;
		}

		return false;
	}

	// 成功买到一个
	public void SuccessBuyOne(LinkedList<LotteryItemget> itemlist)
	{
		string str = "";
		foreach (var item in itemlist)
		{
			if (item.id >= 100001 && item.id <= 100010)
			{
				RuintreasureTemplate temp = (RuintreasureTemplate)DataTemplate.GetInstance().m_RuintreasureTable.getTableData(item.id);
				str += GameUtils.getString(temp.getParameter2());				
			}
		}

		string tips = string.Format(GameUtils.getString("treasure_tip1"), DataTemplate.GetInstance().m_GameConfig.getTreasure_single_reward().ToString());
        RichText text = RichText.GetRichText(tips);
        InterfaceControler.GetInst().AddMsgBox(text.transform, selfTransform.transform.parent);

		InitTipsInfo();
		UpdateCenterItem();
		UpdateRightItem();
	}

	// 成功买到十个
	public void SuccessBuyTen(LinkedList<LotteryItemget> itemlist)
	{
		string str = "";
		foreach (var item in itemlist)
		{
			if (item.id >= 100001 && item.id <= 100010)
			{
				RuintreasureTemplate temp = (RuintreasureTemplate)DataTemplate.GetInstance().m_RuintreasureTable.getTableData(item.id);
				str += GameUtils.getString(temp.getParameter2());
			}
		}

		string tips = string.Format(GameUtils.getString("treasure_tip1"), DataTemplate.GetInstance().m_GameConfig.getTreasure_ten_reward().ToString());
        RichText text = RichText.GetRichText(tips);
        InterfaceControler.GetInst().AddMsgBox(text.transform, selfTransform.transform.parent);

		UpdateCenterItem();
		UpdateRightItem();

		// TODO...
		//foreach (var item in itemlist)
		//{
		//	string str = string.Format(GameUtils.getString("treasure_tip1"), item.num);
		//	InterfaceControler.GetInst().AddMsgBox(str, selfTransform.transform.parent);

		//	UpdateCenterItem();
		//	UpdateRightItem();
		//}
	}

	// 成功刷新
	public void SuccessRefresh(LinkedList<LotteryItemget> itemlist)
	{
		string str = string.Format(GameUtils.getString("treasure_tip1"), DataTemplate.GetInstance().m_GameConfig.getTreasure_refresh_reward().ToString());
        RichText text = RichText.GetRichText(str);
        InterfaceControler.GetInst().AddMsgBox(text.transform, selfTransform.transform.parent);

		InitTipsInfo();
		UpdateCenterItem();
		UpdateRightItem();
	}

	// 去充值界面
	void OnClickRechargeBtn()
	{
		UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
		UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
	}

	void OnClickCancelBtn()
	{
		UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
	}

	// 向服务器发送消息
	void SendMessage()
	{
		CLotteryItem proto = new CLotteryItem();
		proto.lotterytype = m_CurFlag;
		IOControler.GetInstance().SendProtocol(proto);
	}

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Guide_Continue, ShowNewGuide);
    }
    
    
}
